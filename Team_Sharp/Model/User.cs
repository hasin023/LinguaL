namespace Team_Sharp.Model
{
    public class User
    {
        private string _username { get; set; }
        private string _password { get; set; }
        private string _email { get; set; }
        private string _name { get; set; }
        private string _dob { get; set; }
        private string _gender { get; set; }

        private string _language { get; set; }

        private Activity _activity { get; set; }
        private ExamResult _examResult { get; set; }
        private Progress _progress { get; set; }


        public User(string username, string password, string email, string name, string dob, string gender)
        {
            this._username = username;
            this._password = password;
            this._email = email;
            this._name = name;
            this._dob = dob;
            this._gender = gender;

            this._activity = new Activity();
            this._examResult = new ExamResult();
            this._progress = new Progress();
        }

        public User(string username, string password)
        {
            this._username = username;
            this._password = password;

            this._activity = new Activity();
            this._examResult = new ExamResult();
            this._progress = new Progress();
        }

        public User() 
        {
            this._activity = new Activity();
            this._examResult = new ExamResult();
            this._progress = new Progress();
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string DateOfBirth
        {
            get { return _dob; }
            set { _dob = value; }
        }

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }

        public Activity Activity
        {
            get { return _activity; }
            set { _activity = value; }
        }

        public ExamResult ExamResult
        {
            get { return _examResult; }
            set { _examResult = value; }
        }

        public Progress Progress
        {
            get { return _progress; }
            set { _progress = value; }
        }


    }
}
