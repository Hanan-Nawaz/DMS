<%@ Page Title="" Language="C#" MasterPageFile="~/DMS.Master" AutoEventWireup="true" CodeBehind="ViewSalesVolumeReport.aspx.cs" Inherits="DMS.ViewSalesVolumeReport" %>

    <%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
         <title>View Sales Volume Data - DMS</title>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.min.js" integrity="sha512-MqEDqB7me8klOYxXXQlB4LaNf9V9S0+sG1i8LtPOYmHqICuEZ9ZLbyV3qIfADg2UJcLyCm4fawNiFvnYbcBJ1w==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.css" integrity="sha512-f8gN/IhfI+0E9Fc/LKtjVq4ywfhYAVeMGKsECzDUHcFJ5teVwvKTqizm+5a84FINhfrgdvjX8hEJbem2io1iTA==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.js" integrity="sha512-XVz1P4Cymt04puwm5OITPm5gylyyj5vkahvf64T8xlt/ybeTpz4oHqJVIeDtDoF5kSrXMOUmdYewE4JS/4RWAA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.min.css" integrity="sha512-hwwdtOTYkQwW2sedIsbuP1h0mWeJe/hFOfsvNKpRB3CkRxq8EW7QMheec1Sgd8prYxGm1OM9OZcGW7/GUud5Fw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="https://unpkg.com/alpinejs@3.10.3/dist/cdn.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="container-fluid">

        <div class="col-md-13">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col">
                            <center>
                                <label style="color: blue; font-size: 20px; user-select: none;"><i class="fas fa-comment-dollar"></i>View Sales Volume Data</label>
                            </center>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col">
                            <hr>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label>Annual Gross Revenue</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_SD_AGR" AutoPostBack="true" Enabled="false" TabIndex="1" runat="server" placeholder="Annual Gross Revenue" required="true"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-md-6">
                            <label>Daily Gross Revenue</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_SD_DGR" runat="server" Enabled="false" placeholder=""></asp:TextBox>
                            </div>
                        </div>
                        </div>
                    <div class="row">
                        <div class="col-md-6">
                            <label>Annual Opearting Days</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_SD_AOD" runat="server" Enabled="false" AutoPostBack="true" TabIndex="2" placeholder="Annual Opearting Days" required="true"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-md-6">
                            <label>Hourly Gross Revenue</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_SD_HGR" runat="server" Enabled="false"  placeholder=""></asp:TextBox>
                            </div>
                        </div>
                      
                    </div>

                    <div class="row">

                         <div class="col-md-6">
                            <label>Daily Opearting Hours</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_SD_DOH" runat="server" Enabled="false" AutoPostBack="true" TabIndex="3" placeholder="Daily Opearting Hours" required="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Hourly Sales Order</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_SD_HSO" runat="server" Enabled="false" placeholder=""></asp:TextBox>
                            </div>
                        </div>
                     
                       
                    </div>

                     <div class="row">
                           <div class="col-md-6">
                            <label>Recipt</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_SD_ASr" runat="server" Enabled="false" AutoPostBack="true" TabIndex="4" placeholder="Average Sales Recipt" required="true"></asp:TextBox>
                            </div>
                        </div>
                       <div class="col-md-6">
                            <label>Daily Sales Order</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_SD_DSO" runat="server" Enabled="false"  placeholder=""></asp:TextBox>
                            </div>
                        </div>
                        
                    </div>

                    <div class="row">
                          <div class="col-md-6">
                            <label>Sales Data Date</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_SD_date" TextMode="Date" Enabled="false" runat="server" TabIndex="5" placeholder="" required="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Annual Sales Order</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_SD_ASO" runat="server" Enabled="false" placeholder=""></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row mt-2">
                        <label class="font-weight-bold text-primary">Notes</label>
                        <div class="col-md">
                            <div class="form-outline">
                                <asp:TextBox id="ta_SD_Notes" TabIndex="6" Enabled="false" Width="100%" class="form-control border border-1 border-primary" TextMode="multiline" Columns="50" Rows="5" runat="server" />
                            </div>
                        </div>
                    </div>

                    <div class="row mt-3 mb-5">
                        <div class="col-md">
                            <rsweb:ReportViewer ID="ReportViewerSD" KeepSessionAlive="true" Height="100%" ZoomMode="PageWidth" Width="100%" runat="server"></rsweb:ReportViewer>
                            </div>
                        </div>
                </div>
            </div>
        </div>
</div>
</asp:Content>
