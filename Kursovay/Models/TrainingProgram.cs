using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kursovay.Models
{
    internal class TrainingProgram
    {
        public int Id { get; set; }
        public string ProgramName { get; set; }
        public string Category { get; set; } = string.Empty;
        public int DurationHours { get; set; }
        public string Description { get; set; } = string.Empty;

        public List<Group> Groups { get; set; } = new();
    }
}
