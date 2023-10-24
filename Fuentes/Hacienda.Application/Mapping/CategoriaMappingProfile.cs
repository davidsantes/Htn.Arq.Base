using AutoMapper;
using Hacienda.Application.Dtos;
using Hacienda.Domain.Entities;

namespace Hacienda.Application.Mapping;

public class CategoriaMappingProfile : Profile
{
    public CategoriaMappingProfile()
    {
        //Request
        CreateMap<InsertCategoriaProductoRequest, CategoriaProducto>();

        //Response
        CreateMap<CategoriaProducto, GetCategoriaProductoResponse>();
    }
}