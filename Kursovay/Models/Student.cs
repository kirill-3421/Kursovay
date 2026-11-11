using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovay.Models
{
    internal class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBith { get; set; }
        public string Phone { get; set; }
        public string PasportDetails { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; } = "Обучается"; // обучается, выпущен, отчислен

        public int GroipId { get; set; }
        public Group? Group { get; set; }

        public List<Exam> Exams { get; set; } = new();
    }
}
