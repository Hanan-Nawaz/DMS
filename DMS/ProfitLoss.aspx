<%@ Page Title="" Language="C#" MasterPageFile="~/DMS.Master" AutoEventWireup="true" CodeBehind="ProfitLoss.aspx.cs" Inherits="DMS.ProfitLoss" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <title>Profit Loss - DMS</title>

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
                                <label style="color: blue; font-size: 20px; user-select: none;"><i class="fas fa-money-bill-alt"></i> Profit and Loss</label>
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
                                <a class="btn btn-danger text-white" runat="server" href="ProfitLoss.aspx">Reset</a>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <label class="font-weight-bold">Sales Revenue</label>
                         <div class="col-md-6">
                            <label>Id</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_id" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <label>Model</label>
                            <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList CssClass="form-control" ID="ddl_PL_Model" TabIndex="1" runat="server" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select Model</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel> 
                            </div>
                        </div>
                        <div class="col-md-1">
                            <label></label>
                                <div class="form-group">
                                <div class="form-group">
                                    <button type="button" class="btn btn-primary" style="margin-top: 8px;" data-toggle="modal" data-target="#Model">
                                        +
                                    </button>
                                </div>
                            </div>
                        </div>
                     
                    </div>

                      <div class="row">
                             <div class="col-md-6">
                            <label>Gross Sales</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_GrossSales" AutoPostBack="true" runat="server" TabIndex="2" placeholder="Gross Sales" OnTextChanged="tb_PL_GrossSales_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label> Cash Sales</label>
                           <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_CashSales" runat="server"  AutoPostBack="true" TabIndex="3" placeholder="Cash Sales" OnTextChanged="tb_PL_CashSales_TextChanged"></asp:TextBox>
                            </div>
                        </div>

                          
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label> Cash Sales Percentage</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_CashSales_perctange" runat="server" ReadOnly="true" placeholder="Cash Sales Perctange"></asp:TextBox>
                            </div>
                        </div>
                       
                        <div class="col-md-6">
                            <label> Non Cash Sales</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_non_CashSales" runat="server"  AutoPostBack="true" TabIndex="4" placeholder="Non Cash Sales" OnTextChanged="tb_PL_non_CashSales_TextChanged"></asp:TextBox>
                            </div>
                        </div>

                           
                        
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label> Non Cash Sales Percentage</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_nonCashSales_Perctange" runat="server" ReadOnly="true" placeholder="Non Cash Sales Perctange"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Data Date</label>
                           <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_Date" TextMode="Date" runat="server" TabIndex="5" placeholder=""></asp:TextBox>
                            </div>
                        </div>
                       

                    </div>

                    <div class="row">
                           <div class="col-md-6">
                            <label>Gross Sales</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PLO_GrossSales" runat="server" placeholder="" ReadOnly="true" ></asp:TextBox>
                            </div>
                        </div>

                         <div class="col-md-6">
                            <label>Gross Sales Percantage</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_GrossSales_Percentage" runat="server" placeholder="" ReadOnly="true" ></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row border border-bottom-1 border-primary">
                    </div>

                    <div class="row mt-3">
                        <label class="font-weight-bold">Cost of Goods Sold</label>
                        <div class="col-md-6">
                            <label></label>
                            <div class="form-group">
                                <div class="form-group">
                                    <button type="button" class="btn btn-primary" style="margin-top: 8px; width: 100%;" data-toggle="modal" data-target="#COGS">
                                        Add Cost of Goods Sold
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Total Cost</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_TotalCost" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        
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
                                        <asp:DropDownList CssClass="form-control" ID="ddl_PL_LabourModel" TabIndex="1" runat="server" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select Labour Model</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel> 
                            </div>
                        </div>
                        <div class="col-md-1">
                            <label></label>
                                <div class="form-group">
                                <div class="form-group">
                                    <button type="button" class="btn btn-primary" style="margin-top: 8px;" data-toggle="modal" data-target="#Model">
                                        +
                                    </button>
                                </div>
                            </div>
                        </div>

                         <div class="col-md-6">
                            <label></label>
                                <div class="form-group">
                                <div class="form-group">
                                    <button type="button" class="btn btn-primary" style="margin-top: 8px; width: 100%;" data-toggle="modal" data-target="#Model">
                                        Add Cost of Labour
                                    </button>
                                </div>
                            </div>
                        </div>
                     
                    </div>

                    <div class="row">
                         <div class="col-md-6">
                            <label>Cost of Labour</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_CostofLabour" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label>Total Labour</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_TotalLabour" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                         
                    </div>

                    <div class="row border border-bottom-1 border-primary">
                    </div>

                    <div class="row mt-2">
                        <label class="font-weight-bold text-primary">Notes</label>
                        <div class="col-md">
                            <div class="form-outline">
                                <asp:TextBox id="ta_LD_Notes" class="form-control border border-1 border-primary" TextMode="multiline" Columns="50" Rows="5" runat="server" />
                            </div>
                        </div>
                    </div>

                    <div class="row mt-5">
                        <div class="col-4 mx-auto">
                            <center>
                                <div class="form-group">
                                    <asp:Button class="btn btn-primary btn-block btn-lg" ID="btn_PL_Calculate" runat="server" Text="Analyze" OnClick="btn_PL_Calculate_Click" />
                                </div>
                            </center>
                        </div>

                        <div class="col-4"></div>

                        <div class="col-4 mx-auto">
                            <center>
                                <div class="form-group">
                                    <asp:Button class="btn btn-primary btn-block btn-lg" ID="btn_Save_toDB" runat="server" Text="Save to DB" OnClick="btn_Save_toDB_Click"  />
                                </div>
                            </center>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="Model" tabindex="1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header border-bottom-0">
                        <h5 class="modal-title" id="exampleModalLabel2">Add Model</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="row m-1">
                        <div class="col-md">
                            <label>Model</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_model" runat="server" placeholder="Model"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row m-1 border border-bottom-0 border-left-0 border-right-0 border-secondary">
                        <div class="col-md-6"></div>
                        <div class="col-md-2 mt-2">
                            <center>
                                <div class="form-group">
                                    <button type="button" class="btn btn-md" data-dismiss="modal" aria-label="Close">
                                        Close
                                    </button>
                                </div>
                            </center>
                        </div>
                        <div class="col-md-4 mt-2">
                            <center>
                                <div class="form-group">
                                    <asp:Button class="btn btn-primary btn-block btn-md" ID="btn_add_model" runat="server" Text="Add Model" OnClick="btn_add_model_Click" />
                                </div>
                            </center>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Add Cost of Goods Sold -->

          <div class="modal fade" id="COGS" tabindex="1" role="dialog" data-backdrop="static" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header border-bottom-0">
                        <h5 class="modal-title" id="exampleModalLabel3">Add Cost of Goods Sold</h5>
                    </div>
                    <div class="row m-1">
                        <div class="col-md-5">
                            <label>Item Name</label>
                           <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList CssClass="form-control" ID="ddl_PL_ItemName" TabIndex="1" runat="server" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select Item Name</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel> 
                            </div>
                        </div>
                        <div class="col-md-1">
                            <label></label>
                            <div class="form-group">
                                <div class="form-group">
                                    <button type="button" class="btn btn-primary" style="margin-top: 8px;" data-toggle="modal" data-target="#ItemName">
                                        +
                                    </button>
                                </div>
                            </div>
                        </div>
                         <div class="col-md-6">
                            <label>Item Cost</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_ItemPrice" runat="server" AutoPostBack="true" placeholder="Item Price" OnTextChanged="tb_PL_ItemPrice_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-0">
                           <!-- <label>Item Price Percentage</label> -->
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" Visible="false" ID="tb_Price_percentage" runat="server" Text="0" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row m-1 border border-bottom-0 border-left-0 border-right-0 border-secondary">
                        <center>

                            <div class="col-md-12 mt-3">
                                <div class="form-group">
                                    <div style="overflow: auto; width: 100%;">
                                        <asp:GridView ID="girdviewPL" runat="server" OnRowDataBound="girdviewPL_RowDataBound" AutoGenerateColumns="false" Width="100%" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" ShowFooter="true" FooterStyle-CssClass="mydatagrid_footer" CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AllowPaging="True" PageSize="10" OnPageIndexChanging="girdview_PageIndexChanging" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" EmptyDataText="No records has been added.">
                                            <Columns>
                                                <asp:BoundField HeaderText="Item Name" DataField="itemName"></asp:BoundField>
                                                <asp:BoundField HeaderText="Item Price" DataField="itemPrice"></asp:BoundField>
                                                <asp:BoundField HeaderText="Item Price Percentage" DataField="ItemPercentage"></asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" CssClass="btn btn-danger btn-sm" ID="del_btn" Width="100px" Height="30px" CommandArgument='<%# Eval("Id") %>' OnClientClick="return CheckError(this,Are you sure you want to delete?);" OnClick="del_btn_Click">
                                                               <i class="fas fa-trash"></i> Delete 
                                                        </asp:LinkButton>
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
                    
                    <div class="row m-1  border border-bottom-0 border-left-0 border-right-0 border-secondary">
                         <div class="col-md-6 mt-3">
                            <label>Total Item Cost</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_TotalItemsCost" runat="server" Text="0" ReadOnly="true" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6 mt-3">
                            <label>Total Item Price Percentage</label> 
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_TotalItemsCostpercentage" runat="server" Text="0" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row m-1  border border-bottom-0 border-left-0 border-right-0 border-secondary">
                        <div class="col-md-6"></div>
                        <div class="col-md-2 mt-3">
                            <center>
                                <div class="form-group">
                                    <asp:LinkButton runat="server" CssClass="btn btn-danger" ID="btn_del_all_COGS" OnClientClick="return VerifyError(this);" OnClick="btn_del_all_COGS_Click">
                                          Close 
                                     </asp:LinkButton>
                                </div>
                            </center>
                        </div>
                        <div class="col-md-4 mt-3">
                            <center>
                                <div class="form-group">
                                    <asp:Button class="btn btn-primary btn-block btn-md" ID="btn_save" runat="server" OnClientClick="return CheckError(this,Are you sure you want to Save?);" Text="Save" OnClick="btn_save_Click" />
                                </div>
                            </center>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Add Item Name -->

        <div class="modal fade" id="ItemName" tabindex="1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header border-bottom-0">
                        <h5 class="modal-title" id="exampleModalLabel5">Add Item Name</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="row m-1">
                        <div class="col-md">
                            <label>Item Name</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_PL_ItemName" runat="server" placeholder="Add Item Name"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row m-1 border border-bottom-0 border-left-0 border-right-0 border-secondary">
                        <div class="col-md-5"></div>
                        <div class="col-md-2 mt-2">
                            <center>
                                <div class="form-group">
                                    <button type="button" class="btn btn-md" data-dismiss="modal" aria-label="Close">
                                        Close
                                    </button>
                                </div>
                            </center>
                        </div>
                        <div class="col-md-5 mt-2">
                            <center>
                                <div class="form-group">
                                    <asp:Button class="btn btn-primary btn-block btn-md" ID="btn_add_ItemName" runat="server" Text="Add Item Name" OnClick="btn_add_ItemName_Click" />
                                </div>
                            </center>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
