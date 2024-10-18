using FluentAssertions;
using GestionOT5.MVVM.ViewModels.Jornada;
using GestionOT5.Services.Jornada;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using GestionOT5.Test.MVVM.ViewModels.Login;
using Moq;

namespace GestionOT5.Test.MVVM.ViewModels.Jornada
{
    public class JornadasViewModelTest
    {
      

        public JornadasViewModelTest()
        {
        }

        [Fact]
        public void IniciarJornadaWhenExistsJornada()
        {
            //Arrange
            var jornadaService = new Mock<IJornadaService>();
            jornadaService.Setup(x => x.ExistsJornada()).Returns(true);

            var messagesService = new Mock<IMessagesService>();

            var navigationService = new Mock<INavigationService>();

            JornadasViewModel jornadasViewModel = new(jornadaService.Object, messagesService.Object, navigationService.Object);

            //Act
            jornadasViewModel.IniciarJornadaCommand.Execute(null);

            //Assert
            messagesService.Verify(messagesService => messagesService.ShowCustomToast("Ya tiene una jornada inciada"), Times.Once);
        }

        [Fact]
        public void IniciarJornadaWhenNotExistsJornada()
        {
            //Arrange
            var jornadaService = new Mock<IJornadaService>();
            jornadaService.Setup(x => x.ExistsJornada()).Returns(false);

            var messagesService = new Mock<IMessagesService>();

            var navigationService = new Mock<INavigationService>();

            JornadasViewModel jornadasViewModel = new(jornadaService.Object, messagesService.Object, navigationService.Object);

            //Act
            jornadasViewModel.IniciarJornada();

            //Assert
            jornadaService.Verify(jornadaService => jornadaService.CreateJornada(It.IsAny<DateTime>()), Times.Once);
            jornadaService.Verify(jornadaService => jornadaService.MessageJornada(), Times.Once);
        }
        
        [Fact]
        public void FinalizarJornadaWhenExistsJornada()
        {
            //Arrange
            var jornadaService = new Mock<IJornadaService>();
            jornadaService.Setup(x => x.ExistsJornada()).Returns(true);

            var messagesService = new Mock<IMessagesService>();

            var navigationService = new Mock<INavigationService>();

            JornadasViewModel jornadasViewModel = new(jornadaService.Object, messagesService.Object, navigationService.Object);

            //Act
            jornadasViewModel.FinalizarJornadaCommand.Execute(null);

            //Assert
            jornadaService.Verify(jornadaService => jornadaService.DeleteJornada(), Times.Once);
            jornadaService.Verify(jornadaService => jornadaService.MessageJornada(), Times.Once);
        }

        [Fact]
        public void FinalizarJornadaWhenNotExistsJornada()
        {
            //Arrange
            var jornadaService = new Mock<IJornadaService>();
            jornadaService.Setup(x => x.ExistsJornada()).Returns(false);

            var messagesService = new Mock<IMessagesService>();

            var navigationService = new Mock<INavigationService>();

            JornadasViewModel jornadasViewModel = new(jornadaService.Object, messagesService.Object, navigationService.Object);

            //Act
            jornadasViewModel.FinalizarJornada();

            //Assert
            messagesService.Verify(messagesService => messagesService.ShowCustomToast("No hay una jornada inciada"), Times.Once);
        }

        


        [Fact]
        public async Task OnAppearingWhenExistsJornada()
        {
            //Arrange
            var jornadaService = new Mock<IJornadaService>();
            jornadaService.Setup(x => x.MessageJornada()).Returns("Jornada sin inciar");

            var messagesService = new Mock<IMessagesService>();

            var navigationService = new Mock<INavigationService>();

            JornadasViewModel jornadasViewModel = new(jornadaService.Object, messagesService.Object, navigationService.Object);

            //Act
            await jornadasViewModel.OnAppearing();

            //Assert
            jornadaService.Verify(jornadaService => jornadaService.MessageJornada(), Times.Once);
            jornadasViewModel.Mess_1.Should().Be("Jornada sin inciar");
        }
    }
}
