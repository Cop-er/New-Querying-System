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
    public partial class ReportViewer : Form
    {
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;

        public ReportViewer()
        {
            InitializeComponent();
           InitializeReportViewer();
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {

        }

        private void InitializeReportViewer()
        {
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            //this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.Dock = DockStyle.Fill;
            this.reportViewer1.TabIndex = 0;
            this.Controls.Add(this.reportViewer1);

            MongodbConnect mc = new MongodbConnect();
            string _path_1 = mc._birthPrintForm;
            reportViewer1.LocalReport.ReportPath = _path_1;

            ReportParameter[] parameters = new ReportParameter[25];
            parameters[0] = new ReportParameter("RN", "Value1");
            parameters[1] = new ReportParameter("DOR", "Value2");
            parameters[2] = new ReportParameter("NC", "Value3");
            parameters[3] = new ReportParameter("SEX", "Value4");
            parameters[4] = new ReportParameter("DOB", "Value5");
            parameters[5] = new ReportParameter("POB", "Value6");
            parameters[6] = new ReportParameter("NOM", "Value7");
            parameters[7] = new ReportParameter("COM", "Value8");
            parameters[8] = new ReportParameter("NOF", "Value9");
            parameters[9] = new ReportParameter("COF", "Value10");
            parameters[10] = new ReportParameter("DOMOP", "Value11");
            parameters[11] = new ReportParameter("PLACEMAR", "Value12");
            parameters[12] = new ReportParameter("NCC", "Value13");
            parameters[13] = new ReportParameter("URO", "Value13");
            parameters[14] = new ReportParameter("BN", "999");
            parameters[15] = new ReportParameter("PG", "999");
            parameters[16] = new ReportParameter("VS", "Value13");
            parameters[17] = new ReportParameter("AS", "Value13");
            parameters[18] = new ReportParameter("DI", "Value13");
            parameters[19] = new ReportParameter("OR", "Value13");
            parameters[20] = new ReportParameter("ShowHeader", "false");
            parameters[21] = new ReportParameter("DT", "Date");
            parameters[22] = new ReportParameter("CIT", "Date");
            parameters[23] = new ReportParameter("VST", "Date");
            parameters[24] = new ReportParameter("Remarks", "Date");
            reportViewer1.LocalReport.SetParameters(parameters);


            reportViewer1.CurrentPage = 1;
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.RefreshReport();

            
        }
    }
}
