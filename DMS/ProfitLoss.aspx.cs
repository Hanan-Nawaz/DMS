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
        double totalpercentage = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Reload();
                
                DateTime dateTime = DateTime.Today;
                string date = dateTime.ToString("yyyyMMdd");
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
            SqlCommand check_Id = new SqlCommand("SELECT * FROM profit_loss_tb WHERE ([tb_id] = @tb_id)", con);
            check_Id.Parameters.AddWithValue("@tb_id", PLID);
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



            string Query = "SELECT * FROM COGS where profitlossId = @profitlossId";

            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@profitlossId", tb_PL_id.Text);
            SqlDataAdapter sqladp = new SqlDataAdapter(cmd);
            DataTable sqldatab = new DataTable();
            sqladp.Fill(sqldatab);
            ViewState["myViewState"] = sqldatab;

            girdviewPL.DataSource = sqldatab;

            girdviewPL.DataBind();

        }

        protected void btn_PL_Calculate_Click(object sender, EventArgs e)
        {

        }

        protected void btn_Save_toDB_Click(object sender, EventArgs e)
        {

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

            if (ddl_PL_Model.SelectedItem.Text == "Select Model" || tb_PL_CashSales.Text == "" || tb_PL_GrossSales.Text == "" )
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

                        double TotalPercantage = ((PLNONCashSales + PLCashSales) / GrossSales) ;
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
                            tb_Price_percentage.Text = Convert.ToString(Percentage/100);
                            tb_Price_percentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:P0}", double.Parse(tb_Price_percentage.Text));
                        }
                        else
                        {
                            tb_Price_percentage.Text = Convert.ToString(Percentage/100);
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
            if(girdviewPL.Rows.Count > 0)
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

            if(girdviewPL.Rows.Count > 0)
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
                    tb_PL_TotalItemsCostpercentage.Text = Convert.ToString(totalpercentage/100);
                    tb_PL_TotalItemsCostpercentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:P0}", double.Parse(tb_PL_TotalItemsCostpercentage.Text));
                }
                else
                {
                    tb_PL_TotalItemsCostpercentage.Text = Convert.ToString(totalpercentage/100);
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
                tb_PL_TotalCost_percentage.Text = Convert.ToString(totalPer/100);
                tb_PL_TotalCost_percentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p0}", double.Parse(tb_PL_TotalCost_percentage.Text));
            }
            else
            {
                tb_PL_TotalCost_percentage.Text = Convert.ToString(totalPer/100);
                tb_PL_TotalCost_percentage.Text = string.Format(CultureInfo.InvariantCulture, "{0:p2}", double.Parse(tb_PL_TotalCost_percentage.Text));
            }


            double EditedGrossSales;
            string GrossSales = Convert.ToString(tb_PL_GrossSales.Text.Replace(modified.NumberFormat.CurrencySymbol, ""));

            if(GrossSales == "")
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
    }
}