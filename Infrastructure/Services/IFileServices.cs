using Domain;

namespace Infrastructure.Services;

public interface IFileServices
{
    Task<Response<string>> InsertFile(FileUpload upload);

}
