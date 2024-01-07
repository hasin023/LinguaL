using System.Collections.Generic;
using System.IO;
using Team_Sharp.Model;
using Team_Sharp.View.Exams;
using Team_Sharp.View.Lessons;

namespace Team_Sharp.Handlers
{
    public class LessonExamHandler
    {

        private readonly User loggedInUser;

        public LessonExamHandler(User loggedInUser)
        {
            this.loggedInUser = loggedInUser;
        }

        public void MakeLessonFiles()
        {
            for (int i = 1; i <= 15; i++)
            {
                CreateNewLessonFile($"Lesson{i}");
            }
        }

        public void MakeExamFiles()
        {
            for (int i = 1; i <= 15; i++)
            {
                CreateNewExamFile($"Exam{i}");
            }
        }

        private void CreateNewLessonFile(string lessonName)
        {
            string lessonFolderPath = $@"../../../DataBase/Language/{loggedInUser.Language}/LessonLock";
            Directory.CreateDirectory(Path.Combine(lessonFolderPath, loggedInUser.Username));

            string filePath = Path.Combine(lessonFolderPath, loggedInUser.Username, $"{lessonName}.txt");

            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine($"{loggedInUser.Username},false");
            }
        }

        private void CreateNewExamFile(string examName)
        {
            string examFolderPath = $@"../../../DataBase/Language/{loggedInUser.Language}/ExamLock";
            Directory.CreateDirectory(Path.Combine(examFolderPath, loggedInUser.Username));

            string filePath = Path.Combine(examFolderPath, loggedInUser.Username, $"{examName}.txt");

            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine($"{loggedInUser.Username},false");
            }
        }


        public bool IsComplete(string filePath)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    string status = parts[1].Trim();

                    if (status == "true")
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public List<Lecture> ReadLessonFromFile(User user, string lessonName)
        {
            List<Lecture> lectures = new List<Lecture>();

            string filePath = $@"../../../DataBase/Language/{user.Language}/Lesson/{lessonName}.txt";
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


        public List<Question> ParseQuestionsFromFile(string filePath)
        {
            List<Question> questions = new List<Question>();
            string[] lines = File.ReadAllLines(filePath);
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

            return questions;
        }



    }
}
