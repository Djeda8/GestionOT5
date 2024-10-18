using FluentAssertions;
using GestionOT5.MVVM.Models;
using GestionOT5.MVVM.ViewModels.Incidencias;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using Moq;

namespace GestionOT5.Test.MVVM.Incidencias
{
    public class ShowIncidenciaViewModelTest
    {

        [Fact]
        public void ApplyQueryAttributes()
        {
            //Arrange
            var messagesService = new Mock<IMessagesService>();
            var navigationService = new Mock<INavigationService>();

            ShowIncidenciaViewModel showIncidenciaViewModel = new(messagesService.Object, navigationService.Object);

            Dictionary<string, object> dictionay = new Dictionary<string, object>();
            dictionay.Add("incidencia", new Incidencia() { Descripcion = "Test" });

            //Act
            showIncidenciaViewModel.ApplyQueryAttributes(dictionay);

            //Assert
            showIncidenciaViewModel.ItemIncidencia.Descripcion.Should().Be("Test");
        }
    }
}
