using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace basilico.karol._5h.Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class primaversione : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prodotti",
                columns: table => new
                {
                    IdP = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeP = table.Column<string>(type: "TEXT", nullable: true),
                    Descrizione = table.Column<string>(type: "TEXT", nullable: true),
                    Prezzo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodotti", x => x.IdP);
                });

            migrationBuilder.CreateTable(
                name: "Utenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Cognome = table.Column<string>(type: "TEXT", nullable: true),
                    EMail = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    Indirizzo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utenti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ordini",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeProdotto = table.Column<string>(type: "TEXT", nullable: false),
                    Quantita = table.Column<int>(type: "INTEGER", nullable: false),
                    Prezzo = table.Column<decimal>(type: "TEXT", nullable: false),
                    DataOrdine = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UtenteEmail = table.Column<string>(type: "TEXT", nullable: false),
                    UtenteId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordini", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordini_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ordini_UtenteId",
                table: "Ordini",
                column: "UtenteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ordini");

            migrationBuilder.DropTable(
                name: "Prodotti");

            migrationBuilder.DropTable(
                name: "Utenti");
        }
    }
}
