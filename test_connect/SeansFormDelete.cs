using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace cinema
{
    public partial class SeansFormDelete : Form
    {
        private int ID = 0;
        public SeansFormDelete(int id, string hallName, string filmName, string time, string date)
        {
            ID = id;
            InitializeComponent();

            lblHall.Text = hallName;
            lblFilm.Text = filmName;
            lblTime.Text = time;
            lblDate.Text = date;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteSeans();
        }

        private void DeleteSeans()
        {
            string sql = "DELETE FROM seans WHERE id_seans = @SeansID";

            using (SqlConnection conn = new SqlConnection(Program.ConnString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@SeansID", ID);

                    int rows = cmd.ExecuteNonQuery(); // Возвращает количество удаленных строк

                    if (rows > 0)
                    {
                        MessageBox.Show("Сеанс успешно удален!");
                    }
                    else
                    {
                        MessageBox.Show("Сеанс с таким ID не найден!",
                            "Внимание",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
            }
        }
    }
}
