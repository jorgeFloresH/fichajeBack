using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace apiServices.Migrations
{
    public partial class PrimeraMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "agencia",
                columns: table => new
                {
                    id_agencia = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nom_agencia = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    estado = table.Column<int>(type: "integer", nullable: true),
                    acdes = table.Column<int>(type: "integer", nullable: true),
                    mapa = table.Column<int>(type: "integer", nullable: true),
                    multimedia = table.Column<int>(type: "integer", nullable: true),
                    consulta = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("agencia_pkey", x => x.id_agencia);
                });

            migrationBuilder.CreateTable(
                name: "observaciones",
                columns: table => new
                {
                    id_observaciones = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nom_observaciones = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    estado = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("observaciones_pkey", x => x.id_observaciones);
                });

            migrationBuilder.CreateTable(
                name: "prioridad",
                columns: table => new
                {
                    id_prioridad = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    tipo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    rango = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("prioridad_pkey", x => x.id_prioridad);
                });

            migrationBuilder.CreateTable(
                name: "requisitos",
                columns: table => new
                {
                    id_requisitos = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nom_requisitos = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    estado = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("requisitos_pkey", x => x.id_requisitos);
                });

            migrationBuilder.CreateTable(
                name: "tipo_pantalla",
                columns: table => new
                {
                    id_tipo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nom_tipo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    id_agencia = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tipo_pantalla_pkey", x => x.id_tipo);
                });

            migrationBuilder.CreateTable(
                name: "tipo_perfil",
                columns: table => new
                {
                    id_perfil = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nom_tipo_p = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    estado = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tipo_perfil_pkey", x => x.id_perfil);
                });

            migrationBuilder.CreateTable(
                name: "multimedia",
                columns: table => new
                {
                    id_multi = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nom_video = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<int>(type: "integer", nullable: true),
                    tipo = table.Column<string>(type: "text", nullable: true),
                    ruta = table.Column<string>(type: "text", nullable: true),
                    id_agencia = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("multimedia_pkey", x => x.id_multi);
                    table.ForeignKey(
                        name: "id_agencia",
                        column: x => x.id_agencia,
                        principalTable: "agencia",
                        principalColumn: "id_agencia");
                });

            migrationBuilder.CreateTable(
                name: "tramites",
                columns: table => new
                {
                    id_tramite = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    id_agencia = table.Column<long>(type: "bigint", nullable: true),
                    nom_tramite = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    estado = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tramites_pkey", x => x.id_tramite);
                    table.ForeignKey(
                        name: "id_agencia",
                        column: x => x.id_agencia,
                        principalTable: "agencia",
                        principalColumn: "id_agencia");
                });

            migrationBuilder.CreateTable(
                name: "ventanilla",
                columns: table => new
                {
                    id_ventanilla = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nom_ventanilla = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    estado_v = table.Column<int>(type: "integer", nullable: true),
                    id_agencia = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ventanilla_pkey", x => x.id_ventanilla);
                    table.ForeignKey(
                        name: "id_agencia",
                        column: x => x.id_agencia,
                        principalTable: "agencia",
                        principalColumn: "id_agencia");
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id_usuario = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    user_name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    user_password = table.Column<string>(type: "character(64)", fixedLength: true, maxLength: 64, nullable: true),
                    id_perfil = table.Column<long>(type: "bigint", nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    id_agencia = table.Column<long>(type: "bigint", nullable: true),
                    estado = table.Column<int>(type: "integer", nullable: true),
                    nom_usuario = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ape_paterno = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ape_materno = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ci_usuario = table.Column<int>(type: "integer", nullable: true),
                    foto_perfil = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("usuario_pkey", x => x.id_usuario);
                    table.ForeignKey(
                        name: "id_agencia",
                        column: x => x.id_agencia,
                        principalTable: "agencia",
                        principalColumn: "id_agencia");
                    table.ForeignKey(
                        name: "id_perfil",
                        column: x => x.id_perfil,
                        principalTable: "tipo_perfil",
                        principalColumn: "id_perfil");
                });

            migrationBuilder.CreateTable(
                name: "pantalla_mul",
                columns: table => new
                {
                    id_pantalla_mul = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    id_multi = table.Column<long>(type: "bigint", nullable: true),
                    id_tipo = table.Column<long>(type: "bigint", nullable: true),
                    id_agencia = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pantalla_mul_pkey", x => x.id_pantalla_mul);
                    table.ForeignKey(
                        name: "id_agencia",
                        column: x => x.id_agencia,
                        principalTable: "agencia",
                        principalColumn: "id_agencia");
                    table.ForeignKey(
                        name: "id_multi",
                        column: x => x.id_multi,
                        principalTable: "multimedia",
                        principalColumn: "id_multi");
                    table.ForeignKey(
                        name: "id_tipo",
                        column: x => x.id_tipo,
                        principalTable: "tipo_pantalla",
                        principalColumn: "id_tipo");
                });

            migrationBuilder.CreateTable(
                name: "detalle_observacion",
                columns: table => new
                {
                    id_detalle_obs = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    id_observaciones = table.Column<long>(type: "bigint", nullable: true),
                    id_tramite = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("detalle_observacion_pkey", x => x.id_detalle_obs);
                    table.ForeignKey(
                        name: "id_observaciones",
                        column: x => x.id_observaciones,
                        principalTable: "observaciones",
                        principalColumn: "id_observaciones");
                    table.ForeignKey(
                        name: "id_tramite",
                        column: x => x.id_tramite,
                        principalTable: "tramites",
                        principalColumn: "id_tramite");
                });

            migrationBuilder.CreateTable(
                name: "requisito_tramite",
                columns: table => new
                {
                    id_requitram = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    id_requisitos = table.Column<long>(type: "bigint", nullable: true),
                    id_tramite = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("requisito_tramite_pkey", x => x.id_requitram);
                    table.ForeignKey(
                        name: "id_requisitos",
                        column: x => x.id_requisitos,
                        principalTable: "requisitos",
                        principalColumn: "id_requisitos");
                    table.ForeignKey(
                        name: "id_tramite",
                        column: x => x.id_tramite,
                        principalTable: "tramites",
                        principalColumn: "id_tramite");
                });

            migrationBuilder.CreateTable(
                name: "tra_ven",
                columns: table => new
                {
                    id_tran_ven = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    id_tramite = table.Column<long>(type: "bigint", nullable: true),
                    id_ventanilla = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tra_ven_pkey", x => x.id_tran_ven);
                    table.ForeignKey(
                        name: "id_tramite",
                        column: x => x.id_tramite,
                        principalTable: "tramites",
                        principalColumn: "id_tramite");
                    table.ForeignKey(
                        name: "id_ventanilla",
                        column: x => x.id_ventanilla,
                        principalTable: "ventanilla",
                        principalColumn: "id_ventanilla");
                });

            migrationBuilder.CreateTable(
                name: "contacto",
                columns: table => new
                {
                    id_contacto = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    correo = table.Column<string>(type: "text", nullable: true),
                    n_celular = table.Column<int>(type: "integer", nullable: true),
                    id_usuario = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("contacto_pkey", x => x.id_contacto);
                    table.ForeignKey(
                        name: "id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "historial",
                columns: table => new
                {
                    id_historial = table.Column<long>(type: "bigint", nullable: false),
                    id_usuario = table.Column<long>(type: "bigint", nullable: true),
                    ultimo_login = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    password = table.Column<string>(type: "character(64)", fixedLength: true, maxLength: 64, nullable: true),
                    estado_contraseña = table.Column<int>(type: "integer", nullable: true),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    id_agencia = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("historial_pkey", x => x.id_historial);
                    table.ForeignKey(
                        name: "id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    id_ticket = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    id_tramite = table.Column<long>(type: "bigint", nullable: true),
                    id_usuario = table.Column<long>(type: "bigint", nullable: true),
                    estado = table.Column<int>(type: "integer", nullable: true),
                    id_ventanilla = table.Column<long>(type: "bigint", nullable: true),
                    fecha_hora = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    hora_atencion = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    duracion_atencion = table.Column<int>(type: "integer", nullable: true),
                    id_agencia = table.Column<long>(type: "bigint", nullable: true),
                    n_ticket = table.Column<int>(type: "integer", nullable: true),
                    id_prioridad = table.Column<long>(type: "bigint", nullable: true),
                    derivacion = table.Column<int>(type: "integer", nullable: true),
                    hora_list = table.Column<TimeOnly>(type: "time without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ticket_pkey", x => x.id_ticket);
                    table.ForeignKey(
                        name: "id_agencia",
                        column: x => x.id_agencia,
                        principalTable: "agencia",
                        principalColumn: "id_agencia");
                    table.ForeignKey(
                        name: "id_prioridad",
                        column: x => x.id_prioridad,
                        principalTable: "prioridad",
                        principalColumn: "id_prioridad");
                    table.ForeignKey(
                        name: "id_tramite",
                        column: x => x.id_tramite,
                        principalTable: "tramites",
                        principalColumn: "id_tramite");
                    table.ForeignKey(
                        name: "id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario");
                    table.ForeignKey(
                        name: "id_ventanilla",
                        column: x => x.id_ventanilla,
                        principalTable: "ventanilla",
                        principalColumn: "id_ventanilla");
                });

            migrationBuilder.CreateTable(
                name: "ut_tramite",
                columns: table => new
                {
                    id_ut_tramite = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    id_usuario = table.Column<long>(type: "bigint", nullable: true),
                    id_tramite = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ut_tramite_pkey", x => x.id_ut_tramite);
                    table.ForeignKey(
                        name: "id_tramite",
                        column: x => x.id_tramite,
                        principalTable: "tramites",
                        principalColumn: "id_tramite");
                    table.ForeignKey(
                        name: "id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "ven_usuario",
                columns: table => new
                {
                    id_ven_usuario = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    id_ventanilla = table.Column<long>(type: "bigint", nullable: true),
                    id_usuario = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ven_usuario_pkey", x => x.id_ven_usuario);
                    table.ForeignKey(
                        name: "id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario");
                    table.ForeignKey(
                        name: "id_ventanilla",
                        column: x => x.id_ventanilla,
                        principalTable: "ventanilla",
                        principalColumn: "id_ventanilla");
                });

            migrationBuilder.CreateTable(
                name: "historial_derivacion",
                columns: table => new
                {
                    id_derivacion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    id_ticket = table.Column<long>(type: "bigint", nullable: true),
                    ventanilla_origen = table.Column<long>(type: "bigint", nullable: true),
                    ventanilla_destino = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("historial_derivacion_pkey", x => x.id_derivacion);
                    table.ForeignKey(
                        name: "id_ticket",
                        column: x => x.id_ticket,
                        principalTable: "ticket",
                        principalColumn: "id_ticket");
                });

            migrationBuilder.CreateTable(
                name: "user_ticket",
                columns: table => new
                {
                    id_user_ticket = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    id_ticket = table.Column<long>(type: "bigint", nullable: true),
                    id_usuario = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_ticket_pkey", x => x.id_user_ticket);
                    table.ForeignKey(
                        name: "id_ticket",
                        column: x => x.id_ticket,
                        principalTable: "ticket",
                        principalColumn: "id_ticket");
                    table.ForeignKey(
                        name: "id_usuarios",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_contacto_id_usuario",
                table: "contacto",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "fki_id_observaciones",
                table: "detalle_observacion",
                column: "id_observaciones");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_observacion_id_tramite",
                table: "detalle_observacion",
                column: "id_tramite");

            migrationBuilder.CreateIndex(
                name: "fki_id_usuario",
                table: "historial",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "fki_id_ticket",
                table: "historial_derivacion",
                column: "id_ticket");

            migrationBuilder.CreateIndex(
                name: "IX_multimedia_id_agencia",
                table: "multimedia",
                column: "id_agencia");

            migrationBuilder.CreateIndex(
                name: "fki_id_multi",
                table: "pantalla_mul",
                column: "id_multi");

            migrationBuilder.CreateIndex(
                name: "fki_id_pantalla",
                table: "pantalla_mul",
                column: "id_tipo");

            migrationBuilder.CreateIndex(
                name: "fki_id_tipo",
                table: "pantalla_mul",
                column: "id_tipo");

            migrationBuilder.CreateIndex(
                name: "IX_pantalla_mul_id_agencia",
                table: "pantalla_mul",
                column: "id_agencia");

            migrationBuilder.CreateIndex(
                name: "fki_id_requisitos",
                table: "requisito_tramite",
                column: "id_requisitos");

            migrationBuilder.CreateIndex(
                name: "IX_requisito_tramite_id_tramite",
                table: "requisito_tramite",
                column: "id_tramite");

            migrationBuilder.CreateIndex(
                name: "fki_id_agencia",
                table: "ticket",
                column: "id_agencia");

            migrationBuilder.CreateIndex(
                name: "fki_id_persona",
                table: "ticket",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "fki_id_prioridad",
                table: "ticket",
                column: "id_prioridad");

            migrationBuilder.CreateIndex(
                name: "fki_id_tramite",
                table: "ticket",
                column: "id_tramite");

            migrationBuilder.CreateIndex(
                name: "fki_id_ventanilla",
                table: "ticket",
                column: "id_ventanilla");

            migrationBuilder.CreateIndex(
                name: "IX_tra_ven_id_tramite",
                table: "tra_ven",
                column: "id_tramite");

            migrationBuilder.CreateIndex(
                name: "IX_tra_ven_id_ventanilla",
                table: "tra_ven",
                column: "id_ventanilla");

            migrationBuilder.CreateIndex(
                name: "IX_tramites_id_agencia",
                table: "tramites",
                column: "id_agencia");

            migrationBuilder.CreateIndex(
                name: "fki_id_usuarios",
                table: "user_ticket",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_user_ticket_id_ticket",
                table: "user_ticket",
                column: "id_ticket");

            migrationBuilder.CreateIndex(
                name: "fki_id_perfil",
                table: "usuario",
                column: "id_perfil");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_id_agencia",
                table: "usuario",
                column: "id_agencia");

            migrationBuilder.CreateIndex(
                name: "IX_ut_tramite_id_tramite",
                table: "ut_tramite",
                column: "id_tramite");

            migrationBuilder.CreateIndex(
                name: "IX_ut_tramite_id_usuario",
                table: "ut_tramite",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_ven_usuario_id_usuario",
                table: "ven_usuario",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_ven_usuario_id_ventanilla",
                table: "ven_usuario",
                column: "id_ventanilla");

            migrationBuilder.CreateIndex(
                name: "IX_ventanilla_id_agencia",
                table: "ventanilla",
                column: "id_agencia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contacto");

            migrationBuilder.DropTable(
                name: "detalle_observacion");

            migrationBuilder.DropTable(
                name: "historial");

            migrationBuilder.DropTable(
                name: "historial_derivacion");

            migrationBuilder.DropTable(
                name: "pantalla_mul");

            migrationBuilder.DropTable(
                name: "requisito_tramite");

            migrationBuilder.DropTable(
                name: "tra_ven");

            migrationBuilder.DropTable(
                name: "user_ticket");

            migrationBuilder.DropTable(
                name: "ut_tramite");

            migrationBuilder.DropTable(
                name: "ven_usuario");

            migrationBuilder.DropTable(
                name: "observaciones");

            migrationBuilder.DropTable(
                name: "multimedia");

            migrationBuilder.DropTable(
                name: "tipo_pantalla");

            migrationBuilder.DropTable(
                name: "requisitos");

            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "prioridad");

            migrationBuilder.DropTable(
                name: "tramites");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "ventanilla");

            migrationBuilder.DropTable(
                name: "tipo_perfil");

            migrationBuilder.DropTable(
                name: "agencia");
        }
    }
}
