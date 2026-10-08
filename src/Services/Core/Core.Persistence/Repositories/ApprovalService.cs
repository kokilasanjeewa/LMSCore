using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Core.Persistence.Contexts;
using Core.Shared;
using Microsoft.EntityFrameworkCore;


namespace Core.Persistence.Repositories
{
    public class UserRoleService : IUserRoleService
    {
        public Task<bool> HasRoleAsync(int userId, string role)
        {
            // Integrate with IAM later
            return Task.FromResult(true);
        }
    }

    public class ApproverResolver : IApproverResolver
    {
        public Task<int> ResolveApproverAsync(string role, int companyId, int? plantId)
        {
            return role switch
            {
                "FINANCE_MANAGER" => Task.FromResult(10),
                "COMPLIANCE_MANAGER" => Task.FromResult(20),
                "MANAGEMENT" => Task.FromResult(30),
                _ => throw new Exception("Approver not configured")
            };
        }
    }

    public class ApprovalService : IApprovalService
    {
        private readonly ApplicationDbContext _db;
        private readonly IApproverResolver _resolver;
        private readonly IUserRoleService _roleService;
        private readonly IUnitOfWork _unitOfWork;

        public ApprovalService(
            ApplicationDbContext db,
            IApproverResolver resolver,
            IUserRoleService roleService,
            IUnitOfWork unitOfWork)
        {
            _db = db;
            _resolver = resolver;
            _roleService = roleService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApprovalRequest> CreateApprovalRequestAsync(
            int entityTypeID,
            int entityID,
            int workflowID,
            int requestedByUserID,
            int companyID,
            int? plantID = null)
        {
            // 1️⃣ Create ApprovalRequest
            var request = new ApprovalRequest
            {
                EntityTypeID = entityTypeID,
                EntityID = entityID,
                ApprovalWorkflowID = workflowID,
                RequestedByUserSerialID = requestedByUserID,
                ComSerialID = companyID,
                PlantID = plantID,
                CurrentStepOrder = 1,
                CurrentState = ApprovalState.Submitted,
                IsCompleted = false,
                Active = true,
                IsDeleted = false
            };

            await _unitOfWork.Repository<ApprovalRequest>().AddAsync(request);
            await _unitOfWork.SaveNoCommitRoll(new CancellationToken());

            // 2️⃣ Load workflow steps
            var workflowSteps = await _unitOfWork.Repository<ApprovalStep>().Entities
                .Where(s => s.ApprovalWorkflowID == workflowID)
                .OrderBy(s => s.StepOrder)
                .ToListAsync();

            if (!workflowSteps.Any())
                throw new InvalidOperationException("Workflow has no steps.");

            // 3️⃣ Create ApprovalRequestStep snapshot
            foreach (var step in workflowSteps)
            {
                // Load active approvers for this step
                var approvers = await _unitOfWork.Repository<ApprovalStepApprover>()
                    .GetAllAsync(a => a.ApprovalStepID == step.ApprovalStepID && a.IsActive);

                int? assignedUser = null;
                var status = ApprovalStepStatus.NotStarted;

                // First step is pending
                if (step.StepOrder == 1 && approvers.Any())
                {
                    assignedUser = approvers.First().UserSerialID; // assign the first approver
                    status = ApprovalStepStatus.Pending;
                }

                await _unitOfWork.Repository<ApprovalRequestStep>().AddAsync(new ApprovalRequestStep
                {
                    ApprovalRequestID = request.ApprovalRequestID,
                    ApprovalStepID = step.ApprovalStepID,
                    StepOrder = step.StepOrder,
                    StepStatus = status,
                    AssignedUserSerialID = assignedUser,
                    Active = true,
                    IsDeleted = false
                });
                await _unitOfWork.SaveNoCommitRoll(new CancellationToken());

            }

            // 4️⃣ Mark workflow as InProgress if first step exists
            request.CurrentState = ApprovalState.InProgress;

            // 5️⃣ Commit
            await _unitOfWork.CommitAsync();
            await _unitOfWork.Save(new CancellationToken());

            return request;
        }

        public async Task<Result<int>> ExecuteApprovalActionAsync(
    int approvalRequestID,
    int stepID,
    ApprovalActionType actionType,
    int userSerialID,
    string? remarks = null)
        {
            // 1️⃣ Load the pending step including its approval request and steps

            var step = await _unitOfWork.Repository<ApprovalRequestStep>().Entities.Where(s => s.ApprovalRequestID == approvalRequestID &&
                         s.ApprovalStepID == stepID &&
                         s.StepStatus == ApprovalStepStatus.Pending).FirstOrDefaultAsync();

            var approvalStep = await _db.ApprovalStep.Include(a => a.Approvers).Where(a => a.ApprovalStepID == stepID).FirstOrDefaultAsync();

            if (step == null)

                throw new InvalidOperationException(message: "Pending step not found or already processed.");

            step.ApprovalStep = approvalStep;

            if (step?.ApprovalStep == null)

                throw new InvalidOperationException(message: "Approval step configuration missing.");

            if (step.ApprovalStep.Approvers == null || !step.ApprovalStep.Approvers.Any())

                throw new InvalidOperationException(message: "No approvers configured for this step.");
            // 2️⃣ Validate user is assigned as approver for this step
            var approvers = await _unitOfWork.Repository<ApprovalStepApprover>()
                .GetAllAsync(a => a.ApprovalStepID == stepID && a.IsActive);

            if (!approvers.Any(a => a.UserSerialID == userSerialID))
                throw new UnauthorizedAccessException(message: "User not assigned to this step.");

            // 3️⃣ Load the approval request and all its steps
            var request = await _unitOfWork.Repository<ApprovalRequest>().Entities.Where(r => r.ApprovalRequestID == approvalRequestID).FirstOrDefaultAsync();
            if (request == null)
                throw new InvalidOperationException(message: "Approval request not found.");
            var requestSteps = await _unitOfWork.Repository<ApprovalRequestStep>().Entities.Where(r => r.ApprovalRequestID == approvalRequestID).ToListAsync();

            request.RequestSteps = requestSteps;

            var previousState = request.CurrentState;

            // 4️⃣ Update current step status
            step.StepStatus = actionType switch
            {
                ApprovalActionType.Approve => ApprovalStepStatus.Approved,
                ApprovalActionType.Reject => ApprovalStepStatus.Rejected,
                ApprovalActionType.Hold => ApprovalStepStatus.Held,
                _ => throw new InvalidOperationException("Please select a approval type.")
            };

            step.ActionDate = DateTime.UtcNow;

            // 5️⃣ Handle workflow progression
            var orderedSteps = request.RequestSteps.OrderBy(s => s.StepOrder).ToList();
            bool isFinalStep = step.StepOrder == orderedSteps.Max(s => s.StepOrder);
            ApprovalRequestStep? nextStep = null;

            if (actionType == ApprovalActionType.Reject)
            {
                request.CurrentState = ApprovalState.Rejected;
                request.IsCompleted = true;
            }
            else if (actionType == ApprovalActionType.Approve)
            {
                if (step.ApprovalStep.Approvers != null && step.ApprovalStep.Approvers.Any(a => a.IsParallel))
                {
                    // Parallel approval logic: check if all approvers approved
                    var allApproversApproved = approvers
                        .All(a => request.RequestSteps
                            .Any(rs => rs.ApprovalStepID == stepID && rs.StepStatus == ApprovalStepStatus.Approved));

                    if (!allApproversApproved)
                    {
                        request.CurrentState = ApprovalState.InProgress;
                    }
                    else
                    {
                        if (isFinalStep)
                        {
                            request.CurrentState = ApprovalState.Approved;
                            request.IsCompleted = true;
                        }
                        else
                        {
                            // Move to next step
                                nextStep = orderedSteps.First(s => s.StepOrder == step.StepOrder + 1);
                            var nextApprovers = await _unitOfWork.Repository<ApprovalStepApprover>()
                                .GetAllAsync(a => a.ApprovalStepID == nextStep.ApprovalStepID && a.IsActive);

                            nextStep.StepStatus = ApprovalStepStatus.Pending;
                            nextStep.AssignedUserSerialID = nextApprovers.FirstOrDefault()?.UserSerialID ?? 0;
                            request.CurrentStepOrder = nextStep.StepOrder;
                            request.CurrentState = ApprovalState.InProgress;
                        }
                    }
                }
                else
                {
                    // Sequential approval
                    if (isFinalStep)
                    {
                        request.CurrentState = ApprovalState.Approved;
                        request.IsCompleted = true;
                    }
                    else
                    {
                        // Move to next step
                            nextStep = orderedSteps.First(s => s.StepOrder == step.StepOrder + 1);
                        var nextApprovers = await _unitOfWork.Repository<ApprovalStepApprover>()
                            .GetAllAsync(a => a.ApprovalStepID == nextStep.ApprovalStepID && a.IsActive);

                        nextStep.StepStatus = ApprovalStepStatus.Pending;
                        nextStep.AssignedUserSerialID = nextApprovers.FirstOrDefault()?.UserSerialID ?? 0;
                        request.CurrentStepOrder = nextStep.StepOrder;
                        request.CurrentState = ApprovalState.InProgress;
                    }
                }
            }
            else if (actionType == ApprovalActionType.Hold)
            {
                request.CurrentState = ApprovalState.InProgress;
            }

            // 6️⃣ Record action history
            var history = new ApprovalActionHistory
            {
                ApprovalRequestID = approvalRequestID,
                ApprovalStepID = stepID,
                Action = actionType,
                ActionByUserSerialID = userSerialID,
                PreviousState = previousState,
                NewState = request.CurrentState,
                CreatedBy = userSerialID,
                CreatedDate = DateTime.UtcNow,
                Active=true,
                IsDeleted=false,
                Remarks = remarks
            };

            // 7️⃣ Explicitly mark entities for update
            await _unitOfWork.Repository<ApprovalRequestStep>().UpdateAsync(step,step.ApprovalRequestStepID);
            await _unitOfWork.SaveNoCommitRoll(new CancellationToken());
            await _unitOfWork.Repository<ApprovalRequest>().UpdateAsync(request,request.ApprovalRequestID);
            await _unitOfWork.SaveNoCommitRoll(new CancellationToken());


            if (nextStep != null)
            {
                await _unitOfWork.Repository<ApprovalRequestStep>().UpdateAsync(nextStep,nextStep.ApprovalRequestStepID);
            }

            await _unitOfWork.Repository<ApprovalActionHistory>().AddAsync(history);
            await _unitOfWork.SaveNoCommitRoll(new CancellationToken());

            // 7️⃣ Commit all changes
            await _unitOfWork.CommitAsync();
            await _unitOfWork.Save(new CancellationToken());
            return await Result<int>.SuccessAsync(data: request.IsCompleted?1:0, message: "Saved successfully");

        }

        #region
        /*        public async Task<ApprovalRequest> CreateApprovalRequestAsync(
            int entityTypeID,
            int entityID,
            int workflowID,
            int requestedByUserID,
            int companyID,
            int? plantID = null)
        {
            // 1️⃣ Create ApprovalRequest
            var request = new ApprovalRequest
            {
                EntityTypeID = entityTypeID,
                EntityID = entityID,
                ApprovalWorkflowID = workflowID,
                RequestedByUserSerialID = requestedByUserID,
                ComSerialID = companyID,
                PlantID = plantID,
                CurrentStepOrder = 1,
                CurrentState = ApprovalState.Submitted,
                IsCompleted = false,
                Active = true,
                IsDeleted = false
            };

            await _unitOfWork.Repository<ApprovalRequest>().AddAsync(request);
            await _unitOfWork.SaveNoCommitRoll(new CancellationToken());
            // 2️⃣ Load workflow steps
            var workflowSteps = await _unitOfWork.Repository<ApprovalStep>().Entities
                .Where(s => s.ApprovalWorkflowID == workflowID)
                .OrderBy(s => s.StepOrder)
                .ToListAsync();

            if (!workflowSteps.Any())
                throw new InvalidOperationException("Workflow has no steps.");

            // 3️⃣ Create ApprovalRequestStep snapshot
            foreach (var step in workflowSteps)
            {
                int? assignedUser = null;
                var status = ApprovalStepStatus.NotStarted;

                if (step.StepOrder == 1)
                {
                    assignedUser = await _resolver.ResolveApproverAsync(
                        step.ApprovalRole, companyID, plantID);
                    status = ApprovalStepStatus.Pending;
                }

                _db.ApprovalRequestStep.Add(new ApprovalRequestStep
                {
                    ApprovalRequestID = request.ApprovalRequestID,
                    ApprovalStepID = step.ApprovalStepID,
                    StepOrder = step.StepOrder,
                    StepStatus = status,
                    AssignedUserSerialID = assignedUser,
                    Active = true,
                    IsDeleted=false

                });
            }

            // 4️⃣ Mark workflow as InProgress if first step exists
            request.CurrentState = ApprovalState.InProgress;
            // 9️⃣ Commit
            await _unitOfWork.CommitAsync();
            await _unitOfWork.Save(new CancellationToken());
            return request;
        }*/
        //        public async Task<ApprovalActionHistory> ExecuteApprovalActionAsync(
        //       int approvalRequestID,
        //       int stepID,
        //       ApprovalActionType actionType,
        //       int userSerialID,
        //       string? remarks = null)
        //        {
        //            // 1️⃣ Load pending step including workflow step info
        //            var step = await _unitOfWork.Repository<ApprovalRequestStep>().GetEntityWithIncludesAsync
        //                                            (
        //                                                s => s.ApprovalRequestID == approvalRequestID &&
        //                                                     s.ApprovalStepID == stepID &&
        //                                                     s.StepStatus == ApprovalStepStatus.Pending,
        //                                                s => s.ApprovalStep
        //                                            );
        //            // 1️⃣ Load pending step including workflow step info
        ///*            var step = await _db.ApprovalRequestStep
        //                .Include(s => s.ApprovalStep)
        //                .FirstAsync(s =>
        //                    s.ApprovalRequestID == approvalRequestID &&
        //                    s.ApprovalStepID == stepID &&
        //                    s.StepStatus == ApprovalStepStatus.Pending);*/

        //            // 2️⃣ Validate assigned user
        //            if (step.AssignedUserSerialID != userSerialID)
        //                throw new UnauthorizedAccessException("Step not assigned to you.");

        //            if (!await _roleService.HasRoleAsync(userSerialID, step.ApprovalStep.ApprovalRole))
        //                throw new UnauthorizedAccessException("User role mismatch.");

        //            // 3️⃣ Load the approval request and its steps
        //            var request = await _db.ApprovalRequest
        //                .Include(r => r.RequestSteps)
        //                .ThenInclude(rs => rs.ApprovalStep)
        //                .FirstAsync(r => r.ApprovalRequestID == approvalRequestID);

        //            var previousState = request.CurrentState;

        //            // 4️⃣ Update current step status
        //            step.StepStatus = actionType switch
        //            {
        //                ApprovalActionType.Approve => ApprovalStepStatus.Approved,
        //                ApprovalActionType.Reject => ApprovalStepStatus.Rejected,
        //                ApprovalActionType.Hold => ApprovalStepStatus.Held,
        //                _ => throw new InvalidOperationException("Invalid action")
        //            };

        //            step.ActionDate = DateTime.UtcNow;
        //            step.AssignedUserSerialID = userSerialID;

        //            // 5️⃣ Determine workflow state
        //            var orderedSteps = request.RequestSteps
        //                .OrderBy(s => s.StepOrder)
        //                .ToList();

        //            if (actionType == ApprovalActionType.Reject)
        //            {
        //                request.CurrentState = ApprovalState.Rejected;
        //                request.IsCompleted = true;
        //            }
        //            else if (actionType == ApprovalActionType.Approve)
        //            {
        //                // Check if this is the final step
        //                var isFinalStep = step.StepOrder == orderedSteps.Max(s => s.StepOrder);

        //                if (isFinalStep)
        //                {
        //                    request.CurrentState = ApprovalState.Approved;
        //                    request.IsCompleted = true;
        //                }
        //                else
        //                {
        //                    // Move to next step
        //                    request.CurrentStepOrder = step.StepOrder + 1;
        //                    request.CurrentState = ApprovalState.InProgress;

        //                    var nextStep = orderedSteps.First(s => s.StepOrder == request.CurrentStepOrder);

        //                    var approver = await _resolver.ResolveApproverAsync(
        //                        nextStep.ApprovalStep.ApprovalRole,
        //                        request.ComSerialID,
        //                        request.PlantID);

        //                    nextStep.AssignedUserSerialID = approver;
        //                    nextStep.StepStatus = ApprovalStepStatus.Pending;
        //                }
        //            }
        //            else if (actionType == ApprovalActionType.Hold)
        //            {
        //                // No change to step order
        //                request.CurrentState = ApprovalState.InProgress;
        //            }

        //            // 6️⃣ Record action history
        //            var history = new ApprovalActionHistory
        //            {
        //                ApprovalRequestID = approvalRequestID,
        //                ApprovalStepID = stepID,
        //                Action = actionType,
        //                ActionByUserSerialID = userSerialID,
        //                PreviousState = previousState,
        //                NewState = request.CurrentState,
        //                CreatedBy = userSerialID,
        //                CreatedDate = DateTime.Now,
        //                Remarks = remarks
        //            };
        //            await _unitOfWork.Repository<ApprovalActionHistory>().AddAsync(history);
        //            await _unitOfWork.CommitAsync();
        //            await _unitOfWork.Save(new CancellationToken());
        //            return history;
        //        }
        #endregion


        public Task<ApprovalRequest?> GetCurrentApprovalAsync(int entityTypeID, int entityID)
            => _db.ApprovalRequest
                .Where(r => r.EntityTypeID == entityTypeID && r.EntityID == entityID && !r.IsCompleted)
                .OrderByDescending(r => r.RequestedDate)
                .FirstOrDefaultAsync();

        public Task<IEnumerable<ApprovalActionHistory>> GetApprovalHistoryAsync(int entityTypeID, int entityID)
            => _db.ApprovalActionHistory
                .Where(h => _db.ApprovalRequest.Any(r =>
                    r.ApprovalRequestID == h.ApprovalRequestID &&
                    r.EntityTypeID == entityTypeID &&
                    r.EntityID == entityID))
                .OrderBy(h => h.ActionDate)
                .ToListAsync()
                .ContinueWith(t => t.Result.AsEnumerable());

        public async Task<ApprovalWorkflow?> GetWorkflowAsync(
      int entityTypeID,
      int companyID,
      int? plantID = null,
      int? entityID = null)
        {
            var workflow = await _db.ApprovalWorkflow
                .Where(w =>
                    w.EntityTypeID == entityTypeID &&
                    w.ComSerialID == companyID &&
                    w.Active &&
                    !w.IsDeleted)
                .Include(w => w.ApprovalSteps) // include steps
                .OrderByDescending(w => w.CreatedDate)
                .FirstOrDefaultAsync();

            if (workflow != null)
            {
                // Order steps after loading
                var steps = workflow.ApprovalSteps.OrderBy(s => s.StepOrder).ToList();
                workflow.ApprovalSteps = steps;
            }

            return workflow;
        }
    }

}
