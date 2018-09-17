using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHR.Infra.Data.Mapping
{
    public class CandidaturaMap
    {
        public void Configure(EntityTypeBuilder<Candidatura> builder)
        {
            builder
                .HasKey(c => new { c.PessoaID, c.VagaID });

            builder
                .Property(p => p.DataCriacao)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");

            builder
                .HasOne(c => c.Pessoa)
                .WithMany(p => p.Candidaturas)
                .HasForeignKey(c => c.PessoaID);

            builder
                .HasOne(c => c.Vaga)
                .WithMany(v => v.Candidaturas)
                .HasForeignKey(c => c.VagaID);
        }
    }
}
