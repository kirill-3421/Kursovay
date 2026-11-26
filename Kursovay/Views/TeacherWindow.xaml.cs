using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Kursovay.Models;
using Microsoft.EntityFrameworkCore;

namespace Kursovay.Views
{
    /// <summary>
    /// окно преподавателя, позволяет просматривать расписание и выставлять оценки
    /// </summary>
    public partial class TeacherWindow : Window
    {
        private readonly DrivingSchoolDbContext _context;
        private readonly int _instructorId;

        public TeacherWindow(int instructorId)
        {
            InitializeComponent();
            _context = new DrivingSchoolDbContext();
            _instructorId = instructorId;
            LoadData();
        }

        /// <summary>
        /// загружает расписание и список студентов из БД
        /// </summary>
        private void LoadData()
        {
            var instructor = _context.Instructors.Find(_instructorId);
            if (instructor != null)
            {
                TxtWelcome.Text = $"Расписание: {instructor.FullName}";
            }

            GridLessons.ItemsSource = _context.Lessons
                .Include(l => l.Group)
                .Include(l => l.Car)
                .Where(l => l.InstructorId == _instructorId)
                .ToList();

            CmbStudent.ItemsSource = _context.Students.ToList();
        }

        /// <summary>
        /// сохраняет оценку за экзамен в БД
        /// </summary>
        private void BtnAddGrade_Click(object sender, RoutedEventArgs e)
        {
            if (CmbStudent.SelectedItem is not Student student ||
                CmbExamType.SelectedItem is not ComboBoxItem typeItem)
            {
                MessageBox.Show("Выберите студента и тип экзамена!");
                return;
            }

            if (!int.TryParse(TxtGrade.Text, out int grade) || grade < 2 || grade > 5)
            {
                MessageBox.Show("Введите корректную оценку от 2 до 5!");
                return;
            }

            try
            {
                var exam = new Exam
                {
                    StudentId = student.Id,
                    InstructorId = _instructorId,
                    Type = typeItem.Content.ToString(),
                    Grade = grade,
                    Date = DateTime.Now
                };

                _context.Exams.Add(exam);
                _context.SaveChanges();

                MessageBox.Show("Оценка успешно сохранена!");
                TxtGrade.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.InnerException?.Message ?? ex.Message}",
                                "Ошибка БД", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// возврат на окно авторизации
        /// </summary>
        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}