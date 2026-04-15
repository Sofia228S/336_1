using System;
using System.Data;
using System.Windows.Forms;

namespace lab1
{
    public partial class FormIndustry : Form
    {
        private BindingSource bindingSource = new BindingSource();
        private DataTable dataTable;

        // Підключаємо наш шар роботи з даними (тонкий клієнт)
        private DataManager dbManager = new DataManager();

        public FormIndustry()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormIndustry_Load);
        }

        private void FormIndustry_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.AutoGenerateColumns = true;

                // Отримуємо дані через DataManager
                dataTable = dbManager.GetTableData("Industry");

                bindingSource.DataSource = dataTable;
                dataGridView1.DataSource = bindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження Industry: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.EndEdit();
                bindingSource.EndEdit();

                // Зберігаємо зміни через DataManager
                dbManager.SaveTableData(dataTable, "Industry");

                MessageBox.Show("Дані таблиці Industry збережено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка збереження: " + ex.Message);
            }
        }
    }
}
