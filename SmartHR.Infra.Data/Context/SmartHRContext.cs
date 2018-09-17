using Microsoft.EntityFrameworkCore;
using SmartHR.Domain.Entities;
using SmartHR.Infra.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHR.Infra.Data.Context
{
    public class SmartHRContext : DbContext
    {
        public SmartHRContext(DbContextOptions<SmartHRContext> options)
        : base(options) { }

        public DbSet<Candidatura> Candidaturas { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Vaga> Vagas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vaga>(new VagaMap().Configure);
            modelBuilder.Entity<Candidatura>(new CandidaturaMap().Configure);
            modelBuilder.Entity<Pessoa>(new PessoaMap().Configure);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCriacao") != null))
            {

                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCriacao").CurrentValue = DateTime.Now;
                    if (entry.Entity.GetType().GetProperty("Ativo") != null)
                        entry.Property("Ativo").CurrentValue = true;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCriacao").IsModified = false;
                    if (entry.Entity.GetType().GetProperty("DataModificacao") != null)
                        entry.Property("DataModificacao").CurrentValue = DateTime.Now;
                }

            }


            return base.SaveChanges();
        }
    }
}
