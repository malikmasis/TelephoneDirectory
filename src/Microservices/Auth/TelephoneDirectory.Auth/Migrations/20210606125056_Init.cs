﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TelephoneDirectory.Auth.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserAccounts",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "IsDeleted", "Password", "UserName" },
                values: new object[] { 1L, new DateTime(2021, 6, 6, 12, 50, 55, 873, DateTimeKind.Utc).AddTicks(4608), null, false, "admin", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccounts");
        }
    }
}
