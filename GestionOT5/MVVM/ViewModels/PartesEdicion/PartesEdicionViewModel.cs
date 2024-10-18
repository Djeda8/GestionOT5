using CommunityToolkit.Maui.Core.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionOT5.MVVM.Models;
using GestionOT5.Services.Materiales;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using GestionOT5.Services.Subfamilias;
using GestionOT5.Services.Tareas;
using GestionOT5.Services.Tecnicos;
using GestionOT5.Services.Tiempo;
using GestionOT5.Services.Vehiculos;
using System.Collections.ObjectModel;
using System.Timers;

namespace GestionOT5.MVVM.ViewModels.PartesEdicion
{
    public partial class PartesEdicionViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly ITareasService _tareasService;

        private readonly ITiempoService _tiempoService;

        private readonly IFamiliaService _familiaService;
        private readonly ITecnicosService _tecnicosService;
        private readonly IVehiculosService _vehiculosService;

        private System.Timers.Timer aTimer;
        public System.Timers.Timer ATimer { get => aTimer; set => aTimer = value; }

        [ObservableProperty]
        private Ot? otDto;

        [ObservableProperty]
        private bool started;

        [ObservableProperty]
        private Tarea selectedTarea;

        [ObservableProperty]
        private string? horaminuto;

        [ObservableProperty]
        private double cantidad;

        [ObservableProperty]
        private ObservableCollection<Tarea> tareas;

        [ObservableProperty]
        private ObservableCollection<Familia> familias;

        [ObservableProperty]
        private ObservableCollection<Tecnico> tecnicos;

        [ObservableProperty]
        private ObservableCollection<Vehiculo> vehiculos;

        [ObservableProperty]
        private Material? selectedMaterial;

        [ObservableProperty]
        private Tecnico? selectedTecnico;

        [ObservableProperty]
        private Familia? selectedFamilia;

        [ObservableProperty]
        private MaterialImputado? selectedMaterialImputado;

        [ObservableProperty]
        private ObservableCollection<MaterialImputado> materialesImputados;

        [ObservableProperty]
        private TecnicoImputado? selectedTecnicoImputado;

        [ObservableProperty]
        private ObservableCollection<TecnicoImputado> tecnicosImputados;


        private DateTime horaInicio;

        [ObservableProperty]
        private TimeSpan inicioTec;

        [ObservableProperty]
        private TimeSpan finTec;


        public PartesEdicionViewModel(IVehiculosService vehiculosService, ITecnicosService tecnicosService, IFamiliaService familiaService, ITareasService tareasService, IMessagesService messagesService, ITiempoService tiempoService, INavigationService navigationService) : base(messagesService, navigationService)
        {
            _tiempoService = tiempoService;
            _tareasService = tareasService;
            _familiaService = familiaService;
            _tecnicosService = tecnicosService;
            _vehiculosService = vehiculosService;

            SelectedTarea = new();
            Tareas = [];
            MaterialesImputados = [];
            TecnicosImputados = [];
            ATimer = new System.Timers.Timer();

            familias = [];
            tecnicos = [];
            vehiculos = [];
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("ot", out _))
            {
                OtDto = query["ot"] as Ot;
            }

