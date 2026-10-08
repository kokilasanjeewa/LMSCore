using Core.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Repositories
{
    public interface IMenuPermissionRepository
    {        IEnumerable<GetMenuPermissionDynamicDto> GetMenuPermissionsAsync();
    }
}
