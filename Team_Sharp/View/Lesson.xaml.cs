using Team_Sharp.Utility;
using Team_Sharp.View.Lessons;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;

namespace Team_Sharp.View
{
    public partial class Lesson : UserControl
    {
        public Lesson()
        {
            InitializeComponent();

            CheckLessonCompletion();
        }


        private void CheckLessonCompletion()
        {
            for (int i = 1; i <= 15; i++)
            {
                string lessonName = "Lesson" + i;
                string filePath = $@"../../../DataBase/Language/{Global._language}/LessonLock/{Global._userName}/{lessonName}.txt";
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    bool isCompleted = false;

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        string status = parts[1].Trim();

                        if (status == "true")
                        {
                            isCompleted = true;
                            break;

                        }
                    }

                    string currentLessonButton = lessonName;
                    string nextLessonButton = "Lesson" + (i + 1);
                    Button currentButton = (Button)FindName(currentLessonButton);
                    Button nextButton = (Button)FindName(nextLessonButton);

                    if (nextButton != null)
                    {
                        if (isCompleted)
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


        public void lesson1Click(object sender, RoutedEventArgs e)
        {
;           GlobalLesson._lessonFile = "Lesson1";
            new LessonPage().ShowDialog();
        }

        public void lesson2Click(object sender, RoutedEventArgs e)
        {
            GlobalLesson._lessonFile = "Lesson2";
            new LessonPage().ShowDialog();
        }

        public void lesson3Click(object sender, RoutedEventArgs e)
        {
            GlobalLesson._lessonFile = "Lesson3";
            new LessonPage().ShowDialog();
        }

        public void lesson4Click(object sender, RoutedEventArgs e)
        {
            GlobalLesson._lessonFile = "Lesson4";
            new LessonPage().ShowDialog();
        }

        public void lesson5Click(object sender, RoutedEventArgs e)
        {
            GlobalLesson._lessonFile = "Lesson5";
            new LessonPage().ShowDialog();
        }

        public void lesson6Click(object sender, RoutedEventArgs e)
        {
            GlobalLesson._lessonFile = "Lesson6";
            new LessonPage().ShowDialog();
        }

        public void lesson7Click(object sender, RoutedEventArgs e)
        {
            GlobalLesson._lessonFile = "Lesson7";
            new LessonPage().ShowDialog();
        }

        public void lesson8Click(object sender, RoutedEventArgs e)
        {
            GlobalLesson._lessonFile = "Lesson8";
            new LessonPage().ShowDialog();
        }

        public void lesson9Click(object sender, RoutedEventArgs e)
        {
            GlobalLesson._lessonFile = "Lesson9";
            new LessonPage().ShowDialog();
        }

        public void lesson10Click(object sender, RoutedEventArgs e)
        {
            GlobalLesson._lessonFile = "Lesson10";
            new LessonPage().ShowDialog();
        }

        public void lesson11Click(object sender, RoutedEventArgs e)
        {
            GlobalLesson._lessonFile = "Lesson11";
            new LessonPage().ShowDialog();
        }

        public void lesson12Click(object sender, RoutedEventArgs e)
        {
            GlobalLesson._lessonFile = "Lesson12";
            new LessonPage().ShowDialog();
        }

        public void lesson13Click(object sender, RoutedEventArgs e)
        {
            GlobalLesson._lessonFile = "Lesson13";
            new LessonPage().ShowDialog();
        }

        public void lesson14Click(object sender, RoutedEventArgs e)
        {
            GlobalLesson._lessonFile = "Lesson14";
            new LessonPage().ShowDialog();
        }

        public void lesson15Click(object sender, RoutedEventArgs e)
        {
            GlobalLesson._lessonFile = "Lesson15";
            new LessonPage().ShowDialog();
        }
    }
}
