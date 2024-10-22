using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMT.Migrations
{
    /// <inheritdoc />
    public partial class Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:dispodia", "Sim,Não")
                .Annotation("Npgsql:Enum:genero", "Masculino,Feminino,Outro")
                .Annotation("Npgsql:Enum:statusconsulta", "Pendente,Andamento,Concluida,Cancelada");

            migrationBuilder.CreateTable(
                name: "atendente",
                columns: table => new
                {
                    ctpsatend = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    nomeatend = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    sobrenomeatend = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    inicioturnoatend = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    fimturnoatend = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    senhaatend = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("atendente_pkey", x => x.ctpsatend);
                });

            migrationBuilder.CreateTable(
                name: "especialidade",
                columns: table => new
                {
                    idespecialidade = table.Column<int>(type: "integer", nullable: false),
                    nomeespec = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("especialidade_pkey", x => x.idespecialidade);
                });

            migrationBuilder.CreateTable(
                name: "medico",
                columns: table => new
                {
                    crmmed = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    nomemed = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    sobrenomemed = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    telefonemed = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    emailmed = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    senhamed = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("medico_pkey", x => x.crmmed);
                });

            migrationBuilder.CreateTable(
                name: "paciente",
                columns: table => new
                {
                    cpfpaci = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    nomepaci = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    sobrenomepaci = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    nascimentopaci = table.Column<DateOnly>(type: "date", nullable: false),
                    genero = table.Column<string>(type: "text", nullable: false),
                    emailpaci = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    telefonepaci = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    senhapaci = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("paciente_pkey", x => x.cpfpaci);
                });

            migrationBuilder.CreateTable(
                name: "unidade",
                columns: table => new
                {
                    idunidade = table.Column<int>(type: "integer", nullable: false),
                    nomeunidade = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    cepuni = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    logradourouni = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    numerouni = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    bairrouni = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    cidadeuni = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    estadouni = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    paisuni = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("unidade_pkey", x => x.idunidade);
                });

            migrationBuilder.CreateTable(
                name: "rel_med_espec",
                columns: table => new
                {
                    medico_crmmed = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    especialidade_idespecialidade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("rel_med_espec_pkey", x => new { x.medico_crmmed, x.especialidade_idespecialidade });
                    table.ForeignKey(
                        name: "rel_med_espec_especialidade_idespecialidade_fkey",
                        column: x => x.especialidade_idespecialidade,
                        principalTable: "especialidade",
                        principalColumn: "idespecialidade");
                    table.ForeignKey(
                        name: "rel_med_espec_medico_crmmed_fkey",
                        column: x => x.medico_crmmed,
                        principalTable: "medico",
                        principalColumn: "crmmed");
                });

            migrationBuilder.CreateTable(
                name: "administrador",
                columns: table => new
                {
                    idadministrador = table.Column<int>(type: "integer", nullable: false),
                    unidade_idunidade = table.Column<int>(type: "integer", nullable: false),
                    nomeadm = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    sobrenomeadm = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    senhaadm = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("administrador_pkey", x => new { x.idadministrador, x.unidade_idunidade });
                    table.ForeignKey(
                        name: "administrador_unidade_idunidade_fkey",
                        column: x => x.unidade_idunidade,
                        principalTable: "unidade",
                        principalColumn: "idunidade");
                });

            migrationBuilder.CreateTable(
                name: "consulta",
                columns: table => new
                {
                    idconsulta = table.Column<int>(type: "integer", nullable: false),
                    paciente_cpfpaci = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    atendente_ctpsatend = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    medico_crmmed = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    unidade_idunidade = table.Column<int>(type: "integer", nullable: false),
                    especialidade_idespecialidade = table.Column<int>(type: "integer", nullable: false),
                    dataconsul = table.Column<DateOnly>(type: "date", nullable: false),
                    horaconsul = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    statusconsul = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("consulta_pkey", x => new { x.idconsulta, x.paciente_cpfpaci, x.atendente_ctpsatend, x.medico_crmmed, x.unidade_idunidade, x.especialidade_idespecialidade });
                    table.ForeignKey(
                        name: "consulta_atendente_ctpsatend_fkey",
                        column: x => x.atendente_ctpsatend,
                        principalTable: "atendente",
                        principalColumn: "ctpsatend");
                    table.ForeignKey(
                        name: "consulta_especialidade_idespecialidade_fkey",
                        column: x => x.especialidade_idespecialidade,
                        principalTable: "especialidade",
                        principalColumn: "idespecialidade");
                    table.ForeignKey(
                        name: "consulta_medico_crmmed_fkey",
                        column: x => x.medico_crmmed,
                        principalTable: "medico",
                        principalColumn: "crmmed");
                    table.ForeignKey(
                        name: "consulta_paciente_cpfpaci_fkey",
                        column: x => x.paciente_cpfpaci,
                        principalTable: "paciente",
                        principalColumn: "cpfpaci");
                    table.ForeignKey(
                        name: "consulta_unidade_idunidade_fkey",
                        column: x => x.unidade_idunidade,
                        principalTable: "unidade",
                        principalColumn: "idunidade");
                });

            migrationBuilder.CreateTable(
                name: "disponibilidade",
                columns: table => new
                {
                    medico_crmmed = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    unidade_idunidade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("disponibilidade_pkey", x => new { x.medico_crmmed, x.unidade_idunidade });
                    table.ForeignKey(
                        name: "disponibilidade_medico_crmmed_fkey",
                        column: x => x.medico_crmmed,
                        principalTable: "medico",
                        principalColumn: "crmmed");
                    table.ForeignKey(
                        name: "disponibilidade_unidade_idunidade_fkey",
                        column: x => x.unidade_idunidade,
                        principalTable: "unidade",
                        principalColumn: "idunidade");
                });

            migrationBuilder.CreateTable(
                name: "rel_med_unidade",
                columns: table => new
                {
                    medico_crmmed = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    unidade_idunidade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("rel_med_unidade_pkey", x => new { x.medico_crmmed, x.unidade_idunidade });
                    table.ForeignKey(
                        name: "rel_med_unidade_medico_crmmed_fkey",
                        column: x => x.medico_crmmed,
                        principalTable: "medico",
                        principalColumn: "crmmed");
                    table.ForeignKey(
                        name: "rel_med_unidade_unidade_idunidade_fkey",
                        column: x => x.unidade_idunidade,
                        principalTable: "unidade",
                        principalColumn: "idunidade");
                });

            migrationBuilder.CreateTable(
                name: "rel_uni_espec",
                columns: table => new
                {
                    unidade_idunidade = table.Column<int>(type: "integer", nullable: false),
                    especialidade_idespecialidade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("rel_uni_espec_pkey", x => new { x.unidade_idunidade, x.especialidade_idespecialidade });
                    table.ForeignKey(
                        name: "rel_uni_espec_especialidade_idespecialidade_fkey",
                        column: x => x.especialidade_idespecialidade,
                        principalTable: "especialidade",
                        principalColumn: "idespecialidade");
                    table.ForeignKey(
                        name: "rel_uni_espec_unidade_idunidade_fkey",
                        column: x => x.unidade_idunidade,
                        principalTable: "unidade",
                        principalColumn: "idunidade");
                });

            migrationBuilder.CreateIndex(
                name: "IX_administrador_unidade_idunidade",
                table: "administrador",
                column: "unidade_idunidade");

            migrationBuilder.CreateIndex(
                name: "IX_consulta_atendente_ctpsatend",
                table: "consulta",
                column: "atendente_ctpsatend");

            migrationBuilder.CreateIndex(
                name: "IX_consulta_especialidade_idespecialidade",
                table: "consulta",
                column: "especialidade_idespecialidade");

            migrationBuilder.CreateIndex(
                name: "IX_consulta_medico_crmmed",
                table: "consulta",
                column: "medico_crmmed");

            migrationBuilder.CreateIndex(
                name: "IX_consulta_paciente_cpfpaci",
                table: "consulta",
                column: "paciente_cpfpaci");

            migrationBuilder.CreateIndex(
                name: "IX_consulta_unidade_idunidade",
                table: "consulta",
                column: "unidade_idunidade");

            migrationBuilder.CreateIndex(
                name: "IX_disponibilidade_unidade_idunidade",
                table: "disponibilidade",
                column: "unidade_idunidade");

            migrationBuilder.CreateIndex(
                name: "IX_rel_med_espec_especialidade_idespecialidade",
                table: "rel_med_espec",
                column: "especialidade_idespecialidade");

            migrationBuilder.CreateIndex(
                name: "IX_rel_med_unidade_unidade_idunidade",
                table: "rel_med_unidade",
                column: "unidade_idunidade");

            migrationBuilder.CreateIndex(
                name: "IX_rel_uni_espec_especialidade_idespecialidade",
                table: "rel_uni_espec",
                column: "especialidade_idespecialidade");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "administrador");

            migrationBuilder.DropTable(
                name: "consulta");

            migrationBuilder.DropTable(
                name: "disponibilidade");

            migrationBuilder.DropTable(
                name: "rel_med_espec");

            migrationBuilder.DropTable(
                name: "rel_med_unidade");

            migrationBuilder.DropTable(
                name: "rel_uni_espec");

            migrationBuilder.DropTable(
                name: "atendente");

            migrationBuilder.DropTable(
                name: "paciente");

            migrationBuilder.DropTable(
                name: "medico");

            migrationBuilder.DropTable(
                name: "especialidade");

            migrationBuilder.DropTable(
                name: "unidade");
        }
    }
}
