using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Application.Common.Mappings;
using AutoMapper;
using Core.Application.Helper;

namespace Core.Application.DTOs.User
{
    public class GetUsersWithPaginationDto : IMapFrom<Core.Domain.Entities.User>
    {
        public void Mapping(Profile profile)
        {
           profile.CreateMap<Core.Domain.Entities.User, GetUsersWithPaginationDto>().ForMember(dest => dest.PermissionTypeInt, opt => opt.MapFrom(src =>(PermissionType)src.PermissionInt));
        }
        [Required]
        public long Id {  get; set; } 
        [Required]
        public int UserSerialID { get; set; }
        [Required]
        public string? UserID { get; set; }
        public long? EESerialID { get; set; }

        [Required]
        [Display(Name = "User Name")]
        [StringLength(50, ErrorMessage = "The User Name cannot exceed 50 characters. ")]
        public string? UserName { get; set; }
        [Required]
        public string? ComCode { get; set; }
        
        [Required]
        public string? CompanyName { get; set; }

        [Required]
        public string? DeptName { get; set; }
        [Required]
        public string? SectName { get; set; }
        public int PermissionTypeInt { get; set; }
        public string? PermissionType { get; set; }
        [Required]
        public string? GrpName { get; set; }
        [EmailAddress]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$")]
        [StringLength(50, ErrorMessage = "The email address cannot exceed 50 characters. ")]
        [Required]
        public string? Email { get; set; }
        [StringLength(10, ErrorMessage = "The mobile cannot exceed 10 characters. ")]
        public string? Mobile { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Active { get; set; }

    }
}
