using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

#region To extend the JWT token invalidation logic
/*To extend the JWT token invalidation logic using a SQL table, you'll need to store invalidated tokens in a database and check this list during the token validation process. Here’s a detailed guide on how to implement this in an ASP.NET Core application:

  1. Steps to Implement JWT Token Invalidation with SQL Storage
  2. Set up the database.
  3. Create a table for storing invalidated tokens.
  4. Implement the ITokenService to interact with the database.
  5. Configure the JWT token validation to check the database.
  6. Modify the logout endpoint to store the invalidated token in the database.*/
#endregion

namespace Core.Domain.Entities
{
    public class InvalidateToken : BaseAuditableEntity
    {
        [Key]
        [Column(Order = 0)]
        public int InvdTokenID { get; set; }

        [Required]
        [Column(Order = 1)]
        public string? Token  { get; set; }

        [Required(ErrorMessage = "Please add expiration time to the request.")]
        [Column(Order = 2)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime ExpirationTime { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The User Name cannot exceed 25 characters. ")]
        public string? UserID { get; set; }

    }
}
