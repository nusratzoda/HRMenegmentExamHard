using Dapper;
using Domain;
using Services.DataContext;

namespace Infrastructure.Services;

public class MenegerServices
{
    private DataContext _context;

    public MenegerServices(DataContext context)
    {
        _context = context;
    }
    public async Task<Response<List<department_manager>>> GetMenegers()
    {
        await using var connection = _context.CreateConnection();

        var response = await connection.QueryAsync<department_manager>($"select EmployeeId,DepartmentId  concat('FirstName',' ','LastName' ) as FullName from department_manager as d  INNER JOIN department_employee ON department_employee.departmentid = d.id GROUP BY d.id, d.name;");
        return new Response<List<department_manager>>(response.ToList());
    }
    public async Task<Response<department_manager>> AddMeneger(department_manager M)
    {
        using var connection = _context.CreateConnection();
        {
            try
            {
                var sql = $"INSERT into department_manager(EmployeeId,DepartmentId,FromDate,ToDate,CurrentDepartment)VALUES('{M.EmployeeId}','{M.DepartmentId}','{M.FromDate}','{M.ToDate}','{M.CurrentDepartment}')";
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
