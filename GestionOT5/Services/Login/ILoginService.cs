using GestionOT5.MVVM.Models;

namespace GestionOT5.Services.Login
{
    public interface ILoginService
    {
        Task<ResponseLogin> CheckLoginAsync(string? user, string? password);
    }
}