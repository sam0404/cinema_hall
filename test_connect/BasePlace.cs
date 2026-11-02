using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_connect
{
    class BasePlace: Button
    {
        public BasePlace()
        {
        }

        public string PlaceTitle { get; set; }

        public int HallID { get; set; }
    }
}
