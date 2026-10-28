using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
                    conn.Close();
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
