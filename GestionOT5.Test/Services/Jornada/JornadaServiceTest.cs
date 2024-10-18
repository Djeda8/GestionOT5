using FluentAssertions;
using GestionOT5.Services.Jornada;
using GestionOT5.Services.Preferen;
using Moq;

namespace GestionOT5.Test.Services.Jornada
{
    public class JornadaServiceTest
    {
        [Fact]
        public void GetJornadaShouldReturn()
        {
            //Arrange
            var preferencesService = new Mock<IPreferencesService>();

            var jornadaDetailsStr = Newtonsoft.Json.JsonConvert.SerializeObject(new GestionOT5.MVVM.Models.Jornada() { });

            preferencesService.Setup(preferencesService => preferencesService.GetValue(nameof(App.JornadaDetails), "")).Returns(jornadaDetailsStr);

            JornadaService jornadaService = new(preferencesService.Object);

            //Act
            var result = jornadaService.GetJornada();

            //Assert
            result.Should().BeOfType<GestionOT5.MVVM.Models.Jornada>();
        }

        [Fact]
        public void MessageJornadaWhenIsToday()
        {
            //Arrange
            var preferencesService = new Mock<IPreferencesService>();

            var time = DateTime.Now;
            var jornada = new GestionOT5.MVVM.Models.Jornada() { JornadaInicio = time, UserName = "Daniel" };
            var jornadaDetailsStr = Newtonsoft.Json.JsonConvert.SerializeObject(jornada);

            preferencesService.Setup(preferencesService => preferencesService.GetValue(nameof(App.JornadaDetails), "")).Returns(jornadaDetailsStr);
            preferencesService.Setup(preferencesService => preferencesService.ContainsKey(nameof(App.JornadaDetails))).Returns(true);

            JornadaService jornadaService = new(preferencesService.Object);

            //Act
            var result = jornadaService.MessageJornada();

            //Assert
            result.Should().Be($"Jornada iniciada por {jornada?.UserName} hoy {jornada?.JornadaInicio.ToString(@"dd \d\e MMMM")}.");
        }    
        
        [Fact]
        public void MessageJornadaWhenIsNotToday()
        {
            //Arrange
            var preferencesService = new Mock<IPreferencesService>();

            var time = DateTime.Now.AddDays(1);
            var jornada = new GestionOT5.MVVM.Models.Jornada() { JornadaInicio = time, UserName = "Daniel" };
            var jornadaDetailsStr = Newtonsoft.Json.JsonConvert.SerializeObject(jornada);

            preferencesService.Setup(preferencesService => preferencesService.GetValue(nameof(App.JornadaDetails), "")).Returns(jornadaDetailsStr);
            preferencesService.Setup(preferencesService => preferencesService.ContainsKey(nameof(App.JornadaDetails))).Returns(true);

            JornadaService jornadaService = new(preferencesService.Object);

            //Act
            var result = jornadaService.MessageJornada();

            //Assert
            result.Should().Be($"Jornada iniciada por {jornada?.UserName} el {jornada?.JornadaInicio.ToString(@"dd \d\e MMMM")}.");
        }
        
        [Fact]
        public void MessageJornadaWhenNotExistJJornada()
        {
            //Arrange
            var preferencesService = new Mock<IPreferencesService>();

            preferencesService.Setup(preferencesService => preferencesService.ContainsKey(nameof(App.JornadaDetails))).Returns(false);

            JornadaService jornadaService = new(preferencesService.Object);

            //Act
            var result = jornadaService.MessageJornada();

            //Assert
            result.Should().Be($"Jornada sin inciar");
        }

        [Fact]
        public void CreateJornada()
        {
            //Arrange
            var preferencesService = new Mock<IPreferencesService>();

            JornadaService jornadaService = new(preferencesService.Object);
            var time = DateTime.Now;

            GestionOT5.MVVM.Models.Jornada jornada = new GestionOT5.MVVM.Models.Jornada() { UserName = App.UserDetails?.UserName, JornadaInicio = time };
            string jornadaDetailStr = Newtonsoft.Json.JsonConvert.SerializeObject(jornada);

            //Act
            jornadaService.CreateJornada(time);

            //Assert
            preferencesService.Verify(preferencesService => preferencesService.SetValue(nameof(App.JornadaDetails), jornadaDetailStr), Times.Once);
            //preferencesService.Verify(preferencesService => preferencesService.SetValue(nameof(App.JornadaDetails), It.IsAny<String>()), Times.Once);
        }

        [Fact]
        public void DeleteJornada()
        {
            //Arrange
            var preferencesService = new Mock<IPreferencesService>();

            JornadaService jornadaService = new(preferencesService.Object);

            //Act
            jornadaService.DeleteJornada();

            //Assert
            preferencesService.Verify(preferencesService => preferencesService.RemoveKey(nameof(App.JornadaDetails)), Times.Once);
        }

    }
}
