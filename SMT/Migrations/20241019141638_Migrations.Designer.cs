﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SMT.Models;

#nullable disable

namespace SMT.Migrations
{
    [DbContext(typeof(ContextoBD))]
    [Migration("20241019141638_Migrations")]
    partial class Migrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "dispodia", new[] { "Sim", "Não" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "genero", new[] { "Masculino", "Feminino", "Outro" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "statusconsulta", new[] { "Pendente", "Andamento", "Concluida", "Cancelada" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Disponibilidade", b =>
                {
                    b.Property<string>("MedicoCrmmed")
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)")
                        .HasColumnName("medico_crmmed");

                    b.Property<int>("UnidadeIdunidade")
                        .HasColumnType("integer")
                        .HasColumnName("unidade_idunidade");

                    b.HasKey("MedicoCrmmed", "UnidadeIdunidade")
                        .HasName("disponibilidade_pkey");

                    b.HasIndex("UnidadeIdunidade");

                    b.ToTable("disponibilidade", (string)null);
                });

            modelBuilder.Entity("Paciente", b =>
                {
                    b.Property<string>("CpfPaci")
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)")
                        .HasColumnName("cpfpaci");

                    b.Property<string>("EmailPaci")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)")
                        .HasColumnName("emailpaci");

                    b.Property<DateOnly>("NascimentoPaci")
                        .HasColumnType("date")
                        .HasColumnName("nascimentopaci");

                    b.Property<string>("NomePaci")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)")
                        .HasColumnName("nomepaci");

                    b.Property<string>("SenhaPaci")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("senhapaci");

