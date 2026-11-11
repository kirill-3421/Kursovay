using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovay.Models
{
    internal class Exam
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } = string.Empty; // "теория" или "практика"
        public int Grade { get; set; }

        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
    }
}
