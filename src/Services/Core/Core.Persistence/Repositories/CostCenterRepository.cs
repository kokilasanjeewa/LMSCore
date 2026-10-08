using AutoMapper;
using Core.Application.Features.CostCenter.Command;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Core.Persistence.Contexts;
using Core.Shared;

namespace Core.Persistence.Repositories
{
    public class CostCenterRepository : ICostCenterRepository
    {
        private readonly IGenericRepository<CostCenter> _repository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public CostCenterRepository(IGenericRepository<CostCenter> repository, IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
            _unitOfWork = unitOfWork;


        }


        public async Task<Result<int>> DeleteCostCenterAsync(DeleteCostCenterCommand delete, CancellationToken cancellationToken)
        {
            try
            {
                var repository = _unitOfWork.Repository<CostCenter>();

                /*                var hasEmployees = _unitOfWork.Repository<Employee>()
                                     .Entities
                                     .Any(e => e.CostCenter == delete.CostCenterSerialID); // Synchronous


                if (hasEmployees)
                {
                    return await Result<int>.FailureAsync("Delete related data before removing CostCenter.");
                }*/

                using (var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken))
                {
                    //    var existingBuilding = repository.GetById(delete.CostCenterSerialID);
                     repository = _unitOfWork.Repository<Domain.Entities.CostCenter>();

                    var existingBuilding = repository.Entities
                .FirstOrDefault(e => e.CostCenterSerialID == delete.CostCenterSerialID);

                    if (existingBuilding == null)
                    {
                        return await Result<int>.FailureAsync("CostCenter not found.");
                    }

                    if (existingBuilding.DeletedBy > 0)
                    {
                        return await Result<int>.FailureAsync("Already deleted.");
                    }

                    //   _mapper.Map(delete, existingBuilding);

                   //_auditContextService.MnuSerialID = delete.MnuSerialID;

                   //  await repository.HradDeleteAsync((int)delete.CostCenterSerialID, hardDelete: true);

                    await _unitOfWork.SaveNoCommitRoll(cancellationToken);
                    await _unitOfWork.CommitAsync();

                    return await Result<int>.SuccessAsync(delete.CostCenterSerialID);
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                var message = ex.InnerException?.Message ?? ex.Message;
                return await Result<int>.FailureAsync(message);
            }
        }

    }
}
