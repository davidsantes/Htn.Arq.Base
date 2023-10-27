using AutoMapper;
using Hacienda.Application.Dtos;
using Hacienda.Domain.Entities;

namespace Hacienda.Application.Mapping;

public class CategoriaMappingProfile : Profile
{
    public CategoriaMappingProfile()
    {
        //Request
        CreateMap<InsertCategoriaProductoRequest, CategoriaProducto>()
            .ConstructUsing(request => CategoriaProducto.Crear(request.Nombre, request.Descripcion));
        CreateMap<UpdateCategoriaProductoRequest, CategoriaProducto>()
            .ConstructUsing(request => CategoriaProducto.Crear(request.Nombre, request.Descripcion));

        //Response
        CreateMap<CategoriaProducto, GetCategoriaProductoResponse>();
    }
}