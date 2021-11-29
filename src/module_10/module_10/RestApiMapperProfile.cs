namespace RestApi
{
    using AutoMapper;
    using Domain.Entities;
    using RestApi.Models;

    internal class RestApiMapperProfile : Profile
    {
        public RestApiMapperProfile()
        {
            CreateMap<StudentValid, Student>();
            CreateMap<LectureValid, Lecture>();
            CreateMap<LecturerValid, Lecturer>();
            CreateMap<AttendanceValid, Attendance>();
            CreateMap<HomeworkValid, Homework>();
        }
    }
}
