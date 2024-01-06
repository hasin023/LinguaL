using Team_Sharp.Utility;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Team_Sharp
{
    public partial class Registration : Window
    {
        private GenderUtil genderUtil = new GenderUtil();

        public Registration()
        {
            InitializeComponent();
        }


        private void GenderButtonClick(BitmapImage image)
        {
            genderImg.Source = image;
        }

        private void DudeClick(object sender, RoutedEventArgs e)
        {
            if (dudeButton.IsChecked == true)
            {
                GenderButtonClick(genderUtil.SelectMale());
            }
        }

        private void GirlClick(object sender, RoutedEventArgs e)
        {
            if (girlButton.IsChecked == true)
            {
                GenderButtonClick(genderUtil.SelectFemale());
            }
        }

        private void OtherClick(object sender, RoutedEventArgs e)
        {
            if (otherGenButton.IsChecked == true)
            {
                GenderButtonClick(genderUtil.SelectOther());
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

            dudeButton.IsChecked = false;
            girlButton.IsChecked = false;
            otherGenButton.IsChecked = false;

            genderImg.Source = genderUtil.SelectBackground();

            txtEmail.Focus();
        }


        // Register Button
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            UserAuthentication registrationLogic = new UserAuthentication();
            FileWriterHandler fileHandler = new FileWriterHandler();

            User user = registrationLogic.RegisterUser(txtUsername.Text, passBox.Password, conPassBox.Password, txtName.Text, txtEmail.Text, txtDOB.Text, dudeButton.IsChecked ?? false, girlButton.IsChecked ?? false, otherGenButton.IsChecked ?? false);

            if (user != null)
            {
                string newUserFilePath = $"../../../DataBase/User/{user.Username}.txt";
                fileHandler.WriteUserToFile(user, newUserFilePath);

                MessageBox.Show("Account created!!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Hide();
                new Login().Show();
            }
        }


        // Login Link
        private void LoginLink_Click(object sender, RequestNavigateEventArgs e)
        {
            this.Hide();
            new Login().Show();
            e.Handled = true;
        }



        // Window events
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



        // Enter key events

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
