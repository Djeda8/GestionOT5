using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionOT5.MVVM.Models;
using GestionOT5.Services.Login;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using GestionOT5.Services.Preferen;

namespace GestionOT5.MVVM.ViewModels.Login
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly ILoginService _loginService;
        private readonly IPreferencesService _preferencesService;

        [ObservableProperty]
        private string? user;

        [ObservableProperty]
        private string? password;

        public LoginViewModel(ILoginService loginService, IMessagesService messagesService, INavigationService navigationService, IPreferencesService preferencesService) : base(messagesService, navigationService)
        {
            _loginService = loginService;
            _preferencesService = preferencesService;
        }

        [RelayCommand]
        public async Task Login()
        {
            //User = "Daniel";
            //Password = "12345";

            if (!await CheckEntries())
                return;

            var response = await CallLoginService();

            await ShowMessage(response);

            if (response.LoginOk)
            {
                await LoginOk(response.User);
            }
        }

        private async Task ShowMessage(ResponseLogin response)
        {
            if (!string.IsNullOrEmpty(response.Message))
                await _messagesService.ShowCustomToast(response.Message);
        }

        private async Task<ResponseLogin> CallLoginService()
        {
            return await _loginService.CheckLoginAsync(User, Password);
        }

        private async Task<bool> CheckEntries()
        {
            if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
            {
                await _messagesService.ShowCustomToast("Por favor, indique usuario y contraseña");
                return false;
            }
            return true;
        }

        public async Task LoginOk(User userDetails)
        {
            string userDetailStr = Newtonsoft.Json.JsonConvert.SerializeObject(userDetails);
            _preferencesService.SetValue(nameof(App.UserDetails), userDetailStr);

            await _navigationService.GoToAsync("//Jornada");
        }

        private async Task CheckUser()
        {
            string userDetailsStr = _preferencesService.GetValue(nameof(App.UserDetails), "");
            if (!string.IsNullOrWhiteSpace(userDetailsStr))
            {
                var userInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(userDetailsStr);
                App.UserDetails = userInfo;

                await _messagesService.ShowCustomToast($"Bienvenido de nuevo {userInfo?.UserName}");

                await _navigationService.GoToAsync("//Jornada");
            }
        }

        public override async Task OnAppearing()
        {
            await CheckUser();
            await base.OnAppearing();
        }
    }
}
