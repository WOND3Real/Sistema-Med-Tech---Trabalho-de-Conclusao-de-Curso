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

    public DbSet<ContadorData> ContadorData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString).LogTo(Console.WriteLine, LogLevel.Information);;
        }
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<ContadorData>()
            .HasNoKey();

        modelBuilder.Entity<Administrador>(entity =>
        {
            // Corrigido a chave primária composta
            entity.HasKey(e => new { e.IdAdministrador, e.UnidadeIdUnidade }).HasName("administrador_pkey");

            entity.ToTable("administrador");

            // Correção nos nomes das colunas e valores gerados automaticamente
            entity.Property(e => e.IdAdministrador)
                .ValueGeneratedOnAdd()
                .HasColumnName("idadministrador");

            entity.Property(e => e.UnidadeIdUnidade)
                .HasColumnName("unidade_idunidade");

            entity.Property(e => e.NomeAdm)
                .HasMaxLength(20)
                .HasColumnName("nomeadm");

            entity.Property(e => e.SenhaAdm)  // Corrigido para 'SenhaAdm' (de acordo com o nome das propriedades)
                .HasMaxLength(20)
                .HasColumnName("senhaadm");

            entity.Property(e => e.SobrenomeAdm)  // Corrigido para 'SobrenomeAdm'
                .HasMaxLength(20)
                .HasColumnName("sobrenomeadm");

            // Relação de chave estrangeira
            entity.HasOne(d => d.UnidadeIdUnidadeNavigation)
                .WithMany(p => p.Administradors)  // Verifique se o nome da coleção é 'Administradores'
                .HasForeignKey(d => d.UnidadeIdUnidade)
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
            entity.HasKey(e => e.IdConsulta).HasName("consulta_pkey");

            entity.ToTable("consulta");

            // Mapeamento das propriedades
            entity.Property(e => e.IdConsulta).ValueGeneratedOnAdd()
                .HasColumnName("idconsulta");

            entity.Property(e => e.PacienteCpfPaci)
                .HasMaxLength(14)
                .HasColumnName("paciente_cpfpaci");

            entity.Property(e => e.AtendenteCtpsAtend)
                .HasMaxLength(15)
                .HasColumnName("atendente_ctpsatend");

            entity.Property(e => e.MedicoCrmMed)
                .HasMaxLength(7)
                .HasColumnName("medico_crmmed");

            entity.Property(e => e.UnidadeIdUnidade)
                .HasColumnName("unidade_idunidade");

            entity.Property(e => e.EspecialidadeIdEspecialidade)
                .HasColumnName("especialidade_idespecialidade");

            // Mapeamento da Data e Hora da consulta com conversão para DateOnly e TimeOnly
            entity.Property(e => e.DataConsul)
                .HasColumnName("dataconsul")
                .HasConversion(
                    v => v.ToDateTime(TimeOnly.MinValue),  // Convertendo DateOnly para DateTime para armazenar no banco
                    v => DateOnly.FromDateTime(v)           // Convertendo de DateTime para DateOnly ao ler do banco
                );

            entity.Property(e => e.HoraConsul)
                .HasColumnName("horaconsul")
                .HasConversion(
                    v => v.ToTimeSpan(),                   // Convertendo TimeOnly para TimeSpan para armazenar no banco
                    v => TimeOnly.FromTimeSpan(v)          // Convertendo de TimeSpan para TimeOnly ao ler do banco
                );

            entity.Property(e => e.StatusConsul)
                .HasColumnName("statusconsul")
                .HasMaxLength(50);  // Pode ajustar o comprimento conforme necessário

            // Configuração das relações de chave estrangeira
            entity.HasOne(d => d.AtendenteCtpsAtendNavigation)
                .WithMany(p => p.Consulta)
                .HasForeignKey(d => d.AtendenteCtpsAtend)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consulta_atendente_ctpsatend_fkey");

            entity.HasOne(d => d.EspecialidadeIdEspecialidadeNavigation)
                .WithMany(p => p.Consulta)
                .HasForeignKey(d => d.EspecialidadeIdEspecialidade)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consulta_especialidade_idespecialidade_fkey");

            entity.HasOne(d => d.MedicoCrmMedNavigation)
                .WithMany(p => p.Consulta)
                .HasForeignKey(d => d.MedicoCrmMed)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consulta_medico_crmmed_fkey");

            entity.HasOne(d => d.PacienteCpfPaciNavigation)
                .WithMany(p => p.Consulta)
                .HasForeignKey(d => d.PacienteCpfPaci)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consulta_paciente_cpfpaci_fkey");

            entity.HasOne(d => d.UnidadeIdUnidadeNavigation)
                .WithMany(p => p.Consulta)
                .HasForeignKey(d => d.UnidadeIdUnidade)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consulta_unidade_idunidade_fkey");
        });



        modelBuilder.Entity<Especialidade>(entity =>
        {
            entity.HasKey(e => e.Idespecialidade).HasName("especialidade_pkey");

            entity.ToTable("especialidade");

            entity.Property(e => e.Idespecialidade)
                .ValueGeneratedOnAdd()
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
                .HasColumnName("nascimentopaci")
                .HasConversion(new DateOnlyConverter());

            entity.Property(e => e.GeneroPaci)
                .HasColumnName("generopaci");

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
                .ValueGeneratedOnAdd()
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

        OnModelCreatingPartial(modelBuilder);
        });
    }

    internal object SetFromSqlRaw(string v)
    {
        throw new NotImplementedException();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
