using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace cinema
{
    public partial class HallsFormDelete : Form
    {
        private int ID { get; set; }
        public HallsFormDelete()
        {
            InitializeComponent();
        }

        public HallsFormDelete(int id, string title)
        {
            InitializeComponent();

            ID = id;
            lblTitle.Text = title;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteHall();
        }

        private void DeleteHall()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Program.ConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("delete_hall", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_hall", ID);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Зал и связанные с ним данные успешно удалены!");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
