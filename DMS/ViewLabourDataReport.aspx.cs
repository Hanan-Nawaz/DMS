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
    public partial class ViewLabourDataReport : System.Web.UI.Page
    {
        string id;
        protected void Page_Load(object sender, EventArgs e)
        {
            ta_LD_Notes.Enabled = false;

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
            string Query = "SELECT * FROM labor_data_tb where id='" + id + "'";

            var original = new CultureInfo("en-us");

            var modified = (CultureInfo)original.Clone();
            modified.NumberFormat.CurrencyNegativePattern = 1;

            SqlCommand cmd = new SqlCommand(Query, con);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ddl_LD_AGR.SelectedItem.Text = reader["annual_gross_revenue"].ToString();
                tb_LD_AOD.Text = reader["annual_operating_days"].ToString();
                tb_LD_DOH.Text = reader["daily_operating_hrs"].ToString();
                tb_LD_AOH.Text = reader["annual_operating_hrs"].ToString();
                ddl_LD_Model.SelectedItem.Text = reader["model"].ToString();
                tb_LD_Date.Text = reader["labour_data_date"].ToString();
                tb_LDAM_NoL.Text = reader["am_daily_hrs_worked"].ToString();
                tb_LDAM_DOW.Text = reader["am_daily_hrs_worked"].ToString();
                tb_LDAM_ADW.Text = reader["am_annual_days_worked"].ToString();
                tb_LDAM_HW.Text = reader["am_hourly_wages"].ToString();
                if (Convert.ToDouble(tb_LDAM_HW.Text.Replace(modified.NumberFormat.CurrencySymbol, "")) < 0)
                {
                    tb_LDAM_HW.ForeColor = Color.Red;
                }
                else
                {
                    tb_LDAM_HW.ForeColor = Color.Black;
                }
                tb_LDAM_W.Text = reader["am_annual_wages"].ToString();
                if (Convert.ToDouble(tb_LDAM_W.Text.Replace(modified.NumberFormat.CurrencySymbol, "")) < 0)
                {
                    tb_LDAM_W.ForeColor = Color.Red;
                }
                else
                {
                    tb_LDAM_W.ForeColor = Color.Black;
                }
                tb_LDCREW_NoL.Text = reader["crew_no_labor"].ToString();
                tb_LDCREW_DOW.Text = reader["crew_daily_hrs_worked"].ToString();
                tb_LDCREW_ADW.Text = reader["crew_annual_days_worked"].ToString();
                tb_LDCREW_HW.Text = reader["crew_hourly_wages"].ToString();
                if (Convert.ToDouble(tb_LDCREW_HW.Text.Replace(modified.NumberFormat.CurrencySymbol, "")) < 0)
                {
                    tb_LDCREW_HW.ForeColor = Color.Red;
                }
                else
                {
                    tb_LDCREW_HW.ForeColor = Color.Black;
                }
                tb_LDCREW_W.Text = reader["crew_annual_wages"].ToString();
                if (Convert.ToDouble(tb_LDCREW_W.Text.Replace(modified.NumberFormat.CurrencySymbol, "")) < 0)
                {
                    tb_LDCREW_W.ForeColor = Color.Red;
                }
                else
                {
                    tb_LDCREW_W.ForeColor = Color.Black;
                }
                tb_LDGM_NoL.Text = reader["gm_no_labor"].ToString();
                tb_LDGM_DOW.Text = reader["gm_daily_hrs_worked"].ToString();
                tb_LDGM_ADW.Text = reader["gm_annual_days_worked"].ToString();
                tb_LDGM_HW.Text = reader["gm_hourly_wages"].ToString();
                if (Convert.ToDouble(tb_LDGM_HW.Text.Replace(modified.NumberFormat.CurrencySymbol, "")) < 0)
                {
                    tb_LDGM_HW.ForeColor = Color.Red;
                }
                else
                {
                    tb_LDGM_HW.ForeColor = Color.Black;
                }
                tb_LDGM_W.Text = reader["gm_annual_wages"].ToString();
                if (Convert.ToDouble(tb_LDGM_W.Text.Replace(modified.NumberFormat.CurrencySymbol, "")) < 0)
                {
                    tb_LDGM_W.ForeColor = Color.Red;
                }
                else
                {
                    tb_LDGM_W.ForeColor = Color.Black;
                }
                tb_LD_TW.Text = reader["total_annual_wages"].ToString();
                if (Convert.ToDouble(tb_LD_TW.Text.Replace(modified.NumberFormat.CurrencySymbol, "")) < 0)
                {
                    tb_LD_TW.ForeColor = Color.Red;
                }
                else
                {
                    tb_LD_TW.ForeColor = Color.Black;
                }
                ta_LD_Notes.Text = reader["notes"].ToString();
                break;
            }

            reader.Close();
            con.Close();
        }

        void Report()
        {
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportViewerLD.LocalReport.ReportPath = "ReportLD.rdlc";

            reportParameters.Add(new ReportParameter("DateofData", tb_LD_Date.Text));
            reportParameters.Add(new ReportParameter("AnnualGrossRevenue", ddl_LD_AGR.SelectedItem.Text));
            reportParameters.Add(new ReportParameter("AnnualOPDays", tb_LD_AOD.Text));
            reportParameters.Add(new ReportParameter("DailyOpeartingHours", tb_LD_DOH.Text));
            reportParameters.Add(new ReportParameter("AnnualOpeartingHours", tb_LD_AOH.Text));
            reportParameters.Add(new ReportParameter("Model", ddl_LD_Model.SelectedItem.Text));
            DateTime today = DateTime.Today;
            reportParameters.Add(new ReportParameter("TodayDate", today.ToString("d")));

            var original = new CultureInfo("en-us");

            var modified = (CultureInfo)original.Clone();
            modified.NumberFormat.CurrencyNegativePattern = 1;

            if (tb_LDGM_NoL.Text == "" || tb_LDGM_ADW.Text == "" || tb_LDGM_DOW.Text == "" || tb_LDGM_HW.Text == "")
            {
                reportParameters.Add(new ReportParameter("GMNOL", "--"));
                reportParameters.Add(new ReportParameter("GMDHW", "--"));
                reportParameters.Add(new ReportParameter("GMADW", "--"));
                reportParameters.Add(new ReportParameter("GMHW", "--"));
            }
            else
            {
                double GMHW = Convert.ToDouble(tb_LDGM_HW.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                reportParameters.Add(new ReportParameter("GMNOL", tb_LDGM_NoL.Text));
                reportParameters.Add(new ReportParameter("GMDHW", tb_LDGM_DOW.Text));
                reportParameters.Add(new ReportParameter("GMADW", tb_LDGM_ADW.Text));
                if ((GMHW - Math.Truncate(GMHW)) == .00)
                {
                    reportParameters.Add(new ReportParameter("GMHW", GMHW.ToString("C0", modified)));
                }
                else
                {
                    reportParameters.Add(new ReportParameter("GMHW", GMHW.ToString("C2", modified)));
                }
            }

            if (tb_LDAM_NoL.Text == "" || tb_LDAM_ADW.Text == "" || tb_LDAM_DOW.Text == "" || tb_LDAM_HW.Text == "")
            {
                reportParameters.Add(new ReportParameter("AMNOL", "--"));
                reportParameters.Add(new ReportParameter("AMDHW", "--"));
                reportParameters.Add(new ReportParameter("AMADW", "--"));
                reportParameters.Add(new ReportParameter("AMHW", "--"));
            }
            else
            {
                double AMHW = Convert.ToDouble(tb_LDAM_HW.Text.Replace(modified.NumberFormat.CurrencySymbol, "")); ;

                if ((AMHW - Math.Truncate(AMHW)) == .00)
                {
                    reportParameters.Add(new ReportParameter("AMHW", AMHW.ToString("C0", modified)));
                }
                else
                {
                    reportParameters.Add(new ReportParameter("AMHW", AMHW.ToString("C2", modified)));
                }

                reportParameters.Add(new ReportParameter("AMNOL", tb_LDAM_NoL.Text));
                reportParameters.Add(new ReportParameter("AMDHW", tb_LDAM_DOW.Text));
                reportParameters.Add(new ReportParameter("AMADW", tb_LDAM_ADW.Text));
            }

            if (tb_LDCREW_NoL.Text == "" || tb_LDCREW_ADW.Text == "" || tb_LDCREW_DOW.Text == "" || tb_LDCREW_HW.Text == "")
            {
                reportParameters.Add(new ReportParameter("CREWNOL", "--"));
                reportParameters.Add(new ReportParameter("CREWDHW", "--"));
                reportParameters.Add(new ReportParameter("CREWADW", "--"));
                reportParameters.Add(new ReportParameter("CREWHW", "--"));
            }
            else
            {
                double CREWHW = Convert.ToDouble(tb_LDCREW_HW.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                reportParameters.Add(new ReportParameter("CREWNOL", tb_LDCREW_NoL.Text));
                reportParameters.Add(new ReportParameter("CREWDHW", tb_LDCREW_DOW.Text));
                reportParameters.Add(new ReportParameter("CREWADW", tb_LDCREW_ADW.Text));
                if ((CREWHW - Math.Truncate(CREWHW)) == .00)
                {
                    reportParameters.Add(new ReportParameter("CREWHW", CREWHW.ToString("C0", modified)));
                }
                else
                {
                    reportParameters.Add(new ReportParameter("CREWHW", CREWHW.ToString("C2", modified)));
                }
            }

            double AMW = Convert.ToDouble(tb_LDAM_W.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));

            if ((AMW - Math.Truncate(AMW)) == .00)
            {
                reportParameters.Add(new ReportParameter("AMW", AMW.ToString("C0", modified)));
            }
            else
            {
                reportParameters.Add(new ReportParameter("AMW", AMW.ToString("C2", modified)));
            }

            double GMW = Convert.ToDouble(tb_LDGM_W.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));

            if ((GMW - Math.Truncate(GMW)) == .00)
            {
                reportParameters.Add(new ReportParameter("GMW", GMW.ToString("C0", modified)));
            }
            else
            {
                reportParameters.Add(new ReportParameter("GMW", GMW.ToString("C2", modified)));
            }

            double CREWW = Convert.ToDouble(tb_LDCREW_W.Text.Replace(modified.NumberFormat.CurrencySymbol, "")); 

            if ((CREWW - Math.Truncate(CREWW)) == .00)
            {
                reportParameters.Add(new ReportParameter("CREWW", CREWW.ToString("C0", modified)));
            }
            else
            {
                reportParameters.Add(new ReportParameter("CREWW", CREWW.ToString("C2", modified)));
            }

            double TW = Convert.ToDouble(tb_LD_TW.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));

            if ((TW - Math.Truncate(TW)) == .00)
            {
                reportParameters.Add(new ReportParameter("TW", TW.ToString("C0", modified)));
            }
            else
            {
                reportParameters.Add(new ReportParameter("TW", TW.ToString("C2", modified)));
            }

            if (ta_LD_Notes.Text == "")
            {
                reportParameters.Add(new ReportParameter("Notes", "--"));
            }
            else
            {
                reportParameters.Add(new ReportParameter("Notes", ta_LD_Notes.Text));
            }

            ReportViewerLD.LocalReport.SetParameters(reportParameters);
        }

    }
}