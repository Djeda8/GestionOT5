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
    public class PartesEdicionViewModelFixture
    {

        public PartesEdicionViewModelFixture()
        {
            VehiculosService = new Mock<IVehiculosService>();
            TecnicosService = new Mock<ITecnicosService>();
            SubfamiliaService = new Mock<IFamiliaService>();
            TareasService = new Mock<ITareasService>();
            this.MessagesService = new Mock<IMessagesService>();
            this.TiempoService = new Mock<ITiempoService>();
            this.NavigationService = new Mock<INavigationService>();
        }

        public Mock<IVehiculosService> VehiculosService { get; }
        public Mock<ITecnicosService> TecnicosService { get; }
        public Mock<IFamiliaService> SubfamiliaService { get; }
        public Mock<ITareasService> TareasService { get; }
        public Mock<IMaterialesService> MaterialesService { get; }
        public Mock<IMessagesService> MessagesService { get; }
        public Mock<ITiempoService> TiempoService { get; }
        public Mock<INavigationService> NavigationService { get; }
    }
}
