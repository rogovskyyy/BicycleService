using System;
using System.Windows.Forms;
using MyBicycle.Controllers;
using MyBicycle.Model;

namespace MyBicycle
{
    public partial class WINDOW_MAIN : Form
    {
        public int Id { get; set; }

        private readonly WINDOW window;
        public WINDOW_MAIN()
        {
            InitializeComponent();
            dataGridView1.Columns.Add("Id", "Id");
            dataGridView1.Columns.Add("Imię", "Imię");
            dataGridView1.Columns.Add("Nazwisko", "Nazwisko");
            dataGridView1.Columns.Add("Telefon", "Telefon");
            dataGridView1.Columns.Add("Marka i/lub model sprzętu", "Marka i/lub model sprzętu");
            dataGridView1.Columns.Add("Data przyjęcia", "Data przyjęcia");
            dataGridView1.Columns.Add("Krótki opis", "Krótki opis");
            dataGridView1.Columns.Add("Status sprzętu", "Status sprzętu");
            dataGridView1.Columns.Add("Ostatnia aktualizacja", "Ostatnia aktualizacja");

            dataGridView1.Columns[0].Width = 30;
            dataGridView1.Columns[1].Width = 140;
            dataGridView1.Columns[2].Width = 140;

            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DisplayPresetData();
            dataGridView1.ClearSelection();
        }

        public WINDOW_MAIN(WINDOW form) : base()
        {
            window = form;
            window.Hide();
        }

        public void DisplayPresetData()
        {
            Database db = new Database();
            foreach (var item in db.GetAll())
            {
                int rowId = dataGridView1.Rows.Add();

                DataGridViewRow row = dataGridView1.Rows[rowId];

                row.Cells[0].Value = item.Id;
                row.Cells[1].Value = item.Name;
                row.Cells[2].Value = item.Surname;
                row.Cells[3].Value = item.Phone;
                row.Cells[4].Value = item.Model;
                row.Cells[5].Value = item.Date;
                row.Cells[6].Value = item.Description;
                if (item.IsActive == true)
                {
                    row.Cells[7].Style.ForeColor = System.Drawing.Color.OrangeRed;
                    row.Cells[7].Value = "W SERWISIE";
                }
                else
                {
                    row.Cells[7].Style.ForeColor = System.Drawing.Color.DarkGreen;
                    row.Cells[7].Value = "WYDANY";
                }
                row.Cells[8].Value = item.LastUpdate;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            WINDOW_ADD window = new WINDOW_ADD(this);
            window.ShowDialog();
            dataGridView1.ClearSelection();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                WINDOW_EDIT window = new WINDOW_EDIT(this);
                window.ShowDialog();
                dataGridView1.ClearSelection();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                Builder o = new Builder();
                Database db = new Database();
                Customers model = db.SELECT_TO_PDF(Id);
                o.GeneratePdf(model);
                dataGridView1.ClearSelection();
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                Database db = new Database();
                db.UPDATE_STATUS(Id, false);
                UpdateData();
                dataGridView1.ClearSelection();
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                Database db = new Database();
                db.UPDATE_STATUS(Id, true);
                UpdateData();
                dataGridView1.ClearSelection();
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                WINDOW_VIEW window = new WINDOW_VIEW(this);
                window.ShowDialog();
                dataGridView1.ClearSelection();
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            WINDOW_CONFIG window = new WINDOW_CONFIG();
            window.ShowDialog();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            WINDOW_FIND window = new WINDOW_FIND();
            window.ShowDialog();
        }

        public void UpdateData()
        {
            dataGridView1.Rows.Clear();
            DisplayPresetData();
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Id = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void WINDOW_MAIN_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult window = MessageBox.Show("Czy chcesz wyjść z aplikacji?", "Powiadomienie", MessageBoxButtons.YesNo);

            if (window == DialogResult.Yes)
            {
                System.Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}