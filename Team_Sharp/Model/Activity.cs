using System;

namespace Team_Sharp.Model
{
    public class Activity
    {
        private DateTime _date { get; set; }
        private string _name { get; set; }

        public Activity(DateTime date, string name)
        {
            this._date = date;
            this._name = name;
        }

        public Activity() { }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

    

    }
}
