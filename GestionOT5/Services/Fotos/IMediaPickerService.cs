
namespace GestionOT5.Services.Fotos
{
    public interface IMediaPickerService
    {
        Task<FileResult?> CapturePhotoAsync();
        Task<FileResult?> PickPhotoAsync();
    }
}