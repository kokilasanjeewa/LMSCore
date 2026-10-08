using AutoMapper;
using Core.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.DTOS.CostCenter
{
    public class UpdateCostCenterDto : IMapFrom<Domain.Entities.CostCenter>
    {
        [Required]
        public byte CostCenterSerialID { get; set; }

        [Required]
        public string? CostCenterName { get; set; }

        public int MnuSerialID { get; set; } = 0;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCostCenterDto, Domain.Entities.CostCenter>()
                    .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));


        }
    }
}
