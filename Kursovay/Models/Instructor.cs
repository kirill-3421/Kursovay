using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovay.Models
{
    internal class Instructor
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string ContactDetails { get; set; } = string.Empty;

        public string Login { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public List<Lesson> Lessons { get; set; } = new();
        public List<Exam> Exams { get; set; } = new(); 
    }
}
