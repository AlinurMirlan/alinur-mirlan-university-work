using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recolletction
{
    internal class Exam
    {
        public string? Subject { get; set; }
        public int Grade { get; set; }
        public DateTime Date { get; set; }

        public override string ToString() => $"Subject: {Subject} | Grade: {Grade} | Date of exam: {Date:d}";
    }
}
