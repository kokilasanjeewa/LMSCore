using Core.Domain.Common;
using Core.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Domain.Entities
{
    [EntityTypeConfiguration(typeof(DocUploadConfiguration))]
    public class DocUpload : BaseAuditableEntity
    {
        [Key]
        public int DocUploadSerialID { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public short DocTypeSerialID { get; set; }
        [ForeignKey(nameof(DocTypeSerialID))]
        public virtual DocType? DocType { get; set; }
        [Required]
        [Display(Name = "Company Name")]
        [StringLength(60, ErrorMessage = "The company name cannot exceed 60 characters. ")]
        public string? DocName { get; set; }
        [Required]
        [Display(Name = "Company Name")]
        [StringLength(60, ErrorMessage = "The company name cannot exceed 60 characters. ")]
        public string? DocDescription { get; set; }
        [Required]
        public int ModSerialID { get; set; }

        [ForeignKey(nameof(ModSerialID))]
        public virtual AppModule? Module { get; set; }
    }
}


/*Notes
	1.	If coming from another page, this should be a show field.																
																		
		If came here using the Menu, let the user to select the Module.																
																		
	2.	Document Type: Developers should hardcode these lists.																
																		
		Separate list for each Module.																
																		
		Keep separate folders for Document Type under Modules.																
																		
		eg.  E:\ERP Documents\Purchasing\Quotations (to keep supplier quotations)																
																		
			E:\ERP Documents\Inventory\Items   (to keep Item images)															
																		
	3.	In most cases, Document Name could be the Document #																
																		
		eg. Quoations for PO # 23456 -> Quo-01-23456, Quo-02-23456																
																		
	4.	If needed, add Doc Desc.																

*/

