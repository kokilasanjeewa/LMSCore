using AutoMapper;
using Core.Application.Common.Mappings;
using Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.DTOs.Group
{
     public class SearchGroupMenuDto : IMapFrom<Core.Domain.Entities.GroupMenu>
    {
        [Required]
        public int GrpMnuSerialID { get; set; }
        [Required]
        public int GrpMnuID { get; set; }

        [Required]
        public int GrpSerialID { get; set; }

        [Required]
        public int MnuID { get; set; }
        [Required]
        public bool IsRptInc { get; set; }
        [Required]
        public bool IsBtnInc { get; set; }
        [StringLength(1, ErrorMessage = "The MnuMTBR cannot exceed 1 characters. ")]
        [Required]
        public string? MnuMTBR { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GroupMenu, SearchGroupMenuDto>()
                .ForMember(dest => dest.GrpMnuSerialID, opt => opt.MapFrom(src => src.GrpMnuSerialID))
                .ForMember(dest => dest.GrpMnuID, opt => opt.MapFrom(src => src.GrpMnuID))
                .ForMember(dest => dest.MnuMTBR, opt => opt.MapFrom(src => src.Menu.MnuMTBR))
                .ForMember(dest => dest.IsBtnInc, opt => opt.MapFrom(src => src.Menu.IsBtnInc))
                .ForMember(dest => dest.IsRptInc, opt => opt.MapFrom(src => src.Menu.IsRptInc))
                .ForMember(dest => dest.MnuID, opt => opt.MapFrom(src => src.MnuID))
                .ForMember(dest => dest.GrpSerialID, opt => opt.MapFrom(src => src.GrpSerialID));

        }
    }
}

