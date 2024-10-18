using GestionOT5.MVVM.Models;
using GestionOT5.MVVM.Pages.Busqueda;
using GestionOT5.MVVM.Pages.Incidencias;
using GestionOT5.MVVM.Pages.Map;

namespace GestionOT5
{
    public partial class App : Application
    {
        public static User? UserDetails { get; set; }
        public static Jornada? JornadaDetails { get; set; }
        public static TiempoInvertido? TiempoDetails { get; set; }
        public static DateTime HoraInico { get; internal set; }

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            RegisterRoutes();
            MainPage = serviceProvider.GetRequiredService<AppShell>();
        }

        private static void RegisterRoutes()
        {
            Routing.RegisterRoute("BusquedaPage", typeof(BusquedaPage));
            Routing.RegisterRoute("IncidenciasPage", typeof(IncidenciasPage));
            Routing.RegisterRoute("IncidenciaPage", typeof(IncidenciaPage));
            Routing.RegisterRoute("FotoPage", typeof(FotoPage));
            Routing.RegisterRoute("ShowIncidenciaPage", typeof(ShowIncidenciaPage));
            Routing.RegisterRoute("MapPage", typeof(MapPage));
        }
    }
}
