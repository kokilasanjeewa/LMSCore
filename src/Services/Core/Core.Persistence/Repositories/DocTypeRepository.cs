using AutoMapper;
using Core.Application.Features.DocType.Command;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Core.Persistence.Contexts;
using Core.Shared;

namespace Core.Persistence.Repositories
{
    public class DocTypeRepository : IDocTypeRepository
    {
        private readonly IGenericRepository<DocType> _repository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public DocTypeRepository(IGenericRepository<DocType> repository, IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
            _unitOfWork = unitOfWork;


        }

        public async Task<Result<int>> DeleteDocTypeAsync(DeleteDocTypeCommand delete, CancellationToken cancellationToken)
        {
            try
            {
                // Begin transaction (it will reuse an existing one if already started)
                // Begin transaction (it will reuse an existing one if already started)
                return await _unitOfWork.ExecuteInTransactionAsync(async () =>
                {
                    
                    // Fetch the existing polling division by its ID
                    var existingFilePath = await _unitOfWork.Repository<FilePaths>().GetEntityWithIncludesAsync(f => f.DocTypeSerialID== delete.DocTypeSerialID);

                    if (existingFilePath == null)
                    {
                        return await Result<int>.FailureAsync("Polling division not found.");
                    }
                    // Check if the asset location has already been deleted
                    if (existingFilePath.DeletedBy > 0)
                    {
                        return await Result<int>.FailureAsync("Already deleted.");
                    }
                    // Map the updated properties from the update model to the existing entity
                    _mapper.Map(delete, existingFilePath);
                     
                    await _unitOfWork.Repository<FilePaths>()
                        .UpdateAsync(existingFilePath, existingFilePath.FilePathSerialID);
                    // Save changes again to commit the doc type 
                    await _unitOfWork.SaveNoCommitRoll(cancellationToken);

                    //setup del record for doc type 
                    var create = _mapper.Map<DelRecord>(delete);
                    await _unitOfWork.Repository<DelRecord>().AddAsync(create);

                    // Save changes again to commit the doc type 
                    await _unitOfWork.SaveNoCommitRoll(cancellationToken);

                    await _unitOfWork.CommitAsync();

                    return await Result<int>.SuccessAsync(data: existingFilePath.FilePathSerialID, message: "Deleted successfully.");
                }, cancellationToken);
            }
            catch (Exception ex)
            {
                // Rollback the transaction in case of an error
                await _unitOfWork.RollbackAsync();
                var message = ex.InnerException?.Message ?? ex.Message;
                return await Result<int>.FailureAsync(message: message);
            }

        }
    }
}
