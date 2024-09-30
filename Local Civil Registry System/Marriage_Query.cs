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
    }
}
