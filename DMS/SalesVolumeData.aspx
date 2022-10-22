<%@ Page Title="" Language="C#" MasterPageFile="~/DMS.Master" AutoEventWireup="true" CodeBehind="SalesVolumeData.aspx.cs" Inherits="DMS.SalesVolumeData" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Sales Volume Data - DMS</title>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.min.js" integrity="sha512-MqEDqB7me8klOYxXXQlB4LaNf9V9S0+sG1i8LtPOYmHqICuEZ9ZLbyV3qIfADg2UJcLyCm4fawNiFvnYbcBJ1w==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.css" integrity="sha512-f8gN/IhfI+0E9Fc/LKtjVq4ywfhYAVeMGKsECzDUHcFJ5teVwvKTqizm+5a84FINhfrgdvjX8hEJbem2io1iTA==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.js" integrity="sha512-XVz1P4Cymt04puwm5OITPm5gylyyj5vkahvf64T8xlt/ybeTpz4oHqJVIeDtDoF5kSrXMOUmdYewE4JS/4RWAA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.min.css" integrity="sha512-hwwdtOTYkQwW2sedIsbuP1h0mWeJe/hFOfsvNKpRB3CkRxq8EW7QMheec1Sgd8prYxGm1OM9OZcGW7/GUud5Fw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="https://unpkg.com/alpinejs@3.10.3/dist/cdn.min.js"></script>


    <script>

        var obj = { status: false, ele: null };

        function confirmError(stat, heading, headingtext, headtype, cancelmsg, cancelhead) {

            if (obj.status) {
                return true;
            }

            swal({
                title: heading,
                text: headingtext,
                type: headtype,
                showCancelButton: true,
                closeOnConfirm: true,
                closeOnCancel: false,
                confirmButtonClass: "btn-danger",
            },
                function (isConfirm) {
                    if (isConfirm) {
                        // obj.status = true;
                        // obj.ele = stat;
                        // obj.ele.onclick();

                    }
                    else {



                        swal(cancelhead, cancelmsg, "error");


                    }
                });



            return false;
        };

        function Error(stat) {

            if (obj.status) {
                return true;
            }

            swal({
                title: "Warning",
                text: "Do you Want to Reset?",
                type: "warning",
                showCancelButton: true,
                closeOnConfirm: true,
                closeOnCancel: false,
                confirmButtonClass: "btn-danger",
            },
                function (isConfirm) {
                    if (isConfirm) {
                         obj.status = true;
                         obj.ele = stat;
                         obj.ele.click();

                    }
                    else {



                        swal("Cancelled", "Form not Reseted", "error");


                    }
                   
                });



            return false;
        };
    </script>

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
                                <label style="color: blue; font-size: 20px; user-select: none;"><i class="fas fa-comment-dollar"></i>Sales Volume Data</label>
                            </center>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col">
                            <hr>
                        </div>
                    </div>

                    <div class="row mt-2">
                        <div class="col-md-11"></div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton class="btn btn-danger text-white" runat="server" ID="Resetbn" OnClick="Resetbn_Click" OnClientClick="return Error(this);">Reset</asp:LinkButton>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label>Annual Gross Revenue</label>
                            <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox CssClass="form-control" ID="tb_SD_AGR" AutoPostBack="true" TabIndex="1" OnTextChanged="tb_SD_AGR_TextChanged" runat="server" placeholder="Annual Gross Revenue" required="true"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox CssClass="form-control" ID="tb_SD_AOD" runat="server" AutoPostBack="true" OnTextChanged="tb_SD_AOD_TextChanged" TabIndex="2" placeholder="Annual Opearting Days" required="true"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Hourly Gross Revenue</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_SD_HGR" runat="server" Enabled="false" placeholder=""></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-md-6">
                            <label>Daily Opearting Hours</label>
                            <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox CssClass="form-control" ID="tb_SD_DOH" runat="server" AutoPostBack="true" OnTextChanged="tb_SD_DOH_TextChanged" TabIndex="3" placeholder="Daily Opearting Hours" required="true"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox CssClass="form-control" ID="tb_SD_ASr" runat="server" OnTextChanged="tb_SD_ASr_TextChanged" AutoPostBack="true" TabIndex="4" placeholder="Average Sales Recipt" required="true"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Daily Sales Order</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_SD_DSO" runat="server" Enabled="false" placeholder=""></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label>Sales Data Date</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_SD_date" Enabled="false" runat="server" TabIndex="5" placeholder="" required="true"></asp:TextBox>
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
                                <asp:TextBox ID="ta_SD_Notes" TabIndex="6" class="form-control border border-1 border-primary" TextMode="multiline" Columns="50" Rows="5" runat="server" />
                            </div>
                        </div>
                    </div>

                    <div class="row mt-5">
                        <div class="col-4 mx-auto">
                            <center>
                                <div class="form-group">
                                    <asp:Button class="btn btn-primary btn-block btn-lg" ID="btn_SD_Calculate" runat="server" TabIndex="7" Text="Run" OnClick="btn_SD_Calculate_Click" />
                                </div>
                            </center>
                        </div>

                        <div class="col-4"></div>

                        <div class="col-4 mx-auto">
                            <center>
                                <div class="form-group">
                                    <asp:Button class="btn btn-primary btn-block btn-lg" ID="btn_Save_toDB" runat="server" TabIndex="8" Text="Save" OnClick="btn_Save_toDB_Click" />
                                </div>
                            </center>
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
