using AutoMapper;
using GestionOT5.MVVM.Models;

namespace GestionOT5.Dto
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            // Ots
            CreateMap<Ot, OtDTOfinal>().ReverseMap();
            CreateMap<Familia, FamiliaDto>().ReverseMap();
        }
    }
}
