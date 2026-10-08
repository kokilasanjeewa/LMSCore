using AutoMapper;
using Core.Application.Common.Mappings;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Core.Shared;
using MediatR;
using System.ComponentModel.DataAnnotations;


namespace Core.Application.Features.DocType.Command
{
    public class CreateDocTypeCommand : IRequest<Result<int>>, IMapFrom<Domain.Entities.DocType>
    {
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
            profile.CreateMap<CreateDocTypeCommand, Domain.Entities.DocType>();
            profile.CreateMap<CreateDocTypeCommand, Domain.Entities.FilePaths>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => Active));
        }
    }
    internal class CreateDocTypeCommandHandler : IRequestHandler<CreateDocTypeCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateDocTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<Result<int>> Handle(CreateDocTypeCommand command, CancellationToken cancellationToken)
        {
            // Correct way
            CreateDocTypeCommandValidator validator = new CreateDocTypeCommandValidator(_unitOfWork);
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
                    var carete = _mapper.Map<Domain.Entities.DocType>(command);
                    await _unitOfWork.Repository<Domain.Entities.DocType>().AddAsync(carete);
                    await _unitOfWork.SaveNoCommitRoll(cancellationToken);

                    var param = _unitOfWork.Repository<TheNumber>().Entities.Where(p => p.TheNumberName == "DocType").FirstOrDefault();
                    param.LastNumber = param.LastNumber + 1;
                    await _unitOfWork.Repository<TheNumber>().UpdateAsync(param, param.TheNumberSerialID);
                    await _unitOfWork.SaveNoCommitRoll(cancellationToken);

                    var caretePath = _mapper.Map<FilePaths>(command);
                        caretePath.DocTypeSerialID = carete.DocTypeSerialID;
  
                    await _unitOfWork.Repository<FilePaths>().AddAsync(caretePath);
                    await _unitOfWork.SaveNoCommitRoll(cancellationToken);

                    carete.AddDomainEvent(new DocTypeCreatedEvent(carete));

                    await _unitOfWork.CommitAsync();

                    return await Result<int>.SuccessAsync(data: carete.DocTypeSerialID, message: "Saved successfully");

                }, cancellationToken);
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
