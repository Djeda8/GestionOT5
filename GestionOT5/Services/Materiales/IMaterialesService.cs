using GestionOT5.MVVM.Models;

namespace GestionOT5.Services.Materiales
{
    public interface IMaterialesService
    {
        Task<IEnumerable<Material>> GetMaterialesAsync();
    }
}