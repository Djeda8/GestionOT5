using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOT5.MVVM.Models
{
    public class Jornada
    {
        public string? UserName { get; set; }
        public DateTime JornadaInicio { get; set; }
        public DateTime JornadaFin { get; set; }
    }
}
