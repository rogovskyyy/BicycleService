using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MyBicycle.Controllers;
using MyBicycle.Model;

namespace MyBicycle
{
    public partial class WINDOW_FIND : Form
    {
        private readonly Database db;
        public WINDOW_FIND()
        {
            InitializeComponent();

            db = new Database();

            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Imię", "Imię");
            dataGridView1.Columns.Add("Nazwisko", "Nazwisko");
            dataGridView1.Columns.Add("Telefon", "Telefon");
            dataGridView1.Columns.Add("Marka i/lub model sprzętu", "Marka i/lub model sprzętu");
            dataGridView1.Columns.Add("Data przyjęcia", "Data przyjęcia");
            dataGridView1.Columns.Add("Krótki opis", "Krótki opis");
            dataGridView1.Columns.Add("Status sprzętu", "Status sprzętu");
            dataGridView1.Columns.Add("Ostatnia aktualizacja", "Ostatnia aktualizacja");

            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns[0].Width = 25;
            dataGridView1.Columns[1].Width = 140;
            dataGridView1.Columns[2].Width = 140;
            dataGridView1.Columns[3].Width = 140;


        }

        private void DisplayPresetData(List<Customers> result)
        {
            foreach (var item in result)
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
            dataGridView1.Rows.Clear();
            DisplayPresetData(db.SELECT_ALL_WHERE_PHONE(textBox1.Text));
            label5.Text = db.COUNTER.ToString();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            DisplayPresetData(db.SELECT_ALL_BETWEEN_DATE(dateTimePicker1.Value, dateTimePicker2.Value));
            label5.Text = db.COUNTER.ToString();
        }
    }
}