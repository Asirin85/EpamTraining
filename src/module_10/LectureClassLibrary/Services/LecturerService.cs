using Domain.Entities;
using Domain.Repos;
using Domain.Services;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    internal class LecturerService : ILecturerService
    {
        private readonly ILecturerRepository _lecturerRepository;
        public LecturerService(ILecturerRepository lecturerRepository)
        {
            _lecturerRepository = lecturerRepository;
        }

        public void Delete(int id)
        {
            _lecturerRepository.Delete(id);
        }

        public int Edit(Lecturer lecturer)
        {
            return _lecturerRepository.Edit(lecturer);
        }

        public Lecturer Get(int id)
        {
            return _lecturerRepository.Get(id);
        }

        public IReadOnlyCollection<Lecturer> GetAll()
        {
            return _lecturerRepository.GetAll().ToArray();
        }

        public int New(Lecturer lecturer)
        {
            return _lecturerRepository.New(lecturer);
        }
    }
}
