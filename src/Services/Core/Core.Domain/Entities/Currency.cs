using System.ComponentModel.DataAnnotations;
using Core.Domain.Common;

namespace Core.Domain.Entities
{
    public class Currency : BaseAuditableEntity
    {
        [Key]
        public int CurSerialID { get; set; }
        [Required]
        public int CurID { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The department name cannot exceed 30 characters. ")]
        public string? CurNmame { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "The currency code cannot exceed 3 characters. ")]
        public string? CurCode { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "The currency symbol cannot exceed 3 characters. ")]
        public string? CurSymbol { get;}

    }
}
