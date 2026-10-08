using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Core.Application.Common.Mappings;
using Core.Shared;
using Core.Application.Interfaces.Repositories;

namespace Core.Application.Features.CostCenter.Command
{

    public class DeleteCostCenterCommand : IRequest<Result<int>>, IMapFrom<Domain.Entities.CostCenter>
    {
        [Required]
        public byte CostCenterSerialID { get; set; }
        [Required]
        public int? DeletedBy { get; set; }
        [Required]
        public string? DocTable { get; set; }
        [NotMapped]
        [Required]
        public string? Remarks { get; set; }
        public int MnuSerialID { get; set; } = 0;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteCostCenterCommand, Domain.Entities.CostCenter>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.DeletedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.DeletedBy, opt => opt.MapFrom(src => src.DeletedBy));

            profile.CreateMap<DeleteCostCenterCommand, Domain.Entities.DelRecord>()
                 .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.DocSerialID, opt => opt.MapFrom(src => src.CostCenterSerialID))
                .ForMember(dest => dest.DocTable, opt => opt.MapFrom(src => src.DocTable))
                .ForMember(dest => dest.Remarks, opt => opt.MapFrom(src => src.Remarks));

        }
    }
    internal class DeleteCostCenterCommandHandler : IRequestHandler<DeleteCostCenterCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICostCenterRepository _gradeRepository;

        public DeleteCostCenterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICostCenterRepository gradeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _gradeRepository = gradeRepository;
        }

        public async Task<Result<int>> Handle(DeleteCostCenterCommand query, CancellationToken cancellationToken)
        {
            DeleteCostCenterValidator validator = new DeleteCostCenterValidator();
            var validationResult = await validator.ValidateAsync(query, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(errors);
            }
            // Step 1: Retrieve the brand along with brand item types
            return await _gradeRepository.DeleteCostCenterAsync(query, cancellationToken);
        }
    }
}
