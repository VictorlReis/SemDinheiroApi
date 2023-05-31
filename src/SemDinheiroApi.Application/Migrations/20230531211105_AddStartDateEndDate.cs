using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SemDinheiroapi.Migrations
{
    /// <inheritdoc />
    public partial class AddStartDateEndDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fixed",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Transactions",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Transactions",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Transactions",
                newName: "Date");

            migrationBuilder.AddColumn<bool>(
                name: "Fixed",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
