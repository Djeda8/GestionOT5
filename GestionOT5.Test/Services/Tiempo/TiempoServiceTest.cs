using FluentAssertions;
using GestionOT5.MVVM.Models;
using GestionOT5.Services.Preferen;
using GestionOT5.Services.Tiempo;
using Moq;

namespace GestionOT5.Test.Services.Tiempo
{
    public class TiempoServiceTest
    {
        [Fact]
        public void InicioTiempo()
        {
            //Arrange
            var preferencesService = new Mock<IPreferencesService>();

            TiempoService tiempoService = new(preferencesService.Object);
            var time = DateTime.Now;
            TiempoInvertido tiempo = new TiempoInvertido() { HoraInicio = time, TiempoEnMarcha = true };
            string tiempoDetailStr = Newtonsoft.Json.JsonConvert.SerializeObject(tiempo);

            //Act
            tiempoService.InicioTiempo(time);

            //Assert
            preferencesService.Verify(preferencesService => preferencesService.SetValue(nameof(App.TiempoDetails), tiempoDetailStr), Times.Once);
        }

        [Fact]
        public void GetHoraInicio()
        {
            //Arrange
            var preferencesService = new Mock<IPreferencesService>();

            var time = DateTime.Now;
            TiempoInvertido tiempo = new TiempoInvertido() { HoraInicio = time, TiempoEnMarcha = true };
            string tiempoDetailStr = Newtonsoft.Json.JsonConvert.SerializeObject(tiempo);

            preferencesService.Setup(preferencesService => preferencesService.GetValue(nameof(App.TiempoDetails), "")).Returns(tiempoDetailStr);

            TiempoService tiempoService = new(preferencesService.Object);


            //Act
            var result = tiempoService.GetHoraInicio();

            //Assert
            result.Should().Be(time);
        }

        [Fact]
        public void GetHoraInicioWhenIsEmpty()
        {
            //Arrange
            var preferencesService = new Mock<IPreferencesService>();

            preferencesService.Setup(preferencesService => preferencesService.GetValue(nameof(App.TiempoDetails), "")).Returns("");

            TiempoService tiempoService = new(preferencesService.Object);


            //Act
            var result = tiempoService.GetHoraInicio();

            //Assert
            result.Should().Be(null);
        }

        [Fact]
        public void ExistsTiempo()
        {
            //Arrange
            var preferencesService = new Mock<IPreferencesService>();

            preferencesService.Setup(preferencesService => preferencesService.ContainsKey(nameof(App.TiempoDetails))).Returns(true);

            TiempoService tiempoService = new(preferencesService.Object);

            //Act
            var result = tiempoService.ExistsTiempo();

            //Assert
            result.Should().Be(true);
        }

        [Fact]
        public void GetTiempoEnMarchaIsTrue()
        {
            //Arrange
            var preferencesService = new Mock<IPreferencesService>();

            var time = DateTime.Now;
            TiempoInvertido tiempo = new TiempoInvertido() { HoraInicio = time, TiempoEnMarcha = true };
            string tiempoDetailStr = Newtonsoft.Json.JsonConvert.SerializeObject(tiempo);

            preferencesService.Setup(preferencesService => preferencesService.GetValue(nameof(App.TiempoDetails), "")).Returns(tiempoDetailStr);

            TiempoService tiempoService = new(preferencesService.Object);


            //Act
            var result = tiempoService.GetTiempoEnMarcha();

            //Assert
            result.Should().Be(true);
        }

        [Fact]
        public void GetTiempoEnMarchaIsFalseWhenTiempoIsNull()
        {
            //Arrange
            var preferencesService = new Mock<IPreferencesService>();

            preferencesService.Setup(preferencesService => preferencesService.GetValue(nameof(App.TiempoDetails), "")).Returns("");

            TiempoService tiempoService = new(preferencesService.Object);

            //Act
            var result = tiempoService.GetTiempoEnMarcha();

            //Assert
            result.Should().Be(false);
        }

        [Fact]
        public void GetTiempoEnMarchaIsFalse()
        {
            //Arrange
            var preferencesService = new Mock<IPreferencesService>();

            var time = DateTime.Now;
            TiempoInvertido tiempo = new TiempoInvertido() { HoraInicio = time, TiempoEnMarcha = false };
            string tiempoDetailStr = Newtonsoft.Json.JsonConvert.SerializeObject(tiempo);

            preferencesService.Setup(preferencesService => preferencesService.GetValue(nameof(App.TiempoDetails), "")).Returns(tiempoDetailStr);

            TiempoService tiempoService = new(preferencesService.Object);


            //Act
            var result = tiempoService.GetTiempoEnMarcha();

            //Assert
            result.Should().Be(false);
        }
    }
}
