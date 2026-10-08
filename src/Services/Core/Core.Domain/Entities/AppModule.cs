using Core.Domain.Common;
using System.ComponentModel.DataAnnotations;


namespace Core.Domain.Entities
{
    public class AppModule : BaseAuditableEntity
    {
        [Key]
        public int ModSerialID { get; set; }
        [Required]
        public int ModID { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The module name cannot exceed 20 characters. ")]
        public string? ModName { get; set; }

        public ICollection<Menu>? Menu { get; set; }
    }
}

