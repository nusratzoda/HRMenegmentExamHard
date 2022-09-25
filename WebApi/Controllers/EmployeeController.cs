using Domain;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class EmployeeController
{
    private EmployeeService _employeeService;
    public EmployeeController(EmployeeService employee)
    {
        _employeeService = employee;
    }
    [HttpGet("GetDepartments")]
    public async Task<Response<List<employee>>> GetEmoloyee()
    {
        return await _employeeService.GetEmoloyee();
    }
    [HttpGet("GetDepartmentsById")]
    public async Task<Response<List<employee>>> GetEmployeeById()
    {
        return await _employeeService.GetEmployeeById();
    }

    [HttpPost("ADDEmployee")]
    public async Task<Response<employee>> AddEmployee(employee Employee)
    {
        return await _employeeService.AddEmployee(Employee);
    }
    [HttpPut("UpdateEmployee")]
    public async Task<Response<employee>> UpdateEmployee(employee Employee)
    {
        return await _employeeService.UpdateEmployee(Employee);
    }
}
