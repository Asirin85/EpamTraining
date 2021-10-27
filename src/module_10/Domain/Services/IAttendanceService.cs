using Domain.Entities;
using System.Collections.Generic;
#nullable enable
namespace Domain.Services
{
    public interface IAttendanceService
    {
        Attendance? Get(int studentId, int lectureId);
        IReadOnlyCollection<Attendance> GetAll();
        (int studentId, int lectureId) New(Attendance attendance);
        (int studentId, int lectureId) Edit(Attendance attendance);
        void Delete(int studentId, int lectureId);
        IReadOnlyCollection<Attendance> GetAllByStudentName(string studentName);
        IReadOnlyCollection<Attendance> GetAllByLectureName(string lectureName);
    }
}
