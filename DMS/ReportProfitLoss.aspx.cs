using Microsoft.Reporting.WebForms;
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
    public partial class ReportProfitLoss : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Report();
            }
        }

        void Report()
        {
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportViewerPL.LocalReport.ReportPath = "ProfitLoss.rdlc";

            string DateofData = Session["Date"].ToString();
            string Model = Session["Model"].ToString();
            string GrossSales = Session["GrossSales"].ToString();
            string CashSales = Session["CashSales"].ToString();
            string CashSalesPercentage = Session["CashSalesPercentage"].ToString();
            string NonCashSales = Session["NonCashSales"].ToString();
            string NonCashSalesPercentage = Session["NonCashSalesPercentage"].ToString();
            string TotalSales = Session["TotalGrossSales"].ToString();
            string TotalSalesPercentage = Session["TotalGrossSalesPercentage"].ToString();
            string PayrolTax = Session["PayrolTax"].ToString();
            string CardFee = Session["CardFee"].ToString();
            string Operator = Session["Operator"].ToString();
            string Insurance = Session["Insurance"].ToString();
            string TotalCost = Session["TotalCost"].ToString();
            string TotalCostPer = Session["TotalCostPer"].ToString();
            string TotalProfit = Session["TotalProfit"].ToString();
            string TotalProfitPer = Session["TotalProfitPer"].ToString();
            string LEModel = Session["LEModel"].ToString();
            string COL = Session["COL"].ToString();
            string COLPer = Session["COLPer"].ToString();
            string TotalLab = Session["TotalLab"].ToString();
            string InsSet = Session["InsSet"].ToString();
            string InsSetPer = Session["InsSetPer"].ToString();
            string InsGet = Session["InsGet"].ToString();
            string Payrol = Session["Payrol"].ToString();
            string payrolPer = Session["payrolPer"].ToString();
            string payrolget = Session["payrolget"].ToString();
            string OpEx = Session["OpEx"].ToString();
            string OpExPer = Session["OpExPer"].ToString();
            string CardFees = Session["CardFees"].ToString();
            string tbCardProcess = Session["tbCardProcess"].ToString();
            string tbCardProcessPer = Session["tbCardProcessPer"].ToString();
            string TotalOpEx = Session["TotalOpEx"].ToString();
            string TotalOpExPer = Session["TotalOpExPer"].ToString();
            string tbManaged = Session["tbManaged"].ToString();
            string tbManagedPercentage = Session["tbManagedPercentage"].ToString();
            string tbOwner = Session["tbOwner"].ToString();
            string tbOwnerPercentage = Session["tbOwnerPercentage"].ToString();
            string ta_LD_Notes = Session["ta_LD_Notes"].ToString();

            reportParameters.Add(new ReportParameter("DateofData", DateofData));
            reportParameters.Add(new ReportParameter("Model", Model));
            reportParameters.Add(new ReportParameter("GrossSales", GrossSales));
            reportParameters.Add(new ReportParameter("CashSales", CashSales));
            reportParameters.Add(new ReportParameter("CashSalesPercentage", CashSalesPercentage));
            reportParameters.Add(new ReportParameter("NonCashSales", NonCashSales));
            reportParameters.Add(new ReportParameter("NonCashSalesPercentage", NonCashSalesPercentage));
            reportParameters.Add(new ReportParameter("TotalSales", TotalSales));
            reportParameters.Add(new ReportParameter("TotalSalesPercentage", TotalSalesPercentage));
            reportParameters.Add(new ReportParameter("PayrolTax", PayrolTax));
            reportParameters.Add(new ReportParameter("CardFee", CardFee));
            reportParameters.Add(new ReportParameter("Operator", Operator));
            reportParameters.Add(new ReportParameter("Insurance", Insurance));
            reportParameters.Add(new ReportParameter("TotalCost", TotalCost));
            reportParameters.Add(new ReportParameter("TotalCostPer", TotalCostPer));
            reportParameters.Add(new ReportParameter("TotalProfit", TotalProfit));
            reportParameters.Add(new ReportParameter("TotalProfitPer", TotalProfitPer));
            reportParameters.Add(new ReportParameter("LEModel", LEModel));
            reportParameters.Add(new ReportParameter("COL", COL));
            reportParameters.Add(new ReportParameter("COLPer", COLPer));
            reportParameters.Add(new ReportParameter("TotalLab", TotalLab));
            reportParameters.Add(new ReportParameter("InsSet", InsSet));
            reportParameters.Add(new ReportParameter("InsSetPer", InsSetPer));
            reportParameters.Add(new ReportParameter("InsGet", InsGet));
            reportParameters.Add(new ReportParameter("Payrol", Payrol));
            reportParameters.Add(new ReportParameter("payrolPer", payrolPer));
            reportParameters.Add(new ReportParameter("payrolget", payrolget));
            reportParameters.Add(new ReportParameter("OpEx", OpEx));
            reportParameters.Add(new ReportParameter("OpExPer", OpExPer));
            reportParameters.Add(new ReportParameter("CardFees", CardFees));
            reportParameters.Add(new ReportParameter("tbCardProcess", tbCardProcess));
            reportParameters.Add(new ReportParameter("tbCardProcessPer", tbCardProcessPer));
            reportParameters.Add(new ReportParameter("TotalOpEx", TotalOpEx));
            reportParameters.Add(new ReportParameter("TotalOpExPer", TotalOpExPer));
            reportParameters.Add(new ReportParameter("tbManaged", tbManaged));
            reportParameters.Add(new ReportParameter("tbManagedPercentage", tbManagedPercentage));
            reportParameters.Add(new ReportParameter("tbOwner", tbOwner));
            reportParameters.Add(new ReportParameter("tbOwnerPercentage", tbOwnerPercentage));
            reportParameters.Add(new ReportParameter("ta_LD_Notes", ta_LD_Notes));

            string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlConnection con = new SqlConnection(conString);
            string query = "select * from COGS where profitlossId = '" + Session["IDPL"].ToString() + "'";
            SqlDataAdapter sqladpterLabourModel = new SqlDataAdapter(query, con);

            COGS cogs = new COGS();

            sqladpterLabourModel.Fill(cogs, cogs.Tables[0].TableName);

            ReportDataSource datasource = new ReportDataSource("COGSData", cogs.Tables[0]);
            ReportViewerPL.LocalReport.DataSources.Add(datasource);

            string queryCOL = "select * from COL where profitlossId = '" + Session["IDPL"].ToString() + "'";
            SqlDataAdapter sqladpterCOL = new SqlDataAdapter(queryCOL, con);

            COL col = new COL();

            sqladpterCOL.Fill(col, col.Tables[0].TableName);

            ReportDataSource datasourceCOL = new ReportDataSource("COLData", col.Tables[0]);
            ReportViewerPL.LocalReport.DataSources.Add(datasourceCOL);

            string queryOpEX = "select * from OpEX where profitlossId = '" + Session["IDPL"].ToString() + "'";
            SqlDataAdapter sqladpterOpEX = new SqlDataAdapter(queryOpEX, con);

            OpEx opex = new OpEx();

            sqladpterOpEX.Fill(opex, opex.Tables[0].TableName);

            ReportDataSource datasourceopex = new ReportDataSource("OpExData", opex.Tables[0]);
            ReportViewerPL.LocalReport.DataSources.Add(datasourceopex);
            ReportViewerPL.LocalReport.Refresh();

            ReportViewerPL.LocalReport.SetParameters(reportParameters);
        }

       
    }
}