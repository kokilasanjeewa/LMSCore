using Core.Application.Common.Mappings;
using Core.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;


namespace Core.Application.DTOs.Report
{
    public class CreateReportDto : IMapFrom<Domain.Entities.Report>
    {
        [Required]
        public int ModSerialID { get; set; }
        [Required]
        public int MnuID { get; set; }
        [Required]
        public List<string>? DataSet { get; set; }
        [Required]
        public string? ReportName { get; set; }
        [Required]
        public string? DataBaseName { get; set; }
        [Required]
        public string? DataBaseSPName { get; set; }
        public int? ReportTypeID { get; set; }
        public string? Orientation { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<CreateReportDto, Domain.Entities.Report>()
                .ForMember(dest => dest.Active,opt => opt.MapFrom(src =>true))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));

        }
    }
}
