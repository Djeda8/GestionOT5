using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GestionOT5.MVVM.Models;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using Microsoft.Maui.Platform;
using System.Collections.ObjectModel;

namespace GestionOT5.MVVM.ViewModels.Incidencias
{
    public partial class IncidenciasViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Incidencia selectedIncidencia;

        [ObservableProperty]
        private ObservableCollection<Incidencia> incidenciasList;

        public IncidenciasViewModel(IMessagesService messagesService, INavigationService navigationService) : base(messagesService, navigationService)
        {
            IncidenciasList = [];
        }

        [RelayCommand]
        public async Task VisualiceIncidencia()
        {
            if (selectedIncidencia is null)
                return;

            var navigationParameter = new Dictionary<string, object>()
            {
                { "incidencia", SelectedIncidencia }
            };

            await _navigationService.GoToAsyncParameter($"ShowIncidenciaPage", navigationParameter);
        }

        [RelayCommand]
        public async Task Back()
        {
            await _navigationService.Back();
        }

        [RelayCommand]
        public async Task GoNewIncidence()
        {
            WeakReferenceMessenger.Default.Register<Incidencia>(this, HandleIncidencia);
            await _navigationService.GoToAsync("IncidenciaPage");
        }

        private void HandleIncidencia(object recipient, Incidencia incidencia)
        {
            WeakReferenceMessenger.Default.Unregister<Incidencia>(this);
            AddIncidencia(incidencia);
        }

        private void AddIncidencia(Incidencia incidencia)
        {
            incidenciasList.Add(incidencia);
        }
    }
}
