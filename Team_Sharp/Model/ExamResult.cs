namespace Team_Sharp.Model
{
    public class ExamResult
    {
        private int _correctAnswersCount { get; set; }
        private int _inCorrectAnswersCount { get; set; }
        private int _earnedPoints { get; set; }

        public ExamResult()
        {
            this._correctAnswersCount = 0;
            this._inCorrectAnswersCount = 0;
            this._earnedPoints = 0;
        }

        public int CorrectAnswersCount
        {
            get { return _correctAnswersCount; }
            set { _correctAnswersCount = value; }
        }

        public int InCorrectAnswersCount
        {
            get { return _inCorrectAnswersCount; }
            set { _inCorrectAnswersCount = value; }
        }

        public int EarnedPoints
        {
            get { return _earnedPoints; }
            set { _earnedPoints = value; }
        }


    }
}
