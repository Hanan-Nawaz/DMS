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
    public partial class ProfitLoss : System.Web.UI.Page
    {

            double totalamount = 0;
            double totalamountLE = 0;
            double totalpercentage = 0;
            double totalpercentageLE = 0;
            double totalamuntOpEx = 0;
            double totalpercentageOpEx = 0;
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    Reload();

                    DateTime dateTime = DateTime.Today;
                    string date = dateTime.ToString("yyyyMMdd");
                    tb_PL_Date.Text = dateTime.ToString("d");
                    long id = long.Parse(date);
                    long ID = id + 30;
                    IDCheck(ID);
                }
            }

            void IDCheck(long ID)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                con.Open();

                string PLID = "P&L" + ID.ToString();
                SqlCommand check_Id = new SqlCommand("SELECT * FROM ProfitLoss WHERE ([Id] = @Id)", con);
                check_Id.Parameters.AddWithValue("@Id", PLID);
                SqlDataReader reader = check_Id.ExecuteReader();
                if (reader.HasRows)
                {
                    long Para_ID = ID + 30;
                    IDCheck(Para_ID);
                }
                else
                {
                    tb_PL_id.Text = Convert.ToString(PLID);
                }
            }

            void Reload()
            {
                ddl_PL_Model.Items.Clear();
                ddl_PL_ItemName.Items.Clear();
                ddl_Labour_Item_Type.Items.Clear();
                ddl_PL_LabourModel.Items.Clear();

                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                con.Open();


                string ModelQuery = "SELECT * from sales_revenue_model";
                SqlDataAdapter sqladpterModel = new SqlDataAdapter(ModelQuery, con);
                DataTable sqldatabaseModel = new DataTable();
                sqladpterModel.Fill(sqldatabaseModel);
                ddl_PL_Model.DataSource = sqldatabaseModel;
                ddl_PL_Model.DataTextField = "Model";
                ddl_PL_Model.DataValueField = "Id";
                ddl_PL_Model.DataBind();

                ddl_PL_Model.Items.Insert(0, "Select Model");



                string ItemNameQuery = "SELECT * from ItemName";
                SqlDataAdapter sqladpterItemName = new SqlDataAdapter(ItemNameQuery, con);
                DataTable sqldatabaseItemName = new DataTable();
                sqladpterItemName.Fill(sqldatabaseItemName);
                ddl_PL_ItemName.DataSource = sqldatabaseItemName;
                ddl_PL_ItemName.DataTextField = "ItemName";
                ddl_PL_ItemName.DataValueField = "Id";
                ddl_PL_ItemName.DataBind();

                ddl_PL_ItemName.Items.Insert(0, "Select Item Name");

                string ItemNameQueryOpEx = "SELECT * from OpExItemName";
                SqlDataAdapter sqladpterItemNameOpEx = new SqlDataAdapter(ItemNameQueryOpEx, con);
                DataTable sqldatabaseItemNameOpEx = new DataTable();
                sqladpterItemNameOpEx.Fill(sqldatabaseItemNameOpEx);
                ddlOpExItemName.DataSource = sqldatabaseItemNameOpEx;
                ddlOpExItemName.DataTextField = "ItemName";
                ddlOpExItemName.DataValueField = "Id";
                ddlOpExItemName.DataBind();

                ddlOpExItemName.Items.Insert(0, "Select Item Name");

                string LabourTypeQuery = "SELECT * from LabourType";
                SqlDataAdapter sqladpterLabourType = new SqlDataAdapter(LabourTypeQuery, con);
                DataTable sqldatabaseLabourType = new DataTable();
                sqladpterLabourType.Fill(sqldatabaseLabourType);
                ddl_Labour_Item_Type.DataSource = sqldatabaseLabourType;
                ddl_Labour_Item_Type.DataTextField = "LabourType";
                ddl_Labour_Item_Type.DataValueField = "Id";
                ddl_Labour_Item_Type.DataBind();

                ddl_Labour_Item_Type.Items.Insert(0, "Select Labour Type");

                string LabourModelQuery = "SELECT * from LabourExpenseModel";
                SqlDataAdapter sqladpterLabourModel = new SqlDataAdapter(LabourModelQuery, con);
                DataTable sqldatabaseLabourModel = new DataTable();
                sqladpterLabourModel.Fill(sqldatabaseLabourModel);
                ddl_PL_LabourModel.DataSource = sqldatabaseLabourModel;
                ddl_PL_LabourModel.DataTextField = "Model";
                ddl_PL_LabourModel.DataValueField = "Id";
                ddl_PL_LabourModel.DataBind();

                ddl_PL_LabourModel.Items.Insert(0, "Select Labour Model");

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

            protected void btn_Save_toDB_Click(object sender, EventArgs e)
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
                    SavetoDB();
                }

            }

            public void SavetoDB()
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                string sqlStmt = "INSERT INTO ProfitLoss ( Id,Model,GrossSales,CashSales,CashSalesPercentage,NonCashSales,NonCashSalesPercentage,TotalSales,TotalSalesPercentage,Date,PayrollTax,Insurance,CardFee,Operator,TotalCost,TotalCostPercentage,TotalProfit,TotalProfitPercentage,LabourModel,CostofLabor ,LabourPercentage,TotalLabor,SetInsurance,SetInsurancePercentage,GetInsurance,SetPayrollTax,PayrollTaxPercentage,GetPayroll,OpEx,OpExPercentage,SetCardFee,CardProcessingFee,CardProcessingFeePercentage,TotalOpEx,TotalOpExPercentage,Managed,ManagedPercentage,Owner,OwnerPercentage,Notes,CurrentDate) VALUES ( @Id,@Model,@GrossSales,@CashSales,@CashSalesPercentage,@NonCashSales,@NonCashSalesPercentage,@TotalSales,@TotalSalesPercentage,@Date,@PayrollTax,@Insurance,@CardFee,@Operator ,@TotalCost,@TotalCostPercentage,@TotalProfit,@TotalProfitPercentage,@LabourModel,@CostofLabor,@LabourPercentage,@TotalLabor,@SetInsurance,@SetInsurancePercentage,@GetInsurance,@SetPayrollTax,@PayrollTaxPercentage,@GetPayroll ,@OpEx,@OpExPercentage,@SetCardFee,@CardProcessingFee,@CardProcessingFeePercentage,@TotalOpEx,@TotalOpExPercentage,@Managed,@ManagedPercentage,@Owner,@OwnerPercentage,@Notes,@CurrentDate )";
                SqlCommand cmd = new SqlCommand(sqlStmt, cn);

                try
                {


                    cn.Open();

                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Model", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@GrossSales", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@CashSales", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@CashSalesPercentage", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@NonCashSales", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@NonCashSalesPercentage", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@TotalSales", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@TotalSalesPercentage", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@PayrollTax", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Insurance", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@CardFee", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Operator", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@TotalCost", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@TotalCostPercentage", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@TotalProfit", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@TotalProfitPercentage", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@LabourModel", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@CostofLabor", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@LabourPercentage", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@TotalLabor", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@SetInsurance", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@SetInsurancePercentage", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@GetInsurance", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@SetPayrollTax", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@PayrollTaxPercentage", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@GetPayroll", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@OpEx", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@OpExPercentage", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@SetCardFee", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@CardProcessingFee", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@CardProcessingFeePercentage", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@TotalOpEx", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@TotalOpExPercentage", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Managed", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@ManagedPercentage", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Owner", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@OwnerPercentage", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Notes", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@CurrentDate", SqlDbType.VarChar));

                    cmd.Parameters["@Id"].Value = tb_PL_id.Text;
                    cmd.Parameters["@Model"].Value = ddl_PL_Model.SelectedItem.Text;
                    cmd.Parameters["@GrossSales"].Value = tb_PL_GrossSales.Text;
                    cmd.Parameters["@CashSales"].Value = tb_PL_CashSales.Text;
                    cmd.Parameters["@CashSalesPercentage"].Value = tb_PL_CashSales_perctange.Text;
                    cmd.Parameters["@NonCashSales"].Value = tb_PL_non_CashSales.Text;
                    cmd.Parameters["@NonCashSalesPercentage"].Value = tb_PL_nonCashSales_Perctange.Text;
                    cmd.Parameters["@TotalSales"].Value = tb_PLO_GrossSales.Text;
                    cmd.Parameters["@TotalSalesPercentage"].Value = tb_PL_GrossSales_Percentage.Text;
                    cmd.Parameters["@Date"].Value = tb_PL_Date.Text;
                    cmd.Parameters["@PayrollTax"].Value = tbGetPayrollTax.Text;
                    cmd.Parameters["@Insurance"].Value = tbGetInsurance.Text;
                    cmd.Parameters["@CardFee"].Value = tbCardProcessingFee.Text;
                    cmd.Parameters["@Operator"].Value = tbZ.Text;
                    cmd.Parameters["@TotalCost"].Value = tb_PL_TotalCost.Text;
                    cmd.Parameters["@TotalCostPercentage"].Value = tb_PL_TotalCost_percentage.Text;
                    cmd.Parameters["@TotalProfit"].Value = tb_PL_TotalProfit.Text;
                    cmd.Parameters["@TotalProfitPercentage"].Value = tb_PL_TotalProfit_Percentage.Text;
                    cmd.Parameters["@LabourModel"].Value = ddl_PL_LabourModel.SelectedItem.Text;
                    cmd.Parameters["@CostofLabor"].Value = tb_PL_CostofLabour.Text;
                    cmd.Parameters["@LabourPercentage"].Value = tb_CostofLab_Per.Text;
                    cmd.Parameters["@TotalLabor"].Value = tb_PL_TotalLabour.Text;
                    cmd.Parameters["@SetInsurance"].Value = tbInsurance.Text;
                    cmd.Parameters["@SetInsurancePercentage"].Value = tbInsurancePercentage.Text;
                    cmd.Parameters["@GetInsurance"].Value = tbGetInsurance.Text;
                    cmd.Parameters["@SetPayrollTax"].Value = tbSetPayrollTax.Text;
                    cmd.Parameters["@PayrollTaxPercentage"].Value = tbSetPayrollTaxPercentage.Text;
                    cmd.Parameters["@GetPayroll"].Value = tbGetPayrollTax.Text;
                    cmd.Parameters["@OpEx"].Value = tbOpExfromGV.Text;
                    cmd.Parameters["@OpExPercentage"].Value = tbOpExfromGVPer.Text;
                    cmd.Parameters["@SetCardFee"].Value = tbCardProcessingFee.Text;
                    cmd.Parameters["@CardProcessingFee"].Value = tbCardProcess.Text;
                    cmd.Parameters["@CardProcessingFeePercentage"].Value = tbCardProcessingFeePercentage.Text;
                    cmd.Parameters["@TotalOpEx"].Value = tbOpExTotal.Text;
                    cmd.Parameters["@TotalOpExPercentage"].Value = tbOpExTotalPer.Text;
                    cmd.Parameters["@Managed"].Value = tbManaged.Text;
                    cmd.Parameters["@ManagedPercentage"].Value = tbManagedPercentage.Text;
                    cmd.Parameters["@Owner"].Value = tbOwner.Text;
                    cmd.Parameters["@OwnerPercentage"].Value = tbOwnerPercentage.Text;

                    DateTime date = DateTime.Today;

                    cmd.Parameters["@CurrentDate"].Value = date.ToString("d");

                    if (ta_LD_Notes.Text == "")
                    {
                        cmd.Parameters["@Notes"].Value = "-";
                    }
                    else
                    {
                        cmd.Parameters["@Notes"].Value = ta_LD_Notes.Text;
                    }

                    int i = cmd.ExecuteNonQuery();

                    if (i > 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('congratulations', 'Profit Loss Data Inserted to DB Succesfully :)', 'success')", true);
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


            protected void btn_add_model_Click(object sender, EventArgs e)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                con.Open();
                string sqlStmt = "insert into sales_revenue_model (Model) Values(@Model) ";
                SqlCommand cmd;
                cmd = new SqlCommand(sqlStmt, con);

                cmd.Parameters.Add(new SqlParameter("@Model", SqlDbType.VarChar, 50));

                cmd.Parameters["@Model"].Value = tb_model.Text;

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
                    this.Reload();
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

            protected void tb_PL_GrossSales_TextChanged(object sender, EventArgs e)
            {
                var original = new CultureInfo("en-us");

                var modified = (CultureInfo)original.Clone();
                modified.NumberFormat.CurrencyNegativePattern = 1;


                if (CheckInt(tb_PL_GrossSales.Text) == true)
                {
                    double PLGrossSales = Convert.ToDouble(tb_PL_GrossSales.Text);

                    if (PLGrossSales < 0)
                    {
                        tb_PL_GrossSales.ForeColor = Color.Red;
                    }
                    else
                    {
                        tb_PL_GrossSales.ForeColor = Color.Black;
                    }

                    if ((PLGrossSales - Math.Truncate(PLGrossSales)) == .00)
                    {
                        tb_PL_GrossSales.Text = Convert.ToString(PLGrossSales);
                        tb_PL_GrossSales.Text = string.Format(modified, "{0:c0}", double.Parse(tb_PL_GrossSales.Text));
                    }
                    else
                    {
                        tb_PL_GrossSales.Text = Convert.ToString(PLGrossSales);
                        tb_PL_GrossSales.Text = string.Format(modified, "{0:c2}", double.Parse(tb_PL_GrossSales.Text));
                    }
                }
                else
                {
                    string head = "Error";
                    string headtext = "Please enter all values in Integer/Double";
                    string headtype = "warning";
                    string cancelmsg = "Please enter all values in Integer";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                }

            }

            public static bool CheckInt(string input)
            {
                double number = 0.00;
                return double.TryParse(input, out number);
            }

            protected void tb_PL_CashSales_TextChanged(object sender, EventArgs e)
            {
                var original = new CultureInfo("en-us");

                var modified = (CultureInfo)original.Clone();
                modified.NumberFormat.CurrencyNegativePattern = 1;

                if (CheckInt(tb_PL_CashSales.Text) == true)
                {
                    double PLCashSales = Convert.ToDouble(tb_PL_CashSales.Text);
                    string GrossSale = Convert.ToString(tb_PL_GrossSales.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                    double GrossSales = Convert.ToDouble(GrossSale);

                    if (GrossSales < PLCashSales)
                    {
                        string head = "Error";
                        string headtext = "Cash Sales is always Samller than Gross Sales";
                        string headtype = "warning";
                        string cancelmsg = "Cash Sales is always Samller than Gross Sales";
                        string cancelHead = "Cancelled";
                        string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                        tb_PL_CashSales.Text = "";
                    }
                    else
                    {
                        if (tb_PL_GrossSales.Text == "")
                        {
                            string head = "Error";
                            string headtext = "Please enter Gross Sales";
                            string headtype = "warning";
                            string cancelmsg = "Please enter Gross Sales";
                            string cancelHead = "Cancelled";
                            string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                            tb_PL_CashSales.Text = "";
                        }
                        else
                        {
                            double PLCashSalesPerctange = (PLCashSales / GrossSales) * 100;

                            if (PLCashSalesPerctange < 0)
                            {
                                tb_PL_CashSales_perctange.ForeColor = Color.Red;
                            }
                            else
                            {
                                tb_PL_CashSales_perctange.ForeColor = Color.Black;
                            }

                            if ((PLCashSalesPerctange - Math.Truncate(PLCashSalesPerctange)) == .00)
                            {
                                tb_PL_CashSales_perctange.Text = Convert.ToString(PLCashSalesPerctange);
                                tb_PL_CashSales_perctange.Text = string.Format(modified, "{0:n0}", double.Parse(tb_PL_CashSales_perctange.Text));
                                tb_PL_CashSales_perctange.Text = tb_PL_CashSales_perctange.Text + "%";
                            }
                            else
                            {
                                tb_PL_CashSales_perctange.Text = Convert.ToString(PLCashSalesPerctange);
                                tb_PL_CashSales_perctange.Text = string.Format(modified, "{0:n2}", double.Parse(tb_PL_CashSales_perctange.Text));
                                tb_PL_CashSales_perctange.Text = tb_PL_CashSales_perctange.Text + "%";
                            }

                            if (PLCashSales < 0)
                            {
                                tb_PL_CashSales.ForeColor = Color.Red;
                            }
                            else
                            {
                                tb_PL_CashSales.ForeColor = Color.Black;
                            }

                            if ((PLCashSales - Math.Truncate(PLCashSales)) == .00)
                            {
                                tb_PL_CashSales.Text = Convert.ToString(PLCashSales);
                                tb_PL_CashSales.Text = string.Format(modified, "{0:c0}", double.Parse(tb_PL_CashSales.Text));
                            }
                            else
                            {
                                tb_PL_CashSales.Text = Convert.ToString(PLCashSales);
                                tb_PL_CashSales.Text = string.Format(modified, "{0:c2}", double.Parse(tb_PL_CashSales.Text));
                            }
                        }
                    }

                }
                else
                {
                    string head = "Error";
                    string headtext = "Please enter all values in Integer/Double";
                    string headtype = "warning";
                    string cancelmsg = "Please enter all values in Integer";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                }
            }

            protected void tb_PL_non_CashSales_TextChanged(object sender, EventArgs e)
            {
                var original = new CultureInfo("en-us");

                var modified = (CultureInfo)original.Clone();
                modified.NumberFormat.CurrencyNegativePattern = 1;

                if (ddl_PL_Model.SelectedItem.Text == "Select Model" || tb_PL_CashSales.Text == "" || tb_PL_GrossSales.Text == "")
                {
                    string head = "Error";
                    string headtext = "Please Enter All Values";
                    string headtype = "warning";
                    string cancelmsg = "Please Enter All Values";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    tb_PL_non_CashSales.Text = "";
                }
                else
                {

                    if (CheckInt(tb_PL_non_CashSales.Text) == true)
                    {
                        double PLNonCashSales = Convert.ToDouble(tb_PL_non_CashSales.Text);
                        string GrossSale = Convert.ToString(tb_PL_GrossSales.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                        double GrossSales = Convert.ToDouble(GrossSale);
                        string PLCashSale = Convert.ToString(tb_PL_CashSales.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                        double PLCashSales = Convert.ToDouble(PLCashSale);

                        if (GrossSales < PLNonCashSales)
                        {
                            string head = "Error";
                            string headtext = "Cash Sales is always Samller than Gross Sales";
                            string headtype = "warning";
                            string cancelmsg = "Cash Sales is always Samller than Gross Sales";
                            string cancelHead = "Cancelled";
                            string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                            tb_PL_CashSales.Text = "";
                        }
                        else
                        {
                            if (tb_PL_GrossSales.Text == "")
                            {
                                string head = "Error";
                                string headtext = "Please enter Gross Sales";
                                string headtype = "warning";
                                string cancelmsg = "Please enter Gross Sales";
                                string cancelHead = "Cancelled";
                                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                                tb_PL_CashSales.Text = "";
                            }
                            else
                            {
                                double PLNonCashSalesPerctange = (PLNonCashSales / GrossSales) * 100;

                                if (PLNonCashSalesPerctange < 0)
                                {
                                    tb_PL_nonCashSales_Perctange.ForeColor = Color.Red;
                                }
                                else
                                {
                                    tb_PL_nonCashSales_Perctange.ForeColor = Color.Black;
                                }

                                if ((PLNonCashSalesPerctange - Math.Truncate(PLNonCashSalesPerctange)) == .00)
                                {
                                    tb_PL_nonCashSales_Perctange.Text = Convert.ToString(PLNonCashSalesPerctange);
                                    tb_PL_nonCashSales_Perctange.Text = string.Format(modified, "{0:n0}", double.Parse(tb_PL_nonCashSales_Perctange.Text));
                                    tb_PL_nonCashSales_Perctange.Text = tb_PL_nonCashSales_Perctange.Text + "%";
                                }
                                else
                                {
                                    tb_PL_nonCashSales_Perctange.Text = Convert.ToString(PLNonCashSalesPerctange);
                                    tb_PL_nonCashSales_Perctange.Text = string.Format(modified, "{0:n2}", double.Parse(tb_PL_nonCashSales_Perctange.Text));
                                    tb_PL_nonCashSales_Perctange.Text = tb_PL_nonCashSales_Perctange.Text + "%";
                                }

                                if (PLNonCashSales < 0)
                                {
                                    tb_PL_non_CashSales.ForeColor = Color.Red;
                                }
                                else
                                {
                                    tb_PL_non_CashSales.ForeColor = Color.Black;
                                }

                                if ((PLNonCashSales - Math.Truncate(PLNonCashSales)) == .00)
                                {
                                    tb_PL_non_CashSales.Text = Convert.ToString(PLNonCashSales);
                                    tb_PL_non_CashSales.Text = string.Format(modified, "{0:c0}", double.Parse(tb_PL_non_CashSales.Text));
                                }
                                else
                                {
                                    tb_PL_non_CashSales.Text = Convert.ToString(PLNonCashSales);
                                    tb_PL_non_CashSales.Text = string.Format(modified, "{0:c2}", double.Parse(tb_PL_non_CashSales.Text));
                                }
                            }
                        }

                        string PLNONCashSale = Convert.ToString(tb_PL_non_CashSales.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                        double PLNONCashSales = Convert.ToDouble(PLNONCashSale);

                        if ((PLNONCashSales + PLCashSales) != GrossSales)
                        {
                            string head = "Error";
                            string headtext = "Sum of Non-Cash Sales and Cash Sales must be Equal to Gross Sales";
                            string headtype = "warning";
                            string cancelmsg = "Sum of Non-Cash Sales and Cash Sales must be Equal to Gross Sales";
                            string cancelHead = "Cancelled";
                            string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);

                            tb_PL_nonCashSales_Perctange.Text = "";
                            tb_PL_CashSales_perctange.Text = "";
                            tb_PL_CashSales.Text = "";
                            tb_PL_non_CashSales.Text = "";
                        }
                        else
                        {
                            double TotalSales = PLNONCashSales + PLCashSales;
                            if (TotalSales < 0)
                            {
                                tb_PLO_GrossSales.ForeColor = Color.Red;
                            }
                            else
                            {
                                tb_PLO_GrossSales.ForeColor = Color.Black;
                            }

                            if ((TotalSales - Math.Truncate(TotalSales)) == .00)
                            {
                                tb_PLO_GrossSales.Text = Convert.ToString(TotalSales);
                                tb_PLO_GrossSales.Text = string.Format(modified, "{0:c0}", double.Parse(tb_PLO_GrossSales.Text));
                            }
                            else
                            {
                                tb_PLO_GrossSales.Text = Convert.ToString(TotalSales);
                                tb_PLO_GrossSales.Text = string.Format(modified, "{0:c2}", double.Parse(tb_PLO_GrossSales.Text));
                            }

                            double TotalPercantage = ((PLNONCashSales + PLCashSales) / GrossSales);
                            if (TotalPercantage < 0)
                            {
                                tb_PL_GrossSales_Percentage.ForeColor = Color.Red;
                            }
                            else
                            {
                                tb_PL_GrossSales_Percentage.ForeColor = Color.Black;
                            }

                            if ((TotalPercantage - Math.Truncate(TotalPercantage)) == .00)
                            {
                                tb_PL_GrossSales_Percentage.Text = Convert.ToString(TotalPercantage);
                                tb_PL_GrossSales_Percentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p0}", double.Parse(tb_PL_GrossSales_Percentage.Text));
                            }
                            else
                            {
                                tb_PL_GrossSales_Percentage.Text = Convert.ToString(TotalPercantage);
                                tb_PL_GrossSales_Percentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p2}", double.Parse(tb_PL_GrossSales_Percentage.Text));
                            }
                        }

                    }
                    else
                    {
                        string head = "Error";
                        string headtext = "Please enter all values in Integer/Double";
                        string headtype = "warning";
                        string cancelmsg = "Please enter all values in Integer";
                        string cancelHead = "Cancelled";
                        string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    }
                }

            }

            protected void btn_add_ItemName_Click(object sender, EventArgs e)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                con.Open();
                string sqlStmt = "insert into ItemName (ItemName) Values(@ItemName) ";
                SqlCommand cmd;
                cmd = new SqlCommand(sqlStmt, con);


                cmd.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.VarChar, 50));

                cmd.Parameters["@ItemName"].Value = tb_PL_ItemName.Text;

                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    string head = "Congratulations";
                    string headtext = "ItemName Added Successfully.";
                    string headtype = "success";
                    string cancelmsg = "Congrats";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    tb_PL_ItemName.Text = "";
                    this.Reload();
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#COGS').modal('show')", true);
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

            protected void tb_PL_ItemPrice_TextChanged(object sender, EventArgs e)
            {
                var original = new CultureInfo("en-us");

                var modified = (CultureInfo)original.Clone();
                modified.NumberFormat.CurrencyNegativePattern = 1;

                if (tb_PL_CashSales.Text == "" || tb_PL_GrossSales.Text == "")
                {
                    string head = "Error";
                    string headtext = "Please Enter All Values";
                    string headtype = "warning";
                    string cancelmsg = "Please Enter All Values";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    tb_PL_non_CashSales.Text = "";
                }
                else
                {

                    if (ddl_PL_ItemName.SelectedItem.Text == "Select Item Name")
                    {
                        string head = "Error";
                        string headtext = "Please Select Item Name";
                        string headtype = "warning";
                        string cancelmsg = "Please enter all Values";
                        string cancelHead = "Cancelled";
                        string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    }
                    else
                    {
                        if (CheckInt(tb_PL_ItemPrice.Text) == true)
                        {
                            double PLNonCashSales = Convert.ToDouble(tb_PL_ItemPrice.Text);


                            if (PLNonCashSales < 0)
                            {
                                tb_PL_ItemPrice.ForeColor = Color.Red;
                            }
                            else
                            {
                                tb_PL_ItemPrice.ForeColor = Color.Black;
                            }

                            if ((PLNonCashSales - Math.Truncate(PLNonCashSales)) == .00)
                            {
                                tb_PL_ItemPrice.Text = Convert.ToString(PLNonCashSales);
                                tb_PL_ItemPrice.Text = string.Format(modified, "{0:c0}", double.Parse(tb_PL_ItemPrice.Text));
                            }
                            else
                            {
                                tb_PL_ItemPrice.Text = Convert.ToString(PLNonCashSales);
                                tb_PL_ItemPrice.Text = string.Format(modified, "{0:c2}", double.Parse(tb_PL_ItemPrice.Text));
                            }


                            SqlConnection con = new SqlConnection();
                            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                            con.Open();
                            string sqlStmt = "insert into COGS (ItemName,ItemPrice,profitlossId,ItemPercentage) Values(@ItemName,@ItemPrice,@profitlossId,@ItemPercentage) ";
                            SqlCommand cmd;
                            cmd = new SqlCommand(sqlStmt, con);


                            cmd.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.VarChar, 50));
                            cmd.Parameters.Add(new SqlParameter("@ItemPrice", SqlDbType.VarChar, 50));
                            cmd.Parameters.Add(new SqlParameter("@profitlossId", SqlDbType.VarChar, 50));
                            cmd.Parameters.Add(new SqlParameter("@ItemPercentage", SqlDbType.VarChar, 50));

                            cmd.Parameters["@ItemName"].Value = ddl_PL_ItemName.SelectedItem.Text;
                            cmd.Parameters["@ItemPrice"].Value = tb_PL_ItemPrice.Text;
                            cmd.Parameters["@profitlossId"].Value = tb_PL_id.Text;

                            string ItemPrices = Convert.ToString(tb_PL_ItemPrice.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                            double EditedItemPrices = Convert.ToDouble(ItemPrices);

                            string GrossSales = Convert.ToString(tb_PL_GrossSales.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                            double EditedGrossSales = Convert.ToDouble(GrossSales);

                            double Percentage = (EditedItemPrices / EditedGrossSales) * 100;

                            if ((Percentage - Math.Truncate(Percentage)) == .00)
                            {
                                tb_Price_percentage.Text = Convert.ToString(Percentage / 100);
                                tb_Price_percentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:P0}", double.Parse(tb_Price_percentage.Text));
                            }
                            else
                            {
                                tb_Price_percentage.Text = Convert.ToString(Percentage / 100);
                                tb_Price_percentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:P2}", double.Parse(tb_Price_percentage.Text));
                            }



                            cmd.Parameters["@ItemPercentage"].Value = tb_Price_percentage.Text;

                            int i = cmd.ExecuteNonQuery();

                            if (i > 0)
                            {
                                string head = "Congratulations";
                                string headtext = "Cost of Goods Sold Added Successfully.";
                                string headtype = "success";
                                string cancelmsg = "Congrats";
                                string cancelHead = "Cancelled";
                                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                                tb_PL_ItemPrice.Text = "";
                                ddl_PL_ItemName.SelectedIndex = 0;
                                this.Reload();
                                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#COGS').modal('show')", true);
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
                        else
                        {
                            string head = "Error";
                            string headtext = "Please enter all values in Integer/Double";
                            string headtype = "warning";
                            string cancelmsg = "Please enter all values in Integer";
                            string cancelHead = "Cancelled";
                            string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                        }
                    }
                }
            }

            protected void girdview_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                girdviewPL.PageIndex = e.NewPageIndex;
                this.Reload();
            }

            protected void del_btn_Click(object sender, EventArgs e)
            {
                string id = Convert.ToString((sender as LinkButton).CommandArgument);
                string SuccessMsg = "Cost of Good Sold with ID " + id + " is deleted Successfully :)";

                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; con.Open();

                string query = "DELETE FROM COGS WHERE Id='" + id + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Congratulation', '" + SuccessMsg + "' , 'success')", true);
                    this.Reload();
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#COGS').modal('show')", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Warning', 'Un-expected Error! Try Again :) ', 'warning')", true);
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#COGS').modal('show')", true);
                }
            }

            protected void btn_del_all_COGS_Click(object sender, EventArgs e)
            {
                if (girdviewPL.Rows.Count > 0)
                {

                    string id = tb_PL_id.Text;
                    string SuccessMsg = "All Data in Cost of Goods Sold Deleted Successfully :)";

                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; con.Open();

                    string query = "DELETE FROM COGS WHERE profitlossId='" + id + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Congratulation', '" + SuccessMsg + "' , 'success')", true);
                        this.Reload();
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#COGS').modal('close')", true);
                        tb_PL_TotalProfit.Text = "";
                        tb_PL_TotalCost.Text = "";
                        tb_PL_TotalCost_percentage.Text = "";
                        tb_PL_TotalProfit_Percentage.Text = "";
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Warning', 'Un-expected Error! Try Again :) ', 'warning')", true);
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#COGS').modal('close')", true);
                        tb_PL_TotalProfit.Text = "";
                        tb_PL_TotalCost.Text = "";
                        tb_PL_TotalCost_percentage.Text = "";
                        tb_PL_TotalProfit_Percentage.Text = "";
                    }
                }

            }

            protected void btn_save_Click(object sender, EventArgs e)
            {

                if (girdviewPL.Rows.Count > 0)
                {
                    string head = "Congratulations";
                    string headtext = "All Data of Cost of Goods Sold Saved Successfully.";
                    string headtype = "success";
                    string cancelmsg = "Congrats";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#COGS').modal('close')", true);
                    tb_Price_percentage.Text = "";
                }
                else
                {
                    string head = "Error";
                    string headtext = "No Data Found! No Operation Performed";
                    string headtype = "error";
                    string cancelmsg = "No Operation Performed";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#COGS').modal('close')", true);
                    tb_Price_percentage.Text = "";
                }


            }

            protected void girdviewPL_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                var original = new CultureInfo("en-us");

                var modified = (CultureInfo)original.Clone();
                modified.NumberFormat.CurrencyNegativePattern = 1;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    string TotalItemPrice = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "itemPrice"));

                    string NumberPrice = Convert.ToString(TotalItemPrice.Replace(modified.NumberFormat.CurrencySymbol, ""));

                    totalamount += Convert.ToDouble(NumberPrice);

                    if (totalamount < 0)
                    {
                        tb_PL_TotalItemsCost.ForeColor = Color.Red;
                    }
                    else
                    {
                        tb_PL_TotalItemsCost.ForeColor = Color.Black;
                    }

                    if ((totalamount - Math.Truncate(totalamount)) == .00)
                    {
                        tb_PL_TotalItemsCost.Text = Convert.ToString(totalamount);
                        tb_PL_TotalItemsCost.Text = string.Format(modified, "{0:c0}", double.Parse(tb_PL_TotalItemsCost.Text));
                    }
                    else
                    {
                        tb_PL_TotalItemsCost.Text = Convert.ToString(totalamount);
                        tb_PL_TotalItemsCost.Text = string.Format(modified, "{0:c2}", double.Parse(tb_PL_TotalItemsCost.Text));
                    }

                    string TotalItemPercentage = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ItemPercentage"));

                    string NumberItemPercentage = Convert.ToString(TotalItemPercentage.Replace(CultureInfo.InvariantCulture.NumberFormat.PercentSymbol, ""));

                    totalpercentage += Convert.ToDouble(NumberItemPercentage);

                    if (totalpercentage < 0)
                    {
                        tb_PL_TotalItemsCostpercentage.ForeColor = Color.Red;
                    }
                    else
                    {
                        tb_PL_TotalItemsCostpercentage.ForeColor = Color.Black;
                    }

                    if ((totalpercentage - Math.Truncate(totalpercentage)) == .00)
                    {
                        tb_PL_TotalItemsCostpercentage.Text = Convert.ToString(totalpercentage / 100);
                        tb_PL_TotalItemsCostpercentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:P0}", double.Parse(tb_PL_TotalItemsCostpercentage.Text));
                    }
                    else
                    {
                        tb_PL_TotalItemsCostpercentage.Text = Convert.ToString(totalpercentage / 100);
                        tb_PL_TotalItemsCostpercentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:P2}", double.Parse(tb_PL_TotalItemsCostpercentage.Text));
                    }
                }

                string Totalcost = Convert.ToString(tb_PL_TotalItemsCost.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));

                double totalCost = Convert.ToDouble(Totalcost);

                if (totalCost < 0)
                {
                    tb_PL_TotalCost.ForeColor = Color.Red;
                }
                else
                {
                    tb_PL_TotalCost.ForeColor = Color.Black;
                }

                if ((totalCost - Math.Truncate(totalCost)) == .00)
                {
                    tb_PL_TotalCost.Text = Convert.ToString(totalCost);
                    tb_PL_TotalCost.Text = string.Format(modified, "{0:c0}", double.Parse(tb_PL_TotalCost.Text));
                }
                else
                {
                    tb_PL_TotalCost.Text = Convert.ToString(totalCost);
                    tb_PL_TotalCost.Text = string.Format(modified, "{0:c2}", double.Parse(tb_PL_TotalCost.Text));
                }



                string TotalPer = Convert.ToString(tb_PL_TotalItemsCostpercentage.Text.Replace(CultureInfo.InvariantCulture.NumberFormat.PercentSymbol, ""));

                double totalPer = Convert.ToDouble(TotalPer);

                if (totalPer < 0)
                {
                    tb_PL_TotalCost_percentage.ForeColor = Color.Red;
                }
                else
                {
                    tb_PL_TotalCost_percentage.ForeColor = Color.Black;
                }

                if ((totalPer - Math.Truncate(totalPer)) == .00)
                {
                    tb_PL_TotalCost_percentage.Text = Convert.ToString(totalPer / 100);
                    tb_PL_TotalCost_percentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p0}", double.Parse(tb_PL_TotalCost_percentage.Text));
                }
                else
                {
                    tb_PL_TotalCost_percentage.Text = Convert.ToString(totalPer / 100);
                    tb_PL_TotalCost_percentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p2}", double.Parse(tb_PL_TotalCost_percentage.Text));
                }


                double EditedGrossSales;
                string GrossSales = Convert.ToString(tb_PL_GrossSales.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));

                if (GrossSales == "")
                {
                    EditedGrossSales = 0;
                }
                else
                {
                    EditedGrossSales = Convert.ToDouble(GrossSales);
                }

                double totalProfit = EditedGrossSales - totalamount;

                if (totalProfit < 0)
                {
                    tb_PL_TotalProfit.ForeColor = Color.Red;
                }
                else
                {
                    tb_PL_TotalProfit.ForeColor = Color.Black;
                }

                if ((totalProfit - Math.Truncate(totalProfit)) == .00)
                {
                    tb_PL_TotalProfit.Text = Convert.ToString(totalProfit);
                    tb_PL_TotalProfit.Text = string.Format(modified, "{0:c0}", double.Parse(tb_PL_TotalProfit.Text));
                }
                else
                {
                    tb_PL_TotalProfit.Text = Convert.ToString(totalProfit);
                    tb_PL_TotalProfit.Text = string.Format(modified, "{0:c2}", double.Parse(tb_PL_TotalProfit.Text));
                }

                string TotalGross = Convert.ToString(tb_PL_GrossSales.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                double totalproPer;
                if (tb_PL_GrossSales.Text == "")
                {
                    totalproPer = 0;
                }
                else
                {
                    totalproPer = Convert.ToDouble((totalProfit / Convert.ToDouble(TotalGross)) * 100);
                }


                if (totalproPer < 0)
                {
                    tb_PL_TotalProfit_Percentage.ForeColor = Color.Red;
                }
                else
                {
                    tb_PL_TotalProfit_Percentage.ForeColor = Color.Black;
                }

                if ((totalproPer - Math.Truncate(totalproPer)) == .00)
                {
                    tb_PL_TotalProfit_Percentage.Text = Convert.ToString(totalproPer / 100);
                    tb_PL_TotalProfit_Percentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p0}", double.Parse(tb_PL_TotalProfit_Percentage.Text));
                }
                else
                {
                    tb_PL_TotalProfit_Percentage.Text = Convert.ToString(totalproPer / 100);
                    tb_PL_TotalProfit_Percentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p2}", double.Parse(tb_PL_TotalProfit_Percentage.Text));
                }

            }

            protected void btn_add_labour_EModel_Click(object sender, EventArgs e)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                con.Open();
                string sqlStmt = "insert into LabourExpenseModel (Model) Values(@Model) ";
                SqlCommand cmd;
                cmd = new SqlCommand(sqlStmt, con);


                cmd.Parameters.Add(new SqlParameter("@Model", SqlDbType.VarChar, 50));

                cmd.Parameters["@Model"].Value = tb_PL_LaborModel.Text;

                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    string head = "Congratulations";
                    string headtext = "Labour Expense Model Added Successfully.";
                    string headtype = "success";
                    string cancelmsg = "Congrats";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    this.Reload();
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

            protected void tb_PL_LabourCost_TextChanged(object sender, EventArgs e)
            {
                var original = new CultureInfo("en-us");

                var modified = (CultureInfo)original.Clone();
                modified.NumberFormat.CurrencyNegativePattern = 1;

                if (tb_PL_CashSales.Text == "" || tb_PL_GrossSales.Text == "")
                {
                    string head = "Error";
                    string headtext = "Please Enter All Values";
                    string headtype = "warning";
                    string cancelmsg = "Please Enter All Values";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                }
                else
                {

                    if (ddl_Labour_Item_Type.SelectedItem.Text == "Select Labour Type")
                    {
                        string head = "Error";
                        string headtext = "Please Select Labour Type";
                        string headtype = "warning";
                        string cancelmsg = "Please enter all Values";
                        string cancelHead = "Cancelled";
                        string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    }
                    else
                    {
                        if (CheckInt(tb_PL_LabourCost.Text) == true)
                        {
                            double PLLabourCost = Convert.ToDouble(tb_PL_LabourCost.Text);


                            if (PLLabourCost < 0)
                            {
                                tb_PL_LabourCost.ForeColor = Color.Red;
                            }
                            else
                            {
                                tb_PL_LabourCost.ForeColor = Color.Black;
                            }

                            if ((PLLabourCost - Math.Truncate(PLLabourCost)) == .00)
                            {
                                tb_PL_LabourCost.Text = Convert.ToString(PLLabourCost);
                                tb_PL_LabourCost.Text = string.Format(modified, "{0:c0}", double.Parse(tb_PL_LabourCost.Text));
                            }
                            else
                            {
                                tb_PL_LabourCost.Text = Convert.ToString(PLLabourCost);
                                tb_PL_LabourCost.Text = string.Format(modified, "{0:c2}", double.Parse(tb_PL_LabourCost.Text));
                            }


                            SqlConnection con = new SqlConnection();
                            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                            con.Open();
                            string sqlStmt = "insert into COL (LabourType,LabourPrice,profitlossId,LabourPercentage) Values(@LabourType,@LabourPrice,@profitlossId,@LabourPercentage) ";
                            SqlCommand cmd;
                            cmd = new SqlCommand(sqlStmt, con);


                            cmd.Parameters.Add(new SqlParameter("@LabourType", SqlDbType.VarChar, 50));
                            cmd.Parameters.Add(new SqlParameter("@LabourPrice", SqlDbType.VarChar, 50));
                            cmd.Parameters.Add(new SqlParameter("@profitlossId", SqlDbType.VarChar, 50));
                            cmd.Parameters.Add(new SqlParameter("@LabourPercentage", SqlDbType.VarChar, 50));

                            cmd.Parameters["@LabourType"].Value = ddl_Labour_Item_Type.SelectedItem.Text;
                            cmd.Parameters["@LabourPrice"].Value = tb_PL_LabourCost.Text;
                            cmd.Parameters["@profitlossId"].Value = tb_PL_id.Text;

                            string LabourPrices = Convert.ToString(tb_PL_LabourCost.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                            double EditedLabourPrices = Convert.ToDouble(LabourPrices);

                            string GrossSales = Convert.ToString(tb_PL_GrossSales.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                            double EditedGrossSales = Convert.ToDouble(GrossSales);

                            double Percentage = (EditedLabourPrices / EditedGrossSales) * 100;

                            if ((Percentage - Math.Truncate(Percentage)) == .00)
                            {
                                tb_PL_LAbourPercentage.Text = Convert.ToString(Percentage / 100);
                                tb_PL_LAbourPercentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:P0}", double.Parse(tb_PL_LAbourPercentage.Text));
                            }
                            else
                            {
                                tb_PL_LAbourPercentage.Text = Convert.ToString(Percentage / 100);
                                tb_PL_LAbourPercentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:P2}", double.Parse(tb_PL_LAbourPercentage.Text));
                            }



                            cmd.Parameters["@LabourPercentage"].Value = tb_PL_LAbourPercentage.Text;

                            int i = cmd.ExecuteNonQuery();

                            if (i > 0)
                            {
                                string head = "Congratulations";
                                string headtext = "Cost of Labour Added Successfully.";
                                string headtype = "success";
                                string cancelmsg = "Congrats";
                                string cancelHead = "Cancelled";
                                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                                tb_PL_LabourCost.Text = "";
                                ddl_Labour_Item_Type.SelectedIndex = 0;
                                this.Reload();
                                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#COL').modal('show')", true);
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
                        else
                        {
                            string head = "Error";
                            string headtext = "Please enter all values in Integer/Double";
                            string headtype = "warning";
                            string cancelmsg = "Please enter all values in Integer";
                            string cancelHead = "Cancelled";
                            string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                        }
                    }
                }
            }

            protected void del_btn_labour_Click(object sender, EventArgs e)
            {

                string id = Convert.ToString((sender as LinkButton).CommandArgument);
                string SuccessMsg = "Cost of Labour with ID " + id + " is deleted Successfully :)";

                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; con.Open();

                string query = "DELETE FROM COL WHERE Id='" + id + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Congratulation', '" + SuccessMsg + "' , 'success')", true);
                    this.Reload();
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#COL').modal('show')", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Warning', 'Un-expected Error! Try Again :) ', 'warning')", true);
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#COL').modal('show')", true);
                }
            }

            protected void btn_Close_COL_Click(object sender, EventArgs e)
            {
                if (girdviewPLE.Rows.Count > 0)
                {

                    string id = tb_PL_id.Text;
                    string SuccessMsg = "All Data in Cost of Labour Deleted Successfully :)";

                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; con.Open();

                    string query = "DELETE FROM COL WHERE profitlossId='" + id + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Congratulation', '" + SuccessMsg + "' , 'success')", true);
                        this.Reload();
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#COL').modal('close')", true);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Warning', 'Un-expected Error! Try Again :) ', 'warning')", true);
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#COL').modal('close')", true);
                    }
                }
            }

            protected void btn_COL_Save_Click(object sender, EventArgs e)
            {
                values();
                Insurance();
                Payroll();
                if (girdviewPLE.Rows.Count > 0)
                {
                    string head = "Congratulations";
                    string headtext = "All Data Saved Successfully.";
                    string headtype = "success";
                    string cancelmsg = "Congrats";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#COL').modal('close')", true);
                    tb_PL_LAbourPercentage.Text = "";
                }

            }

            protected void btn_Add_labour_type_Click(object sender, EventArgs e)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                con.Open();
                string sqlStmt = "insert into LabourType (LabourType) Values(@LabourType) ";
                SqlCommand cmd;
                cmd = new SqlCommand(sqlStmt, con);


                cmd.Parameters.Add(new SqlParameter("@LabourType", SqlDbType.VarChar, 50));

                cmd.Parameters["@LabourType"].Value = tb_PL_LabourType.Text;

                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    string head = "Congratulations";
                    string headtext = "Labour Type Added Successfully.";
                    string headtype = "success";
                    string cancelmsg = "Congrats";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    tb_PL_ItemName.Text = "";
                    this.Reload();
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#COL').modal('show')", true);
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
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#COL').modal('show')", true);
                }
            }

            protected void girdviewPLE_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                girdviewPLE.PageIndex = e.NewPageIndex;
                this.Reload();
            }

            protected void girdviewPLE_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                var original = new CultureInfo("en-us");

                var modified = (CultureInfo)original.Clone();
                modified.NumberFormat.CurrencyNegativePattern = 1;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    string TotalItemPrice = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LabourPrice"));

                    string NumberPrice = Convert.ToString(TotalItemPrice.Replace(modified.NumberFormat.CurrencySymbol, ""));

                    totalamountLE += Convert.ToDouble(NumberPrice);

                    if (totalamountLE < 0)
                    {
                        tb_PL_ToatalLabour.ForeColor = Color.Red;
                    }
                    else
                    {
                        tb_PL_ToatalLabour.ForeColor = Color.Black;
                    }

                    if ((totalamountLE - Math.Truncate(totalamountLE)) == .00)
                    {
                        tb_PL_ToatalLabour.Text = Convert.ToString(totalamountLE);
                        tb_PL_ToatalLabour.Text = string.Format(modified, "{0:c0}", double.Parse(tb_PL_ToatalLabour.Text));
                    }
                    else
                    {
                        tb_PL_ToatalLabour.Text = Convert.ToString(totalamountLE);
                        tb_PL_ToatalLabour.Text = string.Format(modified, "{0:c2}", double.Parse(tb_PL_ToatalLabour.Text));
                    }

                    string TotalItemPercentage = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LabourPercentage"));

                    string NumberItemPercentage = Convert.ToString(TotalItemPercentage.Replace(CultureInfo.InvariantCulture.NumberFormat.PercentSymbol, ""));

                    totalpercentageLE += Convert.ToDouble(NumberItemPercentage);

                    if (totalpercentageLE < 0)
                    {
                        tb_PL_ToatalLabourPercentage.ForeColor = Color.Red;
                    }
                    else
                    {
                        tb_PL_ToatalLabourPercentage.ForeColor = Color.Black;
                    }

                    if ((totalpercentageLE - Math.Truncate(totalpercentageLE)) == .00)
                    {
                        tb_PL_ToatalLabourPercentage.Text = Convert.ToString(totalpercentageLE / 100);
                        tb_PL_ToatalLabourPercentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:P0}", double.Parse(tb_PL_ToatalLabourPercentage.Text));
                    }
                    else
                    {
                        tb_PL_ToatalLabourPercentage.Text = Convert.ToString(totalpercentageLE / 100);
                        tb_PL_ToatalLabourPercentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:P2}", double.Parse(tb_PL_ToatalLabourPercentage.Text));
                    }
                }

                string Totalcost = Convert.ToString(tb_PL_ToatalLabour.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));

                double totalCost = Convert.ToDouble(Totalcost);

                if (totalCost < 0)
                {
                    tb_PL_CostofLabour.ForeColor = Color.Red;
                }
                else
                {
                    tb_PL_CostofLabour.ForeColor = Color.Black;
                }

                if ((totalCost - Math.Truncate(totalCost)) == .00)
                {
                    tb_PL_CostofLabour.Text = Convert.ToString(totalCost);
                    tb_PL_CostofLabour.Text = string.Format(modified, "{0:c0}", double.Parse(tb_PL_CostofLabour.Text));
                }
                else
                {
                    tb_PL_CostofLabour.Text = Convert.ToString(totalCost);
                    tb_PL_CostofLabour.Text = string.Format(modified, "{0:c2}", double.Parse(tb_PL_CostofLabour.Text));
                }



                string TotalPer = Convert.ToString(tb_PL_ToatalLabourPercentage.Text.Replace(CultureInfo.InvariantCulture.NumberFormat.PercentSymbol, ""));

                double totalPer = Convert.ToDouble(TotalPer);

                if (totalPer < 0)
                {
                    tb_CostofLab_Per.ForeColor = Color.Red;
                }
                else
                {
                    tb_CostofLab_Per.ForeColor = Color.Black;
                }

                if ((totalPer - Math.Truncate(totalPer)) == .00)
                {
                    tb_CostofLab_Per.Text = Convert.ToString(totalPer / 100);
                    tb_CostofLab_Per.Text = string.Format(CultureInfo.InvariantCulture, "{0:p0}", double.Parse(tb_CostofLab_Per.Text));
                }
                else
                {
                    tb_CostofLab_Per.Text = Convert.ToString(totalPer / 100);
                    tb_CostofLab_Per.Text = string.Format(CultureInfo.InvariantCulture, "{0:p2}", double.Parse(tb_CostofLab_Per.Text));
                }
            }

            protected void btnValuesSave_Click(object sender, EventArgs e)
            {
                btn_COL_Save_Click(sender, e);
            }

            void values()
            {
                if (tbPayrolExpense.Text == "" || tbInsuranceExp.Text == "" || tbZ.Text == "" || tbCardFee.Text == "")
                {
                    string head = "Error";
                    string headtext = "Please enter all values by Clicking Add Values Button";
                    string headtype = "warning";
                    string cancelmsg = "Please enter all values";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    tb_PL_GrossSales.Text = "";
                }
                else
                {
                    double InsurancePercentage = Convert.ToDouble(tbInsuranceExp.Text);

                    if ((InsurancePercentage - Math.Truncate(InsurancePercentage)) == .00)
                    {
                        tbGetInsurance.Text = Convert.ToString(InsurancePercentage / 100);
                        tbGetInsurance.Text = string.Format(CultureInfo.InvariantCulture, "{0:P0}", double.Parse(tbGetInsurance.Text));
                    }
                    else
                    {
                        tbGetInsurance.Text = Convert.ToString(InsurancePercentage / 100);
                        tbGetInsurance.Text = string.Format(CultureInfo.InvariantCulture, "{0:P2}", double.Parse(tbGetInsurance.Text));
                    }

                    double PayrollPercentage = Convert.ToDouble(tbPayrolExpense.Text);

                    if ((PayrollPercentage - Math.Truncate(PayrollPercentage)) == .00)
                    {
                        tbGetPayrollTax.Text = Convert.ToString(PayrollPercentage / 100);
                        tbGetPayrollTax.Text = string.Format(CultureInfo.InvariantCulture, "{0:P0}", double.Parse(tbGetPayrollTax.Text));
                    }
                    else
                    {
                        tbGetPayrollTax.Text = Convert.ToString(PayrollPercentage / 100);
                        tbGetPayrollTax.Text = string.Format(CultureInfo.InvariantCulture, "{0:P2}", double.Parse(tbGetPayrollTax.Text));
                    }

                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#Values').modal('close')", true);
                }
            }

            void Insurance()
            {
                if (tbPayrolExpense.Text == "" || tbInsuranceExp.Text == "" || tbZ.Text == "" || tbCardFee.Text == "")
                {
                    string head = "Error";
                    string headtext = "Please enter all values by Clicking Add Values Button";
                    string headtype = "warning";
                    string cancelmsg = "Please enter all values";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    tb_PL_GrossSales.Text = "";
                }
                else if (tb_PL_GrossSales.Text == "")
                {
                    string head = "Error";
                    string headtext = "Please enter Gross Revenue";
                    string headtype = "warning";
                    string cancelmsg = "Please enter all values";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    tb_PL_GrossSales.Text = "";
                }
                else
                {
                    var original = new CultureInfo("en-us");

                    var modified = (CultureInfo)original.Clone();
                    modified.NumberFormat.CurrencyNegativePattern = 1;

                    string PercentageInsurance = Convert.ToString(tbGetInsurance.Text.Replace(CultureInfo.InvariantCulture.NumberFormat.PercentSymbol, ""));
                    double insurance = Convert.ToDouble(PercentageInsurance);

                    string TotalLabourCost = Convert.ToString(tb_PL_CostofLabour.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                    double LabourCost = Convert.ToDouble(TotalLabourCost);

                    string TotalGrossRevenue = Convert.ToString(tb_PLO_GrossSales.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                    double GrossRevenue = Convert.ToDouble(TotalGrossRevenue);


                    double result = (insurance * LabourCost) / 100;

                    if (result < 0)
                    {
                        tbInsurance.ForeColor = Color.Red;
                    }
                    else
                    {
                        tbInsurance.ForeColor = Color.Black;
                    }

                    if ((result - Math.Truncate(result)) == .00)
                    {
                        tbInsurance.Text = Convert.ToString(result);
                        tbInsurance.Text = string.Format(modified, "{0:c0}", double.Parse(tbInsurance.Text));
                    }
                    else
                    {
                        tbInsurance.Text = Convert.ToString(result);
                        tbInsurance.Text = string.Format(modified, "{0:c2}", double.Parse(tbInsurance.Text));
                    }

                    double Result = (result / GrossRevenue) * 100;

                    if (Result < 0)
                    {
                        tbInsurancePercentage.ForeColor = Color.Red;
                    }
                    else
                    {
                        tbInsurancePercentage.ForeColor = Color.Black;
                    }

                    if ((Result - Math.Truncate(Result)) == .00)
                    {
                        tbInsurancePercentage.Text = Convert.ToString(Result / 100);
                        tbInsurancePercentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p0}", double.Parse(tbInsurancePercentage.Text));
                    }
                    else
                    {
                        tbInsurancePercentage.Text = Convert.ToString(Result / 100);
                        tbInsurancePercentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p2}", double.Parse(tbInsurancePercentage.Text));
                    }
                }
            }

            void Payroll()
            {
                if (tbPayrolExpense.Text == "" || tbInsuranceExp.Text == "" || tbZ.Text == "" || tbCardFee.Text == "" || tb_PL_GrossSales.Text == "")
                {
                    string head = "Error";
                    string headtext = "Please enter all values by Clicking Add Values Button";
                    string headtype = "warning";
                    string cancelmsg = "Please enter all values";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    tb_PL_GrossSales.Text = "";
                }
                else if (tb_PL_GrossSales.Text == "")
                {
                    string head = "Error";
                    string headtext = "Please enter Gross Revenue";
                    string headtype = "warning";
                    string cancelmsg = "Please enter all values";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    tb_PL_GrossSales.Text = "";
                }
                else
                {
                    var original = new CultureInfo("en-us");

                    var modified = (CultureInfo)original.Clone();
                    modified.NumberFormat.CurrencyNegativePattern = 1;

                    string PercentagePayroll = Convert.ToString(tbGetPayrollTax.Text.Replace(CultureInfo.InvariantCulture.NumberFormat.PercentSymbol, ""));
                    double Payroll = Convert.ToDouble(PercentagePayroll);

                    string TotalLabourCost = Convert.ToString(tb_PL_CostofLabour.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                    double LabourCost = Convert.ToDouble(TotalLabourCost);

                    string TotalGrossRevenue = Convert.ToString(tb_PL_GrossSales.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                    double GrossRevenue = Convert.ToDouble(TotalGrossRevenue);

                    string Insurance = Convert.ToString(tbInsurance.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                    double NetInsurance = Convert.ToDouble(Insurance);

                    double result = (Payroll * LabourCost) / 100;

                    if (result < 0)
                    {
                        tbSetPayrollTax.ForeColor = Color.Red;
                    }
                    else
                    {
                        tbSetPayrollTax.ForeColor = Color.Black;
                    }

                    if ((result - Math.Truncate(result)) == .00)
                    {
                        tbSetPayrollTax.Text = Convert.ToString(result);
                        tbSetPayrollTax.Text = string.Format(modified, "{0:c0}", double.Parse(tbSetPayrollTax.Text));
                    }
                    else
                    {
                        tbSetPayrollTax.Text = Convert.ToString(result);
                        tbSetPayrollTax.Text = string.Format(modified, "{0:c2}", double.Parse(tbSetPayrollTax.Text));
                    }

                    double TotalLabour = result + LabourCost + NetInsurance;

                    if (TotalLabour < 0)
                    {
                        tb_PL_TotalLabour.ForeColor = Color.Red;
                    }
                    else
                    {
                        tb_PL_TotalLabour.ForeColor = Color.Black;
                    }

                    if ((TotalLabour - Math.Truncate(TotalLabour)) == .00)
                    {
                        tb_PL_TotalLabour.Text = Convert.ToString(TotalLabour);
                        tb_PL_TotalLabour.Text = string.Format(modified, "{0:c0}", double.Parse(tb_PL_TotalLabour.Text));
                    }
                    else
                    {
                        tb_PL_TotalLabour.Text = Convert.ToString(TotalLabour);
                        tb_PL_TotalLabour.Text = string.Format(modified, "{0:c2}", double.Parse(tb_PL_TotalLabour.Text));
                    }

                    double Result = (result / GrossRevenue) * 100;

                    if (Result < 0)
                    {
                        tbSetPayrollTaxPercentage.ForeColor = Color.Red;
                    }
                    else
                    {
                        tbSetPayrollTaxPercentage.ForeColor = Color.Black;
                    }

                    if ((Result - Math.Truncate(Result)) == .00)
                    {
                        tbSetPayrollTaxPercentage.Text = Convert.ToString(Result / 100);
                        tbSetPayrollTaxPercentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p0}", double.Parse(tbSetPayrollTaxPercentage.Text));
                    }
                    else
                    {
                        tbSetPayrollTaxPercentage.Text = Convert.ToString(Result / 100);
                        tbSetPayrollTaxPercentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p2}", double.Parse(tbSetPayrollTaxPercentage.Text));
                    }
                }
            }

            protected void btnDelOpEx_Click(object sender, EventArgs e)
            {
                string id = Convert.ToString((sender as LinkButton).CommandArgument);
                string SuccessMsg = "Opearing Expense with ID " + id + " is deleted Successfully :)";

                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; con.Open();

                string query = "DELETE FROM OpEx WHERE Id='" + id + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Congratulation', '" + SuccessMsg + "' , 'success')", true);
                    this.Reload();
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#OpEx').modal('show')", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Warning', 'Un-expected Error! Try Again :) ', 'warning')", true);
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#OpEx').modal('show')", true);
                }
            }

            protected void btnCloseOpEx_Click(object sender, EventArgs e)
            {
                if (GVOpEx.Rows.Count > 0)
                {

                    string id = tb_PL_id.Text;
                    string SuccessMsg = "All Data of Opearting Expense Deleted Successfully :)";

                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; con.Open();

                    string query = "DELETE FROM OpEx WHERE profitlossId='" + id + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Congratulation', '" + SuccessMsg + "' , 'success')", true);
                        this.Reload();
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#OpEx').modal('close')", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Warning', 'Un-expected Error! Try Again :) ', 'warning')", true);
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#OpEx').modal('close')", true);

                    }
                }
            }

            protected void btnSaveOpEx_Click(object sender, EventArgs e)
            {
                if (GVOpEx.Rows.Count > 0)
                {
                    string head = "Congratulations";
                    string headtext = "All Data of Opearting Expense Saved Successfully.";
                    string headtype = "success";
                    string cancelmsg = "Congrats";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#OpEx').modal('close')", true);
                    tbOpExPer.Text = "";

                    tbOpExfromGV.Text = tbOpExTotalCost.Text;
                    tbOpExfromGVPer.Text = tbOpExTotalCostPercentage.Text;

                    CardFee();

                    CashFlow();
                }
                else
                {
                    string head = "Error";
                    string headtext = "No Data Found! No Operation Performed";
                    string headtype = "error";
                    string cancelmsg = "No Operation Performed";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#OpEx').modal('close')", true);
                    tbOpExPer.Text = "";
                }
            }

            protected void btnOpExItemName_Click(object sender, EventArgs e)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                con.Open();
                string sqlStmt = "insert into OpExItemName (ItemName) Values(@ItemName) ";
                SqlCommand cmd;
                cmd = new SqlCommand(sqlStmt, con);


                cmd.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.VarChar, 50));

                cmd.Parameters["@ItemName"].Value = tbOpExItemName.Text;

                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    string head = "Congratulations";
                    string headtext = "ItemName Added Successfully.";
                    string headtype = "success";
                    string cancelmsg = "Congrats";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    tbOpExItemName.Text = "";
                    this.Reload();
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#OpEx').modal('show')", true);
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

            protected void tbOpEx_TextChanged(object sender, EventArgs e)
            {
                var original = new CultureInfo("en-us");

                var modified = (CultureInfo)original.Clone();
                modified.NumberFormat.CurrencyNegativePattern = 1;

                if (tb_PL_CashSales.Text == "" || tb_PL_GrossSales.Text == "")
                {
                    string head = "Error";
                    string headtext = "Please Enter All Values";
                    string headtype = "warning";
                    string cancelmsg = "Please Enter All Values";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                }
                else
                {

                    if (ddlOpExItemName.SelectedItem.Text == "Select Item Name")
                    {
                        string head = "Error";
                        string headtext = "Please Select Item Name";
                        string headtype = "warning";
                        string cancelmsg = "Please enter all Values";
                        string cancelHead = "Cancelled";
                        string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    }
                    else
                    {
                        if (CheckInt(tbOpEx.Text) == true)
                        {
                            double OpEx = Convert.ToDouble(tbOpEx.Text);


                            if (OpEx < 0)
                            {
                                tbOpEx.ForeColor = Color.Red;
                            }
                            else
                            {
                                tbOpEx.ForeColor = Color.Black;
                            }

                            if ((OpEx - Math.Truncate(OpEx)) == .00)
                            {
                                tbOpEx.Text = Convert.ToString(OpEx);
                                tbOpEx.Text = string.Format(modified, "{0:c0}", double.Parse(tbOpEx.Text));
                            }
                            else
                            {
                                tbOpEx.Text = Convert.ToString(OpEx);
                                tbOpEx.Text = string.Format(modified, "{0:c2}", double.Parse(tbOpEx.Text));
                            }


                            SqlConnection con = new SqlConnection();
                            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                            con.Open();
                            string sqlStmt = "insert into OpEx (ItemName,ItemPrice,profitlossId,ItemPercentage) Values(@ItemName,@ItemPrice,@profitlossId,@ItemPercentage) ";
                            SqlCommand cmd;
                            cmd = new SqlCommand(sqlStmt, con);

                            cmd.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.VarChar, 50));
                            cmd.Parameters.Add(new SqlParameter("@ItemPrice", SqlDbType.VarChar, 50));
                            cmd.Parameters.Add(new SqlParameter("@profitlossId", SqlDbType.VarChar, 50));
                            cmd.Parameters.Add(new SqlParameter("@ItemPercentage", SqlDbType.VarChar, 50));

                            cmd.Parameters["@ItemName"].Value = ddlOpExItemName.SelectedItem.Text;
                            cmd.Parameters["@ItemPrice"].Value = tbOpEx.Text;
                            cmd.Parameters["@profitlossId"].Value = tb_PL_id.Text;

                            string ItemPrices = Convert.ToString(tbOpEx.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                            double EditedItemPrices = Convert.ToDouble(ItemPrices);

                            string GrossSales = Convert.ToString(tb_PL_GrossSales.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                            double EditedGrossSales = Convert.ToDouble(GrossSales);

                            double Percentage = (EditedItemPrices / EditedGrossSales) * 100;

                            if ((Percentage - Math.Truncate(Percentage)) == .00)
                            {
                                tbOpExPer.Text = Convert.ToString(Percentage / 100);
                                tbOpExPer.Text = string.Format(CultureInfo.InvariantCulture, "{0:P0}", double.Parse(tbOpExPer.Text));
                            }
                            else
                            {
                                tbOpExPer.Text = Convert.ToString(Percentage / 100);
                                tbOpExPer.Text = string.Format(CultureInfo.InvariantCulture, "{0:P2}", double.Parse(tbOpExPer.Text));
                            }

                            cmd.Parameters["@ItemPercentage"].Value = tbOpExPer.Text;

                            int i = cmd.ExecuteNonQuery();

                            if (i > 0)
                            {
                                string head = "Congratulations";
                                string headtext = "Opearting Expense Added Successfully.";
                                string headtype = "success";
                                string cancelmsg = "Congrats";
                                string cancelHead = "Cancelled";
                                string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                                tbOpEx.Text = "";
                                ddlOpExItemName.SelectedIndex = -1;
                                this.Reload();
                                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#OpEx').modal('show')", true);
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
                        else
                        {
                            string head = "Error";
                            string headtext = "Please enter all values in Integer/Double";
                            string headtype = "warning";
                            string cancelmsg = "Please enter all values in Integer";
                            string cancelHead = "Cancelled";
                            string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                        }
                    }
                }
            }

            protected void GVOpEx_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                var original = new CultureInfo("en-us");

                var modified = (CultureInfo)original.Clone();
                modified.NumberFormat.CurrencyNegativePattern = 1;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    string TotalItemPrice = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "itemPrice"));

                    string NumberPrice = Convert.ToString(TotalItemPrice.Replace(modified.NumberFormat.CurrencySymbol, ""));

                    totalamuntOpEx += Convert.ToDouble(NumberPrice);

                    if (totalamuntOpEx < 0)
                    {
                        tbOpExTotalCost.ForeColor = Color.Red;
                    }
                    else
                    {
                        tbOpExTotalCost.ForeColor = Color.Black;
                    }

                    if ((totalamuntOpEx - Math.Truncate(totalamuntOpEx)) == .00)
                    {
                        tbOpExTotalCost.Text = Convert.ToString(totalamuntOpEx);
                        tbOpExTotalCost.Text = string.Format(modified, "{0:c0}", double.Parse(tbOpExTotalCost.Text));
                    }
                    else
                    {
                        tbOpExTotalCost.Text = Convert.ToString(totalamuntOpEx);
                        tbOpExTotalCost.Text = string.Format(modified, "{0:c2}", double.Parse(tbOpExTotalCost.Text));
                    }

                    string TotalItemPercentage = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ItemPercentage"));

                    string NumberItemPercentage = Convert.ToString(TotalItemPercentage.Replace(CultureInfo.InvariantCulture.NumberFormat.PercentSymbol, ""));

                    totalpercentageOpEx += Convert.ToDouble(NumberItemPercentage);

                    if (totalpercentageOpEx < 0)
                    {
                        tbOpExTotalCostPercentage.ForeColor = Color.Red;
                    }
                    else
                    {
                        tbOpExTotalCostPercentage.ForeColor = Color.Black;
                    }

                    if ((totalpercentageOpEx - Math.Truncate(totalpercentageOpEx)) == .00)
                    {
                        tbOpExTotalCostPercentage.Text = Convert.ToString(totalpercentageOpEx / 100);
                        tbOpExTotalCostPercentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:P0}", double.Parse(tbOpExTotalCostPercentage.Text));
                    }
                    else
                    {
                        tbOpExTotalCostPercentage.Text = Convert.ToString(totalpercentageOpEx / 100);
                        tbOpExTotalCostPercentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:P2}", double.Parse(tbOpExTotalCostPercentage.Text));
                    }
                }

                string Totalcost = Convert.ToString(tb_PL_TotalItemsCost.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));

                double totalCost = Convert.ToDouble(Totalcost);

                if (totalCost < 0)
                {
                    tb_PL_TotalCost.ForeColor = Color.Red;
                }
                else
                {
                    tb_PL_TotalCost.ForeColor = Color.Black;
                }

                if ((totalCost - Math.Truncate(totalCost)) == .00)
                {
                    tb_PL_TotalCost.Text = Convert.ToString(totalCost);
                    tb_PL_TotalCost.Text = string.Format(modified, "{0:c0}", double.Parse(tb_PL_TotalCost.Text));
                }
                else
                {
                    tb_PL_TotalCost.Text = Convert.ToString(totalCost);
                    tb_PL_TotalCost.Text = string.Format(modified, "{0:c2}", double.Parse(tb_PL_TotalCost.Text));
                }

                string TotalPer = Convert.ToString(tb_PL_TotalItemsCostpercentage.Text.Replace(CultureInfo.InvariantCulture.NumberFormat.PercentSymbol, ""));

                double totalPer = Convert.ToDouble(TotalPer);

                if (totalPer < 0)
                {
                    tb_PL_TotalCost_percentage.ForeColor = Color.Red;
                }
                else
                {
                    tb_PL_TotalCost_percentage.ForeColor = Color.Black;
                }

                if ((totalPer - Math.Truncate(totalPer)) == .00)
                {
                    tb_PL_TotalCost_percentage.Text = Convert.ToString(totalPer / 100);
                    tb_PL_TotalCost_percentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p0}", double.Parse(tb_PL_TotalCost_percentage.Text));
                }
                else
                {
                    tb_PL_TotalCost_percentage.Text = Convert.ToString(totalPer / 100);
                    tb_PL_TotalCost_percentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p2}", double.Parse(tb_PL_TotalCost_percentage.Text));
                }


                double EditedGrossSales;
                string GrossSales = Convert.ToString(tb_PL_GrossSales.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));

                if (GrossSales == "")
                {
                    EditedGrossSales = 0;
                }
                else
                {
                    EditedGrossSales = Convert.ToDouble(GrossSales);
                }

                double totalProfit = EditedGrossSales - totalamount;

                if (totalProfit < 0)
                {
                    tb_PL_TotalProfit.ForeColor = Color.Red;
                }
                else
                {
                    tb_PL_TotalProfit.ForeColor = Color.Black;
                }

                if ((totalProfit - Math.Truncate(totalProfit)) == .00)
                {
                    tb_PL_TotalProfit.Text = Convert.ToString(totalProfit);
                    tb_PL_TotalProfit.Text = string.Format(modified, "{0:c0}", double.Parse(tb_PL_TotalProfit.Text));
                }
                else
                {
                    tb_PL_TotalProfit.Text = Convert.ToString(totalProfit);
                    tb_PL_TotalProfit.Text = string.Format(modified, "{0:c2}", double.Parse(tb_PL_TotalProfit.Text));
                }

                string TotalGross = Convert.ToString(tb_PL_GrossSales.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                double totalproPer;
                if (tb_PL_GrossSales.Text == "")
                {
                    totalproPer = 0;
                }
                else
                {
                    totalproPer = Convert.ToDouble((totalProfit / Convert.ToDouble(TotalGross)) * 100);
                }


                if (totalproPer < 0)
                {
                    tb_PL_TotalProfit_Percentage.ForeColor = Color.Red;
                }
                else
                {
                    tb_PL_TotalProfit_Percentage.ForeColor = Color.Black;
                }

                if ((totalproPer - Math.Truncate(totalproPer)) == .00)
                {
                    tb_PL_TotalProfit_Percentage.Text = Convert.ToString(totalproPer / 100);
                    tb_PL_TotalProfit_Percentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p0}", double.Parse(tb_PL_TotalProfit_Percentage.Text));
                }
                else
                {
                    tb_PL_TotalProfit_Percentage.Text = Convert.ToString(totalproPer / 100);
                    tb_PL_TotalProfit_Percentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p2}", double.Parse(tb_PL_TotalProfit_Percentage.Text));
                }
            }

            protected void GVOpEx_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                GVOpEx.PageIndex = e.NewPageIndex;
                this.Reload();
            }

            void CardFee()
            {
                if (tbPayrolExpense.Text == "" || tbInsuranceExp.Text == "" || tbZ.Text == "" || tbCardFee.Text == "" || tb_PL_non_CashSales.Text == "")
                {
                    string head = "Error";
                    string headtext = "Please enter all values by Clicking Add Values Button";
                    string headtype = "warning";
                    string cancelmsg = "Please enter all values";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                    tb_PL_GrossSales.Text = "";
                }
                else if (tb_PL_non_CashSales.Text == "")
                {
                    string head = "Error";
                    string headtext = "Please enter Non Cash Sales";
                    string headtype = "warning";
                    string cancelmsg = "Please enter all values";
                    string cancelHead = "Cancelled";
                    string script = "<script type=\"text/javascript\"> confirmError('" + this + "', '" + head + "', '" + headtext + "' , '" + headtype + "', '" + cancelmsg + "', '" + cancelHead + "' ); </script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
                }
                else
                {
                    var original = new CultureInfo("en-us");

                    var modified = (CultureInfo)original.Clone();
                    modified.NumberFormat.CurrencyNegativePattern = 1;

                    string NonCashSales = Convert.ToString(tb_PL_non_CashSales.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                    double NonCashSale = Convert.ToDouble(NonCashSales);

                    double CardFee = Convert.ToDouble(tbCardFee.Text);

                    string GrossRev = Convert.ToString(tb_PL_GrossSales.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                    double GrossRevenue = Convert.ToDouble(GrossRev);

                    double result = (NonCashSale * CardFee) / 100;

                    if (result < 0)
                    {
                        tbCardProcess.ForeColor = Color.Red;
                    }
                    else
                    {
                        tbCardProcess.ForeColor = Color.Black;
                    }

                    if ((result - Math.Truncate(result)) == .00)
                    {
                        tbCardProcess.Text = Convert.ToString(result);
                        tbCardProcess.Text = string.Format(modified, "{0:c0}", double.Parse(tbCardProcess.Text));
                    }
                    else
                    {
                        tbCardProcess.Text = Convert.ToString(result);
                        tbCardProcess.Text = string.Format(modified, "{0:c2}", double.Parse(tbCardProcess.Text));
                    }

                    double cardfee = CardFee;

                    if (cardfee < 0)
                    {
                        tbCardProcessingFee.ForeColor = Color.Red;
                    }
                    else
                    {
                        tbCardProcessingFee.ForeColor = Color.Black;
                    }

                    if ((cardfee - Math.Truncate(cardfee)) == .00)
                    {
                        tbCardProcessingFee.Text = Convert.ToString(cardfee / 100);
                        tbCardProcessingFee.Text = string.Format(CultureInfo.InvariantCulture, "{0:P0}", double.Parse(tbCardProcessingFee.Text));
                    }
                    else
                    {
                        tbCardProcessingFee.Text = Convert.ToString(cardfee / 100);
                        tbCardProcessingFee.Text = string.Format(CultureInfo.InvariantCulture, "{0:P2}", double.Parse(tbCardProcessingFee.Text));
                    }

                    double OpExPer = (result / GrossRevenue) * 100;

                    if (OpExPer < 0)
                    {
                        tbCardProcessingFeePercentage.ForeColor = Color.Red;
                    }
                    else
                    {
                        tbCardProcessingFeePercentage.ForeColor = Color.Black;
                    }

                    if ((OpExPer - Math.Truncate(OpExPer)) == .00)
                    {
                        tbCardProcessingFeePercentage.Text = Convert.ToString(OpExPer / 100);
                        tbCardProcessingFeePercentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p0}", double.Parse(tbCardProcessingFeePercentage.Text));
                    }
                    else
                    {
                        tbCardProcessingFeePercentage.Text = Convert.ToString(OpExPer / 100);
                        tbCardProcessingFeePercentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p2}", double.Parse(tbCardProcessingFeePercentage.Text));
                    }

                    string OpExfromGV = Convert.ToString(tbOpExfromGV.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                    double opex = Convert.ToDouble(OpExfromGV);

                    string OpExfromGVPer = Convert.ToString(tbOpExfromGVPer.Text.Replace(CultureInfo.InstalledUICulture.NumberFormat.PercentSymbol, ""));
                    double opexper = Convert.ToDouble(OpExfromGVPer);

                    double TotalOpex = result + opex;
                    double TotalOpexPer = OpExPer + opexper;

                    if (TotalOpexPer < 0)
                    {
                        tbOpExTotalPer.ForeColor = Color.Red;
                    }
                    else
                    {
                        tbOpExTotalPer.ForeColor = Color.Black;
                    }

                    if ((TotalOpexPer - Math.Truncate(TotalOpexPer)) == .00)
                    {
                        tbOpExTotalPer.Text = Convert.ToString(TotalOpexPer / 100);
                        tbOpExTotalPer.Text = string.Format(CultureInfo.InvariantCulture, "{0:p0}", double.Parse(tbOpExTotalPer.Text));
                    }
                    else
                    {
                        tbOpExTotalPer.Text = Convert.ToString(TotalOpexPer / 100);
                        tbOpExTotalPer.Text = string.Format(CultureInfo.InvariantCulture, "{0:p2}", double.Parse(tbOpExTotalPer.Text));
                    }

                    if (TotalOpex < 0)
                    {
                        tbOpExTotal.ForeColor = Color.Red;
                    }
                    else
                    {
                        tbOpExTotal.ForeColor = Color.Black;
                    }

                    if ((TotalOpex - Math.Truncate(TotalOpex)) == .00)
                    {
                        tbOpExTotal.Text = Convert.ToString(TotalOpex);
                        tbOpExTotal.Text = string.Format(modified, "{0:c0}", double.Parse(tbOpExTotal.Text));
                    }
                    else
                    {
                        tbOpExTotal.Text = Convert.ToString(TotalOpex);
                        tbOpExTotal.Text = string.Format(modified, "{0:c2}", double.Parse(tbOpExTotal.Text));
                    }

                }
            }

            void CashFlow()
            {
                var original = new CultureInfo("en-us");

                var modified = (CultureInfo)original.Clone();
                modified.NumberFormat.CurrencyNegativePattern = 1;

                string TotalProfits = Convert.ToString(tb_PL_TotalProfit.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                double TotalProfit = Convert.ToDouble(TotalProfits);

                string Gross = Convert.ToString(tb_PL_GrossSales.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                double GrossRevenue = Convert.ToDouble(Gross);

                string TotalLabours = Convert.ToString(tb_PL_TotalLabour.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                double TotalLabour = Convert.ToDouble(TotalLabours);

                string TotalOpexs = Convert.ToString(tbOpExTotal.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));
                double TotalOpex = Convert.ToDouble(TotalOpexs);

                double Managed = TotalProfit - TotalLabour - TotalOpex;

                if (Managed < 0)
                {
                    tbManaged.ForeColor = Color.Red;
                }
                else
                {
                    tbManaged.ForeColor = Color.Black;
                }

                if ((Managed - Math.Truncate(Managed)) == .00)
                {
                    tbManaged.Text = Convert.ToString(Managed);
                    tbManaged.Text = string.Format(modified, "{0:c0}", double.Parse(tbManaged.Text));
                }
                else
                {
                    tbManaged.Text = Convert.ToString(Managed);
                    tbManaged.Text = string.Format(modified, "{0:c2}", double.Parse(tbManaged.Text));
                }

                double ManagedPer = (Managed / GrossRevenue) * 100;

                if (ManagedPer < 0)
                {
                    tbManagedPercentage.ForeColor = Color.Red;
                }
                else
                {
                    tbManagedPercentage.ForeColor = Color.Black;
                }

                if ((ManagedPer - Math.Truncate(ManagedPer)) == .00)
                {
                    tbManagedPercentage.Text = Convert.ToString(ManagedPer / 100);
                    tbManagedPercentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p0}", double.Parse(tbManagedPercentage.Text));
                }
                else
                {
                    tbManagedPercentage.Text = Convert.ToString(ManagedPer / 100);
                    tbManagedPercentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p2}", double.Parse(tbManagedPercentage.Text));
                }

                double Owner = Managed + double.Parse(tbZ.Text);

                if (Owner < 0)
                {
                    tbOwner.ForeColor = Color.Red;
                }
                else
                {
                    tbOwner.ForeColor = Color.Black;
                }

                if ((Owner - Math.Truncate(Owner)) == .00)
                {
                    tbOwner.Text = Convert.ToString(Owner);
                    tbOwner.Text = string.Format(modified, "{0:c0}", double.Parse(tbOwner.Text));
                }
                else
                {
                    tbOwner.Text = Convert.ToString(Owner);
                    tbOwner.Text = string.Format(modified, "{0:c2}", double.Parse(tbOwner.Text));
                }

                double ownerPer = (Owner / GrossRevenue) * 100;

                if (ownerPer < 0)
                {
                    tbOwnerPercentage.ForeColor = Color.Red;
                }
                else
                {
                    tbOwnerPercentage.ForeColor = Color.Black;
                }

                if ((ownerPer - Math.Truncate(ownerPer)) == .00)
                {
                    tbOwnerPercentage.Text = Convert.ToString(ownerPer / 100);
                    tbOwnerPercentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p0}", double.Parse(tbOwnerPercentage.Text));
                }
                else
                {
                    tbOwnerPercentage.Text = Convert.ToString(ownerPer / 100);
                    tbOwnerPercentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p2}", double.Parse(tbOwnerPercentage.Text));
                }

            }
        protected void Resetbn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfitLoss.aspx");
        }
    }
}