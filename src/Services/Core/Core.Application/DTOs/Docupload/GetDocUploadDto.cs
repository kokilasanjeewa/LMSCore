using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Docupload
{
    public class GetDocUploadDto
    {
        [Required]
        public int DocUploadSerialID { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public short DocTypeSerialID { get; set; }
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
    }
}
