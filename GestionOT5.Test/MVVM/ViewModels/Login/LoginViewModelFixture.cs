using GestionOT5.MVVM.Models;
using GestionOT5.MVVM.ViewModels.Login;
using GestionOT5.Services.Login;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using GestionOT5.Services.Preferen;
using Moq;
using NSubstitute;

namespace GestionOT5.Test.MVVM.ViewModels.Login
{
    public sealed class LoginViewModelFixture : IDisposable
    {
        public Mock<INavigationService> navigationService;

        public LoginViewModel LoginViewModel { get; }

        public ResponseLogin ResponseLogin1 { get; }

        public LoginViewModelFixture()
        {
            string user = "Daniel";
            User userDetails = new() { UserName = "Daniel", Email = "daojub1@hotmail.com" };

            ResponseLogin1 = new ResponseLogin()
            {
                LoginOk = true,
                Message = $"Bienvenido de nuevo {user}",
                User = new()
                {
                    UserName = user,
                    Email = "daojub@hotmail.com"
                }
            };

            var loginService = new Mock<ILoginService>();
            loginService.Setup(x => x.CheckLoginAsync(user,"12345")).ReturnsAsync(ResponseLogin1);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(userDetails);

            var messagesService = new Mock<IMessagesService>();
            var preferencesService = new Mock<IPreferencesService>();
            preferencesService.Setup(x => x.GetValue(nameof(App.UserDetails), "")).Returns(json);


            navigationService = new Mock<INavigationService>();
            navigationService.Setup(c => c.GoToAsync(It.IsAny<string>())).Verifiable();

            LoginViewModel = new LoginViewModel(loginService.Object, messagesService.Object, navigationService.Object, preferencesService.Object);
        }

        public void Dispose()
        {
        }
    }
}
