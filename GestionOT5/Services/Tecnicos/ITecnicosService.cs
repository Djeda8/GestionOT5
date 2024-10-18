using GestionOT5.MVVM.Models;

namespace GestionOT5.Services.Tecnicos
{
    public interface ITecnicosService
    {
        Task<IEnumerable<Tecnico>> GetTecnicosAsync();
    }
}