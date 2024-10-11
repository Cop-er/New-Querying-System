using MongoDB.Bson;
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
    public partial class Marriage_Query : Form
    {
        public Marriage_Query()
        {
            InitializeComponent();
        }

        private void Birth_Query_Load(object sender, EventArgs e)
        {
            this.Text = "Marriage Query";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Size = new Size(803, 456);
        }

        private void btn_mother_name_Click(object sender, EventArgs e)
        {
            CheckEntry("Mother");
        }

        private void btn_father_name_Click(object sender, EventArgs e)
        {
            CheckEntry("Father");
        }

        private void btn_registry_Click(object sender, EventArgs e)
        {
            CheckEntry("Registry");
        }

        private void btn_child_name_Click(object sender, EventArgs e)
        {
            CheckEntry("Child");
        }

        private async void CheckEntry(string _type)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            this.Enabled = false;
            label9.Visible = true;

            MongodbConnect mc = new MongodbConnect();
            //Groom
            if (_type == "Child" && !string.IsNullOrWhiteSpace(txtChild_1.Text) && !string.IsNullOrWhiteSpace(txtChild_2.Text))
            {
                btn_child_name.Enabled = false;
                var _results = await mc.QueryMarriageHusband(txtChild_1.Text, txtChild_2.Text);
                TableDesign(_results);
                btn_child_name.Enabled = true;
            }

            //Bride
            else if (_type == "Mother" && !string.IsNullOrWhiteSpace(txtMother_1.Text) && !string.IsNullOrWhiteSpace(txtMother_2.Text))
            {
                btn_mother_name.Enabled = false;
                var _results = await mc.QueryMarriageWife(txtMother_1.Text, txtMother_2.Text);
                TableDesign(_results);
                btn_mother_name.Enabled = true;
            }

            else if (_type == "Registry" && !string.IsNullOrWhiteSpace(txtRegistry.Text))
            {
                btn_registry.Enabled = false;
                var _results = await mc.QueryMarriageRegistryNumber(txtRegistry.Text);
                TableDesign(_results);
                btn_registry.Enabled = true;
            }

            else
            {
                MessageBox.Show("Please enter both the first name and last name.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            label9.Visible = false;
            this.Enabled = true;

        }

        private void TableDesign(DataTable _results)
        {
            dataGridView1.DataSource = _results;
            dataGridView1.Columns["No."].Width = 30;
            dataGridView1.Columns["Registry Number"].Width = 80;
            dataGridView1.Columns["Groom MI"].Width = 40;
            dataGridView1.Columns["Bride MI"].Width = 40;
        }


        private void Birth_Query_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is Main mainForm) 
                {
                    mainForm.Show(); 
                    mainForm.BringToFront();
                    break;
                }
            };
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                MyFormatter mf = new MyFormatter();

                string FOL = dataGridView1.Rows[e.RowIndex].Cells[29].Value.ToString();
                string PAGE = dataGridView1.Rows[e.RowIndex].Cells[30].Value.ToString();

                string _LCR = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                string LCR = mf.formatRegistry(_LCR);
                string _DREG = dataGridView1.Rows[e.RowIndex].Cells[31].Value.ToString();
                string _DATEMAR = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                string PLACEMAR = "BISLIG, SURIGAO DEL SUR";
            

                string DATEMAR = mf.DateParser(_DATEMAR);
                string DREG = mf.DateParser(_DREG);




                //Groom Name
                string GFIRST = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                string GMI = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                string GLAST = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                string NC = $"{GFIRST}   {GMI}   {GLAST}";

                string GAGE = dataGridView1.Rows[e.RowIndex].Cells[21].Value.ToString();
                string GNATL = dataGridView1.Rows[e.RowIndex].Cells[23].Value.ToString();
                string GCS = dataGridView1.Rows[e.RowIndex].Cells[27].Value.ToString();



                //Groom Father
                string GFFIRST = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                string GFMI = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                string GFLAST = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                string GNOF = $"{GFFIRST}   {GFMI}   {GFLAST}";

                //Groom Mother
                string GMFIRST = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                string GMMI = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                string GMLAST = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                string GNOM = $"{GMFIRST}   {GMMI}   {GMLAST}";











                //Bride Name
                string BFIRST = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                string BMI = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                string BLAST = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                string BNC = $"{BFIRST}   {BMI}   {BLAST}";

                string BAGE = dataGridView1.Rows[e.RowIndex].Cells[22].Value.ToString();
                string BNATL = dataGridView1.Rows[e.RowIndex].Cells[24].Value.ToString();
                string BGCS = dataGridView1.Rows[e.RowIndex].Cells[28].Value.ToString();



                //Bride Father
                string BFFIRST = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
                string BFMI = dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString();
                string BFLAST = dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString();
                string BNOF = $"{BFFIRST}   {BFMI}   {BFLAST}";

                //Bride Mother
                string BMFIRST = dataGridView1.Rows[e.RowIndex].Cells[18].Value.ToString();
                string BMMI = dataGridView1.Rows[e.RowIndex].Cells[19].Value.ToString();
                string BMLAST = dataGridView1.Rows[e.RowIndex].Cells[20].Value.ToString();
                string BNOM = $"{BMFIRST}   {BMMI}   {BMLAST}";




                Nationality_Converter nc = new Nationality_Converter();
                var _FNATL = nc.GetNationalityKey(GNATL);
                var _MNATL = nc.GetNationalityKey(BNATL);





                Marriage_Data_Entry _bde = new Marriage_Data_Entry();

                _bde.Show();
                this.Hide();
            }
        }
    }
}
