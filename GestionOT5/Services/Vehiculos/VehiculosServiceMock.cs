using GestionOT5.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOT5.Services.Vehiculos
{
    public class VehiculosServiceMock : IVehiculosService
    {
        public async Task<IEnumerable<Vehiculo>> GetVehiculosAsync()
        {
            await Task.Delay(1000);

            return
            [
                 new()
                 {
                     Id = 1,
                     Matricula = "Matricula 1",
                 },
                 new()
                 {
                     Id = 2,
                     Matricula = "Matricula 2",
                 },
                 new()
                 {
                     Id = 3,
                     Matricula = "Matricula 3",
                 },
                 new()
                 {
                     Id = 4,
                     Matricula = "Matricula 4",
                 },
                 new()
                 {
                     Id = 5,
                     Matricula = "Matricula 5",
                 },
                 new()
                 {
                     Id = 6,
                     Matricula = "Matricula 6",
                 },
                 new()
                 {
                     Id = 7,
                     Matricula = "Matricula 7",
                 },
                 new()
                 {
                     Id = 8,
                     Matricula = "Matricula 8",
                 },
            ];
        }
    }
}
