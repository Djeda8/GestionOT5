using GestionOT5.MVVM.Models;

namespace GestionOT5.Services.Ots
{
    public interface IOtService
    {
        Task<IEnumerable<MVVM.Models.Ot>> GetOtsAsync();
    }
}
