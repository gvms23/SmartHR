using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHR.Infra.Data.Mapping
{
    public class VagaMap : MapBase
    {
        public void Configure(EntityTypeBuilder<Vaga> builder)
        {

            base.ConfigureBase(builder);
        }
    }
}
