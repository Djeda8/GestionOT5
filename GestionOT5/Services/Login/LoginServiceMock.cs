using GestionOT5.MVVM.Models;

namespace GestionOT5.Services.Login
{
    public class LoginServiceMock : ILoginService
    {
        public async Task<ResponseLogin> CheckLoginAsync(string? user, string? password)
        {
            await Task.Delay(1000);


            switch (password)
            {
                case "12345":
                    return
                       new ResponseLogin()
                       {
                           LoginOk = true,
                           Message = $"Bienvenido de nuevo {user}",
                           User = new()
                           {
                               UserName = user,
                               Email = "daojub@hotmail.com"
                           }
                       };
                default:
                    return
                        new ResponseLogin()
                        {
                            LoginOk = false,
                            Message = "Usuario o contraseña no correctos",
                        };
            }
        }
    }
}
