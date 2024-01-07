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

        public LessonPage(User loggedInUser, string lessonName)
        {
            InitializeComponent();
            this.loggedInUser = loggedInUser;
            this.lessonName = lessonName;
            this.lessonExamHandler = new LessonExamHandler(loggedInUser);
            
            CheckLessonCompletion();
            LoadLesson();
        }


        // NEED TO FIX THIS
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


        private void LoadLesson()
        {
            List<Lecture> lectures = ReadLecturesFromFile();

            List<Lecture> lecturesForALesson = new List<Lecture>();
            foreach (Lecture l in lectures)
            {

                lecturesForALesson.Add(l);
                
            }
            LessonList.ItemsSource = lecturesForALesson;
        }

        private List<Lecture> ReadLecturesFromFile()
        {
            List<Lecture> lectures = new List<Lecture>();

            string filePath = $@"../../../DataBase/Language/{loggedInUser.Language}/Lesson/{lessonName}.txt";
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split('-');
                string french = parts[0].Trim();
                string english = parts[1].Trim();
                Lecture lecture = new Lecture(french, english);
                lectures.Add(lecture);
            }

            return lectures;
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
            string textToAppend = $"{DateTime.Now},{loggedInUser.CurrentActivity}";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(textToAppend);
            }
        }

        private void UpdateLessonStatus()
        {
            string filePath = $@"../../../DataBase/Language/{loggedInUser.Language}/LessonLock/{loggedInUser.Username}/{lessonName}.txt";
            ReplaceLineInFile(filePath, $"{loggedInUser.Username},false", $"{loggedInUser.Username},true");
        }


        private void SaveLessonComplete()
        {
            loggedInUser.CurrentActivity = lessonName;
            saveUserActivity();
        }

        public void completeClick(object sender, RoutedEventArgs e)
        {
            SaveLessonComplete();
            UpdateLessonStatus();
            MessageBox.Show("The next lesson is now Available!!","Success",MessageBoxButton.OK,MessageBoxImage.Information);
            this.Close();   
        }


    }
}
