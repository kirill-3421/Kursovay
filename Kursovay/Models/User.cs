using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovay.Models
{
    /// <summary>
    /// сущность Пользователя
    /// </summary>
    internal class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // Admin, Teacher, Student

        public int? InstructorId { get; set; }
        public Instructor? Instructor { get; set; }

        public int? StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
