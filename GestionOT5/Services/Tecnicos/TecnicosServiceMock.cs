using GestionOT5.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOT5.Services.Tecnicos
{
    public class TecnicosServiceMock : ITecnicosService
    {
        public async Task<IEnumerable<Tecnico>> GetTecnicosAsync()
        {
            await Task.Delay(1000);

            return
            [
                 new()
                 {
                     Id = 1,
                     Nombre = "Tecnico 1",
                 },
                 new()
                 {
                     Id = 2,
                     Nombre = "Tecnico 2",
                 },
                 new()
                 {
                     Id = 3,
                     Nombre = "Tecnico 3",
                 },
                 new()
                 {
                     Id = 4,
                     Nombre = "Tecnico 4",
                 },
                 new()
                 {
                     Id = 5,
                     Nombre = "Tecnico 5",
                 },
                 new()
                 {
                     Id = 6,
                     Nombre = "Tecnico 6",
                 },
                 new()
                 {
                     Id = 7,
                     Nombre = "Tecnico 7",
                 },
                 new()
                 {
                     Id = 8,
                     Nombre = "Tecnico 8",
                 },
            ];
        }
    }
}
