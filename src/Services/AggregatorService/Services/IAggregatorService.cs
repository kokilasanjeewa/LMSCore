using ApplicationService.Models;
using HCM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services
{
    public interface IAggregatorService
    {
        Task<bool> CheckIfEmployeeNotInUserTableAsync(int employeeId, string token);
        Task<IEnumerable<GetEmployeeWithEmployeeIDDto>> GetEmployeesNotInUserTableAsync(int comID, string token);

    }
}
