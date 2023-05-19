using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using System.IO;
using Team_Sharp.Utility;
using System;

namespace Team_Sharp
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

            if (Properties.Settings.Default.Username != string.Empty)
            {
                txtLUsername.Text = Properties.Settings.Default.Username;
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }


        public void checkUser(string str)
        {

            string userFilePath = $@"../../../DataBase/User/{str}.txt";

            if (!File.Exists(userFilePath))
            {
                MessageBox.Show("Account does not exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string[] userLines = File.ReadAllLines(userFilePath);
            string password = null;
            string gender = null;
            string name = null;
            string email = null;
            string DOB = null;


            foreach (string uL in userLines)
            {
                if (uL.StartsWith("Username:"))
                {
                    string storedUsername = uL.Remove(0,9);

                    if (txtLUsername.Text != storedUsername)
                    {
                        MessageBox.Show("Incorrect username", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else if (uL.StartsWith("Password:"))
                {
                    password = uL.Remove(0, 9);
                }
                else if (uL.StartsWith("Gender:"))
                {
                    gender = uL.Remove(0, 7);
                }
                else if (uL.StartsWith("Name:"))
                {
                    name = uL.Remove(0, 5);
                }
                else if (uL.StartsWith("Email:"))
                {
                    email = uL.Remove(0, 6);
                }
                else if (uL.StartsWith("DOB:"))
                {
                    DOB = uL.Remove(0, 4);
                }
            }

            if (password == null)
            {
                MessageBox.Show("Please enter valid password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtLPassword.Password != password)
            {
                MessageBox.Show("Incorrect password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Global._userName = txtLUsername.Text;
            Global._gender = gender;
            Global._name = name;
            Global._email = email;
            Global._DOB = DOB;

            this.Hide();
            new LanguageSelection().Show();
            
        }
        private void loginClick(object sender, RoutedEventArgs e)
        {
            if (checkBox.IsChecked == true)
            {
                Team_Sharp.Properties.Settings.Default.Username = txtLUsername.Text;
                Team_Sharp.Properties.Settings.Default.Save();
            }

            checkUser(txtLUsername.Text);
            
        }
        private void txtEnterKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtLPassword.Focus();
            }
        }
        private void passEnterKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                loginClick(sender, e);
            }
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RegisterLink_Click(object sender, RequestNavigateEventArgs e)
        {
            this.Hide();
            new Registration().Show();
            e.Handled = true;
        }
    }
}
