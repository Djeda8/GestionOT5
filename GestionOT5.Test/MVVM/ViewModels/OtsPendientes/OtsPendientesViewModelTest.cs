using FluentAssertions;
using GestionOT5.MVVM.Models;
using GestionOT5.MVVM.Pages.PartesPendiente.Parte;
using GestionOT5.MVVM.ViewModels.OtsPendientes;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using GestionOT5.Services.Ots;
using Moq;

namespace GestionOT5.Test.MVVM.ViewModels.OtsPendientes
{
    public class OtsPendientesViewModelTest
    {
        public OtsPendientesViewModelTest()
        {
            Seed();
        }

        public List<Ot> otsList;

        [Fact]
        public async Task OnAppearingTest()
        {
            //Arrange
            var otService = new Mock<IOtService>();
            otService.Setup(x => x.GetOtsAsync()).ReturnsAsync(otsList);

            var messagesService = new Mock<IMessagesService>();

            var navigationService = new Mock<INavigationService>();

            OtsPendientesViewModel otsPendientesViewModel = new(otService.Object, messagesService.Object, navigationService.Object);

            //Act
            await otsPendientesViewModel.OnAppearing();

            //Assert
            otsPendientesViewModel.OtsList.Should().HaveCount(3);
            otsPendientesViewModel.Ots.Should().HaveCount(3);
        }

        [Fact]
        public async Task SelectionChangedTest()
        {
            //Arrange
            var otService = new Mock<IOtService>();

            var messagesService = new Mock<IMessagesService>();

            var navigationService = new Mock<INavigationService>();

            OtsPendientesViewModel otsPendientesViewModel = new(otService.Object, messagesService.Object, navigationService.Object);

            otsPendientesViewModel.SelectedOT = new Ot();
            var navigationParameter = new Dictionary<string, object>()
            {
                { "ot", otsPendientesViewModel.SelectedOT }
            };

            //Act
            await otsPendientesViewModel.SelectionChanged();

            //Assert
            navigationService.Verify(servicio => servicio.GoToAsyncParameter($"//PartesPendiente/{nameof(PartePPage)}", navigationParameter), Times.Once);
        }

        [Fact]
        public async Task SelectionChangedWhenIsNull()
        {
            //Arrange
            var otService = new Mock<IOtService>();

            var messagesService = new Mock<IMessagesService>();

            var navigationService = new Mock<INavigationService>();

            OtsPendientesViewModel otsPendientesViewModel = new(otService.Object, messagesService.Object, navigationService.Object);

            otsPendientesViewModel.SelectedOT = null;

            //Act
            await otsPendientesViewModel.SelectionChanged();

            //Assert
            navigationService.Verify(servicio => servicio.GoToAsyncParameter($"//PartesPendiente/{nameof(PartePPage)}", It.IsAny<Dictionary<string, object>>()), Times.Never);
        }

        [Fact]
        public async Task ReloadTest()
        {
            //Arrange
            var otService = new Mock<IOtService>();

            var messagesService = new Mock<IMessagesService>();

            var navigationService = new Mock<INavigationService>();

            OtsPendientesViewModel otsPendientesViewModel = new(otService.Object, messagesService.Object, navigationService.Object);

            otsPendientesViewModel.OtsList = otsList;
            otsPendientesViewModel.SearchText = "2";

            //Act
            await otsPendientesViewModel.Reload();

            //Assert
            otsPendientesViewModel.Ots.Should().HaveCount(1);
            otsPendientesViewModel.Ots[0].Cliente.Should().Be("CP PLAZA KOLITZA, 1");

        }    

        [Fact]
        public async Task ReloadTestWhenOTDirectionIsNull()
        {
            //Arrange
            var otService = new Mock<IOtService>();

            var messagesService = new Mock<IMessagesService>();

            var navigationService = new Mock<INavigationService>();

            OtsPendientesViewModel otsPendientesViewModel = new(otService.Object, messagesService.Object, navigationService.Object);

            foreach (var item in otsList)
            {
                item.Direccion = string.Empty;
            }

            otsPendientesViewModel.OtsList = otsList;
            otsPendientesViewModel.SearchText = "2";

            //Act
            await otsPendientesViewModel.Reload();

            //Assert
            otsPendientesViewModel.Ots.Should().HaveCount(1);
            otsPendientesViewModel.Ots[0].Cliente.Should().Be("CP PLAZA KOLITZA, 1");

        }    
        
        [Fact]
        public async Task ReloadTestWhenSearchIsNull()
        {
            //Arrange
            var otService = new Mock<IOtService>();

            var messagesService = new Mock<IMessagesService>();

            var navigationService = new Mock<INavigationService>();

            OtsPendientesViewModel otsPendientesViewModel = new(otService.Object, messagesService.Object, navigationService.Object);

            otsPendientesViewModel.OtsList = otsList;
            otsPendientesViewModel.SearchText = string.Empty;

            //Act
            await otsPendientesViewModel.Reload();

            //Assert
            otsPendientesViewModel.Ots.Should().HaveCount(3);

        }    
        
        private void Seed()
        {
            otsList = new List<Ot>()
            {
                new()
                    {
                        Numero = 32,
                        Serie = "P",
                        Tipo = "PARTE SERVICIOS",
                        CodigoTipo = "5",
                        Cliente = "CP PLAZA KOLITZA, 1",
                        Direccion = "PLAZA KOLITXA, 1",
                        Fecha = new DateTime(2016, 6, 30, 15, 00, 00),
                        Estado = "INICIADA"
                    },
                    new()
                    {
                        Numero = 34,
                        Serie = "P",
                        Tipo = "PARTE OBRA",
                        CodigoTipo = "5",
                        Cliente = "CP SENDEJA, 3 - BILBAO",
                        Direccion = "C/ SENDEJA, 3",
                        Fecha = new DateTime(2016, 6, 30, 18, 30, 00),
                        Estado = "PENDIENTE"
                    },
                    new()
                    {
                        Numero = 35,
                        Serie = "P",
                        Tipo = "PARTE CCTV",
                        CodigoTipo = "5",
                        Cliente = "CP URETAMENDI 49 A 71",
                        Direccion = "C/ URETAMENDI, 49",
                        Fecha = new DateTime(2016, 6, 30, 21, 00, 00),
                        Estado = "PENDIENTE"
                    }
            };
        }
    }
}
