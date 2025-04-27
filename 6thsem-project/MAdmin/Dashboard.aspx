<%@ Page Title="" Language="C#" MasterPageFile="~/MAdmin/MAdmin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="_6thsem_project.MAdmin.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="pcoded-inner-content">

        <div class="align-align-self-end">
            <asp:Label runat="server" Text="lblMsg" Visible="false"></asp:Label>
        </div>
        <div class="main-body">
            <div class="page-wrapper">
                <div class="page-body">
                    <div class="row">

                        <div class="col-md-6 col-xl-3">
                            <div class="card widget-card-1">
                                <div class="card-block-small">
                                    <i class="icofont icofont-users-alt-5 bg-c-blue card1-icon"></i>
                                    <span class="text-c-blue f-w-600">Users</span>
                                    <h4>  <%Response.Write(Session["user"]); %>  </h4>
                                    <div>
                                        <span class="f-left m-t-10 text-muted">
                                            <a href="Users.aspx"><i class="text-c-blue f-16 icofont icofont-eye-alt m-r-10"></i>View Details</a>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="col-md-6 col-xl-3">
                            <div class="card widget-card-1">
                                <div class="card-block-small">
                                    <i class="icofont icofont-support-faq bg-c-blue card1-icon"></i>
                                    <span class="text-c-blue f-w-600">Bugs & Reports</span>
                                    <h4>  <%Response.Write(Session["contact"]); %>  </h4>
                                    <div>
                                        <span class="f-left m-t-10 text-muted">
                                            <a href="Contacts2.aspx"><i class="text-c-blue f-16 icofont icofont-eye-alt m-r-10"></i>View Details</a>
                                        </span>
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
