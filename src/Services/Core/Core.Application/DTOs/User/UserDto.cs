using AutoMapper;
using Core.Application.Common.Mappings;
using Core.Application.DTOs.Company;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.User
{
    public class UserDto : IMapFrom<Core.Domain.Entities.User>
    {
        public int UserSerialID { get; set; }
        public long? EESerialID { get; set; }
        public string? UserID { get; set; }
        public string? UserName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Domain.Entities.User, UserDto>()
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
        }
    }
}
