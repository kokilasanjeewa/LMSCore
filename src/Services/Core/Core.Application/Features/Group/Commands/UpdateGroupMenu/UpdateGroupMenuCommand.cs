using Core.Application.Common.Mappings;
using Core.Application.Features.Group.Commands.CreateGroup;
using Core.Application.Features.Group.Commands.UpdateGroup;
using Core.Application.Helper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Request;
using Core.Domain.Entities;
using Core.Shared;
using Dapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Data;


namespace Core.Application.Features.Group.Commands.UpdateGroupMenu
{
    public class UpdateGroupMenuCommand : IRequest<Result<int>>, IMapFrom<GroupMenu>
    {
        [Required]
        public int GrpSerialID { get; set; }

        [Required]
        public int UserSerialID { get; set; }

        [Required]
        [Display(Name = "Group Name")]
        [StringLength(25, ErrorMessage = "The group name cannot exceed 25 characters.")]
        public string? GroupName { get; set; }

        [Required]
        public int[]? MenuPermission { get; set; }
        public string? Status { get; set; }

    }

    internal class UpdateGroupMenuCommandHandler : IRequestHandler<UpdateGroupMenuCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _groupRepository;
        private readonly ISqlDataAccess _sqlDataAccess;

        public UpdateGroupMenuCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository, ISqlDataAccess sqlDataAccess)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
            _sqlDataAccess = sqlDataAccess;
        }
        public async Task<Result<int>> Handle(UpdateGroupMenuCommand command, CancellationToken cancellationToken)
        {
            // Validation
            UpdateGroupMenuCommandValidator validator = new UpdateGroupMenuCommandValidator(_groupRepository);
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(messages: errors);
            }
            else
            {
                try
                {
                    // Retrieve the existing Group entity
                    var group = await _unitOfWork.Repository<Domain.Entities.Group>()
                                                  .GetByIdAsync(command.GrpSerialID);
                    if (group == null)
                    {
                        return await Result<int>.FailureAsync(message: "Group not found.");
                    }

                    // Update the group's properties
                    group.GropName = command.GroupName;
                    group.Active = EnumConverter.ConvertEnumStringToNumber<StatusType>(command.Status) == 1;
                    group.IsDeleted = !group.Active;
                    group.AddDomainEvent(new GroupUpdatedEvent(group));

                    // Handle GroupMenu entities
                    var currentMenuPermissions = await _unitOfWork.Repository<GroupMenu>()
                                                              .GetAllFindAsync(gm => gm.GrpSerialID == command.GrpSerialID);
                    var currentMenuIds = currentMenuPermissions.Select(uc => uc.MnuID).ToHashSet();
                    // Get distinct menu IDs to add from the command
                    var addMenuIds = command.MenuPermission.Distinct().ToHashSet();

                    // Determine which menu IDs need to be added
                    var newMenuIdsIdsToAdd = addMenuIds.Except(currentMenuIds).ToList();

                    // Determine which menu IDs need to be updated
                    var updateMenuIds = currentMenuIds.Intersect(addMenuIds).ToList();

                    // Determine which menu IDs need to be marked as deleted
                    var deleteMenuIds = currentMenuIds.Except(addMenuIds).ToList();

                    // Update existing associations if they were marked as deleted
                    foreach (var menuId in updateMenuIds)
                    {
                        var existingMenu = currentMenuPermissions.FirstOrDefault(m => m.MnuID == menuId && m.IsDeleted);
                        if (existingMenu != null)
                        {
                            existingMenu.IsDeleted = false;
                            existingMenu.Active = true;
                            existingMenu.GrpSerialID = command.GrpSerialID;
                        }
                    }

                    // Add new associations
                    foreach (var newMenuId in newMenuIdsIdsToAdd)
                    {
                        // Add new GroupMenu
                        var newGroupMenu = new GroupMenu()
                        {
                            Active = true,
                            GrpSerialID = group.GrpSerialID,
                            MnuID = newMenuId,
                            IsDeleted = false,
                        };
                        currentMenuPermissions.Add(newGroupMenu);
                    }

                    // Set IsDeleted to true and Active to false for old associations
                    foreach (var groupMenuPermission in currentMenuPermissions.Where(uc => deleteMenuIds.Contains(uc.MnuID)))
                    {
                        groupMenuPermission.IsDeleted = true;
                        groupMenuPermission.Active = false;
                    }

                    // Update and add GroupMenus
                    await _unitOfWork.Repository<GroupMenu>().UpdateRangeAsync(currentMenuPermissions);

                    // Save changes to the database
                    var saveResult = await _unitOfWork.Save(cancellationToken);

                    // Check if the save was successful
                    if (saveResult > 0)
                    {
                        // The save operation was successful, so you can proceed to call another function
                        var parameters = new DynamicParameters();
                        parameters.Add("@GrpSerialID", command.GrpSerialID, DbType.Int32);
                        parameters.Add("@UserSerialID", command.UserSerialID, DbType.Int32);
                        parameters.Add("@AddMnuSerialIDs", string.Join(",", newMenuIdsIdsToAdd) , DbType.String);
                        parameters.Add("@EditMnuSerialIDs", string.Join(",", updateMenuIds), DbType.String);
                        parameters.Add("@DeleteMnuSerialIDs", string.Join(",", deleteMenuIds), DbType.String);
                        // Mark existing menu associations as add, edit, and update in user menu permissions
                        await _sqlDataAccess.SaveData("[dbo].[ManageUserMenuPermissions]", parameters);
                    }
                    return await Result<int>.SuccessAsync(data: currentMenuPermissions.Count, message: "Modified successfully.");
                }
                catch (Exception ex)
                {
                    await _unitOfWork.Rollback();
                    var errorMessage = ex.Message + (ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                    return await Result<int>.FailureAsync(message: errorMessage);
                }
            }
        }

    }

}


