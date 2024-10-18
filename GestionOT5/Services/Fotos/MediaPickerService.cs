namespace GestionOT5.Services.Fotos
{
    public class MediaPickerService : IMediaPickerService
    {
        public async Task<FileResult?> CapturePhotoAsync()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                return await MediaPicker.Default.CapturePhotoAsync();
            }
            return null;
        }
        public async Task<FileResult?> PickPhotoAsync()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                return await MediaPicker.Default.PickPhotoAsync();
            }
            return null;
        }
    }
}
