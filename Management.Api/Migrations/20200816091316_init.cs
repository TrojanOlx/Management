using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Management.Api.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CreateUser = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    LastUpdateUser = table.Column<string>(nullable: true),
                    LastUpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoodsExtendFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    DateType = table.Column<int>(nullable: false),
                    Hint = table.Column<string>(nullable: true),
                    CreateUser = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    LastUpdateUser = table.Column<string>(nullable: true),
                    LastUpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsExtendFields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoodsExtendFieldTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GoodsExtendFields = table.Column<string>(nullable: true),
                    CreateUser = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    LastUpdateUser = table.Column<string>(nullable: true),
                    LastUpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsExtendFieldTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoodsImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GoodsId = table.Column<long>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    LocalName = table.Column<string>(nullable: true),
                    CreateUser = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsImages_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsExtendAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GoodsId = table.Column<long>(nullable: false),
                    GoodsExtendFieldId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsExtendAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsExtendAttributes_GoodsExtendFields_GoodsExtendFieldId",
                        column: x => x.GoodsExtendFieldId,
                        principalTable: "GoodsExtendFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsExtendAttributes_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoodsExtendAttributes_GoodsExtendFieldId",
                table: "GoodsExtendAttributes",
                column: "GoodsExtendFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsExtendAttributes_GoodsId",
                table: "GoodsExtendAttributes",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsImages_GoodsId",
                table: "GoodsImages",
                column: "GoodsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodsExtendAttributes");

            migrationBuilder.DropTable(
                name: "GoodsExtendFieldTemplates");

            migrationBuilder.DropTable(
                name: "GoodsImages");

            migrationBuilder.DropTable(
                name: "GoodsExtendFields");

            migrationBuilder.DropTable(
                name: "Goods");
        }
    }
}
