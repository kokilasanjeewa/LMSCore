using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Repositories
{
    public interface ICurrentUserService
    {
        int UserSerialID { get; set; }
        long LoginLogSerialID { get; set; }
    }
}
