using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace cinema
{
    public partial class Add_Edit_FilmForm : Form
    {
        private Operation currntOperation = Operation.ADD;

        private string title = "";
        private decimal price = 0;
        private int ID = 0;

        public Add_Edit_FilmForm()
        {
            currntOperation = Operation.ADD;
            Text = "Добавить фильм";
            InitializeComponent();
        }
        public Add_Edit_FilmForm(string title, string price, int id)
        {
            ID = id;
            currntOperation = Operation.EDIT;
            InitializeComponent();
            btnAdd.Text = "Обновить";
            Text = "Редактировать фильм";

            tbFilmName.Text = title;
            tbFilmPrice.Text = price;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValidateData())
            {
                btnAdd.DialogResult = DialogResult.OK;
                switch(currntOperation){
                    case Operation.ADD:
                        AddFilm();
                        MessageBox.Show("Данные добавлены!");

                        break;

                    case Operation.EDIT:
                        EditFilm();
                        MessageBox.Show("Данные обновлены!");

                        break;
                }
                                
                Close();
            }
            else
            {
                btnAdd.DialogResult = DialogResult.None;
                MessageBox.Show("Проверьте наличие и правильность данных!",
                 "Внимание",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning);
            }
        }

        private void AddFilm()
        {
            using (SqlConnection conn = new SqlConnection(Program.ConnString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("add_film", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@price", price);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void EditFilm()
        {
            using (SqlConnection conn = new SqlConnection(Program.ConnString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("update_film", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@film_id", ID);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        private bool IsValidateData()
        {
            title = tbFilmName.Text.Trim();
            bool validateName =  title != "";
            bool validatePrice = false;            

            string normalizedText = tbFilmPrice.Text.Trim().Replace(',', '.');

            validatePrice = decimal.TryParse(normalizedText, 
                            NumberStyles.Any, 
                            CultureInfo.InvariantCulture,
                            out price);

            if (validatePrice)
            {
            // Проверяем, что цена не отрицательная
                if (price < 0)
                {
                    MessageBox.Show("Цена не может быть отрицательной!");
                    validatePrice = false;
                }
            }

            return validatePrice && validateName;
        }
    }
}
