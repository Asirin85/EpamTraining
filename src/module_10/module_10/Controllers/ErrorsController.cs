namespace RestApi.Controllers
{
    using BusinessLogic.Exceptions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using RestApi.ErrorHandling;

    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        private readonly ILogger _logger;

        public ErrorsController(ILogger<ErrorsController> logger)
        {
            _logger = logger;
        }
        [Route("error")]
        public ErrorResponseModel Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (context is not null)
            {
                var exception = context.Error;
                _logger.LogError(exception.ToString());
                Response.StatusCode = exception switch
                {
                    AttendanceListNullException => 500,
                    FormatNullException => 400,
                    LectureNameNullException => 400,
                    StudentNameNullException => 400,
                    LectureNullException => 500,
                    LecturerNullException => 500,
                    StudentNullException => 500,
                    ReportFormatNotSupportedException => 400,
                    _ => 500
                };
                return new ErrorResponseModel(exception);
            }
            else return null;
        }
    }
}
