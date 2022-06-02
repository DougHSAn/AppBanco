using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppBanco.Migrations
{
    public partial class inicia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    NumAccount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.NumAccount);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    IDTransaction = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionValue = table.Column<int>(type: "int", nullable: false),
                    IDSourceAccount = table.Column<int>(type: "int", nullable: false),
                    IDTargetAccount = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.IDTransaction);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumAccount1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Accounts_NumAccount1",
                        column: x => x.NumAccount1,
                        principalTable: "Accounts",
                        principalColumn: "NumAccount",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccTran",
                columns: table => new
                {
                    IDTransaction = table.Column<int>(type: "int", nullable: false),
                    NumAccount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccTran", x => new { x.NumAccount, x.IDTransaction });
                    table.ForeignKey(
                        name: "FK_AccTran_Accounts_NumAccount",
                        column: x => x.NumAccount,
                        principalTable: "Accounts",
                        principalColumn: "NumAccount",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccTran_Transactions_IDTransaction",
                        column: x => x.IDTransaction,
                        principalTable: "Transactions",
                        principalColumn: "IDTransaction",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccTran_IDTransaction",
                table: "AccTran",
                column: "IDTransaction");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NumAccount1",
                table: "Users",
                column: "NumAccount1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccTran");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
