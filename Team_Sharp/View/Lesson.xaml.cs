using Team_Sharp.Utility;
using Team_Sharp.Handlers;
using Team_Sharp.Model;
using Team_Sharp.View.Lessons;
using System.Windows;
using System.IO;
using System.Windows.Controls;

namespace Team_Sharp.View
{
    public partial class Lesson : UserControl
    {
        private readonly User loggedInUser;

        public Lesson(User loggedInUser)
        {
            InitializeComponent();
            this.loggedInUser = loggedInUser;

            CheckLessonCompletion();
        }


        // NEED TO FIX THIS
        private void CheckLessonCompletion()
        {
            foreach (var _lesson in loggedInUser.Lessons)
            {
                string currentLessonButton = _lesson.LessonName;
                string nextLessonButton = "Lesson" + (int.Parse(_lesson.LessonName.Substring("Lesson".Length)) + 1);

                Button currentButton = (Button)FindName(currentLessonButton);
                Button nextButton = (Button)FindName(nextLessonButton);

                if (currentLessonButton == "Lesson15" && _lesson.IsCompleted)
                {
                    LessonDone.Visibility = Visibility.Visible;
                    LessonDone2.Visibility = Visibility.Visible;
                    currentButton.Style = (Style)FindResource("ExamAccentButton");
                }

                if (nextButton != null)
                {
                    if (_lesson.IsCompleted)
                    {
                        currentButton.Style = (Style)FindResource("ExamAccentButton");
                        nextButton.IsEnabled = true;
                    }
                    else
                    {
                        nextButton.IsEnabled = false;
                    }
                }
            }
        }


        // Lesson Buttons
        private void LessonClick(string lessonName)
        {
            new LessonPage(loggedInUser, lessonName).ShowDialog();
        }

        public void lesson1Click(object sender, RoutedEventArgs e) => LessonClick("Lesson1");

        public void lesson2Click(object sender, RoutedEventArgs e) => LessonClick("Lesson2");

        public void lesson3Click(object sender, RoutedEventArgs e) => LessonClick("Lesson3");

        public void lesson4Click(object sender, RoutedEventArgs e) => LessonClick("Lesson4");

        public void lesson5Click(object sender, RoutedEventArgs e) => LessonClick("Lesson5");

        public void lesson6Click(object sender, RoutedEventArgs e) => LessonClick("Lesson6");

        public void lesson7Click(object sender, RoutedEventArgs e) => LessonClick("Lesson7");

        public void lesson8Click(object sender, RoutedEventArgs e) => LessonClick("Lesson8");

        public void lesson9Click(object sender, RoutedEventArgs e) => LessonClick("Lesson9");

        public void lesson10Click(object sender, RoutedEventArgs e) => LessonClick("Lesson10");

        public void lesson11Click(object sender, RoutedEventArgs e) => LessonClick("Lesson11");

        public void lesson12Click(object sender, RoutedEventArgs e) => LessonClick("Lesson12");

        public void lesson13Click(object sender, RoutedEventArgs e) => LessonClick("Lesson13");

        public void lesson14Click(object sender, RoutedEventArgs e) => LessonClick("Lesson14");

        public void lesson15Click(object sender, RoutedEventArgs e) => LessonClick("Lesson15");


    }
}
