using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEventos.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RedeSocial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedesSociais_Eventos_EventoId",
                table: "RedesSociais");

            migrationBuilder.DropForeignKey(
                name: "FK_RedesSociais_Palestrantes_PalestranteId",
                table: "RedesSociais");

            migrationBuilder.AlterColumn<int>(
                name: "PalestranteId",
                table: "RedesSociais",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "EventoId",
                table: "RedesSociais",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_RedesSociais_Eventos_EventoId",
                table: "RedesSociais",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RedesSociais_Palestrantes_PalestranteId",
                table: "RedesSociais",
                column: "PalestranteId",
                principalTable: "Palestrantes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedesSociais_Eventos_EventoId",
                table: "RedesSociais");

            migrationBuilder.DropForeignKey(
                name: "FK_RedesSociais_Palestrantes_PalestranteId",
                table: "RedesSociais");

            migrationBuilder.AlterColumn<int>(
                name: "PalestranteId",
                table: "RedesSociais",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventoId",
                table: "RedesSociais",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RedesSociais_Eventos_EventoId",
                table: "RedesSociais",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RedesSociais_Palestrantes_PalestranteId",
                table: "RedesSociais",
                column: "PalestranteId",
                principalTable: "Palestrantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
