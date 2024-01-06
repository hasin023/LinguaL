using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Team_Sharp.Handlers;
using Team_Sharp.Model;
using Team_Sharp.Utility;

namespace Team_Sharp
{
    public partial class Login : Window
    {
        private UserAuthentication userAuthentication = new UserAuthentication();

        public Login()
        {
            InitializeComponent();

            if (Properties.Settings.Default.Username != string.Empty)
            {
                txtLUsername.Text = Properties.Settings.Default.Username;
            }
        }


        private void loginClick(object sender, RoutedEventArgs e)
        {
            if (checkBox.IsChecked == true)
            {
                Properties.Settings.Default.Username = txtLUsername.Text;
                Properties.Settings.Default.Save();
            }

            User enteredUser = new User
            {
                Username = txtLUsername.Text,
                Password = txtLPassword.Password
            };

            User authenticatedUser = userAuthentication.AuthenticateUser(enteredUser);

            if (authenticatedUser != null)
            {
                this.Hide();
                new LanguageSelection(authenticatedUser).Show();
            }

        }



        // Register Link
        private void RegisterLink_Click(object sender, RequestNavigateEventArgs e)
        {
            this.Hide();
            new Registration().Show();
            e.Handled = true;
        }


        // Window Buttons
        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }


        // Enter key event
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


    }
}
