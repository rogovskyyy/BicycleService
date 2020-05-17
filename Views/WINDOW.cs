using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyBicycle.Controllers;
using MyBicycle.Model;


namespace MyBicycle
{
    public partial class WINDOW : Form
    {
        private readonly Activate controller;
        private readonly WINDOW_MAIN window;
        public WINDOW()
        {
            InitializeComponent();
            textBox1.CharacterCasing = CharacterCasing.Upper;
            textBox2.CharacterCasing = CharacterCasing.Upper;
            textBox3.CharacterCasing = CharacterCasing.Upper;
            textBox4.CharacterCasing = CharacterCasing.Upper;
            textBox5.CharacterCasing = CharacterCasing.Upper;

            controller = new Activate();
            window = new WINDOW_MAIN();
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            string myText = $"{textBox1.Text}{textBox2.Text}{textBox3.Text}{textBox4.Text}{textBox5.Text}";
            bool status = await Task.Run(() => controller.MakeRequest(myText));

            if (status)
            {
                Properties.Settings.Default["SERIAL"] = myText;
                Properties.Settings.Default.Save();
                window.Show();
                Hide();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WINDOW_Shown(object sender, EventArgs e)
        {
            if (controller.IsProductValidAndActivated())
            {
                window.Show();
                Hide();
            }
        }
    }
}