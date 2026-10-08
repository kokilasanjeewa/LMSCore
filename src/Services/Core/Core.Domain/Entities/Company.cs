using Core.Domain.Common;
using Core.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Core.Domain.Entities
{
    [EntityTypeConfiguration(typeof(CompanyConfiguration))]
    public class Company : BaseAuditableEntity
    {
        [Key]
        public int ComSerialID { get; set; }
        [Required]
        public int ComID { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        [StringLength(60, ErrorMessage = "The company name cannot exceed 60 characters. ")]
        public string? ComName { get; set; }
        [Required]
        [Display(Name = "Company Code")]
        [StringLength(5, ErrorMessage = "The company code cannot exceed 5 characters. ")]
        public string? ComCode { get; set; }
        [Required]
        public int CntrySerialID { get; set; }

        [ForeignKey(nameof(CntrySerialID))]
        public virtual Country? Country { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "The address cannot exceed 60 characters. ")]
        public string? Address1 { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "The address cannot exceed 60 characters. ")]
        public string? Address2 { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "The address cannot exceed 60 characters. ")]
        public string? Address3 { get; set; }

        [Required]
        [StringLength(12, ErrorMessage = "The BR# cannot exceed 12 characters. ")]
        public string? BR { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "The telephone cannot exceed 10 characters. ")]

        public string? Telephone1 { get; set; }

        [StringLength(10, ErrorMessage = "The telephone cannot exceed 10 characters. ")]
        public string? Telephone2 { get; set; }

        [StringLength(10, ErrorMessage = "The telephone cannot exceed 10 characters. ")]
        public string? Telephone3 { get; set; }

        [StringLength(10, ErrorMessage = "The mobile cannot exceed 10 characters. ")]
        public string? Mobile1 { get; set; }

        [StringLength(10, ErrorMessage = "The mobile cannot exceed 10 characters. ")]
        public string? Mobile2 { get; set; }
        [StringLength(10, ErrorMessage = "The fax cannot exceed 10 characters. ")]
        public string? Fax { get; set; }
        [StringLength(35, ErrorMessage = "The email cannot exceed 35 characters. ")]
        public string? Email { get; set; }
        [StringLength(255, ErrorMessage = "The website cannot exceed 255 characters. ")]
        public Uri? WebSite { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "The registered address cannot exceed 60 characters. ")]
        public string? RegAdrs1 { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "The registered address cannot exceed 60 characters. ")]
        public string? RegAdrs2 { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "The registered address cannot exceed 60 characters. ")]
        public string? RegAdrs3 { get; set; }
        [StringLength(20, ErrorMessage = "The vat cannot exceed 20 characters. ")]
        public string? VAT { get; set; }
        [StringLength(20, ErrorMessage = "The svat cannot exceed 20 characters. ")]
        public string? SVAT { get; set; }
        [StringLength(20, ErrorMessage = "The nbt cannot exceed 20 characters. ")]
        public string? NBT { get; set; }
        [StringLength(255, ErrorMessage = "The medium logo url cannot exceed 255 characters. ")]
        public string? CompanyLogoUrl { get; set; }
        [StringLength(255, ErrorMessage = "The small logo cannot exceed 255 characters. ")]
        public string? SmallComLogoUrl { get; set; }
        public byte? SOPayrollPeriodStartDay { get; set; }
        public byte? SOPayrollPeriodEndDay { get; set; }
        public byte? WBPayrollPeriodStartDay { get; set; }
        public byte? WBPayrollPeriodEndDay { get; set; }
        //remove from core module and add to inventory module
        // public virtual ICollection<Warehouse>? Warehouses { get; set; }
    }
}

