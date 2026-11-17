using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace cinema
{
    public partial class SeansFormAddEdit : Form
    {
        private int hallId = 0;
        private int filmId = 0;
        private int timeId = 0;
        private DateTime date = new DateTime();
        private int ID = 0;

        private Operation currentOperation = Operation.ADD;
        public SeansFormAddEdit(int hallId)
        {
            currentOperation = Operation.ADD;
            Text = "Добавить сеанс";
            InitializeComponent();
            LoadData();

            this.hallId = hallId;
            cbHall.SelectedValue = this.hallId;
            date = DateTime.Now;
        }

        public SeansFormAddEdit(int id, int hallId, int filmId, int timeId, DateTime date)
        {
            ID = id;

            currentOperation = Operation.EDIT;
            Text = "Редактировать сеанс";
            InitializeComponent();
            LoadData();

            this.hallId = hallId ;
            this.filmId = filmId;
            this.timeId = timeId;
            this.date = date;
            
            btnAdd.Text = "Обновить";
            cbHall.SelectedValue = this.hallId;
            cbFilm.SelectedValue = this.filmId;
            cbTime.SelectedValue = this.timeId;
            dtpDate.Value = date;
        }

        private void LoadData()
        {
            FillComboxHall();
            FillComboxFilm();
            FillComboxTime();

            CheckFilmID();
            CheckHallID();
            CheckTimeID();
        }

        private void FillComboxHall()
        {
            string sql = "select id_hall, title FROM cinema_hall";

            using (SqlConnection conn = new SqlConnection(Program.ConnString))
            {
                SqlDataAdapter adapt = new SqlDataAdapter(sql, conn);
                DataTable table = new DataTable();
                adapt.Fill(table);

                //cbHall.DropDownStyle = ComboBoxStyle.DropDownList; 

                cbHall.DataSource = table;
                cbHall.DisplayMember = "title";
                cbHall.ValueMember = "id_hall";               
            }
        }

        private void FillComboxFilm()
        {
            string sql = "select film_id, title FROM film";

            using (SqlConnection conn = new SqlConnection(Program.ConnString))
            {
                SqlDataAdapter adapt = new SqlDataAdapter(sql, conn);
                DataTable table = new DataTable();
                adapt.Fill(table);

                cbFilm.DataSource = table;
                cbFilm.DisplayMember = "title";
                cbFilm.ValueMember = "film_id";
            }
        }

        private void FillComboxTime()
        {
            string sql = "select id_coeff, sesion_time FROM time_coeff";

            using (SqlConnection conn = new SqlConnection(Program.ConnString))
            {
                SqlDataAdapter adapt = new SqlDataAdapter(sql, conn);
                DataTable table = new DataTable();
                adapt.Fill(table);

                cbTime.DataSource = table;
                cbTime.DisplayMember = "sesion_time";
                cbTime.ValueMember = "id_coeff";
            }
        }

        private void CheckTimeID()
        {
            if (cbTime.SelectedValue is DataRowView)
            {
                DataRowView row = (DataRowView)cbTime.SelectedValue;
                timeId = Convert.ToInt32(row["id_coeff"]);
            }
            else if (cbFilm.SelectedValue != null)
            {
                timeId = Convert.ToInt32(cbTime.SelectedValue);
            }
        }

        private void CheckHallID()
        {
            if (cbHall.SelectedValue is DataRowView)
            {
                // Если вернулась строка — берем колонку
                DataRowView row = (DataRowView)cbHall.SelectedValue;
                hallId = Convert.ToInt32(row["id_hall"]);
            }
            else if (cbHall.SelectedValue != null)
            {
                // Если вернулось число (int) — просто конвертируем
                hallId = Convert.ToInt32(cbHall.SelectedValue);
            }
        }

        private void CheckFilmID()
        {
            if (cbFilm.SelectedValue is DataRowView)
            {
                DataRowView row = (DataRowView)cbFilm.SelectedValue;
                filmId = Convert.ToInt32(row["film_id"]);
            }
            else if (cbFilm.SelectedValue != null)
            {
                filmId = Convert.ToInt32(cbFilm.SelectedValue);
            }
        }

        private void cbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckTimeID();
        }

        private void cbFilm_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckFilmID();
        }

        private void cbHall_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckHallID();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            date = dtpDate.Value.Date; 
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (true)
            {
                btnAdd.DialogResult = DialogResult.OK;
                switch (currentOperation)
                {
                    case Operation.ADD:
                        AddSeans();
                        MessageBox.Show("Данные добавлены!");

                        break;

                    case Operation.EDIT:
                        EditSeans();
                        MessageBox.Show("Данные обновлены!");

                        break;
                }

                Close();
            }
            else
            {
                btnAdd.DialogResult = DialogResult.None;
                MessageBox.Show("Все поля должны быть заполнены!",
                 "Внимание",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning);
            }
        }

        private void AddSeans()
        {
            using (SqlConnection conn = new SqlConnection(Program.ConnString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("add_seans", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@film_id", filmId);
                    cmd.Parameters.AddWithValue("@id_coeff", timeId);
                    cmd.Parameters.AddWithValue("@id_hall", hallId);
                    cmd.Parameters.AddWithValue("@date", date);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void EditSeans()
        {
            using (SqlConnection conn = new SqlConnection(Program.ConnString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("update_seans", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@film_id", filmId);
                    cmd.Parameters.AddWithValue("@id_coeff", timeId);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@id_hall", hallId);
                    cmd.Parameters.AddWithValue("@id_seans", ID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
