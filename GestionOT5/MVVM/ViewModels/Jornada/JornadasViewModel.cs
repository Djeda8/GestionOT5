using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionOT5.Services.Jornada;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;

namespace GestionOT5.MVVM.ViewModels.Jornada
{
    public partial class JornadasViewModel : BaseViewModel
    {
        private readonly IJornadaService _jornadaService;

        [ObservableProperty]
        private string? mess_1;

        public JornadasViewModel(IJornadaService jornadaService, IMessagesService messagesService, INavigationService navigationService) : base(messagesService, navigationService)
        {
            _jornadaService = jornadaService;
        }

        [RelayCommand]
        public void IniciarJornada()
        {
            if (!_jornadaService.ExistsJornada())
            {
                _jornadaService.CreateJornada(DateTime.Now);
                UpdateJornada();
            }
            else
            {
                _messagesService.ShowCustomToast("Ya tiene una jornada inciada");
            }
        }

        [RelayCommand]
        public void FinalizarJornada()
        {
            if (_jornadaService.ExistsJornada())
            {
                _jornadaService.DeleteJornada();
                UpdateJornada();
            }
            else
            {
                _messagesService.ShowCustomToast("No hay una jornada inciada");
            }
        }

        private void UpdateJornada()
        {
            Mess_1 = _jornadaService.MessageJornada();
        }

        public override async Task OnAppearing()
        {
            UpdateJornada();
            await base.OnAppearing();
        }
    }
}
