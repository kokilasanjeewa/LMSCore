using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Core.Application.Common.Mappings;

namespace Core.Application.DTOs.Reason;

public class UpdateReasonDto : IMapFrom<Core.Domain.Entities.Reason>
{
    [Required]
    public int ReasonSerialID { get; set; }             // Reason ID
    [Required]
    public int ModSerialID { get; set; }
    [Required]
    [StringLength(15)]
    public string? Document { get; set; }
    [Required]
    [StringLength(80)]
    public string? ReasonText { get; set; }
    public bool Active { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateReasonDto, Core.Domain.Entities.Reason>();
    }
}
