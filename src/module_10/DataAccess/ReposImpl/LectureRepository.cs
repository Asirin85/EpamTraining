using AutoMapper;
using DataAccess.Entities;
using Domain.Entities;
using Domain.Repos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
#nullable enable
namespace DataAccess.ReposImpl
{
    internal class LectureRepository : ILectureRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;
        public LectureRepository(ApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }
        public void Delete(int id)
        {
            var lectureToDelete = _applicationContext.Lectures.Find(id);
            _applicationContext.Entry(lectureToDelete).State = EntityState.Deleted;
            _applicationContext.SaveChanges();
        }

        public int Edit(Lecture lecture)
        {
            if (_applicationContext.Lectures.Find(lecture.Id) is LectureDb lectureDb)
            {
                /* var lecturerDb = _mapper.Map<LecturerDb>(lecture.Lecturer);
                  var homeworkDb = _mapper.Map<HomeworkDb>(lecture.Homework);
                  lectureDb.Lecturer = lecturerDb;
                  lectureDb.Homework = homeworkDb;*/
                lectureDb.HomeworkId = lecture.HomeworkId;
                lectureDb.LecturerId = lecture.LecturerId;
                lectureDb.Name = lecture.Name;
                _applicationContext.Entry(lectureDb).State = EntityState.Modified;
                _applicationContext.SaveChanges();
            }
            return lecture.Id;
        }

        public Lecture? Get(int id)
        {
            var lectureDb = _applicationContext.Lectures.FirstOrDefault(x => x.Id == id);
            return _mapper.Map<Lecture?>(lectureDb);
        }

        public IEnumerable<Lecture> GetAll()
        {
            var lecturesDb = _applicationContext.Lectures.ToList();
            return _mapper.Map<IReadOnlyCollection<Lecture>>(lecturesDb);
        }

        public int New(Lecture lecture)
        {
            var lectureDb = _mapper.Map<LectureDb>(lecture);
            var result = _applicationContext.Lectures.Add(lectureDb);
            _applicationContext.SaveChanges();
            return result.Entity.Id;
        }
    }
}
