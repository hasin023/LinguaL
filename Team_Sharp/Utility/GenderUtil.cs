using System;
using System.Windows.Media.Imaging;

namespace Team_Sharp.Utility
{
    public class GenderUtil
    {
        private BitmapImage LoadImage(string fileName)
        {
            return new BitmapImage(new Uri($"Assets/{fileName}", UriKind.RelativeOrAbsolute));
        }

        public BitmapImage SelectMale() => LoadImage("dudeIcon.png");

        public BitmapImage SelectFemale() => LoadImage("girlIcon.png");

        public BitmapImage SelectOther() => LoadImage("otherGenIcon.png");

        public BitmapImage SelectBackground() => LoadImage("background.png");

    }
}
