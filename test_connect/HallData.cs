namespace cinema
{
    class HallData
    {
        public HallData()
        {

        }

        public HallData(int totalPlaces, int placeInRow, int id)
        {
            TotalPlaces = totalPlaces;
            PlaceInRow = placeInRow;
            ID = id;
        }
        public int TotalPlaces { get; private set; }

        public int PlaceInRow { get; private set; }

        public int ID { get; private set; }
    }
}
