namespace RestApi
{
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;
    using RestApi.Models;
    using RestApi.Validation;

    public static class RestApiBoostrapper
    {
        public static IServiceCollection AddRestApi(this IServiceCollection services)
        {
            return services
                .AddAutoMapper(typeof(RestApiMapperProfile))
                .AddTransient<IValidator<StudentValid>, StudentValidator>()
                .AddTransient<IValidator<AttendanceValid>, AttendanceValidator>()
                .AddTransient<IValidator<LecturerValid>, LecturerValidator>()
                .AddTransient<IValidator<LectureValid>, LectureValidator>()
                .AddTransient<IValidator<HomeworkValid>, HomeworkValidator>();
        }
    }
}
