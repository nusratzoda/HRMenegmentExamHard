using Domain;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class MenegerController
{
    private MenegerServices _menegereService;
    public MenegerController(MenegerServices meneger)
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
