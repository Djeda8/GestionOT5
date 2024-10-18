using FluentAssertions;
using GestionOT5.MVVM.Models;
using GestionOT5.Services.Tareas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOT5.Test.Services.Tareas
{
    public class TareasServiceTest
    {
        [Fact]
        public async Task GetTareasAsyncReturnTareasList()
        {
            //Arrange
            TareasServiceMock tareasService = new();

            //Act
            IEnumerable<Tarea> result = await tareasService.GetTareasAsync();

            //Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(8);
        }
    }
}
