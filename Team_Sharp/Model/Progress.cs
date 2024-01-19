namespace Team_Sharp.Model
{
    public class Progress
    {
        private int _userExperience { get; set; }
        private int _userProgressLevel { get; set; }
        private string _userProgressProficiency { get; set; }

        public Progress(int userExperience, int userProgressLevel, string userProgressProficiency)
        {
            this._userExperience = userExperience;
            this._userProgressLevel = userProgressLevel;
            this._userProgressProficiency = userProgressProficiency;
        }

        public Progress() { }

        public int UserExperience
        {
            get { return _userExperience; }
            set { _userExperience = value; }
        }

        public int UserProgressLevel
        {
            get { return _userProgressLevel; }
            set { _userProgressLevel = value; }
        }

        public string UserProgressProficiency
        {
            get { return _userProgressProficiency; }
            set { _userProgressProficiency = value; }
        }
    }
}
