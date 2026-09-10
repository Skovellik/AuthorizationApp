using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace AuthorizationApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public class UserDataModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public partial class MainWindow : Window
    {
        private static List<UserDataModel> usersList = new List<UserDataModel>();
        private static UserDataModel[] usersArray;
        private static bool isInitialized = false;

        public MainWindow()
        {
            InitializeComponent();
            LoadDataOnce("users.txt");
        }

        private void LoadDataOnce(string path)
        {
                if (!isInitialized && File.Exists(path))
                {
                    var lines = File.ReadAllLines(path);
                    foreach (var line in lines)
                    {
                        var parts = line.Split(new[] { ' ', ':' }, System.StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length >= 2)
                        {
                            usersList.Add(new UserDataModel { Login = parts[0], Password = parts[1] });
                        }
                    }
                    usersArray = usersList.ToArray();
                    isInitialized = true;
                }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var login = txtLogin.Text;
            var password = txtPassword.Password;

            if (UserDataBase.IsValidUser(login, password))
            {
                MessageBox.Show($"Приветствую, {login}!");
                Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль. Пожалуйста, зарегистрируйтесь.");
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }
    }
}
