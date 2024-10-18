namespace GestionOT5.Services.Geocoding
{
    public class GeocodingService : IGeocodingService
    {
        public async Task<IEnumerable<Location>> GetLocationsAsync(string address)
        {
            return await Microsoft.Maui.Devices.Sensors.Geocoding.Default.GetLocationsAsync(address);
        }
    }
}
