using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMS
{
    public partial class ViewProfitLoss : System.Web.UI.Page
    {
        string id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            string Query = "SELECT * FROM ProfitLoss";


            SqlCommand cmd = new SqlCommand(Query, con);

            SqlDataAdapter sqladp = new SqlDataAdapter(cmd);
            DataTable sqldatab = new DataTable();
            sqladp.Fill(sqldatab);
            ViewState["myViewState"] = sqldatab;

            girdviewPL.DataSource = sqldatab;

            girdviewPL.DataBind();

        }

        protected void girdview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            girdviewPL.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void view_btn_Click(object sender, EventArgs e)
        {
            string ID = Convert.ToString((sender as LinkButton).CommandArgument);
            Session["EditID"] = ID;
            Response.Redirect("EditProfiLoss.aspx");
        }

        protected void del_btn_Click(object sender, EventArgs e)
        {
            id = Convert.ToString((sender as LinkButton).CommandArgument);
            string SuccessMsg = "Profit & Loss Data with ID " + id + " is deleted Successfully :)";

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; con.Open();

            string query = "DELETE FROM ProfitLoss WHERE Id='" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Congratulation', '" + SuccessMsg + "' , 'success')", true);
                BindGrid();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Warning', 'Un-expected Error! Try Again :) ', 'warning')", true);
            }
        }

        protected void edit_btn_Click(object sender, EventArgs e)
        {
            string ID = Convert.ToString((sender as LinkButton).CommandArgument);
            Session["ViewID"] = ID;
            Response.Redirect("ViewProfitLossReport.aspx");
        }
    }
}