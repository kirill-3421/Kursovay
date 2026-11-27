using Kursovay.Models;
using Kursovay.States;
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
    /// окно администратора, позволяет добавлять учеников, искать их, менять их статус и формировать отчеты
    /// </summary>
    public partial class AdminWindow : Window
    {
        private readonly DrivingSchoolDbContext _context;
        private List<Student> _allStudents;

        public AdminWindow()
        {
            InitializeComponent();
            _context = new DrivingSchoolDbContext();
            LoadData();
        }

        /// <summary>
        /// загружает список групп и учеников из БД
        /// </summary>
        private void LoadData()
        {
            CmbGroup.ItemsSource = _context.Groups.ToList();

            _allStudents = _context.Students.Include(s => s.Group).ToList();
            GridStudents.ItemsSource = _allStudents;
        }

        /// <summary>
        /// поиск учеников по ФИО
        /// </summary>
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_allStudents == null) return;

            string searchText = TxtSearch.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                GridStudents.ItemsSource = _allStudents;
            }
            else
            {
                var filteredList = _allStudents
                    .Where(s => s.FullName.ToLower().Contains(searchText))
                    .ToList();

                GridStudents.ItemsSource = filteredList;

                if (filteredList.Count == 0)
                {
                    MessageBox.Show("Ученик с данным ФИО не найден!!");
                }
            }
        }

        /// <summary>
        /// очищает поиск и возвращает список учеников
        /// </summary>
        private void BtnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            TxtSearch.Text = string.Empty;
            GridStudents.ItemsSource = _allStudents;
        }

        /// <summary>
        /// добавляет нового ученика в БД
        /// </summary>
        private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtFullName.Text) ||
                string.IsNullOrWhiteSpace(TxtPhone.Text) ||
                CmbGroup.SelectedItem == null)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            var newStudent = new Student
            {
                FullName = TxtFullName.Text,
                Phone = TxtPhone.Text,
                DateOfBith = DateTime.Now.AddYears(-20),
                EnrollmentDate = DateTime.Now,
                Status = "Обучается",
                PasportDetails = "Не указаны",
                GroupId = ((Group)CmbGroup.SelectedItem).Id
            };

            _context.Students.Add(newStudent);
            _context.SaveChanges();

            MessageBox.Show("Ученик успешно зачислен!");

            TxtFullName.Clear();
            TxtPhone.Clear();
            LoadData();
        }

        /// <summary>
        /// смена статуса ученика с использованием паттерна "Состояние"
        /// </summary>
        private void ChangeStudentStatus(IStudentState newState)
        {
            if (GridStudents.SelectedItem is not Student selectedStudent)
            {
                MessageBox.Show("Выберите ученика в таблице!");
                return;
            }

            var stateContext = new StudentContext(selectedStudent.Status);

            if (stateContext.TryChangeStatus(newState))
            {
                selectedStudent.Status = stateContext.State.StatusName;
                _context.SaveChanges();
                LoadData();
                MessageBox.Show($"Статус изменен на: {selectedStudent.Status}");
            }
            else
            {
                MessageBox.Show($"Недопустимый переход: из '{selectedStudent.Status}' в '{newState.StatusName}'!",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // переводит выбранного ученика в статус "Обучается"
        private void BtnSetEnrolled_Click(object sender, RoutedEventArgs e) => ChangeStudentStatus(new EnrolledState());

        // переводит выбранного ученика в статус "Выпущен"
        private void BtnSetGraduated_Click(object sender, RoutedEventArgs e) => ChangeStudentStatus(new GraduatedState());

        // переводит выбранного ученика в статус "Отчислен"
        private void BtnSetExpelled_Click(object sender, RoutedEventArgs e) => ChangeStudentStatus(new ExpelledState());

        /// <summary>
        /// формирует простой отчет по количеству учеников в разных статусах
        /// </summary>
        private void BtnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            if (_allStudents == null || _allStudents.Count == 0)
            {
                MessageBox.Show("В базе нет учеников для формирования отчета.");
                return;
            }

            int total = _allStudents.Count;
            int enrolled = _allStudents.Count(s => s.Status == "Обучается");
            int graduated = _allStudents.Count(s => s.Status == "Выпущен");
            int expelled = _allStudents.Count(s => s.Status == "Отчислен");

            string report = $"ОТЧЕТ ПО УЧЕНИКАМ\n\n" +
                            $"Всего учеников: {total}\n" +
                            $"Обучаются: {enrolled}\n" +
                            $"Выпущены: {graduated}\n" +
                            $"Отчислены: {expelled}";

            MessageBox.Show(report, "Отчет", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// закрывает окно администратора и возвращает на окно авторизации
        /// </summary>
        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }

        private void GridStudents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}