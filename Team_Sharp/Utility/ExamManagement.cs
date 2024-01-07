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

            int prevLevel = loggedInUser.UserProgressLevel;
            string prevProficiency = loggedInUser.UserProgressProficiency;
            int prevExp = loggedInUser.Experience;

            // A1, A2, B1, B2, C1, and C2
            loggedInUser.Experience += prevExp + _pointsToGive;

            if (loggedInUser.Experience >= 1000)
            {
                loggedInUser.UserProgressLevel = prevLevel + 1;
                loggedInUser.UserProgressProficiency = "C2";
            }
            else if (loggedInUser.Experience >= 800)
            {
                loggedInUser.UserProgressLevel = prevLevel + 1;
                loggedInUser.UserProgressProficiency = "C1";
            }
            else if (loggedInUser.Experience >= 600)
            {
                loggedInUser.UserProgressLevel = prevLevel + 1;
                loggedInUser.UserProgressProficiency = "B2";
            }
            else if (loggedInUser.Experience >= 400)
            {
                loggedInUser.UserProgressLevel = prevLevel + 1;
                loggedInUser.UserProgressProficiency = "B1";
            }
            else if (loggedInUser.Experience >= 200)
            {
                loggedInUser.UserProgressLevel = prevLevel + 1;
                loggedInUser.UserProgressProficiency = "A2";
            }
            else if (loggedInUser.Experience >= 100)
            {
                loggedInUser.UserProgressLevel = prevLevel + 1;
                loggedInUser.UserProgressProficiency = "A1";
            }
            else
            {
                loggedInUser.UserProgressLevel = prevLevel;
                loggedInUser.UserProgressProficiency = prevProficiency;
            }
        }




    }
}
