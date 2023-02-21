using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StudentLibrary
{
    [DataContract]
    public class SeniorStudent : Student
    {
        private static readonly SeniorStudent[] Seniors =
        {
            new SeniorStudent("Alinur", "Mirlan", new DateTime(2002, 11, 18), Education.Bachelor, "SE-2-20") { Seniority = 2 },
            new SeniorStudent("Erlan", "Esengeldiev", new DateTime(2005, 12, 19), Education.Bachelor, "SE-1-23") {Seniority = 10},
            new SeniorStudent("Daniel", "Migan", new DateTime(2003, 11, 12), Education.Specialist, "ES-1-21") { Seniority = 11 },
            new SeniorStudent("Michigan", "Kaleb", new DateTime(2001, 10, 11), Education.Specialist, "KN-1-21") { Seniority = 6 },
            new SeniorStudent("Michael", "John", new DateTime(2003, 11, 18), Education.Specialist, "ES-1-21") { Seniority = 99 },
        };

        [DataMember]
        public int Seniority { get; set; }

        public SeniorStudent(string name, string surname, DateTime birthDate, Education education, string group) : base(name, surname, birthDate, education, group) { }
        public SeniorStudent() { }

        public static SeniorStudent RandomSeniorStudent()
        {
            var random = new Random();
            return Seniors[random.Next(Seniors.Length)];
        }

        public override string ToString() => $"{base.ToString()} Seniority: {Seniority}.";

        public override void Print() => Console.WriteLine(this.ToString());
    }
}
