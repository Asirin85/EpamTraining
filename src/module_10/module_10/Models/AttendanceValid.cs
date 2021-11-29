namespace RestApi.Models
{

    public record AttendanceValid(int LectureId, int StudentId, int? Mark, bool? StudentAttended);
}
