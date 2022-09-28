using Domain;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly IFileServices _fileServices;

    public FileController(IFileServices fileService)
    {
        _fileServices = fileService;
    }

    [HttpPost("Insert")]
    public Task<Response<string>> Insert([FromForm] FileUpload fileUpload)
    {
        return _fileServices.InsertFile(fileUpload);
    }
}
