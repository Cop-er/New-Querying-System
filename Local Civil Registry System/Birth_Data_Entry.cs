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

            var (_withTime, _noTime) = Date_Time_Now();
            textBox19.Text = _noTime;

           
        }

             

        private (string _withTime, string _noTime) Date_Time_Now()
        {
            DateTime dt = DateTime.Now;
            string _formatDT_with_time = dt.ToString("MMMM dd, yyyy HH:mm:ss");
            string _formatDT_no_time = dt.ToString("MMMM dd, yyyy");
            return (_formatDT_with_time, _formatDT_no_time);
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

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 21; i++)
            {
                string textBoxName = $"textBox{i}";
                Control textBox = this.Controls.Find(textBoxName, true).FirstOrDefault();
                if (textBox is TextBox tb)
                {
                    if (textBox.Name != "textBox21")
                    {
                        tb.Text = "";
                    }
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var (wt, nt) = Date_Time_Now();
            dt_issue.Text = wt;
        }

        //13 Entries
        public void Init_Data_Entry(
            String BN,
            String PG,
            String RN,
            String DOR,
            String NC,
            String SEX,
            String DOB,
            String POB,
            String NOM,
            String COM,
            String NOF,
            String COF,
            String DOMOP,
            String PLACEMAR
            )
        {

            string _SEX = "";
            string _URO = "";

            if (SEX == "1")
            {
                _SEX = "MALE";
                _URO = "upon HIS request";
            } else {
                _SEX = "FEMALE";
                _URO = "upon HER request";
             };

            PlaceOfMarriageCodes pomc = new PlaceOfMarriageCodes();
            var _PLACEMAR = pomc.GetPlacesCodes(PLACEMAR);

            textBox1.Text = RN;
            textBox2.Text = DOR;
            textBox3.Text = NC;
            textBox4.Text = _SEX;
            textBox5.Text = DOB;
            textBox6.Text = POB;
            textBox7.Text = NOM;
            textBox8.Text = COM;
            textBox9.Text = NOF;
            textBox10.Text = COF; 
            textBox11.Text = DOMOP;
            textBox12.Text = _PLACEMAR;
            textBox13.Text = NC;
            textBox14.Text = _URO;
            textBox15.Text = BN;
            textBox16.Text = PG;
            textBox17.Text = "ROLANDO E. CEMANES";
            textBox18.Text = "ENGR. CERELITO V. BASAÑEZ, MPA";
            //textBox20.Text = RN;
            //textBox21.Text = RN;

        }
    }  
}
