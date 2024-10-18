using GestionOT5.MVVM.Models;
using GestionOT5.Services.Preferen;

namespace GestionOT5.Services.Tiempo
{
    public class TiempoService : ITiempoService
    {
        private readonly IPreferencesService _preferencesService;

        public TiempoService(IPreferencesService preferencesService)
        {
            _preferencesService = preferencesService;
        }

        public DateTime? GetHoraInicio()
        {
            string tiempo = _preferencesService.GetValue(nameof(App.TiempoDetails), "");

            return Newtonsoft.Json.JsonConvert.DeserializeObject<TiempoInvertido>(tiempo)?.HoraInicio ?? null;

        }
        public void InicioTiempo(DateTime horaInicio)
        {
            TiempoInvertido tiempo = new TiempoInvertido() { HoraInicio = horaInicio, TiempoEnMarcha = true };
            string tiempoDetailStr = Newtonsoft.Json.JsonConvert.SerializeObject(tiempo);
            _preferencesService.SetValue(nameof(App.TiempoDetails), tiempoDetailStr);
        }

        public bool ExistsTiempo()
        {
            return _preferencesService.ContainsKey(nameof(App.TiempoDetails));
        }

        public bool GetTiempoEnMarcha()
        {
            string tiempo = _preferencesService.GetValue(nameof(App.TiempoDetails), "");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TiempoInvertido>(tiempo)?.TiempoEnMarcha ?? false;
        }
    }
}
