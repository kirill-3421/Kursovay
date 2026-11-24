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
    /// окно администратора, позволяет добавлять учеников, искать их, менять их статус и формировать отчет
    /// </summary>
    public partial class AdminWindow : Window
    {

        public AdminWindow()
        {
            InitializeComponent();

        }
    }
}
