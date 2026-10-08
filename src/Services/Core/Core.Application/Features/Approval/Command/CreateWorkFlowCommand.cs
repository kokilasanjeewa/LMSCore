using AutoMapper;
using Core.Application.Common.Mappings;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Core.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.Features.Approval.Command
{
    public class CreateWorkFlowCommand : IRequest<Result<int>>, IMapFrom<Domain.Entities.ApprovalWorkflow>
    {
        [Required]
        [StringLength(100, ErrorMessage = "The name cannot exceed 150 characters. ")]
        public string? WorkflowName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The code cannot exceed 50 characters. ")]
        public string? WorkflowCode { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The entity cannot exceed 100 characters. ")]
        public string? EntityType { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "The company cannot exceed 60 characters. ")]
        public string? Company { get; set; }
        public List<CreateApprovalStepDto>? Steps { get; set; }

    }
    public class CreateApprovalStepDto
    {
        [Required]
        public int StepOrder { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "The code cannot exceed 20 characters. ")]
        public string? StepCode { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The name cannot exceed 100 characters. ")]
        public string? StepName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The role cannot exceed 100 characters. ")]
        public string? ApprovalRole { get; set; }
        public int? PlantID { get; set; }
        public bool IsMandatory { get; set; }
        public bool CanReject { get; set; }
        public bool IsFinalStep { get; set; }
    }
    internal class CreateWorkFlowCommandHandler : IRequestHandler<CreateWorkFlowCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateWorkFlowCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<Result<int>> Handle(CreateWorkFlowCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateWorkFlowCommandValidator(_unitOfWork);
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
                return await Result<int>.FailureAsync(validationResult.Errors.Select(e => e.ErrorMessage).ToList());

            try
            {
                // 2️⃣ Lookup EntityType ID
                var entityType = await _unitOfWork.Repository<EntityType>().Entities
                    .Where(e => e.EntityName == command.EntityType)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(cancellationToken);

                if (entityType == null)
                    return Result<int>.Failure("Invalid Entity Type.");

                // 3️⃣ Lookup Company ID
                var company = await _unitOfWork.Repository<Company>().Entities
                    .Where(c => c.ComName == command.Company)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(cancellationToken);

                if (company == null)
                    return Result<int>.Failure("Invalid Company.");

                var workflow = new ApprovalWorkflow
                {
                    // 4️⃣ MANUAL UPDATE - NO AUTOMAPPER
                    WorkflowName = command.WorkflowName,
                    WorkflowCode = command.WorkflowCode,
                    EntityTypeID = entityType.EntityTypeID,
                    ComSerialID = company.ComSerialID,
                    CreatedDate = DateTime.UtcNow,
                    Active = true,
                    IsDeleted = false,
                };


                await _unitOfWork.Repository<ApprovalWorkflow>()
                    .AddAsync(workflow);
                await _unitOfWork.SaveNoCommitRoll(cancellationToken);

                // 7️⃣ Handle ADD & UPDATE - MANUAL MAPPING
                if (command.Steps != null && command.Steps.Any())
                {
                    foreach (var stepDto in command.Steps)
                    {
                            // ADD NEW STEP
                            var newStep = new ApprovalStep
                            {
                                ApprovalWorkflowID = workflow.ApprovalWorkflowID,
                                StepOrder = stepDto.StepOrder,
                                StepCode = stepDto.StepCode,
                                StepName = stepDto.StepName,
                                ApprovalRole = stepDto.ApprovalRole,
                                PlantID = stepDto.PlantID,
                                IsMandatory = stepDto.IsMandatory,
                                CanReject = stepDto.CanReject,
                                IsFinalStep = stepDto.IsFinalStep,
                                Active = true,
                                IsDeleted = false,
                            };

                            await _unitOfWork.Repository<ApprovalStep>().AddAsync(newStep);
                            await _unitOfWork.SaveNoCommitRoll(cancellationToken);
        
                     }
                }

                // 8️⃣ Domain event
                workflow.AddDomainEvent(new ApproveWorkFlowCreatedEvent(workflow));

                // 9️⃣ Commit
                await _unitOfWork.CommitAsync();
                await _unitOfWork.Save(cancellationToken);

                return await Result<int>.SuccessAsync(
                    workflow.ApprovalWorkflowID,
                    "Saved successfully");
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return await Result<int>.FailureAsync(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
