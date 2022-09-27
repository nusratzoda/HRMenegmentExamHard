using Dapper;
using Domain;
using Services.DataContext;

namespace Infrastructure.Services;

public class EmployeeService : IEmployeeServices
{
    private DataContext _context;

    public EmployeeService(DataContext context)
    {
        _context = context;
    }
    public async Task<Response<List<employee>>> GetEmoloyee()
    {
        await using var connection = _context.CreateConnection();

        var response = await connection.QueryAsync<employee>($"SELECT department.Id , department.Name , employee.Id as Managerid, CONCAT(FirstName,' ',LastName) as ManagerFullName FROM department JOIN department_employee ON department.id = department_employee.departmentid JOIN employee ON department_employee.employeeid = employee.id;");
        return new Response<List<employee>>(response.ToList());
    }
    public async Task<Response<List<employee>>> GetEmployeeById(int id)
    {
        await using var connection = _context.CreateConnection();

        var response = await connection.QueryAsync<employee>($"SELECT department.Id, department.Name, employee.Id as Managerid, CONCAT(FirstName, ' ', LastName) as ManagerFullName FROM department JOIN department_employee ON department.id = department_employee.departmentid JOIN employee ON department_employee.employeeid = employee.id   where department.Id ={id}; ");
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
