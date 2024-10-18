using System.ComponentModel.DataAnnotations;

namespace GestionOT5.MVVM.Models
{
    public class Ot
    {
        public int Id { get; set; }
        public string? Estado { get; set; }
        public string? CodigoTipo { get; set; }
        public string? TipoParte { get; set; }
        public int Ejercicio { get; set; }
        public string? Serie { get; set; }
        public int Numero { get; set; }
        public string? Tipo { get; set; }
        public string? Cliente { get; set; }
        public string? Direccion { get; set; }
        public DateTime Fecha { get; set; }
        public string OtKey { get => $"{Numero} {Serie} {Ejercicio}"; }
    }
}
