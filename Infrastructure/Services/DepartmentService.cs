using Dapper;
using Domain;
using Npgsql;
using Services.DataContext;

namespace Infrastructure.Services;

public class DepartmentService
{
    private DataContext _context;

    public DepartmentService(DataContext context)
    {
        _context = context;
    }
    public async Task<Response<List<department>>> GetDepartments()
    {
        await using var connection = _context.CreateConnection();

        var response = await connection.QueryAsync<department>($"select d.id,d.name,  concat('FirstName',' ','LastName' ) as FullName from department as d  INNER JOIN department_employee ON department_employee.departmentid = d.id  INNER JOIN department_manager ON department_manager.departmentid = d.id GROUP BY d.id, d.name;");
        return new Response<List<department>>(response.ToList());
    }
    public async Task<Response<List<department>>> GetDepartmentById()
    {
        await using var connection = _context.CreateConnection();

        var response = await connection.QueryAsync<department>($"select d.id,d.name,  concat('FirstName',' ','LastName' ) as FullName from department as d  INNER JOIN department_employee ON department_employee.departmentid = d.id  INNER JOIN department_manager ON department_manager.departmentid = d.id GROUP BY d.id, d.name;");
        return new Response<List<department>>(response.ToList());
    }

    public async Task<Response<department>> AddDepartment(department Department)
    {
        using var connection = _context.CreateConnection();
        {
            try
            {
                var sql = $"INSERT into department(name)VALUES('{Department.Name}')";
                var id = await connection.ExecuteScalarAsync<int>(sql);
                Department.Id = id;
                return new Response<department>(Department);
            }
            catch (Exception ex)
            {
                return new Response<department>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

    public async Task<Response<department>> UpdateDepartment(department Department)
    {

        using var connection = _context.CreateConnection();
        {
            string sql = $"UPDATE department SET Name = '{Department.Name}' WHERE Id = {Department.Id};";
            try
            {
                var response = await connection.ExecuteAsync(sql);
                return new Response<department>(System.Net.HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                return new Response<department>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
