using AutoMapper;
using Core.Application.Common.Mappings;
using Core.Application.Interfaces.Repositories;
using Core.Shared;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.Features.DocType.Command
{

    public class DeleteDocTypeCommand : IRequest<Result<int>>, IMapFrom<Domain.Entities.FilePaths>
    {
        [Required]
        public short DocTypeSerialID { get; set; }

        [Required]
        public int? DeletedBy { get; set; }
        [Required]
        public string? DocTable { get; set; }
        [NotMapped]
        [Required]
        public string? Remarks { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteDocTypeCommand, Domain.Entities.FilePaths>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.DeletedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.DeletedBy, opt => opt.MapFrom(src => src.DeletedBy));
       
            profile.CreateMap<DeleteDocTypeCommand, Domain.Entities.DelRecord>()
                 .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.DocSerialID, opt => opt.MapFrom(src => src.DocTypeSerialID))
                .ForMember(dest => dest.DocTable, opt => opt.MapFrom(src => src.DocTable))
                .ForMember(dest => dest.Remarks, opt => opt.MapFrom(src => src.Remarks));

        }
    }
    internal class DeleteDocTypeCommandHandler : IRequestHandler<DeleteDocTypeCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDocTypeRepository _docTypeRepository;

        public DeleteDocTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IDocTypeRepository docTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _docTypeRepository = docTypeRepository;
        }

        public async Task<Result<int>> Handle(DeleteDocTypeCommand query, CancellationToken cancellationToken)
        {
            DeleteDocTypeValidator validator = new DeleteDocTypeValidator();
            var validationResult = await validator.ValidateAsync(query, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(errors);
            }
            // Step 1: Retrieve the brand along with brand item types
            return await _docTypeRepository.DeleteDocTypeAsync(query, cancellationToken);
        }
    }
}
