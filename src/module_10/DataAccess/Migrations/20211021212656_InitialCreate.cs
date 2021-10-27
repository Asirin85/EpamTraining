using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Homeworks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Task = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homeworks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lectures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LecturerId = table.Column<int>(type: "integer", nullable: true),
                    HomeworkId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lectures_Homeworks_HomeworkId",
                        column: x => x.HomeworkId,
                        principalTable: "Homeworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lectures_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    LectureId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    Mark = table.Column<int>(type: "integer", nullable: true),
                    StudentAttended = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => new { x.StudentId, x.LectureId });
                    table.ForeignKey(
                        name: "FK_Attendances_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendances_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_LectureId",
                table: "Attendances",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_HomeworkId",
                table: "Lectures",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_LecturerId",
                table: "Lectures",
                column: "LecturerId");
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Name", "Email", "PhoneNumber" },
                values: new object[,]
                {
                    {"Ivan", "asirin@gmail.com", "+72224124242" },
                    {"Anton", "lol@mail.ru", "+51242422323" },
                    {"Dmitrii", "lelol@mail.ru", "+31323541285" },
                }
                );
            migrationBuilder.InsertData(
                table: "Homeworks",
                columns: new[] { "Task" },
                values: new object[,]
                {
                    {"Принести спортивную форму" },
                    {"Выполнить задания с.12-с.79" },
                    {"Сделать что-нибудь" },
                }
                );
            migrationBuilder.InsertData(
                table: "Lecturers",
                columns: new[] { "Name", "Email" },
                values: new object[,]
                {
                    {"Alexey Vilatovich", "al.vilat@gmail.com"},
                    {"Ilya Olegovich", "ol.ya@ya.ru" },
                    {"Dmitrii Alexandrovich", "dmi.lexa@gmail.com"},
                }
                );
            migrationBuilder.InsertData(
                table: "Lectures",
                columns: new[] { "Name", "LecturerId", "HomeworkId" },
                values: new object[,]
                {
                    {"PE", 1, 1},
                    {"Math", 2,2 },
                    {"Engels", 3,3},
                }
                );
            migrationBuilder.InsertData(
                table: "Attendances",
                columns: new[] { "LectureId", "StudentId", "Mark", "StudentAttended" },
                values: new object[,]
                {
                    {1, 1, 3, true},
                    {1, 2, 0, true},
                    {1, 3, 0, false},
                    {2, 1, 0, false},
                    {2, 2, 2, true},
                    {2, 3, 3, true},
                    {3, 1, 0, false},
                    {3, 2, 0, false},
                    {3, 3, 0, false},
                }
                );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "Lectures");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Homeworks");

            migrationBuilder.DropTable(
                name: "Lecturers");
        }
    }
}
