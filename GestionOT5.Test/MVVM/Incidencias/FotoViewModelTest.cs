using FluentAssertions;
using GestionOT5.MVVM.ViewModels.Incidencias;
using GestionOT5.Services.Fotos;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using Moq;

namespace GestionOT5.Test.MVVM.Incidencias
{
    public class FotoViewModelTest : IClassFixture<FotoViewModelFixture>
    {
        private readonly Mock<IMediaPickerService> mediaPickerService;
        private readonly Mock<IMessagesService> messagesService;
        private readonly Mock<INavigationService> navigationService;

        public FotoViewModelTest(FotoViewModelFixture fixture)
        {
            mediaPickerService = fixture.MediaPickerService;
            messagesService = fixture.MessagesService;
            navigationService = fixture.NavigationService;
        }

        [Fact]
        public async Task Back()
        {
            //Arrange
            FotoViewModel fotoViewModel = new(mediaPickerService.Object, messagesService.Object, navigationService.Object);

            //Act
            await fotoViewModel.BackCommand.ExecuteAsync(null);

            //Assert
            navigationService.Verify(servicio => servicio.Back(), Times.Once);
        }

        [Fact]
        public async Task AceptWhenItemPhotoIsNull()
        {
            //Arrange
            FotoViewModel fotoViewModel = new(mediaPickerService.Object, messagesService.Object, navigationService.Object);
            fotoViewModel.ItemPhoto = null;

            //Act
            await fotoViewModel.AceptCommand.ExecuteAsync(null);

            //Assert
            messagesService.Verify(servicio => servicio.ShowCustomToast("Por favor, toma o elige una foto de la galeria."), Times.Once);
        }

        [Fact]
        public async Task AceptWhenItemPhotoNotIsNull()
        {
            //Arrange
            FotoViewModel fotoViewModel = new(mediaPickerService.Object, messagesService.Object, navigationService.Object);
            fotoViewModel.ItemPhoto = new();

            //Act
            await fotoViewModel.AceptCommand.ExecuteAsync(null);

            //Assert
            navigationService.Verify(servicio => servicio.GoToAsync(".."), Times.Once);
        }

        //[Fact]
        //public async Task TakePhotoWithCamera()
        //{
        //    //Arrange
        //    mediaPickerService.Setup(service => service.CapturePhotoAsync()).ReturnsAsync(new FileResult("dotnet_bot.png", "image/png"));


        //    FotoViewModel fotoViewModel = new(mediaPickerService.Object, messagesService.Object, navigationService.Object);
        //    fotoViewModel.ItemPhoto = new();


        //    //Act
        //    await fotoViewModel.TakePhotoCommand.ExecuteAsync("Take");

        //    //Assert
        //    fotoViewModel.Photo.FileName.Should().NotBeNull();
        //}
    }
}
