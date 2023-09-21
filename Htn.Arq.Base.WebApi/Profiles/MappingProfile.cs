using AutoMapper;
using Htn.Arq.Base.Bll.Entities;
using Htn.Arq.Base.WebApi.Dtos;

namespace Htn.Arq.Base.WebApi.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CategoriaProducto, CategoriaProductoDto>();
            CreateMap<CategoriaProductoDto, CategoriaProducto>();
        }
    }
}