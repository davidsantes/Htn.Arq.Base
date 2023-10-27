using AutoMapper;
using Hacienda.Application.Dtos;
using Hacienda.Domain.Entities;

namespace Hacienda.Application.Mapping;

public class CategoriaMappingProfile : Profile
{
    public CategoriaMappingProfile()
    {
        //Request
        CreateMap<InsertCategoriaProductoRequest, Categoria>()
            .ConstructUsing(request => Categoria.Crear(request.Nombre, request.Descripcion));
        CreateMap<UpdateCategoriaProductoRequest, Categoria>()
            .ConstructUsing(request => Categoria.Crear(request.Nombre, request.Descripcion));

        //Response
        CreateMap<Categoria, GetCategoriaProductoResponse>();
    }
}