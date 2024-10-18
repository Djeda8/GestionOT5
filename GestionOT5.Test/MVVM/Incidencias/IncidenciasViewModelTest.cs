using CommunityToolkit.Mvvm.Messaging;
using FluentAssertions;
using GestionOT5.MVVM.Models;
using GestionOT5.MVVM.ViewModels.Incidencias;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using Moq;

namespace GestionOT5.Test.MVVM.Incidencias
{
    public class IncidenciasViewModelTest : IClassFixture<IncidenciasViewModelFixture>
    {
        private readonly Mock<IMessagesService> messagesService;
        private readonly Mock<INavigationService> navigationService;
        public IncidenciasViewModelTest(IncidenciasViewModelFixture fixture)
        {
            messagesService = fixture.MessagesService;
            navigationService = fixture.NavigationService;
        }

        [Fact]
        public async Task VisualiceIncidenciaWhenSelectedIncidenciaIsNull()
        {
            //Arrange
            IncidenciasViewModel incidenciasViewModel = new(messagesService.Object, navigationService.Object);

            //Act
            await incidenciasViewModel.VisualiceIncidenciaCommand.ExecuteAsync(null);
            incidenciasViewModel.SelectedIncidencia = null;

            //Assert
            navigationService.Verify(servicio => servicio.GoToAsyncParameter($"ShowIncidenciaPage", It.IsAny<Dictionary<string, object>>()), Times.Never);
        }       
        
        [Fact]
        public async Task VisualiceIncidencia()
        {
            //Arrange
            IncidenciasViewModel incidenciasViewModel = new(messagesService.Object, navigationService.Object);
            incidenciasViewModel.SelectedIncidencia = new Incidencia()
            {
                Descripcion = "Test",
                EsInterna = true,
            };

            //Act
            await incidenciasViewModel.VisualiceIncidenciaCommand.ExecuteAsync(null);

            //Assert
            navigationService.Verify(servicio => servicio.GoToAsyncParameter($"ShowIncidenciaPage", It.IsAny<Dictionary<string, object>>()), Times.Once);
        }

        [Fact]
        public async Task Back()
        {
            //Arrange
            IncidenciasViewModel incidenciasViewModel = new(messagesService.Object, navigationService.Object);

            //Act
            await incidenciasViewModel.BackCommand.ExecuteAsync(null);

            //Assert
            navigationService.Verify(servicio => servicio.Back(), Times.Once);
        }

        [Fact]
        public async Task GoNewIncidence()
        {
            //Arrange
            var navigationService = new Mock<INavigationService>();
            IncidenciasViewModel incidenciasViewModel = new(messagesService.Object, navigationService.Object);

            //Act
            await incidenciasViewModel.GoNewIncidenceCommand.ExecuteAsync(null);

            //Assert
            navigationService.Verify(servicio => servicio.GoToAsync("IncidenciaPage"), Times.Once);
        }

        [Fact]
        public async Task HandleIncidencia()
        {
            //Arrange
            IncidenciasViewModel incidenciasViewModel = new(messagesService.Object, navigationService.Object);

            //Act
            await incidenciasViewModel.GoNewIncidenceCommand.ExecuteAsync(null);
            WeakReferenceMessenger.Default.Send(new Incidencia() { Descripcion = "Test" });

            //Assert
            var itemPhoto = incidenciasViewModel.IncidenciasList.Where(x => string.Equals(x.Descripcion, "Test")) as Incidencia;
            itemPhoto?.Descripcion.Should().Be("Test");
        }
    }
}
