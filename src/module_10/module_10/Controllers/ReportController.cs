namespace RestApi.Controllers
{
    using BusinessLogic.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/report")]
    public class ReportController : ControllerBase
    {
        private readonly IReportable _reportService;

        public ReportController(IReportable reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("student/{studentName}/{format}")]
        public ActionResult<string> GetStudentReport(string studentName, string format)
        {
            return _reportService.CreateReportByStudentName(studentName, format);
        }
        [HttpGet("lecture/{lectureName}/{format}")]
        public ActionResult<string> GetLectureReport(string lectureName, string format)
        {
            return _reportService.CreateReportByLectureName(lectureName, format);
        }
    }
}
