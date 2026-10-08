using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Application.Common.Mappings;

namespace Core.Application.DTOs.DocType
{
    public class UpdateDocTypeDto : IMapFrom<Core.Domain.Entities.DocType>
    {
        [Required]
        public short DocTypeSerialID { get; set; }
        [Required]
        public int ModSerialID { get; set; }
        [Required]
        [StringLength(80, ErrorMessage = "The file path cannot exceed 80 characters. ")]
        public string? DocumentType { get; set; }
        public bool Active { get; set; }

    }
}
