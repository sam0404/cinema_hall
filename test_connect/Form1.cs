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
    public partial class Form1 : Form
    {
        private FilmsForm films = null;
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            LoadHalls();    
        }
        private void LoadTickets()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки:\n" + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = Microsoft.VisualBasic.Interaction.InputBox(
                    "Введите название подразделения:", "Добавление", "");

                // Если пусто (отмена или ничего не ввели) — выходим МОЛЧА
                if (string.IsNullOrWhiteSpace(name)) return;

                using (SqlConnection conn = new SqlConnection(Program.ConnString))
                {
                    conn.Open();

                    // Проверка на дубликат
                    SqlCommand checkCmd = new SqlCommand(
                        "SELECT COUNT(*) FROM Department WHERE Department_name = @name", conn);
                    checkCmd.Parameters.AddWithValue("@name", name.Trim());
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Такое подразделение уже существует.",
                            "Дубликат", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Department (Department_name) VALUES (@name)", conn);
                    cmd.Parameters.AddWithValue("@name", name.Trim());
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Подразделение успешно добавлено.",
                    "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);

               // DepartmentForm_Load(sender, e);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Не удалось добавить подразделение.\n\nПричина: " + ex.Message,
                    "Ошибка базы данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла непредвиденная ошибка.\n\nПричина: " + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private void LoadHalls()
        {
            try
            {
                string command = "select title from cinema_hall";

                using (SqlConnection conn = new SqlConnection(Program.ConnString))
                {
                    conn.Open();
                    SqlDataAdapter adapt = new SqlDataAdapter(command, conn);
                    DataTable table = new DataTable();
                    adapt.Fill(table);

                    int index = 0;

                    foreach (DataRow row in table.Rows)
                    {
                        string title = row["title"].ToString();
                        Button selectButton = new Button();
                        cinemaPanel.Controls.Add(selectButton);
                        selectButton.Size = new Size(100, 60);
                        selectButton.BackColor = SystemColors.ButtonFace;
                        selectButton.Location = new Point(3, index * 90);
                        selectButton.Text = title;
                        selectButton.Click += new System.EventHandler(button1_Click_1);
                        index++;
                        //MessageBox.Show(title, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (films == null)
            {
                films = new FilmsForm();
            }

            films.ShowDialog();
        }

       
    }


}
