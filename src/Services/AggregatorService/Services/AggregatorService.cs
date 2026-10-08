using ApplicationService.Models;
using ApplicationService.Services;
using HCM.Domain.Entities;

public class AggregatorService : IAggregatorService
{
    private readonly IUserService _userService;
    private readonly IEmployeeService _employeeService;

    public AggregatorService(IUserService userService, IEmployeeService employeeService)
    {
        _userService = userService;
        _employeeService = employeeService;
    }

    public async Task<IEnumerable<GetEmployeeWithEmployeeIDDto>> GetEmployeesNotInUserTableAsync(int comID,string token)
    {
        // Fetch all employees
        var employees = await _employeeService.GetAllEmployeesAsync(comID,token);

        // Fetch all users
        var users = await _userService.GetAllUsersAsync(token);

        // Get employee IDs in user table
        var userIds = new HashSet<long?>(users.Select(u => u.EESerialID));

        // Filter employees not in user table
        var employeesNotInUserTable = employees.Where(e => !userIds.Contains(e.EESerialID));

        return employeesNotInUserTable;
    }
    public async Task<bool> CheckIfEmployeeNotInUserTableAsync(int employeeId, string token)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(employeeId,token);

        if (employee == null)
        {
            throw new Exception("Employee does not exist.");
        }

        return !await _userService.IsEmployeeInUserTableAsync(employeeId,token);
    }
}

