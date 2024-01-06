using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Team_Sharp.Utility;
using Team_Sharp.Model;

namespace Team_Sharp.View.Exams
{
    public partial class ExamQ : Window
    {
        private readonly User loggedInUser;
        private string questionNo;
        public RadioButton _b1 { get; set; }
        public RadioButton _b2 { get; set; }
        public RadioButton _b3 { get; set; }
        public RadioButton _b4 { get; set; }
        public RadioButton _b5 { get; set; }

        public ExamQ(User loggedInUser, string questionNo, string b1, string b2, string b3, string b4, string b5)
        {
            InitializeComponent();

            this.loggedInUser = loggedInUser;
            this.questionNo = questionNo;
            _b1 = (RadioButton)FindName(b1);
            _b2 = (RadioButton)FindName(b2);
            _b3 = (RadioButton)FindName(b3);
            _b4 = (RadioButton)FindName(b4);
            _b5 = (RadioButton)FindName(b5);

            LoadQuestions();
        }

        public List<Question> LoadQuestions()
        {
            string eQuestion = $@"../../../DataBase/Language/{loggedInUser.Language}/Question/{questionNo}.txt";

            List<Question> questions = new List<Question>();
            string[] lines = File.ReadAllLines(eQuestion);
            Question currentQuestion = null;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (line.StartsWith("Q."))
                {
                    if (currentQuestion != null)
                    {
                        questions.Add(currentQuestion);
                    }
                    currentQuestion = new Question(line.Substring(2).Trim(), new List<Option>());
                }
                else if (line.StartsWith("A."))
                {
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
        int pointToGive = 12;
        public void checkAnswer()
        {
            CheckQuestion(_b1, pointToGive);
            CheckQuestion(_b2, pointToGive);
            CheckQuestion(_b3, pointToGive);
            CheckQuestion(_b4, pointToGive);
            CheckQuestion(_b5, pointToGive);

            if (points >= 36)
            {
                MessageBox.Show($"Answered {correctAns} questions correctly\nScored {points} points\nStatus: PASSED", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show($"Answered {wrongAns} questions wrong\nScored {points} points\nStatus: FAILED", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
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


        public static void ReplaceLineInFile(string filePath, string oldLine, string newLine)
        {
            string[] lines = File.ReadAllLines(filePath);

            int index = Array.IndexOf(lines, oldLine);

            if (index >= 0)
            {
                lines[index] = newLine;

                File.WriteAllLines(filePath, lines);
            }
        }


        private void saveUserActivity()
        {
            string filePath = $@"../../../DataBase/DashBoardActivity/{loggedInUser.Language}/{loggedInUser.Username}.txt";
            string textToAppend = $"{DateTime.Now},{GlobalActivity._activity}";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(textToAppend);
            }
        }

        private void UpdateExamStatus()
        {
            string filePath = $@"../../../DataBase/Language/{loggedInUser.Language}/ExamLock/{loggedInUser.UserProgressLevel}/{questionNo}.txt";

            ReplaceLineInFile(filePath,$"{loggedInUser.Username},false",$"{loggedInUser.Username},true");
        }

        private void SavePassedUserProgress()
        {
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

            // A1, A2, B1, B2, C1, and C2
            int newExp = int.Parse(exp) + points;
            int newLevel;
            string newProficiency = null;
            if (newExp >= 1000)
            {
                newLevel = int.Parse(level) + 1;
                newProficiency = "C2";
            }
            else if (newExp >= 800)
            {
                newLevel = int.Parse(level) + 1;
                newProficiency = "C1";
            }
            else if (newExp >= 600)
            {
                newLevel = int.Parse(level) + 1;
                newProficiency = "B2";
            }
            else if (newExp >= 400)
            {
                newLevel = int.Parse(level) + 1;
                newProficiency = "B1";
            }
            else if (newExp >= 200)
            {
                newLevel = int.Parse(level) + 1;
                newProficiency = "A2";
            }
            else if( newExp >= 100)
            {
                newLevel = int.Parse(level) + 1;
                newProficiency = "A1";
            }
            else
            {
                newLevel = int.Parse(level);
                newProficiency = proficiency;
            }

            ReplaceLineInFile(progress, $"EXP:{exp}", $"EXP:{newExp}");
            ReplaceLineInFile(progress, $"Level:{level}", $"Level:{newLevel}");
            loggedInUser.UserProgressLevel = newLevel;
            ReplaceLineInFile(progress, $"Proficiency:{proficiency}", $"Proficiency:{newProficiency}");
            loggedInUser.UserProgressProficiency = newProficiency;

            //Saving the Exam Activity
            loggedInUser.Activity.Name = questionNo;
            saveUserActivity();


            MessageBox.Show($"You have reached level {loggedInUser.UserProgressLevel}\nYour proficiency level is {loggedInUser.UserProgressProficiency}", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void SaveFailedUserProgress()
        {
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


            MessageBox.Show($"You are level {loggedInUser.UserProgressLevel}\nYour proficiency level is {loggedInUser.UserProgressProficiency}", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void submitClick(object sender, RoutedEventArgs e)
        {
            checkAnswer();
            if (points >= 36)
            {
                SavePassedUserProgress();
                //UpdateExamStatus
                UpdateExamStatus();
            }
            else
            {
                SaveFailedUserProgress();
            }
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
            if(SubmitButton.IsEnabled == true)
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
