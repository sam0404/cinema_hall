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
        public Hall()
        {
            InitializeComponent();
        }
        public Hall(int totalAmountPlaces, int placesInRow = 10)
        {
            InitializeComponent();

            TotalAMountPlaces = totalAmountPlaces;
            PlacesInRow = placesInRow;
        }


        private bool isCancelSelect = false;
        private Dictionary<string, Place> buttonsPlace = new Dictionary<string, Place>();
        private Panel hall = null;

        private int minSize = 0;

        public int TotalAMountPlaces { set; get; }
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
            int sizeWidth = (int)(0.98 * minSize / (PlacesInRow + (PlacesInRow - 1) * 0.2));
            int rows = (int)Math.Floor((double)(TotalAMountPlaces / PlacesInRow));
            int sizeHeight = (int)(0.8 * minSize / (rows + (rows - 1) * 0.2));


            int size = Math.Min(sizeWidth, sizeHeight);

            MessageBox.Show("Size " + size.ToString());
            int offsetX = (int)(0.2 * size);
            int offsetY = (int)(0.2 * size);

            int placeWidth = (PlacesInRow - 1) * offsetX + size * PlacesInRow;
            int delta = (minSize - placeWidth) / 2;

            int positionY = (int)(0.2 * minSize);

            int tempI = 0;
            int row = 1;
            
            int maxWidth = 0;

            for (int i = 0; i < TotalAMountPlaces; i++)
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
                p1.Text =row.ToString() + " " + (i + 1 - (row - 1) * PlacesInRow).ToString();
                p1.UseVisualStyleBackColor = true;
                p1.BackColor = SystemColors.GradientInactiveCaption;

                p1.Click += new System.EventHandler(this.button1_Click_1);

                buttonsPlace.Add(p1.Text, p1);
            }  

        }

        private void LoadDataFromDB()
        {
            string sql = "SELECT * FROM ticket_view where Зал = @title";

            using (SqlConnection conn = new SqlConnection(Program.ConnString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@title", Program.Hall);

                  
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table); 

                        foreach (DataRow row in table.Rows)
                        {
                         // Обращение по имени колонки (безопасно)
                            string place = row["Место"].ToString();
                            int state = (int)row["Состояние"];

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

        private void buy_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Place> pair in buttonsPlace)
            {
                if (pair.Value.placeType == PlaceType.FREE && pair.Value.placeStatus == PlaceStatus.SELECTED)
                {
                    pair.Value.placeType = PlaceType.BUSY;
                    pair.Value.placeStatus = PlaceStatus.NONE;
                    pair.Value.BackColor = Color.DarkOliveGreen;
                }
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
                    pair.Value.placeType = PlaceType.FREE;
                    pair.Value.placeStatus = PlaceStatus.NONE;
                    pair.Value.BackColor = SystemColors.GradientInactiveCaption;
                }
            }           

            isCancelSelect = false;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Place> pair in buttonsPlace)
            {
                if (pair.Value.placeType == PlaceType.FREE && pair.Value.placeStatus == PlaceStatus.SELECTED)
                {
                    pair.Value.placeStatus = PlaceStatus.NONE;
                    pair.Value.BackColor = SystemColors.GradientInactiveCaption;
                }
            }
        }

        private void Hall_Load(object sender, EventArgs e)
        {
            CreateHall();
            CreateScreen(minSize);
            InitHall();
            LoadDataFromDB();
        }       
    
    }
}
