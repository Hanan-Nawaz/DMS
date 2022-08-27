<%@ Page Title="" Language="C#" MasterPageFile="~/DMS.Master" AutoEventWireup="true" CodeBehind="EditLabourData.aspx.cs" Inherits="DMS.EditLabourData" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Edit LD - DMS</title>

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
                                <label style="color: blue; font-size: 20px; user-select: none;"><i class="fas fa-users"></i>Labour Data</label>
                            </center>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col">
                            <hr>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-5">
                            <label>Annual Gross Renvenue</label>
                            <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList CssClass="form-control" ID="ddl_LD_AGR" runat="server" TabIndex="1" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select Annual Gross Renvenue</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <label></label>
                            <div class="form-group">
                                <button type="button" class="btn btn-primary" style="margin-top: 8px;" data-toggle="modal" data-target="#form">
                                    +
                                </button>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Annual Operating Days</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LD_AOD" runat="server" TabIndex="2" TextMode="Number" placeholder="Annual Operating Days"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label>Daily Opearting Hours</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LD_DOH" runat="server" TabIndex="3" AutoPostBack="true" OnTextChanged="tb_LD_DOH_TextChanged" TextMode="Number" placeholder="Daily Opearting Hours"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Annual Opearting Hours</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LD_AOH" runat="server" Enabled="false" placeholder=""></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-5">
                            <label>Model</label>
                            <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList CssClass="form-control" ID="ddl_LD_Model" TabIndex="4" runat="server" AutoPostBack="true">
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
                        <div class="col-md-3">
                            <label>Labour Data Date</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LD_Date" TextMode="Date" runat="server" TabIndex="5" placeholder=""></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-md-3">
                            <label>Saved Date</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_saveDate" Enabled="false" runat="server" TabIndex="5" placeholder=""></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row border border-bottom-1 border-primary">
                    </div>

                    <div class="row mt-2">
                        <label class="font-weight-bold text-primary">GM Labour</label>

                        <div class="col-md-3">
                            <label>Number of Labour</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDGM_NoL" TabIndex="6" runat="server" placeholder="Number of Labour"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Daily Hours Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDGM_DOW" runat="server" TabIndex="7" placeholder="Daily Hours Worked"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Annual Days Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDGM_ADW" runat="server" TabIndex="8" placeholder="Annual Days Worked"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Hourly Wage $</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDGM_HW" runat="server" OnTextChanged="tb_LDGM_HW_TextChanged" AutoPostBack="true" TabIndex="9" placeholder="Hourly Wage $"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row border border-bottom-1 border-primary">
                    </div>

                    <div class="row mt-2">
                        <label class="font-weight-bold text-primary">AM Labour</label>

                        <div class="col-md-3">
                            <label>Number of Labour</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDAM_NoL" runat="server" TabIndex="10" placeholder="Number of Labour"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Daily Hours Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDAM_DOW" runat="server" TabIndex="11" placeholder="Daily Hours Worked"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Annual Days Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDAM_ADW" runat="server" TabIndex="12" placeholder="Annual Days Worked"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Hourly Wage $</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDAM_HW" OnTextChanged="tb_LDAM_HW_TextChanged" AutoPostBack="true" runat="server" TabIndex="13" placeholder="Hourly Wage $"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row border border-bottom-1 border-primary">
                    </div>

                    <div class="row mt-2">
                        <label class="font-weight-bold text-primary">CREW Labour</label>
                        <div class="col-md-3">
                            <label>Number of Labour</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDCREW_NoL" runat="server" TabIndex="14" placeholder="Number of Labour"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Daily Hours Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDCREW_DOW" runat="server" TabIndex="15" placeholder="Daily Hours Worked"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Annual Days Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDCREW_ADW" runat="server" TabIndex="16" placeholder="Annual Days Worked"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Hourly Wage $</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDCREW_HW" OnTextChanged="tb_LDCREW_HW_TextChanged" AutoPostBack="true" runat="server" TabIndex="17" placeholder="Hourly Wage $"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row border border-bottom-1 border-primary">
                    </div>

                    <div class="row mt-2">
                        <label class="font-weight-bold text-primary">Annual Wage</label>

                        <div class="col-md-3">
                            <label>GM Labour</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDGM_W" runat="server" Enabled="false" placeholder="GM Labour"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>AM Labour</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDAM_W" runat="server" Enabled="false" placeholder="AM Labour"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>CREW Labour</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDCREW_W" runat="server" Enabled="false" placeholder="CREW Labour"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Total Wages </label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LD_TW" runat="server" Enabled="false" placeholder="Total Wages"></asp:TextBox>
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
                                    <asp:Button class="btn btn-primary btn-block btn-lg" ID="btn_LD_Calculate" runat="server" TabIndex="19" Text="Analyze" OnClick="btn_LD_Calculate_Click" />
                                </div>
                            </center>
                        </div>

                        <div class="col-4"></div>

                        <div class="col-4 mx-auto">
                            <center>
                                <div class="form-group">
                                    <asp:Button class="btn btn-primary btn-block btn-lg" ID="btn_Save_toDB" runat="server" TabIndex="19" Text="Save to DB" OnClick="btn_Save_toDB_Click" />
                                </div>
                            </center>
                        </div>

                    </div>

                    <div class="row mt-3 mb-5">
                        <div class="col-md">
                            <rsweb:ReportViewer ID="ReportViewerLD" KeepSessionAlive="true" Height="100%" ZoomMode="PageWidth" Width="100%" runat="server"></rsweb:ReportViewer>
                            </div>
                        </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="form" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header border-bottom-0">
                        <h5 class="modal-title" id="exampleModalLabel">Add Annual Gross Revenue Range</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="row m-1">
                        <div class="col-md">
                            <label>Annual Gross Revenue</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_AGR" runat="server" placeholder="Annual Gross Revenue Range (Example $10000-$20000)"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row m-1 border border-bottom-0 border-left-0 border-right-0 border-secondary">
                        <div class="col-md-2"></div>
                        <div class="col-md-2 mt-2">
                            <center>
                                <div class="form-group">
                                    <button type="button" class="btn btn-md" data-dismiss="modal" aria-label="Close">
                                        Close
                                    </button>
                                </div>
                            </center>
                        </div>
                        <div class="col-md-8 mt-2">
                            <center>
                                <div class="form-group">
                                    <asp:Button class="btn btn-primary btn-block btn-md" ID="btn_add_AGR" runat="server" Text="Add Annual Gross Revenue" OnClick="btn_add_AGR_Click" />
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

    </div>
</asp:Content>
