using GestionOT5.MVVM.Models;
using GestionOT5.MVVM.ViewModels.Login;
using GestionOT5.Services.Login;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using GestionOT5.Services.Preferen;
using Moq;

namespace GestionOT5.Test.MVVM.ViewModels.Login
{
    public class LoginViewModelTests : IClassFixture<LoginViewModelFixture>
    {
        private readonly LoginViewModel _loginViewModel;

        private Mock<INavigationService> navigationServiceF;

        private ResponseLogin responseLogin1;

        public LoginViewModelTests(LoginViewModelFixture fixture)
        {
            _loginViewModel = fixture.LoginViewModel;
            navigationServiceF = fixture.navigationService;
            responseLogin1 = fixture.ResponseLogin1;
        }

        [Fact]
        public async Task LoginTestWhenPasswordIsEmpty()
        {
            //Arrange
            var loginService = new Mock<ILoginService>();

            User userDetails = new() { UserName = "Daniel", Email = "daojub1@hotmail.com" };

            var messagesService = new Mock<IMessagesService>();
            //messagesService.Setup(x => x.ShowCustomToast(It.IsAny<String>())).Verifiable();

            var preferencesService = new Mock<IPreferencesService>();

            var navigationService = new Mock<INavigationService>();

            var loginViewModel = new LoginViewModel(loginService.Object, messagesService.Object, navigationService.Object, preferencesService.Object);

            loginViewModel.User = "Daniel";
            loginViewModel.Password = string.Empty;

            //Act
            await loginViewModel.LoginCommand.ExecuteAsync(null);

            //Assert
            messagesService.Verify(messagesService => messagesService.ShowCustomToast("Por favor, indique usuario y contraseña"), Times.Once);
        }

        [Fact]
        public async Task LoginTestWhenAllIsOK()
        {
            //Arrange
            var loginService = new Mock<ILoginService>();
            ResponseLogin responseLogin = new ResponseLogin()
            {
                LoginOk = true,
                Message = $"Bienvenido de nuevo Daniel",
                User = new()
                {
                    UserName = "Daniel",
                    Email = "daojub1@hotmail.com"
                }
            };

            loginService.Setup(x => x.CheckLoginAsync("Daniel", "12345")).ReturnsAsync(responseLogin);

            User userDetails = new() { UserName = "Daniel", Email = "daojub1@hotmail.com" };
            string userDetailStr = Newtonsoft.Json.JsonConvert.SerializeObject(userDetails);

            var messagesService = new Mock<IMessagesService>();
            //messagesService.Setup(x => x.ShowCustomToast(It.IsAny<String>())).Verifiable();

            var preferencesService = new Mock<IPreferencesService>();

            var navigationService = new Mock<INavigationService>();
            //navigationService.Setup(c => c.GoToAsync(It.IsAny<String>())).Verifiable();

            var loginViewModel = new LoginViewModel(loginService.Object, messagesService.Object, navigationService.Object, preferencesService.Object);

            loginViewModel.User = "Daniel";
            loginViewModel.Password = "12345";

            //Act
            await loginViewModel.LoginCommand.ExecuteAsync(null);

            //Assert
            messagesService.Verify(messagesService => messagesService.ShowCustomToast("Bienvenido de nuevo Daniel"), Times.Once);
            preferencesService.Verify(servicio => servicio.SetValue(nameof(App.UserDetails), userDetailStr), Times.Once);
            navigationService.Verify(servicio => servicio.GoToAsync("//Jornada"), Times.Once);

        }

        [Fact]
        public async Task LoginOkTestShouldCallGoAsyncOnce()
        {
            //Arrange
            User userDetails = new() { UserName = "Daniel", Email = "daojub1@hotmail.com" };

            //Act
            await _loginViewModel.LoginOk(userDetails);

            //Assert
            navigationServiceF.Verify(servicio => servicio.GoToAsync("//Jornada"), Times.Once);
        }

        [Fact]
        public async Task LoginOkTest2ShouldCallGoAsyncOnce()
        {
            //Arrange
            var loginService = new Mock<ILoginService>();

            var messagesService = new Mock<IMessagesService>();
            var preferencesService = new Mock<IPreferencesService>();

            var navigationService = new Mock<INavigationService>();
            navigationService.Setup(c => c.GoToAsync(It.IsAny<String>())).Verifiable();

            var loginViewModel = new LoginViewModel(loginService.Object, messagesService.Object, navigationService.Object, preferencesService.Object);

            User userDetails = new() { UserName = "Daniel", Email = "daojub1@hotmail.com" };

            //Act
            await loginViewModel.LoginOk(userDetails);

            //Assert
            navigationService.Verify(servicio => servicio.GoToAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task OnAppearingCheckUser()
        {
            //Arrange
            var loginService = new Mock<ILoginService>();

            User userDetails = new() { UserName = "Daniel", Email = "daojub1@hotmail.com" };
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(userDetails);

            var messagesService = new Mock<IMessagesService>();

            var preferencesService = new Mock<IPreferencesService>();
            preferencesService.Setup(x => x.GetValue(nameof(App.UserDetails), "")).Returns(json);

            var navigationService = new Mock<INavigationService>();
            navigationService.Setup(c => c.GoToAsync(It.IsAny<String>())).Verifiable();

            var loginViewModel = new LoginViewModel(loginService.Object, messagesService.Object, navigationService.Object, preferencesService.Object);


            //Act
            await loginViewModel.OnAppearing();

            //Assert
            navigationService.Verify(servicio => servicio.GoToAsync("//Jornada"), Times.Once);
        }

        [Fact]
        public async Task OnAppearingCheckUserWhenUserIsEmpty()
        {
            //Arrange
            var loginService = new Mock<ILoginService>();

            User userDetails = new() { UserName = "Daniel", Email = "daojub1@hotmail.com" };
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(userDetails);

            var messagesService = new Mock<IMessagesService>();

            var preferencesService = new Mock<IPreferencesService>();
            preferencesService.Setup(x => x.GetValue(nameof(App.UserDetails), "")).Returns("");

            var navigationService = new Mock<INavigationService>();
            navigationService.Setup(c => c.GoToAsync(It.IsAny<String>())).Verifiable();

            var loginViewModel = new LoginViewModel(loginService.Object, messagesService.Object, navigationService.Object, preferencesService.Object);

            //Act
            await loginViewModel.OnAppearing();

            //Assert
            navigationService.Verify(servicio => servicio.GoToAsync("//Jornada"), Times.Never);
        }
    }
}
