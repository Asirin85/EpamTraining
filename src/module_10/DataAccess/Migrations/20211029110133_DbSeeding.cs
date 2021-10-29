using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DbSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        }
    }
}
