using FluentAssertions;
using GestionOT5.MVVM.Models;
using GestionOT5.Services.Vehiculos;

namespace GestionOT5.Test.Services.Vehiculos
{
    public class VehiculosServiceMockTest
    {
        [Fact]
        public async Task GetVehiculosAsyncReturnTareasList()
        {
            //Arrange
            VehiculosServiceMock tareasService = new();

            //Act
            IEnumerable<Vehiculo> result = await tareasService.GetVehiculosAsync();

            //Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(8);
        }
    }
}
