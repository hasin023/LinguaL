using Team_Sharp.Model;
using Team_Sharp.View.Lessons;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Team_Sharp.Handlers;

namespace Team_Sharp.View
{
    public partial class Lesson : UserControl
    {
        private readonly User loggedInUser;
        private LessonExamHandler lessonExamHandler;

        public Lesson(User loggedInUser)
        {
            InitializeComponent();
            this.loggedInUser = loggedInUser;
            this.lessonExamHandler = new LessonExamHandler(loggedInUser);

            HandleLessonLocks();
        }


        // Handle Lesson Locks on Startup
        private void HandleLessonLocks()
        {
            for (int i = 1; i <= 15; i++)
            {
                string lessonName = "Lesson" + i;
                string filePath = $@"../../../DataBase/Language/{loggedInUser.Language}/LessonLock/{loggedInUser.Username}/{lessonName}.txt";
                if (File.Exists(filePath))
                {
                    bool isComplete = lessonExamHandler.IsComplete(filePath);

                    string currentLessonButton = lessonName;
                    string nextLessonButton = "Lesson" + (i + 1);
                    Button currentButton = (Button)FindName(currentLessonButton);
                    Button nextButton = (Button)FindName(nextLessonButton);

                    if (currentLessonButton == "Lesson15" && isComplete == true)
                    {
                        LessonDone.Visibility = Visibility.Visible;
                        LessonDone2.Visibility = Visibility.Visible;
                        currentButton.Style = (Style)FindResource("ExamAccentButton");
                    }

                    if (nextButton != null)
                    {
                        if (isComplete)
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
