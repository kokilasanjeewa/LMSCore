
using AutoMapper;
using Core.Application.Common.Mappings;

namespace Core.Application.DTOS.CostCenter
{
    public class CreateCostCenterDto : IMapFrom<Domain.Entities.CostCenter>
    {

        public string? CostCenterName { get; set; }

        public int MnuSerialID { get; set; } = 0;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCostCenterDto, Domain.Entities.CostCenter>()
                    .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));


        }
    }
}
