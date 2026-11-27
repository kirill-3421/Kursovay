using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kursovay.Views
{
    /// <summary>
    /// личный кабинет ученика, позволяет просматривать информацию о группе и свои оценки
    /// </summary>
    public partial class StudentWindow : Window
    {
        public StudentWindow(int studentId)
        {
            InitializeComponent();

            using var context = new DrivingSchoolDbContext();

            var student = context.Students
                .Include(s => s.Group)
                .ThenInclude(g => g.TrainingProgram)
                .Include(s => s.Exams)
                .ThenInclude(e => e.Instructor)
                .FirstOrDefault(s => s.Id == studentId);

            if (student == null)
            {
                MessageBox.Show("Данные ученика не найдены в базе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TxtWelcome.Text = $"Добро пожаловать, {student.FullName}!";

            if (student.Group != null)
            {
                string programName = student.Group.TrainingProgram?.ProgramName ?? "Не указана";
                TxtGroupInfo.Text = $"Группа: {student.Group.GroupNumber} (Программа: {programName})";
            }
            else
            {
                TxtGroupInfo.Text = "Вы пока не зачислены в группу";
            }

            var lastExam = student.Exams.OrderByDescending(e => e.Date).FirstOrDefault();
            if (lastExam?.Instructor != null)
            {
                TxtInstructorInfo.Text = lastExam.Instructor.FullName;
            }
            else
            {
                TxtInstructorInfo.Text = "Преподаватель пока не назначен";
            }

            GridExams.ItemsSource = student.Exams.OrderByDescending(e => e.Date).ToList();
        }

        /// <summary>
        /// возвращает пользователя на окно авторизации
        /// </summary>
        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
