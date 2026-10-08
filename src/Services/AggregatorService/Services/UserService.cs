using ApplicationService.Models;
using ApplicationService.Services;
using Core.Domain.Entities;
using HCM.Domain.Entities;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private void AddAuthorizationHeader(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<List<UserDto>> GetAllUsersAsync(string token)
    {
        AddAuthorizationHeader(token);

        try
        {

            var response = await _httpClient.GetAsync($"auth/getall");

            // Log response details
            Console.WriteLine($"Response Status: {response.StatusCode}");

            response.EnsureSuccessStatusCode(); // Ensure the status code is 2xx

            // Deserialize the content to IEnumerable<User>
            var users = await response.Content.ReadFromJsonAsync<IEnumerable<UserDto>>();

            return (List<UserDto>)(users ?? new List<UserDto>()); // Return an empty list if the result is null
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request error: {ex.Message}");
            throw new ApplicationException("An error occurred while retrieving users.", ex);
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Deserialization error: {ex.Message}");
            throw new ApplicationException("An error occurred while deserializing the user data.", ex);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Request canceled");
            throw new TaskCanceledException("The request was canceled.");
        }
    }

    public async Task<bool> IsEmployeeInUserTableAsync(int employeeId, string token)
    {
        AddAuthorizationHeader(token);
        var response = await _httpClient.PostAsJsonAsync("api/employees/getbyid", new { EmployeeId = employeeId });

        if (response.IsSuccessStatusCode)
        {
            var employee = await response.Content.ReadFromJsonAsync<Employee>();
            return employee != null;  // Return true if employee is found
        }

        return false; // Return false if the request failed
    }

}

