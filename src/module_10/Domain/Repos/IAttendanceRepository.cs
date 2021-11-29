using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Repos
{
    public interface IAttendanceRepository
    {
        void Delete(int studentId, int lectureId);
        AttendaceId Edit(Attendance attendance);
        Attendance? Get(int studentId, int lectureId);
        IEnumerable<Attendance> GetAll();
        AttendaceId New(Attendance attendance);
        IEnumerable<Attendance> GetAllByLectureName(string lectureName);
        IEnumerable<Attendance> GetAllByStudentName(string studentName);

    }
}
