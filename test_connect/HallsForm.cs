using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace cinema
{
    public partial class HallsForm : Form
    {
        public HallsForm()
        {
            InitializeComponent();
        }

        private void HallsForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string sql = "SELECT * FROM vw_HallInfo";

            using (SqlConnection conn = new SqlConnection(Program.ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table); 

                        dgvHalls.DataSource = table;

                        if (dgvHalls.Columns.Contains("id_hall"))
                        {
                            dgvHalls.Columns["id_hall"].Visible = false;
                        }                       

                        if (dgvHalls.Columns.Count >= 4)
                        {
                            dgvHalls.Columns[1].Width = 240;
                            dgvHalls.Columns[2].Width = 60;
                        }                                                
                    }
                }
            }        
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            HallsFormAddEdit addForm = new HallsFormAddEdit();

            addForm.ShowDialog();
            addForm = null;

            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            decimal rows = Convert.ToDecimal(dgvHalls.CurrentRow.Cells["Всего мест"].Value) / Convert.ToDecimal(dgvHalls.CurrentRow.Cells["Мест в ряду"].Value);
            HallsFormAddEdit editForm = new HallsFormAddEdit((int)dgvHalls.CurrentRow.Cells["id_hall"].Value, dgvHalls.CurrentRow.Cells["Название зала"].Value.ToString(), dgvHalls.CurrentRow.Cells["Ценовой коэффициент"].Value.ToString(), rows, Convert.ToDecimal(dgvHalls.CurrentRow.Cells["Мест в ряду"].Value));

            editForm.ShowDialog();
            editForm = null;

            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string columnNames = "";

            HallsFormDelete deleteForm = new HallsFormDelete((int)dgvHalls.CurrentRow.Cells["id_hall"].Value, dgvHalls.CurrentRow.Cells["Название зала"].Value.ToString());

            deleteForm.ShowDialog();
            deleteForm = null;

            LoadData();
        }
    }
}
