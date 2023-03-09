<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quản lý áo quần</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/popper.min.js"></script>
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <style>
        .bg-header {
            height: max-content;
            box-shadow: 1px 3px 7px 2px #00000026;
            height: max-content;
            box-shadow: 1px 3px 7px 2px #00000026;
            width: 100%;
            position: fixed;
            z-index: 1;
            background: white;
            width: 100%;
            top: 0;
        }

            .bg-header img {
                width: 150px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="d-flex bg-header p-1">
            <div class="container" style="display: flex; justify-content: space-between;">
                <img src="https://bizweb.dktcdn.net/100/448/042/themes/895670/assets/logo.png?1678249760920" alt="Alternate Text" />
                <div>
                    <nav class="navbar navbar-expand-lg navbar-light">
                        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
                            <span class="navbar-toggler-icon"></span>
                        </button>
                        <div class="collapse navbar-collapse" id="navbarNavDropdown">
                            <ul class="navbar-nav">
                                <li class="nav-item active">
                                    <a class="nav-link" href="#">Trang Chủ <span class="sr-only">(current)</span></a>
                                </li>
                                <li class="nav-item dropdown active">
                                    <a class="nav-link dropdown-toggle" href="#" id="navbarDropdownMenuLink1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Sản phẩm
                                    </a>
                                    <div class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                        <div>
                                            <a class="dropdown-item" href="#">Áo </a>
                                            <a class="dropdown-item" href="#">Váy</a>
                                            <a class="dropdown-item" href="#">Đầm</a>
                                        </div>
                                    </div>
                                </li>
                                <li class="nav-item dropdown active">
                                    <a class="nav-link dropdown-toggle" href="#" id="navbarDropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Chương trình khuyến mãi
                                    </a>
                                    <div class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                        <a class="dropdown-item" href="#">Khuyến mãi 8/3</a>
                                        <a class="dropdown-item" href="#">Giảm giá 20% cho các sản phẩm áo</a>
                                        <a class="dropdown-item" href="#">Giảm giá 10% cho các sản phẩm khác</a>
                                    </div>
                                </li>
                                <li class="nav-item active">
                                    <a class="nav-link" href="#">Đơn hàng</a>
                                </li>
                                <li class="nav-item active">
                                    <a class="nav-link" href="#">Hệ thông cửa hàng</a>
                                </li>
                            </ul>
                        </div>
                    </nav>
                </div>
                <div style="display: flex; align-items: center; font-size: 25px;">
                    <div class="mr-3">
                        <i class="fa fa-users" aria-hidden="true"></i>
                    </div>
                    <div>
                        <i class="fa fa-cart-arrow-down" aria-hidden="true"></i>
                    </div>
                </div>
            </div>
        </div>
        <div id="carouselExampleControls" class="carousel slide mt-5" data-ride="carousel">
            <div class="carousel-inner">
                <div class="carousel-item active">
                    <img class="d-block w-100" src="images/slider_1.png" alt="First slide" />
                </div>
                <div class="carousel-item">
                    <img class="d-block w-100" src="images/slider_2.png" alt="Second slide" />
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
        <div class="container mt-3">
            <div class="row">
                <div class="col-3 p-1" style="display: flex; align-items: center;">
                    <img style="width: 16%; height: fit-content; margin-right: 5px;" src="images/policies_icon_1.png" />
                    <div>
                        <b>Miễn phí vận chuyển</b><br />
                        <span>Nhận hàng trong vòng 3 ngày</span>
                    </div>
                </div>
                <div class="col-3" style="display: flex; align-items: center;">
                    <img style="width: 16%; height: fit-content; margin-right: 5px;" src="images/policies_icon_2.png" />
                    <div>
                        <b>Quà tặng hấp dẫn</b><br />
                        <span>Nhiều ưu đãi khuyến mãi hot</span>
                    </div>
                </div>
                <div class="col-3" style="display: flex; align-items: center;">
                    <img style="width: 16%; height: fit-content; margin-right: 5px;" src="images/policies_icon_3.png" />
                    <div>
                        <b>Bảo đảm chất lượng</b><br />
                        <span>Sản phẩm đã dược kiểm định</span>
                    </div>
                </div>
                <div class="col-3" style="display: flex; align-items: center;">
                    <img style="width: 16%; height: fit-content; margin-right: 5px;" src="images/policies_icon_4.png" />
                    <div>
                        <b>Hotline: 19006750</b><br />
                        <span>Dịch vụ hỗ trợ bạn 24/7</span>
                    </div>
                </div>
            </div>
            <h1 class="text-center m-4">Thời trang EGA</h1>
            <style>
            </style>
            <div class="row">
                <div class="col-2 p-2">
                    <img style="width: inherit;" src="images/season_coll_1_img.png" />
                    <br />
                    <br />
                    <span class="text-center" style="font-weight: 600; display: flex; justify-content: center; font-size: 23px;">Đầm</span>
                    <span class="text-center" style="color: #888; display: flex; justify-content: center; font-size: 17px;">14 sản phẩm</span>
                </div>
                <div class="col-2 p-2">
                    <img style="width: inherit;" src="images/season_coll_2_img.png" />
                    <br />
                    <br />
                    <span class="text-center" style="font-weight: 600; display: flex; justify-content: center; font-size: 23px;">Đầm</span>
                    <span class="text-center" style="color: #888; display: flex; justify-content: center; font-size: 17px;">14 sản phẩm</span>
                </div>
                <div class="col-2 p-2">
                    <img style="width: inherit;" src="images/season_coll_3_img.png" />
                    <br />
                    <br />
                    <span class="text-center" style="font-weight: 600; display: flex; justify-content: center; font-size: 23px;">Đầm</span>
                    <span class="text-center" style="color: #888; display: flex; justify-content: center; font-size: 17px;">14 sản phẩm</span>
                </div>
                <div class="col-2 p-2">
                    <img style="width: inherit;" src="images/season_coll_4_img.png" />
                    <br />
                    <br />
                    <span class="text-center" style="font-weight: 600; display: flex; justify-content: center; font-size: 23px;">Đầm</span>
                    <span class="text-center" style="color: #888; display: flex; justify-content: center; font-size: 17px;">14 sản phẩm</span>
                </div>
                <div class="col-2 p-2">
                    <img style="width: inherit;" src="images/season_coll_5_img.png" />
                    <br />
                    <br />
                    <span class="text-center" style="font-weight: 600; display: flex; justify-content: center; font-size: 23px;">Đầm</span>
                    <span class="text-center" style="color: #888; display: flex; justify-content: center; font-size: 17px;">14 sản phẩm</span>
                </div>
                <div class="col-2 p-2">
                    <img style="width: inherit;" src="images/season_coll_6_img.png" />
                    <br />
                    <br />
                    <span class="text-center" style="font-weight: 600; display: flex; justify-content: center; font-size: 23px;">Đầm</span>
                    <span class="text-center" style="color: #888; display: flex; justify-content: center; font-size: 17px;">14 sản phẩm</span>
                </div>
            </div>
            <div class="row">
                <img style="width: -webkit-fill-available;" src="images/mai_giam_gia.png" />
            </div>
            <h1 class="text-center m-4">Giảm sốc 50%</h1>
            <div class="row">
                <style>
                    .a-mua {
                        position: absolute;
                        margin: 5%;
                        text-decoration: none;
                        background-color: red;
                        color: white;
                        border-radius: 24px;
                        padding: 6px 14px;
                        font-size: 17px;
                        font-weight: 500;
                        box-shadow: 1px 3px 7px 2px #00000026;
                    }
                </style>
                <asp:Repeater runat="server" ID="rpAoQuan">
                    <ItemTemplate>
                        <div class="col-3">
                            <a href="#" class="a-mua">Mua</a>
                            <img style="width: inherit; box-shadow: 1px 3px 7px 2px #00000026; border-radius: 10px;" src="<%#Eval("spct_image") %>" alt="Alternate Text" />

                            <br />
                            <br />
                            <div style="display: grid">
                                <span style="font-size: 13px; color: #9696969c !important; font-weight: 500;"><%#Eval("sp_name") %></span>
                                <span style="font-size: 20px; font-weight: 700;"><%#Eval("spct_name") %></span>
                                <span style="font-size: 17px; color: #ff0000a1;"><%#Eval("spct_giathanh") %></span>
                            </div>
                            <br />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <style>
            .footer {
                background-color: black;
                width: 100%;
                height: max-content;
                color: white
            }

                .footer a {
                    color: white;
                    text-decoration: none
                }

                .footer h1 {
                    font-size: 30px;
                }
        </style>
        <div class="footer p-3">
            <div class="row">
                <div class="col-3">
                    <img style="width: inherit" src="https://bizweb.dktcdn.net/100/448/042/themes/895670/assets/logo-footer.png?1678249760920" alt="Alternate Text" />
                    <span><i class="fa fa-map-marker mr-2" style="width: 20px; height: 20px;" aria-hidden="true"></i>Việt Nam</span><br />
                    <span><i class="fa fa-phone-square mr-2 " style="width: 20px; height: 20px;" aria-hidden="true"></i>0905163918</span><br />
                    <span><i class="fa fa-envelope mr-2" style="width: 20px; height: 20px;" aria-hidden="true"></i>txvq0101@gmail.com</span><br />
                </div>
                <div class="col-3" style="display: grid">
                    <h1>CHÍNH SÁCH</h1>
                    <a href="#" class="ml-2">Giới thiệu</a>
                    <a href="#" class="ml-2">Hệ thống cửa hàng</a>
                    <a href="#" class="ml-2">Câu hỏi thường gặp</a>
                    <a href="#" class="ml-2">Gọi điện đặt hàng</a>
                </div>
                <div class="col-3" style="display: grid">
                    <h1>HỖ TRỢ KHÁCH HÀNG </h1>
                    <a href="#" class="ml-2">Thông tin liên hệ</a>
                    <a href="#" class="ml-2">Chính sách giao hàng</a>
                    <a href="#" class="ml-2">Chính sách đổi hàng</a>
                    <a href="#" class="ml-2">Chính sách bán hàng</a>
                </div>
                <div class="col-3" style="display: grid">
                    <h1>ĐĂNG KÝ NHẬN TIN</h1>
                    <span class="ml-2">Bạn có muốn nhận khuyến mãi đặc biệt? Đăng ký ngay.</span>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
