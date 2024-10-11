using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Local_Civil_Registry_System
{
    public partial class Birth_Data_Entry : Form
    {
        dynamic _showRemarks = "false";
        dynamic _showHeader = "false";

        public Birth_Data_Entry()
        {
            InitializeComponent();
        }

        private void Birth_Data_Entry_Load(object sender, EventArgs e)
        {
            this.Size = new Size(807, 559);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            this.MaximizeBox = false;
            button4.BackColor = Color.FromName("black");
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
            closingForm();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.BackColor == Color.FromArgb(64, 0, 64))
            {
                button4.BackColor = Color.FromName("Black");
                Remarks_Sizing_No_Remarks();
                _showRemarks = "false";

            } else
            {
                button4.BackColor = Color.FromArgb(64, 0, 64);
                Remarks_Sizing_With_Remarks();
                _showRemarks = "true";
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.BackColor == Color.FromArgb(64, 0, 64))
            {
                button3.BackColor = Color.FromName("Black");
                _showHeader = "false";

            }
            else
            {
                button3.BackColor = Color.FromArgb(64, 0, 64);
                _showHeader = "true";
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            Print_Data();
        }

        private async Task<string> Title_Signature(string data)
        {

            MongodbConnect mc = new MongodbConnect();
            var document = await mc.LCR_Signatures_Title(data);
            if (document != null && document.Contains("Appointed_Position"))
            {
                var appointedPosition = document["Appointed_Position"].AsString;
                return appointedPosition;
            }

            return "";
        }


        private async void Print_Data()
        {
            string BN = textBox15.Text;
            string PG = textBox16.Text;
            string RN = textBox1.Text;
            string DOR = textBox2.Text;
            string NC = textBox3.Text;
            string SEX = textBox4.Text;
            string DOB = textBox5.Text;
            string POB = textBox6.Text;
            string NOM = textBox7.Text;
            string COM = textBox8.Text;
            string NOF = textBox9.Text;
            string COF = textBox10.Text;
            string DOMOP = textBox11.Text;
            string PLACEMAR = textBox12.Text;
            string NCC = textBox13.Text;
            string URO = textBox14.Text;
            string VS = textBox17.Text; 
            string AS = textBox18.Text; 

            string VI = textBox14.Text; 

            string OR = textBox20.Text; 
            string DT = textBox19.Text;

            string CIT = await Title_Signature(textBox18.Text);
            string VST = await Title_Signature(textBox17.Text);

            string DI = _showRemarks; //Remarks Showing
            string Remarks = textBox21.Text;


            BirthReport rp = new BirthReport();
            rp.InitializeReportViewer(
                BN, PG, RN, DOR, NC, SEX, DOB, POB, NOM, COM, NOF, COF, DOMOP, PLACEMAR, NCC, URO, VS, AS, VI, DI, OR, DT, CIT, VST, Remarks, _showHeader
            );

            rp.Show();

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void Birth_Data_Entry_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void closingForm()
        {
            Birth_Query.Instance.Show();
            this.Close();
        }
    }  
}
