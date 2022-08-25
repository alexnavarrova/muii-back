using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class MigrationAnimaUnicoCorral : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CorralAnimal",
                table: "CorralAnimal");

            migrationBuilder.DropIndex(
                name: "IX_CorralAnimal_AnimalId",
                table: "CorralAnimal");

            migrationBuilder.DropColumn(
                name: "CorralAnimalId",
                table: "CorralAnimal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CorralAnimal",
                table: "CorralAnimal",
                columns: new[] { "AnimalId", "CorralId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CorralAnimal",
                table: "CorralAnimal");

            migrationBuilder.AddColumn<Guid>(
                name: "CorralAnimalId",
                table: "CorralAnimal",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CorralAnimal",
                table: "CorralAnimal",
                column: "CorralAnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_CorralAnimal_AnimalId",
                table: "CorralAnimal",
                column: "AnimalId");
        }
    }
}
