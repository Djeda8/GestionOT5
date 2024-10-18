using GestionOT5.MVVM.Models;

namespace GestionOT5.Services.Subfamilias
{
    public class FamiliaServiceMock : IFamiliaService
    {
        public async Task<IEnumerable<Familia>> GetFamiliasAsync()
        {
            await Task.Delay(1000);

            return
            [
                 new()
                 {
                     Id = 1,
                     CodigoSubfamlia = "Subfamilia 1",
                 },
                 new()
                 {
                     Id = 2,
                     CodigoSubfamlia = "Subfamilia 2",
                 },
                 new()
                 {
                     Id = 3,
                     CodigoSubfamlia = "Subfamilia 3",
                 },
                 new()
                 {
                     Id = 4,
                     CodigoSubfamlia = "Subfamilia 4",
                 },
                 new()
                 {
                     Id = 5,
                     CodigoSubfamlia = "Subfamilia 5",
                 },
                 new()
                 {
                     Id = 6,
                     CodigoSubfamlia = "Subfamilia 6",
                 },
                 new()
                 {
                     Id = 7,
                     CodigoSubfamlia = "Subfamilia 7",
                 },
                 new()
                 {
                     Id = 8,
                     CodigoSubfamlia = "Subfamilia 8",
                 },
            ];
        }
    }
}
