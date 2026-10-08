using MediatR;
using Core.Domain.Entities;
using Core.Application.Common.Mappings;
using Core.Shared;
using Core.Application.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;
using Core.Application.Helper;
using Core.Application.Request;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Application.Features.Users.Commands.UpdateUser
{
    public record UpdateUserCommand : IRequest<Result<int>>, IMapFrom<User>
    {
        [Required]
        public int UserSerialID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Please note that the user name should not contain more than 50 characters.")]
        public string? UserName { get; set; }

        public string? PermissionType { get; set; }
        public string? Status { get; set; }
        public int? GrpSerialID { get; set; }
        [Required]
        public string? PassWd { get; set; }
        [NotMapped]
        public int[]? Companies { get; set; }
        [NotMapped]
        public int[]? MenuPermission { get; set; }

    }

    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly string _pepper;
        private readonly int _iteration = 3;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _pepper = Environment.GetEnvironmentVariable("PasswordHashExamplePepper");
            _userRepository = userRepository;
        }

        public async Task<Result<int>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            UpdateUserCommandValidator validator = new UpdateUserCommandValidator(_userRepository);

            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(errors);
            }

            try
            {
                var existingUser = await _unitOfWork.Repository<User>().GetByIdAsync(command.UserSerialID);
                if (existingUser == null)
                {
                    return await Result<int>.FailureAsync(new List<string> { "Could not be found." });
                }

                var previousPermissionType = existingUser.PermissionType;
                var newPermissionType = (PermissionType)EnumConverter.StringToEnum<PermissionType>(command.PermissionType)!;
                var permissionTypeChanged = previousPermissionType != newPermissionType;

                existingUser.UserName = command.UserName;
                existingUser.PermissionType = newPermissionType;
                existingUser.Active = EnumConverter.ConvertEnumStringToNumber<StatusType>(command.Status) == 1;
                existingUser.GrpSerialID = command.GrpSerialID;

                if (!string.IsNullOrWhiteSpace(command.PassWd) && command.PassWd != "DH@12345678")
                {
                    existingUser.PassWd = command.PassWd;
                    existingUser.PasswdSalt = PasswordHasher.GenerateSalt();
                    existingUser.PasswdHash = PasswordHasher.ComputeHash(command.PassWd, existingUser.PasswdSalt, _pepper, _iteration);
                }

                var currentMenuPermissions = await _unitOfWork.Repository<UserMenuPermission>()
                    .GetAllAsync(c => c.UserSerialID == command.UserSerialID);

                if (permissionTypeChanged)
                {
                    SoftDeleteAllMenuPermissions(currentMenuPermissions);
                }

                if (newPermissionType == PermissionType.Individual || command.MenuPermission != null)
                {
                    SyncMenuPermissions(existingUser, currentMenuPermissions, command);
                }

                await _unitOfWork.Repository<UserMenuPermission>().UpdateRangeAsync(currentMenuPermissions);

                var currentCompanies = await _unitOfWork.Repository<UserCompany>()
                    .GetAllAsync(c => c.UserSerialID == command.UserSerialID);
                var currentCompanyIds = currentCompanies
                    .Where(c => !c.IsDeleted && c.Active)
                    .Select(uc => uc.ComSerialID)
                    .ToHashSet();

                var addCompanyIds = (command.Companies ?? Array.Empty<int>()).Distinct().ToHashSet();
                var newCompanyIdsToAdd = addCompanyIds.Except(currentCompanyIds).ToList();
                var updateCompanyIds = currentCompanyIds.Intersect(addCompanyIds).ToList();
                var deleteCompanyIds = currentCompanyIds.Except(addCompanyIds).ToList();

                foreach (var companyId in updateCompanyIds)
                {
                    var existingCompany = currentCompanies.FirstOrDefault(m => m.ComSerialID == companyId && m.IsDeleted);
                    if (existingCompany != null)
                    {
                        existingCompany.IsDeleted = false;
                        existingCompany.Active = true;
                    }
                }

                foreach (var newCompanyId in newCompanyIdsToAdd)
                {
                    var newUserCompany = new UserCompany
                    {
                        ComSerialID = newCompanyId,
                        UserSerialID = command.UserSerialID,
                        Active = true,
                        IsDeleted = false
                    };
                    existingUser.Companies.Add(newUserCompany);
                }

                foreach (var userCompany in currentCompanies.Where(uc => deleteCompanyIds.Contains(uc.ComSerialID)))
                {
                    userCompany.IsDeleted = true;
                    userCompany.Active = false;
                }

                await _unitOfWork.Repository<User>().UpdateAsync(existingUser, existingUser.UserSerialID);
                existingUser.AddDomainEvent(new UserUpdatedEvent(existingUser));

                await _unitOfWork.Save(cancellationToken);
                return await Result<int>.SuccessAsync(existingUser.UserSerialID, "Modified successfully.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return await Result<int>.FailureAsync(new List<string> { ex.Message + ex.InnerException });
            }
        }

        private static void SoftDeleteAllMenuPermissions(List<UserMenuPermission> currentMenuPermissions)
        {
            foreach (var permission in currentMenuPermissions.Where(p => !p.IsDeleted))
            {
                permission.IsDeleted = true;
                permission.Active = false;
            }
        }

        private static void SyncMenuPermissions(
            User existingUser,
            List<UserMenuPermission> currentMenuPermissions,
            UpdateUserCommand command)
        {
            var desiredMenuIds = (command.MenuPermission ?? Array.Empty<int>()).Distinct().ToHashSet();
            var activeMenuIds = currentMenuPermissions
                .Where(p => !p.IsDeleted && p.Active)
                .Select(p => p.MnuID)
                .ToHashSet();

            var menuIdsToRemove = activeMenuIds.Except(desiredMenuIds).ToList();

            foreach (var menuId in desiredMenuIds)
            {
                var existingPermission = currentMenuPermissions.FirstOrDefault(p => p.MnuID == menuId);
                if (existingPermission == null)
                {
                    var newPermission = new UserMenuPermission
                    {
                        MnuID = menuId,
                        UserSerialID = command.UserSerialID,
                        GrpSerialID = command.GrpSerialID,
                        Active = true,
                        IsDeleted = false
                    };
                    existingUser.MenuPermissions.Add(newPermission);
                    currentMenuPermissions.Add(newPermission);
                    continue;
                }

                if (existingPermission.IsDeleted || !existingPermission.Active)
                {
                    existingPermission.IsDeleted = false;
                    existingPermission.Active = true;
                    existingPermission.GrpSerialID = command.GrpSerialID;
                }
            }

            foreach (var permission in currentMenuPermissions.Where(p => menuIdsToRemove.Contains(p.MnuID) && !p.IsDeleted))
            {
                permission.IsDeleted = true;
                permission.Active = false;
                permission.GrpSerialID = command.GrpSerialID;
            }
        }
    }
}
