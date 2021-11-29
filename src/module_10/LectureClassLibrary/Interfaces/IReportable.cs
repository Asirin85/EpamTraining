namespace BusinessLogic.Interfaces
{
    public interface IReportable
    {
        string CreateReportByStudentName(string studentName, string format);
        string CreateReportByLectureName(string lectureName, string format);
    }
}
