using GestionOT5.Services.Jornada;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using Moq;

namespace GestionOT5.Test.MVVM.PartesPendiente
{
    public class PartesPendienteViewModelFixture
    {
        public PartesPendienteViewModelFixture()
        {
            this.JornadaService = new Mock<IJornadaService>();
            this.MessagesService = new Mock<IMessagesService>();
            this.NavigationService = new Mock<INavigationService>();
        }
        public Mock<IJornadaService> JornadaService { get; }
        public Mock<IMessagesService> MessagesService { get; }
        public Mock<INavigationService> NavigationService { get; }
    }
}
