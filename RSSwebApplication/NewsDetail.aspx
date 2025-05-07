<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsDetail.aspx.cs" Inherits="RSSwebApplication.NewsDetail" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Haber Detayı</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width:600px; margin:auto;">
            <asp:Literal ID="ltNewsDetail" runat="server"></asp:Literal>
            <br />
            <a href="Home.aspx">← Geri dön</a>
        </div>
    </form>
</body>
</html>
