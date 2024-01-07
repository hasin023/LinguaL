using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Team_Sharp.Model;
using Team_Sharp.Handlers;

namespace Team_Sharp.View.Lessons
{
    public partial class LessonPage : Window
    {
        private User loggedInUser;
        private string lessonName;
        private LessonExamHandler lessonExamHandler;
        private FileWriterHandler fileWriterHandler;

        public LessonPage(User loggedInUser, string lessonName)
        {
            InitializeComponent();
            this.loggedInUser = loggedInUser;
            this.lessonName = lessonName;
            this.lessonExamHandler = new LessonExamHandler(loggedInUser);
            this.fileWriterHandler = new FileWriterHandler();
            
            CheckLessonCompletion();
            LoadLesson();
        }


        public void completeClick(object sender, RoutedEventArgs e)
        {
            SaveLessonComplete();
            UpdateLessonStatus();
            MessageBox.Show("The next lesson is now Available!!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }


        private void CheckLessonCompletion()
        {
            string filePath = $@"../../../DataBase/Language/{loggedInUser.Language}/LessonLock/{loggedInUser.Username}/{lessonName}.txt";
            if (File.Exists(filePath))
            {
                bool isComplete = lessonExamHandler.IsComplete(filePath);

                string currentLessonButton = "CompleteButton";
                Button currentButton = (Button)FindName(currentLessonButton);
                   
                if (isComplete)
                {
                    currentButton.Style = (Style)FindResource("ExamButton");
                    currentButton.Content = "Completed";
                    currentButton.IsEnabled = false;
                }
                else
                {
                    currentButton.IsEnabled = true;
                }
                    
            }
            
        }


        // Load the lesson from the list of lectures
        private void LoadLesson()
        {
            List<Lecture> lectures = lessonExamHandler.ReadLessonFromFile(loggedInUser, lessonName);

            List<Lecture> lecturesForALesson = new List<Lecture>();
            foreach (Lecture l in lectures)
            {

                lecturesForALesson.Add(l);
                
            }
            LessonList.ItemsSource = lecturesForALesson;
        }


        // Update the lesson status & save the user activity
        private void UpdateLessonStatus()
        {
            string filePath = $@"../../../DataBase/Language/{loggedInUser.Language}/LessonLock/{loggedInUser.Username}/{lessonName}.txt";
            fileWriterHandler.ReplaceLineInFile(filePath, $"{loggedInUser.Username},false", $"{loggedInUser.Username},true");
        }

        private void SaveUserActivity()
        {
            string filePath = $@"../../../DataBase/DashBoardActivity/{loggedInUser.Language}/{loggedInUser.Username}.txt";
            string textToAppend = $"{DateTime.Now},{loggedInUser.GetActivityName()}";
            fileWriterHandler.AppendTextToFile(filePath, textToAppend);
        }

        private void SaveLessonComplete()
        {
            loggedInUser.SetActivityName(lessonName);
            SaveUserActivity();
        }



    }
}
