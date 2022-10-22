<%@ Page Title="" Language="C#" MasterPageFile="~/DMS.Master" AutoEventWireup="true" CodeBehind="ViewProfitLossReport.aspx.cs" Inherits="DMS.ViewProfitLossReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>View Profit Loss Report - DMS</title>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.min.js" integrity="sha512-MqEDqB7me8klOYxXXQlB4LaNf9V9S0+sG1i8LtPOYmHqICuEZ9ZLbyV3qIfADg2UJcLyCm4fawNiFvnYbcBJ1w==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.css" integrity="sha512-f8gN/IhfI+0E9Fc/LKtjVq4ywfhYAVeMGKsECzDUHcFJ5teVwvKTqizm+5a84FINhfrgdvjX8hEJbem2io1iTA==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.js" integrity="sha512-XVz1P4Cymt04puwm5OITPm5gylyyj5vkahvf64T8xlt/ybeTpz4oHqJVIeDtDoF5kSrXMOUmdYewE4JS/4RWAA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.min.css" integrity="sha512-hwwdtOTYkQwW2sedIsbuP1h0mWeJe/hFOfsvNKpRB3CkRxq8EW7QMheec1Sgd8prYxGm1OM9OZcGW7/GUud5Fw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="https://unpkg.com/alpinejs@3.10.3/dist/cdn.min.js"></script>

    <link rel="stylesheet" href="CSS/girdview.css" type="text/css" />

    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.min.js" integrity="sha512-MqEDqB7me8klOYxXXQlB4LaNf9V9S0+sG1i8LtPOYmHqICuEZ9ZLbyV3qIfADg2UJcLyCm4fawNiFvnYbcBJ1w==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.css" integrity="sha512-f8gN/IhfI+0E9Fc/LKtjVq4ywfhYAVeMGKsECzDUHcFJ5teVwvKTqizm+5a84FINhfrgdvjX8hEJbem2io1iTA==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.js" integrity="sha512-XVz1P4Cymt04puwm5OITPm5gylyyj5vkahvf64T8xlt/ybeTpz4oHqJVIeDtDoF5kSrXMOUmdYewE4JS/4RWAA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.min.css" integrity="sha512-hwwdtOTYkQwW2sedIsbuP1h0mWeJe/hFOfsvNKpRB3CkRxq8EW7QMheec1Sgd8prYxGm1OM9OZcGW7/GUud5Fw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="https://unpkg.com/alpinejs@3.10.3/dist/cdn.min.js"></script>


    <style>
        .header {
            background-color: blue;
            color: White;
            height: 18px;
            text-align: center;
            border: 2px solid white;
            font-size: 16px;
        }


        .rows {
            background-color: #fff;
            font-family: Arial;
            font-size: 14px;
            color: #000;
            min-height: 20px;
            text-align: center;
        }

        .mydatagrid td {
            padding: 5px;
            border: solid 1px Blue;
        }

        .mydatagrid th {
            padding: 5px;
            border: solid 1px white;
        }
    </style>

    <script>

        var obj = { status: false, ele: null };

        function CheckError(stat, text) {

            if (obj.status) {
                return true;
            }


            swal({
                title: "Warning",
                text: text,
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
                        swal("Cancelled", "Process cancelled :)", "error");
                    }
                });

            return false;
        };
    </script>

    <script>

        var obj = { status: false, ele: null };

        function VerifyError(stat) {

            if (obj.status) {
                return true;
            }

            var rowCount = 0;
            var gridView = document.getElementById("<%=girdviewPL.ClientID %>");
            var rows = gridView.getElementsByTagName("tr")
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].getElementsByTagName("td").length > 0) {
                    rowCount++;
                }
            }

            if (rowCount > 1) {

                swal({
                    title: "Warning",
                    text: "By closing You lost all Data you Entered. Are you Sure you want to close this PopUP? OtherWise Save it.",
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
                            swal("Cancelled", "Process cancelled :)", "error");
                        }
                    });
            }
            else {


                obj.status = true;
                obj.ele = stat;
                obj.ele.click();

            }

            var rowCountLE = 0;
            var gridViewLE = document.getElementById("<%=girdviewPLE.ClientID %>");
            var rowsLE = gridViewLE.getElementsByTagName("tr")
            for (var j = 0; j < rowsLE.length; j++) {
                if (rowsLE[j].getElementsByTagName("td").length > 0) {
                    rowCountLE++;
                }
            }

            if (rowCountLE > 1) {

                swal({
                    title: "Warning",
                    text: "By closing You lost all Data you Entered. Are you Sure you want to close this PopUP? OtherWise Save it.",
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
                            swal("Cancelled", "Process cancelled :)", "error");
                        }
                    });
            }
            else {


                obj.status = true;
                obj.ele = stat;
                obj.ele.click();

            }

            var rowCountOpEx = 0;
            var gridViewOpEx = document.getElementById("<%=girdviewPLE.ClientID %>");
            var rowsOpEx = gridViewOpEx.getElementsByTagName("tr")
            for (var k = 0; k < rowsOpEx.length; k++) {
                if (rowsOpEx[k].getElementsByTagName("td").length > 0) {
                    rowCountOpEx++;
                }
            }

            if (rowCountOpEx > 1) {

                swal({
                    title: "Warning",
                    text: "By closing You lost all Data you Entered. Are you Sure you want to close this PopUP? OtherWise Save it.",
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
                            swal("Cancelled", "Process cancelled :)", "error");
                        }
                    });
            }
            else {


                obj.status = true;
                obj.ele = stat;
                obj.ele.click();

            }



            return false;
        };
    </script>

    <script>

        var obj = { status: false, ele: null };

        function VerifyErrorLE(stat) {

            if (obj.status) {
                return true;
            }

            var rowCountLE = 0;
            var gridViewLE = document.getElementById("<%=girdviewPLE.ClientID %>");
            var rowsLE = gridViewLE.getElementsByTagName("tr")
            for (var j = 0; j < rowsLE.length; j++) {
                if (rowsLE[j].getElementsByTagName("td").length > 0) {
                    rowCountLE++;
                }
            }

            if (rowCountLE > 1) {

                swal({
                    title: "Warning",
                    text: "By closing You lost all Data you Entered. Are you Sure you want to close this PopUP? OtherWise Save it.",
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
                            swal("Cancelled", "Process cancelled :)", "error");
                        }
                    });
            }
            else {


                obj.status = true;
                obj.ele = stat;
                obj.ele.click();

            }



            return false;
        };
    </script>

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
                                <label style="color: blue; font-size: 20px; user-select: none;"><i class="fas fa-money-bill-alt"></i>Profit and Loss</label>
                            </center>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col">
                            <hr>
                        </div>
                    </div>

                    <div class="row mt-2">
                        <div class="col-md-11">
                           
                        </div>
                        
                      
                    </div>

                    <div class="row">
                        <label class="font-weight-bold">Sales Revenue</label>

                        <div class="col-md-6">
                            <label>Model</label>
                            <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList CssClass="form-control" ID="ddl_PL_Model" TabIndex="1" runat="server" AutoPostBack="true" Enabled="false">
                                            <asp:ListItem Value="0">Select Model</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                       
                        <div class="col-md-6">
                            <label>Gross Sales</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_GrossSales" AutoPostBack="true" runat="server" TabIndex="2" placeholder="Gross Sales" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-md-6">
                            <label>Cash Sales</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_CashSales" runat="server" AutoPostBack="true" TabIndex="3" placeholder="Cash Sales" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label>Cash Sales Percentage</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_CashSales_perctange" runat="server" ReadOnly="true" placeholder="Cash Sales Perctange"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">


                        <div class="col-md-6">
                            <label>Non Cash Sales</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_non_CashSales" runat="server" AutoPostBack="true" TabIndex="4" placeholder="Non Cash Sales" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Non Cash Sales Percentage</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_nonCashSales_Perctange" runat="server" ReadOnly="true" placeholder="Non Cash Sales Perctange"></asp:TextBox>
                            </div>
                        </div>


                    </div>



                    <div class="row">
                        <div class="col-md-6">
                            <label>Total Gross Sales</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PLO_GrossSales" runat="server" placeholder="" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label>Total Gross Sales Percantage</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_GrossSales_Percentage" runat="server" placeholder="" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row border border-bottom-1 border-primary">
                    </div>

                    <div class="row mt-2">
                        <div class="col-md-6">
                            <label>Data Date</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_Date" runat="server" TabIndex="5" placeholder="" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                       <div class="col-md-6">
                            <label>Saved Date</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbSaveDtae" runat="server" TabIndex="5" placeholder="" ReadOnly="true"></asp:TextBox>
                             <asp:TextBox CssClass="form-control" Visible="false" ID="tb_PL_id" runat="server" ReadOnly="true"></asp:TextBox>

                            </div>
                        </div>
                      
                    </div>

                    <div class="row border border-bottom-1 border-primary">
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label>Payroll Tax</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbPayrolExpense" ReadOnly="true" runat="server" placeholder="Payroll Tax"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Insurance </label>
                            <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                <asp:TextBox CssClass="form-control" ID="tbInsuranceExp" ReadOnly="true" runat="server" placeholder="Insurance"></asp:TextBox>
                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <label>Card Fee</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbCardFee" ReadOnly="true" runat="server" placeholder="Card Fee"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Operator</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbZ" ReadOnly="true" runat="server" placeholder="Operator"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                     <div class="row border border-bottom-1 border-primary">
                    </div>
                    <div class="row mt-3">
                        <label class="font-weight-bold">Cost of Goods Sold</label>
                        
                        <div class="col-md-6">
                            <label>Total Cost</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_TotalCost" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>


                    </div>

                     <div class="row m-1 border border-bottom-0 border-left-0 border-right-0 border-secondary">
                        <center>

                            <div class="col-md-12 mt-3">
                                <div class="form-group">
                                    <div style="overflow: auto; width: 100%;">
                                        <asp:GridView ID="girdviewPL" runat="server"  AutoGenerateColumns="false" Width="100%" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" ShowFooter="true" FooterStyle-CssClass="mydatagrid_footer" CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AllowPaging="True" PageSize="10" OnPageIndexChanging="girdview_PageIndexChanging" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" EmptyDataText="No records has been added.">
                                            <Columns>
                                                <asp:BoundField HeaderText="Item Name" DataField="itemName"></asp:BoundField>
                                                <asp:BoundField HeaderText="Item Price" DataField="itemPrice"></asp:BoundField>
                                                <asp:BoundField HeaderText="Item Price Percentage" DataField="ItemPercentage"></asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </center>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label>Total Cost Percentage</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_TotalCost_percentage" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label>Total Profit</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_TotalProfit" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>


                    </div>



                    <div class="row">
                        <div class="col-md-6 ">
                            <label>Total Profit Percentage</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_TotalProfit_Percentage" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row border border-bottom-1 border-primary">
                    </div>

                    <div class="row mt-3">
                        <label class="font-weight-bold">Labour Expense</label>
                        <div class="col-md-5">
                            <label>Model</label>
                            <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList CssClass="form-control" Enabled="false" ID="ddl_PL_LabourModel" TabIndex="1" runat="server" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select Labour Model</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                       

                        



                    </div>

                   

                      <div class="row m-1 border border-bottom-0 border-left-0 border-right-0 border-secondary">
                        <center>

                            <div class="col-md-12 mt-3">
                                <div class="form-group">
                                    <div style="overflow: auto; width: 100%;">
                                        <asp:GridView ID="girdviewPLE" runat="server" AutoGenerateColumns="false" Width="100%" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" ShowFooter="true" FooterStyle-CssClass="mydatagrid_footer" CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AllowPaging="True" PageSize="10" OnPageIndexChanging="girdviewPLE_PageIndexChanging" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" EmptyDataText="No records has been added.">
                                            <Columns>
                                                <asp:BoundField HeaderText="Labour Type" DataField="LabourType"></asp:BoundField>
                                                <asp:BoundField HeaderText="Labour Price" DataField="LabourPrice"></asp:BoundField>
                                                <asp:BoundField HeaderText="Labour Price Percentage" DataField="LabourPercentage"></asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                       
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </center>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label>Cost of Labour</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_CostofLabour" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label>Cost of Labour Percentage</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_CostofLab_Per" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label>Total Labour</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_TotalLabour" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row border border-bottom-1 border-primary">
                    </div>



                    <div class="row mt-3">
                        <label class="font-weight-bold">Insurance</label>
                        <div class="col-md-6">
                            <label>Insurance</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbInsurance" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Insurance Percentage</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbInsurancePercentage" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>


                    </div>

                    <div class="row mt-3">

                        <div class="col-md-6">
                            <label>Add Insurance</label>
                            <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox CssClass="form-control" AutoPostBack="true" ID="tbGetInsurance" runat="server" ReadOnly="true"></asp:TextBox>
                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>

                    <div class="row border border-bottom-1 border-primary">
                    </div>

                    <div class="row mt-3">
                        <label class="font-weight-bold">Payroll Tax</label>
                        <div class="col-md-6">
                            <label>Payroll Tax</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbSetPayrollTax" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-md-6">
                            <label>Payroll Tax Percentage</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbSetPayrollTaxPercentage" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Payroll Tax %</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbGetPayrollTax" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row border border-bottom-1 border-primary">
                    </div>

                     <div class="row mt-3">
                        <label class="font-weight-bold">Operating Expense</label>
                      
                     </div>
                    <div class="row m-1 border border-bottom-0 border-left-0 border-right-0 border-secondary">
                        <center>

                            <div class="col-md-12 mt-3">
                                <div class="form-group">
                                    <div style="overflow: auto; width: 100%;">
                                        <asp:GridView ID="GVOpEx" runat="server" AutoGenerateColumns="false" Width="100%" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" ShowFooter="true" FooterStyle-CssClass="mydatagrid_footer" CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AllowPaging="True" PageSize="10" OnPageIndexChanging="GVOpEx_PageIndexChanging" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" EmptyDataText="No records has been added.">
                                            <Columns>
                                                <asp:BoundField HeaderText="Item Name" DataField="itemName"></asp:BoundField>
                                                <asp:BoundField HeaderText="Item Price" DataField="itemPrice"></asp:BoundField>
                                                <asp:BoundField HeaderText="Item Price Percentage" DataField="ItemPercentage"></asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                     
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </center>
                    </div>
                     
                    <div class="row">
                         <div class="col-md-6">
                            <label>Opearting Expense</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbOpExfromGV" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Opearting Expense Percentage</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbOpExfromGVPer" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row border border-bottom-1 border-primary">
                    </div>

                    <div class="row mt-3">
                        <label class="font-weight-bold">Card Processing Fee</label>
                       
                        <div class="col-md-6">
                            <label>Card Fee</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbCardProcessingFee" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                         <div class="col-md-6">
                            <label>Card Processing Fee</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbCardProcess" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-md-6">
                            <label>Card Processing Fee Percentage</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbCardProcessingFeePercentage" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        
                    </div>

                     <div class="row border border-bottom-1 border-primary">
                    </div>

                     <div class="row">
                        <div class="col-md-6">
                            <label>Total Opearting Expense </label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbOpExTotal" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-md-6">
                            <label>Total Opearting Expense Percentage</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbOpExTotalPer" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        
                    </div>

                     <div class="row border border-bottom-1 border-primary">
                    </div>

                    <div class="row">
                        <label class="font-weight-bold">Cash Flow (EBITDA)</label>
                         <div class="col-md-6">
                            <label>Managed</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbManaged" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-md-6">
                            <label>Managed Percentage</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbManagedPercentage" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        
                    </div>

                    <div class="row">
                         <div class="col-md-6">
                            <label>Owner </label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbOwner" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-md-6">
                            <label>Owner Percentage</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tbOwnerPercentage" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        
                    </div>

                   

                    <div class="row border border-bottom-1 border-primary">
                    </div>


                    <div class="row mt-2">
                        <label class="font-weight-bold text-primary">Notes</label>
                        <div class="col-md">
                            <div class="form-outline">
                                <asp:TextBox ID="ta_LD_Notes" Width="100%" class="form-control border border-1 border-primary" TextMode="multiline" Columns="50" Rows="5" runat="server" />
                            </div>
                        </div>
                    </div>

                    <div class="row mt-5">
                        <div class="col-4 mx-auto">
                            <center>
                                <div class="form-group">
                                    <asp:Button class="btn btn-primary btn-block btn-lg" ID="btn_PL_Calculate"  runat="server" Text="Analyze" OnClick="btn_PL_Calculate_Click" />
                                </div>
                            </center>
                        </div>

                      

                      

                    </div>

                    

                </div>
            </div>
        </div>

    </div>
</asp:Content>
