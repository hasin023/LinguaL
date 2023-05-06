using Team_Sharp.Utility;
using Team_Sharp.View.Exams;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Team_Sharp
{
    public partial class LanguageSelection : Window
    {
        public LanguageSelection()
        {
            InitializeComponent();
        }

        private void startClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Global._language = languageComboBox.Text;


            string userProgress = $@"../../../DataBase/Language/{Global._language}/Progress/{Global._userName}.txt";
            if (File.Exists(userProgress))
            {
                new Menu().Show();
            }
            else
            {
                MakeLessonFiles();
                MakeExamFiles();

                //WORK ON THIS
                new PlacementTest("q1op1", "q2op1", "q3op1", "q4op1", "q5op1").Show();
            }
        }

        private void exitClick(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }



        private void makeNewLessonFolder(string str)
        {
            string lessonFolderPath = $@"../../../DataBase/Language/{languageComboBox.Text}/LessonLock";

            Directory.CreateDirectory(Path.Combine(lessonFolderPath, str));
        }


        private void CreateNewLessonFile(string lessonName)
        {
            makeNewLessonFolder(Global._userName);
            string filePath = $@"../../../DataBase/Language/{languageComboBox.Text}/LessonLock/{Global._userName}/{lessonName}.txt";

            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine($"{Global._userName},false");
            }
        }

        private void MakeLessonFiles()
        {
            CreateNewLessonFile("Lesson1");
            CreateNewLessonFile("Lesson2");
            CreateNewLessonFile("Lesson3");
            CreateNewLessonFile("Lesson4");
            CreateNewLessonFile("Lesson5");
            CreateNewLessonFile("Lesson6");
            CreateNewLessonFile("Lesson7");
            CreateNewLessonFile("Lesson8");
            CreateNewLessonFile("Lesson9");
            CreateNewLessonFile("Lesson10");
            CreateNewLessonFile("Lesson11");
            CreateNewLessonFile("Lesson12");
            CreateNewLessonFile("Lesson13");
            CreateNewLessonFile("Lesson14");
            CreateNewLessonFile("Lesson15");
        }


        private void makeNewExamFolder(string str)
        {
            string examFolderPath = $@"../../../DataBase/Language/{languageComboBox.Text}/ExamLock";

            Directory.CreateDirectory(Path.Combine(examFolderPath, str));
        }

        private void CreateNewExamFile(string examName)
        {
            makeNewExamFolder(Global._userName);
            string filePath = $@"../../../DataBase/Language/{languageComboBox.Text}/ExamLock/{Global._userName}/{examName}.txt";

            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine($"{Global._userName},false");
            }
        }

        private void MakeExamFiles()
        {
            CreateNewExamFile("Exam1");
            CreateNewExamFile("Exam2");
            CreateNewExamFile("Exam3");
            CreateNewExamFile("Exam4");
            CreateNewExamFile("Exam5");
            CreateNewExamFile("Exam6");
            CreateNewExamFile("Exam7");
            CreateNewExamFile("Exam8");
            CreateNewExamFile("Exam9");
            CreateNewExamFile("Exam10");
            CreateNewExamFile("Exam11");
            CreateNewExamFile("Exam12");
            CreateNewExamFile("Exam13");
            CreateNewExamFile("Exam14");
            CreateNewExamFile("Exam15");
        }
    }
}
