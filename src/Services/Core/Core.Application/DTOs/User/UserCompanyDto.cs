using AutoMapper;
using Core.Application.Common.Mappings;
using Core.Application.DTOs.User;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.User
{
    public class UserCompanyDto : IMapFrom<Core.Domain.Entities.UserCompany>
    {
        [Required]
        public int ComSerialID { get; set; }
        [Required]
        public string? ComName { get; set; }
        [Required]
        public string? ComCode { get; set; }
        public void Mapping(Profile profile)
        {

            profile.CreateMap<Core.Domain.Entities.UserCompany, UserCompanyDto>()
                    .ForMember(dest => dest.ComSerialID, opt => opt.MapFrom(src => src.ComSerialID))
                    .ForMember(dest => dest.ComName, opt => opt.MapFrom(src => src.Company.ComName))
                    .ForMember(dest => dest.ComCode, opt => opt.MapFrom(src => src.Company.ComCode));
        }
    }
}
