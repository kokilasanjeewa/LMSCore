using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Models
{
    public class UserDto
    {
        public int UserSerialID { get; set; }
        public long? EESerialID { get; set; }
        public string? UserID { get; set; }
        public string? UserName { get; set; }
    }
}
