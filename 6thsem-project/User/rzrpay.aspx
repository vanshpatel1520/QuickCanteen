<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="rzrpay.aspx.cs" Inherits="_6thsem_project.User.rzrpay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
        .row {
            margin-bottom: 10px;
        }
    </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


        <script src="https://checkout.razorpay.com/v1/checkout.js"></script>
	<script type="text/javascript">

        function senemail(a, b, c) {
            var service_price = a;
            var service_id = b;
            var slot_id = c;



            var totalAmount = parseInt(sessionStorage["grandTotalPrice"]) * 100;

            var merchant_total = parseInt(sessionStorage["grandTotalPrice"]);

            var merchant_order_id = "123";
            var currency_code_id = "INR";
            var options = {
                "key": "rzp_test_1UxX48dG8lRzxf",
                "amount": merchant_total, // 2000 paise = INR 20
                "name": "quick canteen",
                "description": "Food Order",

                "currency": "INR",
                "netbanking": true,
                prefill: {
                    name: "Quick Canteen",
                    email: "QuickCanteen69@gmail.com",
                    contact: 9898274756,

                },
                notes: {
                    soolegal_order_id: merchant_order_id,
                },
                "handler": function (response) {

                    var servicePrice = service_price;
                    var serviceId = service_id;
                    var slotId = slot_id;


                    $.ajax({
                        type: "POST",
                        url: "InsertBookingData.aspx",
                        data: { servicePrice: service_price, serviceId: service_id, slotId: slot_id },
                        success: function (data) {

                            window.location = "Invoice.aspx?service_id=" + serviceId;
                        },
                        error: function () {

                            alert("Failed to insert booking data.");
                        }
                    });
                },


                "theme": {
                    "color": "#528FF0"
                }

            };

            var rzp1 = new Razorpay(options);
            rzp1.open();

            
        }

    </script>





<h1>Registration</h1>
<div>
    <div class="row">
        <div class="col-md-offset-1 col-md-2">
            <asp:Label runat="server" Text="Name" />
        </div>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtName" CssClass="form-control" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-offset-1 col-md-2">
            <asp:Label runat="server" Text="Mobile" />
        </div>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-offset-1 col-md-2">
            <asp:Label runat="server" Text="Email" />
        </div>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-offset-1 col-md-2">
            <asp:Label runat="server" Text="Amount" />
        </div>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtAmount" CssClass="form-control" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-6 text-right">
            <asp:Button runat="server" ID="btnRegister" Text="Register" CssClass="btn btn-primary" OnClientClick="return senemail('<%= txtAmount.ClientID %>', '<%= txtServiceID.ClientID %>', '<%= txtSlotID.ClientID %>');" />

        </div>
    </div>
</div>



</asp:Content>
