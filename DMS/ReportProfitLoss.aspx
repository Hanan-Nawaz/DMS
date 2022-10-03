<%@ Page Title="" Language="C#" MasterPageFile="~/DMS.Master" AutoEventWireup="true" CodeBehind="ReportProfitLoss.aspx.cs" Inherits="DMS.ReportProfitLoss" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <title>Profit Loss - DMS</title>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="container-fluid">

        <div class="col-md-13">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col">
                            <center>
                                <label style="color: blue; font-size: 20px; user-select: none;"><i class="fas fa-money-bill-alt"></i>Report Profit and Loss</label>
                            </center>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col">
                            <hr>
                        </div>
                    </div>

                    <div class="row mt-3 mb-5">
                        <div class="col-md">
                            <rsweb:ReportViewer ID="ReportViewerPL" KeepSessionAlive="true" Height="100%" ZoomMode="PageWidth" Width="100%" runat="server"></rsweb:ReportViewer>
                            </div>
                        </div>

                </div>
            </div>
        </div>
    </div>


</asp:Content>
