using DataAccess.ReposImpl;
using Domain.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
        {
            return services
                .AddAutoMapper(typeof(MapperProfile))
                .AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString))
                .AddScoped<IAttendanceRepository, AttendanceRepository>()
                .AddScoped<IHomeworkRepository, HomeworkRepository>()
                .AddScoped<IStudentRepository, StudentRepository>()
                .AddScoped<ILecturerRepository, LecturerRepository>()
                .AddScoped<ILectureRepository, LectureRepository>();
        }
    }
}
