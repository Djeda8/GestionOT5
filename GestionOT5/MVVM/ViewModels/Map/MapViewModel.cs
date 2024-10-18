using CommunityToolkit.Mvvm.ComponentModel;
using GestionOT5.MVVM.Models;
using GestionOT5.Services.Geocoding;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace GestionOT5.MVVM.ViewModels.Map
{
    public partial class MapViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IGeocodingService _geocodingService;

        [ObservableProperty]
        private Ot otDto;
        private Location? location;

        public MapViewModel(IGeocodingService geocodingService, IMessagesService messagesService, INavigationService navigationService) : base(messagesService, navigationService)
        {
            _geocodingService = geocodingService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("ot"))
            {
                OtDto = query["ot"] as Ot;
            }
        }

        public override async Task OnAppearing()
        {
            await GetLocation();
            AddPin();
            MoveToRegion();

            await base.OnAppearing();
        }

        private void MoveToRegion()
        {
            if (location is null)
                return;

            MapSpan mapSpan = new MapSpan(location, 0.01, 0.01);
            MyMap.MoveToRegion(mapSpan);
        }

        private async Task GetLocation()
        {
            string address = OtDto.Direccion;

            IEnumerable<Location> locations = await _geocodingService.GetLocationsAsync(address);
            location = locations?.FirstOrDefault();
        }

        private void AddPin()
        {
            Pin pin = new()
            {
                Label = OtDto.Cliente,
                Address = OtDto.Direccion,
                Type = PinType.Place,
                Location = new Location(location.Latitude, location.Longitude)
            };
            MyMap.Pins.Add(pin);
        }
    }
}
