using AutoMapper;
using FluentAssertions;
using GestionOT5.Dto;
using GestionOT5.Services.Ots;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;

namespace GestionOT5.Test.Services.Ots
{
    public class OtBLServiceTest
    {
        private List<OtDTOfinal> itemList;

        public OtBLServiceTest()
        {
            Seed();
        }

        private void Seed()
        {
            itemList =
            [
                new()
                    {
                        Numero = 32,
                        Serie = "P",
                        Tipo = "PARTE SERVICIOS",
                        CodigoTipo = "5",
                        Cliente = "CP PLAZA KOLITZA, 1",
                        Direccion = "PLAZA KOLITXA, 1",
                        Fecha = new DateTime(2016, 6, 30, 15, 00, 00),
                        Estado = "INICIADA"
                    },
                    new()
                    {
                        Numero = 34,
                        Serie = "P",
                        Tipo = "PARTE OBRA",
                        CodigoTipo = "5",
                        Cliente = "CP SENDEJA, 3 - BILBAO",
                        Direccion = "C/ SENDEJA, 3",
                        Fecha = new DateTime(2016, 6, 30, 18, 30, 00),
                        Estado = "PENDIENTE"
                    },
                    new()
                    {
                        Numero = 35,
                        Serie = "P",
                        Tipo = "PARTE CCTV",
                        CodigoTipo = "5",
                        Cliente = "CP URETAMENDI 49 A 71",
                        Direccion = "C/ URETAMENDI, 49",
                        Fecha = new DateTime(2016, 6, 30, 21, 00, 00),
                        Estado = "PENDIENTE"
                    }
            ];

        }

        [Fact]
        public async Task GetOtsAsync()
        {
            //Arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(itemList)),
            };

            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
              .Protected()
              .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
              .ReturnsAsync(response);

            var httpClient = new HttpClient(handlerMock.Object);

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            OtBLService otBLService = new OtBLService(mapper);
            otBLService._client = httpClient;


            //Act
            var result = await otBLService.GetOtsAsync();

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetOtsAsyncWhenIsNull()
        {
            //Arrange
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            OtBLService otBLService = new OtBLService(mapper);
            otBLService._client = null;

            //Act
            Func<Task> action = async () => { await otBLService.GetOtsAsync(); };

            //Assert
            var ex = await Assert.ThrowsAsync<Exception>(action);
            Assert.Contains("Object reference not set to an instance of an object.", ex.Message);
        }
    }
}
