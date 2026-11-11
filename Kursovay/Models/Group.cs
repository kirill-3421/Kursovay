using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovay.Models
{
    internal class Group
    {
        public int Id { get; set; }
        public string GroupNumber { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int TrainingProgramId { get; set; }
        public TrainingProgram? TrainingProgram { get; set; }

        
        public List<Student> Students { get; set; } = new();
        public List<Lesson> Lessons { get; set; } = new();
    }
}
