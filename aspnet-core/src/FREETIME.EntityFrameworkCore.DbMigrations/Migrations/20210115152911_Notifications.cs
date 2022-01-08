using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FREETIME.Migrations
{
    public partial class Notifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbpDzNotificationInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: true),
                    RecipientId = table.Column<Guid>(type: "uuid", nullable: true),
                    RecipientRoleId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExpireAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DetailUrl = table.Column<string>(type: "character varying(96)", maxLength: 96, nullable: true),
                    DetailUrlType = table.Column<byte>(type: "smallint", nullable: true),
                    RecipientPermission = table.Column<string>(type: "character varying(96)", maxLength: 96, nullable: true),
                    NotificationName = table.Column<string>(type: "character varying(96)", maxLength: 96, nullable: false),
                    Data = table.Column<string>(type: "character varying(1048576)", maxLength: 1048576, nullable: true),
                    Content = table.Column<string>(type: "character varying(1048576)", maxLength: 1048576, nullable: true),
                    DataTypeName = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    EntityTypeName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    EntityTypeAssemblyQualifiedName = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    EntityId = table.Column<string>(type: "character varying(96)", maxLength: 96, nullable: true),
                    ExternalId = table.Column<string>(type: "character varying(96)", maxLength: 96, nullable: true),
                    Severity = table.Column<byte>(type: "smallint", nullable: false),
                    State = table.Column<byte>(type: "smallint", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpDzNotificationInfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpDzNotificationInfo_Code",
                table: "AbpDzNotificationInfo",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_AbpDzNotificationInfo_DataTypeName",
                table: "AbpDzNotificationInfo",
                column: "DataTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpDzNotificationInfo_EntityId",
                table: "AbpDzNotificationInfo",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpDzNotificationInfo_RecipientId",
                table: "AbpDzNotificationInfo",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpDzNotificationInfo_RecipientPermission",
                table: "AbpDzNotificationInfo",
                column: "RecipientPermission");

            migrationBuilder.CreateIndex(
                name: "IX_AbpDzNotificationInfo_RecipientRoleId",
                table: "AbpDzNotificationInfo",
                column: "RecipientRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpDzNotificationInfo_SenderId",
                table: "AbpDzNotificationInfo",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpDzNotificationInfo_Severity",
                table: "AbpDzNotificationInfo",
                column: "Severity");

            migrationBuilder.CreateIndex(
                name: "IX_AbpDzNotificationInfo_State",
                table: "AbpDzNotificationInfo",
                column: "State");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpDzNotificationInfo");
        }
    }
}
