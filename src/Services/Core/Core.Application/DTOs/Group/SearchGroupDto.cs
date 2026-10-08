using AutoMapper;
using Core.Application.Common.Mappings;
using Core.Application.DTOs.Company;
using Core.Application.DTOs.User;
using Core.Domain.Entities;
using Core.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Group
{
    public class SearchGroupDto : IMapFrom<Core.Domain.Entities.Group>
    {
        [Required]
        public int GrpSerialID { get; set; }

        [Required]
        [Display(Name = "Group Name")]
        [StringLength(25, ErrorMessage = "The group name cannot exceed 25 characters.")]
        public string? GroupName { get; set; }
        public string? Status { get; set; }
        public bool Active { get; set; }

        public List<SearchGroupMenuDto>? GroupMenus { get; set; } = new List<SearchGroupMenuDto>();
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Domain.Entities.Group, SearchGroupDto>()
                .ForMember(dest => dest.GrpSerialID, opt => opt.MapFrom(src => src.GrpSerialID))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Active ? "Active" : "Inactive"))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.GropName));

        }
    }

}
