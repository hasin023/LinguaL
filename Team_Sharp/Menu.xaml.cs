using Team_Sharp.View;
using System.Windows;
using System.Windows.Input;
using Team_Sharp.Handlers;
using Team_Sharp.Model;

namespace Team_Sharp
{
    public partial class Menu : Window
    {
        private User loggedInUser;
        private FileReaderHandler fileReaderHandler;

        public Menu(User loggedInUser)
        {
            InitializeComponent();
            this.loggedInUser = loggedInUser;
            fileReaderHandler = new FileReaderHandler();

            HandleOnStartUP();
        }

        public void HandleOnStartUP()
        {
            UserNameText.Text = loggedInUser.Name;
            string progress = $@"../../../DataBase/Language/{loggedInUser.Language}/Progress/{loggedInUser.Username}.txt";

            if (!fileReaderHandler.UserFileExists(progress))
            {
                MessageBox.Show("No records found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            fileReaderHandler.ReadProgress(progress, loggedInUser);

            userBlock.Text = loggedInUser.Username;
            langBlock.Text = loggedInUser.Language;

            fileReaderHandler.SetUserImage(loggedInUser.Gender, userImage);
        }

        

        // Menu Buttons
        private void dashboardClick(object sender, RoutedEventArgs e)
        {
            menuCon.Content = new Dashboard(loggedInUser);
        }

        private void lessonClick(object sender, RoutedEventArgs e)
        {
            menuCon.Content = new Lesson(loggedInUser);
        }

        private void examClick(object sender, RoutedEventArgs e)
        {
            menuCon.Content = new Exam(loggedInUser);
        }


        // Window Buttons
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool isMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (isMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Height = 720;
                    this.Width = 1080;

                    isMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    isMaximized = true;
                }
            }

        }


        // Exit Application
        private void exitApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Logout
        private void logoutClick(object sender, RoutedEventArgs e)
        {
            loggedInUser = null;
            this.Hide();
            new Login().Show();
        }


    }
}
