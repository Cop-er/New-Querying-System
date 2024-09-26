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
    public partial class Birth_Data_Entry : Form
    {
        public Birth_Data_Entry()
        {
            InitializeComponent();
        }

        private void Birth_Data_Entry_Load(object sender, EventArgs e)
        {
            this.Size = new Size(807, 559);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            this.MaximizeBox = false;
            button4.BackColor = Color.FromName("ControlLight");
            Center_Screen();
            Add_Signatures();
        }

        private void Center_Screen()
        {
            var screen = Screen.PrimaryScreen.WorkingArea;
            int x = (screen.Width - this.Width) / 2;
            int y = (screen.Height - this.Height) / 2;
            this.Location = new Point(x, y);
        }

        private void Remarks_Sizing_With_Remarks()
        {
            tableLayoutPanel1.Dock = DockStyle.Left;
            this.Size = new Size(1300, 559);
            tableLayoutPanel4.Visible = true;
            Center_Screen();
        }

        private void Remarks_Sizing_No_Remarks()
        {
            this.Size = new Size(807, 559);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel4.Visible = false;
            Center_Screen();
        }

        private async void Add_Signatures()
        {
            textBox17.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox17.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox18.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox18.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection autoCompleteData = new AutoCompleteStringCollection();

            MongodbConnect mc = new MongodbConnect();
            var _doc = await mc.LCR_Signatures();

            foreach (var sign in _doc)
            {
                if (sign.Contains("UserName"))
                {
                    string _sign = sign["UserName"].AsString;
                    autoCompleteData.Add(_sign);
                }
            }

            textBox17.AutoCompleteCustomSource = autoCompleteData;
            textBox18.AutoCompleteCustomSource = autoCompleteData;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Birth_Query bq = new Birth_Query();
            bq.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.BackColor == Color.FromArgb(128, 128, 255))
            {
                button4.BackColor = Color.FromName("ControlLight");
                Remarks_Sizing_No_Remarks();

            } else
            {
                button4.BackColor = Color.FromArgb(128, 128, 255);
                Remarks_Sizing_With_Remarks();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
