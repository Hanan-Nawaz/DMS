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
    public partial class EditLabourData : System.Web.UI.Page
    {
        double DaysOpHours, annualOpDays, GMNDL, GMDHW, GMADW, GMHW, AMNDL, AMDHW, AMADW, AMHW, CREWNDL, CREWDHW, CREWADW, CREWHW;
        double totalAM, totalGM, totalCREW, total;
        string id;

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["ID"];
            if (!Page.IsPostBack)
            {
                Reload();
                getDatafromDB();
            }
        }

        void getDatafromDB()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            con.Open();
            string Query = "SELECT * FROM labor_data_tb where id='" + id +"'";

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
                tb_saveDate.Text = reader["saved_date"].ToString();
                ta_LD_Notes.Text = reader["notes"].ToString();
                break;
            }

            reader.Close();
            con.Close();
        }

        void Reload()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            con.Open();

            ddl_LD_AGR.AppendDataBoundItems = true;

            string annualOpDaysQuery = "SELECT * from annual_gross_revenue_dropdown";
            SqlDataAdapter sqladpter = new SqlDataAdapter(annualOpDaysQuery, con);
            DataTable sqldatabase = new DataTable();
            sqladpter.Fill(sqldatabase);
            ddl_LD_AGR.DataSource = sqldatabase;
            ddl_LD_AGR.DataTextField = "annual_gross_revenue";
            ddl_LD_AGR.DataValueField = "id";
            ddl_LD_AGR.DataBind();

            ddl_LD_Model.AppendDataBoundItems = true;

            string ModelQuery = "SELECT * from model_dropdown";
            SqlDataAdapter sqladpterModel = new SqlDataAdapter(ModelQuery, con);
            DataTable sqldatabaseModel = new DataTable();
            sqladpterModel.Fill(sqldatabaseModel);
            ddl_LD_Model.DataSource = sqldatabaseModel;
            ddl_LD_Model.DataTextField = "model";
            ddl_LD_Model.DataValueField = "id";
            ddl_LD_Model.DataBind();
        }

        protected void tb_LD_DOH_TextChanged(object sender, EventArgs e)
        {
            if (tb_LD_AOD.Text == "")
            {
                string head = "Error";
                string headtext = "PLease fill Annual Operating Days Field";
                string headtype = "warning";
                string cancelmsg = "Please enter the Value";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);

                tb_LD_AOD.Text = "";
                tb_LD_DOH.Text = "";
            }
            else if (tb_LD_DOH.Text == "")
            {
                string head = "Error";
                string headtext = "PLease fill Daily Operating Hours Field";
                string headtype = "warning";
                string cancelmsg = "Please enter the Value";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);

                tb_LD_AOD.Text = "";
                tb_LD_DOH.Text = "";
            }
            else
            {
                annualOpDays = Convert.ToInt32(tb_LD_AOD.Text);
                DaysOpHours = Convert.ToInt32(tb_LD_DOH.Text);
                if (annualOpDays > 365 || DaysOpHours > 24)
                {
                    string head = "Error";
                    string headtext = "Days must be smaller than 365 and Hours must be Smaller than 24";
                    string headtype = "warning";
                    string cancelmsg = "Please Re-enter the Values";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);

                    tb_LD_AOD.Text = "";
                    tb_LD_DOH.Text = "";
                }
                else
                {

                    double AnnualOPHours = annualOpDays * DaysOpHours;

                    string Hours = AnnualOPHours.ToString() + " hrs";

                    tb_LD_AOH.Text = Hours;
                }
            }
        }

        protected void tb_LDCREW_HW_TextChanged(object sender, EventArgs e)
        {
            var original = new CultureInfo("en-us");

            var modified = (CultureInfo)original.Clone();
            modified.NumberFormat.CurrencyNegativePattern = 1;

            if (tb_LDCREW_NoL.Text == "" || tb_LDCREW_HW.Text == "" || tb_LDCREW_DOW.Text == "" || tb_LDCREW_ADW.Text == "")
            {
                tb_LDCREW_HW.Text = "";
            }
            else
            {
                if (CheckInt(tb_LDCREW_HW.Text) == true)
                {
                    double CREWHW = Convert.ToDouble(tb_LDCREW_HW.Text);

                    if (CREWHW < 0)
                    {
                        tb_LDCREW_HW.ForeColor = Color.Red;
                    }
                    else
                    {
                        tb_LDCREW_HW.ForeColor = Color.Black;
                    }

                    if ((CREWHW - Math.Truncate(CREWHW)) == .00)
                    {
                        tb_LDCREW_HW.Text = Convert.ToString(CREWHW);
                        tb_LDCREW_HW.Text = string.Format(modified, "{0:c0}", double.Parse(tb_LDCREW_HW.Text));
                    }
                    else
                    {
                        tb_LDCREW_HW.Text = Convert.ToString(CREWHW);
                        tb_LDCREW_HW.Text = string.Format(modified, "{0:c2}", double.Parse(tb_LDCREW_HW.Text));
                    }
                }
                else
                {
                    string head = "Error";
                    string headtext = "Please enter all values in Integer/Double (CREW)";
                    string headtype = "warning";
                    string cancelmsg = "Please enter all values in Integer";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                }

            }
        }

        protected void tb_LDAM_HW_TextChanged(object sender, EventArgs e)
        {
            var original = new CultureInfo("en-us");

            var modified = (CultureInfo)original.Clone();
            modified.NumberFormat.CurrencyNegativePattern = 1;

            if (tb_LDAM_NoL.Text == "" || tb_LDAM_HW.Text == "" || tb_LDAM_DOW.Text == "" || tb_LDAM_ADW.Text == "")
            {
                tb_LDAM_HW.Text = "";
            }
            else
            {
                if (CheckInt(tb_LDAM_HW.Text) == true)
                {
                    double AMHW = Convert.ToDouble(tb_LDAM_HW.Text);

                    if (AMHW < 0)
                    {
                        tb_LDAM_HW.ForeColor = Color.Red;
                    }
                    else
                    {
                        tb_LDAM_HW.ForeColor = Color.Black;
                    }

                    if ((AMHW - Math.Truncate(AMHW)) == .00)
                    {
                        tb_LDAM_HW.Text = Convert.ToString(AMHW);
                        tb_LDAM_HW.Text = string.Format(modified, "{0:c0}", double.Parse(tb_LDAM_HW.Text));
                    }
                    else
                    {
                        tb_LDAM_HW.Text = Convert.ToString(AMHW);
                        tb_LDAM_HW.Text = string.Format(modified, "{0:c2}", double.Parse(tb_LDAM_HW.Text));
                    }
                }
                else
                {
                    string head = "Error";
                    string headtext = "Please enter all values in Integer/Double (AM)";
                    string headtype = "warning";
                    string cancelmsg = "Please enter all values in Integer";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                }

            }
        }

        protected void tb_LDGM_HW_TextChanged(object sender, EventArgs e)
        {
            var original = new CultureInfo("en-us");

            var modified = (CultureInfo)original.Clone();
            modified.NumberFormat.CurrencyNegativePattern = 1;

            if (tb_LDGM_NoL.Text == "" || tb_LDGM_HW.Text == "" || tb_LDGM_DOW.Text == "" || tb_LDGM_ADW.Text == "")
            {
                tb_LDGM_HW.Text = "";
            }
            else
            {
                if (CheckInt(tb_LDGM_HW.Text) == true)
                {
                    double GMHW = Convert.ToDouble(tb_LDGM_HW.Text);

                    if (GMHW < 0)
                    {
                        tb_LDGM_HW.ForeColor = Color.Red;
                    }
                    else
                    {
                        tb_LDGM_HW.ForeColor = Color.Black;
                    }

                    if ((GMHW - Math.Truncate(GMHW)) == .00)
                    {
                        tb_LDGM_HW.Text = Convert.ToString(GMHW);
                        tb_LDGM_HW.Text = string.Format(modified, "{0:c0}", double.Parse(tb_LDGM_HW.Text));
                    }
                    else
                    {
                        tb_LDGM_HW.Text = Convert.ToString(GMHW);
                        tb_LDGM_HW.Text = string.Format(modified, "{0:c2}", double.Parse(tb_LDGM_HW.Text));
                    }
                }
                else
                {
                    string head = "Error";
                    string headtext = "Please enter all values in Integer/Double (GM)";
                    string headtype = "warning";
                    string cancelmsg = "Please enter all values in Integer";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                }

            }
        }

        protected void btn_add_AGR_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            con.Open();
            string sqlStmt = "insert into annual_gross_revenue_dropdown (annual_gross_revenue) Values(@annual_gross_revenue) ";
            SqlCommand cmd;
            cmd = new SqlCommand(sqlStmt, con);


            cmd.Parameters.Add(new SqlParameter("@annual_gross_revenue", SqlDbType.VarChar, 50));

            cmd.Parameters["@annual_gross_revenue"].Value = tb_AGR.Text;

            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {
                string head = "Congratulations";
                string headtext = "Annual Gross Revenue Added Successfully.";
                string headtype = "success";
                string cancelmsg = "Congrats";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);

                tb_AGR.Text = "";

            }
            else
            {
                string head = "Error";
                string headtext = "Unknown Error! Try Again.";
                string headtype = "warning";
                string cancelmsg = "Please Re-enter the Value";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);

            }
        }

        protected void btn_add_model_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            con.Open();
            string sqlStmt = "insert into model_dropdown (model) Values(@model) ";
            SqlCommand cmd;
            cmd = new SqlCommand(sqlStmt, con);


            cmd.Parameters.Add(new SqlParameter("@model", SqlDbType.VarChar, 50));

            cmd.Parameters["@model"].Value = tb_model.Text;

            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {
                string head = "Congratulations";
                string headtext = "Model Added Successfully.";
                string headtype = "success";
                string cancelmsg = "Congrats";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);

                tb_model.Text = "";
            }
            else
            {
                string head = "Error";
                string headtext = "Unknown Error! Try Again.";
                string headtype = "warning";
                string cancelmsg = "Please Re-enter the Value";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);

            }
        }

        protected void btn_LD_Calculate_Click(object sender, EventArgs e)
        {
            check();
            Report();

        }

        protected void btn_Save_toDB_Click(object sender, EventArgs e)
        {
            check();
            SavetoDB();
        }

        void check()
        {

            if (ddl_LD_Model.SelectedItem.Text == "Select Model" || ddl_LD_AGR.SelectedItem.Text == "Select Annual Gross Renvenue" || tb_LD_Date.Text == "" || tb_LD_AOD.Text == "" || tb_LD_DOH.Text == "")
            {
                string head = "Error";
                string headtext = "Please Fill all Manadatory Fields";
                string headtype = "warning";
                string cancelmsg = "Please Enter all the Values";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
            }

            if (tb_LDAM_ADW.Text != "" && tb_LDAM_DOW.Text == "" && tb_LDAM_HW.Text == "" && tb_LDAM_NoL.Text == "")
            {
                string head = "Error";
                string headtext = "You Enter one or more fields of AM Labour, Either clear filled Fields or enter all fields.";
                string headtype = "warning";
                string cancelmsg = "Please Enter all the Values";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
            }

            if (tb_LDAM_ADW.Text == "" && tb_LDAM_DOW.Text != "" && tb_LDAM_HW.Text == "" && tb_LDAM_NoL.Text == "")
            {
                string head = "Error";
                string headtext = "You Enter one or more fields of AM Labour, Either clear filled Fields or enter all fields.";
                string headtype = "warning";
                string cancelmsg = "Please Enter all the Values";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
            }

            if (tb_LDAM_ADW.Text == "" && tb_LDAM_DOW.Text == "" && tb_LDAM_HW.Text != "" && tb_LDAM_NoL.Text == "")
            {
                string head = "Error";
                string headtext = "You Enter one or more fields of AM Labour, Either clear filled Fields or enter all fields.";
                string headtype = "warning";
                string cancelmsg = "Please Enter all the Values";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
            }

            if (tb_LDAM_ADW.Text == "" && tb_LDAM_DOW.Text == "" && tb_LDAM_HW.Text == "" && tb_LDAM_NoL.Text != "")
            {
                string head = "Error";
                string headtext = "You Enter one or more fields of AM Labour, Either clear filled Fields or enter all fields.";
                string headtype = "warning";
                string cancelmsg = "Please Enter all the Values";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
            }

            if (tb_LDGM_ADW.Text != "" && tb_LDGM_DOW.Text == "" && tb_LDGM_HW.Text == "" && tb_LDGM_NoL.Text == "")
            {
                string head = "Error";
                string headtext = "You Enter one or more fields of GM Labour, Either clear filled Fields or enter all fields.";
                string headtype = "warning";
                string cancelmsg = "Please Enter all the Values";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
            }

            if (tb_LDGM_ADW.Text == "" && tb_LDGM_DOW.Text != "" && tb_LDGM_HW.Text == "" && tb_LDGM_NoL.Text == "")
            {
                string head = "Error";
                string headtext = "You Enter one or more fields of GM Labour, Either clear filled Fields or enter all fields.";
                string headtype = "warning";
                string cancelmsg = "Please Enter all the Values";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
            }

            if (tb_LDGM_ADW.Text == "" && tb_LDGM_DOW.Text == "" && tb_LDGM_HW.Text != "" && tb_LDGM_NoL.Text == "")
            {
                string head = "Error";
                string headtext = "You Enter one or more fields of GM Labour, Either clear filled Fields or enter all fields.";
                string headtype = "warning";
                string cancelmsg = "Please Enter all the Values";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
            }

            if (tb_LDGM_ADW.Text == "" && tb_LDGM_DOW.Text == "" && tb_LDGM_HW.Text == "" && tb_LDGM_NoL.Text != "")
            {
                string head = "Error";
                string headtext = "You Enter one or more fields of GM Labour, Either clear filled Fields or enter all fields.";
                string headtype = "warning";
                string cancelmsg = "Please Enter all the Values";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);

            }

            if (tb_LDCREW_ADW.Text != "" && tb_LDCREW_DOW.Text == "" && tb_LDCREW_HW.Text == "" && tb_LDCREW_NoL.Text == "")
            {
                string head = "Error";
                string headtext = "You Enter one or more fields of CREW Labour, Either clear filled Fields or enter all fields.";
                string headtype = "warning";
                string cancelmsg = "Please Enter all the Values";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
            }

            if (tb_LDCREW_ADW.Text == "" && tb_LDCREW_DOW.Text != "" && tb_LDCREW_HW.Text == "" && tb_LDCREW_NoL.Text == "")
            {
                string head = "Error";
                string headtext = "You Enter one or more fields of CREW Labour, Either clear filled Fields or enter all fields.";
                string headtype = "warning";
                string cancelmsg = "Please Enter all the Values";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
            }

            if (tb_LDCREW_ADW.Text == "" && tb_LDCREW_DOW.Text == "" && tb_LDCREW_HW.Text != "" && tb_LDCREW_NoL.Text == "")
            {
                string head = "Error";
                string headtext = "You Enter one or more fields of CREW Labour, Either clear filled Fields or enter all fields.";
                string headtype = "warning";
                string cancelmsg = "Please Enter all the Values";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
            }

            if (tb_LDCREW_ADW.Text == "" && tb_LDCREW_DOW.Text == "" && tb_LDCREW_HW.Text == "" && tb_LDCREW_NoL.Text != "")
            {
                string head = "Error";
                string headtext = "You Enter one or more fields of CREW Labour, Either clear filled Fields or enter all fields.";
                string headtype = "warning";
                string cancelmsg = "Please Enter all the Values";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);

            }

            if (tb_LDCREW_ADW.Text == "" && tb_LDCREW_DOW.Text == "" && tb_LDCREW_HW.Text == ""
                && tb_LDCREW_NoL.Text == "" && tb_LDAM_ADW.Text == "" && tb_LDAM_DOW.Text == ""
                && tb_LDAM_HW.Text == "" && tb_LDAM_NoL.Text == "" && tb_LDGM_ADW.Text == "" &&
                tb_LDGM_DOW.Text == "" && tb_LDGM_HW.Text == "" && tb_LDGM_NoL.Text == "")
            {
                string head = "Error";
                string headtext = "PLease atleast fill one, GM or AM or CREW.";
                string headtype = "warning";
                string cancelmsg = "Please Enter atleast one.";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
            }



            Calculation();

        }

        void Calculation()
        {
            var original = new CultureInfo("en-us");

            var modified = (CultureInfo)original.Clone();
            modified.NumberFormat.CurrencyNegativePattern = 1;

            if (tb_LD_AOD.Text == "" || tb_LD_DOH.Text == "")
            {
                annualOpDays = 0;
                DaysOpHours = 0;
            }
            else
            {
                annualOpDays = Convert.ToDouble(tb_LD_AOD.Text);
                DaysOpHours = Convert.ToDouble(tb_LD_DOH.Text);
            }

            //GM

            if (tb_LDGM_ADW.Text == "" || tb_LDGM_DOW.Text == "" || tb_LDGM_HW.Text == "" || tb_LDGM_NoL.Text == "" || tb_LDGM_ADW.Text == "-" || tb_LDGM_DOW.Text == "-" || tb_LDGM_HW.Text == "$0" || tb_LDGM_NoL.Text == "-")
            {
                GMNDL = 0;
                GMDHW = 0;
                GMADW = 0;
                GMHW = 0;
            }
            else
            {
                string GM_HW = Convert.ToString(tb_LDGM_HW.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                if (CheckInt(tb_LDGM_NoL.Text) == true && CheckInt(tb_LDGM_DOW.Text) == true
                    && CheckInt(tb_LDGM_ADW.Text) == true && CheckInt(GM_HW) == true)
                {
                    GMNDL = Convert.ToDouble(tb_LDGM_NoL.Text);
                    GMDHW = Convert.ToDouble(tb_LDGM_DOW.Text);
                    GMADW = Convert.ToDouble(tb_LDGM_ADW.Text);
                    GMHW = Convert.ToDouble(GM_HW);
                }
                else
                {
                    string head = "Error";
                    string headtext = "Please enter all values in Integer (GM)";
                    string headtype = "warning";
                    string cancelmsg = "Please enter all values in Integer";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                }
            }

            //AM

            if (tb_LDAM_ADW.Text == "" || tb_LDAM_DOW.Text == "" || tb_LDAM_HW.Text == "" || tb_LDAM_NoL.Text == "" || tb_LDAM_ADW.Text == "-" || tb_LDAM_DOW.Text == "-" || tb_LDAM_HW.Text == "$0" || tb_LDAM_NoL.Text == "-")
            {
                AMNDL = 0;
                AMDHW = 0;
                AMADW = 0;
                AMHW = 0;
            }
            else
            {
                string AM_HW = Convert.ToString(tb_LDAM_HW.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                if (CheckInt(tb_LDAM_NoL.Text) == true && CheckInt(tb_LDAM_DOW.Text) == true
                    && CheckInt(tb_LDAM_ADW.Text) == true && CheckInt(AM_HW) == true)
                {
                    AMNDL = Convert.ToDouble(tb_LDAM_NoL.Text);
                    AMDHW = Convert.ToDouble(tb_LDAM_DOW.Text);
                    AMADW = Convert.ToDouble(tb_LDAM_ADW.Text);
                    AMHW = Convert.ToDouble(AM_HW);
                }
                else
                {
                    string head = "Error";
                    string headtext = "Please enter all values in Integer (AM)";
                    string headtype = "warning";
                    string cancelmsg = "Please enter all values in Integer";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                }


            }

            //CREW

            if (tb_LDCREW_ADW.Text == "" || tb_LDCREW_DOW.Text == "" || tb_LDCREW_HW.Text == "" || tb_LDCREW_NoL.Text == "" || tb_LDCREW_ADW.Text == "-" || tb_LDCREW_DOW.Text == "-" || tb_LDCREW_HW.Text == "$0" || tb_LDCREW_NoL.Text == "-")
            {
                CREWNDL = 0;
                CREWDHW = 0;
                CREWADW = 0;
                CREWHW = 0;
            }
            else
            {
                string CREW_HW = Convert.ToString(tb_LDCREW_HW.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                if (CheckInt(tb_LDCREW_NoL.Text) == true && CheckInt(tb_LDCREW_DOW.Text) == true
                    && CheckInt(tb_LDCREW_ADW.Text) == true && CheckInt(CREW_HW) == true)
                {
                    CREWNDL = Convert.ToDouble(tb_LDCREW_NoL.Text);
                    CREWDHW = Convert.ToDouble(tb_LDCREW_DOW.Text);
                    CREWADW = Convert.ToDouble(tb_LDCREW_ADW.Text);
                    CREWHW = Convert.ToDouble(CREW_HW);
                }
                else
                {
                    string head = "Error";
                    string headtext = "Please enter all values in Integer (CREW)";
                    string headtype = "warning";
                    string cancelmsg = "Please enter all values in Integer";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                }
            }

            totalAM = AMNDL * AMADW * AMDHW * AMHW;
            totalGM = GMNDL * GMADW * GMDHW * GMHW;
            totalCREW = CREWNDL * CREWADW * CREWDHW * CREWHW;
            total = totalAM + totalGM + totalCREW;

            if (totalGM < 0)
            {
                tb_LDGM_W.ForeColor = Color.Red;
            }
            else
            {
                tb_LDGM_W.ForeColor = Color.Black;
            }

            if (totalAM < 0)
            {
                tb_LDAM_W.ForeColor = Color.Red;
            }
            else
            {
                tb_LDAM_W.ForeColor = Color.Black;
            }

            if (totalCREW < 0)
            {
                tb_LDCREW_W.ForeColor = Color.Red;
            }
            else
            {
                tb_LDCREW_W.ForeColor = Color.Black;
            }

            if (total < 0)
            {
                tb_LD_TW.ForeColor = Color.Red;
            }
            else
            {
                tb_LD_TW.ForeColor = Color.Black;
            }

            //GM

            if ((totalGM - Math.Truncate(totalGM)) == .00)
            {
                tb_LDGM_W.Text = Convert.ToString(totalGM);
                tb_LDGM_W.Text = string.Format(modified, "{0:c0}", double.Parse(tb_LDGM_W.Text));
            }
            else
            {
                tb_LDGM_W.Text = Convert.ToString(totalGM);
                tb_LDGM_W.Text = string.Format(modified, "{0:c2}", double.Parse(tb_LDGM_W.Text));
            }

            //AM

            if ((totalAM - Math.Truncate(totalAM)) == .00)
            {
                tb_LDAM_W.Text = Convert.ToString(totalAM);
                tb_LDAM_W.Text = string.Format(modified, "{0:c0}", double.Parse(tb_LDAM_W.Text));
            }
            else
            {
                tb_LDAM_W.Text = Convert.ToString(totalAM);
                tb_LDAM_W.Text = string.Format(modified, "{0:c2}", double.Parse(tb_LDAM_W.Text));
            }

            //CREW

            if ((totalCREW - Math.Truncate(totalCREW)) == .00)
            {
                tb_LDCREW_W.Text = Convert.ToString(totalCREW);
                tb_LDCREW_W.Text = string.Format(modified, "{0:c0}", double.Parse(tb_LDCREW_W.Text));
            }
            else
            {
                tb_LDCREW_W.Text = Convert.ToString(totalGM);
                tb_LDCREW_W.Text = string.Format(modified, "{0:c2}", double.Parse(tb_LDCREW_W.Text));
            }

            //Total

            if ((total - Math.Truncate(total)) == .00)
            {
                tb_LD_TW.Text = Convert.ToString(total);
                tb_LD_TW.Text = string.Format(modified, "{0:c0}", double.Parse(tb_LD_TW.Text));
            }
            else
            {
                tb_LD_TW.Text = Convert.ToString(total);
                tb_LD_TW.Text = string.Format(modified, "{0:c2}", double.Parse(tb_LD_TW.Text));
            }

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

            double AMW = totalAM;

            if ((AMW - Math.Truncate(AMW)) == .00)
            {
                reportParameters.Add(new ReportParameter("AMW", AMW.ToString("C0", modified)));
            }
            else
            {
                reportParameters.Add(new ReportParameter("AMW", AMW.ToString("C2", modified)));
            }

            double GMW = totalGM;

            if ((GMW - Math.Truncate(GMW)) == .00)
            {
                reportParameters.Add(new ReportParameter("GMW", GMW.ToString("C0", modified)));
            }
            else
            {
                reportParameters.Add(new ReportParameter("GMW", GMW.ToString("C2", modified)));
            }

            double CREWW = totalCREW;

            if ((CREWW - Math.Truncate(CREWW)) == .00)
            {
                reportParameters.Add(new ReportParameter("CREWW", CREWW.ToString("C0", modified)));
            }
            else
            {
                reportParameters.Add(new ReportParameter("CREWW", CREWW.ToString("C2", modified)));
            }

            double TW = total;

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

        public static bool CheckInt(string input)
        {
            double number = 0.00;
            return double.TryParse(input, out number);
        }

        public void SavetoDB()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            DateTime date = DateTime.Today;
            string sqlStmt = "insert into labor_data_tb (annual_gross_revenue,annual_operating_days,daily_operating_hrs,annual_operating_hrs,model,labour_data_date,am_no_labor,am_daily_hrs_worked,am_annual_days_worked,am_hourly_wages,am_annual_wages,crew_no_labor,crew_daily_hrs_worked,crew_annual_days_worked,crew_hourly_wages,crew_annual_wages,gm_no_labor,gm_daily_hrs_worked,gm_annual_days_worked,gm_hourly_wages,gm_annual_wages,total_annual_wages,saved_date,notes) values (@annual_gross_revenue,@annual_operating_days,@daily_operating_hrs,@annual_operating_hrs,@model,@labour_data_date,@am_no_labor,@am_daily_hrs_worked,@am_annual_days_worked,@am_hourly_wages,@am_annual_wages,@crew_no_labor,@crew_daily_hrs_worked,@crew_annual_days_worked,@crew_hourly_wages,@crew_annual_wages,@gm_no_labor,@gm_daily_hrs_worked,@gm_annual_days_worked,@gm_hourly_wages,@gm_annual_wages,@total_annual_wages,@saved_date,@notes)";

            SqlCommand cmd = new SqlCommand(sqlStmt, cn);

            try
            {


                cn.Open();

                cmd.Parameters.Add(new SqlParameter("@annual_gross_revenue", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@annual_operating_days", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@daily_operating_hrs", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@annual_operating_hrs", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@model", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@labour_data_date", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@am_no_labor", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@am_daily_hrs_worked", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@am_annual_days_worked", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@am_hourly_wages", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@am_annual_wages", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@crew_no_labor", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@crew_daily_hrs_worked", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@crew_annual_days_worked", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@crew_hourly_wages", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@crew_annual_wages", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@gm_no_labor", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@gm_daily_hrs_worked", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@gm_annual_days_worked", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@gm_hourly_wages", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@gm_annual_wages", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@total_annual_wages", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@saved_date", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@notes", SqlDbType.Text));

                cmd.Parameters["@annual_gross_revenue"].Value = ddl_LD_AGR.SelectedItem.Text;
                cmd.Parameters["@annual_operating_days"].Value = tb_LD_AOD.Text;
                cmd.Parameters["@daily_operating_hrs"].Value = tb_LD_DOH.Text;
                cmd.Parameters["@annual_operating_hrs"].Value = tb_LD_AOH.Text;
                cmd.Parameters["@model"].Value = ddl_LD_Model.SelectedItem.Text;
                cmd.Parameters["@labour_data_date"].Value = tb_LD_Date.Text;

                if (tb_LDAM_ADW.Text == "")
                {
                    cmd.Parameters["@am_no_labor"].Value = "-";
                    cmd.Parameters["@am_daily_hrs_worked"].Value = "-";
                    cmd.Parameters["@am_annual_days_worked"].Value = "-";
                    cmd.Parameters["@am_hourly_wages"].Value = "$0";
                }
                else
                {
                    cmd.Parameters["@am_no_labor"].Value = tb_LDAM_NoL.Text;
                    cmd.Parameters["@am_daily_hrs_worked"].Value = tb_LDAM_DOW.Text;
                    cmd.Parameters["@am_annual_days_worked"].Value = tb_LDAM_ADW.Text;
                    cmd.Parameters["@am_hourly_wages"].Value = tb_LDAM_HW.Text;
                }

                cmd.Parameters["@am_annual_wages"].Value = tb_LDAM_W.Text;

                if (tb_LDCREW_ADW.Text == "")
                {
                    cmd.Parameters["@crew_no_labor"].Value = "-";
                    cmd.Parameters["@crew_daily_hrs_worked"].Value = "-";
                    cmd.Parameters["@crew_annual_days_worked"].Value = "-";
                    cmd.Parameters["@crew_hourly_wages"].Value = "$0";
                }
                else
                {
                    cmd.Parameters["@crew_no_labor"].Value = tb_LDCREW_NoL.Text;
                    cmd.Parameters["@crew_daily_hrs_worked"].Value = tb_LDCREW_DOW.Text;
                    cmd.Parameters["@crew_annual_days_worked"].Value = tb_LDCREW_ADW.Text;
                    cmd.Parameters["@crew_hourly_wages"].Value = tb_LDCREW_HW.Text;
                }

                cmd.Parameters["@crew_annual_wages"].Value = tb_LDCREW_W.Text;

                if (tb_LDGM_ADW.Text == "")
                {
                    cmd.Parameters["@gm_no_labor"].Value = "-";
                    cmd.Parameters["@gm_daily_hrs_worked"].Value = "-";
                    cmd.Parameters["@gm_annual_days_worked"].Value = "-";
                    cmd.Parameters["@gm_hourly_wages"].Value = "$0";
                }
                else
                {
                    cmd.Parameters["@gm_no_labor"].Value = tb_LDGM_NoL.Text;
                    cmd.Parameters["@gm_daily_hrs_worked"].Value = tb_LDGM_DOW.Text;
                    cmd.Parameters["@gm_annual_days_worked"].Value = tb_LDGM_ADW.Text;
                    cmd.Parameters["@gm_hourly_wages"].Value = tb_LDGM_HW.Text;
                }

                cmd.Parameters["@gm_annual_wages"].Value = tb_LDGM_W.Text;
                cmd.Parameters["@total_annual_wages"].Value = tb_LD_TW.Text;
                cmd.Parameters["@saved_date"].Value = date.ToString("d");

                if (ta_LD_Notes.Text == "")
                {
                    cmd.Parameters["@notes"].Value = "-";
                }
                else
                {
                    cmd.Parameters["@notes"].Value = ta_LD_Notes.Text;
                }

                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; con.Open();

                    string query = "DELETE FROM labor_data_tb WHERE id='" + id + "'";
                    SqlCommand comd = new SqlCommand(query, con);
                    int j = comd.ExecuteNonQuery();

                    if(j > 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('congratulations', 'Labour Data Updated to DB Succesfully :)', 'success')", true);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Error', 'Unknown Error! Try Again :)', 'error')", true);
                    }
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