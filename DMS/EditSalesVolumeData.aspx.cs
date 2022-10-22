using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMS
{
    public partial class EditSalesVolumeData : System.Web.UI.Page
    {
        string id;

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["ID"];
            if (!Page.IsPostBack)
            {
                getDatafromDB();
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

        protected void tb_SD_AGR_TextChanged(object sender, EventArgs e)
        {
            var original = new CultureInfo("en-us");

            var modified = (CultureInfo)original.Clone();
            modified.NumberFormat.CurrencyNegativePattern = 1;

            if (CheckInt(tb_SD_AGR.Text) == true)
            {
                int AnnualgR;
                double Annual_GR = Convert.ToDouble(tb_SD_AGR.Text);
                if (Annual_GR < 0)
                {
                    tb_SD_AGR.ForeColor = Color.Red;
                }
                else
                {
                    tb_SD_AGR.ForeColor = Color.Black;
                }

                if ((Annual_GR - Math.Truncate(Annual_GR)) == .00)
                {
                    tb_SD_AGR.Text = Convert.ToString(Annual_GR);
                    tb_SD_AGR.Text = string.Format(modified, "{0:c0}", double.Parse(tb_SD_AGR.Text));
                }
                else if ((Annual_GR - Math.Truncate(Annual_GR)) <= .50)
                {
                    AnnualgR = (int)Math.Floor(Annual_GR);
                    tb_SD_AGR.Text = Convert.ToString(AnnualgR);
                    tb_SD_AGR.Text = string.Format(modified, "{0:c0}", double.Parse(tb_SD_AGR.Text));
                }
                else if ((Annual_GR - Math.Truncate(Annual_GR)) >= .50)
                {
                    AnnualgR = (int)Math.Ceiling(Annual_GR);
                    tb_SD_AGR.Text = Convert.ToString(AnnualgR);
                    tb_SD_AGR.Text = string.Format(modified, "{0:c0}", double.Parse(tb_SD_AGR.Text));
                }
            }
            else
            {
                string head = "Error";
                string headtext = "Please enter value of Annual Gross Revenue in Integer/Double";
                string headtype = "warning";
                string cancelmsg = "Please enter all values in Integer";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);

                tb_SD_AGR.Text = "";
            }
        }
        public static bool CheckInt(string input)
        {
            double number = 0.00;
            return double.TryParse(input, out number);
        }

        protected void tb_SD_AOD_TextChanged(object sender, EventArgs e)
        {
            if (CheckInt(tb_SD_AOD.Text) == false)
            {
                string head = "Error";
                string headtext = "Please enter value of Annual Opearting Days in Integer/Double";
                string headtype = "warning";
                string cancelmsg = "Please enter all values in Integer";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
            }
            else
            {
                if (Convert.ToInt32(tb_SD_AOD.Text) > 365)
                {
                    string head = "Error";
                    string headtext = "Annual Opearting Days must be smaller than 365.";
                    string headtype = "warning";
                    string cancelmsg = "";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    tb_SD_AOD.Text = "";
                }
            }
        }

        protected void tb_SD_DOH_TextChanged(object sender, EventArgs e)
        {
            if (CheckInt(tb_SD_DOH.Text) == false)
            {
                string head = "Error";
                string headtext = "Please enter value of Daily Opearting Hours in Integer/Double";
                string headtype = "warning";
                string cancelmsg = "Please enter all values in Integer";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
            }
            else
            {
                if (Convert.ToInt32(tb_SD_DOH.Text) > 24)
                {
                    string head = "Error";
                    string headtext = "Daily Opearting Hours must be smaller than 24.";
                    string headtype = "warning";
                    string cancelmsg = "";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    tb_SD_DOH.Text = "";
                }
            }
        }

        protected void tb_SD_ASr_TextChanged(object sender, EventArgs e)
        {
            var original = new CultureInfo("en-us");

            var modified = (CultureInfo)original.Clone();
            modified.NumberFormat.CurrencyNegativePattern = 1;

            if (CheckInt(tb_SD_ASr.Text) == true)
            {
                double Average_SR = Convert.ToDouble(tb_SD_ASr.Text);
                int AveragesR;
                if (Average_SR < 0)
                {
                    tb_SD_ASr.ForeColor = Color.Red;
                }
                else
                {
                    tb_SD_ASr.ForeColor = Color.Black;
                }

                if ((Average_SR - Math.Truncate(Average_SR)) == .00)
                {
                    tb_SD_ASr.Text = Convert.ToString(Average_SR);
                    tb_SD_ASr.Text = string.Format(modified, "{0:c0}", double.Parse(tb_SD_ASr.Text));
                }
                else if ((Average_SR - Math.Truncate(Average_SR)) <= .50)
                {
                    AveragesR = (int)Math.Floor(Average_SR);
                    tb_SD_ASr.Text = Convert.ToString(AveragesR);
                    tb_SD_ASr.Text = string.Format(modified, "{0:c0}", double.Parse(tb_SD_ASr.Text));
                }
                else if ((Average_SR - Math.Truncate(Average_SR)) >= .50)
                {
                    AveragesR = (int)Math.Ceiling(Average_SR);
                    tb_SD_ASr.Text = Convert.ToString(AveragesR);
                    tb_SD_ASr.Text = string.Format(modified, "{0:c0}", double.Parse(tb_SD_ASr.Text));
                }
            }
            else
            {
                string head = "Error";
                string headtext = "Please enter value of Average Sales Recipt in Integer/Double";
                string headtype = "warning";
                string cancelmsg = "Please enter all values in Integer";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                tb_SD_ASr.Text = "";
            }
        }

        protected void btn_SD_Calculate_Click(object sender, EventArgs e)
        {
            calculate();
            Report();
        }

        protected void btn_Save_toDB_Click(object sender, EventArgs e)
        {
            calculate();
            SaveToDB();
        }

        void calculate()
        {
            var original = new CultureInfo("en-us");

            var modified = (CultureInfo)original.Clone();
            modified.NumberFormat.CurrencyNegativePattern = 1;

            double Annual_Gross_Revenue = Convert.ToDouble(tb_SD_AGR.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
            double Annual_Op_Days = Convert.ToDouble(tb_SD_AOD.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
            double Daily_Op_Hours = Convert.ToDouble(tb_SD_DOH.Text);
            double Avg_Sale_Recipt = Convert.ToDouble(tb_SD_ASr.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));

            double Daily_Gross_Revenue = Annual_Gross_Revenue / Annual_Op_Days;
            int DailyGrossrevenue;

            if (Daily_Gross_Revenue < 0)
            {
                tb_SD_DGR.ForeColor = Color.Red;
            }
            else
            {
                tb_SD_DGR.ForeColor = Color.Black;
            }

            if (Daily_Gross_Revenue - Math.Truncate(Daily_Gross_Revenue) == .00)
            {
                tb_SD_DGR.Text = Convert.ToString(Daily_Gross_Revenue);
                tb_SD_DGR.Text = string.Format(modified, "{0:c0}", double.Parse(tb_SD_DGR.Text));
            }
            else if (Daily_Gross_Revenue - Math.Truncate(Daily_Gross_Revenue) <= .50)
            {
                DailyGrossrevenue = (int)Math.Floor(Daily_Gross_Revenue);
                tb_SD_DGR.Text = Convert.ToString(DailyGrossrevenue);
                tb_SD_DGR.Text = string.Format(modified, "{0:c0}", double.Parse(tb_SD_DGR.Text));
            }
            else if (Daily_Gross_Revenue - Math.Truncate(Daily_Gross_Revenue) >= .50)
            {
                DailyGrossrevenue = (int)Math.Ceiling(Daily_Gross_Revenue);
                tb_SD_DGR.Text = Convert.ToString(DailyGrossrevenue);
                tb_SD_DGR.Text = string.Format(modified, "{0:c0}", double.Parse(tb_SD_DGR.Text));
            }

            double DailyGrossRevenue = Convert.ToDouble(tb_SD_DGR.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));

            double Houly_Gross_Revenue = DailyGrossRevenue / Daily_Op_Hours;
            int Houly_Grossrevenue;

            if (Houly_Gross_Revenue < 0)
            {
                tb_SD_HGR.ForeColor = Color.Red;
            }
            else
            {
                tb_SD_HGR.ForeColor = Color.Black;
            }

            if (Houly_Gross_Revenue - Math.Truncate(Houly_Gross_Revenue) == .00)
            {
                tb_SD_HGR.Text = Convert.ToString(Houly_Gross_Revenue);
                tb_SD_HGR.Text = string.Format(modified, "{0:c0}", double.Parse(tb_SD_HGR.Text));
            }
            else if (Houly_Gross_Revenue - Math.Truncate(Houly_Gross_Revenue) <= .50)
            {
                Houly_Grossrevenue = (int)Math.Floor(Houly_Gross_Revenue);
                tb_SD_HGR.Text = Convert.ToString(Houly_Grossrevenue);
                tb_SD_HGR.Text = string.Format(modified, "{0:c0}", double.Parse(tb_SD_HGR.Text));
            }
            else if (Houly_Gross_Revenue - Math.Truncate(Houly_Gross_Revenue) >= .50)
            {
                Houly_Grossrevenue = (int)Math.Ceiling(Houly_Gross_Revenue);
                tb_SD_HGR.Text = Convert.ToString(Houly_Grossrevenue + 1);
                tb_SD_HGR.Text = string.Format(modified, "{0:c0}", double.Parse(tb_SD_HGR.Text));
            }

            double HoulyGrossRevenue = Convert.ToDouble(tb_SD_HGR.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
            double Hourly_sale_order = HoulyGrossRevenue / Avg_Sale_Recipt;
            int Hourly_saleOrder;

            if (Hourly_sale_order < 0)
            {
                tb_SD_HSO.ForeColor = Color.Red;
            }
            else
            {
                tb_SD_HSO.ForeColor = Color.Black;
            }

            if (Hourly_sale_order - Math.Truncate(Hourly_sale_order) == .00)
            {
                tb_SD_HSO.Text = Convert.ToString(Hourly_sale_order);
                tb_SD_HSO.Text = string.Format("{0:n0}", double.Parse(tb_SD_HSO.Text));
            }
            else if (Hourly_sale_order - Math.Truncate(Hourly_sale_order) <= .50)
            {
                Hourly_saleOrder = (int)Math.Floor(Hourly_sale_order);
                tb_SD_HSO.Text = Convert.ToString(Hourly_saleOrder);
                tb_SD_HSO.Text = string.Format("{0:n0}", double.Parse(tb_SD_HSO.Text));
            }
            else if (Hourly_sale_order - Math.Truncate(Hourly_sale_order) >= .50)
            {
                Hourly_saleOrder = (int)Math.Ceiling(Hourly_sale_order);
                tb_SD_HSO.Text = Convert.ToString(Hourly_saleOrder);
                tb_SD_HSO.Text = string.Format("{0:n0}", double.Parse(tb_SD_HSO.Text));
            }

            double Hourlysaleorder = Convert.ToDouble(tb_SD_HSO.Text);
            double Daily_Sales_Order = Hourlysaleorder * Daily_Op_Hours;
            int DailySalesorder;

            if (Daily_Sales_Order < 0)
            {
                tb_SD_DSO.ForeColor = Color.Red;
            }
            else
            {
                tb_SD_DSO.ForeColor = Color.Black;
            }

            if (Daily_Sales_Order - Math.Truncate(Daily_Sales_Order) == .00)
            {
                tb_SD_DSO.Text = Convert.ToString(Daily_Sales_Order);
                tb_SD_DSO.Text = string.Format("{0:n0}", double.Parse(tb_SD_DSO.Text));
            }
            else if (Daily_Sales_Order - Math.Truncate(Daily_Sales_Order) <= .50)
            {
                DailySalesorder = (int)Math.Floor(Daily_Sales_Order);
                tb_SD_DSO.Text = Convert.ToString(DailySalesorder);
                tb_SD_DSO.Text = string.Format("{0:n0}", double.Parse(tb_SD_DSO.Text));
            }
            else if (Daily_Sales_Order - Math.Truncate(Daily_Sales_Order) <= .50)
            {
                DailySalesorder = (int)Math.Ceiling(Daily_Sales_Order);
                tb_SD_DSO.Text = Convert.ToString(DailySalesorder);
                tb_SD_DSO.Text = string.Format("{0:n0}", double.Parse(tb_SD_DSO.Text));
            }

            double DailySalesOrder = Convert.ToDouble(tb_SD_DSO.Text);
            double Annual_sales_Order = DailySalesOrder * Annual_Op_Days;
            int Annual_salesorder;

            if (Annual_sales_Order < 0)
            {
                tb_SD_ASO.ForeColor = Color.Red;
            }
            else
            {
                tb_SD_ASO.ForeColor = Color.Black;
            }

            if (Annual_sales_Order - Math.Truncate(Annual_sales_Order) == .00)
            {
                tb_SD_ASO.Text = Convert.ToString(Annual_sales_Order);
                tb_SD_ASO.Text = string.Format("{0:n0}", double.Parse(tb_SD_ASO.Text));

            }
            else if (Annual_sales_Order - Math.Truncate(Annual_sales_Order) <= .50)
            {
                Annual_salesorder = (int)Math.Floor(Annual_sales_Order);
                tb_SD_ASO.Text = Convert.ToString(Annual_salesorder);
                tb_SD_ASO.Text = string.Format("{0:n0}", double.Parse(tb_SD_ASO.Text));

            }
            else if (Annual_sales_Order - Math.Truncate(Annual_sales_Order) >= .50)
            {
                Annual_salesorder = (int)Math.Ceiling(Annual_sales_Order);
                tb_SD_ASO.Text = Convert.ToString(Annual_salesorder);
                tb_SD_ASO.Text = string.Format("{0:n0}", double.Parse(tb_SD_ASO.Text));

            }
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

        void SaveToDB()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            cn.Open();

            DateTime date = DateTime.Today;
            string sqlStmt = "UPDATE sales_volume_tb SET anum_gross_rev = @anum_gross_rev,anum_op_days = @anum_op_days,daily_op_hrs = @daily_op_hrs,avg_sale_recpt = @avg_sale_recpt,daily_gross_rev = @daily_gross_rev,hourly_gross_rev = @hourly_gross_rev,hourly_sale_ord = @hourly_sale_ord,daily_sale_ord = @daily_sale_ord,anum_sale_ord = @anum_sale_ord,saved_date = @saved_date,sales_date = @sales_date ,sales_notes = @sales_notes WHERE id = '" + id + "'";

            SqlCommand cmd = new SqlCommand(sqlStmt, cn);

            try
            {

                cmd.Parameters.Add(new SqlParameter("@anum_gross_rev", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@anum_op_days", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@daily_op_hrs", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@avg_sale_recpt", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@daily_gross_rev", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@hourly_gross_rev", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@hourly_sale_ord", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@daily_sale_ord", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@anum_sale_ord", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@saved_date", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@sales_date", SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@sales_notes", SqlDbType.Text));

                cmd.Parameters["@anum_gross_rev"].Value = tb_SD_AGR.Text;
                cmd.Parameters["@anum_op_days"].Value = tb_SD_AOD.Text;
                cmd.Parameters["@daily_op_hrs"].Value = tb_SD_DOH.Text;
                cmd.Parameters["@avg_sale_recpt"].Value = tb_SD_ASr.Text;
                cmd.Parameters["@daily_gross_rev"].Value = tb_SD_DGR.Text;
                cmd.Parameters["@hourly_gross_rev"].Value = tb_SD_HGR.Text;
                cmd.Parameters["@hourly_sale_ord"].Value = tb_SD_HSO.Text;
                cmd.Parameters["@daily_sale_ord"].Value = tb_SD_DSO.Text;
                cmd.Parameters["@anum_sale_ord"].Value = tb_SD_ASO.Text;
                cmd.Parameters["@sales_date"].Value = tb_SD_date.Text;
                cmd.Parameters["@saved_date"].Value = date.ToString("d");


                if (ta_SD_Notes.Text == "")
                {
                    cmd.Parameters["@sales_notes"].Value = "-";
                }
                else
                {
                    cmd.Parameters["@sales_notes"].Value = ta_SD_Notes.Text;
                }

                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('congratulations', 'Sales Volume Data Updated Succesfully :)', 'success')", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Error', 'Unknown Error! Try Again :)', 'error')", true);
                }

            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Error', " + ex.Message + ", 'error')", true);
            }
            finally
            {
                cn.Close();
            }
        }

        protected void Resetbn_Click(object sender, EventArgs e)
        {
            Response.Redirect("SalesVolumeData.aspx");
        }
    }
}