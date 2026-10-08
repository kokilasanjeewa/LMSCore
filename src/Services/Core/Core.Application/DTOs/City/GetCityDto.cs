using AutoMapper;
using Core.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;


namespace Core.Application.DTOs.City
{
    public class GetCityDto : IMapFrom<Core.Domain.Entities.City>
    {
        [Required]
        public int CitySerialID { get; set; }

        public string Name { get; set; } = null!;

        /// <summary>
        /// Foreign key to State
        /// </summary>
        public int StateSerialID { get; set; }

        public string StateCode { get; set; } = null!;

        public string StateName { get; set; } = null!;

        /// <summary>
        /// Foreign key to Country
        /// </summary>
        public int CntrySerialID { get; set; }

        public string CountryCode { get; set; } = null!;

        public string CountryName { get; set; } = null!;

        public string? Type { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public string? Native { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Domain.Entities.City, GetCityDto>();
        }
    }

    public class FormOptionsDto
    {
        public IEnumerable<FormOptionDto> OfficeLocations { get; set; } = new List<FormOptionDto>();
        public IEnumerable<FormOptionDto> Countries { get; set; } = new List<FormOptionDto>();
        public IEnumerable<FormOptionDto> Currencies { get; set; } = new List<FormOptionDto>();
    }

    public class FormOptionDto
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public int? Value { get; set; }
        public int? FilterValue { get; set; }
    }
}
