namespace Team_Sharp.Model
{
    public class ExamClass
    {
        private string _examName { get; set; }
        private bool _isCompleted { get; set; }

        public ExamClass(string examName, bool isCompleted) 
        {
            this._examName = examName;
            this._isCompleted = isCompleted;
        }

        public string ExamName
        {
            get { return _examName; }
            set { _examName = value; }
        }

        public bool IsCompleted
        {
            get { return _isCompleted; }
            set { _isCompleted = value; }
        }
    }
}
