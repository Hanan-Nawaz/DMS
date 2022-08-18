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
                                <asp:TextBox CssClass="form-control" ID="tb_LD_AGR" runat="server" placeholder="Annual Gross Renvenue" required="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Annual Operating Days</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LD_AOD" runat="server" placeholder="Annual Operating Days" required></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label>Daily Opearting Hours</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="tb_LD_DOH" runat="server" placeholder="Daily Opearting Hours" required></asp:TextBox>
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
                    </div>

                    <div class="row">
                        <div class="col-md">
                            <label>Please select one</label>
                            <div class="form-group d-flex justify-content-center">
                                <label runat="server" id="lbl_radioC1" class="btn btn-primary mr-1">
                                    <asp:RadioButton runat="server" ID="radioC1" GroupName="radioC" />
                                    GM Only
                                </label>
                                <label runat="server" id="lbl_radioC2" class="btn btn-primary mr-1">
                                    <asp:RadioButton runat="server" ID="radioC2" GroupName="radioC" />
                                    GM & AM Only 
                                </label>

                                <label runat="server" id="Label1" class="btn btn-primary mr-1">
                                    <asp:RadioButton runat="server" ID="RadioButton1" GroupName="radioC"  />
                                    AM Only
                                </label>
                                <label runat="server" id="Label2" class="btn btn-primary mr-1">
                                    <asp:RadioButton runat="server" ID="RadioButton2" GroupName="radioC" />
                                    AM & CREW Only 
                                </label>

                                <label runat="server" id="Label3" class="btn btn-primary mr-1">
                                    <asp:RadioButton runat="server" ID="RadioButton3" GroupName="radioC" />
                                    CREW Only
                                </label>
                                <label runat="server" id="Label4" class="btn btn-primary mr-1">
                                    <asp:RadioButton runat="server" ID="RadioButton4" GroupName="radioC" />
                                    GM & CREW Only 
                                </label>

                                <label runat="server" id="Label5" class="btn btn-primary">
                                    <asp:RadioButton runat="server" ID="RadioButton5" GroupName="radioC" />
                                    ALL 
                                </label>

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
                                <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Number of Labour" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Daily Hours Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Daily Hours Worked" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Annual Days Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Annual Days Worked" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Hourly Wage $</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Hourly Wage $" required></asp:TextBox>
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
                                <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="Number of Labour" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Daily Hours Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" placeholder="Daily Hours Worked" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Annual Days Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server" placeholder="Annual Days Worked" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Hourly Wage $</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox8" runat="server" placeholder="Hourly Wage $" required></asp:TextBox>
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
                                <asp:TextBox CssClass="form-control" ID="TextBox9" runat="server" placeholder="Number of Labour" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Daily Hours Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox10" runat="server" placeholder="Daily Hours Worked" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Annual Days Worked</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox11" runat="server" placeholder="Annual Days Worked" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Hourly Wage $</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox12" runat="server" placeholder="Hourly Wage $" required></asp:TextBox>
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
                                <asp:TextBox CssClass="form-control" ID="TextBox13" runat="server" Enabled="false" placeholder="GM Labour"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>AM Labour</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox14" runat="server" Enabled="false" placeholder="AM Labour"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>CREW Labour</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox15" runat="server" Enabled="false" placeholder="CREW Labour"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Total Wages </label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox16" runat="server" Enabled="false" placeholder="Total Wages"></asp:TextBox>
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
