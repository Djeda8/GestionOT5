using GestionOT5.Services.Fotos;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOT5.Test.MVVM.Incidencias
{
    public class FotoViewModelFixture
    {
        public FotoViewModelFixture()
        {
            MediaPickerService = new Mock<IMediaPickerService>();
            this.MessagesService = new Mock<IMessagesService>();
            this.NavigationService = new Mock<INavigationService>();
        }

        public Mock<IMediaPickerService> MediaPickerService { get; }
        public Mock<IMessagesService> MessagesService { get; }
        public Mock<INavigationService> NavigationService { get; }
    }
}
