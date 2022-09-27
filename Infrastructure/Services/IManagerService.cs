using Domain;

namespace Infrastructure.Services;

public interface IManagerService
{
    Task<Response<List<department_manager>>> GetMenegers();
    Task<Response<department_manager>> AddMeneger(department_manager M);
}
