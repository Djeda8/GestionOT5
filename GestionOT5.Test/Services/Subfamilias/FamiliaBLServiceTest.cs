using AutoMapper;
using FluentAssertions;
using GestionOT5.Dto;
using GestionOT5.Services.Subfamilias;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GestionOT5.Test.Services.Subfamilias
{

    public class FamiliaBLServiceTest
    {
        private List<FamiliaDto> itemList;
        public FamiliaBLServiceTest()
        {
            Seed();
        }

        private void Seed()
        {
            itemList =
            [
               new()
                    {
                        TipoParte ="1",
                        CodigoFamilia = "HORAS",
                        CodigoSubfamlia = "ACHIQUES",
                        Borrado = false
                    },
                    new()
                    {
                        TipoParte = "1",
                        CodigoFamilia = "HORAS",
                        CodigoSubfamlia = "NACEXTRA",
                        Borrado = false
                    },
                    new()
                    {
                        TipoParte = "1",
                        CodigoFamilia = "HORAS",
                        CodigoSubfamlia = "NAVIDAD",
                        Borrado = false
                    }
            ];

        }

        [Fact]
        public async Task GetFamiliasAsync()
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

            FamiliaBLService fammiliaBLService = new FamiliaBLService(mapper);
            fammiliaBLService._client = httpClient;


            //Act
            var result = await fammiliaBLService.GetFamiliasAsync();

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

            FamiliaBLService otBLService = new FamiliaBLService(mapper);
            otBLService._client = null;

            //Act
            Func<Task> action = async () => { await otBLService.GetFamiliasAsync(); };

            //Assert
            var ex = await Assert.ThrowsAsync<Exception>(action);
            Assert.Contains("Object reference not set to an instance of an object.", ex.Message);
        }
    }
}
