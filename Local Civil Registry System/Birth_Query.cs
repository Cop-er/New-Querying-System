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
            if (_type == "Child" && !string.IsNullOrWhiteSpace(txtChild_1.Text) && !string.IsNullOrWhiteSpace(txtChild_2.Text))
            {
                btn_child_name.Enabled = false;
                var _results = await mc.QueryBirthChild(txtChild_1.Text, txtChild_2.Text);
                TableDesign(_results);
                btn_child_name.Enabled = true;
            }

            else if (_type == "Mother" && !string.IsNullOrWhiteSpace(txtMother_1.Text) && !string.IsNullOrWhiteSpace(txtMother_2.Text))
            {
                btn_mother_name.Enabled = false;
                var _results = await mc.QueryMotherChild(txtMother_1.Text, txtMother_2.Text);
                TableDesign(_results);
                btn_mother_name.Enabled = true;
            }

            else if (_type == "Father" && !string.IsNullOrWhiteSpace(txtFather_1.Text) && !string.IsNullOrWhiteSpace(txtFather_2.Text))
            {
                btn_father_name.Enabled = false;
                var _results = await mc.QueryFatherChild(txtFather_1.Text, txtFather_2.Text);
                TableDesign(_results);
                btn_father_name.Enabled = true;
            }

            else if (_type == "Registry" && !string.IsNullOrWhiteSpace(txtRegistry.Text))
            {
                btn_registry.Enabled = false;
                var _results = await mc.QueryRegistryNumber(txtRegistry.Text);
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
            dataGridView1.Columns["MI"].Width = 40;
            dataGridView1.Columns["Mother MI"].Width = 40;
            dataGridView1.Columns["Father MI"].Width = 40;
        }


        private void Birth_Query_FormClosed(object sender, FormClosedEventArgs e)
        {
           // foreach (Form form in Application.OpenForms)
           // {
           //     if (form is Main mainForm)
           //     {
           //         mainForm.Show();
           //         mainForm.BringToFront();
           //         break;
           //     }
           // };
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string LCR = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

                string FIRST = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                string MI = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                string LAST = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

                string NC = $"{FIRST}   {MI}   {LAST}";


                string MFIRST = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                string MMI = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                string MLAST = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                string MNATL = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();

                string NOM = $"{MFIRST}   {MMI}   {MLAST}";

                string FFIRST = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                string FMI = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                string FLAST = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                string FNATL = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();

                string NOF = $"{FFIRST}   {FMI}   {FLAST}";


                string DATE = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                string SEX = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                string FOL = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
                string PAGE = dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString();
                string DATEMAR = dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString();
                string PLACEMAR = dataGridView1.Rows[e.RowIndex].Cells[18].Value.ToString();
                string DREG = dataGridView1.Rows[e.RowIndex].Cells[19].Value.ToString();

                string POB = "BISLIG, SURIGAO DEL SUR";

                Nationality_Converter nc = new Nationality_Converter();
                var _MNATL = nc.GetNationalityKey(MNATL);
                var _FNATL = nc.GetNationalityKey(FNATL);

                Birth_Data_Entry _bde = new Birth_Data_Entry();
                _bde.Init_Data_Entry(
                    FOL,
                    PAGE,
                    LCR,
                    DREG,
                    NC,
                    SEX,
                    DATE,
                    POB,
                    NOM,
                    _MNATL,
                    NOF,
                    _FNATL,
                    DATEMAR,
                    PLACEMAR
                    );

                _bde.Show();
                this.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
