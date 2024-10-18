using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionOT5.Services.Navigation;
using GestionOT5.Services.Preferen;

namespace GestionOT5.MVVM.ViewModels.AppShell
{
    public partial class AppShellViewModel : ObservableObject
    {
        private readonly IPreferencesService _preferencesService;
        private readonly INavigationService _navigationService;

        public AppShellViewModel(IPreferencesService preferencesService, INavigationService navigationService)
        {
            _preferencesService = preferencesService;
            _navigationService = navigationService;
        }

        [RelayCommand]
        public async Task Logout()
        {
            if (_preferencesService.ContainsKey(nameof(App.UserDetails)))
            {
                _preferencesService.RemoveKey(nameof(App.UserDetails));
            }

            await _navigationService.GoToAsync("//Login");
            _navigationService.FlyoutIsPresented(false);
        }
    }
}
