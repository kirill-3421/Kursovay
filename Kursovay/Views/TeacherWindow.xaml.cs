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
        public TeacherWindow(int instructorId)
        {
            InitializeComponent();
            _instructorId = instructorId;
        }
    }
}