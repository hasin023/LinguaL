using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Sharp.Model
{
    public class LessonClass
    {
        private string _lessonName { get; set; }
        private bool _isCompleted { get; set; }

        public LessonClass(string lessonName, bool isCompleted)
        {
            this._lessonName = lessonName;
            this._isCompleted = isCompleted;
        }

        public string LessonName
        {
            get { return _lessonName; }
            set { _lessonName = value; }
        }

        public bool IsCompleted
        {
            get { return _isCompleted; }
            set { _isCompleted = value; }
        }
    }
}
