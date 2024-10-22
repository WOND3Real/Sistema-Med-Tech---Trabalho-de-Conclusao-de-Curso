using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace SMT.Models;

public partial class ContextoBD : DbContext
{
    public ContextoBD()
    {
    }
    public ContextoBD(DbContextOptions<ContextoBD> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrador> Administradores { get; set; }

    public virtual DbSet<Atendente> Atendentes { get; set; }

    public virtual DbSet<Consultum> Consulta { get; set; }

    public virtual DbSet<Especialidade> Especialidades { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Unidade> Unidades { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("DefaultConnection");
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("dispodia", new[] { "Sim", "Não" })
            .HasPostgresEnum("genero", new[] { "Masculino", "Feminino", "Outro" })
            .HasPostgresEnum("statusconsulta", new[] { "Pendente", "Andamento", "Concluida", "Cancelada" });

        modelBuilder.Entity<Administrador>(entity =>
        {
            entity.HasKey(e => new { e.Idadministrador, e.UnidadeIdunidade }).HasName("administrador_pkey");

            entity.ToTable("administrador");

            entity.Property(e => e.Idadministrador).HasColumnName("idadministrador");
            entity.Property(e => e.UnidadeIdunidade).HasColumnName("unidade_idunidade");
            entity.Property(e => e.Nomeadm)
                .HasMaxLength(20)
                .HasColumnName("nomeadm");
            entity.Property(e => e.Senhaadm)
                .HasMaxLength(20)
                .HasColumnName("senhaadm");
            entity.Property(e => e.Sobrenomeadm)
                .HasMaxLength(20)
                .HasColumnName("sobrenomeadm");

            entity.HasOne(d => d.UnidadeIdunidadeNavigation).WithMany(p => p.Administradors)
                .HasForeignKey(d => d.UnidadeIdunidade)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("administrador_unidade_idunidade_fkey");
        });

        modelBuilder.Entity<Atendente>(entity =>
        {
            entity.HasKey(e => e.Ctpsatend).HasName("atendente_pkey");

            entity.ToTable("atendente");

            entity.Property(e => e.Ctpsatend)
                .HasMaxLength(15)
                .HasColumnName("ctpsatend");
            entity.Property(e => e.Fimturnoatend).HasColumnName("fimturnoatend");
            entity.Property(e => e.Inicioturnoatend).HasColumnName("inicioturnoatend");
            entity.Property(e => e.Nomeatend)
                .HasMaxLength(45)
                .HasColumnName("nomeatend");
            entity.Property(e => e.Senhaatend)
                .HasMaxLength(20)
                .HasColumnName("senhaatend");
            entity.Property(e => e.Sobrenomeatend)
                .HasMaxLength(45)
                .HasColumnName("sobrenomeatend");
        });

        modelBuilder.Entity<Consultum>(entity =>
        {
            entity.HasKey(e => new { e.Idconsulta, e.PacienteCpfpaci, e.AtendenteCtpsatend, e.MedicoCrmmed, e.UnidadeIdunidade, e.EspecialidadeIdespecialidade }).HasName("consulta_pkey");

            entity.ToTable("consulta");

            entity.Property(e => e.Idconsulta).HasColumnName("idconsulta");
            entity.Property(e => e.PacienteCpfpaci)
                .HasMaxLength(14)
                .HasColumnName("paciente_cpfpaci");
            entity.Property(e => e.AtendenteCtpsatend)
                .HasMaxLength(15)
                .HasColumnName("atendente_ctpsatend");
            entity.Property(e => e.MedicoCrmmed)
                .HasMaxLength(7)
                .HasColumnName("medico_crmmed");
            entity.Property(e => e.UnidadeIdunidade).HasColumnName("unidade_idunidade");
            entity.Property(e => e.EspecialidadeIdespecialidade).HasColumnName("especialidade_idespecialidade");
            entity.Property(e => e.Dataconsul).HasColumnName("dataconsul");
            entity.Property(e => e.Horaconsul).HasColumnName("horaconsul");

            entity.HasOne(d => d.AtendenteCtpsatendNavigation).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.AtendenteCtpsatend)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consulta_atendente_ctpsatend_fkey");

            entity.HasOne(d => d.EspecialidadeIdespecialidadeNavigation).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.EspecialidadeIdespecialidade)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consulta_especialidade_idespecialidade_fkey");

            entity.HasOne(d => d.MedicoCrmmedNavigation).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.MedicoCrmmed)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consulta_medico_crmmed_fkey");

            entity.HasOne(d => d.PacienteCpfpaciNavigation).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.PacienteCpfpaci)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consulta_paciente_cpfpaci_fkey");

            entity.HasOne(d => d.UnidadeIdunidadeNavigation).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.UnidadeIdunidade)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consulta_unidade_idunidade_fkey");
        });

        modelBuilder.Entity<Especialidade>(entity =>
        {
            entity.HasKey(e => e.Idespecialidade).HasName("especialidade_pkey");

            entity.ToTable("especialidade");

            entity.Property(e => e.Idespecialidade)
                .ValueGeneratedNever()
                .HasColumnName("idespecialidade");
            entity.Property(e => e.Nomeespec).HasColumnName("nomeespec");
        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.HasKey(e => e.Crmmed).HasName("medico_pkey");

            entity.ToTable("medico");

            entity.Property(e => e.Crmmed)
                .HasMaxLength(7)
                .HasColumnName("crmmed");
            entity.Property(e => e.Emailmed)
                .HasMaxLength(45)
                .HasColumnName("emailmed");
            entity.Property(e => e.Nomemed)
                .HasMaxLength(45)
                .HasColumnName("nomemed");
            entity.Property(e => e.Senhamed)
                .HasMaxLength(20)
                .HasColumnName("senhamed");
            entity.Property(e => e.Sobrenomemed)
                .HasMaxLength(45)
                .HasColumnName("sobrenomemed");
            entity.Property(e => e.Telefonemed)
                .HasMaxLength(14)
                .HasColumnName("telefonemed");

            entity.HasMany(d => d.EspecialidadeIdespecialidades).WithMany(p => p.MedicoCrmmeds)
                .UsingEntity<Dictionary<string, object>>(
                    "RelMedEspec",
                    r => r.HasOne<Especialidade>().WithMany()
                        .HasForeignKey("EspecialidadeIdespecialidade")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("rel_med_espec_especialidade_idespecialidade_fkey"),
                    l => l.HasOne<Medico>().WithMany()
                        .HasForeignKey("MedicoCrmmed")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("rel_med_espec_medico_crmmed_fkey"),
                    j =>
                    {
                        j.HasKey("MedicoCrmmed", "EspecialidadeIdespecialidade").HasName("rel_med_espec_pkey");
                        j.ToTable("rel_med_espec");
                        j.IndexerProperty<string>("MedicoCrmmed")
                            .HasMaxLength(7)
                            .HasColumnName("medico_crmmed");
                        j.IndexerProperty<int>("EspecialidadeIdespecialidade").HasColumnName("especialidade_idespecialidade");
                    });

            entity.HasMany(d => d.UnidadeIdunidades).WithMany(p => p.MedicoCrmmeds)
                .UsingEntity<Dictionary<string, object>>(
                    "Disponibilidade",
                    r => r.HasOne<Unidade>().WithMany()
                        .HasForeignKey("UnidadeIdunidade")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("disponibilidade_unidade_idunidade_fkey"),
                    l => l.HasOne<Medico>().WithMany()
                        .HasForeignKey("MedicoCrmmed")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("disponibilidade_medico_crmmed_fkey"),
                    j =>
                    {
                        j.HasKey("MedicoCrmmed", "UnidadeIdunidade").HasName("disponibilidade_pkey");
                        j.ToTable("disponibilidade");
                        j.IndexerProperty<string>("MedicoCrmmed")
                            .HasMaxLength(7)
                            .HasColumnName("medico_crmmed");
                        j.IndexerProperty<int>("UnidadeIdunidade").HasColumnName("unidade_idunidade");
                    });

            entity.HasMany(d => d.UnidadeIdunidadesNavigation).WithMany(p => p.MedicoCrmmedsNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "RelMedUnidade",
                    r => r.HasOne<Unidade>().WithMany()
                        .HasForeignKey("UnidadeIdunidade")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("rel_med_unidade_unidade_idunidade_fkey"),
                    l => l.HasOne<Medico>().WithMany()
                        .HasForeignKey("MedicoCrmmed")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("rel_med_unidade_medico_crmmed_fkey"),
                    j =>
                    {
                        j.HasKey("MedicoCrmmed", "UnidadeIdunidade").HasName("rel_med_unidade_pkey");
                        j.ToTable("rel_med_unidade");
                        j.IndexerProperty<string>("MedicoCrmmed")
                            .HasMaxLength(7)
                            .HasColumnName("medico_crmmed");
                        j.IndexerProperty<int>("UnidadeIdunidade").HasColumnName("unidade_idunidade");
                    });
        });

        modelBuilder.Entity<Paciente>(entity => 
        {
            entity.HasKey(e => e.CpfPaci).HasName("paciente_pkey");

            entity.ToTable("paciente");

            entity.Property(e => e.CpfPaci)
                .HasMaxLength(14)
                .HasColumnName("cpfpaci");

            entity.Property(e => e.NomePaci)
                .HasMaxLength(45)
                .HasColumnName("nomepaci");

            entity.Property(e => e.SobrenomePaci)
                .HasMaxLength(45)
                .HasColumnName("sobrenomepaci");

            entity.Property(e => e.NascimentoPaci)
                .HasColumnName("nascimentopaci");

            entity.Property(e => e.generoPaci)
                .HasConversion<string>()  // Converte o enum para string no banco de dados
                .HasColumnName("genero");

            entity.Property(e => e.EmailPaci)
                .HasMaxLength(45)
                .HasColumnName("emailpaci");

            entity.Property(e => e.TelefonePaci)
                .HasMaxLength(14)
                .HasColumnName("telefonepaci");

            entity.Property(e => e.SenhaPaci)
                .HasMaxLength(20)
                .HasColumnName("senhapaci");
        });


        modelBuilder.Entity<Unidade>(entity =>
        {
            entity.HasKey(e => e.Idunidade).HasName("unidade_pkey");

            entity.ToTable("unidade");

            entity.Property(e => e.Idunidade)
                .ValueGeneratedNever()
                .HasColumnName("idunidade");
            entity.Property(e => e.Bairrouni)
                .HasMaxLength(20)
                .HasColumnName("bairrouni");
            entity.Property(e => e.Cepuni)
                .HasMaxLength(10)
                .HasColumnName("cepuni");
            entity.Property(e => e.Cidadeuni)
                .HasMaxLength(20)
                .HasColumnName("cidadeuni");
            entity.Property(e => e.Estadouni)
                .HasMaxLength(20)
                .HasColumnName("estadouni");
            entity.Property(e => e.Logradourouni)
                .HasMaxLength(45)
                .HasColumnName("logradourouni");
            entity.Property(e => e.Nomeunidade)
                .HasMaxLength(20)
                .HasColumnName("nomeunidade");
            entity.Property(e => e.Numerouni)
                .HasMaxLength(6)
                .HasColumnName("numerouni");
            entity.Property(e => e.Paisuni)
                .HasMaxLength(20)
                .HasColumnName("paisuni");

            entity.HasMany(d => d.EspecialidadeIdespecialidades).WithMany(p => p.UnidadeIdunidades)
                .UsingEntity<Dictionary<string, object>>(
                    "RelUniEspec",
                    r => r.HasOne<Especialidade>().WithMany()
                        .HasForeignKey("EspecialidadeIdespecialidade")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("rel_uni_espec_especialidade_idespecialidade_fkey"),
                    l => l.HasOne<Unidade>().WithMany()
                        .HasForeignKey("UnidadeIdunidade")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("rel_uni_espec_unidade_idunidade_fkey"),
                    j =>
                    {
                        j.HasKey("UnidadeIdunidade", "EspecialidadeIdespecialidade").HasName("rel_uni_espec_pkey");
                        j.ToTable("rel_uni_espec");
                        j.IndexerProperty<int>("UnidadeIdunidade").HasColumnName("unidade_idunidade");
                        j.IndexerProperty<int>("EspecialidadeIdespecialidade").HasColumnName("especialidade_idespecialidade");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
