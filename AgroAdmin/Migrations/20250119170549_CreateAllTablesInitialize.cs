using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroAdmin.Migrations
{
    /// <inheritdoc />
    public partial class CreateAllTablesInitialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Katalogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Katalogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DesUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DesRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescribtionUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescribtionRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewPicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductOnes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DesUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DesRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TasirModdaUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TasirModdaRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KimyoviySinfiUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KimyoviySinfiRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreparatShakliUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreparatShakliRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QadogiUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QadogiRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductType = table.Column<int>(type: "int", nullable: false),
                    AdditionUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionRu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOnes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTwos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DesUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DesRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionUZ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SarfUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SarfRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductIcon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductTwoType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTwos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableOnes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EkinTuriUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EkinTuriRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BegonaQarshiUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BegonaQarshiRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SarfMeyoriUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SarfMeyoriRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirgaSarfUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirgaSarfRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Onlsum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductOneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableOnes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableOnes_ProductOnes_ProductOneId",
                        column: x => x.ProductOneId,
                        principalTable: "ProductOnes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TableTwos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameUz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Foiz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductTwoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableTwos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableTwos_ProductTwos_ProductTwoId",
                        column: x => x.ProductTwoId,
                        principalTable: "ProductTwos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableOnes_ProductOneId",
                table: "TableOnes",
                column: "ProductOneId");

            migrationBuilder.CreateIndex(
                name: "IX_TableTwos_ProductTwoId",
                table: "TableTwos",
                column: "ProductTwoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Katalogs");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "TableOnes");

            migrationBuilder.DropTable(
                name: "TableTwos");

            migrationBuilder.DropTable(
                name: "ProductOnes");

            migrationBuilder.DropTable(
                name: "ProductTwos");
        }
    }
}
