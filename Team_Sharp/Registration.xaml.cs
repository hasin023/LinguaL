using Team_Sharp.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Team_Sharp
{
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Exit_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Minimize_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            txtEmail.Clear();
            txtName.Clear();
            txtUsername.Clear();
            txtDOB.Clear();
            passBox.Clear();
            conPassBox.Clear();

            if (dudeButton.IsChecked == true)
            {
                dudeButton.IsChecked = false;
            }
            else if(girlButton.IsChecked == true)
            {
                girlButton.IsChecked = false;
            }
            else
            {
                otherGenButton.IsChecked = false;
            }

            genderImg.Source = new BitmapImage(new Uri("Assets/background.png", UriKind.RelativeOrAbsolute));

            txtEmail.Focus();
        }

            public List<User> users = new List<User>();

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (passBox.Password.Length >= 6)
            {
                if (passBox.Password == conPassBox.Password)
                {
                    User newUser = new User(txtUsername.Text, passBox.Password, (users.Count + 1), txtEmail.Text, txtName.Text, txtDOB.Text);
                    users.Add(newUser);
                    MessageBox.Show("Draft Saved" + "\n" + "Register to Create Account", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Passwords do not Match!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Password must be at least 6 characters!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void regUser(string str)
        {
            string newUser = $@"../../../DataBase/User/{str}.txt";

            using (StreamWriter writer = new StreamWriter(newUser))
            {
                writer.WriteLine("Username:" + txtUsername.Text);

                if (passBox.Password.Count() >= 6)
                {
                    if (passBox.Password == conPassBox.Password)
                    {
                        writer.WriteLine("Password:" + passBox.Password);
                    }
                    else
                    {
                        MessageBox.Show("Passwords do not Match!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Password must be at least 6 characters!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                writer.WriteLine("Email:" + txtEmail.Text);
                writer.WriteLine("Name:" + txtName.Text);
                writer.WriteLine("DOB:" + txtDOB.Text);

                if (dudeButton.IsChecked == true)
                {
                    writer.WriteLine("Gender:Male");
                }
                else if (girlButton.IsChecked == true)
                {
                    writer.WriteLine("Gender:Female");
                }
                else if (otherGenButton.IsChecked == true)
                {
                    writer.WriteLine("Gender:Other");
                }

            }
        }


        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if (passBox.Password.Length >= 6)
            {
                if (passBox.Password == passBox.Password)
                {
                    regUser(txtUsername.Text);
            
                    MessageBox.Show("Account created!!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Hide();
                    new Login().Show();
                }
                else
                {
                    MessageBox.Show("Passwords not Match!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Password must at least 6 characters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dudeClick(object sender, RoutedEventArgs e)
        {
            if (dudeButton.IsChecked == true)
            {
                genderImg.Source = new BitmapImage(new Uri("Assets/dudeIcon.png", UriKind.RelativeOrAbsolute));
            }
        }

        private void girlClick(object sender, RoutedEventArgs e)
        {
            if (girlButton.IsChecked == true)
            {
                genderImg.Source = new BitmapImage(new Uri("Assets/girlIcon.png", UriKind.RelativeOrAbsolute));
            }

        }

        private void otherClick(object sender, RoutedEventArgs e)
        {
            if (otherGenButton.IsChecked == true)
            {
                genderImg.Source = new BitmapImage(new Uri("Assets/otherGenIcon.png", UriKind.RelativeOrAbsolute));
            }

        }

        private void LoginLink_Click(object sender, RequestNavigateEventArgs e)
        {
            this.Hide();
            new Login().Show();
            e.Handled = true;
        }

        private void emailEnterKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtUsername.Focus();
            }
        }

        private void userEnterKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtName.Focus();
            }
        }

        private void nameEnterKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtDOB.Focus();
            }
        }

        private void dobEnterKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                passBox.Focus();
            }
        }

        private void passEnterKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                conPassBox.Focus();
            }
        }
    }
}
