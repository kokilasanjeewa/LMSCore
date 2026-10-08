using AutoMapper;
using Core.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Group
{
    public class GetGroupsWithPaginationDto : IMapFrom<Core.Domain.Entities.Group>
    {
        [Required]
        public int GrpSerialID { get; set; }
        [Required]
        public int GrpID { get; set; }
        [Required]
        [Display(Name = "Group Name")]
        [StringLength(25, ErrorMessage = "The group name cannot exceed 25 characters. ")]
        public string? GropName { get; set; }
        public long Id { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Active { get; set; }
        public bool IsDeleted { get; set; }
        public void Mapping(Profile profile)
        {
            var c = profile.CreateMap<Core.Domain.Entities.Group, GetGroupsWithPaginationDto>().ForMember(dest => dest.GrpSerialID, opt => opt.MapFrom(src => src.GrpSerialID));

        }
    }
}