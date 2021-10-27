using Domain.Entities;
using System.Collections.Generic;
#nullable enable
namespace Domain.Services
{
    public interface ILectureService
    {
        Lecture? Get(int id);
        IReadOnlyCollection<Lecture> GetAll();
        int New(Lecture lecture);
        int Edit(Lecture lecture);
        void Delete(int id);
    }
}
