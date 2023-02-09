using StudentLibrary;
using TeacherLibrary;

namespace StudentWithAdvisorLibrary
{
    public class StudentWithAdvisor : Student
    {
        public Teacher Teacher { get; set; }

        public StudentWithAdvisor(string name, string surname, DateTime birthDate, Education education, string group, Teacher teacher) : base(name, surname, birthDate, education, group)
        {
            Teacher = teacher;
        }

        public override string ToString() => $"{base.ToString()}. Teacher: {Teacher.Name} {Teacher.Surname}.";
    }
}