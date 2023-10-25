﻿using FluentValidation;
using Hacienda.Shared.Global.Resources;

namespace Hacienda.Application.Dtos.Categorias.Validators;

public class UpdateCategoriaProductoRequestValidator : AbstractValidator<UpdateCategoriaProductoRequest>
{
    public UpdateCategoriaProductoRequestValidator()
    {
        RuleFor(categoria => categoria.Nombre)
            .NotEmpty().WithMessage(ValidationResources.CampoObligatorio)
            .MaximumLength(50).WithMessage(string.Format(ValidationResources.CampoLongitudMaxima, 50));

        RuleFor(categoria => categoria.Descripcion)
            .NotEmpty().WithMessage(ValidationResources.CampoObligatorio)
            .MaximumLength(100).WithMessage(string.Format(ValidationResources.CampoLongitudMaxima, 100));
    }
}