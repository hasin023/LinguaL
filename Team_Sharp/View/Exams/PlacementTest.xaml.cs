using Team_Sharp.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Team_Sharp.Model;

namespace Team_Sharp.View.Exams
{
    public partial class PlacementTest : Window
    {
        private User loggedInUser;
        private string questionName = "PlacementTest";

        public RadioButton _b1 { get; set; }
        public RadioButton _b2 { get; set; }
        public RadioButton _b3 { get; set; }
        public RadioButton _b4 { get; set; }
        public RadioButton _b5 { get; set; }

        public PlacementTest(User loggedInUser, string b1, string b2, string b3, string b4, string b5)
        {
            InitializeComponent();
            this.loggedInUser = loggedInUser;

            _b1 = (RadioButton)FindName(b1);
            _b2 = (RadioButton)FindName(b2);
            _b3 = (RadioButton)FindName(b3);
            _b4 = (RadioButton)FindName(b4);
            _b5 = (RadioButton)FindName(b5);

            LoadQuestions();

        }


        public List<Question> LoadQuestions()
        {
            string eQuestion = $@"../../../DataBase/Language/{loggedInUser.Language}/Question/{questionName}.txt";

            List<Question> questions = new List<Question>();
            string[] lines = File.ReadAllLines(eQuestion);
            Question currentQuestion = null;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (line.StartsWith("Q."))
                {
                    // New question
                    if (currentQuestion != null)
                    {
                        questions.Add(currentQuestion);
                    }
                    currentQuestion = new Question(line.Substring(2).Trim(), new List<Option>());
                }
                else if (line.StartsWith("A."))
                {
                    // Option for the current question
                    if (currentQuestion != null && currentQuestion.Options.Count < 3)
                    {
                        currentQuestion.Options.Add(new Option(line.Substring(2).Trim()));
                    }

                }

            }
            if (currentQuestion != null)
            {
                questions.Add(currentQuestion);
            }

            for (int i = 0; i < questions.Count; i++)
            {
                TextBlock questionTextBlock = (TextBlock)FindName($"q{i + 1}Text");
                questionTextBlock.Text = questions[i].Text;
                for (int j = 0; j < questions[i].Options.Count; j++)
                {
                    RadioButton optionRadioButton = (RadioButton)FindName($"q{i + 1}op{j + 1}");
                    optionRadioButton.Content = questions[i].Options[j].Text;
                    optionRadioButton.Tag = j;
                }
            }

            return questions;
        }


        int correctAns = 0;
        int wrongAns = 0;
        int points = 0;
        int pointToGive = 20;
        public void checkAnswer()
        {
            CheckQuestion(_b1, pointToGive);
            CheckQuestion(_b2, pointToGive);
            CheckQuestion(_b3, pointToGive);
            CheckQuestion(_b4, pointToGive);
            CheckQuestion(_b5, pointToGive);

            if (points >= 60)
            {
                MessageBox.Show($"Answered {correctAns} questions correctly\nScored {points} points\nStatus: PASSED", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show($"Answered {wrongAns} questions wrong\nScored {points} points\nStatus: FAILED", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void CheckQuestion(RadioButton radioButton, int pointsToAdd)
        {
            if (radioButton.IsChecked == true)
            {
                correctAns++;
                points += pointsToAdd;
            }
            else
            {
                wrongAns++;
            }
        }

        private void saveUserActivity()
        {
            string filePath = $@"../../../DataBase/DashBoardActivity/{loggedInUser.Language}/{loggedInUser.Username}.txt";
            string textToAppend = $"{DateTime.Now},{loggedInUser.CurrentActivity}";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(textToAppend);
            }
        }
        private void SavePassedUserProgress()
        {
            loggedInUser.Experience = points;
            loggedInUser.UserProgressLevel = 1;
            loggedInUser.UserProgressProficiency = "A1";

            string progress = $@"../../../DataBase/Language/{loggedInUser.Language}/Progress/{loggedInUser.Username}.txt";

            using (StreamWriter sw = File.AppendText(progress))
            {
                sw.WriteLine($"EXP:{loggedInUser.Experience}");
                sw.WriteLine($"Level:{loggedInUser.UserProgressLevel}");
                sw.WriteLine($"Proficiency:{loggedInUser.UserProgressProficiency}");
            }

            //Saving the Exam Activity
            loggedInUser.CurrentActivity = questionName;
            saveUserActivity();

            MessageBox.Show($"You have reached level {loggedInUser.UserProgressLevel}\nYour proficiency level is {loggedInUser.UserProgressProficiency}", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void SaveFailedUserProgress()
        {
            loggedInUser.Experience = points;
            loggedInUser.UserProgressLevel = 0;
            loggedInUser.UserProgressProficiency = "None";

            string progress = $@"../../../DataBase/Language/{loggedInUser.Language}/Progress/{loggedInUser.Username}.txt";

            using (StreamWriter sw = File.AppendText(progress))
            {
                sw.WriteLine($"EXP:{loggedInUser.Experience}");
                sw.WriteLine($"Level:{loggedInUser.UserProgressLevel}");
                sw.WriteLine($"Proficiency:{loggedInUser.UserProgressProficiency}");
            }

            MessageBox.Show($"You are level {loggedInUser.UserProgressLevel}\nYour proficiency level is {loggedInUser.UserProgressProficiency}", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void submitClick(object sender, RoutedEventArgs e)
        {
            checkAnswer();
            if (points >= 60)
            {
                SavePassedUserProgress();
            }
            else
            {
                SaveFailedUserProgress();
            }

            new Menu(loggedInUser).Show();
        }

        private void MarkCorrectOptions(RadioButton radioButton)
        {
            radioButton.Style = (Style)FindResource("MaterialDesignAccentRadioButton");
            radioButton.IsChecked = true;
        }

        private void UnmarkCorrectOptions(RadioButton radioButton)
        {
            radioButton.Style = (Style)FindResource("MaterialDesignDarkRadioButton");
            radioButton.IsChecked = false;
        }

        private void ShowButtonMethod()
        {
            MarkCorrectOptions(_b1);
            MarkCorrectOptions(_b2);
            MarkCorrectOptions(_b3);
            MarkCorrectOptions(_b4);
            MarkCorrectOptions(_b5);

            points -= 5;
            SubmitButton.IsEnabled = false;
        }

        private void ResetButtonMethod()
        {
            UnmarkCorrectOptions(_b1);
            UnmarkCorrectOptions(_b2);
            UnmarkCorrectOptions(_b3);
            UnmarkCorrectOptions(_b4);
            UnmarkCorrectOptions(_b5);

            SubmitButton.IsEnabled = true;
        }

        public void showResetClick(object sender, RoutedEventArgs e)
        {
            if (SubmitButton.IsEnabled == true)
            {
                ShowButtonMethod();
                ShowResetButton.Content = "Reset";
            }
            else
            {
                ResetButtonMethod();
                ShowResetButton.Content = "Show";
            }
        }
    }
}
