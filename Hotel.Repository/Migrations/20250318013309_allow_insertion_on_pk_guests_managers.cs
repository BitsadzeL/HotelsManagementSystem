using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotels.Repository.Migrations
{
    /// <inheritdoc />
    public partial class allow_insertion_on_pk_guests_managers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraint in Bookings table that references Guests
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Guests_GuestId",
                table: "Bookings");

            // Drop Primary Key constraints first
            migrationBuilder.DropPrimaryKey(name: "PK_Guests", table: "Guests");
            migrationBuilder.DropPrimaryKey(name: "PK_Managers", table: "Managers");

            // Drop the existing columns with IDENTITY
            migrationBuilder.DropColumn(name: "Id", table: "Guests");
            migrationBuilder.DropColumn(name: "Id", table: "Managers");

            // Recreate the Id column WITHOUT IDENTITY
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Guests",
                nullable: false
            );

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Managers",
                nullable: false
            );

            // Restore Primary Keys
            migrationBuilder.AddPrimaryKey(name: "PK_Guests", table: "Guests", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_Managers", table: "Managers", column: "Id");

            // Add the foreign key constraint back to the Bookings table
            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Guests_GuestId",
                table: "Bookings",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }



        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Managers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Guests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
