using Domain;

namespace Infrastructure.Services;

public interface IEmployeeServices
{
    Task<Response<List<employee>>> GetEmoloyee();
    Task<Response<List<employee>>> GetEmployeeById(int id);
    Task<Response<employee>> AddEmployee(employee Employee);
    Task<Response<employee>> UpdateEmployee(employee Employee);
}
