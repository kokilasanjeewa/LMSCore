using AutoMapper;
using Core.Application.Common.Mappings;
using Core.Application.Features.Users.Commands.CreateUser;
using Core.Application.Interfaces.Repositories;
using Core.Application.Request;
using Core.Domain.Entities;
using Core.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Core.Application.Features.Group.Commands.CreateGroup
{
    public class CreateGroupCommand : IRequest<Result<int>>, IMapFrom<Domain.Entities.Group>
    {
        [Required]
        [Display(Name = "Group Name")]
        [StringLength(25, ErrorMessage = "The group name cannot exceed 25 characters. ")]
        public string? GropName { get; set; }
    }
    internal class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _groupRepository;

        public CreateGroupCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
        }

        public async Task<Result<int>> Handle(CreateGroupCommand command, CancellationToken cancellationToken)
        {
            // Correct way
            CreateGroupCommandValidator validator = new CreateGroupCommandValidator(_groupRepository);
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

                var group = new Domain.Entities.Group()
                {
                    GropName = command.GropName,
                    Active = true

                };

                await _unitOfWork.Repository<Domain.Entities.Group>().AddAsync(group);
                group.AddDomainEvent(new GroupCreatedEvent(group));

                await _unitOfWork.Save(cancellationToken);
                return await Result<int>.SuccessAsync(data: group.GrpSerialID, message: "Saved successfully");
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return await Result<int>.FailureAsync(message: ex.Message + ex.InnerException);


            }
        }
    }
}
