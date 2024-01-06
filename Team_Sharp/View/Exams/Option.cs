namespace Team_Sharp.View.Exams
{
    public class Option
    {
        private string _text { get; set; }
        private bool _isCorrect { get; set; }

        public Option(string text)
        {
            this._text = text;
        }

        public Option(string text, bool isCorrect)
        {
            this._text = text;
            this._isCorrect = isCorrect;
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public bool IsCorrect
        {
            get { return _isCorrect; }
            set { _isCorrect = value; }
        }

    }
}
