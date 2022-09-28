using Domain;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class MenegerController : ControllerBase
{
    private IManagerService _menegereService;
    public MenegerController(IManagerService meneger)
    {
        _menegereService = meneger;
    }
    [HttpGet("GetMenegers")]
    public async Task<Response<List<department_manager>>> GetMenegers()
    {
        return await _menegereService.GetMenegers();
    }
    [HttpPost("ADDMenegers")]
    public async Task<Response<department_manager>> ADDMenegers(department_manager meneger)
    {
        return await _menegereService.AddMeneger(meneger);
    }
}
