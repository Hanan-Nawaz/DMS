<%@ Page Title="" Language="C#" MasterPageFile="~/DMS.Master" AutoEventWireup="true" CodeBehind="ViewAnnualGrossRevenue.aspx.cs" Inherits="DMS.ViewAnnualGrossRevenue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>View AGR - DMS</title>

     <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.min.js" integrity="sha512-MqEDqB7me8klOYxXXQlB4LaNf9V9S0+sG1i8LtPOYmHqICuEZ9ZLbyV3qIfADg2UJcLyCm4fawNiFvnYbcBJ1w==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.css" integrity="sha512-f8gN/IhfI+0E9Fc/LKtjVq4ywfhYAVeMGKsECzDUHcFJ5teVwvKTqizm+5a84FINhfrgdvjX8hEJbem2io1iTA==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.js" integrity="sha512-XVz1P4Cymt04puwm5OITPm5gylyyj5vkahvf64T8xlt/ybeTpz4oHqJVIeDtDoF5kSrXMOUmdYewE4JS/4RWAA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.min.css" integrity="sha512-hwwdtOTYkQwW2sedIsbuP1h0mWeJe/hFOfsvNKpRB3CkRxq8EW7QMheec1Sgd8prYxGm1OM9OZcGW7/GUud5Fw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="https://unpkg.com/alpinejs@3.10.3/dist/cdn.min.js"></script>

    <link rel="stylesheet" href="CSS/girdview.css" type="text/css" />

    <style>
        .header {
            background-color: blue;
            color: White;
            height: 18px;
            text-align: center;
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
                                <label style="color: blue; font-size: 20px; user-select: none;"><i class="fas fa-list fa-fw "></i>View Annual Gross Revenue </label>
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
                                    <asp:GridView ID="girdviewAGR" runat="server" AutoGenerateColumns="false" Width="100%" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" ShowFooter="true" FooterStyle-CssClass="mydatagrid_footer" CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AllowPaging="True" PageSize="10" OnPageIndexChanging="girdview_PageIndexChanging" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <asp:BoundField HeaderText="Annual Gross Revenue" DataField="annual_gross_revenue"></asp:BoundField>
                                            <asp:BoundField Visible="false" DataField="id"></asp:BoundField>
                                            <asp:TemplateField ItemStyle-Width="240">
                                                <ItemTemplate>
                                                         <asp:LinkButton runat="server" CssClass="btn btn-danger btn-sm" ID="del_btn" CommandArgument='<%# Eval("id") %>' OnClientClick="return confirmError(this);" OnClick="del_btn_Click"  >
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
                        </center>
                    </div>
                </div>  
            </div>
        </div>
    </div>
</asp:Content>