                    b.Property<string>("SobrenomePaci")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)")
                        .HasColumnName("sobrenomepaci");

                    b.Property<string>("TelefonePaci")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)")
                        .HasColumnName("telefonepaci");

                    b.Property<string>("generoPaci")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("genero");

                    b.HasKey("CpfPaci")
                        .HasName("paciente_pkey");

                    b.ToTable("paciente", (string)null);
                });

            modelBuilder.Entity("RelMedEspec", b =>
                {
                    b.Property<string>("MedicoCrmmed")
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)")
                        .HasColumnName("medico_crmmed");

                    b.Property<int>("EspecialidadeIdespecialidade")
                        .HasColumnType("integer")
                        .HasColumnName("especialidade_idespecialidade");

                    b.HasKey("MedicoCrmmed", "EspecialidadeIdespecialidade")
                        .HasName("rel_med_espec_pkey");

                    b.HasIndex("EspecialidadeIdespecialidade");

                    b.ToTable("rel_med_espec", (string)null);
                });

            modelBuilder.Entity("RelMedUnidade", b =>
                {
                    b.Property<string>("MedicoCrmmed")
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)")
                        .HasColumnName("medico_crmmed");

                    b.Property<int>("UnidadeIdunidade")
                        .HasColumnType("integer")
                        .HasColumnName("unidade_idunidade");

                    b.HasKey("MedicoCrmmed", "UnidadeIdunidade")
                        .HasName("rel_med_unidade_pkey");

                    b.HasIndex("UnidadeIdunidade");

                    b.ToTable("rel_med_unidade", (string)null);
                });

            modelBuilder.Entity("RelUniEspec", b =>
                {
                    b.Property<int>("UnidadeIdunidade")
                        .HasColumnType("integer")
                        .HasColumnName("unidade_idunidade");

                    b.Property<int>("EspecialidadeIdespecialidade")
                        .HasColumnType("integer")
                        .HasColumnName("especialidade_idespecialidade");

                    b.HasKey("UnidadeIdunidade", "EspecialidadeIdespecialidade")
                        .HasName("rel_uni_espec_pkey");

                    b.HasIndex("EspecialidadeIdespecialidade");

                    b.ToTable("rel_uni_espec", (string)null);
                });

            modelBuilder.Entity("SMT.Models.Administrador", b =>
                {
                    b.Property<int>("Idadministrador")
                        .HasColumnType("integer")
                        .HasColumnName("idadministrador");

                    b.Property<int>("UnidadeIdunidade")
                        .HasColumnType("integer")
                        .HasColumnName("unidade_idunidade");

                    b.Property<string>("Nomeadm")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("nomeadm");

                    b.Property<string>("Senhaadm")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("senhaadm");

                    b.Property<string>("Sobrenomeadm")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("sobrenomeadm");

                    b.HasKey("Idadministrador", "UnidadeIdunidade")
                        .HasName("administrador_pkey");

                    b.HasIndex("UnidadeIdunidade");

                    b.ToTable("administrador", (string)null);
                });

            modelBuilder.Entity("SMT.Models.Atendente", b =>
                {
                    b.Property<string>("Ctpsatend")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("ctpsatend");

                    b.Property<TimeOnly>("Fimturnoatend")
                        .HasColumnType("time without time zone")
                        .HasColumnName("fimturnoatend");

                    b.Property<TimeOnly>("Inicioturnoatend")
                        .HasColumnType("time without time zone")
                        .HasColumnName("inicioturnoatend");

                    b.Property<string>("Nomeatend")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)")
                        .HasColumnName("nomeatend");

                    b.Property<string>("Senhaatend")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("senhaatend");

                    b.Property<string>("Sobrenomeatend")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)")
                        .HasColumnName("sobrenomeatend");

                    b.HasKey("Ctpsatend")
                        .HasName("atendente_pkey");

                    b.ToTable("atendente", (string)null);
                });

            modelBuilder.Entity("SMT.Models.Consultum", b =>
                {
                    b.Property<int>("Idconsulta")
                        .HasColumnType("integer")
                        .HasColumnName("idconsulta");

                    b.Property<string>("PacienteCpfpaci")
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)")
                        .HasColumnName("paciente_cpfpaci");

                    b.Property<string>("AtendenteCtpsatend")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("atendente_ctpsatend");

                    b.Property<string>("MedicoCrmmed")
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)")
                        .HasColumnName("medico_crmmed");

                    b.Property<int>("UnidadeIdunidade")
                        .HasColumnType("integer")
                        .HasColumnName("unidade_idunidade");

                    b.Property<int>("EspecialidadeIdespecialidade")
                        .HasColumnType("integer")
                        .HasColumnName("especialidade_idespecialidade");

                    b.Property<DateOnly>("Dataconsul")
                        .HasColumnType("date")
                        .HasColumnName("dataconsul");

                    b.Property<TimeOnly>("Horaconsul")
                        .HasColumnType("time without time zone")
                        .HasColumnName("horaconsul");

                    b.Property<int>("statusconsul")
                        .HasColumnType("integer");

                    b.HasKey("Idconsulta", "PacienteCpfpaci", "AtendenteCtpsatend", "MedicoCrmmed", "UnidadeIdunidade", "EspecialidadeIdespecialidade")
                        .HasName("consulta_pkey");

                    b.HasIndex("AtendenteCtpsatend");

                    b.HasIndex("EspecialidadeIdespecialidade");

                    b.HasIndex("MedicoCrmmed");

                    b.HasIndex("PacienteCpfpaci");

                    b.HasIndex("UnidadeIdunidade");

                    b.ToTable("consulta", (string)null);
                });

            modelBuilder.Entity("SMT.Models.Especialidade", b =>
                {
                    b.Property<int>("Idespecialidade")
                        .HasColumnType("integer")
                        .HasColumnName("idespecialidade");

                    b.Property<int>("Nomeespec")
                        .HasColumnType("integer")
                        .HasColumnName("nomeespec");

                    b.HasKey("Idespecialidade")
                        .HasName("especialidade_pkey");

                    b.ToTable("especialidade", (string)null);
                });

            modelBuilder.Entity("SMT.Models.Medico", b =>
                {
                    b.Property<string>("Crmmed")
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)")
                        .HasColumnName("crmmed");

                    b.Property<string>("Emailmed")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)")
                        .HasColumnName("emailmed");

                    b.Property<string>("Nomemed")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)")
                        .HasColumnName("nomemed");

                    b.Property<string>("Senhamed")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("senhamed");

                    b.Property<string>("Sobrenomemed")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)")
                        .HasColumnName("sobrenomemed");

                    b.Property<string>("Telefonemed")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)")
                        .HasColumnName("telefonemed");

                    b.HasKey("Crmmed")
                        .HasName("medico_pkey");

                    b.ToTable("medico", (string)null);
                });

            modelBuilder.Entity("SMT.Models.Unidade", b =>
                {
                    b.Property<int>("Idunidade")
                        .HasColumnType("integer")
                        .HasColumnName("idunidade");

                    b.Property<string>("Bairrouni")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("bairrouni");

                    b.Property<string>("Cepuni")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("cepuni");

                    b.Property<string>("Cidadeuni")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("cidadeuni");

                    b.Property<string>("Estadouni")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("estadouni");

                    b.Property<string>("Logradourouni")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)")
                        .HasColumnName("logradourouni");

                    b.Property<string>("Nomeunidade")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("nomeunidade");

                    b.Property<string>("Numerouni")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("character varying(6)")
                        .HasColumnName("numerouni");

                    b.Property<string>("Paisuni")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("paisuni");

                    b.HasKey("Idunidade")
                        .HasName("unidade_pkey");

                    b.ToTable("unidade", (string)null);
                });

            modelBuilder.Entity("Disponibilidade", b =>
                {
                    b.HasOne("SMT.Models.Medico", null)
                        .WithMany()
                        .HasForeignKey("MedicoCrmmed")
                        .IsRequired()
                        .HasConstraintName("disponibilidade_medico_crmmed_fkey");

                    b.HasOne("SMT.Models.Unidade", null)
                        .WithMany()
                        .HasForeignKey("UnidadeIdunidade")
                        .IsRequired()
                        .HasConstraintName("disponibilidade_unidade_idunidade_fkey");
                });

            modelBuilder.Entity("RelMedEspec", b =>
                {
                    b.HasOne("SMT.Models.Especialidade", null)
                        .WithMany()
                        .HasForeignKey("EspecialidadeIdespecialidade")
                        .IsRequired()
                        .HasConstraintName("rel_med_espec_especialidade_idespecialidade_fkey");

                    b.HasOne("SMT.Models.Medico", null)
                        .WithMany()
                        .HasForeignKey("MedicoCrmmed")
                        .IsRequired()
                        .HasConstraintName("rel_med_espec_medico_crmmed_fkey");
                });

            modelBuilder.Entity("RelMedUnidade", b =>
                {
                    b.HasOne("SMT.Models.Medico", null)
                        .WithMany()
                        .HasForeignKey("MedicoCrmmed")
                        .IsRequired()
                        .HasConstraintName("rel_med_unidade_medico_crmmed_fkey");

                    b.HasOne("SMT.Models.Unidade", null)
                        .WithMany()
                        .HasForeignKey("UnidadeIdunidade")
                        .IsRequired()
                        .HasConstraintName("rel_med_unidade_unidade_idunidade_fkey");
                });

            modelBuilder.Entity("RelUniEspec", b =>
                {
                    b.HasOne("SMT.Models.Especialidade", null)
                        .WithMany()
                        .HasForeignKey("EspecialidadeIdespecialidade")
                        .IsRequired()
                        .HasConstraintName("rel_uni_espec_especialidade_idespecialidade_fkey");

                    b.HasOne("SMT.Models.Unidade", null)
                        .WithMany()
                        .HasForeignKey("UnidadeIdunidade")
                        .IsRequired()
                        .HasConstraintName("rel_uni_espec_unidade_idunidade_fkey");
                });

            modelBuilder.Entity("SMT.Models.Administrador", b =>
                {
                    b.HasOne("SMT.Models.Unidade", "UnidadeIdunidadeNavigation")
                        .WithMany("Administradors")
                        .HasForeignKey("UnidadeIdunidade")
                        .IsRequired()
                        .HasConstraintName("administrador_unidade_idunidade_fkey");

                    b.Navigation("UnidadeIdunidadeNavigation");
                });

            modelBuilder.Entity("SMT.Models.Consultum", b =>
                {
                    b.HasOne("SMT.Models.Atendente", "AtendenteCtpsatendNavigation")
                        .WithMany("Consulta")
                        .HasForeignKey("AtendenteCtpsatend")
                        .IsRequired()
                        .HasConstraintName("consulta_atendente_ctpsatend_fkey");

                    b.HasOne("SMT.Models.Especialidade", "EspecialidadeIdespecialidadeNavigation")
                        .WithMany("Consulta")
                        .HasForeignKey("EspecialidadeIdespecialidade")
                        .IsRequired()
                        .HasConstraintName("consulta_especialidade_idespecialidade_fkey");

                    b.HasOne("SMT.Models.Medico", "MedicoCrmmedNavigation")
                        .WithMany("Consulta")
                        .HasForeignKey("MedicoCrmmed")
                        .IsRequired()
                        .HasConstraintName("consulta_medico_crmmed_fkey");

                    b.HasOne("Paciente", "PacienteCpfpaciNavigation")
                        .WithMany("Consulta")
                        .HasForeignKey("PacienteCpfpaci")
                        .IsRequired()
                        .HasConstraintName("consulta_paciente_cpfpaci_fkey");

                    b.HasOne("SMT.Models.Unidade", "UnidadeIdunidadeNavigation")
                        .WithMany("Consulta")
                        .HasForeignKey("UnidadeIdunidade")
                        .IsRequired()
                        .HasConstraintName("consulta_unidade_idunidade_fkey");

                    b.Navigation("AtendenteCtpsatendNavigation");

                    b.Navigation("EspecialidadeIdespecialidadeNavigation");

                    b.Navigation("MedicoCrmmedNavigation");

                    b.Navigation("PacienteCpfpaciNavigation");

                    b.Navigation("UnidadeIdunidadeNavigation");
                });

            modelBuilder.Entity("Paciente", b =>
                {
                    b.Navigation("Consulta");
                });

            modelBuilder.Entity("SMT.Models.Atendente", b =>
                {
                    b.Navigation("Consulta");
                });

            modelBuilder.Entity("SMT.Models.Especialidade", b =>
                {
                    b.Navigation("Consulta");
                });

            modelBuilder.Entity("SMT.Models.Medico", b =>
                {
                    b.Navigation("Consulta");
                });

            modelBuilder.Entity("SMT.Models.Unidade", b =>
                {
                    b.Navigation("Administradors");

                    b.Navigation("Consulta");
                });
#pragma warning restore 612, 618
        }
    }
}