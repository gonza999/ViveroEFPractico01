using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using Vivero.Entities;

namespace Vivero.Data
{
    public class ViveroDbContext : DbContext
    {
        public ViveroDbContext()
            : base("name=ViveroDbContext")
        {
        }

        public virtual DbSet<Planta> Plantas { get; set; }
        public virtual DbSet<TipoDePlanta> TiposDePlantas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Planta>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<TipoDePlanta>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<TipoDePlanta>()
                .HasMany(e => e.Plantas)
                .WithRequired(e => e.TiposDePlanta)
                .WillCascadeOnDelete(false);
        }
    }
}
