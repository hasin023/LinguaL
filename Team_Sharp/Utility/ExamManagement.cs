using System.Windows.Controls;
using Team_Sharp.Handlers;
using Team_Sharp.Model;

namespace Team_Sharp.Utility
{
    public class ExamManagement
    {
        private readonly User loggedInUser;
        private int _pointsToGive;
        private FileReaderHandler fileReaderHandler;

        public ExamManagement(User loggedInUser, int pointsToGive)
        {
            this.loggedInUser = loggedInUser;
            this._pointsToGive = pointsToGive;
            this.fileReaderHandler = new FileReaderHandler();
        }

        public int PointsToGive
        {
            get { return _pointsToGive; }
            set { _pointsToGive = value; }
        }


        public void CheckCorrectOption(RadioButton radioButton)
        {
            if (radioButton.IsChecked == true)
            {
                loggedInUser.ExamResult.CorrectAnswersCount++;
                loggedInUser.ExamResult.EarnedPoints += _pointsToGive;                
            }
            else
            {
                loggedInUser.ExamResult.InCorrectAnswersCount++;
            }
        }


        public void CalculatePassingProgress(string filePath)
        {
            fileReaderHandler.ReadProgress(filePath, loggedInUser);

            int prevLevel = loggedInUser.Progress.UserProgressLevel;
            string prevProficiency = loggedInUser.Progress.UserProgressProficiency;
            int prevExp = loggedInUser.Progress.UserExperience;

            // A1, A2, B1, B2, C1, and C2
            loggedInUser.Progress.UserExperience += prevExp + _pointsToGive;

            if (loggedInUser.Progress.UserExperience >= 1000)
            {
                loggedInUser.Progress.UserProgressLevel = prevLevel + 1;
                loggedInUser.Progress.UserProgressProficiency = "C2";
            }
            else if (loggedInUser.Progress.UserExperience >= 800)
            {
                loggedInUser.Progress.UserProgressLevel = prevLevel + 1;
                loggedInUser.Progress.UserProgressProficiency = "C1";
            }
            else if (loggedInUser.Progress.UserExperience >= 600)
            {
                loggedInUser.Progress.UserProgressLevel = prevLevel + 1;
                loggedInUser.Progress.UserProgressProficiency = "B2";
            }
            else if (loggedInUser.Progress.UserExperience >= 400)
            {
                loggedInUser.Progress.UserProgressLevel = prevLevel + 1;
                loggedInUser.Progress.UserProgressProficiency = "B1";
            }
            else if (loggedInUser.Progress.UserExperience >= 200)
            {
                loggedInUser.Progress.UserProgressLevel = prevLevel + 1;
                loggedInUser.Progress.UserProgressProficiency = "A2";
            }
            else if (loggedInUser.Progress.UserExperience >= 100)
            {
                loggedInUser.Progress.UserProgressLevel = prevLevel + 1;
                loggedInUser.Progress.UserProgressProficiency = "A1";
            }
            else
            {
                loggedInUser.Progress.UserProgressLevel = prevLevel;
                loggedInUser.Progress.UserProgressProficiency = prevProficiency;
            }
        }




    }
}
