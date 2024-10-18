using GestionOT5.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOT5.Services.Tareas
{
    public class TareasServiceMock : ITareasService
    {
        public async Task<IEnumerable<Tarea>> GetTareasAsync()
        {
            await Task.Delay(1000);

            return
            [
                 new()
                 {
                     Id = 1,
                     Descripcion = "Tarea 1",
                     Done = false
                 },
                 new()
                 {
                     Id = 2,
                     Descripcion = "Tarea 2",
                     Done = false
                 },
                 new()
                 {
                     Id = 3,
                     Descripcion = "Tarea 3",
                     Done = false
                 },
                 new()
                 {
                     Id = 4,
                     Descripcion = "Tarea 4",
                     Done = false
                 },
                 new()
                 {
                     Id = 5,
                     Descripcion = "Tarea 5",
                     Done = false
                 },
                 new()
                 {
                     Id = 6,
                     Descripcion = "Tarea 6",
                     Done = false
                 },
                 new()
                 {
                     Id = 7,
                     Descripcion = "Tarea 7",
                     Done = false
                 },
                 new()
                 {
                     Id = 8,
                     Descripcion = "Tarea 8",
                     Done = false
                 },
            ];
        }
    }
}
