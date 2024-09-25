using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Local_Civil_Registry_System
{
    public partial class Birth_Query : Form
    {
        public Birth_Query()
        {
            InitializeComponent();
        }

        private void Birth_Query_Load(object sender, EventArgs e)
        {
            this.Text = "Birth Query";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Size = new Size(803, 456);
        }

        private void btn_mother_name_Click(object sender, EventArgs e)
        {

        }

        private void btn_father_name_Click(object sender, EventArgs e)
        {

        }

        private void btn_registry_Click(object sender, EventArgs e)
        {

        }

        private async void btn_child_name_Click(object sender, EventArgs e)
        {
            MongodbConnect mc = new MongodbConnect();
            await mc.QueryBirthChild("John", "Sm");
        }


        private void Birth_Query_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
