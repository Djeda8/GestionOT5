
namespace GestionOT5.Services.Geocoding
{
    public interface IGeocodingService
    {
        Task<IEnumerable<Location>> GetLocationsAsync(string address);
    }
}