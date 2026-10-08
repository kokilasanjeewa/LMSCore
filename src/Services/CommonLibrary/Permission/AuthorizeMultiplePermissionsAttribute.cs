using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtTokenAuthentication.Permission
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AuthorizeMultiplePermissionsAttribute : AuthorizeAttribute
    {
        public AuthorizeMultiplePermissionsAttribute(params int[] permissionIds)
        {
            Policy = string.Join("||", permissionIds);
        }
    }
}


