using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MailSender.Data.Migrations
{
    /// <inheritdoc />
    public partial class SchedulerTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchedulerTaskId",
                table: "Recipients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SchedulerTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServerId = table.Column<int>(type: "int", nullable: true),
                    SenderId = table.Column<int>(type: "int", nullable: true),
                    MessageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulerTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchedulerTasks_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SchedulerTasks_Senders_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Senders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SchedulerTasks_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_SchedulerTaskId",
                table: "Recipients",
                column: "SchedulerTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulerTasks_MessageId",
                table: "SchedulerTasks",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulerTasks_SenderId",
                table: "SchedulerTasks",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulerTasks_ServerId",
                table: "SchedulerTasks",
                column: "ServerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipients_SchedulerTasks_SchedulerTaskId",
                table: "Recipients",
                column: "SchedulerTaskId",
                principalTable: "SchedulerTasks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipients_SchedulerTasks_SchedulerTaskId",
                table: "Recipients");

            migrationBuilder.DropTable(
                name: "SchedulerTasks");

            migrationBuilder.DropIndex(
                name: "IX_Recipients_SchedulerTaskId",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "SchedulerTaskId",
                table: "Recipients");
        }
    }
}
