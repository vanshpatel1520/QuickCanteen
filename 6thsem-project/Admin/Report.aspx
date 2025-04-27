﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="_6thsem_project.Admin.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="pcoded-inner-content pt-0">

        <div class="align-align-self-end">
            <asp:Label runat="server" Text="lblMsg" Visible="false"></asp:Label>
        </div>
        <div class="main-body">
            <div class="page-wrapper">

                <div class="page-body">
                    <div class="row">
                        <div class="col-sm-12 ">
                            <div class="card">
                                <div class="card-header">
                                    <div class="container">
                                        <div class="form-row">
                                            <div class=" form-group col-md-4">
                                                <label>From Date</label>
                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ForeColor="red"
                                                    ErrorMessage="*" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtFromDate"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtFromDate" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class=" form-group col-md-4">
                                                <label>To Date</label>
                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ForeColor="red"
                                                    ErrorMessage="*" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtToDate"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtToDate" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class=" form-group col-md-4">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary mt-md-4"
                                                    OnClick="btnSearch_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-block">
                                    <div class="row">

                                        <div class="col-12  mobile-inputs">
                                            <h4 class="sub-title">Selling Report</h4>
                                            <div class="card-block table-border-style">
                                                <div class="table-responsive">

                                                    <asp:Repeater ID="rReport" runat="server">
                                                        <HeaderTemplate>
                                                            <table class="table data-table-export table-hover nowrap">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="table-plus">SrNo</th>
                                                                        <th>Full Name</th>
                                                                        <th>Email</th>
                                                                        <th>Item Orders</th>
                                                                        <th>Total Cost</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="table-plus"><%#Eval("SrNo") %> </td>
                                                                <td><%#Eval("Name") %></td>
                                                                <td><%#Eval("Email") %></td>
                                                                <td><%#Eval("TotalOrders") %></td>
                                                                <td><%#Eval("TotalPrice") %></td>

                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody>
    </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row pl-4">
                                        <asp:Label ID="lblTotal" runat="server" Font-Size="Small" Font-Bold="true"></asp:Label>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
