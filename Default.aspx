<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quản lý áo quần</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <asp:Repeater runat="server" ID="rpAoQuan">
                    <ItemTemplate>
                        <div class="col-3">
                            <img src="<%#Eval("spct_image") %>" alt="Alternate Text" />
                            <div style="display: grid">
                                <span><%#Eval("spct_name") %></span>
                                <span><%#Eval("spct_giathanh") %></span>
                                <span><%#Eval("sp_name") %></span>
                                <%--<span><%#Eval("dm_sp") %></span>--%>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>
