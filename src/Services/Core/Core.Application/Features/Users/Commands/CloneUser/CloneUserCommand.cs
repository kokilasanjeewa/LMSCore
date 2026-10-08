using AutoMapper;
using Core.Application.Common.Mappings;
using Core.Application.Features.Users.Commands.CreateUser;
using Core.Application.Helper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Request;
using Core.Domain.Entities;
using Core.Shared;
using MediatR;
using System.ComponentModel.DataAnnotations;


namespace Core.Application.Features.Users.Commands.CloneUser
{
    public record CloneUserCommand : IRequest<Result<int>>, IMapFrom<User>
    {
        [Required]
        public int UserSerialID { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z][A-Za-z]*$", ErrorMessage = "The userid must begin with a capital letter and contain no spaces.")]
        public string? CloneToUserID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Please note that the user name should not contain more than 50 characters.")]
        public string? UserName { get; set; }
        [Required]
        public string? PassWd { get; set; }
        [Required]
        public int EESerialID { get; set; }
    }

    internal class CloneUserCommandHandler : IRequestHandler<CloneUserCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly string _pepper;
        private readonly int _iteration = 3;

        public CloneUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _pepper = Environment.GetEnvironmentVariable("PasswordHashExamplePepper");
            _userRepository = userRepository;
        }

        public async Task<Result<int>> Handle(CloneUserCommand command, CancellationToken cancellationToken)
        {
            // Correct way
            CloneUserCommandValidator validator = new CloneUserCommandValidator(_userRepository);
            var validationResult = await validator.ValidateAsync(command);

            //  Test error validation
            // _validator.ValidateAndThrow(command);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(messages: errors);
            }
            try
            {
                // Retrieve existing user
                var cloneUser = await _unitOfWork.Repository<User>().GetByIdAsync(command.UserSerialID);
                if (cloneUser == null)
                {
                    return await Result<int>.FailureAsync(new List<string> { "Could not be found." });
                }
               var user = new User()
                {
                    UserID = command.CloneToUserID,
                    UserName = command.UserName,
                    EESerialID = command.EESerialID,
                    PermissionType =cloneUser.PermissionType,
                    Active = true,
                    GrpSerialID = cloneUser.GrpSerialID,
                    PassWd = command.PassWd,
                    PasswdSalt = PasswordHasher.GenerateSalt(),
                };
                user.PasswdHash = PasswordHasher.ComputeHash(command.PassWd, user.PasswdSalt, _pepper, _iteration);

                await _unitOfWork.Repository<User>().AddAsync(user);
                user.AddDomainEvent(new UserCreatedEvent(user));
                await _unitOfWork.Save(cancellationToken);

                var currentMenuPermissions = await _unitOfWork.Repository<UserMenuPermission>()
                    .GetAllAsync(c => c.UserSerialID == command.UserSerialID);
                var currentMenuPermissionIds = currentMenuPermissions
                    .Where(m => m.Active)
                    .Select(uc => uc.MnuID)
                    .Distinct()
                    .ToList();

                var currentCompanies = await _unitOfWork.Repository<UserCompany>()
                    .GetAllAsync(c => c.UserSerialID == command.UserSerialID);
                var currentCompanyIds = currentCompanies
                    .Where(m => m.Active)
                    .Select(uc => uc.ComSerialID)
                    .Distinct()
                    .ToList();

                if (currentMenuPermissionIds.Count > 0)
                {
                    var menuPermissions = currentMenuPermissionIds.Select(menuId => new UserMenuPermission
                    {
                        MnuID = menuId,
                        UserSerialID = user.UserSerialID,
                        GrpSerialID = user.GrpSerialID,
                        Active = true,
                        IsDeleted = false
                    }).ToList();

                    await _unitOfWork.Repository<UserMenuPermission>().AddRangeAsync(menuPermissions);
                }

                if (currentCompanyIds.Count > 0)
                {
                    var userCompanies = currentCompanyIds.Select(companyId => new UserCompany
                    {
                        ComSerialID = companyId,
                        UserSerialID = user.UserSerialID,
                        Active = true,
                        IsDeleted = false
                    }).ToList();

                    await _unitOfWork.Repository<UserCompany>().AddRangeAsync(userCompanies);
                }

                if (currentMenuPermissionIds.Count > 0 || currentCompanyIds.Count > 0)
                {
                    await _unitOfWork.Save(cancellationToken);
                }
                return await Result<int>.SuccessAsync(data: user.UserSerialID, message: "Saved successfully.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return await Result<int>.FailureAsync(message: ex.Message + ex.InnerException);


            }
        }
    }
}
