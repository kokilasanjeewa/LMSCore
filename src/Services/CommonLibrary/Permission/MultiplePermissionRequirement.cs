using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtTokenAuthentication.Permission
{
    public class MultiplePermissionRequirement : IAuthorizationRequirement
    {
        public int[] PermissionIds { get; }

        public MultiplePermissionRequirement(int[] permissionIds)
        {
            PermissionIds = permissionIds;
        }
    }
}
