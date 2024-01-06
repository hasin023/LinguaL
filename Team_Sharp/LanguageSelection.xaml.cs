using Team_Sharp.Utility;
using Team_Sharp.View.Exams;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Team_Sharp
{
    public partial class LanguageSelection : Window
    {
        private readonly User loggedInUser;
        private readonly LessonExamHandler lessonExamHandler;

        public LanguageSelection(User loggedInUser)
        {
            InitializeComponent();
            this.loggedInUser = loggedInUser;
            this.lessonExamHandler = new LessonExamHandler(loggedInUser);
        }

        private void startClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            loggedInUser.Language = languageComboBox.Text;


            string userProgress = $@"../../../DataBase/Language/{loggedInUser.Language}/Progress/{loggedInUser.Username}.txt";
            if (File.Exists(userProgress))
            {
                new Menu(loggedInUser).Show();
            }
            else
            {
                lessonExamHandler.MakeLessonFiles();
                lessonExamHandler.MakeExamFiles();

                new PlacementTest("q1op1", "q2op1", "q3op1", "q4op1", "q5op1").Show();
            }
        }



        // Window Buttons
        private void exitClick(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }


    }
}
