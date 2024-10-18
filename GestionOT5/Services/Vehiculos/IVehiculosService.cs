using GestionOT5.MVVM.Models;

namespace GestionOT5.Services.Vehiculos
{
    public interface IVehiculosService
    {
        Task<IEnumerable<Vehiculo>> GetVehiculosAsync();
    }
}