using AutoMapper;
using CommunityToolkit.Maui;
using GestionOT5.Dto;
using GestionOT5.MVVM.Pages.Agenda;
using GestionOT5.MVVM.Pages.Busqueda;
using GestionOT5.MVVM.Pages.Incidencias;
using GestionOT5.MVVM.Pages.Jornada;
using GestionOT5.MVVM.Pages.Login;
using GestionOT5.MVVM.Pages.Map;
using GestionOT5.MVVM.Pages.OtsFinalizadas;
using GestionOT5.MVVM.Pages.OtsPendientes;
using GestionOT5.MVVM.Pages.PartesEdicion.Materiales;
using GestionOT5.MVVM.Pages.PartesEdicion.Parte;
using GestionOT5.MVVM.Pages.PartesEdicion.Tecnicos;
using GestionOT5.MVVM.Pages.PartesEdicion.Tiempo;
using GestionOT5.MVVM.Pages.PartesPendiente.Historico;
using GestionOT5.MVVM.Pages.PartesPendiente.Parte;
using GestionOT5.MVVM.ViewModels.AppShell;
using GestionOT5.MVVM.ViewModels.Busqueda;
using GestionOT5.MVVM.ViewModels.Incidencias;
using GestionOT5.MVVM.ViewModels.Jornada;
using GestionOT5.MVVM.ViewModels.Login;
using GestionOT5.MVVM.ViewModels.Map;
using GestionOT5.MVVM.ViewModels.OtsPendientes;
using GestionOT5.MVVM.ViewModels.PartesEdicion;
using GestionOT5.MVVM.ViewModels.PartesPendiente;
using GestionOT5.Services.Fotos;
using GestionOT5.Services.Geocoding;
using GestionOT5.Services.Jornada;
using GestionOT5.Services.Login;
using GestionOT5.Services.Materiales;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using GestionOT5.Services.Ots;
using GestionOT5.Services.Preferen;
using GestionOT5.Services.Subfamilias;
using GestionOT5.Services.Tareas;
using GestionOT5.Services.Tecnicos;
using GestionOT5.Services.Tiempo;
using GestionOT5.Services.Vehiculos;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Maps;

namespace GestionOT5
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fontello2.ttf", "Icons");
                })
                .UseMauiMaps();

#if DEBUG
            builder.Logging.AddDebug();
#endif


            builder.Services.AddTransient<LoginPage>();

            builder.Services.AddTransient<JornadasPage>();

            builder.Services.AddTransient<OtsPendientesPage>();

            builder.Services.AddTransient<PartePPage>();
            builder.Services.AddTransient<HistoricoPage>();

            builder.Services.AddTransient<OtsFinalizadasPage>();

            builder.Services.AddTransient<AgendaPage>();

            builder.Services.AddTransient<TiempoPage>();
            builder.Services.AddTransient<ParteEPage>();
            builder.Services.AddTransient<MaterialesPage>();
            builder.Services.AddTransient<TecnicosPage>();
            builder.Services.AddTransient<BusquedaPage>();
            builder.Services.AddTransient<IncidenciaPage>();
            builder.Services.AddTransient<IncidenciasPage>();
            builder.Services.AddTransient<FotoPage>();
            builder.Services.AddTransient<ShowIncidenciaPage>();
            builder.Services.AddTransient<MapPage>();


            builder.Services.AddTransient<AppShell>();

            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<JornadasViewModel>();
            builder.Services.AddTransient<OtsPendientesViewModel>();
            builder.Services.AddTransient<PartesPendienteViewModel>();
            builder.Services.AddTransient<PartesEdicionViewModel>();
            builder.Services.AddTransient<BusquedaViewModel>();
            builder.Services.AddTransient<IncidenciasViewModel>();
            builder.Services.AddTransient<IncidenciaViewModel>();
            builder.Services.AddTransient<FotoViewModel>();
            builder.Services.AddTransient<ShowIncidenciaViewModel>();
            builder.Services.AddTransient<MapViewModel>();

            builder.Services.AddSingleton<IMediaPickerService, MediaPickerService>();
            builder.Services.AddSingleton<IGeocodingService, GeocodingService>();
            builder.Services.AddSingleton<IPreferencesService, PreferencesService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<ILoginService, LoginServiceMock>();
            builder.Services.AddSingleton<IJornadaService, JornadaService>();
            builder.Services.AddSingleton<ITiempoService, TiempoService>();
            builder.Services.AddSingleton<IMessagesService, MessagesService>();
            builder.Services.AddSingleton<ITareasService, TareasServiceMock>();
            builder.Services.AddSingleton<IMaterialesService, MaterialesServiceMock>();
            builder.Services.AddSingleton<ITecnicosService, TecnicosServiceMock>();
            builder.Services.AddSingleton<IVehiculosService, VehiculosServiceMock>();

            var useMocks = true;
            if (useMocks)
            {
                builder.Services.AddSingleton<IOtService, OtServiceMock>();
                builder.Services.AddSingleton<IFamiliaService, FamiliaServiceMock>();
            }
            else
            {
                builder.Services.AddSingleton<IOtService, OtBLService>();
                builder.Services.AddSingleton<IFamiliaService, FamiliaBLService>();
            }

            builder.Services.AddSingleton(CreateMapper());

            return builder.Build();
        }

        public static IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoMappingProfile>();
            });
            return mapperConfiguration.CreateMapper();
        }
    }
}
