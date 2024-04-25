using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SafeGuardPro.Models;

namespace SafeGuardPro.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Colaborador> Colaboradors { get; set; }

    public virtual DbSet<Entrega> Entregas { get; set; }

    public virtual DbSet<Epi> Epis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=projeto;User Id=postgres;Password=senai901;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Colaborador>(entity =>
        {
            entity.HasKey(e => e.CodCol).HasName("colaborador_pkey");

            entity.ToTable("colaborador");

            entity.HasIndex(e => e.Cpf, "colaborador_cpf_key").IsUnique();

            entity.HasIndex(e => e.NumTel, "colaborador_num_tel_key").IsUnique();

            entity.HasIndex(e => e.CodCol, "idx_cod_colaborador");

            entity.HasIndex(e => e.Cpf, "idx_cod_cpf");

            entity.HasIndex(e => e.Ctps, "idx_cod_ctps");

            entity.HasIndex(e => e.Ctps, "uk_ctps").IsUnique();

            entity.Property(e => e.CodCol).HasColumnName("cod_col");
            entity.Property(e => e.Cpf).HasColumnName("cpf");
            entity.Property(e => e.Ctps).HasColumnName("ctps");
            entity.Property(e => e.DataAdmissao).HasColumnName("data_admissao");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.NomeCol)
                .HasMaxLength(50)
                .HasColumnName("nome_col");
            entity.Property(e => e.NumTel).HasColumnName("num_tel");
        });

        modelBuilder.Entity<Entrega>(entity =>
        {
            entity.HasKey(e => e.CodEntrega).HasName("entrega_pkey");

            entity.ToTable("entrega");

            entity.HasIndex(e => e.CodEntrega, "idx_cod_entrega");

            entity.HasIndex(e => new { e.CodEpi, e.CodCol }, "idx_cod_epi_colaborador");

            entity.Property(e => e.CodEntrega).HasColumnName("cod_entrega");
            entity.Property(e => e.CodCol).HasColumnName("cod_col");
            entity.Property(e => e.CodEpi).HasColumnName("cod_epi");
            entity.Property(e => e.DataEntrega).HasColumnName("data_entrega");
            entity.Property(e => e.DataVal).HasColumnName("data_val");

            entity.HasOne(d => d.CodColNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.CodCol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entrega_cod_col_fkey");

            entity.HasOne(d => d.CodEpiNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.CodEpi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entrega_cod_epi_fkey");
        });

        modelBuilder.Entity<Epi>(entity =>
        {
            entity.HasKey(e => e.CodEpi).HasName("epi_pkey");

            entity.ToTable("epi");

            entity.HasIndex(e => e.CodEpi, "idx_cod_epi");

            entity.Property(e => e.CodEpi).HasColumnName("cod_epi");
            entity.Property(e => e.Descricao)
                .HasMaxLength(100)
                .HasColumnName("descricao");
            entity.Property(e => e.NomeEpi)
                .HasMaxLength(50)
                .HasColumnName("nome_epi");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
