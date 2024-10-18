using FluentAssertions;
using GestionOT5.MVVM.Models;
using GestionOT5.MVVM.ViewModels.PartesEdicion;
using GestionOT5.Services.Materiales;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using GestionOT5.Services.Subfamilias;
using GestionOT5.Services.Tareas;
using GestionOT5.Services.Tecnicos;
using GestionOT5.Services.Tiempo;
using GestionOT5.Services.Vehiculos;
using Moq;

namespace GestionOT5.Test.MVVM.PartesEdicion
{
    public class PartesEdicionViewModelTest : IClassFixture<PartesEdicionViewModelFixture>
    {
        private readonly Mock<IVehiculosService> vehiculosService;
        private readonly Mock<ITecnicosService> tecnicosService;
        private readonly Mock<IFamiliaService> subfamiliaService;
        private readonly Mock<ITareasService> tareasService;
        private readonly Mock<IMaterialesService> materialesService;
        private readonly Mock<IMessagesService> messagesService;
        private readonly Mock<ITiempoService> tiempoService;
        private readonly Mock<INavigationService> navigationService;

        public PartesEdicionViewModelTest(PartesEdicionViewModelFixture fixture)
        {
            vehiculosService = fixture.VehiculosService;
            tecnicosService = fixture.TecnicosService;
            subfamiliaService = fixture.SubfamiliaService;
            tareasService = fixture.TareasService;
            materialesService = fixture.MaterialesService;
            messagesService = fixture.MessagesService;
            tiempoService = fixture.TiempoService;
            navigationService = fixture.NavigationService;
        }

        [Fact]
        public async Task OnAppearingaTimerShouldBeEnabledWhenTimeIsRunning()
        {
            //Arrange
            tiempoService.Setup(tiempoService => tiempoService.ExistsTiempo()).Returns(true);
            tiempoService.Setup(tiempoService => tiempoService.GetHoraInicio()).Returns(DateTime.Now);
            tiempoService.Setup(tiempoService => tiempoService.GetTiempoEnMarcha()).Returns(true);

            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object);

            //Act
            await partesEdicionViewModel.OnAppearing();

            //Assert
            partesEdicionViewModel.ATimer.Enabled.Should().BeTrue();

        }

        [Fact]
        public async Task PauseTimerShouldBeDisabled()
        {
            //Arrange
            tiempoService.Setup(tiempoService => tiempoService.ExistsTiempo()).Returns(true);
            tiempoService.Setup(tiempoService => tiempoService.GetHoraInicio()).Returns(DateTime.Now);
            tiempoService.Setup(tiempoService => tiempoService.GetTiempoEnMarcha()).Returns(true);

            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object);

            //Act
            await partesEdicionViewModel.OnAppearing();
            partesEdicionViewModel.Pause();

