using GestionOT5.MVVM.ViewModels.AppShell;
using GestionOT5.Services.Navigation;
using GestionOT5.Services.Preferen;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOT5.Test.MVVM.AppShell
{
    public class AppShellViewModelTest
    {
        [Fact]
        public async Task LogoutContainsKey()
        {
            //Arrange
            var preferencesService = new Mock<IPreferencesService>();
            preferencesService.Setup(service => service.ContainsKey(It.IsAny<string>())).Returns(true);

            var navigationService = new Mock<INavigationService>();

            AppShellViewModel appShellViewModel = new(preferencesService.Object,navigationService.Object);
            
            //Act
            await appShellViewModel.LogoutCommand.ExecuteAsync(null);

            //Assert
            navigationService.Verify(servicio => servicio.GoToAsync($"//Login"), Times.Once);
        }    
        
        [Fact]
        public async Task LogoutNotContainsKey()
        {
            //Arrange
            var preferencesService = new Mock<IPreferencesService>();
            preferencesService.Setup(service => service.ContainsKey(It.IsAny<string>())).Returns(false);

            var navigationService = new Mock<INavigationService>();

            AppShellViewModel appShellViewModel = new(preferencesService.Object,navigationService.Object);
            
            //Act
            await appShellViewModel.LogoutCommand.ExecuteAsync(null);

            //Assert
            navigationService.Verify(servicio => servicio.GoToAsync($"//Login"), Times.Once);
        }
    }
}
