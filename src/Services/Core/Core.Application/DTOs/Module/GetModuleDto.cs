using AutoMapper;
using Core.Application.Common.Mappings;
using Core.Application.DTOs.DocType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Module
{
    public class GetModuleDto : IMapFrom<Core.Domain.Entities.AppModule>
    {
        [Required]
        public int ModSerialID { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "The module name cannot exceed 20 characters. ")]
        public string? ModName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Domain.Entities.AppModule, GetModuleDto>();
        }
    }
}
