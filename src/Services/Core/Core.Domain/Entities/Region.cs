using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Region : BaseAuditableEntity
    {
        [Key]
        public int RegionSerialID { get; set; }

        public string Name { get; set; } = null!;

        /// <summary>
        /// JSON column storing translations: key = language code, value = translated name
        /// </summary>
        //public Dictionary<string, string> Translations { get; set; } = new();
        public string? Translations { get; set; } 

        public bool Flag { get; set; }

        public string WikiDataId { get; set; } = null!;
    }
}
