using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace test_connect
{
    public partial class Hall : Form
    {
        private TimeSpan _time;
        private DateTime _date;

        private bool isCancelSelect = false;
        private Dictionary<string, Place> buttonsPlace = new Dictionary<string, Place>();
        private Panel hall = null;

        private int _lastRow = 0;
        private int _lastColumn = 0;

        private int minSize = 0;
        private int sizeWidth = 0;
        private int rows = 0;
        private int sizeHeight = 0;
        
        private int size = 0;
        private int offsetX = 0;
        private int offsetY = 0;

        private int hallID = 0;
        public Hall()
        {
            InitializeComponent();
        }
        public Hall(int totalAmountPlaces, TimeSpan time, DateTime date, int placesInRow = 10)
        {
            InitializeComponent();

            TotalAmountPlaces = totalAmountPlaces;
            PlacesInRow = placesInRow;
            _time = time;
            _date = date;
        }
        
        public int TotalAmountPlaces { set; get; }
        public int PlacesInRow { set; get; }

        int header = 40;
        int border = 6;
        

        private void CreateHall()
        {                       
            hall = new Panel();

            this.Controls.Add(hall);

            if (this.Width > this.Height)
            {
                minSize = this.Height;
            }
            else 
            {
                minSize = this.Width;
            }

            minSize = (int)(minSize * 0.9);

            hall.BackColor = SystemColors.ControlDark;
            hall.Size = new Size(minSize, minSize);
            hall.Location = new Point((this.Width - minSize - 2 * border) / 2, (this.Height - minSize - header - border) / 2);
        }

        private void CreateScreen(int size)
        {
            Panel screen = new Panel();
            hall.Controls.Add(screen);
            screen.BackColor = SystemColors.GradientActiveCaption;
            int width = (int)(0.5 * size);
            int height = (int)(0.1 * size);
            screen.Size = new Size(width, height);
            screen.Location = new Point((size - width) / 2, 0);  
        }

        private void InitHall()
        {
            sizeWidth = (int)(0.98 * minSize / (PlacesInRow + (PlacesInRow - 1) * 0.2));
            rows = (int)Math.Floor((double)(TotalAmountPlaces / PlacesInRow));
            sizeHeight = (int)(0.8 * minSize / (rows + (rows - 1) * 0.2));
            
            size = Math.Min(sizeWidth, sizeHeight);

            offsetX = (int)(0.2 * size);
            offsetY = (int)(0.2 * size);

            int placeWidth = (PlacesInRow - 1) * offsetX + size * PlacesInRow;
            int delta = (minSize - placeWidth) / 2;

            int positionY = (int)(0.2 * minSize);

            int tempI = 0;
            int row = 1;
            
            int maxWidth = 0;

            for (int i = 0; i < TotalAmountPlaces; i++)
            {
                var p1 = new Place(1);
                hall.Controls.Add(p1);
                var positionX = (int)(delta) + (i - tempI) * (size + offsetX);

                if (i > 0 && i % PlacesInRow == 0)
                {
                    maxWidth = positionX;
                    row++;
                    tempI = i;
                    positionY += size + offsetY;
                    positionX = (int)(delta);
                }

                p1.Location = new Point(positionX, positionY);
                p1.Name = (i + 1).ToString();
                p1.Size = new Size(size, size);
                p1.TabIndex = 0;
                p1.Text =row.ToString() + " " + (i % PlacesInRow + 1).ToString();
                p1.UseVisualStyleBackColor = true;
                p1.BackColor = SystemColors.GradientInactiveCaption;

                p1.Click += new System.EventHandler(this.button1_Click_1);

                buttonsPlace.Add(p1.Text, p1);
            }

            _lastRow = (int)(TotalAmountPlaces / PlacesInRow) + 1;
            _lastColumn = TotalAmountPlaces % PlacesInRow;
        }

        private Place AddPlaceView()
        {
            int placeWidth = (PlacesInRow - 1) * offsetX + size * PlacesInRow;
            int delta = (minSize - placeWidth) / 2;

            int positionY = (int)(0.2 * minSize) + (size + offsetY) * (_lastRow - 1);


            int maxWidth = 0;

            var p1 = new Place(1);
            hall.Controls.Add(p1);
            var positionX = (int)(delta) + _lastColumn * (size + offsetX);

            if (_lastColumn > 0 && _lastColumn % PlacesInRow == 0)
            {
                maxWidth = positionX;
                _lastRow++;
                _lastColumn = 0;
                positionY += size + offsetY;
                positionX = (int)(delta);
            }

            p1.Location = new Point(positionX, positionY);
            p1.Name = (_lastRow * PlacesInRow + _lastColumn + 1).ToString();
            p1.Size = new Size(size, size);
            p1.TabIndex = 0;
            p1.Text = _lastRow.ToString() + " " + (_lastColumn % PlacesInRow + 1).ToString();
            
            p1.PlaceTitle = p1.Text;
            p1.HallID = hallID;
            p1.StateForSale = 0;
            
            p1.UseVisualStyleBackColor = true;
            p1.BackColor = SystemColors.GradientInactiveCaption;

            p1.Click += new System.EventHandler(this.button1_Click_1);
            _lastColumn++;

            buttonsPlace.Add(p1.Text, p1);
            MessageBox.Show("Место добавлено: " + p1.Text);
            TotalAmountPlaces++;

            return p1;
        }

        private void FillFromDB()
        {
            string sql = "SELECT * FROM ticket_view where Зал = @title and Время = @time and Дата = @date";

            using (SqlConnection conn = new SqlConnection(Program.ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@title", Program.Hall);
                    cmd.Parameters.AddWithValue("@time", _time);
                    cmd.Parameters.AddWithValue("@date", _date);


                    using (SqlDataAdapter adaptar = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adaptar.Fill(table);
                        // Обращение по имени колонки (безопасно)
                        foreach (DataRow row in table.Rows)
                        {
                            string place = row["Место"].ToString();
                            int state = (int)row["Состояние"];

                            buttonsPlace[place].TicketID = (int)row["Билет_ID"];
                            buttonsPlace[place].PlaceTitle = place;
                            buttonsPlace[place].StateForSale = (int)row["Состояние"];
                            hallID = buttonsPlace[place].HallID = (int)row["Зал_ID"];
                            buttonsPlace[place].TimeID = (int)row["Время_ID"];
                            buttonsPlace[place].FilmID = (int)row["Фильм_ID"];
                        
                            if (state == 1)
                            {
                                buttonsPlace[place].placeType = PlaceType.BUSY;
                                buttonsPlace[place].BackColor = Color.DarkOliveGreen;
                            }
                        }
                    }
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var place = (sender as Place);

            switch (place.placeType)
            {
                case PlaceType.FREE:
                    if (isCancelSelect) return;

                    if (place.placeStatus == PlaceStatus.NONE)
                    {
                        place.placeStatus = PlaceStatus.SELECTED;
                        place.BackColor = SystemColors.ControlDarkDark;
                    }
                    else if (place.placeStatus == PlaceStatus.SELECTED)
                    {
                        place.placeStatus = PlaceStatus.NONE;
                        place.BackColor = SystemColors.GradientInactiveCaption;
                    }

                    return;

                case PlaceType.BUSY:
                    if (!isCancelSelect) return;

                    if (place.placeStatus == PlaceStatus.NONE)
                    {
                        place.placeStatus = PlaceStatus.SELECTED;
                        place.BackColor = SystemColors.ControlDarkDark;
                    }
                    else if (place.placeStatus == PlaceStatus.SELECTED)
                    {
                        place.placeStatus = PlaceStatus.NONE;
                        place.BackColor = Color.DarkOliveGreen;
                    }
                    return;
            }
        }

        private void Edit_Hall(Place place)
        {
            using (SqlConnection conn = new SqlConnection(Program.ConnString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("update_ticket", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ticket_id", place.TicketID);
                    cmd.Parameters.AddWithValue("@is_sell", place.StateForSale);

                    int rows = cmd.ExecuteNonQuery(); // Возвращает количество добавленных строк
                    MessageBox.Show("Row "+ rows.ToString());
                }

                conn.Close();
            }
        }

        private void Add_Place_DB(Place place)
        {
            using (SqlConnection conn = new SqlConnection(Program.ConnString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("add_place", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@place_title", place.PlaceTitle);
                    cmd.Parameters.AddWithValue("@id_hall", place.HallID);

                    cmd.ExecuteNonQuery();

                }

                conn.Close();
            }
        }

        private void buy_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (KeyValuePair<string, Place> pair in buttonsPlace)
                {
                    if (pair.Value.placeType == PlaceType.FREE && pair.Value.placeStatus == PlaceStatus.SELECTED)
                    {
                        pair.Value.StateForSale = 1;
                        Edit_Hall(pair.Value);
                        pair.Value.placeType = PlaceType.BUSY;
                        pair.Value.placeStatus = PlaceStatus.NONE;
                        pair.Value.BackColor = Color.DarkOliveGreen;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Edit rows: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isCancelSelect = true;            
        }

        private void AcceptCancel_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Place> pair in buttonsPlace)
            {
                if (pair.Value.placeType == PlaceType.BUSY && pair.Value.placeStatus == PlaceStatus.SELECTED)
                {
                    pair.Value.StateForSale = 0;
                    Edit_Hall(pair.Value);
                    pair.Value.placeType = PlaceType.FREE;
                    pair.Value.placeStatus = PlaceStatus.NONE;
                    pair.Value.BackColor = SystemColors.GradientInactiveCaption;
                }
            }           

            isCancelSelect = false;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            isCancelSelect = false; 

            foreach (KeyValuePair<string, Place> pair in buttonsPlace)
            {
                if (pair.Value.placeType == PlaceType.FREE  && pair.Value.placeStatus == PlaceStatus.SELECTED)
                {
                    pair.Value.placeStatus = PlaceStatus.NONE;
                    pair.Value.BackColor = SystemColors.GradientInactiveCaption;
                }
                else if ( pair.Value.placeType == PlaceType.BUSY && pair.Value.placeStatus == PlaceStatus.SELECTED)
                {
                    pair.Value.placeStatus = PlaceStatus.NONE;
                    pair.Value.BackColor = Color.DarkOliveGreen;
                }
            }
        }

        private Place RemovePlaceView(){
            int row = (int)Math.Ceiling((double)TotalAmountPlaces / (double)PlacesInRow);
            int column = TotalAmountPlaces % PlacesInRow;
            if (column == 0)
            {
                column = 10;
            }

            string placeName = row.ToString() + " " + column.ToString();

            if (!buttonsPlace.ContainsKey(placeName))
            {
                MessageBox.Show("Невозможно удалить место");
                return null;
            }

            Place place = buttonsPlace[placeName];
            hall.Controls.Remove(place);
            buttonsPlace.Remove(placeName);

            if (column == 1)
            {
                _lastColumn = 10;
                _lastRow--;
            }
            else
            {
                _lastColumn = column - 1;
            }

            TotalAmountPlaces--;

            return place;
        }

        private void Remove_Place_DB(Place place)
        {
            string sql = "DELETE FROM place WHERE place_title = @PlaceTitle and id_hall = @IdHall";

            using (SqlConnection conn = new SqlConnection(Program.ConnString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PlaceTitle", place.PlaceTitle);
                    cmd.Parameters.AddWithValue("@IdHall", place.HallID);

                    int rows = cmd.ExecuteNonQuery(); // Возвращает количество удаленных строк

                    if (rows > 0)
                        MessageBox.Show("Билет успешно удален!");
                    else
                        MessageBox.Show("Билет с таким ID не найден.");
                }
            }
        }

        private void Hall_Load(object sender, EventArgs e)
        {
            buttonsPlace.Clear();

            CreateHall();
            CreateScreen(minSize);
            InitHall();
            FillFromDB();
        }

        private void btnAddPlace_Click(object sender, EventArgs e)
        {
            Add_Place_DB(AddPlaceView());
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Place place = RemovePlaceView();
            if (place != null)
            {
                Remove_Place_DB(place);
                place = null;
            }
        }

        private void Hall_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }       
    
    }
}
