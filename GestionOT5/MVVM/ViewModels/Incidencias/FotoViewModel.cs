using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GestionOT5.MVVM.Models;
using GestionOT5.Services.Fotos;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using GestionOT5.Utilities;
using Microsoft.Maui.Graphics.Platform;
using IImage = Microsoft.Maui.Graphics.IImage;

namespace GestionOT5.MVVM.ViewModels.Incidencias
{
    public partial class FotoViewModel : BaseViewModel
    {
        private readonly IMediaPickerService _mediaPickerService;

        [ObservableProperty]
        private ItemPhoto? itemPhoto;

        private FileResult? photo;

        public FileResult? Photo { get => photo; set => photo = value; }

        public FotoViewModel(IMediaPickerService mediaPickerService, IMessagesService messagesService, INavigationService navigationService) : base(messagesService, navigationService)
        {
            _mediaPickerService = mediaPickerService;
        }

        [RelayCommand]
        public async Task Back()
        {
            await _navigationService.Back();
        }

        [RelayCommand]
        public async Task Acept()
        {
            if (ItemPhoto is null)
            {
                await _messagesService.ShowCustomToast("Por favor, toma o elige una foto de la galeria.");
                return;
            }

            WeakReferenceMessenger.Default.Send(ItemPhoto);
            await _navigationService.GoToAsync("..");
        }

        [RelayCommand]
        public async Task TakePhoto(string from)
        {
            Photo = await CallService(from);
            var strResized = await ProcessPhoto(Photo);
            await AssingToImage(strResized, Photo?.FileName);
        }

        private async Task<FileResult?> CallService(string from)
        {
            FileResult? photo = null;
            try
            {
                photo = from switch
                {
                    "Take" => await _mediaPickerService.CapturePhotoAsync(),
                    "Pick" => await _mediaPickerService.PickPhotoAsync(),
                    _ => null,
                };
                return photo;
            }
            catch (FeatureNotSupportedException)
            {
                await _messagesService.ShowCustomToast("La función no es compatible con el dispositivo.");
            }
            catch (PermissionException)
            {
                await _messagesService.ShowCustomToast("Permisos no concedidos.");
            }
            catch (Exception)
            {
                await _messagesService.ShowCustomToast("Error al capturar la imagen con la cámara.");
            }
            return photo;
        }

        private async Task<Stream?> ProcessPhoto(FileResult? photo)
        {
            Stream? strResized = null;
            if (photo != null)
            {
                Stream sourceStream = await photo.OpenReadAsync();
                strResized = ResizeCapturePhoto(sourceStream);
                await AssingToImage(strResized, photo.FileName);
            }
            return strResized;
        }

        private async Task AssingToImage(Stream strResized, string name)
        {
            ItemPhoto = new()
            {
                Filename = $"{name}",
                JPGDataBase64 = Convert.ToBase64String(await strResized.ReadAllBytes()),
                Str = strResized
            };
            strResized.Position = 0;
            ItemPhoto.Source = ImageSource.FromStream(() => strResized);
        }

        private static Stream ResizeCapturePhoto(Stream stream)
        {
            // Resize
            IImage image = PlatformImage.FromStream(stream);
            var resizeImg = image.Downsize(1024, true);
            var str = resizeImg.AsStream();
            return str;
        }
    }
}
