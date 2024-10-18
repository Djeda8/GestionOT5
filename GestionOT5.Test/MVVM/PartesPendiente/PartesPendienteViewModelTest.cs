using FluentAssertions;
using GestionOT5.MVVM.Models;
using GestionOT5.MVVM.Pages.PartesEdicion.Tiempo;
using GestionOT5.MVVM.ViewModels.PartesPendiente;
using GestionOT5.Services.Jornada;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using Moq;

namespace GestionOT5.Test.MVVM.PartesPendiente
{
    public class PartesPendienteViewModelTest : IClassFixture<PartesPendienteViewModelFixture>
    {
        private readonly Mock<IJornadaService> jornadaService;
        private readonly Mock<IMessagesService> messagesService;
        private readonly Mock<INavigationService> navigationService;
        public PartesPendienteViewModelTest(PartesPendienteViewModelFixture fixture)
        {
            jornadaService = fixture.JornadaService;
            messagesService = fixture.MessagesService;
            navigationService = fixture.NavigationService;
        }

        [Fact]
        public void ApplyQueryAttributesOT()
        {
            //Arrange
            PartesPendienteViewModel partesPendienteViewModel = new(jornadaService.Object, messagesService.Object, navigationService.Object);

            Dictionary<string, object> dictionay = new Dictionary<string, object>();
            dictionay.Add("ot", new Ot() { Cliente = "Este" });

            //Act
            partesPendienteViewModel.ApplyQueryAttributes(dictionay);

            //Assert
            partesPendienteViewModel.OtDto.Should().NotBeNull();
            partesPendienteViewModel.OtDto?.Cliente.Should().Be("Este");
        }

        [Fact]
        public async Task OkExistsJornadaTrue()
        {
            //Arrange
            jornadaService.Setup(jornadaService => jornadaService.ExistsJornada()).Returns(true);
            
            PartesPendienteViewModel partesPendienteViewModel = new(jornadaService.Object, messagesService.Object, navigationService.Object)
            {
                OtDto = new Ot()
            };

            var navigationParameter = new Dictionary<string, object>()
            {
                { "ot", partesPendienteViewModel.OtDto }
            };

            //Act
            await partesPendienteViewModel.OkCommand.ExecuteAsync(null);

            //Assert
            navigationService.Verify(servicio => servicio.GoToAsyncParameter($"//PartesEdicion/{nameof(TiempoPage)}", It.IsAny<Dictionary<string, object>>()), Times.Once);
            navigationService.Verify(servicio => servicio.GoToAsyncParameter($"//PartesEdicion/{nameof(TiempoPage)}", navigationParameter), Times.Once);
        }

        [Fact]
        public async Task OkExistsJornadaFalse()
        {
            //Arrange
            jornadaService.Setup(jornadaService => jornadaService.ExistsJornada()).Returns(false);

            PartesPendienteViewModel partesPendienteViewModel = new(jornadaService.Object, messagesService.Object, navigationService.Object);

            var navigationParameter = new Dictionary<string, object>()
            {
                { "ot", new Ot() }
            };
            //Act
            await partesPendienteViewModel.OkCommand.ExecuteAsync(null);

            //Assert
            messagesService.Verify(servicio => servicio.ShowCustomToast("Por favor, incie jornada antes de abrir el parte."), Times.Once);
        }

        [Fact]
        public async Task GoMap()
        {
            //Arrange
            PartesPendienteViewModel partesPendienteViewModel = new(jornadaService.Object, messagesService.Object, navigationService.Object);
            partesPendienteViewModel.OtDto = new();

            //Act
            await partesPendienteViewModel.GoMapCommand.ExecuteAsync(null);

            //Assert
            navigationService.Verify(servicio => servicio.GoToAsyncParameter($"MapPage", It.IsAny<Dictionary<string, object>>()), Times.Once);
        }
    }
}
