using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Team_Sharp.Handlers;
using Team_Sharp.Model;
using Team_Sharp.Utility;

namespace Team_Sharp.View.Exams
{
    public partial class PlacementTest : Window, IExam, IActivity
    {
        private string QUESTION_NAME = "PlacementTest";
        private readonly int PASSING_POINT = 60;

        private User loggedInUser;
        private FileWriterHandler fileWriterHandler;
        private FileReaderHandler fileReaderHandler;
        private LessonExamHandler lessonExamHandler;
        private ExamManagement examManagement;


        public RadioButton _b1 { get; set; }
        public RadioButton _b2 { get; set; }
        public RadioButton _b3 { get; set; }
        public RadioButton _b4 { get; set; }
        public RadioButton _b5 { get; set; }

        public PlacementTest(User loggedInUser, FileWriterHandler fileWriterHandler, FileReaderHandler fileReaderHandler, LessonExamHandler lessonExamHandler, string b1, string b2, string b3, string b4, string b5)
        {
            InitializeComponent();
            this.loggedInUser = loggedInUser;
            this.fileWriterHandler = fileWriterHandler;
            this.fileReaderHandler = fileReaderHandler;
            this.lessonExamHandler = lessonExamHandler;
            this.examManagement = new ExamManagement(loggedInUser);

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

            string progress = $@"../../../DataBase/Language/{loggedInUser.Language}/Progress/{loggedInUser.Username}.txt";

            if (loggedInUser.ExamResult.EarnedPoints >= PASSING_POINT)
            {
                SaveProgress(progress, 1, "A1");
            }
            else
            {
                SaveProgress(progress, 0, "NONE");
            }

            new Menu(loggedInUser, fileReaderHandler, fileWriterHandler, lessonExamHandler).Show();
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


        // Load Exam Question
        public void LoadQuestions()
        {
            LessonExamHandler lessonExamHandler = new LessonExamHandler(loggedInUser);

            string eQuestion = $@"../../../DataBase/Language/{loggedInUser.Language}/Question/{QUESTION_NAME}.txt";
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


        // Save the user placementTest activity
        public void SaveUserActivity()
        {
            string filePath = $@"../../../DataBase/DashBoardActivity/{loggedInUser.Language}/{loggedInUser.Username}.txt";
            string textToAppend = $"{DateTime.Now},{loggedInUser.Activity.Name}";
            fileWriterHandler.AppendTextToFile(filePath, textToAppend);
        }


        // Exam Management Logic
        public void CheckAllAnswers()
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

        private void SaveProgress(string fileName, int level, string proficiency)
        {
            loggedInUser.Progress.UserExperience = loggedInUser.ExamResult.EarnedPoints;
            loggedInUser.Progress.UserProgressLevel = level;
            loggedInUser.Progress.UserProgressProficiency = proficiency;

            fileWriterHandler.WriteProgress(fileName, loggedInUser);

            loggedInUser.Activity.Name = QUESTION_NAME;
            SaveUserActivity();

            MessageBox.Show($"You have reached level {loggedInUser.Progress.UserProgressLevel}\nYour proficiency level is {loggedInUser.Progress.UserProgressProficiency}", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
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
