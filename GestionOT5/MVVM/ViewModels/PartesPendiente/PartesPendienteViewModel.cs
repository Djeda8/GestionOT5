using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionOT5.MVVM.Models;
using GestionOT5.MVVM.Pages.PartesEdicion.Tiempo;
using GestionOT5.Services.Jornada;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;

namespace GestionOT5.MVVM.ViewModels.PartesPendiente
{
    //[QueryProperty(nameof(Numero), "numero")]
    public partial class PartesPendienteViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IJornadaService _jornadaService;

        [ObservableProperty]
        private string? numero;

        [ObservableProperty]
        private Ot? otDto;

        public PartesPendienteViewModel(IJornadaService jornadaService, IMessagesService messagesService, INavigationService navigationService) : base(messagesService, navigationService)
        {
            _jornadaService = jornadaService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("ot"))
            {
                OtDto = query["ot"] as Ot;
            }
            //Numero = query["numero"].ToString();

        }

        [RelayCommand]
        public async Task GoMap()
        {
            if (OtDto is not null)
            {
                var navigationParameter = new Dictionary<string, object>()
                    {
                        { "ot", OtDto }
                    };
                await _navigationService.GoToAsyncParameter($"MapPage", navigationParameter);
            }
        }

        [RelayCommand]
        public async Task Ok()
        {
            if (!_jornadaService.ExistsJornada())
            {
                await _messagesService.ShowCustomToast("Por favor, incie jornada antes de abrir el parte.");
                return;
            }

            var navigationParameter = new Dictionary<string, object>()
            {
                { "ot", OtDto }
            };
            await _navigationService.GoToAsyncParameter($"//PartesEdicion/{nameof(TiempoPage)}", navigationParameter);
        }
    }
}
