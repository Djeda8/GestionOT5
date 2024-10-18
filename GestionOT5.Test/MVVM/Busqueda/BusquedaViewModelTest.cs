using FluentAssertions;
using GestionOT5.MVVM.Models;
using GestionOT5.MVVM.Pages.PartesEdicion.Materiales;
using GestionOT5.MVVM.ViewModels.Busqueda;
using GestionOT5.Services.Materiales;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using Moq;

namespace GestionOT5.Test.MVVM.Busqueda
{
    public class BusquedaViewModelTest
    {
        public BusquedaViewModelTest()
        {
            Seed();
        }

        public List<Material> materialesList;

        [Fact]
        public async Task IniciarJornadaWhenNotExistsJornada()
        {
            //Arrange
            var materialesService = new Mock<IMaterialesService>();

            var messagesService = new Mock<IMessagesService>();

            var navigationService = new Mock<INavigationService>();

            BusquedaViewModel busquedaViewModel = new(materialesService.Object, messagesService.Object, navigationService.Object)
            {
                SelectedMaterial = null
            };

            //Act
            await busquedaViewModel.SelectionChangedCommand.ExecuteAsync(null);

            //Assert
            navigationService.Verify(servicio => servicio.GoToAsyncParameter($"//PartesEdicion/{nameof(MaterialesPage)}", It.IsAny<Dictionary<string, object>>()), Times.Never);

        }      
        
        [Fact]
        public async Task IniciarJornadaWhenNotExistsJornadaShouldNavigate()
        {
            //Arrange
            var materialesService = new Mock<IMaterialesService>();

            var messagesService = new Mock<IMessagesService>();

            var navigationService = new Mock<INavigationService>();

            BusquedaViewModel busquedaViewModel = new(materialesService.Object, messagesService.Object, navigationService.Object)
            {
                SelectedMaterial = new Material(),
            };

            //Act
            await busquedaViewModel.SelectionChangedCommand.ExecuteAsync(null);

            //Assert
            navigationService.Verify(servicio => servicio.GoToAsyncParameter($"//PartesEdicion/{nameof(MaterialesPage)}", It.IsAny<Dictionary<string, object>>()), Times.Once);
        }       
        
        [Fact]
        public async Task ReloadWhenSearchTextIsNull()
        {
            //Arrange
            var materialesService = new Mock<IMaterialesService>();
            materialesService.Setup(service => service.GetMaterialesAsync()).ReturnsAsync(materialesList);

            var messagesService = new Mock<IMessagesService>();

            var navigationService = new Mock<INavigationService>();

            BusquedaViewModel busquedaViewModel = new(materialesService.Object, messagesService.Object, navigationService.Object)
            {
                SearchText = null,
                materialesList = materialesList
            };

            //Act
            await busquedaViewModel.Reload();

            //Assert
            busquedaViewModel.Materiales.Should().HaveCount(8);
        }

        [Fact]
        public async Task ReloadWhenSearchTextIsNotNull()
        {
            //Arrange
            var materialesService = new Mock<IMaterialesService>();
            materialesService.Setup(service => service.GetMaterialesAsync()).ReturnsAsync(materialesList);

            var messagesService = new Mock<IMessagesService>();

            var navigationService = new Mock<INavigationService>();

            BusquedaViewModel busquedaViewModel = new(materialesService.Object, messagesService.Object, navigationService.Object)
            {
                SearchText = "1",
                materialesList = materialesList
            };

            //Act
            await busquedaViewModel.Reload();

            //Assert
            busquedaViewModel.Materiales.Should().HaveCount(1);
        }     
        
        [Fact]
        public async Task OnAppearing()
        {
            //Arrange
            var materialesService = new Mock<IMaterialesService>();
            materialesService.Setup(service => service.GetMaterialesAsync()).ReturnsAsync(materialesList);

            var messagesService = new Mock<IMessagesService>();

            var navigationService = new Mock<INavigationService>();

            BusquedaViewModel busquedaViewModel = new(materialesService.Object, messagesService.Object, navigationService.Object);

            //Act
            await busquedaViewModel.OnAppearing();

            //Assert
            busquedaViewModel.Materiales.Should().HaveCount(8);
        }
        private void Seed()
        {
            materialesList = new List<Material>()
            {
                 new()
                 {
                     Id = 1,
                     Descripcion = "Material 1",
                     Unidad = "Uds"
                 },
                 new()
                 {
                     Id = 2,
                     Descripcion = "Material 2",
                     Unidad = "Uds"
                 },
                 new()
                 {
                     Id = 3,
                     Descripcion = "Material 3",
                     Unidad = "Uds"
                 },
                 new()
                 {
                     Id = 4,
                     Descripcion = "Material 4",
                     Unidad = "Uds"
                 },
                 new()
                 {
                     Id = 5,
                     Descripcion = "Material 5",
                     Unidad = "Uds"
                 },
                 new()
                 {
                     Id = 6,
                     Descripcion = "Material 6",
                     Unidad = "Uds"
                 },
                 new()
                 {
                     Id = 7,
                     Descripcion = "Material 7",
                     Unidad = "Uds"
                 },
                 new()
                 {
                     Id = 8,
                     Descripcion = "Material 8",
                     Unidad = "Uds"
                 },
            };
        }
    }
}
