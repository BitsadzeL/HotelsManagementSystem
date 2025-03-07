using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hotels.Repository.Migrations
{
    /// <inheritdoc />
    public partial class seedHotelsManagersRooms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "City", "Country", "Rating", "Title" },
                values: new object[,]
                {
                    { 1, "123 Plaza Ave", "Tbilisi", "Georgia", 4.5f, "Grand Plaza" },
                    { 2, "456 Ocean St", "Berlin", "Germany", 4.1f, "Luxury Inn" },
                    { 3, "789 Mountain Rd", "Barcelona", "Spain", 5f, "Barcelona Plaza" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "Email", "HotelId", "IdNumber", "Name", "PhoneNumber", "Surname" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", 1, "12345678901", "John", "+995123456789", "Doe" },
                    { 2, "jane.smith@example.com", 2, "98765432101", "Jane", "+995987654321", "Smith" },
                    { 3, "michael.jordan@example.com", 3, "19283746501", "Michael", "+995112233445", "Jordan" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "HotelId", "Price", "Title" },
                values: new object[,]
                {
                    { 1, 1, 150f, "Deluxe Room" },
                    { 2, 2, 100f, "Standard Room" },
                    { 3, 3, 250f, "Suite" },
                    { 4, 3, 500f, "Royal" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
