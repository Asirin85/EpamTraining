using AutoMapper;
using DataAccess.Entities;
using Domain.Entities;

namespace DataAccess
{
    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<HomeworkDb, Homework>().ReverseMap();
            CreateMap<LectureDb, Lecture>().ReverseMap();
            CreateMap<LecturerDb, Lecturer>().ReverseMap();
            CreateMap<StudentDb, Student>().ReverseMap();
            CreateMap<AttendanceDb, Attendance>().ReverseMap();
        }
    }
}
