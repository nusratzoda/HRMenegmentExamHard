using System.Net;
using Domain;
using Microsoft.AspNetCore.Hosting;
namespace Infrastructure.Services;
public class FileServices : IFileServices
{
    private readonly IWebHostEnvironment _environment;
    public FileServices(IWebHostEnvironment environment)
    {
        _environment = environment;
    }
    public async Task<Response<string>> InsertFile(FileUpload upload)
    {
        try
        {
            if (upload.File != null)
            {
                var rootpath = _environment.WebRootPath;
                var path = Path.Combine(rootpath, "images", upload.File.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await upload.File.CopyToAsync(stream);
                }
                return new Response<string>(HttpStatusCode.OK, upload.File.FileName);
            }
            else
            {
                return new Response<string>(HttpStatusCode.BadRequest, "BadRequest");
            }
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}