using Team_Sharp.View.Exams;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Team_Sharp.Model;
using System.Linq;

namespace Team_Sharp.View
{
    public partial class Exam : UserControl
    {
        private readonly User loggedInUser;

        public Exam(User loggedInUser)
        {
            InitializeComponent();
            this.loggedInUser = loggedInUser;

            CheckExamCompletion();
        }

        private void CheckExamCompletion()
        {
            foreach (var exam in loggedInUser.Exams)
            {
                string examName = exam.ExamName;
                string filePath = $@"../../../DataBase/Language/{loggedInUser.Language}/ExamLock/{loggedInUser.Username}/{examName}.txt";

                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    bool isCompleted = lines.Any(line => line.Split(',')[1].Trim() == "true");

                    string currentExamButton = examName;
                    string nextExamButton = $"Exam{loggedInUser.Exams.IndexOf(exam) + 2}";
                    Button currentButton = (Button)FindName(currentExamButton);
                    Button nextButton = (Button)FindName(nextExamButton);

                    if (currentExamButton == $"Exam{loggedInUser.Exams.Count}" && isCompleted)
                    {
                        ExamDone.Visibility = Visibility.Visible;
                        ExamDone2.Visibility = Visibility.Visible;
                        currentButton.Style = (Style)FindResource("ExamAccentButton");
                        currentButton.IsEnabled = false;
                    }

                    if (nextButton != null)
                    {
                        if (isCompleted)
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

                    exam.IsCompleted = isCompleted;
                }
            }
        }


        public void exam1Click(object sender, RoutedEventArgs e)
        {
            new ExamQ(loggedInUser, "Exam1", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam2Click(object sender, RoutedEventArgs e)
        {
            new ExamQ(loggedInUser, "Exam2", "q1op2", "q2op2", "q3op2", "q4op2", "q5op2").ShowDialog();
        }

        public void exam3Click(object sender, RoutedEventArgs e)
        {
            new ExamQ(loggedInUser, "Exam3", "q1op3", "q2op3", "q3op3", "q4op3", "q5op3").ShowDialog();
        }

        public void exam4Click(object sender, RoutedEventArgs e)
        {
            new ExamQ(loggedInUser, "Exam4", "q1op1", "q2op2", "q3op3", "q4op3", "q5op2").ShowDialog();
        }

        public void exam5Click(object sender, RoutedEventArgs e)
        {
            new ExamQ(loggedInUser, "Exam5", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam6Click(object sender, RoutedEventArgs e)
        {
            new ExamQ(loggedInUser, "Exam6", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam7Click(object sender, RoutedEventArgs e)
        {
            new ExamQ(loggedInUser, "Exam7", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam8Click(object sender, RoutedEventArgs e)
        {
            new ExamQ(loggedInUser, "Exam8", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam9Click(object sender, RoutedEventArgs e)
        {
            new ExamQ(loggedInUser, "Exam9", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam10Click(object sender, RoutedEventArgs e)
        {
            new ExamQ(loggedInUser, "Exam10", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam11Click(object sender, RoutedEventArgs e)
        {
            new ExamQ(loggedInUser, "Exam11", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam12Click(object sender, RoutedEventArgs e)
        {
            new ExamQ(loggedInUser, "Exam12", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam13Click(object sender, RoutedEventArgs e)
        {
            new ExamQ(loggedInUser, "Exam13", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam14Click(object sender, RoutedEventArgs e)
        {
            new ExamQ(loggedInUser, "Exam14", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam15Click(object sender, RoutedEventArgs e)
        {
            new ExamQ(loggedInUser, "Exam15", "q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }
    }
}
