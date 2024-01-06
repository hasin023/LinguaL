using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Team_Sharp.Utility
{
    public class UserAuthentication
    {
        FileReaderHandler fileReaderHandler = new FileReaderHandler();
        PasswordHasher passwordHasher = new PasswordHasher();

        public User RegisterUser(string username, string password, string confirmPassword, string name, string email, string dob, bool isMale, bool isFemale, bool isOther)
        {
            if (password.Length >= 6)
            {
                if (password == confirmPassword)
                {
                    User user = new User(username, password)
                    {
                        Name = name,
                        Email = email,
                        DateOfBirth = dob
                    };

                    if (isMale)
                    {
                        user.Gender = "Male";
                    }
                    else if (isFemale)
                    {
                        user.Gender = "Female";
                    }
                    else if (isOther)
                    {
                        user.Gender = "Other";
                    }

                    return user;
                }
                else
                {
                    MessageBox.Show("Passwords do not match!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Password must be at least 6 characters!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return null;
        }



        public User AuthenticateUser(User user)
        {
            string userFilePath = $@"../../../DataBase/User/{user.Username}.txt";

            if (!fileReaderHandler.UserFileExists(userFilePath))
            {
                MessageBox.Show("Account does not exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            User storedUser = fileReaderHandler.ReadUserFromFile(userFilePath);

            if (storedUser == null)
            {
                MessageBox.Show("Error reading user information", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            if (!string.Equals(user.Username, storedUser.Username, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Incorrect username", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            string enteredPasswordHash = passwordHasher.HashPassword(user.Password);

            if (!string.Equals(enteredPasswordHash, storedUser.Password))
            {
                MessageBox.Show("Incorrect password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            return storedUser;
        }

    }
}
