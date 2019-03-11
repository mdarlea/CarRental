using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Type = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    TransmissionType = table.Column<string>(nullable: false),
                    NumberOfDoors = table.Column<int>(nullable: false),
                    NumberOfBags = table.Column<int>(nullable: false),
                    NumberOfSeats = table.Column<int>(nullable: false),
                    HasPetrol = table.Column<short>(nullable: false),
                    HasDiesel = table.Column<short>(nullable: false),
                    HasAirConditioning = table.Column<short>(nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 300, nullable: true),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_CarTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "CarTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvailableCars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CarId = table.Column<int>(nullable: false),
                    NumberOfCars = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableCars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvailableCars_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    AvailableCarId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(maxLength: 250, nullable: false),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_AvailableCars_AvailableCarId",
                        column: x => x.AvailableCarId,
                        principalTable: "AvailableCars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_aspnetusers_UserId",
                        column: x => x.UserId,
                        principalTable: "aspnetusers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarTypes",
                columns: new[] { "Id", "Type" },
                values: new object[] { 1, "Small" });

            migrationBuilder.InsertData(
                table: "CarTypes",
                columns: new[] { "Id", "Type" },
                values: new object[] { 2, "Mid-Size" });

            migrationBuilder.InsertData(
                table: "CarTypes",
                columns: new[] { "Id", "Type" },
                values: new object[] { 3, "Large" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "HasAirConditioning", "HasDiesel", "HasPetrol", "ImageUrl", "Name", "NumberOfBags", "NumberOfDoors", "NumberOfSeats", "TransmissionType", "TypeId" },
                values: new object[,]
                {
                    { 1, (short)1, (short)0, (short)1, "assets/images/VolkswagenUp.png", "Volkswagen Up!", 1, 5, 4, "Manual", 1 },
                    { 2, (short)1, (short)1, (short)1, "assets/images/VolkswagenPolo.png", "Volkswagen Polo", 2, 5, 5, "Manual", 1 },
                    { 3, (short)1, (short)0, (short)1, "assets/images/RenaultClioAutomatic.png", "Renault Clio Automatic", 2, 5, 5, "Automatic", 1 },
                    { 4, (short)1, (short)1, (short)1, "assets/images/VolkswagenGolf.png", "Volkswagen Golf", 2, 5, 5, "Manual", 2 },
                    { 5, (short)1, (short)1, (short)1, "assets/images/VolkswagenGolfAutomatic.png", "Volkswagen Golf Automatic", 2, 5, 5, "Automatic", 2 },
                    { 6, (short)1, (short)1, (short)1, "assets/images/DaciaDuster.png", "Dacia Duster 4x4", 3, 5, 5, "Manual", 2 },
                    { 7, (short)1, (short)1, (short)0, "assets/images/MercedesBenzC.png", "Mercedes-Benz C-Class Automatic", 2, 4, 5, "Automatic", 3 },
                    { 8, (short)1, (short)1, (short)0, "assets/images/MercedesBenzGLC.png", "Mercedes Benz GLC 220 Automatic Diesel", 3, 4, 5, "Automatic", 3 },
                    { 9, (short)1, (short)1, (short)0, "assets/images/MercedesBenzGLE.png", "Mercedes-Benz GLE 250 Automatic Diesel", 3, 5, 5, "Automatic", 3 }
                });

            migrationBuilder.InsertData(
                table: "AvailableCars",
                columns: new[] { "Id", "CarId", "NumberOfCars", "Price" },
                values: new object[,]
                {
                    { 1, 1, 10, 85.99m },
                    { 2, 2, 15, 87.99m },
                    { 3, 3, 5, 94.01m },
                    { 4, 4, 7, 112.00m },
                    { 5, 5, 12, 118.00m },
                    { 6, 6, 13, 135.99m },
                    { 7, 7, 5, 276.01m },
                    { 8, 8, 9, 308.00m },
                    { 9, 9, 4, 352.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvailableCars_CarId",
                table: "AvailableCars",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AvailableCarId",
                table: "Bookings",
                column: "AvailableCarId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_TypeId",
                table: "Cars",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "AvailableCars");

            migrationBuilder.DropTable(
                name: "aspnetusers");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "CarTypes");
        }
    }
}
