using Domain.Entities;
using Domain.Repos;
using Domain.Services;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    internal class HomeworkService : IHomeworkService
    {
        private readonly IHomeworkRepository _homeworkRepository;

        public HomeworkService(IHomeworkRepository homeworkRepository)
        {
            _homeworkRepository = homeworkRepository;
        }

        public void Delete(int id)
        {
            _homeworkRepository.Delete(id);
        }

        public int Edit(Homework homework)
        {
            return _homeworkRepository.Edit(homework);
        }

        public Homework Get(int id)
        {
            return _homeworkRepository.Get(id);
        }

        public IReadOnlyCollection<Homework> GetAll()
        {
            return _homeworkRepository.GetAll().ToArray();
        }

        public int New(Homework homework)
        {
            return _homeworkRepository.New(homework);
        }
    }
}
