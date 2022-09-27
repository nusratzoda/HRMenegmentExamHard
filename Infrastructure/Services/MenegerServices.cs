using Dapper;
using Domain;
using Services.DataContext;

namespace Infrastructure.Services;
public class MenegerServices : IManagerService
{
    private DataContext _context;

    public MenegerServices(DataContext context)
    {
        _context = context;
    }
    public async Task<Response<List<department_manager>>> GetMenegers()
    {
        await using var connection = _context.CreateConnection();

        var response = await connection.QueryAsync<department_manager>($"SELECT department.Id , department.Name , employee.Id as Managerid, CONCAT(FirstName,' ',LastName) as ManagerFullName FROM department JOIN department_employee ON department.id = department_employee.departmentid JOIN employee ON department_employee.employeeid = employee.id;");
        return new Response<List<department_manager>>(response.ToList());
    }
    public async Task<Response<department_manager>> AddMeneger(department_manager M)
    {
        using var connection = _context.CreateConnection();
        {
            try
            {
                var sql = $"INSERT into department_manager(EmployeeId,DepartmentId,FromDate,ToDate,CurrentDepartment) VALUES ({(int)M.EmployeeId},{(int)M.DepartmentId},'{M.FromDate}','{M.ToDate}','{M.CurrentDepartment}')";
                var id = await connection.ExecuteScalarAsync<int>(sql);
                M.DepartmentId = id;
                return new Response<department_manager>(M);
            }
            catch (Exception ex)
            {
                return new Response<department_manager>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
