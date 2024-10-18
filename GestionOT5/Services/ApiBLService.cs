using AutoMapper;
using System.Net.Http.Headers;

namespace GestionOT5.Services
{
    public class ApiBLService
    {
        public HttpClient _client;
        protected IMapper _mapper;

        public ApiBLService(IMapper mapper)
        {
            _mapper = mapper;
            _client = new HttpClient();
            _client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
