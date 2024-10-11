using Microsoft.Reporting.WinForms;
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
    public partial class MarriageReport : Form
    {

        public MarriageReport()
        {
            InitializeComponent();
        }

        private void Printing_Load(object sender, EventArgs e)
        {
            InitReport();
            this.reportViewer1.RefreshReport();
        }

        private void InitReport()
        {
            // Initialize the ReportViewer control
           // this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer1.Dock = DockStyle.Fill; // Ensures it fills the form
            this.reportViewer1.TabIndex = 0;
           // this.Controls.Add(this.reportViewer1);

            // MongoDB connection object
            MongodbConnect mc = new MongodbConnect();
            string _path_1 = mc._birthPrintForm;

            // Set the path of the local report (rdlc file)
            //reportViewer1.LocalReport.ReportPath = _path_1;
            Console.WriteLine("Print Location");
            Console.WriteLine(reportViewer1.LocalReport.ReportPath);
        }

        // Method to initialize report viewer with parameters
        public void InitializeReportViewer(
            string BN,
            string PG,
            string RN,
            string DOR,
            string NC,
            string SEX,
            string DOB,
            string POB,
            string NOM,
            string COM,
            string NOF,
            string COF,
            string DOMOP,
            string PLACEMAR,
            string NCC,
            string URO,
            string VS,
            string AS,
            string VI,
            string DI,
            string OR,
            string DT,
            string CIT,
            string VST,
            string Remarks,
            string _showHeader
        )
        {
            InitReport();
            // Create and assign report parameters
            ReportParameter[] parameters = new ReportParameter[25]
            {
                new ReportParameter("RN", RN),
                new ReportParameter("DOR", DOR),
                new ReportParameter("NC", NC),
                new ReportParameter("SEX", SEX),
                new ReportParameter("DOB", DOB),
                new ReportParameter("POB", POB),
                new ReportParameter("NOM", NOM),
                new ReportParameter("COM", COM),
                new ReportParameter("NOF", NOF),
                new ReportParameter("COF", COF),
                new ReportParameter("DOMOP", DOMOP),
                new ReportParameter("PLACEMAR", PLACEMAR),
                new ReportParameter("NCC", NCC),
                new ReportParameter("URO", URO),
                new ReportParameter("BN", BN),
                new ReportParameter("PG", PG),
                new ReportParameter("VS", VS),
                new ReportParameter("AS", AS),
                new ReportParameter("DI", DI),
                new ReportParameter("OR", OR),
                new ReportParameter("ShowHeader", _showHeader), // Hides the report header
                new ReportParameter("DT", DT),
                new ReportParameter("CIT", CIT),
                new ReportParameter("VST", VST),
                new ReportParameter("Remarks", Remarks)
            };

            // Assign the parameters to the report
            reportViewer1.LocalReport.SetParameters(parameters);

            // Set additional properties for the ReportViewer
            reportViewer1.CurrentPage = 1;
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout); // Use print layout
            reportViewer1.RefreshReport(); // Refresh the report to apply changes
        }

    }
}
