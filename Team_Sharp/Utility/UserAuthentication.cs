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


    }
}
