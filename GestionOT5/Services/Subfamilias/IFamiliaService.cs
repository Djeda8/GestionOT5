using GestionOT5.MVVM.Models;

namespace GestionOT5.Services.Subfamilias
{
    public interface IFamiliaService
    {
        Task<IEnumerable<Familia>> GetFamiliasAsync();
    }
}