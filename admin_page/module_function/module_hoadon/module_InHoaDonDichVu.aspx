<%@ Page Language="C#" AutoEventWireup="true" CodeFile="module_InHoaDonDichVu.aspx.cs" Inherits="admin_page_module_function_module_hoadon_module_InHoaDonDichVu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administrator</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <link rel="stylesheet" href="/admin_css/vendor.css" />
    <link rel="stylesheet" href="/admin_css/app-blue.css" />
    <script src="/admin_js/sweetalert.min.js"></script>
    <link href='https://fonts.googleapis.com/css?family=Roboto' rel='stylesheet' />
    <style>
        .hoadon {
            display: block;
            margin: 0 auto;
            text-align: center;
            width: 200px;
            height: auto;
            color: black;
            font-weight: 600;
        }

            .hoadon #title {
                width: 100%;
                text-align: center;
                padding-right: 10px;
            }

                .hoadon #title h3 {
                    font-size: 18px;
                    font-family: system-ui;
                    padding-right: 15px;
                }

                .hoadon #title span {
                    font-size: 10px;
                }

            .hoadon #title_footer span {
                font-size: 8px;
                padding-right: 20px;
            }


            .hoadon #ngaythang {
                width: 100%;
                text-align: left;
                font-size: 10px;
            }

            .hoadon table.table {
                margin: 0;
                padding: 0;
                font-size: 10px;
                border: 1px solid black;
            }

                .hoadon table.table tr {
                    border-bottom-style: solid;
                    border-bottom-width: medium;
                }

                .hoadon table.table td {
                    margin: 0;
                    padding: 0;
                }

                    .hoadon table.table td.nameDV {
                        text-align: left;
                        width: 80px;
                    }

                    .hoadon table.table td.soluong {
                        width: 10px;
                    }

                .hoadon table.table label {
                    font-weight: 600;
                }

        .table__count {
            display: flex;
            align-items: center;
            justify-content: space-around;
            font-size: 10px;
        }

        .title__count {
            flex: 1;
            display: flex;
            align-items: center;
            justify-content: flex-start;
        }

        .money__count {
            flex-basis: 62%;
        }

        .money__count--modify {
            font-size: 12px;
        }

        .table__flex {
            margin-top: 10px;
        }

        .preview {
            display: block;
            width: 30%;
            margin: 0 auto;
            text-align: center;
        }

            .preview #ngaythang {
                text-align: left;
            }

            .preview label {
                font-weight: 500;
            }
            .preview .table__count{
                font-size: 18px;
            }
            .preview .money__count--modify{
                font-size: 19px;
            }
    </style>
    <script>
        function hiddenButton() {
            document.getElementById("btnQuayLai").style.display = "none";
            document.getElementById("hoadonkhachhang").classList.remove("preview");
            document.getElementById("hoadonkhachhang").classList.add("hoadon");
            document.getElementById("btnPrint").style.display = "none";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <a href="/admin-them-moi-hoa-don" class="btn btn-primary float-lg-right m-2" id="btnQuayLai">Quay lại</a>
        <div id="hoadonkhachhang" class="preview">
            <div id="title">
                <h3>Salon NICENAILS</h3>
                <span>K28/2 Phan Tứ - Đà Nẵng</span><br />
                <span>Hotline: 0905912552</span>
                <span style="padding-right: 25px">- 0919698094</span>
                <h3>PHIẾU TÍNH TIỀN</h3>
            </div>
            <div id="ngaythang">
                <span>Ngày: <%=date%> Giờ: <%=time%></span>
                <br />
                <span>Mã HĐ: <%=MaHD%></span>
                <br />
                <span>Tên KH: <%=KhachHang_name%></span>
                <br />
                <span>Nhân viên: <%=Nhanvien%></span>
            </div>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <td>Dịch vụ</td>
                        <td class="soluong">SL</td>
                        <td>TT</td>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rpHoaDonThanhToan">
                        <ItemTemplate>
                            <tr>
                                <td class="nameDV">
                                    <label><%#Eval("dichvu_title") %></label>
                                </td>
                                <td>
                                    <label><%#Eval("dichvu_soluong") %></label>
                                </td>
                                <td class="thanhtien">
                                    <label><%#Eval("dichvu_thanhtien") %></label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <div class="table__flex">
                <div class="table__count">
                    <p class="title__count">Tổng HĐ</p>
                    <p class="money__count"><%=TongHoaDon%></p>
                </div>
                <div class="table__count">
                    <p class="title__count">Giảm giá</p>
                    <p class="money__count"><%=GiamGia%></p>
                </div>
                <div class="table__count">
                    <p class="title__count">Phải trả</p>
                    <p class="money__count money__count--modify"><%=PhaiTra%></p>
                </div>
            </div>
            <div id="title_footer">
                <span>Salon NICENAILS xin cảm ơn quý khách</span><br />
                <span>Hẹn gặp lại!</span>
            </div>
            <br />
            <button id="btnPrint" class="btn btn-primary" onclick="hiddenButton();window.print()">Print</button>
        </div>
    </form>
</body>
</html>
