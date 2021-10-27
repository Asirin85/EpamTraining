using Domain.Entities;
using Domain.Repos;
using Domain.Services;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    internal class LectureService : ILectureService
    {
        private readonly ILectureRepository _lectureRepository;

        public LectureService(ILectureRepository lectureRepository)
        {
            _lectureRepository = lectureRepository;
        }

        public void Delete(int id)
        {
            _lectureRepository.Delete(id);
        }

        public int Edit(Lecture lecture)
        {
            return _lectureRepository.Edit(lecture);
        }

        public Lecture Get(int id)
        {
            return _lectureRepository.Get(id);
        }

        public IReadOnlyCollection<Lecture> GetAll()
        {
            return _lectureRepository.GetAll().ToArray();
        }

        public int New(Lecture lecture)
        {
            return _lectureRepository.New(lecture);
        }
    }
}
