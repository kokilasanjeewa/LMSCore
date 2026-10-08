using ApplicationService.Models;
using ApplicationService.Services;
using HCM.Domain.Entities;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;

public class EmployeeService : IEmployeeService
{
    private readonly HttpClient _httpClient;

    public EmployeeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private void AddAuthorizationHeader(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<IEnumerable<GetEmployeeWithEmployeeIDDto>> GetAllEmployeesAsync(int comID, string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.PostAsJsonAsync("Employees/employees-by-company", new { Active =true,ComSerialID = comID });
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<GetEmployeeWithEmployeeIDDto>>();
    }

    public async Task<Employee> GetEmployeeByIdAsync(int employeeId, string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.PostAsJsonAsync("employees/getbyid", new { EmployeeId = employeeId });
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Employee>();
    }
}

