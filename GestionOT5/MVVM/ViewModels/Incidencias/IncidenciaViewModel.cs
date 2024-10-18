using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GestionOT5.MVVM.Models;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection.PortableExecutable;

namespace GestionOT5.MVVM.ViewModels.Incidencias
{
    public partial class IncidenciaViewModel : BaseViewModel
    {

        private ItemPhoto? itemPhoto;

        [ObservableProperty]
        private ObservableCollection<ItemPhoto> photosList;

        [ObservableProperty]
        private string descripcion;

        [ObservableProperty]
        private bool esInterna;

        public IncidenciaViewModel(IMessagesService messagesService, INavigationService navigationService) : base(messagesService, navigationService)
        {
            PhotosList = [];
        }

        [RelayCommand]
        public async Task Accept()
        {
            if (!await CheckData())
                return;

            await CreateNewIncidencia();
        }

        private async Task CreateNewIncidencia()
        {
            var incidencia = new Incidencia()
            {
                Descripcion = Descripcion,
                EsInterna = EsInterna,
                PhotosList = photosList
            };

            await SendBackIncidencia(incidencia);
        }

        private async Task SendBackIncidencia(Incidencia incidencia)
        {
            WeakReferenceMessenger.Default.Send(incidencia);
            await _navigationService.GoToAsync("..");
        }

        private async Task<bool> CheckData()
        {
            if (string.IsNullOrEmpty(Descripcion))
            {
                await _messagesService.ShowCustomToast("Por favor, indique una descripcion para la incidencia.");
                return false;
            }
            return true;
        }

        [RelayCommand]
        public async Task Back()
        {
            await _navigationService.Back();
        }

        [RelayCommand]
        public async Task AddNewFoto()
        {
            WeakReferenceMessenger.Default.Register<ItemPhoto>(this, HandleItemPhoto);
            await _navigationService.GoToAsync("FotoPage");
        }

        private void HandleItemPhoto(object recipient, ItemPhoto itemPhoto)
        {
            ItemPhoto newItemPhoto = new()
            {
                Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(itemPhoto.JPGDataBase64))),
                JPGDataBase64 = itemPhoto.JPGDataBase64,
                Filename = itemPhoto.Filename,
                Str = itemPhoto.Str,
            };
            WeakReferenceMessenger.Default.Unregister<ItemPhoto>(this);
            AddListPhotos(newItemPhoto);
        }

        private void AddListPhotos(ItemPhoto itemPhoto)
        {
            photosList.Add(itemPhoto);
        }
    }
}
