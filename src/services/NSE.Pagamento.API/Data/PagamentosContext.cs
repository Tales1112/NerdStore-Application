﻿using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NSE.Core.Data;
using NSE.Core.Messages;
using NSE.Pagamentos.API.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Pagamentos.API.Data
{
    public class PagamentosContext : DbContext, IUnitOfWork
    {
        public PagamentosContext(DbContextOptions<PagamentosContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Cascade;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PagamentosContext).Assembly);
        }
        public async Task<bool> Commit()
        {
            return await SaveChangesAsync() > 0;
        }
    }
}
