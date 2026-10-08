using Core.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class DocType : BaseAuditableEntity
    {
        [Key]
        public short DocTypeSerialID { get; set; }
        [Required]
        public int ModSerialID { get; set; }
        [ForeignKey(nameof(ModSerialID))]
        public virtual AppModule? Module { get; set; }
        [Required]
        [StringLength(80, ErrorMessage = "The file path cannot exceed 80 characters. ")]
        public string? DocumentType { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}

/*eg.

        Module Document Type								
		Purchasing						Supplier Quotations								
		Purchasing						Supplier Invoices								
		Rental						Customer PO								
		Rental						Customer Quotations								
		Inventory						Item Images								
		Inventory						Supplier AOD								
		HRM						Employee Images								
		HRM						Applications								
		HRM						Vacancy Notices								
		HRM						Prev. Employment								
		HRM						Edu. Certificates								
		HRM						Personal Documents								
*/

