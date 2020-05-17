using System;
using System.Windows.Forms;
using MyBicycle.Model;
using MyBicycle.Controllers;

namespace MyBicycle
{
    public partial class WINDOW_ADD : Form
    {
        private readonly WINDOW_MAIN window;
        public WINDOW_ADD(WINDOW_MAIN _window)
        {
            InitializeComponent();
            window = _window;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Database db = new Database(this);
            db.INSERT_NEW_CUSTOMER();
            window.UpdateData();
            window.Id = 1;
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
