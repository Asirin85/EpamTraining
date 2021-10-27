namespace BusinessLogic.Logic
{
    using BusinessLogic.Exceptions;
    using BusinessLogic.Interfaces;
    using Domain.Entities;
    using Domain.Services;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class ReportCreator : IReportable
    {
        private readonly IAttendanceService _attendanceService;

        public ReportCreator(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        public string CreateReportByStudentName(string studentName, string format)
        {
            if (studentName is null) throw new StudentNameNullException("Received student name is empty.");
            if (format is null) throw new FormatNullException("Received format is empty.");
            var attendancesStudentName = _attendanceService.GetAllByStudentName(studentName);
            return CreateReport(format, attendancesStudentName);
        }
        public string CreateReportByLectureName(string lectureName, string format)
        {
            if (lectureName is null) throw new LectureNameNullException("Received lecture name is empty.");
            if (format is null) throw new FormatNullException("Received format is empty.");
            var attendancesByLectureName = _attendanceService.GetAllByLectureName(lectureName);
            return CreateReport(format, attendancesByLectureName);
        }
        private string CreateReport(string format, IEnumerable<Attendance> attendances) => format.ToLowerInvariant() switch
        {
            "json" => CreateJSONReport(attendances),
            "xml" => CreateXMLReport(attendances),
            _ => throw new ReportFormatNotSupportedException("Received report format is not supported.")
        };
        private string CreateJSONReport(IEnumerable<Attendance> attendances)
        {
            if (attendances is null) throw new AttendanceListNullException("Unable to make report, attendance list is empty.");
            return JsonConvert.SerializeObject(attendances);
        }
        private string CreateXMLReport(IEnumerable<Attendance> attendances)
        {
            if (attendances is null) throw new AttendanceListNullException("Unable to make report, attendance list is empty.");
            using (var stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(attendances.GetType());
                serializer.Serialize(stringwriter, attendances);
                return stringwriter.ToString();
            }
        }
    }
}
