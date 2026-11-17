using System.Windows.Forms;

namespace cinema
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
