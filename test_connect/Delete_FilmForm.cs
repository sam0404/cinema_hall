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
    public partial class Delete_FilmForm : Form
    {
        private int ID;
        public Delete_FilmForm()
        {
            InitializeComponent();
        }

        public Delete_FilmForm(string title, string price, int id)
        {
            InitializeComponent();

            ID = id;
            lblName.Text = title;
            lblPrice.Text = price;

        }

        private void DeleteFilm()
        {
            string sql = "DELETE FROM film WHERE film_id = @FilmID";

            using (SqlConnection conn = new SqlConnection(Program.ConnString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@FilmID", ID);

                    int rows = cmd.ExecuteNonQuery(); // Возвращает количество удаленных строк

                    if (rows > 0)
                    {
                        MessageBox.Show("Фильм успешно удален!");
                    }
                    else
                    {
                        MessageBox.Show("Фильм с таким ID не найден!",
                            "Внимание",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteFilm();
        }
    }
}
