<%@ Page Title="" Language="C#" MasterPageFile="~/DMS.Master" AutoEventWireup="true" CodeBehind="ViewReports.aspx.cs" Inherits="DMS.ViewReports" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <title>view Reports P&L - DMS</title>
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
                                <label style="color: blue; font-size: 20px; user-select: none;"><i class="fas fa-book"></i>Reports Profit and Loss</label>
                            </center>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col">
                            <hr>
                        </div>
                    </div>

                    <div class="row mt-2">
                        <div class="col-md-5">
                            <label>Start Date</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbStart" TextMode="Date" runat="server" TabIndex="5" placeholder=""></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-5">
                           <label>End Date</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbEnd" TextMode="Date" runat="server" TabIndex="5" placeholder=""></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <label></label>
                            <div class="form-group">
                                <asp:LinkButton runat="server" class="btn btn-primary" ID="btnGenerateReport" style="margin-top: 8px;" OnClick="btnGenerateReport_Click">
                                    Search
                                </asp:LinkButton>
                            </div>
                        </div>
                      
                    </div>

                     <div class="row border border-bottom-1 border-primary">
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

