using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOT5.MVVM.Models
{
    public partial class ItemPhoto : ObservableObject
    {
        public string Filename { get; set; }

        [ObservableProperty]
        private ImageSource source;
        public string JPGDataBase64 { get; set; }
        public Stream Str { get; set; }
    }
}
