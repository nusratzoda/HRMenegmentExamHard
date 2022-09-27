using Domain;

namespace Services;


public interface IDepartMentServices
{
    Task<Response<List<department>>> GetDepartments();
    Task<Response<List<department>>> GetDepartmentById(int id);
    Task<Response<department>> AddDepartment(department Department);
    Task<Response<department>> UpdateDepartment(department Department);
}
