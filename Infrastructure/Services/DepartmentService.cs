using Dapper;
using Domain;
using Services;
using Services.DataContext;

namespace Infrastructure.Services;

public class DepartmentService : IDepartMentServices
{
    private DataContext _context;

    public DepartmentService(DataContext context)
    {
        _context = context;
    }
    public async Task<Response<List<department>>> GetDepartments()
    {
        await using var connection = _context.CreateConnection();

        var response = await connection.QueryAsync<department>($"SELECT department.Id , department.Name , employee.Id as Managerid, CONCAT(FirstName,' ',LastName) as ManagerFullName FROM department JOIN department_employee ON department.id = department_employee.departmentid JOIN employee ON department_employee.employeeid = employee.id;");
        return new Response<List<department>>(response.ToList());
    }
    public async Task<Response<List<department>>> GetDepartmentById(int id)
    {
        await using var connection = _context.CreateConnection();

        var response = await connection.QueryAsync<department>($"SELECT department.Id , department.Name , employee.Id as Managerid, CONCAT(FirstName,' ', LastName) as ManagerFullName FROM department JOIN department_employee ON department.id = department_employee.departmentid JOIN employee ON department_employee.employeeid = employee.id   where department.Id={id};");
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
