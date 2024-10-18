using FluentAssertions;
using GestionOT5.MVVM.Models;
using GestionOT5.Services.Subfamilias;

namespace GestionOT5.Test.Services.Subfamilias
{
    public class FamiliaServiceMockTest
    {
        [Fact]
        public async Task GetSubfamiliasAsyncReturnTareasList()
        {
            //Arrange
            FamiliaServiceMock tareasService = new();

            //Act
            IEnumerable<Familia> result = await tareasService.GetFamiliasAsync();

            //Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(8);
        }
    }
}
