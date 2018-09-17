using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHR.Infra.Data.Mapping
{
    public class PessoaMap : MapBase
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {

            base.ConfigureBase(builder);
        }
    }
}
