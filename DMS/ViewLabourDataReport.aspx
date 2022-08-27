<%@ Page Title="" Language="C#" MasterPageFile="~/DMS.Master" AutoEventWireup="true" CodeBehind="ViewLabourDataReport.aspx.cs" Inherits="DMS.ViewLabourDataReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <title>View LD Report - DMS</title>

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
                                        <asp:DropDownList CssClass="form-control" ID="ddl_LD_AGR" Enabled="false" runat="server" TabIndex="1" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select Annual Gross Renvenue</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Annual Operating Days</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" Enabled="false" ID="tb_LD_AOD" runat="server" TabIndex="2" TextMode="Number" placeholder="Annual Operating Days"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label>Daily Opearting Hours</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LD_DOH" Enabled="false" runat="server" TabIndex="3" AutoPostBack="true" TextMode="Number" placeholder="Daily Opearting Hours"></asp:TextBox>
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
                        <div class="col-md-6">
                            <label>Model</label>
                            <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList CssClass="form-control" ID="ddl_LD_Model" Enabled="false" TabIndex="4" runat="server" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select Model</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Labour Data Date</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LD_Date" Enabled="false" TextMode="Date" runat="server" TabIndex="5" placeholder=""></asp:TextBox>
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
                                <asp:TextBox CssClass="form-control" ID="tb_LDGM_NoL" Enabled="false" TabIndex="6" runat="server" placeholder="Number of Labour"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Daily Hours Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDGM_DOW" Enabled="false" runat="server" TabIndex="7" placeholder="Daily Hours Worked"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Annual Days Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDGM_ADW" Enabled="false" runat="server" TabIndex="8" placeholder="Annual Days Worked"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Hourly Wage $</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDGM_HW" Enabled="false" runat="server" AutoPostBack="true" TabIndex="9" placeholder="Hourly Wage $"></asp:TextBox>
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
                                <asp:TextBox CssClass="form-control" ID="tb_LDAM_NoL" Enabled="false" runat="server" TabIndex="10" placeholder="Number of Labour"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Daily Hours Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDAM_DOW" Enabled="false" runat="server" TabIndex="11" placeholder="Daily Hours Worked"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Annual Days Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDAM_ADW" Enabled="false" runat="server" TabIndex="12" placeholder="Annual Days Worked"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Hourly Wage $</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDAM_HW" Enabled="false" AutoPostBack="true" runat="server" TabIndex="13" placeholder="Hourly Wage $"></asp:TextBox>
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
                                <asp:TextBox CssClass="form-control" ID="tb_LDCREW_NoL" Enabled="false" runat="server" TabIndex="14" placeholder="Number of Labour"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Daily Hours Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDCREW_DOW" Enabled="false" runat="server" TabIndex="15" placeholder="Daily Hours Worked"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Annual Days Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDCREW_ADW" Enabled="false" runat="server" TabIndex="16" placeholder="Annual Days Worked"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Hourly Wage $</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LDCREW_HW" Enabled="false" AutoPostBack="true" runat="server" TabIndex="17" placeholder="Hourly Wage $"></asp:TextBox>
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
                        <div class="col-md-12">
                            <div class="form-outline">
                                <asp:TextBox id="ta_LD_Notes" Width="100%" class="form-control border border-1 border-primary" TextMode="multiline" Columns="50" Rows="5" runat="server" />
                            </div>
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
    </div>
</asp:Content>
