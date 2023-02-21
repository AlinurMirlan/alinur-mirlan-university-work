using FourthLab.Attributes;

namespace StudentLibrary
{
    public enum Education
    {
        Specialist, Bachelor, SecondEducation
    }

    [ClassHierarchy("Object", "Person", "Student")]
    [ImplementedInterfaces()]
    public class Student : Person
    {
        private static readonly Student[] Students =
        {
            new Student("Alinur", "Mirlan", new DateTime(2002, 11, 18), Education.Bachelor, "SE-2-20"),
            new Student("Erlan", "Esengeldiev", new DateTime(2005, 12, 19), Education.Bachelor, "SE-1-23"),
            new Student("Daniel", "Migan", new DateTime(2003, 11, 12), Education.Specialist, "ES-1-21"),
            new Student("Michigan", "Kaleb", new DateTime(2001, 10, 11), Education.Specialist, "KN-1-21"),
            new Student("Michael", "John", new DateTime(2003, 11, 18), Education.Specialist, "ES-1-21"),
        };

        public Education Education { get; set; }

        public string Group { get; set; }

        public Student(string name, string surname, DateTime birthDate, Education education, string group) : base(name, surname, birthDate)
        {
            Education = education;
            Group = group;
        }

        public override void Print() => Console.WriteLine(this.ToString());

        public override string ToString() => $"{base.ToString()} Education: {Education}. Group: {Group}.";

        public static Student RandomStudent()
        {
            var random = new Random();
            return Students[random.Next(Students.Length)];
        }
    }
}