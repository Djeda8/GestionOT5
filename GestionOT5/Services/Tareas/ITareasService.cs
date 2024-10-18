using GestionOT5.MVVM.Models;

namespace GestionOT5.Services.Tareas
{
    public interface ITareasService
    {
        Task<IEnumerable<Tarea>> GetTareasAsync();
    }
}