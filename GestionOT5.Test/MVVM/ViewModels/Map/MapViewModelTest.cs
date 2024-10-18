using FluentAssertions;
using GestionOT5.MVVM.Models;
using GestionOT5.MVVM.ViewModels.Map;
using GestionOT5.Services.Geocoding;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOT5.Test.MVVM.ViewModels.Map
{
    public class MapViewModelTest
    {
        [Fact]
        public void ApplyQueryAttributes()
        {
            //Arrange
            var geocodingService = new Mock<IGeocodingService>();
            var messagesService = new Mock<IMessagesService>();
            var navigationService = new Mock<INavigationService>();

            MapViewModel mapViewModel = new(geocodingService.Object, messagesService.Object, navigationService.Object);

            Dictionary<string, object> dictionay = new Dictionary<string, object>();
            dictionay.Add("ot", new Ot() { Cliente = "Test" });

            //Act
            mapViewModel.ApplyQueryAttributes(dictionay);

            //Assert
            mapViewModel.OtDto.Cliente.Should().Be("Test");
        }

        [Fact]
        public async Task OnAppearing()
        {
            //Arrange
            var geocodingService = new Mock<IGeocodingService>();
            var messagesService = new Mock<IMessagesService>();
            var navigationService = new Mock<INavigationService>();

            IEnumerable<Location> locations = new List<Location>()
            {
                new Location()
                {
                     Latitude = 1,
                     Longitude = 2,
                }
            };

            geocodingService.Setup(service => service.GetLocationsAsync(It.IsAny<string>())).ReturnsAsync(locations);

            MapViewModel mapViewModel = new(geocodingService.Object, messagesService.Object, navigationService.Object);
            mapViewModel.OtDto = new Ot() { Cliente = "Test", Direccion = "115 Chessel Street Bristol" };
            mapViewModel.MyMap = new();

            //Act
            await mapViewModel.OnAppearing();

            //Assert
            mapViewModel.OtDto?.Cliente.Should().Be("Test");
            mapViewModel.MyMap.Pins.Should().HaveCount(1);
        }
    }
}
