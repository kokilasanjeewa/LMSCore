using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Core.Application.Common.Mappings;

namespace Core.Application.DTOs.Reason;

public class GetReasonsDto : IMapFrom<Core.Domain.Entities.Reason>
{
    [Required]
    public int ReasonSerialID { get; set; }

    [Required]
    public int? ModSerialID { get; set; }
    public string? Module { get; set; }

    [Required]
    [StringLength(15)]
    public string? Document { get; set; }

    [Required]
    [StringLength(80)]
    public string? ReasonText { get; set; }
    public bool Active { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Core.Domain.Entities.Reason, GetReasonsDto>()
                 .ForMember(dest => dest.Module, opt => opt.MapFrom(src => src.Module.ModName));
    }
}
