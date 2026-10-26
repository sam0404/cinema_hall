using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_connect
{
    public partial class FilmsForm : Form
    {
        private Hall hall = null;
        public FilmsForm()
        {
            InitializeComponent();
        }

        private void FilmsForm_Load(object sender, EventArgs e)
        {
            LoadFilms();
        }
        private int ID { set; get; }

        private void LoadFilms()
        {
            string sql = "SELECT * FROM ticket_view_seans where Зал = @title";

            using (SqlConnection conn = new SqlConnection(Program.ConnString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@title", Program.Hall);

                    // ✅ Передаем в адаптер готовую команду (cmd), а не текст и подключение
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table); // Теперь параметр @title на месте!

                        dgvCinema.DataSource = table;

                        // Обязательно проверяем, что колонки существуют, прежде чем менять ширину
                        if (dgvCinema.Columns.Count >= 7)
                        {
                            dgvCinema.Columns[1].Width = 160;
                            dgvCinema.Columns[2].Width = 60;
                            dgvCinema.Columns[3].Width = 80;
                            dgvCinema.Columns[4].Width = 60;
                            dgvCinema.Columns[5].Width = 60;
                            dgvCinema.Columns[6].Width = 60;
                        }

                        
                    }
                }
            }
        }

        private void Open_Click(object sender, EventArgs e)
        {
            if (hall == null)
            {
                int totalPlaces = (int)dgvCinema.CurrentRow.Cells[4].Value;
                hall = new Hall(totalPlaces);
            }

            hall.ShowDialog();      
        }

    }
}
