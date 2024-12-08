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

    public virtual DbSet<Especialidade> Especialidades { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public DbSet<ContadorData> ContadorData { get; set; }

    public DbSet<ConsultaDetalhada> ConsultaDetalhada { get; set; }

    public DbSet<Contribuinte> Contribuintes { get; set; } 

    public DbSet<ConsultaPaciente> ConsultaPacientes { get; set; }

    public DbSet<Unidade> Unidade { get; set; }

    public DbSet<MedicoEspecialidade> MedicosEspecialidades { get; set; }

    public DbSet<Consulta> Consulta { get; set; }

    public DbSet<Atendente> Atendente { get; set; }

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

        // Configurações específicas para o modelo Atendente (opcional)
        modelBuilder.Entity<Atendente>(entity =>
        {
            entity.HasKey(a => a.CTPAtend); // Define a chave primária
            entity.Property(a => a.CTPAtend).HasColumnName("ctpsatend").HasMaxLength(15);
            entity.Property(a => a.NomeAtend).HasColumnName("nomeatend").HasMaxLength(45);
            entity.Property(a => a.SobrenomeAtend).HasColumnName("sobrenomeatend").HasMaxLength(45);
            entity.Property(a => a.InicioTurnoAtend).HasColumnName("inicioturnoatend");
            entity.Property(a => a.FimTurnoAtend).HasColumnName("fimturnoatend");
            entity.Property(a => a.SenhaAtend).HasColumnName("senhaatend").HasMaxLength(20);
        });

        // Configuração da tabela 'consulta'
        modelBuilder.Entity<Consulta>(entity =>
        {
            entity.ToTable("consulta");

            // Chave primária
            entity.HasKey(e => e.IdConsulta)
                .HasName("consulta_pkey");

            // Propriedades e configuração da chave primária com geração automática
            entity.Property(e => e.IdConsulta)
                .HasColumnName("idconsulta")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PacienteCpfPaci)
                .HasColumnName("paciente_cpfpaci")
                .HasMaxLength(14)
                .IsRequired();

            entity.Property(e => e.AtendenteCtpsatend)
                .HasColumnName("atendente_ctpsatend")
                .HasMaxLength(15)
                .IsRequired();

            entity.Property(e => e.MedicoCrmmed)
                .HasColumnName("medico_crmmed")
                .HasMaxLength(7)
                .IsRequired();

            entity.Property(e => e.UnidadeIdunidade)
                .HasColumnName("unidade_idunidade")
                .IsRequired();

            entity.Property(e => e.EspecialidadeIdespecialidade)
                .HasColumnName("especialidade_idespecialidade")
                .IsRequired();

            entity.Property(e => e.DataConsul)
                .HasColumnName("dataconsul")
                .IsRequired();

            entity.Property(e => e.HoraConsul)
                .HasColumnName("horaconsul")
                .IsRequired();

            entity.Property(e => e.StatusConsul)
                .HasColumnName("statusconsul")
                .HasMaxLength(15)
                .IsRequired();

            // Configuração das chaves estrangeiras
            entity.HasOne<Paciente>()
                .WithMany()
                .HasForeignKey(e => e.PacienteCpfPaci)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne<Atendente>()
                .WithMany()
                .HasForeignKey(e => e.AtendenteCtpsatend)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne<Medico>()
                .WithMany()
                .HasForeignKey(e => e.MedicoCrmmed)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne<Unidade>()
                .WithMany()
                .HasForeignKey(e => e.UnidadeIdunidade)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne<Especialidade>()
                .WithMany()
                .HasForeignKey(e => e.EspecialidadeIdespecialidade)
                .OnDelete(DeleteBehavior.NoAction);
        });

            modelBuilder.Entity<MedicoEspecialidade>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("medicosespecialidades");
        });
        
            modelBuilder.Entity<Unidade>(entity =>
            {
                entity.ToTable("unidade");

                entity.HasKey(e => e.IdUnidade)
                      .HasName("unidade_pkey");

                entity.Property(e => e.IdUnidade)
                      .HasColumnName("idunidade")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.NomeUnidade)
                      .HasColumnName("nomeunidade")
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.CepUni)
                      .HasColumnName("cepuni")
                      .HasMaxLength(10)
                      .IsRequired();

                entity.Property(e => e.LogradouroUni)
                      .HasColumnName("logradourouni")
                      .HasMaxLength(45)
                      .IsRequired();

                entity.Property(e => e.NumeroUni)
                      .HasColumnName("numerouni")
                      .HasMaxLength(6)
                      .IsRequired();

                entity.Property(e => e.BairroUni)
                      .HasColumnName("bairrouni")
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.CidadeUni)
                      .HasColumnName("cidadeuni")
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.EstadoUni)
                      .HasColumnName("estadouni")
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.PaisUni)
                      .HasColumnName("paisuni")
                      .HasMaxLength(20)
                      .IsRequired();
            });

        // Configuração para a View ConsultaPaciente
        modelBuilder.Entity<ConsultaPaciente>(entity =>
        {
            // Define a entidade como uma view e não uma tabela de dados
            entity.HasNoKey();
            entity.ToView("consultapaciente");
        });

        modelBuilder.Entity<Contribuinte>(entity =>
        {
            entity.HasNoKey();  // Indica que não há chave primária
            entity.ToView("Contribuintes");  // Nome da view no banco de dados

            // Verifique se o mapeamento está correto, por exemplo:
            entity.Property(e => e.ConsultorioOuGuiche)
                .HasColumnType("double precision");  // Define explicitamente o tipo da coluna
        });



        modelBuilder.Entity<ConsultaDetalhada>().HasNoKey();

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
    }

    internal object SetFromSqlRaw(string v)
    {
        throw new NotImplementedException();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}