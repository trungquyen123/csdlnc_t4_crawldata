<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_MasterPage.master" AutoEventWireup="true" CodeFile="Admin_Default.aspx.cs" Inherits="Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headlink" runat="Server">
    <style>
        #doanhthuhomnay {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="hihead" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="himenu" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="hibodyhead" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="hibodywrapper" runat="Server">
    <div class="card card-block">
        <a href="/../../admin-thong-ke-doanh-thu" class="btn btn-primary">Thống kê doanh thu</a>
        <div id="doanhthuhomnay" class="col-12 mb-2">
            <h3>DOANH THU HÔM NAY</h3>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div runat="server" id="div_doanhthuhomnay">
                        <table class="table table-striped mt-1">
                            <thead>
                                <tr>
                                    <th style="width: 50px;">#</th>
                                    <th scope="col">Ngày</th>
                                    <th scope="col">Tên KH</th>
                                    <th scope="col">Tổng HĐ</th>
                                    <th scope="col">Giảm giá</th>
                                    <th scope="col">Phải trả</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="rpHoaDonHomNay">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Container.ItemIndex+1 %></td>
                                            <td>
                                                <%#Eval("hoadon_giothanhtoan", "{0: dd/MM/yyyy}") %>
                                            </td>
                                            <td>
                                                <%#Eval("khachhang_name") %>
                                            </td>
                                            <td>
                                                <%#Eval("hoadon_tongtien") %>
                                            </td>
                                            <td>
                                                <%#Eval("hoadon_tongtiengiam") %>
                                            </td>
                                            <td>
                                                <%#Eval("hoadon_phaitra") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr style="font-weight: 600">
                                    <td></td>
                                    <td>Tổng cộng:</td>
                                    <td></td>
                                    <td><%=TongTien%></td>
                                    <td><%=TongTienGiam%></td>
                                    <td><%=PhaiTra%></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="hibodybottom" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="hifooter" runat="Server">
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="hifootersite" runat="Server">
</asp:Content>

