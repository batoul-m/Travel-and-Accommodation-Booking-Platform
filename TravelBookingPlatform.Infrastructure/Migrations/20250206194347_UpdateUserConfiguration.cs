using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBookingPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b0a3e9b6-2b47-4fbd-9a9d-5c7c07fbcf45"),
                column: "Password",
                value: "ALaU/k/AAn4cw05diU6Bb0Va6ZCQt2dDewGrngK3ez2i4TBtRCSEzIgStvbVeRZB8A==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b0a3e9b6-2b47-4fbd-9a9d-5c7c07fbcf45"),
                column: "Password",
                value: "$2a$12$N/d8BEFb3ogUsr/inJKZHOMnLXit5jWoIZlc60BRcGENb6QyZ2Fe6");
        }
    }
}
