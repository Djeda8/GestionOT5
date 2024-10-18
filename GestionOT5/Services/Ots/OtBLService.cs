using AutoMapper;
using GestionOT5.Dto;
using GestionOT5.MVVM.Models;
using Newtonsoft.Json;
using System;

namespace GestionOT5.Services.Ots
{
    public class OtBLService : ApiBLService, IOtService
    {
        public OtBLService(IMapper mapper) : base(mapper)
        {

        }

        public async Task<IEnumerable<Ot>> GetOtsAsync()
        {
            var uri = new Uri($"http://192.168.0.60:5010/api/Ots");

            IEnumerable<OtDTOfinal> itemList = [];
            IEnumerable<Ot> result = [];

            try
            {
                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var returnMessage = await response.Content.ReadAsStringAsync();

                    itemList = JsonConvert.DeserializeObject<List<OtDTOfinal>>(returnMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (itemList is not null)
            {
                result = _mapper.Map<IEnumerable<OtDTOfinal>, IEnumerable<Ot>>(itemList);
            }
            return result;
        }
    }
}
