namespace PortalGalaxy.Services.Interfaces
{
    public interface IFileUploader
    {
        Task<string> UploadFileAsync(string? base64Imagen, string? archivo);
    }
}
