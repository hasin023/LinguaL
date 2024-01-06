using System.Collections.Generic;
using Team_Sharp.View;

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
        private string _currentActivity { get; set; }
        private int _exp { get; set; }
        private int _userProgressLevel { get; set; }
        private string _userProgressProficiency { get; set; }

        private Activity _activity { get; set; }
        private List<LessonClass> _lessons { get; set; }
        private List<ExamClass> _exams { get; set; }


        public User(string username, string password, string email, string name, string dob, string gender)
        {
            this._username = username;
            this._password = password;
            this._email = email;
            this._name = name;
            this._dob = dob;
            this._gender = gender;

            this._lessons = new List<LessonClass>();
            this._exams = new List<ExamClass>();
        }

        public User(string username, string password)
        {
            this._username = username;
            this._password = password;

            this._lessons = new List<LessonClass>();
            this._exams = new List<ExamClass>();
        }

        public User() 
        {
            this._lessons = new List<LessonClass>();
            this._exams = new List<ExamClass>();
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

        public string CurrentActivity
        {
            get { return _currentActivity; }
            set { _currentActivity = value; }
        }

        public int Experience
        {
            get { return _exp; }
            set { _exp = value; }
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

        public Activity Activity
        {
            get { return _activity; }
            set { _activity = value; }
        }

        public List<LessonClass> Lessons
        {
            get { return _lessons; }
            set { _lessons = value; }
        }

        public List<ExamClass> Exams
        {
            get { return _exams; }
            set { _exams = value; }
        }

    }
}
