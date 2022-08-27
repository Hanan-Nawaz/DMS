﻿using System;
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
    public partial class ViewModel : System.Web.UI.Page
    {
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

            string Query = "SELECT * FROM model_dropdown";


            SqlCommand cmd = new SqlCommand(Query, con);

            SqlDataAdapter sqladp = new SqlDataAdapter(cmd);
            DataTable sqldatab = new DataTable();
            sqladp.Fill(sqldatab);
            ViewState["myViewState"] = sqldatab;

            girdviewModel.DataSource = sqldatab;

            girdviewModel.DataBind();

        }

        protected void girdview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            girdviewModel.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void del_btn_Click(object sender, EventArgs e)
        {
            string id = Convert.ToString((sender as LinkButton).CommandArgument);
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString; con.Open();

            string query = "DELETE FROM model_dropdown WHERE id='" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Congratulation', 'Annual Gross Revenue Deleted Successfully:)', 'success')", true);
                BindGrid();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Warning', 'Un-expected Error! Try Again :) ', 'warning')", true);
            }
        }
    }
}