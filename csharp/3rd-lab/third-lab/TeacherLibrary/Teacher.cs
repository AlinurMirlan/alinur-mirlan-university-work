using PersonLibrary;
using StudentLibrary;

namespace TeacherLibrary
{

    public sealed class Teacher : Person
    {
        private static readonly Teacher[] Teachers =
        {
        new Teacher("Julia", "Manki", new DateTime(1991, 1, 12), "Maths", "Physics"),
        new Teacher("Angelina", "Julia", new DateTime(1987, 12, 12), "Biology"),
        new Teacher("Haul", "Nancy", new DateTime(1992, 4, 12), "Biology", "Chemistry"),
        new Teacher("Michael", "Gabriel", new DateTime(1979, 2, 1), "Computer Science", "Theory of Making Decisions"),
        new Teacher("Asan", "Ulanov", new DateTime(1993, 4, 18), "Web Design", "Algorithms and Data Structures", "Databases"),
        new Teacher("Kole", "Bennet", new DateTime(1971, 7, 7), "OOP", "Design Patterns"),
    };
        public List<string> Subjects { get; set; } = new();
        public List<Student> Students { get; set; } = new();

        public Teacher(string name, string surname, DateTime birthDate, params string[] subjects) : base(name, surname, birthDate)
        {
            Subjects.AddRange(subjects);
        }

        public override string ToString() => $"{base.ToString()}. Subjects: {Subjects.Aggregate(string.Empty, (total, subject) => total + $"{subject}, ")[..^2]}";

        public static Teacher RandomTeacher()
        {
            Random random = new();
            return Teachers[random.Next(Teachers.Length)];
        }
    }
}