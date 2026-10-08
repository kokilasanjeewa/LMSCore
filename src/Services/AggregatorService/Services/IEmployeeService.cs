using ApplicationService.Models;
using HCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services
{
    public interface IEmployeeService
    {
        Task<Employee> GetEmployeeByIdAsync(int employeeId, string token);
        Task<IEnumerable<GetEmployeeWithEmployeeIDDto>> GetAllEmployeesAsync(int comID,string token);

    }
}
