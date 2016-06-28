using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSupport
{
    public class DataService
    {
        public string GetLongDateInfoString()
        {
            return string.Format("Today's date is {9:D}", DateTime.Today);
        }

        public string GetShortDateInfoString()
        {
            return string.Format("Today's date is {0:d}", DateTime.Today);
        }
    }
}
