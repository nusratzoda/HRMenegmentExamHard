using Dapper;
using Domain;
using Services.DataContext;

namespace Infrastructure.Services;

public class EmployeeService
{
    private DataContext _context;

    public EmployeeService(DataContext context)
    {
        _context = context;
    }
    public async Task<Response<List<employee>>> GetEmoloyee()
    {
        await using var connection = _context.CreateConnection();

        var response = await connection.QueryAsync<employee>($"select d.id,d.name,  concat('FirstName',' ','LastName' ) as FullName from department as d  INNER JOIN department_employee ON department_employee.departmentid = d.id  INNER JOIN department_manager ON department_manager.departmentid = d.id GROUP BY d.id, d.name;");
        return new Response<List<employee>>(response.ToList());
    }
    public async Task<Response<List<employee>>> GetEmployeeById()
    {
        await using var connection = _context.CreateConnection();

        var response = await connection.QueryAsync<employee>($"select d.id,d.name,  concat('FirstName',' ','LastName' ) as FullName from department as d  INNER JOIN department_employee ON department_employee.departmentid = d.id  INNER JOIN department_manager ON department_manager.departmentid = d.id GROUP BY d.id, d.name;");
        return new Response<List<employee>>(response.ToList());
    }

    public async Task<Response<employee>> AddEmployee(employee Employee)
    {
        using var connection = _context.CreateConnection();
        {
            try
            {


                var sql = $"INSERT into employee(BirthDate,FirstName,LastName,HireDate,Gender)VALUES('{Employee.BirthDate}','{Employee.FirstName}','{Employee.LastName}','{Employee.HireDate}',{(int)Employee.Gender})";
                var id = await connection.ExecuteScalarAsync<int>(sql);
                Employee.Id = id;
                return new Response<employee>(Employee);
            }
            catch (Exception ex)
            {
                return new Response<employee>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

    public async Task<Response<employee>> UpdateEmployee(employee Employee)
    {

        using var connection = _context.CreateConnection();
        {
            string sql = $"UPDATE employee SET BirthDate = {Employee.BirthDate},FirstName ='{Employee.FirstName}',LastName = '{Employee.LastName}',HireDate = '{Employee.HireDate}', Gender = '{Employee.Gender}'  WHERE Id = {Employee.Id};";
            try
            {
                var response = await connection.ExecuteAsync(sql);
                return new Response<employee>(System.Net.HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                return new Response<employee>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
