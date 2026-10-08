using Core.Domain.Common;
using Core.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    [EntityTypeConfiguration(typeof(CityConfiguration))]
    public class City : BaseAuditableEntity
    {
        [Key]
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

        public int? Level { get; set; }

        public int? ParentId { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public string? Native { get; set; }

        public long? Population { get; set; }

        public string? Timezone { get; set; }

        /// <summary>
        /// JSON column storing translations
        /// </summary>
        //public Dictionary<string, string> Translations { get; set; } = new();
        public string? Translations { get; set; } 

        public bool Flag { get; set; }

        public string? WikiDataId { get; set; }
    }
}
