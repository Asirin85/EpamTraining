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
                FullName = name[0].ToString().ToUpperInvariant() + name.Substring(1) + " " + surname[0].ToString().ToUpperInvariant() + surname.Substring(1);
                Email = name.ToLowerInvariant() + "." + surname.ToLowerInvariant() + "@epam.com";
            }
        }
        public Student(string email)
        {
            if (email != null && email.Length > 0)
            {
                Email = email.ToLowerInvariant();
                string name = email.ToLowerInvariant().Substring(0, email.IndexOf("."));
                string surname = email.ToLowerInvariant().Substring(email.IndexOf(".") + 1, email.IndexOf("@") - email.IndexOf(".") - 1);
                FullName = name[0].ToString().ToUpperInvariant() + name.Substring(1) + " " + surname[0].ToString().ToUpperInvariant() + surname.Substring(1);
            }
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
