using BusinessLogic.Interfaces;
using BusinessLogic.Logic;
using BusinessLogic.Services;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            return services
                .AddScoped<IAttendanceService, AttendanceService>()
                .AddScoped<IHomeworkService, HomeworkService>()
                .AddScoped<ILecturerService, LecturerService>()
                .AddScoped<ILectureService, LectureService>()
                .AddScoped<IStudentService, StudentService>()
                .AddScoped<IReportable, ReportCreator>();
        }
    }
}
