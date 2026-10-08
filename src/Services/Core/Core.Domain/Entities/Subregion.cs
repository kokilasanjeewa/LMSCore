using Core.Domain.Common;
using System.ComponentModel.DataAnnotations;


namespace Core.Domain.Entities
{
    public class Subregion : BaseAuditableEntity
    {
        [Key]
        public int SubregionSerialID { get; set; }

        public string Name { get; set; } = null!;

        /// <summary>
        /// JSON column storing translations: key = language code, value = translated name
        /// </summary>
        //public Dictionary<string, string> Translations { get; set; } = new();
        public string? Translations { get; set; }

        /// <summary>
        /// Foreign key to Region
        /// </summary>
        public int RegionSerialID { get; set; }

        public bool Flag { get; set; }

        public string WikiDataId { get; set; } = null!;
    }
}
