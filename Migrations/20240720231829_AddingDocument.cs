using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smERP.Migrations
{
    /// <inheritdoc />
    public partial class AddingDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SensitiveDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EncryptedContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UploadedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensitiveDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensitiveDocuments_Employees_UploadedById",
                        column: x => x.UploadedById,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionDocuments",
                columns: table => new
                {
                    SensitiveDocumentId = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDocuments", x => new { x.TransactionId, x.SensitiveDocumentId });
                    table.ForeignKey(
                        name: "FK_TransactionDocuments_BaseTransactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "BaseTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionDocuments_SensitiveDocuments_SensitiveDocumentId",
                        column: x => x.SensitiveDocumentId,
                        principalTable: "SensitiveDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SensitiveDocuments_UploadedById",
                table: "SensitiveDocuments",
                column: "UploadedById");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDocuments_SensitiveDocumentId",
                table: "TransactionDocuments",
                column: "SensitiveDocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionDocuments");

            migrationBuilder.DropTable(
                name: "SensitiveDocuments");
        }
    }
}
