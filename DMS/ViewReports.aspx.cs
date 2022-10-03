using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMS
{
    public partial class ViewReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        void Report()
        {
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportViewerPL.LocalReport.ReportPath = "CustomProfitLoss.rdlc";

            string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlConnection con = new SqlConnection(conString);

            string query = "select * from ProfitLoss where (Date BETWEEN '" + tbStart.Text + "' AND '" + tbEnd.Text + "')";
            SqlDataAdapter sqladpterLabourModel = new SqlDataAdapter(query, con);

            PLData profitloss = new PLData();

            sqladpterLabourModel.Fill(profitloss, profitloss.Tables[0].TableName);
            ReportDataSource datasource = new ReportDataSource("PLData", profitloss.Tables[0]);
            ReportViewerPL.LocalReport.DataSources.Add(datasource);

            reportParameters.Add(new ReportParameter("StartDate", tbStart.Text));
            reportParameters.Add(new ReportParameter("EndDate", tbEnd.Text));

            ReportViewerPL.LocalReport.Refresh();
            ReportViewerPL.LocalReport.SetParameters(reportParameters);

        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            Report();
        }
    }
}