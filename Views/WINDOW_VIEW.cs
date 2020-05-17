using System;
using System.Windows.Forms;
using MyBicycle.Controllers;

namespace MyBicycle
{
    public partial class WINDOW_VIEW : Form
    {
        private readonly WINDOW_MAIN window;
        private readonly Database db;

        public WINDOW_VIEW(WINDOW_MAIN _window)
        {
            InitializeComponent();
            window = _window;

            db = new Database();

            var result = db.SELECT_ONE(window.Id);

            textBox1.Text = result.Name;
            textBox2.Text = result.Surname;
            maskedTextBox1.Text = result.Phone;
            textBox3.Text = result.Model;
            dateTimePicker1.Value = result.Date;
            textBox4.Text = result.Description;
            textBox5.Text = result.Description_Long;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
