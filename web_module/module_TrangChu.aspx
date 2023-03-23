<%@ Page Title="" Language="C#" MasterPageFile="~/Web_MasterPage.master" AutoEventWireup="true" CodeFile="module_TrangChu.aspx.cs" Inherits="web_module_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        * {
            margin: 0px;
            padding: 0px;
        }

        .card-img-ega:hover {
            box-shadow: 0px 1px 24px 0px #00000028;
            border-radius: 90px;
            transform: scale(1.1);
        }

        .card-ega {
            display: grid;
            justify-items: center;
        }

            .card-ega h5 {
                font-weight: 500;
                margin: 0px;
                padding: 0px
            }

            .card-ega span {
                color: #888;
            }

        .card-giamgia h5 {
            color: #F58A20;
            margin-bottom: 0px !important;
            padding: 0px !important;
            font-size: 15px;
        }

        .card-giamgia span {
            font-size: 15px
        }

        .card-giamgia {
            margin: 0px;
        }

        .card-giamgia-copy {
            background-color: #F58A20;
            /*padding: 5px;*/
            color: white;
            font-size: 13px;
            padding: 4px;
        }

        .card-giamgia-dk {
            font-size: 13px;
            color: #888;
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
    <div id="carouselExampleControls" class="carousel slide" data-ride="carousel">
        <div class="carousel-inner">
            <div class="carousel-item active">
                <img class="d-block w-100 h-75" src="../images/slider_1.png" alt="First slide">
            </div>
            <div class="carousel-item">
                <img class="d-block w-100" src="../images/slider_2.png" alt="Second slide">
            </div>
        </div>
        <a class="carousel-control-prev" href="#carouselExampleControls" role="button" data-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>
        </a>
        <a class="carousel-control-next" href="#carouselExampleControls" role="button" data-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="sr-only">Next</span>
        </a>
    </div>
    <div class="container">
        <div class="row mt-4 mb-4">
            <div class="col-3 p-1">
                <div class="d-flex ">
                    <div class="mr-2">
                        <img src="../images/policies_icon_1.png" />
                    </div>
                    <div>
                        <span style="font-weight: 600">Miễn phí vận chuyển</span><br />
                        <span>Nhận hàng trong vòng 3 ngày</span>
                    </div>
                </div>
            </div>
            <div class="col-3 p-1">
                <div class="d-flex ">
                    <div class="mr-2">
                        <img src="../images/policies_icon_2.png" />
                    </div>
                    <div>
                        <span style="font-weight: 600">Quà tặng hấp dẫn</span><br />
                        <span>Nhiều ưu đãi khuyến mãi hot</span>
                    </div>
                </div>
            </div>
            <div class="col-3 p-1">
                <div class="d-flex ">
                    <div class="mr-2">
                        <img src="../images/policies_icon_3.png" />
                    </div>
                    <div>
                        <span style="font-weight: 600">Bảo đảm chất lượng</span><br />
                        <span>Sản phẩm đã dược kiểm định</span>
                    </div>
                </div>
            </div>
            <div class="col-3 p-1">
                <div class="d-flex ">
                    <div class="mr-2">
                        <img src="../images/policies_icon_4.png" />
                    </div>
                    <div>
                        <span style="font-weight: 600">Hotline: 19006750</span><br />
                        <span>Dịch vụ hỗ trợ bạn 24/7</span>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <h3 class="text-center mt-4 mb-4" style="font-weight: 400">Thời trang EGA</h3>
            <div class="row">
                <asp:Repeater runat="server" ID="rpThoiTrangEGA">
                    <ItemTemplate>
                        <div class="col-2 p-3 card-ega">
                            <a href="/chi-tiet-<%#Eval("productcate_id") %>" class="mb-3">
                                <img class="img-fluid card-img-ega" src="<%#Eval("meta_image") %>" />
                            </a>
                            <h5><%#Eval("productcate_title") %></h5>
                            <span>10 Sản phẩm</span>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <br />
        <br />
        <div class="row">
            <div class="col-3" style="padding: 10px 10px">
                <div class="card p-2">
                    <div class="d-flex">
                        <div class="w-25 mr-2 d-flex">
                            <img class="img-fluid" src="https://bizweb.dktcdn.net/thumb/medium/100/448/042/themes/895670/assets/coupon_1_img.png?1678938687575" alt="Alternate Text" />
                        </div>
                        <div class="card-giamgia">
                            <div>
                                <h5 style="">NHẬP MÃ: EGA10</h5>
                                <span>Mã giảm 10% cho đơn hàng tối thiểu 2tr</span>
                            </div>
                            <div style="display: flex; align-items: center; justify-content: space-between;">
                                <a href="#" class="card-giamgia-copy">Sao chép</a>
                                <a href="#" class="card-giamgia-dk">Điều kiện</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-3" style="padding: 10px 10px">
                <div class="card p-2">
                    <div class="d-flex">
                        <div class="w-25 mr-2 d-flex">
                            <img class="img-fluid" src="https://bizweb.dktcdn.net/thumb/medium/100/448/042/themes/895670/assets/coupon_2_img.png?1678938687575" alt="Alternate Text" />
                        </div>
                        <div class="card-giamgia">
                            <div>
                                <h5 style="">NHẬP MÃ: EGA15</h5>
                                <span>Mã giảm 15% cho đơn hàng tối thiểu 5tr</span>
                            </div>
                            <div style="display: flex; align-items: center; justify-content: space-between;">
                                <a href="#" class="card-giamgia-copy">Sao chép</a>
                                <a href="#" class="card-giamgia-dk">Điều kiện</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-3" style="padding: 10px 10px">
                <div class="card p-2">
                    <div class="d-flex">
                        <div class="w-25 mr-2 d-flex">
                            <img class="img-fluid" src="https://bizweb.dktcdn.net/thumb/medium/100/448/042/themes/895670/assets/coupon_3_img.png?1678938687575" alt="Alternate Text" />
                        </div>
                        <div class="card-giamgia">
                            <div>
                                <h5 style="">NHẬP MÃ: EGA2022</h5>
                                <span>Đồng giá 2tr các sản phẩm Đầm AYADA</span>
                            </div>
                            <div style="display: flex; align-items: center; justify-content: space-between;">
                                <a href="#" class="card-giamgia-copy">Sao chép</a>
                                <a href="#" class="card-giamgia-dk">Điều kiện</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-3" style="padding: 10px 10px">
                <div class="card p-2">
                    <div class="d-flex">
                        <div class="w-25 mr-2 d-flex">
                            <img class="img-fluid" src="https://bizweb.dktcdn.net/thumb/medium/100/448/042/themes/895670/assets/coupon_4_img.png?1678938687575" alt="Alternate Text" />
                        </div>
                        <div class="card-giamgia">
                            <div>
                                <h5 style="">NHẬP MÃ: EGAFREESHIP</h5>
                                <span>Miễn phí ship cho đơn hàng tối thiểu 1tr</span>
                            </div>
                            <div style="display: flex; align-items: center; justify-content: space-between;">
                                <a href="#" class="card-giamgia-copy">Sao chép</a>
                                <a href="#" class="card-giamgia-dk">Điều kiện</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />

        <%--Đồ hiệu--%>
        <div class="card-hanghieu">
            <h3 class="text-center">Thời trang hàng hiệu</h3>
            <br />
            <div class="row">
                <asp:Repeater runat="server" ID="rpDoHieu">
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
            <br />
            <div style="display: flex; justify-content: center;">
                <a href="/chi-tiet-6" class="card-xemtatca">Xem tất cả</a>
            </div>
        </div>
        <%--Đồ thể thao--%>
        <div class="card-hanghieu">
            <br />
            <h3 class="text-center">Set đồ tập thể thao</h3>
            <br />
            <div class="row">
                <asp:Repeater runat="server" ID="rpDoTheThao">
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
            <br />
            <div style="display: flex; justify-content: center;">
                <a href="/chi-tiet-8" class="card-xemtatca">Xem tất cả</a>
            </div>
        </div>
        <%--Áo sơ mi--%>
        <div class="card-hanghieu">
            <br />
            <h3 class="text-center">Thời trang công sở</h3>
            <br />
            <div class="row">
                <asp:Repeater runat="server" ID="rpAoSoMi">
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
            <br />
            <div style="display: flex; justify-content: center;">
                <a href="/chi-tiet-11" class="card-xemtatca">Xem tất cả</a>
            </div>
        </div>
    </div>
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>

