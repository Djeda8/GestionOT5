using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionOT5.MVVM.Models;
using GestionOT5.MVVM.Pages.PartesEdicion.Materiales;
using GestionOT5.Services.Materiales;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using System.Collections.ObjectModel;

namespace GestionOT5.MVVM.ViewModels.Busqueda
{
    [QueryProperty(nameof(tipo), "tipo")]
    public partial class BusquedaViewModel : BaseViewModel
    {
        private readonly IMaterialesService _materialesService;

        [ObservableProperty]
        private  Material? selectedMaterial;

        [ObservableProperty]
        private ObservableCollection<Material> materiales;

        private string tipo;

        public IEnumerable<Material> materialesList;

        [ObservableProperty]
        private string? searchText;

        public BusquedaViewModel(IMaterialesService materialesService, IMessagesService messagesService, INavigationService navigationService) : base(messagesService, navigationService)
        {
            _materialesService = materialesService;
            SelectedMaterial = new();
            Materiales = [];
        }

        [RelayCommand]
        public async Task SelectionChanged()
        {
            if (SelectedMaterial is null)
                return;

            var navigationParameter = new Dictionary<string, object>()
            {
                { "selectedMaterial", SelectedMaterial }
            };
            await _navigationService.GoToAsyncParameter($"//PartesEdicion/{nameof(MaterialesPage)}", navigationParameter);
        }

        private async Task CargaListado()
        {
            materialesList = await _materialesService.GetMaterialesAsync();
            Materiales = new(materialesList);
        }

        [RelayCommand]
        public async Task Reload()
        {

            await Task.Delay(1000);
            if (string.IsNullOrEmpty(SearchText))
            {
                Materiales = new(materialesList);
            }
            else
            {
                Materiales = new(materialesList.
                    Where(ma => !string.IsNullOrEmpty(ma.Descripcion) && ma.Descripcion.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)).
                    ToList<Material>());
            }
        }


        public override async Task OnAppearing()
        {
            await CargaListado();

            await base.OnAppearing();
        }
    }
}
