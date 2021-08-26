using System;

namespace Students
{
    class Student
    {
        public string FullName { get; }
        public string Email { get; }

        public Student(string name, string surname)
        {
            if (name != null && surname != null && name.Length > 0 && surname.Length > 0)
            {
                FullName = $"{ToUpperFirstLetter(name)} {ToUpperFirstLetter(surname)}";
                Email = $"{name.ToLowerInvariant()}.{surname.ToLowerInvariant()}@epam.com";
            }
            else throw new ArgumentException("Wrong arguments in Student constructor");
        }
        public Student(string email)
        {
            if (email != null && email.Length > 0)
            {
                Email = email.ToLowerInvariant();
                string name = Email[0..Email.IndexOf(".")];
                string surname = Email[(Email.IndexOf(".") + 1)..Email.IndexOf("@")];
                FullName = $"{ToUpperFirstLetter(name)} {ToUpperFirstLetter(surname)}";
            }
            else throw new ArgumentException("Wrong arguments in Student constructor");
        }
        private string ToUpperFirstLetter(string s)
        {
            if (s != null && s.Length > 0)
                return $"{s[0].ToString().ToUpperInvariant()}{s[1..^0]}";
            else return "";
        }
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                Student s = obj as Student;
                if (s != null)
                    return FullName.Equals(s.FullName);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return FullName.GetHashCode();
        }
    }
}
