using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotels.Repository.Migrations
{
    /// <inheritdoc />
    public partial class HotelId_nullable_while_adding_manager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Hotels_HotelId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_HotelId",
                table: "Managers");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Managers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_HotelId",
                table: "Managers",
                column: "HotelId",
                unique: true,
                filter: "[HotelId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Hotels_HotelId",
                table: "Managers",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Hotels_HotelId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_HotelId",
                table: "Managers");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Managers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Managers_HotelId",
                table: "Managers",
                column: "HotelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Hotels_HotelId",
                table: "Managers",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
