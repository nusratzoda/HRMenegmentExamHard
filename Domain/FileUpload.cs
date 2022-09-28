using Microsoft.AspNetCore.Http;

namespace Domain;

public class FileUpload
{
    public IFormFile? File { get; set; }
}
