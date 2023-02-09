using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondLab.entities
{
    internal class Client
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int LessonDuration { get; set; }

        public Client(int id, int year, int month, int lessonDuration)
        {
            Id = id;
            Year = year;
            Month = month;
            LessonDuration = lessonDuration;
        }

        public override string ToString() => $"{{ Id: {Id}, Year: {Year}, Month: {Month}, Duration: {LessonDuration} }}\n";
    }
}
