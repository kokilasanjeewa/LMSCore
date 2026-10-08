using Core.Application.Common.Mappings;
using Core.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper;


namespace Core.Application.DTOs.Report
{
    public class UpdateReportDto : IMapFrom<Core.Domain.Entities.Report>
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
        public string? DataBaseSPName { get; set; }
        [Required]
        public string? DataBaseName { get; set; }
        public int? ReportTypeID { get; set; }
        public string? Orientation { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateReportDto, Domain.Entities.Report>();

        }
    }
}
