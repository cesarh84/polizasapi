using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace polizas_api.Migrations
{
    /// <inheritdoc />
    public partial class PolizaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Polizas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Polizas_UsuarioId",
                table: "Polizas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Polizas_AspNetUsers_UsuarioId",
                table: "Polizas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Polizas_AspNetUsers_UsuarioId",
                table: "Polizas");

            migrationBuilder.DropIndex(
                name: "IX_Polizas_UsuarioId",
                table: "Polizas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Polizas");
        }
    }
}
