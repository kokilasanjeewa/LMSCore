using Core.Domain.Common;
using Core.Domain.Configarations;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    [EntityTypeConfiguration(typeof(MenuConfiguration))]
    public class Menu : BaseAuditableEntity
    {
        [Key]
        [Column(Order = 0)]
        public int MnuSerialID { get; set; }
        [Required]
        [Column(Order = 1)]
        public int MnuID { get; set; }
        [Required]
        [Column(Order = 2)]
        public int? ModSerialID { get; set; }

        [ForeignKey(nameof(ModSerialID))]
        public virtual AppModule? Module { get; set; }
        [Required]
        [Column(Order = 3, TypeName = "tinyint")]
        public int? MnuLevel { get; set; }
        [Required]
        [Column(Order = 4, TypeName = "decimal(8, 6)")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal MnuPosition { get; set; }

        [StringLength(20, ErrorMessage = "The menu name cannot exceed 20 characters. ")]
        [Column(Order = 5, TypeName = "varchar(20)")]

        public string? MnuName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The menu text cannot exceed 30 characters. ")]
        [Column(Order = 6, TypeName = "varchar(30)")]
        public string? MnuText { get; set; }
        [Required]
        [StringLength(35, ErrorMessage = "The page name cannot exceed 35 characters. ")]
        [Column(Order = 7, TypeName = "varchar(35)")]
        public string? PageName { get; set; }
        [Column(Order = 8)]
        public bool IsShown { get; set; }
        [Column(Order = 9)]
        public int? ParentID { get; set; }=0;
        public bool IsRptInc { get; set; } = true;
        public bool IsBtnInc { get; set; } = true;
        [StringLength(1, ErrorMessage = "The MnuMTBR cannot exceed 1 characters. ")]
        [Required]
        public string? MnuMTBR { get; set; }    



    }
} 

 