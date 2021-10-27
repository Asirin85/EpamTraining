using Domain.Entities;
using System.Collections.Generic;
#nullable enable
namespace Domain.Repos
{
    public interface ILectureRepository
    {
        void Delete(int id);
        int Edit(Lecture lecture);
        Lecture? Get(int id);
        IEnumerable<Lecture> GetAll();
        int New(Lecture lecture);
    }
}
