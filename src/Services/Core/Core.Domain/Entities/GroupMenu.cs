using Core.Domain.Common;
using Core.Domain.Configarations;
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
    [EntityTypeConfiguration(typeof(GroupMenuConfigaration))]
    public class GroupMenu : BaseAuditableEntity
    {
        [Key]
        public int GrpMnuSerialID { get; set; }
        [Required]
        public int GrpMnuID { get; set; }
        [Required]
        public int GrpSerialID { get; set; }
        [ForeignKey(nameof(GrpSerialID))]
        public virtual Group? Group { get; set; }
        [Required]
        public int MnuID { get; set; }
        [ForeignKey(nameof(MnuID))]
        public virtual Menu? Menu { get; set; }
        public GroupMenu(int grpSerialID, int mnuID)
        {
            GrpSerialID = grpSerialID;
            MnuID = mnuID;
        }
        public GroupMenu()
        {
            
        }

    }
}
