using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOT5.MVVM.Models
{
    public partial class Incidencia : ObservableObject
    {
        [ObservableProperty]
        private string descripcion;
        public bool EsInterna { get; set; }
        public IEnumerable<ItemPhoto> PhotosList { get; set; }
    }
}
