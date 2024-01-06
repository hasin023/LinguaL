using System.IO;

namespace Team_Sharp.Utility
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


    }
}
