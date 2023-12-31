﻿using Hacienda.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hacienda.Infrastructure.DbContextEf;

/// <summary>
/// Aplica la configuración de la entidad CategoriaProducto
/// </summary>
public class EntityDbContextCategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categorias");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Nombre)
            .HasColumnName("Nombre")
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(e => e.Descripcion)
            .HasColumnName("Descripcion")
            .HasMaxLength(100);
        builder.Property(e => e.FechaAlta)
            .HasColumnName("FechaAlta")
            .HasDefaultValueSql("GETDATE()");
    }
}
