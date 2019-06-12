<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="ASP.net.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            加密:
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Button"  OnClick="Button1_Click"/>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </div>
        <div>
            解密
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        </div>
        <asp:Button ID="Button3" runat="server" Text="Button" OnClick="Button3_Click"/>
    </form>
</body>
</html>
