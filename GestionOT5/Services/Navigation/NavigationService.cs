
namespace GestionOT5.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        public async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }

        public void FlyoutIsPresented(bool v)
        {
            Shell.Current.FlyoutIsPresented = v;
        }
        public async Task GoToAsync(string route)
        {
            await Shell.Current.GoToAsync(route);
        }

        public async Task GoToAsyncParameter(string route, Dictionary<string, object> navigationParameter)
        {
            await Shell.Current.GoToAsync(route, navigationParameter);
        }
    }
}
