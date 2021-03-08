using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace ReservationSystem.Models
{
    public partial class ReservationContext : DbContext
    {
        private readonly IConfiguration configuration;
        public ReservationContext()
        {

        }

        public ReservationContext(DbContextOptions<ReservationContext> options, IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;
        }

        public virtual DbSet<planing> planings { get; set; }
        public virtual DbSet<reservation> reservations { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<typereservation> typereservations { get; set; }
        public virtual DbSet<utilisateur> utilisateurs { get; set; }
        public virtual DbSet<utilisateur_role> utilisateur_roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(configuration.GetConnectionString("MySQLConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<planing>(entity =>
            {
                entity.HasKey(e => e.IdPlan)
                    .HasName("PRIMARY");

                entity.HasOne(d => d.IdTypeRNavigation)
                    .WithMany(p => p.planings)
                    .HasForeignKey(d => d.IdTypeR)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("planing_ibfk_1");
            });

            modelBuilder.Entity<reservation>(entity =>
            {
                entity.HasKey(e => e.IdRe)
                    .HasName("PRIMARY");

                entity.Property(e => e.Date_demande).HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Status).HasDefaultValueSql("'''En Attente'''");

                entity.HasOne(d => d.IdPlanNavigation)
                    .WithMany(p => p.reservations)
                    .HasForeignKey(d => d.IdPlan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reservation_ibfk_2");

                entity.HasOne(d => d.IdTypeRNavigation)
                    .WithMany(p => p.reservations)
                    .HasForeignKey(d => d.IdTypeR)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reservation_ibfk_1");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.reservations)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reservation_ibfk_3");
            });

            modelBuilder.Entity<role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PRIMARY");
            });

            modelBuilder.Entity<typereservation>(entity =>
            {
                entity.HasKey(e => e.IdTypeR)
                    .HasName("PRIMARY");
            });

            modelBuilder.Entity<utilisateur>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PRIMARY");

                entity.Property(e => e.NRV).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Nom).HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Prenom).HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<utilisateur_role>(entity =>
            {
                entity.HasKey(e => new { e.IdUser, e.IdRole })
                    .HasName("PRIMARY");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.utilisateur_roles)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("utilisateur_role_ibfk_2");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.utilisateur_roles)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("utilisateur_role_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
