﻿using Microsoft.Reporting.WebForms;
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
    public partial class SalesVolumeData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void tb_SD_AGR_TextChanged(object sender, EventArgs e)
        {
            var original = new CultureInfo("en-us");

            var modified = (CultureInfo)original.Clone();
            modified.NumberFormat.CurrencyNegativePattern = 1;
            
                if (CheckInt(tb_SD_AGR.Text) == true)
                {
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
                    else
                    {
                    tb_SD_AGR.Text = Convert.ToString(Annual_GR);
                    tb_SD_AGR.Text = string.Format(modified, "{0:c2}", double.Parse(tb_SD_AGR.Text));
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
                if(Convert.ToInt32(tb_SD_DOH.Text) > 24)
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
                else
                {
                    tb_SD_ASr.Text = Convert.ToString(Average_SR);
                    tb_SD_ASr.Text = string.Format(modified, "{0:c2}", double.Parse(tb_SD_ASr.Text));
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
            
            if(Daily_Gross_Revenue < 0)
            {
                tb_SD_DGR.ForeColor = Color.Red;
            }
            else
            {
                tb_SD_DGR.ForeColor= Color.Black;
            }

            if(Daily_Gross_Revenue - Math.Truncate(Daily_Gross_Revenue) == .00)
            {
                tb_SD_DGR.Text = Convert.ToString(Daily_Gross_Revenue);
                tb_SD_DGR.Text = string.Format(modified, "{0:c0}", double.Parse(tb_SD_DGR.Text));
            }
            else
            {
                tb_SD_DGR.Text = Convert.ToString(Daily_Gross_Revenue);
                tb_SD_DGR.Text = string.Format(modified, "{0:c2}", double.Parse(tb_SD_DGR.Text));
            }

            double Houly_Gross_Revenue = Daily_Gross_Revenue / Daily_Op_Hours;

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
            else
            {
                tb_SD_HGR.Text = Convert.ToString(Houly_Gross_Revenue);
                tb_SD_HGR.Text = string.Format(modified, "{0:c2}", double.Parse(tb_SD_HGR.Text));
            }

            double Hourly_sale_order = Houly_Gross_Revenue / Avg_Sale_Recipt;

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
            else
            {
                tb_SD_HSO.Text = Convert.ToString(Hourly_sale_order);
                tb_SD_HSO.Text = string.Format("{0:n2}", double.Parse(tb_SD_HSO.Text));

            }

            double Daily_Sales_Order = Hourly_sale_order * Daily_Op_Hours;

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
            else
            {
                tb_SD_DSO.Text = Convert.ToString(Daily_Sales_Order);
                tb_SD_DSO.Text = string.Format("{0:n2}", double.Parse(tb_SD_DSO.Text));
            }

            double Annual_sales_Order = Daily_Sales_Order * Annual_Op_Days;

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
            else
            {
                tb_SD_ASO.Text = Convert.ToString(Annual_sales_Order);
                tb_SD_ASO.Text = string.Format("{0:n2}", double.Parse(tb_SD_ASO.Text));
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

            if(ta_SD_Notes.Text == "")
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
            string sqlStmt = "INSERT INTO sales_volume_tb (anum_gross_rev,anum_op_days,daily_op_hrs,avg_sale_recpt,daily_gross_rev,hourly_gross_rev,hourly_sale_ord,daily_sale_ord,anum_sale_ord,saved_date,sales_date,sales_notes)VALUES (@anum_gross_rev,@anum_op_days,@daily_op_hrs,@avg_sale_recpt,@daily_gross_rev,@hourly_gross_rev,@hourly_sale_ord,@daily_sale_ord,@anum_sale_ord,@saved_date,@sales_date,@sales_notes)";

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
                     ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('congratulations', 'Sales Volume Data Inserted to DB Succesfully :)', 'success')", true);
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

    }
}