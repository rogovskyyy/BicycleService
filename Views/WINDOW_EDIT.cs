using System;
using System.Windows.Forms;
using MyBicycle.Model;
using MyBicycle.Controllers;

namespace MyBicycle
{
    public partial class WINDOW_EDIT : Form
    {
        private int ID { get; set; }
        private bool ACTIVE { get; set; }
        private readonly WINDOW_MAIN window;
        private readonly Database db;
        public WINDOW_EDIT(WINDOW_MAIN Window)
        {
            InitializeComponent();
            window = Window;
            db = new Database();
            Customers result = db.SELECT_ONE(window.Id);

            ID = result.Id;
            ACTIVE = result.IsActive;
            textBox1.Text = result.Name;
            textBox2.Text = result.Surname;
            maskedTextBox1.Text = result.Phone;
            textBox3.Text = result.Model;
            dateTimePicker1.Value = result.Date;
            textBox4.Text = result.Description;
            textBox5.Text = result.Description_Long;

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Customers model = new Customers
            {
                Id = ID,
                Name = textBox1.Text,
                Surname = textBox2.Text,
                Phone = maskedTextBox1.Text,
                Model = textBox3.Text,
                Date = dateTimePicker1.Value,
                Description = textBox4.Text,
                Description_Long = textBox5.Text,
                LastUpdate = DateTime.Now,
                IsActive = ACTIVE
            };

            db.UPDATE_CUSTOMER(model);
            window.UpdateData();
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
