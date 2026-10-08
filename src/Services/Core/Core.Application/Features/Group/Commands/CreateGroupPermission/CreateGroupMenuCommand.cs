using Core.Application.Common.Mappings;
using Core.Application.Features.Group.Commands.CreateGroup;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Core.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Group.Commands.CreateGroupPermission
{

    public class CreateGroupMenuCommand : IRequest<Result<int>>, IMapFrom<GroupMenu>
    {
        [Required]
        [Display(Name = "Group Name")]
        [StringLength(25, ErrorMessage = "The group name cannot exceed 25 characters. ")]
        public string? GroupName { get; set; }
        [Required]
        public int[]? MenuPermission { get; set; }
    }
    internal class CreateGroupMenuCommandHandler : IRequestHandler<CreateGroupMenuCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _groupRepository;

        public CreateGroupMenuCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
        }

        public async Task<Result<int>> Handle(CreateGroupMenuCommand command, CancellationToken cancellationToken)
        {
            // Validation
            CreateGroupMenuCommandValidator validator = new CreateGroupMenuCommandValidator(_groupRepository);
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(messages: errors);
            }
            else
            {
                try
                {
                    var group = new Domain.Entities.Group
                    {
                        GropName = command.GroupName,
                        Active = true,
                        IsDeleted = false
                    };
                    // Save the Group first to ensure GrpSerialID is set
                    await _unitOfWork.Repository<Domain.Entities.Group>().AddAsync(group);
                    group.AddDomainEvent(new GroupCreatedEvent(group));
                    await _unitOfWork.Save(cancellationToken);

                    var groupMenus = new List<GroupMenu>();

                    foreach (var serialId in command.MenuPermission)
                    {
                        var groupmenu = new GroupMenu()
                        {
                            Active = true,
                            GrpSerialID = group.GrpSerialID, // GrpSerialID should now be set
                            MnuID = serialId,
                            IsDeleted = false,
                        };
                        groupmenu.AddDomainEvent(new GroupMenuCreatedEvent(groupmenu));
                        groupMenus.Add(groupmenu);
                    }
                    // Save the Group first to ensure GrpSerialID is set
                    await _unitOfWork.Repository<GroupMenu>().AddRangeAsync(groupMenus);
                    await _unitOfWork.Save(cancellationToken);

                    return await Result<int>.SuccessAsync(data: groupMenus.Count, message: $"Saved successfully.");
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
