using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recolletction
{
    enum Education
    {
        Specialist, Bachelor, SecondEducation
    }

    internal class Student : Person, IDateAndCopy
    {
        private readonly Education education;
        private int group;
        private ArrayList tests = new();
        private ArrayList exams = new();

        public int Group
        {
            get => group;
            set => group = value <= 100 || value > 599 ? throw new ArgumentException($"{nameof(Group)} ought to be in the range between 101 and 599 inclusively.") : value;
        }

        public ArrayList Exams
        {
            get => exams;
            set => exams = value ?? throw new ArgumentNullException(nameof(value));
        }

        public Person Person
        {
            get => this;
            set
            {
                ((Person)this).Name = value.Name;
                ((Person)this).Surname = value.Surname;
                ((Person)this).Date = value.Date;
            }
        }

        public double AverageGrade
        {
            get
            {
                int gradeCount = 0;
                double gradeSum = exams.OfType<Exam>().Aggregate(0, (total, exam) =>
                {
                    gradeCount += 1;
                    return total + exam.Grade;
                });
                return gradeSum / gradeCount;
            }
        }

        public Student(string name, string surname, DateTime birthDate) : base(name, surname, birthDate) { }

        public Student(string name, string surname, DateTime birthDate, Education education, int group) : base(name, surname, birthDate) 
        {
            this.education = education;
            this.group = group;
        }

        public Student(Person person, Education education, int group) : base(person.Name, person.Surname, person.Date)
        {
            this.education = education;
            this.group = group;
        }

        public void AddExams(params Exam[] exams) => this.exams.AddRange(exams);

        public void AddTests(params Test[] tests) => this.tests.AddRange(tests);

        public override string ToString() => $"{base.ToString()}\nCredits: {ConcatSequence<Test>(tests)}\nExams: {ConcatSequence<Exam>(exams)}";

        public override string ToShortString() => $"{base.ToString()}\nAverage Grade: {AverageGrade}";

        public override object DeepCopy() => new Student(Name, Surname, birthDate, education, group) { exams = this.exams, tests = this.tests };

        public IEnumerable<object> Enumerate() => Enumerate(tests, exams);

        public IEnumerable<Exam> EnumerateExamsAboveGrade(int grade)
        {
            foreach (Exam exam in exams.OfType<Exam>())
            {
                if (exam.Grade > grade)
                    yield return exam;
            }
        }

        private static string ConcatSequence<T>(IEnumerable array)
        {
            StringBuilder builder = new();
            foreach (T element in array.OfType<T>())
                builder.Append($"{element}, ");

            builder.Remove(builder.Length - 3, 2);
            return builder.ToString();
        }

        private static IEnumerable<object> Enumerate(params IEnumerable[] enumerables)
        {
            foreach (IEnumerable enumerable in enumerables)
            {
                foreach (object element in enumerable)
                    yield return element;
            }
        }
    }
}
