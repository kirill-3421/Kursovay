using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Kursovay.Models
{
    internal class Lesson
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Topic { get; set; } = string.Empty;

        public int GroupId { get; set; }
        public Group? Group { get; set; }

        public int CarId { get; set; }
        public Car? Car { get; set; }

        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
    }
}