            if (query.TryGetValue("selectedMaterial", out _))
            {
                SelectedMaterial = query["selectedMaterial"] as Material;
            }
        }

        [RelayCommand]
        public async Task AddTecnico()
        {
            if (!await CheckAddTecnico())
                return;

            TecnicosImputados.Add(new TecnicoImputado()
            {
                IdTecnico = SelectedTecnico?.Id ?? 0,
                Subfamilia = SelectedFamilia?.CodigoSubfamlia ?? string.Empty,
                Nombre = SelectedTecnico?.Nombre ?? string.Empty,
                Tiempo = FinTec - InicioTec
            });
        }

        private async Task<bool> CheckAddTecnico()
        {
            if (SelectedFamilia is null)
            {
                await _messagesService.ShowCustomToast("Seleccione primero una subfamilia.");
                return false;
            }
            if (SelectedTecnico is null)
            {
                await _messagesService.ShowCustomToast("Seleccione primero un técnico.");
                return false;
            }

            if (InicioTec == TimeSpan.Zero)
            {
                await _messagesService.ShowCustomToast("Indique una hora de incio.");
                return false;
            }

            if (FinTec == TimeSpan.Zero)
            {
                await _messagesService.ShowCustomToast("Indique una hora de fin.");
                return false;
            }
            return true;
        }

        [RelayCommand]
        public async Task DelTecnico()
        {
            if (SelectedTecnicoImputado is null)
            {
                await _messagesService.ShowCustomToast("Seleccione primero un tecnico imputado");
                return;
            }
            TecnicosImputados.Remove(SelectedTecnicoImputado);
            SelectedTecnicoImputado = null;
        }

        [RelayCommand]
        public async Task AddMaterial()
        {
            if (!await CheckAddMaterial())
                return;

            MaterialesImputados.Add(new MaterialImputado()
            {
                Descripcion = SelectedMaterial?.Descripcion,
                Cantidad = Cantidad,
                Unidad = SelectedMaterial?.Unidad
            });

            if (MyEntry is not null)
                await MyEntry.HideKeyboardAsync();
        }

        private async Task<bool> CheckAddMaterial()
        {
            if (SelectedMaterial is null)
            {
                await _messagesService.ShowCustomToast("Seleccione primero un material.");
                return false;
            }

            if (Cantidad == 0)
            {
                await _messagesService.ShowCustomToast("Indique una cantidad.");
                return false;
            }
            return true;
        }

        [RelayCommand]
        public async Task DelMaterial()
        {
            if (SelectedMaterialImputado is null)
            {
                await _messagesService.ShowCustomToast("Seleccione primero un material imputado");
                return;
            }
            MaterialesImputados.Remove(SelectedMaterialImputado);
            SelectedMaterialImputado = null;
        }

        [RelayCommand]
        public void DelCantidad()
        {
            Cantidad = 0;
        }

        [RelayCommand]
        public async Task Search(string tipo)
        {
            await _navigationService.GoToAsync($"BusquedaPage?tipo={tipo}");
        }

        [RelayCommand]
        public void Start()
        {
            Started = true;
            horaInicio = DateTime.Now;
            IniciarTiempo(horaInicio);
        }

        [RelayCommand]
        public async Task GoToParte(string page)
        {
            await _navigationService.GoToAsync($"//PartesEdicion/{page}");
        }


        [RelayCommand]
        public async Task GoToIncidencias()
        {
            await _navigationService.GoToAsync($"IncidenciasPage");
        }

        private void IniciarTiempo(DateTime horaInicio)
        {
            _tiempoService.InicioTiempo(horaInicio);
            ATimer.Start();
        }

        private void SetTimer()
        {
            ATimer.Elapsed += OnTimedEvent;
        }

        public void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            TimeSpan tiempoTranscurrido = DateTime.UtcNow - horaInicio;
            Horaminuto = tiempoTranscurrido.ToString(@"hh\(\h\)\:mm\(\m\)\:ss\(\s\)");
        }

        [RelayCommand]
        public void Pause()
        {
            Started = false;
            ATimer.Stop();
        }

        private void CheckEstaEnMarcha()
        {
            Started = _tiempoService.GetTiempoEnMarcha();

            if (Started)
            {
                ATimer.Start();
            }
        }

        public override async Task OnAppearing()
        {
            ATimer = new System.Timers.Timer(5000);
            Started = false;
            Horaminuto = "00(h):00(m):00(s)";
            SetTimer();

            if (_tiempoService.ExistsTiempo())
            {
                var data = _tiempoService.GetHoraInicio();

                if (data != null)
                {
                    horaInicio = (DateTime)data;
                    CheckEstaEnMarcha();
                }
            }

            Tareas = new(await _tareasService.GetTareasAsync());
            Familias = new(await _familiaService.GetFamiliasAsync());
            Tecnicos = new(await _tecnicosService.GetTecnicosAsync());
            Vehiculos = new(await _vehiculosService.GetVehiculosAsync());

            await base.OnAppearing();
        }

        public override async Task OnDisappearing()
        {
            ATimer.Dispose();
            await base.OnDisappearing();
        }
    }
}
