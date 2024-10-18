using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionOT5.MVVM.Models;
using GestionOT5.MVVM.Pages.PartesPendiente.Parte;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using GestionOT5.Services.Ots;
using System.Collections.ObjectModel;

namespace GestionOT5.MVVM.ViewModels.OtsPendientes
{
    public partial class OtsPendientesViewModel : BaseViewModel
    {
        private readonly IOtService _otService;

        [ObservableProperty]
        private Ot selectedOT;

        [ObservableProperty]
        private string? searchText;

        [ObservableProperty]
        private ObservableCollection<Ot> ots;

        [ObservableProperty]
        private bool isRefreshing;
        private bool doingSomething;

        public IEnumerable<Ot> OtsList { get; set; }

        public OtsPendientesViewModel(IOtService otService,IMessagesService messagesService, INavigationService navigationService) : base(messagesService, navigationService)
        { 
            _otService = otService;
            SelectedOT = new();
            Ots = [];
        }

        [RelayCommand]
        public async Task SelectionChanged()
        {
            if (SelectedOT is null)
                return;

            var navigationParameter = new Dictionary<string, object>()
            {
                { "ot", SelectedOT }
            };

            await _navigationService.GoToAsyncParameter($"//PartesPendiente/{nameof(PartePPage)}", navigationParameter);
            //await Shell.Current.GoToAsync($"//PartesPendiente/{nameof(PartePPage)}?numero={selectedOT.Numero}");
        }

        [RelayCommand]
        public async Task Reload()
        {
            doingSomething = true;
            IsRefreshing = true;
            await Task.Delay(1000);
            if (string.IsNullOrEmpty(SearchText))
            {
                Ots = new(OtsList);
            }
            else
            {
                Ots = new(OtsList
                .Where(ot => (ot.OtKey.ToLower().Contains(SearchText.ToLower())) || (!string.IsNullOrEmpty(ot.Direccion) && ot.Direccion.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)))
                .ToList<Ot>());
            }
            IsRefreshing = false;
            doingSomething = false;
        }

        [RelayCommand]
        public void Refresh()
        {
            if (doingSomething) return;
            IsRefreshing = false;
        }

        public override async Task OnAppearing()
        {
            doingSomething = true;
            IsRefreshing = true;
            OtsList = await _otService.GetOtsAsync();
            Ots = new(OtsList);
            doingSomething = false;
            IsRefreshing = false;
        }
    }
}
