using FluentAssertions;
using GestionOT5.MVVM.Models;
using GestionOT5.Services.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOT5.Test.Services.Login
{
    public class LoginServiceMockTest
    {
        [Fact]
        public async Task CheckLoginAsyncOk()
        {
            //Arrange
            LoginServiceMock loginService = new();
            var user = "Daniel";
            //Act
            ResponseLogin result = await loginService.CheckLoginAsync(user, "12345");

            //Assert
            result.Message.Should().Be($"Bienvenido de nuevo {user}");
        }

        [Fact]
        public async Task CheckLoginAsyncNoOk()
        {
            //Arrange
            LoginServiceMock loginService = new();
            var user = "Daniel";
            //Act
            ResponseLogin result = await loginService.CheckLoginAsync(user, "123456");

            //Assert
            result.Message.Should().Be($"Usuario o contraseña no correctos");
        }
    }
}
