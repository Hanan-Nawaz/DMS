<%@ Page Title="" Language="C#" MasterPageFile="~/DMS.Master" AutoEventWireup="true" CodeBehind="ViewSalesVolumeData.aspx.cs" Inherits="DMS.ViewSalesVolumeData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <title>View Sales Volume Data - DMS</title>
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

        function confirmError(stat) {

            if (obj.status) {
                return true;
            }


            swal({
                title: "Warning",
                text: "Are you sure you want to delete?",
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
                                <label style="color: blue; font-size: 20px; user-select: none;"><i class="fas fa-list fa-fw "></i>View Sales Volume Data </label>
                            </center>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col">
                            <hr>
                        </div>
                    </div>

                    <div class="row">
                        <center>

                            <div class="col-md-12">
                                <div class="form-group">
                                    <div style="overflow: auto; width: 100%;">
                                        <asp:GridView ID="girdviewLD" runat="server" AutoGenerateColumns="false" Width="100%" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" ShowFooter="true" FooterStyle-CssClass="mydatagrid_footer" CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AllowPaging="True" PageSize="10" OnPageIndexChanging="girdview_PageIndexChanging" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:BoundField HeaderText="ID" DataField="id" Visible="false"></asp:BoundField>
                                                <asp:BoundField HeaderText="Annual Gross Revenue" DataField="anum_gross_rev"></asp:BoundField>
                                                <asp:BoundField HeaderText="Annual Opearting Days" DataField="anum_op_days"></asp:BoundField>
                                                <asp:BoundField HeaderText="Daily Opearting Hours" DataField="daily_op_hrs"></asp:BoundField>
                                                <asp:BoundField HeaderText="Average Sales Recipt" DataField="avg_sale_recpt"></asp:BoundField>
                                                <asp:BoundField HeaderText="Daily Gross Revenue" DataField="daily_gross_rev"></asp:BoundField>
                                                <asp:BoundField HeaderText="Hourly Gross Revenue" DataField="hourly_gross_rev"></asp:BoundField>
                                                <asp:BoundField HeaderText="Hourly Sales Order" DataField="hourly_sale_ord"></asp:BoundField>
                                                <asp:BoundField HeaderText="Daily Sales Order" DataField="daily_sale_ord"></asp:BoundField>
                                                <asp:BoundField HeaderText="Annual Sales Order" DataField="anum_sale_ord"></asp:BoundField>
                                                <asp:BoundField HeaderText="Saved Date" DataField="saved_date"></asp:BoundField>
                                                <asp:BoundField HeaderText="Sales Data Date" DataField="sales_date"></asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div>
                                                              <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm" Width="100px" Height="30px" ID="view_btn" CommandArgument='<%# Eval("id") %>' OnClick="view_btn_Click">
                                                               <i class="fas fa-pencil"></i> Edit
                                                        </asp:LinkButton>
                                                            <asp:LinkButton runat="server" CssClass="btn btn-success btn-sm" ID="edit_btn" Width="100px" Height="30px" CommandArgument='<%# Eval("id") %>' OnClick="edit_btn_Click">
                                                               <i class="fas fa-eye"></i> View 
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" CssClass="btn btn-danger btn-sm" ID="del_btn" Width="100px" Height="30px" CommandArgument='<%# Eval("id") %>' OnClientClick="return confirmError(this);" OnClick="del_btn_Click">
                                                               <i class="fas fa-trash"></i> Delete 
                                                        </asp:LinkButton>
                                                        </div>
                                                      
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
                </div>
            </div>
        </div>
    </div>
</asp:Content>
