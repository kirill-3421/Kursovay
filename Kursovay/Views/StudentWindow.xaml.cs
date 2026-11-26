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
    public partial class StudentWindow : Window
    {
        private readonly int _studentId;

        public StudentWindow(int studentId)
        {
            InitializeComponent();

            _studentId = studentId;

        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
