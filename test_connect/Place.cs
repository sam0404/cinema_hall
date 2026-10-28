using System;
using System.Windows.Forms;

namespace test_connect
{
    
    class Place: Button
    {
        public Place(int place)
        {
            PlaceNumber = place;
        }

        public int PlaceNumber { get; private set; }

        public PlaceType placeType = PlaceType.FREE;
        public PlaceStatus placeStatus = PlaceStatus.NONE;

        public int FilmID { get; set; }
        public int TimeID { get; set; }
        public int TicketID { get; set; }
        public string PlaceTitle { get; set; }
        public int StateForSale { get; set; }

        public int HallID { get; set; }
    }
}
