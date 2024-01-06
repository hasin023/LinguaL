using Team_Sharp.View;
using System;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Windows.Media.Imaging;
using Team_Sharp.Utility;

namespace Team_Sharp
{
    public partial class Menu : Window
    {
        private readonly User loggedInUser;

        public Menu(User loggedInUser)
        {
            InitializeComponent();
            this.loggedInUser = loggedInUser;

            HandleOnStartUP();
        }


        public void HandleOnStartUP()
        {
            UserNameText.Text = loggedInUser.Name;
            string progress = $@"../../../DataBase/Language/{loggedInUser.Language}/Progress/{loggedInUser.Username}.txt";

            if (!File.Exists(progress))
            {
                MessageBox.Show("No records found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string[] proggLines = File.ReadAllLines(progress);

            string exp = null;
            string level = null;
            string proficiency = null;

            foreach (string uL in proggLines)
            {
                if (uL.StartsWith("EXP:"))
                {
                    exp = uL.Remove(0, 4);
                }
                else if (uL.StartsWith("Level:"))
                {
                    level = uL.Remove(0, 6);
                }
                else if (uL.StartsWith("Proficiency:"))
                {
                    proficiency = uL.Remove(0, 12);
                }

            }

            loggedInUser.Experience = int.Parse(exp);
            loggedInUser.UserProgressLevel = int.Parse(level);
            loggedInUser.UserProgressProficiency = proficiency;

            userBlock.Text = Global._userName;

            langBlock.Text = Global._language;

            if (Global._gender == "Male")
            {
                userImage.ImageSource = new BitmapImage(new Uri(@"../../../Team_Sharp/Assets/dudeIcon.png", UriKind.RelativeOrAbsolute));
            }
            else if (Global._gender == "Female")
            {
                userImage.ImageSource = new BitmapImage(new Uri(@"../../../Team_Sharp/Assets/girlIcon.png", UriKind.RelativeOrAbsolute));
            }
            else if (Global._gender == "Other")
            {
                userImage.ImageSource = new BitmapImage(new Uri(@"../../../Team_Sharp/Assets/otherGenIcon.png", UriKind.RelativeOrAbsolute));
            }
        }

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

        private void dashboardClick(object sender, RoutedEventArgs e)
        {
            menuCon.Content = new Dashboard();
        }

        private void lessonClick(object sender, RoutedEventArgs e)
        {
            menuCon.Content = new Lesson();
        }

        private void examClick(object sender, RoutedEventArgs e)
        {
            menuCon.Content = new Exam();
        }

        private void exitApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void logoutClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new Login().Show();
        }
    }
}
