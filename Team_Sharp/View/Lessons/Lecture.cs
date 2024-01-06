using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Sharp.View.Lessons
{
    public class Lecture
    {
        private string _french { get; set; }
        private string _english { get; set; }

        public Lecture(string french, string english)
        {
            this._french = french;
            this._english = english;
        }

        public string French
        {
            get { return _french; }
            set { _french = value; }
        }

        public string English
        {
            get { return _english; }
            set { _english = value; }
        }
    }
}
