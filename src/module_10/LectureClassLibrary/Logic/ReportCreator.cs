namespace BusinessLogic.Logic
{
    using BusinessLogic.Exceptions;
    using BusinessLogic.Interfaces;
    using Domain.Entities;
    using Domain.Services;
    using System.Collections.Generic;

    public class ReportCreator : IReportable
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IFormatConverter<Attendance> _formatConverter;
        public ReportCreator(IAttendanceService attendanceService, IFormatConverter<Attendance> formatConverter)
        {
            _attendanceService = attendanceService;
            _formatConverter = formatConverter;
        }

        public string CreateReportByStudentName(string studentName, string format)
        {
            if (studentName is null) throw new StudentNameNullException("Received student name is empty.");
            if (format is null) throw new FormatNullException("Received format is empty.");
            var attendancesStudentName = _attendanceService.GetAllByStudentName(studentName);
            if (attendancesStudentName is null) throw new AttendanceListNullException("Unable to make report, attendance list is empty.");
            return CreateReport(format, attendancesStudentName);
        }
        public string CreateReportByLectureName(string lectureName, string format)
        {
            if (lectureName is null) throw new LectureNameNullException("Received lecture name is empty.");
            if (format is null) throw new FormatNullException("Received format is empty.");
            var attendancesByLectureName = _attendanceService.GetAllByLectureName(lectureName);
            if (attendancesByLectureName is null) throw new AttendanceListNullException("Unable to make report, attendance list is empty.");
            return CreateReport(format, attendancesByLectureName);
        }
        private string CreateReport(string format, IEnumerable<Attendance> attendances) => format.ToLowerInvariant() switch
        {
            "json" => _formatConverter.ConvertToJSONString(attendances),
            "xml" => _formatConverter.ConvertToXMLString(attendances),
            _ => throw new ReportFormatNotSupportedException("Received report format is not supported.")
        };
    }
}
