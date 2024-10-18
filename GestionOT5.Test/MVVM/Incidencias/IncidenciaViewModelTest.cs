using CommunityToolkit.Mvvm.Messaging;
using FluentAssertions;
using GestionOT5.MVVM.Models;
using GestionOT5.MVVM.ViewModels.Incidencias;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using Moq;

namespace GestionOT5.Test.MVVM.Incidencias
{
    public class IncidenciaViewModelTest : IClassFixture<IncidenciaViewModelFixture>
    {
        private readonly Mock<IMessagesService> messagesService;
        private readonly Mock<INavigationService> navigationService;
        public IncidenciaViewModelTest(IncidenciaViewModelFixture fixture)
        {
            messagesService = fixture.MessagesService;
            navigationService = fixture.NavigationService;
        }

        [Fact]
        public async Task Accept()
        {
            //Arrange
            IncidenciaViewModel incidenciaViewModel = new(messagesService.Object, navigationService.Object);
            incidenciaViewModel.Descripcion = "Test";

            //Act
            await incidenciaViewModel.AcceptCommand.ExecuteAsync(null);

            //Assert
            navigationService.Verify(servicio => servicio.GoToAsync(".."), Times.Once);
        }

        [Fact]
        public async Task AcceptWhenDescriptionIsNull()
        {
            //Arrange
            IncidenciaViewModel incidenciaViewModel = new(messagesService.Object, navigationService.Object);

            //Act
            await incidenciaViewModel.AcceptCommand.ExecuteAsync(null);

            //Assert
            navigationService.Verify(servicio => servicio.GoToAsync(".."), Times.Never);
            messagesService.Verify(servicio => servicio.ShowCustomToast("Por favor, indique una descripcion para la incidencia."), Times.Once);
        }

        [Fact]
        public async Task Back()
        {
            //Arrange
            IncidenciaViewModel incidenciaViewModel = new(messagesService.Object, navigationService.Object);

            //Act
            await incidenciaViewModel.BackCommand.ExecuteAsync(null);

            //Assert
            navigationService.Verify(servicio => servicio.Back(), Times.Once);
        }

        [Fact]
        public async Task AddNewFoto()
        {
            //Arrange
            IncidenciaViewModel incidenciaViewModel = new(messagesService.Object, navigationService.Object);

            //Act
            await incidenciaViewModel.AddNewFotoCommand.ExecuteAsync(null);

            //Assert
            navigationService.Verify(servicio => servicio.GoToAsync("FotoPage"), Times.Once);
        }

        [Fact]
        public async Task HandleItemPhoto()
        {
            //Arrange
            IncidenciaViewModel incidenciaViewModel = new(messagesService.Object, navigationService.Object);

            //Act
            await incidenciaViewModel.AddNewFotoCommand.ExecuteAsync(null);
            WeakReferenceMessenger.Default.Send(new ItemPhoto() { Filename = "Test"});

            //Assert
            var itemPhoto = incidenciaViewModel.PhotosList.Where(x => string.Equals(x.Filename, "Test")) as ItemPhoto;
            itemPhoto?.Filename.Should().Be("Test");
        }
    }
}
