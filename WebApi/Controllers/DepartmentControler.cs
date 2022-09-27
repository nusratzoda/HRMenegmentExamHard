using Domain;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class DepartmentControler
{
    private IDepartMentServices _departmentService;
    public DepartmentControler(IDepartMentServices department)
    {
        _departmentService = department;
    }
    [HttpGet("GetDepartments")]
    public async Task<Response<List<department>>> GetDepartments()
    {
        return await _departmentService.GetDepartments();
    }
    [HttpGet("GetDepartmentsById")]
    public async Task<Response<List<department>>> GetDepartmentsById(int id)
    {
        return await _departmentService.GetDepartmentById(id);
    }

    [HttpPost("ADDDepartment")]
    public async Task<Response<department>> AddDepartment(department Department)
    {
        return await _departmentService.AddDepartment(Department);
    }
    [HttpPut("UpdateDepartment")]
    public async Task<Response<department>> UpdateDepartment(department Department)
    {
        return await _departmentService.UpdateDepartment(Department);
    }
}
