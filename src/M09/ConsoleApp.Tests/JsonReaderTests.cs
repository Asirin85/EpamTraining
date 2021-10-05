using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Tests
{
    public class JsonReaderTests
    {
        [Test]
        public void Test_For_ReadFromJson_IncorrectInput([Values(null, "", "abc")] string path)
        {
            Assert.That(() => JsonReader.ReadFromJson(path), Throws.TypeOf<FileNotFoundException>());
        }
        [Test]
        public void Test_For_ReadFromJson_CorrectInput()
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "testdata.json");
            List<Student> students =  JsonReader.ReadFromJson(path);
            Assert.That(students.Count, Is.EqualTo(4));
        }
    }
}
