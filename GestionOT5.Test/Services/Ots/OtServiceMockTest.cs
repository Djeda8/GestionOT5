using FluentAssertions;
using GestionOT5.MVVM.Models;
using GestionOT5.Services.Ots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOT5.Test.Services.Ot
{
    public class OtServiceMockTest
    {
        [Fact]
        public async Task GetOtsAsyncShouldReturn()
        {
            //Arrange
            OtServiceMock otServiceMock = new();

            //Act
            IEnumerable<GestionOT5.MVVM.Models.Ot> result = await otServiceMock.GetOtsAsync();

            //Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(3);
        }
    }
}
