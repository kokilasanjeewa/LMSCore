using AutoMapper;
using MediatR;
using Core.Domain.Entities;
using Core.Application.Common.Mappings;
using Core.Shared;
using Core.Application.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;
using Core.Application.Helper;
using Core.Application.Request;

namespace Core.Application.Features.Users.Commands.CreateUser
{
    public record CreateUserCommand : IRequest<Result<int>>, IMapFrom<User>
    {
        [Required]
        [RegularExpression(@"^[A-Z][A-Za-z]*$", ErrorMessage = "The userid must begin with a capital letter and contain no spaces.")]
        public string? UserID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Please note that the user name should not contain more than 50 characters.")]
        public string? UserName { get; set; }
        public string? PermissionType { get; set; }
        public string? Status { get; set; }
        public int? GrpSerialID { get; set; } = 0;
        [Required]
        public string? PassWd { get; set; }
        public long? EESerialID { get; set; }
        public int[]? Companies { get; set; }
        public int[]? MenuPermission { get; set; }
    }

    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly string _pepper;
        private readonly int _iteration = 3;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _pepper = Environment.GetEnvironmentVariable("PasswordHashExamplePepper");
            _userRepository = userRepository;
        }

        public async Task<Result<int>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            // Correct way
            CreateUserCommandValidator validator = new CreateUserCommandValidator(_userRepository);
            var validationResult = await validator.ValidateAsync(command);

            //  Test error validation
            // _validator.ValidateAndThrow(command);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(messages:errors);
            }
            try
            {
                var permissionType = (PermissionType)EnumConverter.StringToEnum<PermissionType>(command.PermissionType)!;

                var user = new User()
                {
                    UserID = command.UserID,
                    UserName = command.UserName,
                    EESerialID = command.EESerialID,
                    PermissionType = permissionType,
                    Active = EnumConverter.ConvertEnumStringToNumber<StatusType>(command.Status) == 1,
                    GrpSerialID = command.GrpSerialID,
                    PassWd = command.PassWd,
                    PasswdSalt = PasswordHasher.GenerateSalt(),
                };
                user.PasswdHash = PasswordHasher.ComputeHash(command.PassWd, user.PasswdSalt, _pepper, _iteration);

                await _unitOfWork.Repository<User>().AddAsync(user);
                user.AddDomainEvent(new UserCreatedEvent(user));
                await _unitOfWork.Save(cancellationToken);

                var menuIds = (command.MenuPermission ?? Array.Empty<int>()).Distinct().ToList();
                var companyIds = (command.Companies ?? Array.Empty<int>()).Distinct().ToList();
                var shouldSaveMenuPermissions =
                    permissionType == PermissionType.Individual || menuIds.Count > 0;

                if (shouldSaveMenuPermissions && menuIds.Count > 0)
                {
                    var menuPermissions = menuIds.Select(menuId => new UserMenuPermission
                    {
                        MnuID = menuId,
                        UserSerialID = user.UserSerialID,
                        GrpSerialID = command.GrpSerialID,
                        Active = true,
                        IsDeleted = false
                    }).ToList();

                    await _unitOfWork.Repository<UserMenuPermission>().AddRangeAsync(menuPermissions);
                }

                if (companyIds.Count > 0)
                {
                    var userCompanies = companyIds.Select(companyId => new UserCompany
                    {
                        ComSerialID = companyId,
                        UserSerialID = user.UserSerialID,
                        Active = true,
                        IsDeleted = false
                    }).ToList();

                    await _unitOfWork.Repository<UserCompany>().AddRangeAsync(userCompanies);
                }

                if (menuIds.Count > 0 || companyIds.Count > 0)
                {
                    await _unitOfWork.Save(cancellationToken);
                }

                return await Result<int>.SuccessAsync(data: user.UserSerialID, message: "Saved successfully.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return await Result<int>.FailureAsync(message: ex.Message+ex.InnerException);


            }
        }
    }
}
