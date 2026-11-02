using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace test_connect
{
    public partial class FilmForm : Form
    {
        public FilmForm()
        {
            InitializeComponent();

            dgvFilms.RowPostPaint += dgvFilms_RowPostPaint;
            LoadFilms();
        }
        
        private void LoadFilms()
        {
            string sql = "SELECT film_id, title, price FROM film";

            using (SqlConnection conn = new SqlConnection(Program.ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table); 

                        dgvFilms.DataSource = table;

                        if (dgvFilms.Columns.Contains("film_id"))
                        {
                            dgvFilms.Columns["film_id"].Visible = false;
                        }

                        // Обязательно проверяем, что колонки существуют, прежде чем менять ширину
                        if (dgvFilms.Columns.Count >= 2)
                        {
                            dgvFilms.Columns[1].Width = 440;
                            dgvFilms.Columns[2].Width = 60;
                        }
                    }
                }
            }
        }

        private void dgvFilms_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Получаем номер строки (индекс + 1, так как индексы начинаются с 0)
            string rowNumber = (e.RowIndex + 1).ToString();

            // Настраиваем стиль текста
            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // Рисуем номер в первой колонке (или в заголовке строк)
            // Если вы хотите, чтобы номер был в первой колонке, используйте e.RowBounds.Left
            // Если хотите в заголовке строк (слева), используйте Rectangle.Empty
            Rectangle headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top,
                                                   dgvFilms.RowHeadersWidth, e.RowBounds.Height);

            e.Graphics.DrawString(rowNumber, this.Font, SystemBrushes.ControlText,
                                  headerBounds, centerFormat);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add_Edit_FilmForm editForm = new Add_Edit_FilmForm();
            
            editForm.ShowDialog();
            editForm = null;
            
            LoadFilms();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Add_Edit_FilmForm editForm = new Add_Edit_FilmForm((string)dgvFilms.CurrentRow.Cells[1].Value, dgvFilms.CurrentRow.Cells[2].Value.ToString(), (int)dgvFilms.CurrentRow.Cells[0].Value);
            
            editForm.ShowDialog();
            editForm = null;
            
            LoadFilms();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete_FilmForm deleteForm = new Delete_FilmForm((string)dgvFilms.CurrentRow.Cells[1].Value, dgvFilms.CurrentRow.Cells[2].Value.ToString(), (int)dgvFilms.CurrentRow.Cells[0].Value);

            deleteForm.ShowDialog();

            LoadFilms();
        }

    }
}
