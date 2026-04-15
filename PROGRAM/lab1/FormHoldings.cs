using System;
using System.Data;
using System.Windows.Forms;

namespace lab1
{
    public partial class FormHoldings : Form
    {
        private BindingSource bindingSource = new BindingSource();
        private DataTable dataTable;

        // Підключаємо наш шар роботи з даними (тонкий клієнт)
        private DataManager dbManager = new DataManager();

        public FormHoldings()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormHoldings_Load);
        }

        private void FormHoldings_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.AutoGenerateColumns = true;

                // Отримуємо дані через DataManager
                dataTable = dbManager.GetTableData("Holdings");

                bindingSource.DataSource = dataTable;
                dataGridView1.DataSource = bindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження Holdings: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.EndEdit();
                bindingSource.EndEdit();

                // Зберігаємо зміни через DataManager
                dbManager.SaveTableData(dataTable, "Holdings");

                MessageBox.Show("Дані таблиці Holdings збережено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка збереження: " + ex.Message);
            }
        }
    }
}
