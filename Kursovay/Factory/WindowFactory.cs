using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows; 
using Kursovay.Models;
using Kursovay.Views; 


namespace Kursovay.Factory
{
    internal class WindowFactory : IWindowFactory
    {
        /// <summary>
        /// создает окно в зависимости от пользователя
        /// </summary>
        public Window CreateWindowForUser(Models.User user)
        {
            return user.Role switch
            {
                "Admin" => new AdminWindow(),
                "Teacher" => new TeacherWindow(user.InstructorId ?? 1),
                "Student" => new StudentWindow(user.StudentId ?? 1),
                _ => throw new System.Exception("Неизвестная роль пользователя")
            };
        }
    }
}
