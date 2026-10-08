using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.User
{
    public class UserCreateDto
    {
        [Required]
        [RegularExpression(@"^[A-Z][A-Za-z]*$", ErrorMessage = "The userid first letter capital and no space.")]
        public string? UserID { get; set; }
         [Required]
        [StringLength(50, ErrorMessage = "The User Name cannot exceed 50 characters. ")]
        public string? UserName { get; set; }
        public PermissionType PermissionType { get; set; }
        public int? GrpSerialID { get; set; }
        [Required]
        public string? PassWd { get; set; }
    }
}
