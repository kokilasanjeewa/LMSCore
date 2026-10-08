using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Common;

namespace Core.Domain.Entities
{
    public class FilePaths : BaseAuditableEntity
    {
        [Key]
        public short FilePathSerialID { get; set; }
        [Required]
        public short DocTypeSerialID { get; set; }
        [ForeignKey(nameof(DocTypeSerialID))]
        public virtual DocType? DocType { get; set; }
        [Required]
        [StringLength(80, ErrorMessage = "The file path cannot exceed 80 characters. ")]
        public string? FilePath { get; set; }
        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}

/*File Path																
F:\DocumentScans\Purchasing\Supplier Quotations																
F:\DocumentScans\Inventory\Item Imnages																
F:\DocumentScans\HRM\Prev. Employment																
F:\DocumentScans\HRM\Personal Documents	*/															
