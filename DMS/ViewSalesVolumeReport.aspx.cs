using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMS
{
    public partial class ViewSalesVolumeReport : System.Web.UI.Page
    {
        string id;

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["ID"];
            if (!Page.IsPostBack)
            {
                getDatafromDB();
                Report();
            }
        }

        void getDatafromDB()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            con.Open();
            string Query = "SELECT * FROM sales_volume_tb where id='" + id + "'";

            var original = new CultureInfo("en-us");

            var modified = (CultureInfo)original.Clone();
            modified.NumberFormat.CurrencyNegativePattern = 1;

            SqlCommand cmd = new SqlCommand(Query, con);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tb_SD_AGR.Text = reader["anum_gross_rev"].ToString();
                tb_SD_AOD.Text = reader["anum_op_days"].ToString();
                tb_SD_DOH.Text = reader["daily_op_hrs"].ToString();
                tb_SD_ASr.Text = reader["avg_sale_recpt"].ToString();
                tb_SD_DGR.Text = reader["daily_gross_rev"].ToString();
                tb_SD_HGR.Text = reader["hourly_gross_rev"].ToString();
                tb_SD_HSO.Text = reader["hourly_sale_ord"].ToString();
                tb_SD_DSO.Text = reader["daily_sale_ord"].ToString();
                tb_SD_ASO.Text = reader["anum_sale_ord"].ToString();
                tb_SD_date.Text = reader["sales_date"].ToString();
                ta_SD_Notes.Text = reader["sales_notes"].ToString();
                if (Convert.ToDouble(tb_SD_AGR.Text.Replace(modified.NumberFormat.CurrencySymbol, "")) < 0)
                {
                    tb_SD_AGR.ForeColor = Color.Red;
                }
                else
                {
                    tb_SD_AGR.ForeColor = Color.Black;
                }

                if (Convert.ToDouble(tb_SD_ASr.Text.Replace(modified.NumberFormat.CurrencySymbol, "")) < 0)
                {
                    tb_SD_ASr.ForeColor = Color.Red;
                }
                else
                {
                    tb_SD_ASr.ForeColor = Color.Black;
                }
                if (Convert.ToDouble(tb_SD_DGR.Text.Replace(modified.NumberFormat.CurrencySymbol, "")) < 0)
                {
                    tb_SD_DGR.ForeColor = Color.Red;
                }
                else
                {
                    tb_SD_DGR.ForeColor = Color.Black;
                }
                if (Convert.ToDouble(tb_SD_HGR.Text.Replace(modified.NumberFormat.CurrencySymbol, "")) < 0)
                {
                    tb_SD_HGR.ForeColor = Color.Red;
                }
                else
                {
                    tb_SD_HGR.ForeColor = Color.Black;
                }
                break;
            }

            reader.Close();
            con.Close();
        }

        void Report()
        {
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportViewerSD.LocalReport.ReportPath = "SalesVolumeReport.rdlc";

            reportParameters.Add(new ReportParameter("DateofData", tb_SD_date.Text));
            reportParameters.Add(new ReportParameter("AnnualGrossRevenue", tb_SD_AGR.Text));
            reportParameters.Add(new ReportParameter("AnnualOPDays", tb_SD_AOD.Text));
            reportParameters.Add(new ReportParameter("DailyOpeartingHours", tb_SD_DOH.Text));
            reportParameters.Add(new ReportParameter("AverageSalesRecipt", tb_SD_ASr.Text));
            DateTime today = DateTime.Today;
            reportParameters.Add(new ReportParameter("SavedDate", today.ToString("d")));
            reportParameters.Add(new ReportParameter("DailyGrossRevenue", tb_SD_DGR.Text));
            reportParameters.Add(new ReportParameter("HourlyGrossRevenue", tb_SD_HGR.Text));
            reportParameters.Add(new ReportParameter("HourlySalesOrder", tb_SD_HSO.Text));
            reportParameters.Add(new ReportParameter("DailySalesOrder", tb_SD_DSO.Text));
            reportParameters.Add(new ReportParameter("AnnualSalesOrder", tb_SD_ASO.Text));

            if (ta_SD_Notes.Text == "")
            {
                reportParameters.Add(new ReportParameter("Notes", "-"));
            }
            else
            {
                reportParameters.Add(new ReportParameter("Notes", ta_SD_Notes.Text));
            }

            ReportViewerSD.LocalReport.SetParameters(reportParameters);
        }
    }
}