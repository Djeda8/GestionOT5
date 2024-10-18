using FluentAssertions;
using GestionOT5.MVVM.Models;
using GestionOT5.Services.Materiales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOT5.Test.Services.Materiales
{
    public class MaterialesServiceMockTest
    {
        [Fact]
        public async Task GetMaterialesAsyncReturnTareasList()
        {
            //Arrange
            MaterialesServiceMock materialesService = new();

            //Act
            IEnumerable<Material> result = await materialesService.GetMaterialesAsync();

            //Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(8);
        }
    }
}
