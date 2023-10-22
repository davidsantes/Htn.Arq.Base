using AutoMapper;
using Hacienda.Application.Dtos;
using Hacienda.Application.Dtos.Primitives;
using Hacienda.Domain.Entities;
using Hacienda.Domain.Primitives;

namespace Hacienda.Application.Mapping
{
    public class CategoriaMappingProfile : Profile
    {
        public CategoriaMappingProfile()
        {
            //Request
            CreateMap<InsertCategoriaProductoRequest, CategoriaProducto>();
            CreateMap<CategoriaProductoIdRequest, CategoriaProductoId>();

            //Response
            CreateMap<CategoriaProducto, GetCategoriaProductoResponse>();
            CreateMap<CategoriaProductoId, CategoriaProductoIdResponse>();
        }
    }
}