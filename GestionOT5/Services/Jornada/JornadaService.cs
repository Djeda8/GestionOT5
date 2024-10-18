using GestionOT5.Services.Preferen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOT5.Services.Jornada
{
    public class JornadaService : IJornadaService
    {
        private readonly IPreferencesService _preferencesService;

        public JornadaService(IPreferencesService preferencesService)
        {
            _preferencesService = preferencesService;
        }

        public string MessageJornada()
        {
            if (ExistsJornada())
            {
                var jornadaInfo = GetJornada();
                App.JornadaDetails = jornadaInfo;

                var esHoy = "";
                if (jornadaInfo?.JornadaInicio.Date == DateTime.Today)
                {
                    esHoy = "hoy";
                }
                else
                {
                    esHoy = "el";
                }
                return $"Jornada iniciada por {jornadaInfo?.UserName} {esHoy} {jornadaInfo?.JornadaInicio.ToString(@"dd \d\e MMMM")}.";
            }
            else
            {
                return "Jornada sin inciar";
            }
        }

        public void CreateJornada(DateTime time)
        {
            MVVM.Models.Jornada jornada = new MVVM.Models.Jornada() { UserName = App.UserDetails?.UserName, JornadaInicio = time };
            string jornadaDetailStr = Newtonsoft.Json.JsonConvert.SerializeObject(jornada);
            _preferencesService.SetValue(nameof(App.JornadaDetails), jornadaDetailStr);
        }
        public void DeleteJornada()
        {
            _preferencesService.RemoveKey(nameof(App.JornadaDetails));
        }

        public bool ExistsJornada()
        {
            return _preferencesService.ContainsKey(nameof(App.JornadaDetails));
        }

        public MVVM.Models.Jornada? GetJornada()
        {
            string jornadaDetailsStr = _preferencesService.GetValue(nameof(App.JornadaDetails), "");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<MVVM.Models.Jornada>(jornadaDetailsStr);
        }
    }
}
