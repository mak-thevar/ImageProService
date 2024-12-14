using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageProService.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImageMetadatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OriginalFileName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    BlobFileName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    ThumbnailBlobFileName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Url = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Width = table.Column<int>(type: "INTEGER", nullable: false),
                    Height = table.Column<int>(type: "INTEGER", nullable: false),
                    Format = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    FileSizeBytes = table.Column<long>(type: "INTEGER", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    AIInsights = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    AnalysisStatus = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageMetadatas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageMetadatas_BlobFileName",
                table: "ImageMetadatas",
                column: "BlobFileName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageMetadatas");
        }
    }
}
