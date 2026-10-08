using ApplicationService.Models;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services
{
    public interface IUserService
    {
        Task<bool> IsEmployeeInUserTableAsync(int employeeId, string token);
        Task<List<UserDto>> GetAllUsersAsync(string token);

    }
}
