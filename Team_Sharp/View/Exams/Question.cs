using System.Collections.Generic;

namespace Team_Sharp.View.Exams
{
    public class Question
    {
        private string _text { get; set; }
        private List<Option> _options { get; set; }

        public Question(string text, List<Option> options)
        {
            this._text = text;
            this._options = options;
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public List<Option> Options
        {
            get { return _options; }
            set { _options = value; }
        }
    }
}
