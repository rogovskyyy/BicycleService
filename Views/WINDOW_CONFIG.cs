using System;
using System.Windows.Forms;
using MyBicycle.Controllers;
using MyBicycle.Model;

namespace MyBicycle
{
    public partial class WINDOW_CONFIG : Form
    {
        public WINDOW_CONFIG()
        {
            InitializeComponent();
            Config n = new Config();
            textBox1.Text = n.Name;
            textBox2.Text = n.Street;
            textBox3.Text = n.City;
            maskedTextBox3.Text = n.Post;
            maskedTextBox2.Text = n.Phone;
            textBox5.Text = n.Email;
            maskedTextBox1.Text = n.License;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["NAME"] = textBox1.Text.ToString();
            Properties.Settings.Default["STREET"] = textBox2.Text.ToString();
            Properties.Settings.Default["CITY"] = textBox3.Text.ToString();
            Properties.Settings.Default["POST"] = maskedTextBox3.Text.ToString();
            Properties.Settings.Default["PHONE"] = maskedTextBox2.Text.ToString();
            Properties.Settings.Default["EMAIL"] = textBox5.Text.ToString();
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
