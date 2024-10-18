

namespace GestionOT5.Services.Navigation
{
    public interface INavigationService
    {
        Task Back();
        void FlyoutIsPresented(bool v);
        Task GoToAsync(string route);
        Task GoToAsyncParameter(string route, Dictionary<string, object> navigationParameter);
    }
}