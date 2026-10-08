using AutoMapper;
using Core.Application.Common.Mappings;
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

namespace Core.Application.Features.DocType.Command
{

    public class UpdateDocTypeCommand : IRequest<Result<int>>, IMapFrom<Domain.Entities.DocType>
    {
        [Required]
        public short DocTypeSerialID { get; set; }

        [Required]
        public int ModSerialID { get; set; }
        [Required]
        public string? DocumentType { get; set; }
        [Required]
        [StringLength(80, ErrorMessage = "The file path cannot exceed 80 characters. ")]
        public string? FilePath { get; set; }
        public bool Active { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateDocTypeCommand, Domain.Entities.DocType>();
            profile.CreateMap<UpdateDocTypeCommand, Domain.Entities.FilePaths>();

        }
    }
    internal class UpdateDocTypeCommandHandler : IRequestHandler<UpdateDocTypeCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateDocTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<Result<int>> Handle(UpdateDocTypeCommand command, CancellationToken cancellationToken)
        {
            // Correct way
            UpdateDocTypeCommandValidator validator = new UpdateDocTypeCommandValidator(_unitOfWork);
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
                // Begin transaction (it will reuse an existing one if already started)
                return await _unitOfWork.ExecuteInTransactionAsync(async () =>
                {
                    short docTypeId = Convert.ToInt16(command.DocTypeSerialID); 
                    var update = await _unitOfWork.Repository<Domain.Entities.DocType>().GetByIdAsync(docTypeId);
                    _mapper.Map(command, update); // ✅ Map into the existing entity

                    await _unitOfWork.Repository<Domain.Entities.DocType>().UpdateAsync(update,update.DocTypeSerialID);
                    await _unitOfWork.SaveNoCommitRoll(cancellationToken);

                    var updatePath = await _unitOfWork.Repository<Domain.Entities.FilePaths>().GetEntityWithIncludesAsync(x => x.DocTypeSerialID== command.DocTypeSerialID);
                    _mapper.Map(command, updatePath); // ✅ Map into the existing entity

                    await _unitOfWork.Repository<FilePaths>().UpdateAsync(updatePath, updatePath.FilePathSerialID);
                    await _unitOfWork.SaveNoCommitRoll(cancellationToken);

                    update.AddDomainEvent(new DocTypeUpdatedEvent(update));

                    await _unitOfWork.CommitAsync();

                    return await Result<int>.SuccessAsync(data: update.DocTypeSerialID, message: "Modified successfully");
                },cancellationToken);
            }
            catch (Exception ex)
            {
                // Rollback transaction in case of an error
                await _unitOfWork.RollbackAsync();
                var message = ex.InnerException?.Message ?? ex.Message;

                return await Result<int>.FailureAsync(message: ex.Message + ex.InnerException);
            }
        }
    }
}
