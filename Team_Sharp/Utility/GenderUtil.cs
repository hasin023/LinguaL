using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Team_Sharp.Utility
{
    public class GenderUtil
    {
        public BitmapImage selectMale()
        {
            return new BitmapImage(new Uri("Assets/dudeIcon.png", UriKind.RelativeOrAbsolute));
        }

        public BitmapImage selectFemale()
        {
            return new BitmapImage(new Uri("Assets/girlIcon.png", UriKind.RelativeOrAbsolute));
        }

        public BitmapImage selectOther()
        {
            return new BitmapImage(new Uri("Assets/otherGenIcon.png", UriKind.RelativeOrAbsolute));
        }
    }
}
