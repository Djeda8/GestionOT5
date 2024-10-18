using GestionOT5.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOT5.Services.Materiales
{
    public class MaterialesServiceMock : IMaterialesService
    {
        public async Task<IEnumerable<Material>> GetMaterialesAsync()
        {
            await Task.Delay(1000);

            return
            [
                 new()
                 {
                     Id = 1,
                     Descripcion = "Material 1",
                     Unidad = "Uds"
                 },
                 new()
                 {
                     Id = 2,
                     Descripcion = "Material 2",
                     Unidad = "Uds"
                 },
                 new()
                 {
                     Id = 3,
                     Descripcion = "Material 3",
                     Unidad = "Uds"
                 },
                 new()
                 {
                     Id = 4,
                     Descripcion = "Material 4",
                     Unidad = "Uds"
                 },
                 new()
                 {
                     Id = 5,
                     Descripcion = "Material 5",
                     Unidad = "Uds"
                 },
                 new()
                 {
                     Id = 6,
                     Descripcion = "Material 6",
                     Unidad = "Uds"
                 },
                 new()
                 {
                     Id = 7,
                     Descripcion = "Material 7",
                     Unidad = "Uds"
                 },
                 new()
                 {
                     Id = 8,
                     Descripcion = "Material 8",
                     Unidad = "Uds"
                 },
            ];
        }
    }
}
