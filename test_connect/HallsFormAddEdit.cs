using System;
using System.Data;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;

namespace cinema
{
    public partial class HallsFormAddEdit : Form
    {
        private Operation currentOperation = Operation.ADD;
        private int ID;
        private float priceKoeff = 1;
        public HallsFormAddEdit()
        {
            InitializeComponent();
            
            currentOperation = Operation.ADD;
            btnAdd.Text = "Добавить";
        }

        public HallsFormAddEdit(int id, string title, string koeff, decimal totalRows, decimal totalPlaceInRows)
        {
            InitializeComponent();
            currentOperation = Operation.EDIT;

            btnAdd.Text = "Обновить";

            ID = id;
            tbTitle.Text = title;
            tbKoeff.Text = koeff;
            nudRows.Value = totalRows;
            nudPlaceInRow.Value = totalPlaceInRows;
        }

        private void HallsFormAddEdit_Load(object sender, EventArgs e)
        {
            nudRows.Controls[0].Visible = false;
            nudPlaceInRow.Controls[0].Visible = false;
        }

        private bool IsValidate()
        {
            string text = tbKoeff.Text.Trim();
            text = text.Replace('.', ',');

            if (!float.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out priceKoeff))
            {
                MessageBox.Show("Значение должно быть числом!",
                 "Внимание",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning);

                return false;
            }

            string title = tbTitle.Text.Trim();
            if (title == ""){
                 MessageBox.Show("Поле должно быть заполнено!",
                 "Внимание",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning);

                return false;
            }

            if (nudPlaceInRow.Value == 0 || nudRows.Value == 0)
            {
                 MessageBox.Show("Значения должны быть больше 0!",
                 "Внимание",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        private void AddData()
        {
            string title = tbTitle.Text.Trim();
            int rows = (int)nudRows.Value;
            int placesPerRow = (int)nudPlaceInRow.Value;

            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Введите название зала!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(Program.ConnString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("add_hall_with_places", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@price_coeff", priceKoeff);
                        cmd.Parameters.AddWithValue("@rows_count", rows);
                        cmd.Parameters.AddWithValue("@places_per_row", placesPerRow);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            int newHallId = Convert.ToInt32(result);
                            MessageBox.Show("Зал успешно создан! ID: " + newHallId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при создании зала: " + ex.Message);
                }
            }
        }

        private void EditData()
        {
            string newTitle = tbTitle.Text.Trim();
            float newCoeff = Convert.ToSingle(tbKoeff.Text.Replace(',', '.'));
            int newRows = (int)nudRows.Value;
            int newPlacesPerRow = (int)nudPlaceInRow.Value;

            try
            {
                using (SqlConnection conn = new SqlConnection(Program.ConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("update_hall_layout", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Передаем все 5 параметров
                        cmd.Parameters.AddWithValue("@id_hall", ID);
                        cmd.Parameters.AddWithValue("@new_title", newTitle);
                        cmd.Parameters.AddWithValue("@new_price_coeff", newCoeff);
                        cmd.Parameters.AddWithValue("@new_rows", newRows);
                        cmd.Parameters.AddWithValue("@new_places_per_row", newPlacesPerRow);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Данные зала и планировка успешно обновлены!");
                    }
                }
            }
            catch (SqlException ex)
            {
                // Сюда попадет ошибка, если попытаетесь удалить места с проданными билетами
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!IsValidate())
            {
                btnAdd.DialogResult = DialogResult.None;
                return;
            }

            btnAdd.DialogResult = DialogResult.OK;

            switch (currentOperation)
            {
                case Operation.ADD:
                    AddData();

                    break;

                case Operation.EDIT:
                    EditData();

                    break;
            }
        }

    }
}
