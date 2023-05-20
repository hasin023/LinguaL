using Team_Sharp.Utility;
using Team_Sharp.View.Exams;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignColors;
using MaterialDesignThemes;

namespace Team_Sharp.View
{
    public partial class Exam : UserControl
    {
        public Exam()
        {
            InitializeComponent();

            CheckExamCompletion();

        }

        private void CheckExamCompletion()
        {
            for (int i = 1; i <= 15; i++)
            {
                string examName = "Exam" + i;
                string filePath = $@"../../../DataBase/Language/{Global._language}/ExamLock/{Global._userName}/{examName}.txt";
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

                    string currentExamButton = examName;
                    string nextExamButton = "Exam" + (i+1);
                    Button currentButton = (Button)FindName(currentExamButton);
                    Button nextButton = (Button)FindName(nextExamButton);

                    if (currentExamButton == "Exam15" && isCompleted == true)
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
                }
            }
        }


        public void exam1Click(object sender, RoutedEventArgs e)
        {
            GlobalQuesFile._question = "Exam1";
            new ExamQ("q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam2Click(object sender, RoutedEventArgs e)
        {
            GlobalQuesFile._question = "Exam2";
            new ExamQ("q1op2", "q2op2", "q3op2", "q4op2", "q5op2").ShowDialog();
        }

        public void exam3Click(object sender, RoutedEventArgs e)
        {
            GlobalQuesFile._question = "Exam3";
            new ExamQ("q1op3", "q2op3", "q3op3", "q4op3", "q5op3").ShowDialog();
        }

        public void exam4Click(object sender, RoutedEventArgs e)
        {
            GlobalQuesFile._question = "Exam4";
            new ExamQ("q1op1", "q2op2", "q3op3", "q4op3", "q5op2").ShowDialog();
        }

        public void exam5Click(object sender, RoutedEventArgs e)
        {
            GlobalQuesFile._question = "Exam5";
            new ExamQ("q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam6Click(object sender, RoutedEventArgs e)
        {
            GlobalQuesFile._question = "Exam6";
            new ExamQ("q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam7Click(object sender, RoutedEventArgs e)
        {
            GlobalQuesFile._question = "Exam7";
            new ExamQ("q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam8Click(object sender, RoutedEventArgs e)
        {
            GlobalQuesFile._question = "Exam8";
            new ExamQ("q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam9Click(object sender, RoutedEventArgs e)
        {
            GlobalQuesFile._question = "Exam9";
            new ExamQ("q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam10Click(object sender, RoutedEventArgs e)
        {
            GlobalQuesFile._question = "Exam10";
            new ExamQ("q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam11Click(object sender, RoutedEventArgs e)
        {
            GlobalQuesFile._question = "Exam11";
            new ExamQ("q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam12Click(object sender, RoutedEventArgs e)
        {
            GlobalQuesFile._question = "Exam12";
            new ExamQ("q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam13Click(object sender, RoutedEventArgs e)
        {
            GlobalQuesFile._question = "Exam13";
            new ExamQ("q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam14Click(object sender, RoutedEventArgs e)
        {
            GlobalQuesFile._question = "Exam14";
            new ExamQ("q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }

        public void exam15Click(object sender, RoutedEventArgs e)
        {
            GlobalQuesFile._question = "Exam15";
            new ExamQ("q1op1", "q2op1", "q3op1", "q4op1", "q5op1").ShowDialog();
        }
    }
}
