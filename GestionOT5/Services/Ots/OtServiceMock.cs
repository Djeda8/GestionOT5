using GestionOT5.MVVM.Models;

namespace GestionOT5.Services.Ots
{
    public class OtServiceMock : IOtService
    {
        public async Task<IEnumerable<MVVM.Models.Ot>> GetOtsAsync()
        {
            await Task.Delay(1000);

            return new List<MVVM.Models.Ot>()
            {
                new()
                    {
                        Id =1,
                        Numero = 32,
                        Serie = "P",
                        TipoParte = "P",
                        Tipo = "PARTE SERVICIOS",
                        CodigoTipo = "5",
                        Cliente = "CP PLAZA KOLITZA, 1",
                        Direccion = "PLAZA KOLITXA, 1",
                        Fecha = new DateTime(2016, 6, 30, 15, 00, 00),
                        Estado = "INICIADA"
                    },
                    new()
                    {
                        Id = 2,
                        Numero = 34,
                        Serie = "P",
                        TipoParte = "P",
                        Tipo = "PARTE OBRA",
                        CodigoTipo = "5",
                        Cliente = "CP SENDEJA, 3 - BILBAO",
                        Direccion = "C/ SENDEJA, 3",
                        Fecha = new DateTime(2016, 6, 30, 18, 30, 00),
                        Estado = "PENDIENTE"
                    },
                    new()
                    {
                        Id = 3,
                        Numero = 35,
                        Serie = "P",
                        TipoParte = "P",
                        Tipo = "PARTE CCTV",
                        CodigoTipo = "5",
                        Cliente = "CP URETAMENDI 49 A 71",
                        Direccion = "C/ URETAMENDI, 49",
                        Fecha = new DateTime(2016, 6, 30, 21, 00, 00),
                        Estado = "PENDIENTE"
                    }
            };
        }
    }
}
