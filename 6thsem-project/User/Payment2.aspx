<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment2.aspx.cs" Inherits="_6thsem_project.User.Payment2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>orm action="/Home/CreateOrder" method="post">
    <input name="__RequestVerificationT
   <foken" type="hidden" value="[your anti-forgery token]" />

    <div class="row">
        <label for="name">Customer Name</label>
        <input id="name" name="Name" type="text" class="form-control" />
    </div>
    <div class="row">
        <label for="email">Email ID</label>
        <input id="email" name="Email" type="text" class="form-control" />
    </div>
    <div class="row">
        <label for="amount">Amount (INR)</label>
        <input id="amount" name="Amount" type="text" class="form-control" />
    </div>
    <div class="row">
        <br />
        <button type="submit" class="btn btn-primary">Submit</button>
    </div>
</form>
</body>
</html>
