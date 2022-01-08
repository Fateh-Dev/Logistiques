using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FREETIME.Migrations
{
    public partial class AbpDZ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbpDzCollection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ChildId = table.Column<string>(type: "character varying(96)", maxLength: 96, nullable: true),
                    ParrentId = table.Column<string>(type: "character varying(96)", maxLength: 96, nullable: true),
                    CollectionCode = table.Column<string>(type: "character varying(62)", maxLength: 62, nullable: true),
                    Value = table.Column<string>(type: "character varying(1048576)", maxLength: 1048576, nullable: true),
                    Data = table.Column<string>(type: "character varying(1048576)", maxLength: 1048576, nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpDzCollection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpDzEnum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ParrentId = table.Column<int>(type: "integer", nullable: true),
                    EntityType = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Display = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Group = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Category = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Description = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Abbreviation = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Url = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Value = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Data = table.Column<string>(type: "character varying(1048576)", maxLength: 1048576, nullable: true),
                    IsStatic = table.Column<bool>(type: "boolean", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    IsSelectable = table.Column<bool>(type: "boolean", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpDzEnum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpDzEnum_AbpDzEnum_ParrentId",
                        column: x => x.ParrentId,
                        principalTable: "AbpDzEnum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpDzCollection_ChildId",
                table: "AbpDzCollection",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpDzCollection_CollectionCode",
                table: "AbpDzCollection",
                column: "CollectionCode");

            migrationBuilder.CreateIndex(
                name: "IX_AbpDzCollection_ParrentId",
                table: "AbpDzCollection",
                column: "ParrentId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpDzEnum_Code",
                table: "AbpDzEnum",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_AbpDzEnum_ParrentId",
                table: "AbpDzEnum",
                column: "ParrentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpDzCollection");

            migrationBuilder.DropTable(
                name: "AbpDzEnum");
        }
    }
}
