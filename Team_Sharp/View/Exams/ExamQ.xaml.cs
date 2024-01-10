using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Team_Sharp.Handlers;
using Team_Sharp.Model;
using Team_Sharp.Utility;

namespace Team_Sharp.View.Exams
{
    public partial class ExamQ : Window
    {
        private readonly int POINT_TO_ADD = 12;
        private readonly int PASSING_POINT = 36;

        private User loggedInUser;
        private string questionNo;
        private FileWriterHandler fileWriterHandler;
        private FileReaderHandler fileReaderHandler;
        private ExamManagement examManagement;

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
            this.fileWriterHandler = new FileWriterHandler();
            this.fileReaderHandler = new FileReaderHandler();
            this.examManagement = new ExamManagement(loggedInUser, POINT_TO_ADD);

            _b1 = (RadioButton)FindName(b1);
            _b2 = (RadioButton)FindName(b2);
            _b3 = (RadioButton)FindName(b3);
            _b4 = (RadioButton)FindName(b4);
            _b5 = (RadioButton)FindName(b5);

            LoadQuestions();
        }


        // Window Buttons
        public void submitClick(object sender, RoutedEventArgs e)
        {
            CheckAllAnswers();

            if (loggedInUser.ExamResult.EarnedPoints >= PASSING_POINT)
            {
                SavePassingProgress();
                UpdateExamStatus();
            }
            else
            {
                SaveFailingProgress();
            }
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


        // Load Exam Questions
        private void LoadQuestions()
        {
            LessonExamHandler lessonExamHandler = new LessonExamHandler(loggedInUser);

            string eQuestion = $@"../../../DataBase/Language/{loggedInUser.Language}/Question/{questionNo}.txt";
            List<Question> questions = lessonExamHandler.ParseQuestionsFromFile(eQuestion);

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
        }


        // Update the exam status & save the user activity
        private void UpdateExamStatus()
        {
            string filePath = $@"../../../DataBase/Language/{loggedInUser.Language}/ExamLock/{loggedInUser.Username}/{questionNo}.txt";
            fileWriterHandler.ReplaceLineInFile(filePath, $"{loggedInUser.Username},false", $"{loggedInUser.Username},true");
        }

        private void SaveUserActivity()
        {
            string filePath = $@"../../../DataBase/DashBoardActivity/{loggedInUser.Language}/{loggedInUser.Username}.txt";
            string textToAppend = $"{DateTime.Now},{loggedInUser.Activity.Name}";
            fileWriterHandler.AppendTextToFile(filePath, textToAppend);
        }


        // Exam Management Logic
        private void CheckAllAnswers()
        {
            examManagement.ResetProgress(loggedInUser);

            examManagement.CheckCorrectOption(_b1);
            examManagement.CheckCorrectOption(_b2);
            examManagement.CheckCorrectOption(_b3);
            examManagement.CheckCorrectOption(_b4);
            examManagement.CheckCorrectOption(_b5);

            if (loggedInUser.ExamResult.EarnedPoints >= PASSING_POINT)
            {
                MessageBox.Show($"Answered {loggedInUser.ExamResult.CorrectAnswersCount} questions correctly\nScored {loggedInUser.ExamResult.EarnedPoints} points\nStatus: PASSED", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show($"Answered {loggedInUser.ExamResult.InCorrectAnswersCount} questions incorrectly\nScored {loggedInUser.ExamResult.EarnedPoints} points\nStatus: FAILED", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }

        }

        private void SavePassingProgress()
        {
            string progress = $@"../../../DataBase/Language/{loggedInUser.Language}/Progress/{loggedInUser.Username}.txt";
            fileReaderHandler.ReadProgress(progress, loggedInUser);

            int prevLevel = loggedInUser.Progress.UserProgressLevel;
            string prevProficiency = loggedInUser.Progress.UserProgressProficiency;
            int prevExp = loggedInUser.Progress.UserExperience;

            examManagement.CalculatePassingProgress(progress);

            fileWriterHandler.ReplaceLineInFile(progress, $"EXP:{prevExp}", $"EXP:{loggedInUser.Progress.UserExperience}");
            fileWriterHandler.ReplaceLineInFile(progress, $"Level:{prevLevel}", $"Level:{loggedInUser.Progress.UserProgressLevel}");
            fileWriterHandler.ReplaceLineInFile(progress, $"Proficiency:{prevProficiency}", $"Proficiency:{loggedInUser.Progress.UserProgressProficiency}");

            loggedInUser.Activity.Name = questionNo;
            SaveUserActivity();

            MessageBox.Show($"You have reached level {loggedInUser.Progress.UserProgressLevel}\nYour proficiency level is {loggedInUser.Progress.UserProgressProficiency}", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SaveFailingProgress()
        {
            string progress = $@"../../../DataBase/Language/{loggedInUser.Language}/Progress/{loggedInUser.Username}.txt";
            fileReaderHandler.ReadProgress(progress, loggedInUser);

            MessageBox.Show($"You are level {loggedInUser.Progress.UserProgressLevel}\nYour proficiency level is {loggedInUser.Progress.UserProgressProficiency}", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        // Show/Reset Button Logic
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

            loggedInUser.ExamResult.EarnedPoints -= 5;
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



    }
}
