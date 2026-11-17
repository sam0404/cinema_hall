using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace cinema
{
    public partial class CinemaForm : Form
    {
        private SeansForm seans = null;
        private FilmForm films = null;
        private HallsForm halls = null;

        private Dictionary<string, HallData> hallsData = new Dictionary<string, HallData>();


        public CinemaForm()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            LoadHalls();    
        }

        private void LoadHalls()
        {
            cinemaPanel.Controls.Clear();
            hallsData.Clear();

            try
            {
                string command = "select * from vw_HallInfo";

                using (SqlConnection conn = new SqlConnection(Program.ConnString))
                {
                    conn.Open();
                    SqlDataAdapter adapt = new SqlDataAdapter(command, conn);
                    DataTable table = new DataTable();
                    adapt.Fill(table);

                    int index = 0;

                    foreach (DataRow row in table.Rows)
                    {
                        string title = row["Название зала"].ToString();
                        hallsData.Add(title, new HallData((int)row["Всего мест"], (int)row["Мест в ряду"], (int)row["id_hall"]));
                        Button selectButton = new Button();
                        cinemaPanel.Controls.Add(selectButton);
                        selectButton.Size = new Size(100, 60);
                        selectButton.BackColor = SystemColors.ButtonFace;
                        selectButton.Location = new Point(3, index * 90);
                        selectButton.Text = title;
                        selectButton.Click += new System.EventHandler(button1_Click_1);
                        index++;
                    }
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки:\n" + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Program.Hall = (sender as Button).Text;

            if (seans == null)
            {
                seans = new SeansForm(hallsData[Program.Hall].PlaceInRow, hallsData[Program.Hall].ID);
            }

            seans.ShowDialog();
        }

        private void btnFilm_Click(object sender, EventArgs e)
        {
            if (films == null)
            {
                films = new FilmForm();
            }

            films.ShowDialog();
        }

        private void btnHall_Click(object sender, EventArgs e)
        {
            if (halls == null)
            {
                halls = new HallsForm();
            }

            halls.ShowDialog();
            LoadHalls();
        }
    }
}
