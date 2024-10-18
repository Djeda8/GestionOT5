using AutoMapper;
using GestionOT5.Dto;
using GestionOT5.MVVM.Models;
using Newtonsoft.Json;

namespace GestionOT5.Services.Subfamilias
{
    public class FamiliaBLService : ApiBLService, IFamiliaService
    {
        public FamiliaBLService(IMapper mapper) : base(mapper)
        {

        }

        public async Task<IEnumerable<Familia>> GetFamiliasAsync()
        {
            var uri = new Uri($"http://192.168.0.60:5010/api/Familias");

            List<FamiliaDto> itemList = null;

            try
            {
                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var returnMessage = await response.Content.ReadAsStringAsync();

                    itemList = JsonConvert.DeserializeObject<List<FamiliaDto>>(returnMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var result = _mapper.Map<IEnumerable<FamiliaDto>, IEnumerable<Familia>>(itemList);

            return result;
        }

    }
}
