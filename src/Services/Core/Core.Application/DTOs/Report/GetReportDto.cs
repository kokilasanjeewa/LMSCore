using AutoMapper;
using Core.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Report
{
    public class GetReportDto : IMapFrom<Core.Domain.Entities.Report>
    {
        [Required]
        public short ReportSerialID { get; set; }
        [Required]
        public int ModSerialID { get; set; }
        [Required]
        public int MnuID { get; set; }
        [Required]
        public List<string>? DataSet { get; set; }
        [Required]
        public string? ReportName { get; set; }
        [Required]
        public string? DatabaseName { get; set; }
        [Required]
        public string? StoredProcedureName { get; set; }

        public int? ReportType { get; set; }
        public string? Orientation { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Report, GetReportDto>()
                   .ForMember(dest => dest.DatabaseName, opt => opt.MapFrom(src => src.DataBaseName))
                   .ForMember(dest => dest.StoredProcedureName, opt => opt.MapFrom(src => src.DataBaseSPName))
                   .ForMember(dest => dest.ReportType, opt => opt.MapFrom(src => src.ReportTypeID));



        }
    }
}
