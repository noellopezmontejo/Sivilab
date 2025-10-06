using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Registro_Evento.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet para la tabla Usuario
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración específica para la tabla Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                // Configurar la tabla
                entity.ToTable("Usuario");

                // Configurar la clave primaria
                entity.HasKey(e => e.Id);

                // Configurar columnas específicas
                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd(); // IDENTITY(1,1)

                entity.Property(e => e.Nombre)
                    .HasColumnType("nvarchar(100)")
                    .IsRequired();

                entity.Property(e => e.Apellido)
                    .HasColumnType("nvarchar(100)")
                    .IsRequired();

                entity.Property(e => e.Curp)
                    .HasColumnType("char(18)")
                    .IsRequired();

                entity.Property(e => e.Edad)
                    .HasColumnType("int")
                    .IsRequired(false);

                entity.Property(e => e.Escolaridad)
                    .HasColumnType("nvarchar(100)")
                    .IsRequired(false);

                entity.Property(e => e.Email)
                    .HasColumnType("nvarchar(150)")
                    .IsRequired(false);

                entity.Property(e => e.Telefono)
                    .HasColumnType("nvarchar(20)")
                    .IsRequired(false);

                // Configurar índices únicos
                entity.HasIndex(e => e.Curp)
                    .IsUnique()
                    .HasDatabaseName("IX_Usuario_Curp");

                entity.HasIndex(e => e.Email)
                    .IsUnique()
                    .HasDatabaseName("IX_Usuario_Email");

                // Configurar restricciones
                entity.ToTable(t => t.HasCheckConstraint("CK_Usuario_Edad", "Edad >= 0"));
            });
        }
    }
}