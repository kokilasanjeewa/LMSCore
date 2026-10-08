using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Docupload
{
    public class UpdateDocUploadDto
    {
        [Required]
        public int DocUploadSerialID { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public short DocTypeSerialID { get; set; }
        [Required]
        [Display(Name = "Name")]
        [StringLength(60, ErrorMessage = "The name cannot exceed 60 characters. ")]
        public string? DocName { get; set; }
        [Required]
        [Display(Name = "Description")]
        [StringLength(60, ErrorMessage = "The description cannot exceed 60 characters. ")]
        public string? DocDescription { get; set; }
        [Required]
        public int ModSerialID { get; set; }
    }
}
