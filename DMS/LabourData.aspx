<%@ Page Title="" Language="C#" MasterPageFile="~/DMS.Master" AutoEventWireup="true" CodeBehind="LabourData.aspx.cs" Inherits="DMS.LabourData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Labour Data</title>
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
                        <div class="col-md-6">
                            <label>Annual Gross Renvenue</label>
                             <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList CssClass="form-control" ID="ddl_LD_AGR" runat="server" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select Annual Gross Renvenue</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Annual Operating Days</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LD_AOD" runat="server" placeholder="Annual Operating Days" ></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label>Daily Opearting Hours</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LD_DOH" runat="server" placeholder="Daily Opearting Hours" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Annual Opearting Hours</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LD_AOH" runat="server" Enabled="false" placeholder="--"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label>Model</label>
                            <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList CssClass="form-control" ID="ddl_LD_Model" runat="server" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select Model</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Labour Data Date</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LD_Date" TextMode="Date" runat="server" placeholder="--"></asp:TextBox>
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
                                <asp:TextBox CssClass="form-control" ID="tb_LDGM_NoL" runat="server" placeholder="Number of Labour" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Daily Hours Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDGM_DOW" runat="server" placeholder="Daily Hours Worked" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Annual Days Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDGM_ADW" runat="server" placeholder="Annual Days Worked" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Hourly Wage $</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDGM_HW" runat="server" placeholder="Hourly Wage $" ></asp:TextBox>
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
                                <asp:TextBox CssClass="form-control" ID="tb_LDAM_NoL" runat="server" placeholder="Number of Labour" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Daily Hours Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDAM_DOW" runat="server" placeholder="Daily Hours Worked" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Annual Days Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDAM_ADW" runat="server" placeholder="Annual Days Worked" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Hourly Wage $</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDAM_HW" runat="server" placeholder="Hourly Wage $" ></asp:TextBox>
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
                                <asp:TextBox CssClass="form-control" ID="tb_LDCREW_NoL" runat="server" placeholder="Number of Labour" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Daily Hours Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDCREW_DOW" runat="server" placeholder="Daily Hours Worked" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Annual Days Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDCREW_ADW" runat="server" placeholder="Annual Days Worked" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Hourly Wage $</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDCREW_HW" runat="server" placeholder="Hourly Wage $" ></asp:TextBox>
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
                                <textarea class="form-control border border-1 border-primary" id="ta_LD_Notes" rows="4"></textarea>
                            </div>
                        </div>
                    </div>

                    <div class="row mt-5">
                        <div class="col-8 mx-auto">
                            <center>
                                <div class="form-group">
                                    <asp:Button class="btn btn-primary btn-block btn-lg" ID="btn_LD_Calculate" runat="server" Text="Calculate" />
                                </div>
                            </center>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
