using Core.Domain.Common;
using Core.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Core.Domain.Entities
{
    [EntityTypeConfiguration(typeof(CountryConfiguration))]
    public class Country : BaseAuditableEntity
    {
        [Key]
        public int CntrySerialID { get; set; }
        [Required]
        public int CntryID { get; set; }

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

        // Not implement in Database 

        [NotMapped]
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [StringLength(15, ErrorMessage = "The VAT cannot exceed 15 characters. ")]
        public decimal VAT { get; set; }

        [NotMapped]
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [StringLength(15, ErrorMessage = "The SVAT cannot exceed 15 characters. ")]
        public decimal SVAT { get; set; }

        [NotMapped]
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [StringLength(15, ErrorMessage = "The NBT exceed 15 characters. ")]
        public decimal NBT { get; set; }

        public string NumericCode { get; set; } = null!;


        public string Capital { get; set; } = null!;

        public string Currency { get; set; } = null!;

        public string CurrencyName { get; set; } = null!;

        public string CurrencySymbol { get; set; } = null!;

        public string Tld { get; set; } = null!;

        public string Native { get; set; } = null!;

        public string Region { get; set; } = null!;

        public int RegionSerialID { get; set; }

        public string Subregion { get; set; } = null!;

        public int SubregionSerialID { get; set; }

        public string Nationality { get; set; } = null!;

        public decimal AreaSqKm { get; set; }

        public string? PostalCodeFormat { get; set; }

        public string? PostalCodeRegex { get; set; }
        public string? Translations    { get; set; }
        public string? Timezones       { get; set; }
        /// <summary>
        /// JSON Column
        /// </summary>
        //public List<TimeZoneInfoDto> Timezones { get; set; } = new();

        /// <summary>
        /// JSON Column (key = language code, value = translated name)
        /// </summary>
        //public Dictionary<string, string> Translations { get; set; } = new();

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string Emoji { get; set; } = null!;

        public string EmojiU { get; set; } = null!;
        public string WikiDataId { get; set; } = null!;
    }
    public class TimeZoneInfoDto
    {
        public string ZoneName { get; set; } = null!;

        public int GmtOffset { get; set; }

        public string GmtOffsetName { get; set; } = null!;

        public string Abbreviation { get; set; } = null!;

        public string TzName { get; set; } = null!;
    }

}
