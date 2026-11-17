using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace cinema
{
    public partial class SeansForm : Form
    {
        private Hall hall = null;
        private int placeInRow = 0;
        private int hallId = 0;
        public SeansForm()
        {
            InitializeComponent();
        }

        public SeansForm(int placeInRow, int hallId)
        {
            InitializeComponent();
            this.placeInRow = placeInRow;
            this.hallId = hallId;
        }

        private void FilmsForm_Load(object sender, EventArgs e)
        {
            LoadSeans();
        }
        private int ID { set; get; }

        private void LoadSeans()
        {
            string sql = "SELECT * FROM vw_TicketViewSeans where Зал = @title";

            using (SqlConnection conn = new SqlConnection(Program.ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@title", Program.Hall);

                    // Передаем в адаптер готовую команду (cmd), а не текст и подключение
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table); // Теперь параметр @title на месте!

                        dgvCinema.DataSource = table;

                        if (dgvCinema.Columns.Contains("id_seans"))
                        {
                            dgvCinema.Columns["id_seans"].Visible = false;
                        }

                        if (dgvCinema.Columns.Contains("id_hall"))
                        {
                            dgvCinema.Columns["id_hall"].Visible = false;
                        }

                        if (dgvCinema.Columns.Contains("film_id"))
                        {
                            dgvCinema.Columns["film_id"].Visible = false;
                        }

                        if (dgvCinema.Columns.Contains("id_coeff"))
                        {
                            dgvCinema.Columns["id_coeff"].Visible = false;
                        }

                        // Обязательно проверяем, что колонки существуют, прежде чем менять ширину
                        if (dgvCinema.Columns.Count >= 7)
                        {
                            dgvCinema.Columns[2].Width = 160;
                            dgvCinema.Columns[3].Width = 60;
                            dgvCinema.Columns[4].Width = 80;
                            dgvCinema.Columns[5].Width = 60;
                            dgvCinema.Columns[6].Width = 60;
                            dgvCinema.Columns[7].Width = 60;
                        }                                                
                    }
                }
            }
        }

        private void Open_Click(object sender, EventArgs e)
        {
            hall = new Hall((int)dgvCinema.CurrentRow.Cells[5].Value, placeInRow, (TimeSpan)dgvCinema.CurrentRow.Cells[3].Value, (DateTime)dgvCinema.CurrentRow.Cells[4].Value);            

            hall.ShowDialog();

            LoadSeans();
        }

        private void FilmsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            hall = null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SeansFormAddEdit seansForm = new SeansFormAddEdit(hallId);

            seansForm.ShowDialog();
            seansForm = null;

            LoadSeans();
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            SeansFormAddEdit seansForm = new SeansFormAddEdit(
                (int)dgvCinema.CurrentRow.Cells[0].Value,
                (int)dgvCinema.CurrentRow.Cells[8].Value,
                (int)dgvCinema.CurrentRow.Cells[9].Value,
                (int)dgvCinema.CurrentRow.Cells[10].Value,
                (DateTime)dgvCinema.CurrentRow.Cells[4].Value);

            seansForm.ShowDialog();
            seansForm = null;

            LoadSeans();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            SeansFormDelete seansForm = new SeansFormDelete(
                (int)dgvCinema.CurrentRow.Cells[0].Value,
                (string)dgvCinema.CurrentRow.Cells[1].Value,
                (string)dgvCinema.CurrentRow.Cells[2].Value,
                dgvCinema.CurrentRow.Cells[3].Value.ToString(),
                ((DateTime)dgvCinema.CurrentRow.Cells[4].Value).ToString("dd.MM.yyyy"));

            seansForm.ShowDialog();
            seansForm = null;

            LoadSeans();
        }
    }
}