            //Assert
            partesEdicionViewModel.ATimer.Enabled.Should().BeFalse();
        }

        [Fact]
        public async Task SearchGoToBusquedaPage()
        {
            //Arrange
            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object);

            //Act
            await partesEdicionViewModel.Search("Material");

            //Assert
            navigationService.Verify(servicio => servicio.GoToAsync("BusquedaPage?tipo=Material"), Times.Once);
        }

        [Fact]
        public async Task StarTimerShouldBeEnabled()
        {
            //Arrange
            tiempoService.Setup(tiempoService => tiempoService.ExistsTiempo()).Returns(true);
            tiempoService.Setup(tiempoService => tiempoService.GetHoraInicio()).Returns(DateTime.Now);
            tiempoService.Setup(tiempoService => tiempoService.GetTiempoEnMarcha()).Returns(false);

            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object);

            //Act
            await partesEdicionViewModel.OnAppearing();
            partesEdicionViewModel.Start();

            //Assert
            partesEdicionViewModel.ATimer.Enabled.Should().BeTrue();
        }

        [Fact]
        public void DelCantidadShouldBeCantidadEqual0()
        {
            //Arrange

            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object)
            {
                Cantidad = 5
            };

            //Act
            partesEdicionViewModel.DelCantidad();

            //Assert
            partesEdicionViewModel.Cantidad.Should().Be(0);
        }

        [Fact]
        public async Task AddMaterialMaterialesImputadosShouldHave1()
        {
            //Arrange
            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object)
            {
                SelectedMaterial = new GestionOT5.MVVM.Models.Material() { Id = 1, Descripcion = "Marerial Test", Unidad = "Uds" },
                Cantidad = 1
            };

            //Act
            await partesEdicionViewModel.AddMaterial();

            //Assert
            partesEdicionViewModel.MaterialesImputados.Should().HaveCount(1);
        }

        [Fact]
        public async Task AddMaterialMaterialesImputadosShouldHave0()
        {
            //Arrange
            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object)
            {
                SelectedMaterial = new GestionOT5.MVVM.Models.Material() { Id = 1, Descripcion = "Marerial Test", Unidad = "Uds" },
            };

            //Act
            await partesEdicionViewModel.AddMaterial();

            //Assert
            partesEdicionViewModel.MaterialesImputados.Should().HaveCount(0);
            messagesService.Verify(servicio => servicio.ShowCustomToast("Indique una cantidad."), Times.Once);
        }

        [Fact]
        public async Task AddMaterialMaterialesImputadosShouldHave0WhenSelectelMaterialIsNull()
        {
            //Arrange
            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object)
            {
                SelectedMaterial = null,
                MyEntry = new Entry()
            };

            //Act
            await partesEdicionViewModel.AddMaterial();

            //Assert
            partesEdicionViewModel.MaterialesImputados.Should().HaveCount(0);
            messagesService.Verify(servicio => servicio.ShowCustomToast("Seleccione primero un material."), Times.Once);
        }

        [Fact]
        public async Task DelMaterialMaterialesImputadosShouldHave0()
        {
            //Arrange
            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object)
            {
                SelectedMaterial = new GestionOT5.MVVM.Models.Material() { Id = 1, Descripcion = "Marerial Test", Unidad = "Uds" },
                Cantidad = 1,
            };

            //Act
            await partesEdicionViewModel.AddMaterial();
            var a = partesEdicionViewModel.MaterialesImputados.FirstOrDefault();
            partesEdicionViewModel.SelectedMaterialImputado = a;
            await partesEdicionViewModel.DelMaterialCommand.ExecuteAsync(null);

            //Assert
            partesEdicionViewModel.MaterialesImputados.Should().HaveCount(0);
        }

        [Fact]
        public async Task DelMaterialIfSelectedMaterialIsNull()
        {
            //Arrange
            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object)
            {
                SelectedMaterialImputado = null,
            };

            partesEdicionViewModel.SelectedMaterialImputado = partesEdicionViewModel.MaterialesImputados.FirstOrDefault();

            //Act
            await partesEdicionViewModel.DelMaterial();

            //Assert
            messagesService.Verify(servicio => servicio.ShowCustomToast("Seleccione primero un material imputado"), Times.Once);
        }

        [Fact]
        public async Task GoToParte()
        {
            //Arrange
            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object);

            var page = "MaterialesPage";
            //Act
            await partesEdicionViewModel.GoToParte(page);

            //Assert
            navigationService.Verify(servicio => servicio.GoToAsync($"//PartesEdicion/{page}"), Times.Once);
        }

        [Fact]
        public async Task OnDisappearing()
        {
            //Arrange
            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object);

            //Act
            await partesEdicionViewModel.OnDisappearing();
            Action comparison = () => { partesEdicionViewModel.ATimer.Start(); };

            //Assert
            comparison.Should().Throw<ObjectDisposedException>();
        }

        [Fact]
        public void ApplyQueryAttributesOT()
        {
            //Arrange
            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object);

            Dictionary<string, object> dictionay = new Dictionary<string, object>();
            dictionay.Add("ot", new Ot() { Cliente = "Este" });

            //Act
            partesEdicionViewModel.ApplyQueryAttributes(dictionay);

            //Assert
            partesEdicionViewModel.OtDto.Should().NotBeNull();
            partesEdicionViewModel.OtDto?.Cliente.Should().Be("Este");
        }

        [Fact]
        public void ApplyQueryAttributesSelectedMaterial()
        {
            //Arrange
            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object);

            Dictionary<string, object> dictionay = new Dictionary<string, object>();
            dictionay.Add("selectedMaterial", new Material() { Descripcion = "Este Otro" });

            //Act
            partesEdicionViewModel.ApplyQueryAttributes(dictionay);

            //Assert
            partesEdicionViewModel.SelectedMaterial.Should().NotBeNull();
            partesEdicionViewModel.SelectedMaterial?.Descripcion.Should().Be("Este Otro");
        }

        [Fact]
        public async Task AddTecnicoMaterialesImputadosShouldHave1()
        {
            //Arrange
            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object)
            {
                SelectedTecnico = new Tecnico() { Id = 1, Nombre = "Tecnico Test" },
                SelectedFamilia = new Familia() { Id = 1, CodigoSubfamlia = "Subfamilia text" },
                InicioTec = new TimeSpan(1, 30, 0),
                FinTec = new TimeSpan(2, 30, 0),
            };

            //Act
            await partesEdicionViewModel.AddTecnicoCommand.ExecuteAsync(null);

            //Assert
            partesEdicionViewModel.TecnicosImputados.Should().HaveCount(1);
        }

        [Fact]
        public async Task AddTecnicoMaterialesImputadosShouldHave0()
        {
            //Arrange
            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object)
            {
                SelectedTecnico = new Tecnico() { Id = 1, Nombre = "Tecnico Test" },
                InicioTec = new TimeSpan(1, 30, 0),
                FinTec = new TimeSpan(2, 30, 0),
            };

            //Act
            await partesEdicionViewModel.AddTecnicoCommand.ExecuteAsync(null);

            //Assert
            partesEdicionViewModel.MaterialesImputados.Should().HaveCount(0);
            messagesService.Verify(servicio => servicio.ShowCustomToast("Seleccione primero una subfamilia."), Times.Once);
        }

        [Fact]
        public async Task AddTecnicoMaterialesImputadosShouldHave0WhenSelectelTecnicoIsNull()
        {
            //Arrange
            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object)
            {
                SelectedFamilia = new Familia() { Id = 1, CodigoSubfamlia = "Subfamilia text" },
                InicioTec = new TimeSpan(1, 30, 0),
                FinTec = new TimeSpan(2, 30, 0),
            };

            //Act
            await partesEdicionViewModel.AddTecnicoCommand.ExecuteAsync(null);


            //Assert
            partesEdicionViewModel.MaterialesImputados.Should().HaveCount(0);
            messagesService.Verify(servicio => servicio.ShowCustomToast("Seleccione primero un técnico."), Times.Once);
        }        
        
        [Fact]
        public async Task AddTecnicoMaterialesImputadosShouldHave0WhenHoraIncioIsNull()
        {
            //Arrange
            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object)
            {
                SelectedTecnico = new Tecnico() { Id = 1, Nombre = "Tecnico Test" },
                SelectedFamilia = new Familia() { Id = 1, CodigoSubfamlia = "Subfamilia text" },
                FinTec = new TimeSpan(2, 30, 0),
            };

            //Act
            await partesEdicionViewModel.AddTecnicoCommand.ExecuteAsync(null);

            //Assert
            partesEdicionViewModel.MaterialesImputados.Should().HaveCount(0);
            messagesService.Verify(servicio => servicio.ShowCustomToast("Indique una hora de incio."), Times.Once);
        }        
        
        [Fact]
        public async Task AddTecnicoMaterialesImputadosShouldHave0WhenHoraFinIsNull()
        {
            //Arrange
            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object)
            {
                SelectedTecnico = new Tecnico() { Id = 1, Nombre = "Tecnico Test" },
                SelectedFamilia = new Familia() { Id = 1, CodigoSubfamlia = "Subfamilia text" },
                InicioTec = new TimeSpan(1, 30, 0),
            };

            //Act
            await partesEdicionViewModel.AddTecnicoCommand.ExecuteAsync(null);

            //Assert
            partesEdicionViewModel.MaterialesImputados.Should().HaveCount(0);
            messagesService.Verify(servicio => servicio.ShowCustomToast("Indique una hora de fin."), Times.Once);
        }

        [Fact]
        public async Task DelTecnicoTecnicosImputadosShouldHave0()
        {
            //Arrange
            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object)
            {
                SelectedTecnico = new Tecnico() { Id = 1, Nombre = "Tecnico Test" },
                SelectedFamilia = new Familia() { Id = 1, CodigoSubfamlia = "Subfamilia text" },
                InicioTec = new TimeSpan(1, 30, 0),
                FinTec = new TimeSpan(2, 30, 0),
            };

            //Act
            await partesEdicionViewModel.AddTecnicoCommand.ExecuteAsync(null);
            var a = partesEdicionViewModel.TecnicosImputados.FirstOrDefault();
            partesEdicionViewModel.SelectedTecnicoImputado = a;
            await partesEdicionViewModel.DelTecnicoCommand.ExecuteAsync(null);

            //Assert
            partesEdicionViewModel.MaterialesImputados.Should().HaveCount(0);
        }

        [Fact]
        public async Task DelTecnicoIfSelectedMaterialIsNull()
        {
            //Arrange
            PartesEdicionViewModel partesEdicionViewModel = new(vehiculosService.Object, tecnicosService.Object, subfamiliaService.Object, tareasService.Object, messagesService.Object, tiempoService.Object, navigationService.Object)
            {
                SelectedTecnicoImputado = null,
            };

            partesEdicionViewModel.SelectedMaterialImputado = partesEdicionViewModel.MaterialesImputados.FirstOrDefault();

            //Act
            await partesEdicionViewModel.DelTecnicoCommand.ExecuteAsync(null);

            //Assert
            messagesService.Verify(servicio => servicio.ShowCustomToast("Seleccione primero un tecnico imputado"), Times.Once);
        }
    }
}
