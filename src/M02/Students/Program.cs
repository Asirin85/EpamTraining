using System;
using System.Collections.Generic;
using System.Linq;

namespace Students
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] subjects = { "Maths", "PE", "Physics", "English", "Programming", "History" };
            var student1c1 = new Student("vasya.pupkin@epam.com");
            var student2c1 = new Student("sHREk.swampov@epam.com");
            var student3c1 = new Student("anton.VASkin@epam.com");
            var student1c2 = new Student("Vasya", "Pupkin");
            var student2c2 = new Student("Shrek", "Swampov");
            var student3c2 = new Student("Anton", "Vaskin");
            var studentSubjectDict = new Dictionary<Student, HashSet<string>>();
            Student[] students = { student1c1, student2c1, student3c1, student1c2, student2c2, student3c2 }; // Put students in array to use foreach for initializing dictionary
            var rnd = new Random();
            int nextRandom; // To store random value for checks
            foreach (Student st in students)
            {
                List<int> possible = Enumerable.Range(0, 6).ToList(); // Possible values for randomizer
                studentSubjectDict[st] = new HashSet<string>();
                while (studentSubjectDict[st].Count < 3)
                {
                    nextRandom = rnd.Next(0, possible.Count);
                    studentSubjectDict[st].Add(subjects[possible[nextRandom]]);
                    possible.RemoveAt(nextRandom);
                }
            }
            foreach (var item in studentSubjectDict)
            {
                Console.WriteLine(item.Key.FullName); // return 3 out of 6
                Console.WriteLine(item.Key.Email);
            }
        }
    }
}
