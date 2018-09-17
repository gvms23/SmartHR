using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHR.Infra.Data.Mapping
{
    public class MapBase
    {
        public void ConfigureBase<T>(EntityTypeBuilder<T> builder) where T : EntityBase
        {
            builder.HasIndex(e => e.Id);

            builder.Property(p => p.DataCriacao)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.Ativo)
                .IsRequired();
        }
    }
}
