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
    public partial class ViewProfitLossReport : System.Web.UI.Page
    {
        string ID;
        protected void Page_Load(object sender, EventArgs e)
        {
            ta_LD_Notes.Enabled = false;

            ID = Session["ViewID"].ToString();
            if (!IsPostBack)
            {
                getDatafromDB();
                Reload();
                
            }
        }

        void getDatafromDB()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            con.Open();
            string Query = "SELECT * FROM ProfitLoss where Id='" + ID + "'";
            SqlCommand cmd = new SqlCommand(Query, con);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ddl_PL_Model.SelectedItem.Text = reader["Model"].ToString();
                 tb_PL_Date.Text = reader["Date"].ToString();
                 tb_PL_id.Text = reader["Id"].ToString();
                 ddl_PL_Model.SelectedItem.Text = reader["Model"].ToString();
                 tb_PL_GrossSales.Text = reader["GrossSales"].ToString();
                tb_PL_CashSales.Text = reader["CashSales"].ToString();
                 tb_PL_CashSales_perctange.Text = reader["CashSalesPercentage"].ToString();
                 tb_PL_non_CashSales.Text = reader["NonCashSales"].ToString();
                 tb_PL_nonCashSales_Perctange.Text = reader["NonCashSalesPercentage"].ToString();
                tb_PLO_GrossSales.Text = reader["TotalSales"].ToString();
                 tb_PL_GrossSales_Percentage.Text = reader["TotalSalesPercentage"].ToString();
                tbPayrolExpense.Text = reader["PayrollTax"].ToString();
                tbCardFee.Text = reader["CardFee"].ToString();
                  tbZ.Text = reader["Operator"].ToString();
                tbInsuranceExp.Text = reader["Insurance"].ToString();
                tb_PL_TotalCost.Text = reader["TotalCost"].ToString();
                 tb_PL_TotalCost_percentage.Text = reader["TotalCostPercentage"].ToString();
                 tb_PL_TotalProfit.Text = reader["TotalProfit"].ToString();
                 tb_PL_TotalProfit_Percentage.Text = reader["TotalProfitPercentage"].ToString();
                 ddl_PL_LabourModel.SelectedItem.Text = reader["LabourModel"].ToString();
                 tb_PL_CostofLabour.Text = reader["CostofLabor"].ToString();
                 tb_CostofLab_Per.Text = reader["LabourPercentage"].ToString();
                 tb_PL_TotalLabour.Text = reader["TotalLabor"].ToString();
                 tbInsurance.Text = reader["SetInsurance"].ToString();
                 tbInsurancePercentage.Text = reader["SetInsurancePercentage"].ToString();
                 tbGetInsurance.Text = reader["GetInsurance"].ToString();
                 tbSetPayrollTax.Text = reader["SetPayrollTax"].ToString();
                 tbSetPayrollTaxPercentage.Text = reader["PayrollTaxPercentage"].ToString();
                 tbGetPayrollTax.Text = reader["GetPayroll"].ToString();
                tbOpExfromGV.Text = reader["OpEx"].ToString();
                 tbOpExfromGVPer.Text = reader["OpExPercentage"].ToString();
                 tbCardProcessingFee.Text = reader["SetCardFee"].ToString();
                 tbCardProcess.Text = reader["CardProcessingFee"].ToString();
                 tbCardProcessingFeePercentage.Text = reader["CardProcessingFeePercentage"].ToString();
                 tbOpExTotal.Text = reader["TotalOpEx"].ToString();
                 tbOpExTotalPer.Text = reader["TotalOpExPercentage"].ToString();
                 tbManaged.Text = reader["Managed"].ToString();
                 tbManagedPercentage.Text = reader["ManagedPercentage"].ToString();
                 tbOwner.Text = reader["Owner"].ToString();
                 tbOwnerPercentage.Text = reader["OwnerPercentage"].ToString();
                 tbSaveDtae.Text = reader["CurrentDate"].ToString();
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

            string Query = "SELECT * FROM COGS where profitlossId = @profitlossId";

            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@profitlossId", tb_PL_id.Text);
            SqlDataAdapter sqladp = new SqlDataAdapter(cmd);
            DataTable sqldatab = new DataTable();
            sqladp.Fill(sqldatab);
            ViewState["myViewState"] = sqldatab;

            girdviewPL.DataSource = sqldatab;

            girdviewPL.DataBind();

            string QueryPLE = "SELECT * FROM COL where profitlossId = @profitlossId";

            SqlCommand cmdPLE = new SqlCommand(QueryPLE, con);
            cmdPLE.Parameters.AddWithValue("@profitlossId", tb_PL_id.Text);
            SqlDataAdapter sqladpPLE = new SqlDataAdapter(cmdPLE);
            DataTable sqldatabPLE = new DataTable();
            sqladpPLE.Fill(sqldatabPLE);
            ViewState["myViewStatePLE"] = sqldatabPLE;

            girdviewPLE.DataSource = sqldatabPLE;

            girdviewPLE.DataBind();

            string QueryOpEx = "SELECT * FROM OpEx where profitlossId = @profitlossId";

            SqlCommand cmdOpExPLE = new SqlCommand(QueryOpEx, con);
            cmdOpExPLE.Parameters.AddWithValue("@profitlossId", tb_PL_id.Text);
            SqlDataAdapter sqladpOpEx = new SqlDataAdapter(cmdOpExPLE);
            DataTable sqldatabOpEx = new DataTable();
            sqladpOpEx.Fill(sqldatabOpEx);
            ViewState["myViewStatePLE"] = sqldatabOpEx;

            GVOpEx.DataSource = sqldatabOpEx;

            GVOpEx.DataBind();
        }

        protected void btn_PL_Calculate_Click(object sender, EventArgs e)
        {
            if (tbOwnerPercentage.Text == "" || ddl_PL_Model.SelectedItem.Text == "Select Model" || ddl_PL_LabourModel.SelectedItem.Text == "Select Labour Model")
            {
                string head = "Error";
                string headtext = "Please enter all values ";
                string headtype = "warning";
                string cancelmsg = "Please enter all values";
                string cancelHead = "Cancelled";
                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);

            }
            else
            {
                Session["Date"] = tb_PL_Date.Text;
                Session["IDPL"] = tb_PL_id.Text;
                Session["Model"] = ddl_PL_Model.SelectedItem.Text;
                Session["GrossSales"] = tb_PL_GrossSales.Text;
                Session["CashSales"] = tb_PL_CashSales.Text;
                Session["CashSalesPercentage"] = tb_PL_CashSales_perctange.Text;
                Session["NonCashSales"] = tb_PL_non_CashSales.Text;
                Session["NonCashSalesPercentage"] = tb_PL_nonCashSales_Perctange.Text;
                Session["TotalGrossSales"] = tb_PLO_GrossSales.Text;
                Session["TotalGrossSalesPercentage"] = tb_PL_GrossSales_Percentage.Text;
                Session["PayrolTax"] = tbGetPayrollTax.Text;
                Session["CardFee"] = tbCardProcessingFee.Text;
                Session["Operator"] = tbZ.Text;
                Session["Insurance"] = tbGetInsurance.Text;
                Session["TotalCost"] = tb_PL_TotalCost.Text;
                Session["TotalCostPer"] = tb_PL_TotalCost_percentage.Text;
                Session["TotalProfit"] = tb_PL_TotalProfit.Text;
                Session["TotalProfitPer"] = tb_PL_TotalProfit_Percentage.Text;
                Session["LEModel"] = ddl_PL_LabourModel.SelectedItem.Text;
                Session["COL"] = tb_PL_CostofLabour.Text;
                Session["COLPer"] = tb_CostofLab_Per.Text;
                Session["TotalLab"] = tb_PL_TotalLabour.Text;
                Session["InsSet"] = tbInsurance.Text;
                Session["InsSetPer"] = tbInsurancePercentage.Text;
                Session["InsGet"] = tbGetInsurance.Text;
                Session["Payrol"] = tbSetPayrollTax.Text;
                Session["payrolPer"] = tbSetPayrollTaxPercentage.Text;
                Session["payrolget"] = tbGetPayrollTax.Text;
                Session["OpEx"] = tbOpExfromGV.Text;
                Session["OpExPer"] = tbOpExfromGVPer.Text;
                Session["CardFees"] = tbCardProcessingFee.Text;
                Session["tbCardProcess"] = tbCardProcess.Text;
                Session["tbCardProcessPer"] = tbCardProcessingFeePercentage.Text;
                Session["TotalOpEx"] = tbOpExTotal.Text;
                Session["TotalOpExPer"] = tbOpExTotalPer.Text;
                Session["tbManaged"] = tbManaged.Text;
                Session["tbManagedPercentage"] = tbManagedPercentage.Text;
                Session["tbOwner"] = tbOwner.Text;
                Session["tbOwnerPercentage"] = tbOwnerPercentage.Text;
                if (ta_LD_Notes.Text == "")
                {
                    Session["ta_LD_Notes"] = "--";
                }
                else
                {
                    Session["ta_LD_Notes"] = ta_LD_Notes.Text;

                }
                Response.Write("<script>window.open ('ReportProfitLoss.aspx','_blank');</script>");

            }
        }

        public static bool CheckInt(string input)
        {
            double number = 0.00;
            return double.TryParse(input, out number);
        }

        protected void girdview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            girdviewPL.PageIndex = e.NewPageIndex;
            this.Reload();
        }

        protected void girdviewPLE_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            girdviewPLE.PageIndex = e.NewPageIndex;
            this.Reload();
        }

        protected void GVOpEx_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVOpEx.PageIndex = e.NewPageIndex;
            this.Reload();
        }

    }
}
    
