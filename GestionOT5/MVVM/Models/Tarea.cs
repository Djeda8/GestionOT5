using CommunityToolkit.Mvvm.ComponentModel;

namespace GestionOT5.MVVM.Models
{
    public partial class Tarea : ObservableObject
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }

        [ObservableProperty]
        private bool done;
    }
}
