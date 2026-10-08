using AutoMapper;
using Core.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;


namespace Core.Application.DTOs.Country
{
    public class GetCountryDto : IMapFrom<Core.Domain.Entities.Country>
    {
        [Required]
        public int CntrySerialID { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "The country name cannot exceed 60 characters. ")]
        public string? Name { get; set; }
        [Required]
        [StringLength(2, ErrorMessage = "The two letter Iso code cannot exceed 2 characters. ")]
        public string? TwoLetterIsoCode { get; set; }
        [Required]
        [StringLength(3, ErrorMessage = "The three letter Iso code cannot exceed 3 characters. ")]
        public string? ThreeLetterIsoCode { get; set; }
        [StringLength(255, ErrorMessage = "The flag url cannot exceed 255 characters. ")]
        public string? FlagUrl { get; set; }
        public bool Flag { get; set; }

        public int? DisplayOrder { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "The country code cannot exceed 3 characters. ")]
        public string? CntryCode { get; set; } // PhoneCode
        public string NumericCode { get; set; } = null!;
        public string Capital { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public string CurrencyName { get; set; } = null!;
        public string CurrencySymbol { get; set; } = null!;
        public string Tld { get; set; } = null!;
        public string Native { get; set; } = null!;
        public string Nationality { get; set; } = null!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Domain.Entities.Country, GetCountryDto>();
        }
    }
}
