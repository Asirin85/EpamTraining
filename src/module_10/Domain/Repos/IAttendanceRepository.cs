using Domain.Entities;
using System.Collections.Generic;
#nullable enable
namespace Domain.Repos
{
    public interface IAttendanceRepository
    {
        void Delete(int studentId, int lectureId);
        (int studentId, int lectureId) Edit(Attendance attendance);
        Attendance? Get(int studentId, int lectureId);
        IEnumerable<Attendance> GetAll();
        (int studentId, int lectureId) New(Attendance attendance);
        IEnumerable<Attendance> GetAllByLectureName(string lectureName);
        IEnumerable<Attendance> GetAllByStudentName(string studentName);

    }
}
