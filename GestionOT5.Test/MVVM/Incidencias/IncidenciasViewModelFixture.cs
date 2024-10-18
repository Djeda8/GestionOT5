using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using Moq;

namespace GestionOT5.Test.MVVM.Incidencias
{
    public class IncidenciasViewModelFixture : IDisposable
    {
        public IncidenciasViewModelFixture()
        {
            this.MessagesService = new Mock<IMessagesService>();
            this.NavigationService = new Mock<INavigationService>();
        }
        public Mock<IMessagesService> MessagesService { get; }
        public Mock<INavigationService> NavigationService { get; }

        public void Dispose()
        {
            
        }
    }
}
