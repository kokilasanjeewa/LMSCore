using AutoMapper;
using Core.Application.Common.Mappings;

using System.ComponentModel.DataAnnotations;

namespace Core.Application.DTOs.State
{

    public class GetStateDto : IMapFrom<Core.Domain.Entities.State>
    {
        [Required]
        public int StateSerialID { get; set; }

        public string Name { get; set; } = null!;

        /// <summary>
        /// Foreign key to Country
        /// </summary>
        public int CntrySerialID { get; set; }

        public string CountryCode { get; set; } = null!;

        public string CountryName { get; set; } = null!;

        public string? FipsCode { get; set; }
        [StringLength(2, ErrorMessage = "The two letter Iso code cannot exceed 2 characters. ")]
        public string? TwoLetterIsoCode { get; set; }
        [StringLength(3, ErrorMessage = "The three letter Iso code cannot exceed 3 characters. ")]
        public string? ThreeLetterIsoCode { get; set; }
        public string? Type { get; set; }

        public int? Level { get; set; }

        public int? ParentId { get; set; }

        public string? Native { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public bool Flag { get; set; }

        public string? WikiDataId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Domain.Entities.State, GetStateDto>();
        }
    }
}
