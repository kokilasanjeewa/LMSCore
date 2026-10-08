using Core.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{
    public class EntityType : BaseAuditableEntity
    {
        [Key]
        public int EntityTypeID { get; set; }

        [Required, MaxLength(50)]
        public string EntityCode { get; set; } = null!;   // SUPPLIER, CUSTOMER, PO

        [Required, MaxLength(100)]
        public string EntityName { get; set; } = null!;

    }
}
