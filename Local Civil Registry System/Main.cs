﻿using System;
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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            this.Text = "Main Menu";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Size = new Size(803, 456);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Birth_Query BirthQueryForm = new Birth_Query();
            BirthQueryForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Marriage_Query Marriage_Query_Form = new Marriage_Query();
            Marriage_Query_Form.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Death_Query Death_Query_Form = new Death_Query();
            Death_Query_Form.Show();
            this.Hide();
        }
    }
}
