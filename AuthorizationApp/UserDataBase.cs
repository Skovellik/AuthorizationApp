using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AuthorizationApp
{
    public static class UserDataBase
    {
        private const string FilePath = "users.txt";

        // Метод для проверки наличия пользователя в базе данных
        public static bool IsValidUser(string login, string password)
        {
            if (!File.Exists(FilePath))
            {
                return false;
            }

            return File.ReadLines(FilePath).Any(line =>
                line.Split(':').FirstOrDefault() == login && line.Split(':').LastOrDefault() == password);
        }

        // Метод для регистрации нового пользователя
        public static void RegisterUser(string firstName, string lastName, string login, string password)
        {
            if (IsValidUser(login, null))
            {
                MessageBox.Show("Пользователь с таким логином уже существует.");
                return;
            }

            var userEntry = $"{login}:{password}";
            File.AppendAllText(FilePath, userEntry + Environment.NewLine);
        }
    }
}
