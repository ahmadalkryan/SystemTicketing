using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class fist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DaviceCategories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    abbreviation = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaviceCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TicketsStatus",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketsStatus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    AttachementPath = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    updateDate = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    TicketStatusId = table.Column<int>(type: "int", nullable: false),
                    DeviceCtegoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_DaviceCategories_DeviceCtegoryID",
                        column: x => x.DeviceCtegoryID,
                        principalTable: "DaviceCategories",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Tickets_TicketsStatus_TicketStatusId",
                        column: x => x.TicketStatusId,
                        principalTable: "TicketsStatus",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Role_id = table.Column<string>(type: "nvarchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.Role_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_UserRole_Roles_Role_id",
                        column: x => x.Role_id,
                        principalTable: "Roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_UserRole_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Notifictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    sentAt = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    TicketID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifictions_Tickets_TicketID",
                        column: x => x.TicketID,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifictions_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "TicketsTrace",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    NewStatusID = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    TicketID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    TicketStatusId = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketsTrace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketsTrace_TicketsStatus_TicketStatusId",
                        column: x => x.TicketStatusId,
                        principalTable: "TicketsStatus",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_TicketsTrace_Tickets_TicketID",
                        column: x => x.TicketID,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketsTrace_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifictions_TicketID",
                table: "Notifictions",
                column: "TicketID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifictions_UserID",
                table: "Notifictions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_DeviceCtegoryID",
                table: "Tickets",
                column: "DeviceCtegoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketStatusId",
                table: "Tickets",
                column: "TicketStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketsTrace_TicketID",
                table: "TicketsTrace",
                column: "TicketID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketsTrace_TicketStatusId",
                table: "TicketsTrace",
                column: "TicketStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketsTrace_UserID",
                table: "TicketsTrace",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_user_id",
                table: "UserRole",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifictions");

            migrationBuilder.DropTable(
                name: "TicketsTrace");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "DaviceCategories");

            migrationBuilder.DropTable(
                name: "TicketsStatus");
        }
    }
}
