using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MonitoringDumpTruck.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mining_truck");

            migrationBuilder.CreateTable(
                name: "dump_trucks",
                schema: "mining_truck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Model = table.Column<string>(type: "text", nullable: true),
                    YearIssue = table.Column<DateOnly>(type: "date", nullable: true),
                    GosNumber = table.Column<string>(type: "text", nullable: true),
                    KPP = table.Column<string>(type: "text", nullable: true),
                    LoadCapacity = table.Column<int>(type: "integer", nullable: true),
                    BodyVolume = table.Column<int>(type: "integer", nullable: true),
                    TOIR = table.Column<string>(type: "text", nullable: true),
                    MaxSpeed = table.Column<int>(type: "integer", nullable: true),
                    FullMass = table.Column<int>(type: "integer", nullable: true),
                    Mileage = table.Column<int>(type: "integer", nullable: true),
                    MaxFuel = table.Column<int>(type: "integer", nullable: true),
                    EngineModel = table.Column<string>(type: "text", nullable: true),
                    TireModel = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dump_trucks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "road_types",
                schema: "mining_truck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_road_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "mining_truck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "statutes",
                schema: "mining_truck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statutes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "fillings",
                schema: "mining_truck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InitialVolume = table.Column<double>(type: "double precision", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DumpTruckId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fillings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fillings_dump_trucks_DumpTruckId",
                        column: x => x.DumpTruckId,
                        principalSchema: "mining_truck",
                        principalTable: "dump_trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "working_hours",
                schema: "mining_truck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DumpTruckId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_working_hours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_working_hours_dump_trucks_DumpTruckId",
                        column: x => x.DumpTruckId,
                        principalSchema: "mining_truck",
                        principalTable: "dump_trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "mining_truck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "mining_truck",
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pointers",
                schema: "mining_truck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DumpTruckId = table.Column<int>(type: "integer", nullable: false),
                    RoadTypeId = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Speed = table.Column<double>(type: "double precision", nullable: false),
                    Fuel = table.Column<double>(type: "double precision", nullable: false),
                    EngineTemperature = table.Column<double>(type: "double precision", nullable: false),
                    EnginePressure = table.Column<double>(type: "double precision", nullable: false),
                    EngineSpeed = table.Column<double>(type: "double precision", nullable: false),
                    EngineLoad = table.Column<double>(type: "double precision", nullable: false),
                    EngineVibration = table.Column<double>(type: "double precision", nullable: false),
                    TirePressure = table.Column<double>(type: "double precision", nullable: false),
                    TireTemperature = table.Column<double>(type: "double precision", nullable: false),
                    TireTreadDepth = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pointers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pointers_dump_trucks_DumpTruckId",
                        column: x => x.DumpTruckId,
                        principalSchema: "mining_truck",
                        principalTable: "dump_trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pointers_road_types_RoadTypeId",
                        column: x => x.RoadTypeId,
                        principalSchema: "mining_truck",
                        principalTable: "road_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pointers_statutes_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "mining_truck",
                        principalTable: "statutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_fillings_DumpTruckId",
                schema: "mining_truck",
                table: "fillings",
                column: "DumpTruckId");

            migrationBuilder.CreateIndex(
                name: "IX_pointers_DumpTruckId",
                schema: "mining_truck",
                table: "pointers",
                column: "DumpTruckId");

            migrationBuilder.CreateIndex(
                name: "IX_pointers_RoadTypeId",
                schema: "mining_truck",
                table: "pointers",
                column: "RoadTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_pointers_StatusId",
                schema: "mining_truck",
                table: "pointers",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_users_RoleId",
                schema: "mining_truck",
                table: "users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_working_hours_DumpTruckId",
                schema: "mining_truck",
                table: "working_hours",
                column: "DumpTruckId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fillings",
                schema: "mining_truck");

            migrationBuilder.DropTable(
                name: "pointers",
                schema: "mining_truck");

            migrationBuilder.DropTable(
                name: "users",
                schema: "mining_truck");

            migrationBuilder.DropTable(
                name: "working_hours",
                schema: "mining_truck");

            migrationBuilder.DropTable(
                name: "road_types",
                schema: "mining_truck");

            migrationBuilder.DropTable(
                name: "statutes",
                schema: "mining_truck");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "mining_truck");

            migrationBuilder.DropTable(
                name: "dump_trucks",
                schema: "mining_truck");
        }
    }
}
