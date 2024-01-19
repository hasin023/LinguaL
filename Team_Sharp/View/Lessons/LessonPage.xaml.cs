using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Team_Sharp.Model;
using Team_Sharp.Handlers;

namespace Team_Sharp.View.Lessons
{
    public partial class LessonPage : Window, ILesson, IActivity, IStatus
    {
        private User loggedInUser;
        private string lessonName;
        private LessonExamHandler lessonExamHandler;
        private FileWriterHandler fileWriterHandler;

        public LessonPage(User loggedInUser, string lessonName, LessonExamHandler lessonExamHandler, FileWriterHandler fileWriterHandler)
        {
            InitializeComponent();
            this.loggedInUser = loggedInUser;
            this.lessonName = lessonName;
            this.lessonExamHandler = lessonExamHandler;
            this.fileWriterHandler = fileWriterHandler;
            
            CheckLessonCompletion();
            LoadLesson();
        }


        public void completeClick(object sender, RoutedEventArgs e)
        {
            SaveLessonComplete();
            UpdateStatus();
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
        public void LoadLesson()
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
        public void UpdateStatus()
        {
            string filePath = $@"../../../DataBase/Language/{loggedInUser.Language}/LessonLock/{loggedInUser.Username}/{lessonName}.txt";
            fileWriterHandler.ReplaceLineInFile(filePath, $"{loggedInUser.Username},false", $"{loggedInUser.Username},true");
        }

        public void SaveUserActivity()
        {
            string filePath = $@"../../../DataBase/DashBoardActivity/{loggedInUser.Language}/{loggedInUser.Username}.txt";
            string textToAppend = $"{DateTime.Now},{loggedInUser.Activity.Name}";
            fileWriterHandler.AppendTextToFile(filePath, textToAppend);
        }

        public void SaveLessonComplete()
        {
            loggedInUser.Activity.Name = lessonName;
            SaveUserActivity();
        }

    }
}
