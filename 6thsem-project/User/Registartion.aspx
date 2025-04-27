<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Registartion.aspx.cs" Inherits="_6thsem_project.User.Registartion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        /*for disaapearing alert message*/
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
    <script>
        function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgUser.ClientID%>').prop('src', e.target.result)
                        .width(200)
                        .height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_container">
                <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
                <div class="align-self-auto">
                </div>
                <asp:Label ID="lblHeaderMsg" runat="server" Text="<h2> User Registartion</h2>"></asp:Label>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="form_container">

                        <div>

                            <asp:RequiredFieldValidator ID="rfvName" runat="server"
                                ErrorMessage="Name is Required" ControlToValidate="txtName" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revName" runat="server" ControlToValidate="txtName" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"
                                ValidationExpression="^[a-zA-Z\s]+$" ErrorMessage="Name must be in Characters only"></asp:RegularExpressionValidator>

                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter Full Name"
                                ToolTip="Full Name"></asp:TextBox>
                        </div>


                        <div>
                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server"
                                ErrorMessage="Username is Required" ControlToValidate="txtUsername"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>

                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter  Username"
                                ToolTip="Username"></asp:TextBox>
                        </div>

                        <div>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                                ErrorMessage="Email is Required" ControlToValidate="txtEmail" ForeColor="Red"
                                Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter  Email"
                                ToolTip="Email" TextMode="Email"></asp:TextBox>
                        </div>


                    </div>
                </div>


                <div class="col-md-6">
                    <div class="form_container">


                        <div>

                            <asp:RequiredFieldValidator ID="rfvMobile" runat="server"
                                ErrorMessage="Phone Number is Required" ControlToValidate="txtMobile" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revMobile" runat="server" ControlToValidate="txtMobile" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"
                                ValidationExpression="^[0-9]{10}$" ErrorMessage="Phone No. must have  10 digits"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Enter Phone Number" TextMode="Phone"
                                ToolTip="Phone Number"></asp:TextBox>
                        </div>

                        <div>
                            <asp:FileUpload ID="fuUserImage" runat="server" CssClass="form-control" ToolTip="User Image"
                                onchange="ImagePreview(this);" />
                        </div>

                        <div>

                            <asp:RequiredFieldValidator ID="rvfPassword" runat="server"
                                ErrorMessage="Password is Required" ControlToValidate="txtPassword"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Enter a Password"
                                ToolTip="Password" TextMode="Password"></asp:TextBox>
                        </div>

                    </div>
                </div>

                <div class="row pl-4">
                    <div class="btn_box">
                        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-success rounded-pill pl-4 pr-4 text-white"
                            OnClick="btnRegister_Click" />

                        <asp:Label ID="lblAlreadyUser" runat="server"
                            CssClass="pl-3 text-black-100" Text="Already registered? <a href='Login.aspx' class='badge badge-info'>Login here..</a>">
                        </asp:Label>
                    </div>
                </div>
                <hr />
                

                <div class="row p-5">
                    <div style="align-items: center">
                        <asp:Image ID="imgUser" runat="server" CssClass="img-thumbnail" />
                    </div>
                </div>

            </div>
        </div>
    </section>

</asp:Content>
