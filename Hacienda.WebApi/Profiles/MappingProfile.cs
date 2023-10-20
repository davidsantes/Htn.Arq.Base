using AutoMapper;
using Hacienda.Domain.Entities;
using Hacienda.WebApi.Dtos;

namespace Hacienda.WebApi.Profiles
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