using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace lab1
{
    public partial class Form1 : Form
    {
        private BindingSource bindingSource = new BindingSource();
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=lb1;Integrated Security=True;";

        // Створюємо екземпляр нашої нової проміжної ланки
        private DataManager dbManager = new DataManager();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                dataGridView1.AutoGenerateColumns = true;

                // Звертаємося до проміжної ланки замість прямих SQL запитів
                dataTable = dbManager.GetTableData("Master");

                bindingSource.DataSource = dataTable;
                dataGridView1.DataSource = bindingSource;

                if (dataGridView1.Columns.Count > 0)
                {
                    RenameHeaders();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка БД: " + ex.Message);
            }
        }
        private void RenameHeaders()
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                string fileName = column.DataPropertyName.ToUpper();

                if (fileName == "SYMBOL")
                    column.HeaderText = "Символ";
                else if (fileName == "CO_NAME")
                    column.HeaderText = "Компанія";
                else if (fileName == "CUR_PRICE")
                    column.HeaderText = "Ціна";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bindingSource.MovePrevious();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bindingSource.MoveNext();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bindingSource.AddNew();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bindingSource.RemoveCurrent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Розробник: Студентка 3 курсу Шиян С.О.\nСпеціальність: Програмування\nВаріант 5", "Про програму", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                bindingSource.Filter = "";
            }
            else
            {
                bindingSource.Filter = string.Format("CO_NAME LIKE '%{0}%'", textBox1.Text);
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            LoadData();
        }

        private void holdingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHoldings fh = new FormHoldings();
            fh.Show();
        }

        private void industryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormIndustry fi = new FormIndustry();
            fi.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.EndEdit();
                bindingSource.EndEdit();

                // Передаємо змінені дані нашому менеджеру для збереження
                dbManager.SaveTableData(dataTable, "Master");

                MessageBox.Show("Дані успішно збережено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка збереження: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Додає новий порожній рядок у таблицю для введення інформації про нову акцію.";
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Готово";
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Видаляє вже існуючий рядок акції у таблиці.";
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Готово";
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Зберігає новий рядок у таблицю для виведення інформації про нову акцію.";
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Готово";
        }

        private void button7_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Пошук серед наявних рядків у таблиці за назвою компанії чи її ініціалами.";
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Готово";
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void змістToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Отримуємо шлях до файлу index.html
                string helpFilePath = System.IO.Path.Combine(Application.StartupPath, "Help", "index.html");

                // Спеціальний запуск для .NET 8 (дозволяє системі самій обрати браузер)
                var processInfo = new System.Diagnostics.ProcessStartInfo(helpFilePath)
                {
                    UseShellExecute = true
                };

                System.Diagnostics.Process.Start(processInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не вдалося відкрити довідку: " + ex.Message);
            }
        }
    }
}
