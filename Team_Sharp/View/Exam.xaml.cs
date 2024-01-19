using Team_Sharp.View.Exams;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Team_Sharp.Model;
using Team_Sharp.Handlers;

namespace Team_Sharp.View
{
    public partial class Exam : UserControl
    {
        private readonly User loggedInUser;
        private LessonExamHandler lessonExamHandler;
        private FileReaderHandler fileReaderHandler;
        private FileWriterHandler fileWriterHandler;

        public Exam(User loggedInUser, LessonExamHandler lessonExamHandler, FileReaderHandler fileReaderHandler, FileWriterHandler fileWriterHandler)
        {
            InitializeComponent();
            this.loggedInUser = loggedInUser;
            this.lessonExamHandler = lessonExamHandler;
            this.fileReaderHandler = fileReaderHandler;
            this.fileWriterHandler = fileWriterHandler;

            HandleExamLocks();
        }

        // Handling the exam locks on Startup
        private void HandleExamLocks()
        {
            for (int i = 1; i <= 15; i++)
            {
                string examName = "Exam" + i;
                string filePath = $@"../../../DataBase/Language/{loggedInUser.Language}/ExamLock/{loggedInUser.Username}/{examName}.txt";
                if (fileReaderHandler.UserFileExists(filePath))
                {
                    bool isComplete = lessonExamHandler.IsComplete(filePath);

                    string currentExamButton = examName;
                    string nextExamButton = "Exam" + (i + 1);
                    Button currentButton = (Button)FindName(currentExamButton);
                    Button nextButton = (Button)FindName(nextExamButton);

                    if (currentExamButton == "Exam15" && isComplete == true)
                    {
                        ExamDone.Visibility = Visibility.Visible;
                        ExamDone2.Visibility = Visibility.Visible;
                        currentButton.Style = (Style)FindResource("ExamAccentButton");
                        currentButton.IsEnabled = false;
                    }

                    if (nextButton != null)
                    {
                        if (isComplete)
                        {
                            currentButton.Style = (Style)FindResource("ExamAccentButton");
                            currentButton.IsEnabled = false;
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


        public void OpenExamDialog(object sender, RoutedEventArgs e, string examName, string op1, string op2, string op3, string op4, string op5)
        {
            new ExamQ(loggedInUser, fileReaderHandler, fileWriterHandler, examName, op1, op2, op3, op4, op5).ShowDialog();
        }

        public void exam1Click(object sender, RoutedEventArgs e)
        {
            OpenExamDialog(sender, e, "Exam1", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1");
        }

        public void exam2Click(object sender, RoutedEventArgs e)
        {
            OpenExamDialog(sender, e, "Exam2", "q1op2", "q2op2", "q3op2", "q4op2", "q5op2");
        }

        public void exam3Click(object sender, RoutedEventArgs e)
        {
            OpenExamDialog(sender, e, "Exam3", "q1op3", "q2op3", "q3op3", "q4op3", "q5op3");
        }

        public void exam4Click(object sender, RoutedEventArgs e)
        {
            OpenExamDialog(sender, e, "Exam4", "q1op1", "q2op2", "q3op3", "q4op3", "q5op2");
        }

        public void exam5Click(object sender, RoutedEventArgs e)
        {
            OpenExamDialog(sender, e, "Exam5", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1");
        }

        public void exam6Click(object sender, RoutedEventArgs e)
        {
            OpenExamDialog(sender, e, "Exam6", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1");
        }

        public void exam7Click(object sender, RoutedEventArgs e)
        {
            OpenExamDialog(sender, e, "Exam7", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1");
        }

        public void exam8Click(object sender, RoutedEventArgs e)
        {
            OpenExamDialog(sender, e, "Exam8", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1");
        }

        public void exam9Click(object sender, RoutedEventArgs e)
        {
            OpenExamDialog(sender, e, "Exam9", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1");
        }

        public void exam10Click(object sender, RoutedEventArgs e)
        {
            OpenExamDialog(sender, e, "Exam10", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1");
        }

        public void exam11Click(object sender, RoutedEventArgs e)
        {
            OpenExamDialog(sender, e, "Exam11", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1");
        }

        public void exam12Click(object sender, RoutedEventArgs e)
        {
            OpenExamDialog(sender, e, "Exam12", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1");
        }

        public void exam13Click(object sender, RoutedEventArgs e)
        {
            OpenExamDialog(sender, e, "Exam13", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1");
        }

        public void exam14Click(object sender, RoutedEventArgs e)
        {
            OpenExamDialog(sender, e, "Exam14", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1");
        }

        public void exam15Click(object sender, RoutedEventArgs e)
        {
            OpenExamDialog(sender, e, "Exam15", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1");
        }

    }
}
