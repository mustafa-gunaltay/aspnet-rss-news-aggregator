<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="RSSwebApplication.Home" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Haberler</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Kategoriye Göre Filtrele</h2>
            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
            </asp:DropDownList>
            <hr />
            <asp:Literal ID="ltNewsList" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
