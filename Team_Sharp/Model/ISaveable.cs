using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Sharp.Model
{
    public interface ISaveable
    {
        void SavePassingProgress();
        void SaveFailingProgress();
    }
}
