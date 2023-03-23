<%@ Page Title="" Language="C#" MasterPageFile="~/Web_MasterPage.master" AutoEventWireup="true" CodeFile="module_ChiTiet.aspx.cs" Inherits="web_module_module_ChiTiet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        label {
            margin: 0px;
            padding: 0px;
        }

        .card-hanghieu h3 {
            font-weight: 400;
        }

        .card-hanghieu-title {
            color: black;
            font-size: 20px;
            font-weight: 600;
        }

            .card-hanghieu-title:hover {
                color: #123cc9;
                text-decoration: none;
                font-weight: 500;
            }

        .card-hanghieu-loai {
            font-size: 15px;
            color: #888;
            font-weight: 500;
        }

        .card-hanghieu-gianew {
            color: orangered;
            font-size: 17px;
            font-weight: 500;
        }

        .card-hanghieu-gia {
            font-size: 15px;
            color: #888;
            text-decoration: line-through;
            font-weight: 500;
        }

        .card-xemtatca {
            background-color: white;
            box-shadow: 0px 1px 24px 0px #00000028;
            font-size: 17px;
            padding: 8px 22px;
            border-radius: 9px;
            color: #F58A20;
            border-style: solid;
            border-color: #F58A20;
            font-weight: 400;
        }

            .card-xemtatca:hover {
                color: white;
                background-color: #F58A20;
                text-decoration: none
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    <div class="container">
        <div class="row ">
            <div class="col-4">
                <div>
                    <asp:Repeater runat="server" ID="rpLoai">
                        <ItemTemplate>
                            <h1 style="font-weight: 400"><%#Eval("productcate_title") %></h1>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="ml-2">
                    <p>THƯƠNG HIỆU</p>
                    <div class="ml-4">
                        <asp:Repeater runat="server" ID="rpThuongHieu">
                            <ItemTemplate>
                                <div style="display: flex; align-items: center">
                                    <input type="radio" class="form-check-input" onclick="ChangeThuongHieu(<%#Eval("id_TH") %>)" name="name" value="" />
                                    <label><%#Eval("thuonghieu") %></label>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div style="display: flex; align-items: center">
                            <input type="radio" class="form-check-input" runat="server" onclick="BoChon()" name="name" value="" />
                            <label>Tất cả</label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-8">
                <div class="row">
                    <asp:Repeater runat="server" ID="rpChiTietSanPham">
                        <ItemTemplate>
                            <div class="col-3 p-2">
                                <a href="/san-pham-<%#Eval("product_id") %>">
                                    <div class="card p-2">
                                        <img src="../images_SanPham/<%#Eval("product_image") %>" alt="Alternate Text" />
                                    </div>
                                </a>
                                <div style="display: grid; margin-top: 10px">
                                    <span class="card-hanghieu-loai"><%#Eval("city_name") %></span>
                                    <a href="/san-pham-<%#Eval("product_id") %>" class="card-hanghieu-title"><%#Eval("product_title") %></a>
                                    <span class="card-hanghieu-gianew"><%#Eval("product_price_new") %> đ</span>
                                    <span class="card-hanghieu-gia"><%#Eval("product_price") %> đ</span>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <div style="display: none">
            <input type="text" id="txtTH" runat="server" name="name" value="" />
            <a href="#" runat="server" id="btnChangeTH" onserverclick="btnChangeTH_ServerClick">content</a>
            <a href="#" runat="server" id="btnBoChon" onserverclick="btnBoChon_ServerClick">content</a>
            <script>
                function ChangeThuongHieu(text) {
                    document.getElementById("<%=txtTH.ClientID%>").value = text;
                    document.getElementById("<%=btnChangeTH.ClientID%>").click();
                    console.log(1);
                }
                function BoChon() {
                    document.getElementById("<%=btnBoChon.ClientID%>").click();
                }
            </script>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>

