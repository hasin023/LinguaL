using Team_Sharp.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using MaterialDesignColors;
using MaterialDesignThemes;
using System.Windows.Controls;

namespace Team_Sharp.View.Lessons
{
    public partial class LessonPage : Window
    {

        private List<Lecture> lectures = new List<Lecture>();
        public LessonPage()
        {
            InitializeComponent();

            //Check for complete button
            CheckLessonCompletion(GlobalLesson._lessonFile);


            LoadLesson(GlobalLesson._lessonFile);


        }


        private void CheckLessonCompletion(string str)
        {
            string filePath = $@"../../../DataBase/Language/{Global._language}/LessonLock/{Global._userName}/{str}.txt";
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

                string currentLessonButton = "CompleteButton";
                Button currentButton = (Button)FindName(currentLessonButton);
                   
                if (isCompleted)
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


        private void LoadLesson(string str)
        {
            List<Lecture> lectures = ReadLecturesFromFile(str);


            List<Lecture> lecturesForALesson = new List<Lecture>();

            foreach (Lecture l in lectures)
            {

                lecturesForALesson.Add(l);
                
            }
            LessonList.ItemsSource = lecturesForALesson;
        }

        private List<Lecture> ReadLecturesFromFile(string str)
        {
            List<Lecture> lectures = new List<Lecture>();

            string filePath = $@"../../../DataBase/Language/{Global._language}/Lesson/{str}.txt";
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split('-');
                string french = parts[0].Trim();
                string english = parts[1].Trim();
                lectures.Add(new Lecture { French = french, English = english });
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


        private void saveUserActivity(string str)
        {
            string filePath = $@"../../../DataBase/DashBoardActivity/{Global._language}/{str}.txt";
            string textToAppend = $"{DateTime.Now},{GlobalActivity._activity}";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(textToAppend);
            }
        }

        private void UpdateLessonStatus()
        {
            string filePath = $@"../../../DataBase/Language/{Global._language}/LessonLock/{Global._userName}/{GlobalLesson._lessonFile}.txt";

            ReplaceLineInFile(filePath, $"{Global._userName},false", $"{Global._userName},true");
        }


        private void SaveLessonComplete()
        {
            //Saving the Lesson Activity
            GlobalActivity._activity = GlobalLesson._lessonFile;
            saveUserActivity(Global._userName);

        }

        public void completeClick(object sender, RoutedEventArgs e)
        {

            SaveLessonComplete();

            //UpdateLessonStatus
            UpdateLessonStatus();
            MessageBox.Show("The next lesson is now Available!!","Success",MessageBoxButton.OK,MessageBoxImage.Information);
            this.Close();
            
        }

    }
}
