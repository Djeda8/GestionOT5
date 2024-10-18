using FluentAssertions;
using GestionOT5.MVVM.Models;
using GestionOT5.Services.Tecnicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOT5.Test.Services.Tecnicos
{
    public class TecnicosServiceMockTest
    {
        [Fact]
        public async Task GetSubfamiliasAsyncReturnTareasList()
        {
            //Arrange
            TecnicosServiceMock tareasService = new();

            //Act
            IEnumerable<Tecnico> result = await tareasService.GetTecnicosAsync();

            //Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(8);
        }
    }
}
