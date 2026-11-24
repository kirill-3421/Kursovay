using Kursovay.Factory;
using Kursovay.Infrastructure;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Kursovay.Models;

namespace Kursovay
{
    /// <summary>
    /// главное окно отвечает за авторизацию и регистрацию пользователей
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            EnsureAdminExists();
        }

        /// <summary>
        /// шифрует введенный пароль, ищет пользователя в БД и открывает нужное окно через фабрику
        /// </summary>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            using var context = new DrivingSchoolDbContext();

            string inputHash = PasswordHasher.HashPassword(TxtPassword.Password);

            var user = context.Users.FirstOrDefault(u => u.Login == TxtLogin.Text && u.Password == inputHash);

            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            IWindowFactory factory = new WindowFactory();
            Window nextWindow = factory.CreateWindowForUser(user);
            nextWindow.Show();
            this.Close();
        }

        /// <summary>
        /// проверяет совпадение паролей, уникальность логина и сохраняет нового пользователя
        /// </summary>
        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RegLogin.Text) || string.IsNullOrWhiteSpace(RegPassword.Password))
            {
                MessageBox.Show("Заполните логин и пароль!");
                return;
            }

            if (RegPassword.Password != RegPasswordConfirm.Password)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }

            using var context = new DrivingSchoolDbContext();

            if (context.Users.Any(u => u.Login == RegLogin.Text))
            {
                MessageBox.Show("Пользователь с таким логином уже существует!");
                return;
            }

            string role = ((System.Windows.Controls.ComboBoxItem)RegRole.SelectedItem).Content.ToString();

            var newUser = new User
            {
                Login = RegLogin.Text,
                Password = PasswordHasher.HashPassword(RegPassword.Password), 
                Role = role,
                StudentId = null,
                InstructorId = null
            };

            context.Users.Add(newUser);
            context.SaveChanges();

            MessageBox.Show("Регистрация успешна! Теперь вы можете войти.");

            RegLogin.Clear();
            RegPassword.Clear();
            RegPasswordConfirm.Clear();
        }

        /// <summary>
        /// проверяет, существует ли в БД администратор
        /// </summary>
        private void EnsureAdminExists()
        {
            using var context = new DrivingSchoolDbContext();

            if (!context.Users.Any(u => u.Role == "Admin"))
            {
                var admin = new User
                {
                    Login = "admin",
                    Password = PasswordHasher.HashPassword("admin123"), 
                    Role = "Admin"
                };

                context.Users.Add(admin);
                context.SaveChanges();
            }
        }
    }
}