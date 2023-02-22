<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_MasterPage.master" AutoEventWireup="true" CodeFile="module_HoaDonThemMoi.aspx.cs" Inherits="admin_page_module_function_module_hoadon_module_HoaDonThemMoi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headlink" runat="Server">
    <script>
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        function btnCheck(id) {
            document.getElementById("<%=txtDichvu_id.ClientID%>").value = id;
            document.getElementById("<%=btnThemDichVu.ClientID%>").click();
            console.log(id)
        }
        //func load dịch vụ chi tiết
        function btnDichVuChiTiet(id) {
            document.getElementById("<%=txtNhomDichVu_id.ClientID%>").value = id;
            document.getElementById("<%=btnDichVuChiTiet.ClientID%>").click();
        }
        //func xóa dịch vụ
        function RemoveDichVu(id) {
            swal("Bạn có thực sự muốn xóa dịch vụ này?",
                "Nếu xóa, dữ liệu không thể khôi phục",
                "warning",
                {
                    buttons: true,
                    successMode: true
                }).then(function (value) {
                    if (value == true) {
                        document.getElementById("<%=txt_RemoveID.ClientID%>").value = id;
                        document.getElementById("<%=btnRemoveDichVu.ClientID%>").click();
                    }
                });

        }

        //func tìm kiếm kh
        function Search() {
            var str = document.getElementById("txtSearch").value;
            //console.log(str);
            document.getElementById("<%=txt_Search.ClientID%>").value = str;
            document.getElementById("<%=txt_Guest.ClientID%>").value = str;
            document.getElementById("<%=btnSearch.ClientID%>").click();
        }
        //event press button enter
        document.onkeydown = function () {
            if ((window.event) && (window.event.keyCode == 13)) {
                Search();
            }
        }
        //set name khách hàng vào thẻ input
        function setName(name, code) {
            document.getElementById("txtSearch").value = name;
            document.getElementById("<%=txtHoaDon.ClientID%>").value = code;
        }
        function ShowModal() {
            document.querySelector(".modal__overlay_customer").style.display = "block";
            document.getElementById("box-modal").style.display = "block";
            document.getElementById("btnHuy").addEventListener("click", function () {
                document.getElementById("box-modal").style.display = "none";
                document.querySelector(".modal__overlay_customer").style.display = "none";

            });
        }
        function checkid(id) {
            var arrayvalue = document.getElementById("<%= txtDanhSachChecked.ClientID %>").value;
            var array = JSON.parse("[" + arrayvalue + "]");
            var index = array.indexOf(id);
            if (index > -1) {
                array.splice(index, 1);
                document.getElementById("<%= txtDanhSachChecked.ClientID %>").value = array;
                document.getElementById("<%= txtCountChecked.ClientID %>").value = document.getElementById("<%= txtCountChecked.ClientID %>").value - 1;
            }
            else {
                document.getElementById("<%= txtCountChecked.ClientID %>").value = array.push(id);
                document.getElementById("<%= txtDanhSachChecked.ClientID %>").value = array;
            }
        }
        function ShowModalKhuyenMai() {
            document.querySelector(".modal__overlay").style.display = "block";
            document.getElementById("box-modal-khuyenmai").style.display = "block";
            document.getElementById("btnHuyKhuyenMai").addEventListener("click", function () {
                document.getElementById("box-modal-khuyenmai").style.display = "none";
                document.querySelector(".modal__overlay").style.display = "none";
            });
            var listChecked = document.getElementById("<%= txtDanhSachChecked.ClientID %>").value
            if (listChecked != "") {
                var arrayID = listChecked.split(',');
                for (var i = 0; i < arrayID.length; i++) {
                    document.getElementById("ckb" + arrayID[i]).checked = true;
                }
            }
            else {
                console.log("chưa chọn gì cả");
            }
          <%--var listCheckedd = document.getElementById("<%= txtKhuyenMaiDaChon.ClientID %>").value
            if (listCheckedd != "") {
                var arrayID = listCheckedd.split(',');
                for (var i = 0; i < arrayID.length; i++) {
                    document.getElementById("ckb" + arrayID[i]).checked = true;
                }
            }--%>
        }
        function checkNULL() {
            var CustomerName = document.getElementById("txtSearch");
            if (CustomerName.value.trim() == "") {
                swal('Tên Khách hàng không được để trống!', '', 'warning').then(function () { CustomerName.focus(); });
                return false;
            }
            return true;
        }
        function btnCheckHoaDon(id) {
            document.getElementById("<%=txtHoaDon_ID.ClientID%>").value = id;
            document.getElementById("<%=btnXemHoaDon.ClientID%>").click();
            //console.log(id);
        }
        function checkForm() {
            var CustomerName = document.getElementById("<%=txt_NameKhachHang.ClientID%>");
            var CustomerPhone = document.getElementById("<%=txt_Phone.ClientID%>");
            var CustomerAddress = document.getElementById("<%=txt_Address.ClientID%>");
            if (CustomerName.value.trim() == "") {
                swal('Tên khách hàng không được để trống!', '', 'warning').then(function () { CustomerName.focus(); });
                return false;
            }
            if (CustomerPhone.value.trim() == "") {
                swal('Số điện thoại không được để trống!', '', 'warning').then(function () { CustomerPhone.focus(); });
                return false;
            }
            return true;
        }
        function setNULL() {
            document.getElementById("txtSearch").value = "";
        }
        //func xác thực thanh toán
        function confirmThanhToan() {
            swal("Bạn có thực sự muốn thanh toán hóa đơn này?",
                "",
                "warning",
                {
                    buttons: true,
                    successMode: true
                }).then(function (value) {
                    if (value == true) {
                        document.getElementById('<%=btnThanhToan.ClientID%>').click();
                    }
                });
        }
        //func xác thực xóa hóa đơn
        function confirmDeleteKhachHang(id) {
            swal("Bạn có thực sự muốn xóa hóa đơn này?",
                "Nếu xóa, dữ liệu không thể khôi phục",
                "warning",
                {
                    buttons: true,
                    successMode: true
                }).then(function (value) {
                    if (value == true) {
                        document.getElementById("<%=txtHoaDon_ID.ClientID%>").value = id;
                        document.getElementById("<%=btnXoaKhachHang.ClientID%>").click();
                    }
                });
        }

        // func đổi tên khách hàng
        function changeNameCustomer() {
            var name = document.getElementById("txtSearch").value;
            document.getElementById("<%=txt_newName.ClientID%>").value = name;
            document.getElementById("<%=btnChangeName.ClientID%>").click();
            console.log(name);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="hihead" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="himenu" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="hibodyhead" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="hibodywrapper" runat="Server">
    <style>
        .dichvu {
            display: block;
            text-align: center;
            height: auto;
            border: 1px dotted blue;
        }

            .dichvu .img_icon,
            .dichvudachon .img_icon {
                width: 100%;
                height: auto;
            }

                .dichvu .img_icon img,
                .dichvudachon .img_icon img {
                    max-width: 100%;
                    max-height: 100%;
                }

        .dichvu_title {
            margin: 0;
            color: #000;
        }

        .curency {
            color: red;
            font-size: 15px;
        }

        td.amount {
            width: 110px;
        }

        .dichvudachon {
            display: block;
            text-align: center;
            height: auto;
            border: 1px dotted;
            position: relative;
        }

        .remove {
            width: 30px;
            height: 30px;
            position: absolute;
            top: 2px;
            right: 2px;
            background: rgba(0,0,0,0.5);
            font-size: 20px;
            text-decoration: none !important;
            border-radius: 50%;
        }

        .hoadonkhachhang {
            display: block;
            position: relative;
            text-align: center;
        }

        .remove__khachhang {
            width: 20px;
            height: 20px;
            position: absolute;
            top: 0px;
            right: 5px;
            background: rgba(0,0,0,0.5);
            font-size: 16px;
            text-decoration: none !important;
            border-radius: 50%;
            padding-bottom: 22px;
        }

        .customer {
            display: flex;
        }

        .search_title {
            width: 70%;
        }

        .search {
            width: 90%;
            border: none;
            border-bottom: 1px solid #bababa;
            border-radius: 0px;
        }

        .modal__overlay,
        .modal__overlay_customer {
            position: fixed;
            background-color: rgba(0,0,0,0.6);
            z-index: 1;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            display: none;
        }

        #box-modal {
            width: 545px;
            height: 477px;
            display: block;
            position: fixed;
            top: 8%;
            left: 32%;
            border-radius: 16px;
            z-index: 1000;
            background-color: #ffffff;
            outline: none;
            border: 1px solid;
            display: none;
            animation-name: modal;
            animation-duration: 1s;
        }

            #box-modal p.title {
                text-align: center;
            }

            #box-modal .form-content {
                display: block;
                margin: 0 auto;
            }

            #box-modal .form-control {
                width: 80%;
                border-radius: 1.5rem;
                margin: 0 auto;
            }

        @keyframes modal {
            from {
                top: 0%;
                left: 32%;
            }

            to {
                top: 8%;
                left: 32%;
            }
        }

        .btnGiff {
            font-size: 24px;
            color: red !important;
            padding: 0;
            margin: 0;
        }

        #box-modal-khuyenmai {
            width: 600px;
            height: auto;
            display: block;
            position: fixed;
            top: 8%;
            left: 29%;
            border-radius: 16px;
            z-index: 1000;
            background-color: #ffffff;
            outline: none;
            border: 1px solid;
            animation-name: modal_khuyenmai;
            animation-duration: 1s;
            display: none;
        }

        @keyframes modal_khuyenmai {
            from {
                top: 0%;
                left: 29%;
            }

            to {
                top: 8%;
                left: 29%;
            }
        }

        .form-group {
            padding-left: 10px;
        }

        .styleInput {
            display: block;
            width: 90%;
            padding: 0.5rem 0.75rem;
            font-size: 1rem;
            line-height: 1.25;
            color: #55595c;
            background-color: #fff;
            background-image: none;
            background-clip: padding-box;
            border: 1px solid rgba(0, 0, 0, 0.15);
            border-radius: 1.5rem;
        }

        .styleSpan {
            text-align: left;
            line-height: 32px;
        }

        .row_input {
            padding-left: 20px;
        }

        .setForm {
            border: none;
        }

        .table__count {
            display: flex;
            align-items: center;
            justify-content: space-around;
            font-weight: 700;
        }

        .themhoadon {
            float: left;
            margin-left: 5px;
        }

        #btnCheck {
            width: 95%;
        }
    </style>
    <div class="card card-block">
        <div>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="row">
                        Nhóm dịch vụ:
                        <asp:Repeater ID="rpNhomDichVu" runat="server">
                            <ItemTemplate>
                                <a href="javascript:void(0)" id="btnDichVu" onclick="btnDichVuChiTiet(<%#Eval("dvcate_id") %>)" class="btn btn-primary"><%#Eval("dvcate_name") %></a>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div style="display: none">
                            <input id="txtNhomDichVu_id" runat="server" type="text" placeholder="id" />
                            <a href="javascript:void(0)" id="btnDichVuChiTiet" runat="server" onserverclick="btnDichVuChiTiet_ServerClick"></a>
                        </div>
                    </div>
                    <div class="row">
                        <asp:Repeater ID="rpDichVu" runat="server">
                            <ItemTemplate>
                                <a href="javascript:void(0)" id="btnDichVu" onclick="btnCheck(<%#Eval("dichvu_id") %>)">
                                    <div class="col-2 dichvu ml-1 mb-3">
                                        <p class="dichvu_title"><%#Eval("dichvu_title") %></p>
                                        <%--<span class="curency"><%#Eval("dichvu_price")%> VND</span>--%>
                                    </div>
                                </a>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div style="display: none">
                        <input id="txtDichvu_id" runat="server" type="text" placeholder="id" />
                        <a href="javascript:void(0)" id="btnThemDichVu" runat="server" onserverclick="btnThemDichVu_ServerClick"></a>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <hr />
    <div class="card card-block">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div style="display: none">
                    <input id="txt_RemoveID" runat="server" type="text" placeholder="id" />
                    <a href="javascript:void(0)" id="btnRemoveDichVu" runat="server" onserverclick="btnRemoveDichVu_ServerClick"></a>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="display: none">
            <input type="text" runat="server" id="txtMaHoaDon" />
            <input type="text" runat="server" id="txtHoaDon" placeholder="hd đang chọn" />
        </div>
        <div class="row ">
            <div class="col-6">
                <div class="container" style="text-align: center">
                    <b>Khách hàng hôm nay:</b>
                    <div class="Listcustomer mt-1 mb-2">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <asp:Repeater runat="server" ID="rpListKhachHang">
                                        <ItemTemplate>
                                            <div class="col-4 hoadonkhachhang">
                                                <a class="btn btn-primary" href="javascript:void(0)" id="btnCheck" onclick="btnCheckHoaDon(<%#Eval("hoadon_id") %>)" title="Xem hóa đơn">KH <%#Eval("khachhang_name") %></a>
                                                <a href="javascript:void(0)" class="remove__khachhang text-white" id="btnRemoveKhachHang(<%#Eval("hoadon_id") %>)" onclick="confirmDeleteKhachHang(<%#Eval("hoadon_id") %>)" title="Xóa khách hàng">X</a>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="row">
                                    <a class="btn btn-primary themhoadon" href="javascript:void(0)" runat="server" id="btnThemHoaDonMoi" onserverclick="btnThemHoaDonMoi_ServerClick">Thêm mới</a>
                                    <div style="display: none">
                                        <input id="txtHoaDon_ID" runat="server" type="text" />
                                        <a href="javascript:void(0)" id="btnXemHoaDon" runat="server" onserverclick="btnXemHoaDon_ServerClick"></a>
                                        <a href="javascript:void(0)" id="btnXoaKhachHang" runat="server" onserverclick="btnXoaKhachHang_ServerClick"></a>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="col-6">
                <div class="container">
                    <span style="margin-left: 30%" class="text-primary mb-2">Hóa đơn thanh toán</span>
                    <div class="customer">
                        <div class="search_title">
                            <input type="text" id="txtSearch" class="form-control search mr-2" placeholder="Tìm kiếm khách hàng" autocomplete="off" onchange="changeNameCustomer()" />
                            <%-- <span><i class="fa fa-search"></i></span>--%>
                        </div>
                        <a href="javascript:void(0)" class="btn btn-primary" title="Thêm khách hàng" onclick="ShowModal()">+</a>
                    </div>
                    <br />
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="nhanvien form-groups" runat="server" id="blog_NhanVien">
                                <label class="col-3">Nhân viên:</label>
                                <asp:DropDownList runat="server" ID="ddlNhanVien" DataTextField="username_fullname" DataValueField="username_id" CssClass="form-control col-6" OnSelectedIndexChanged="ddlNhanVien_SelectedIndexChanged" AutoPostBack="true" Font-Size="Medium" Height="40px"></asp:DropDownList>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <table class="table table-borderless mt-3">
                                <thead>
                                    <tr>
                                        <th style="width: 50px">#</th>
                                        <th scope="col">Dịch vụ</th>
                                        <th scope="col">SL</th>
                                        <th scope="col" class="text-lg-right">T.tiền</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rpHoaDonThanhToan">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <a href="javascript:void(0)" id="btnRemove(<%#Eval("dichvu_id") %>)" onclick="RemoveDichVu(<%#Eval("dichvu_id") %>)" title="Xóa dịch vụ"><i class="fa fa-trash" style="font-size: 24px; color: red;"></i></a>
                                                </td>
                                                <td>
                                                    <label id="txtTenDichVu<%#Eval("dichvu_id") %>"><%#Eval("dichvu_title") %></label>
                                                </td>
                                                <td class="amount">
                                                    <input type="number" min="1" id="<%#Eval("dichvu_id") %>" onchange="myTotal(<%#Eval("dichvu_id") %>),myUpdate(<%#Eval("dichvu_id") %>)" onkeypress="return isNumberKey(event)" class="form-control" value="<%#Eval("dichvu_soluong") %>" autocomplete="off" style="text-align: center" />
                                                </td>
                                                <td hidden="hidden">
                                                    <label id="txtGiaDichVu<%#Eval("dichvu_id") %>"><%#Eval("dichvu_price") %></label>
                                                </td>
                                                <td class="text-lg-right">
                                                    <label id="txtThanhTien<%#Eval("dichvu_id") %>"><%#Eval("dichvu_thanhtien") %></label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td></td>
                                        <td>Giảm giá (<%=GiamGia%>%)
                                            <a href="javascript:void(0)" class="btnGiff" title="Khuyến mãi" onclick="ShowModalKhuyenMai()"><i class="fa fa-gift"></i></a>
                                        </td>
                                        <td></td>
                                        <td class="text-lg-right"><%=TongTienGiam%></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <th>Tổng cộng </th>
                                        <td></td>
                                        <th class="text-lg-right"><%=Tongtien %></th>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="float-lg-right">
                                <a href="javascript:void(0)" class="btn btn-success" runat="server" id="btnThanhToan" onclick="return checkNULL()" onserverclick="btnThanhToan_ServerClick">THANH TOÁN</a>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="up_ProductCT" runat="server">
            <ContentTemplate>
                <div style="display: none">
                    <input id="txt_ID" type="text" runat="server" />
                    <input id="txt_SoLuong" type="text" runat="server" />
                    <input id="txt_GiaTien" type="text" runat="server" />
                    <input id="txt_TongTien" type="text" runat="server" />
                    <a href="javascript:void(0)" id="btnUpdateDichVu" type="button" runat="server" onserverclick="btnUpdateDichVu_ServerClick">Update</a>
                    <%--  nút tìm kiếm khách hàng--%>
                    <a href="javascript:void(0)" id="btnSearch" type="button" runat="server" onserverclick="btnSearch_ServerClick">Search</a>
                    <input id="txt_Search" type="text" runat="server" placeholder="name" />
                    <input id="txt_Guest" type="text" runat="server" placeholder="name kh không lưu" />
                    <input id="txt_newName" type="text" runat="server" placeholder="tên kh mới" />
                    <a href="javascript:void(0)" id="btnChangeName" type="button" runat="server" onserverclick="btnChangeName_ServerClick">ChangeName</a>
                    <%--get CTKM--%>
                    <%--<a href="javascript:void(0)" id="btnKhuyenMai" type="button" runat="server" onserverclick="btnKhuyenMai_ServerClick">Khuyến mãi</a>--%>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <%--modal thêm khách hàng--%>
            <div class="modal__overlay_customer">
                <div id="box-modal">
                    <p class="title m-2">Thêm khách hàng</p>
                    <div>
                        <div class="form-row row_input">
                            <div class="col-3 styleSpan">
                                <span>Tên khách hàng:</span>
                            </div>
                            <div class="col-9 mb-2">
                                <input type="text" id="txt_NameKhachHang" runat="server" value="" placeholder="Your Name *" class="styleInput" />
                            </div>
                        </div>
                        <div class="form-row row_input">
                            <div class="col-3 styleSpan">
                                <span>Điện thoại:</span>
                            </div>
                            <div class="col-9 mb-2">
                                <input type="text" id="txt_Phone" name="name" runat="server" placeholder="Your Number Phone *" class="styleInput" />
                            </div>
                        </div>
                        <div class="form-row row_input">
                            <div class="col-3 styleSpan">
                                <span>Địa chỉ:</span>
                            </div>
                            <div class="col-9 mb-2">
                                <input type="text" id="txt_Address" runat="server" value="" placeholder="Your Address *" class="styleInput" />
                            </div>
                        </div>
                        <div class="form-row row_input">
                            <div class="col-3 styleSpan">
                                <span>Email:</span>
                            </div>
                            <div class="col-9 mb-2">
                                <input type="text" id="txt_Email" runat="server" value="" placeholder="Your Email *" class="styleInput" />
                            </div>
                        </div>
                        <div class="form-group" style="text-align: center">
                            <a href="javascript:void(0)" type="button" class="btn btn-primary" runat="server" id="btnLuuAccount" onclick="return checkForm()" onserverclick="btnLuuAccount_ServerClick">Lưu</a>
                            <a href="javascript:void(0)" type="button" class="btn btn-danger" id="btnHuy">Hủy</a>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <%--modal khuyến mãi--%>
            <div class="modal__overlay">
                <div id="box-modal-khuyenmai">
                    <p class="title m-2">Khuyến mãi trên hóa đơn</p>
                    <div class="modal-body">
                        <table class="table table-borderless">
                            <tbody>
                                <asp:Repeater runat="server" ID="rpKhuyenMai">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <input type="checkbox" id="ckb<%#Eval("khuyenmai_id") %>" onclick="checkid(<%#Eval("khuyenmai_id") %> )" />
                                            </td>
                                            <td>
                                                <label><%#Eval("khuyenmai_name") %></label>
                                            </td>
                                            <td>
                                                <label id="txtKhuyenMai<%#Eval("khuyenmai_id") %>"><%#Eval("khuyenmai_percent") %>%</label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                    <div style="display: none">
                        <input id="txtDanhSachChecked" runat="server" type="text" placeholder="idkm" />
                        <input id="txtCountChecked" runat="server" type="text" />
                        <input type="text" value="" runat="server" id="txtKhuyenMaiDaChon" placeholder="km đã chọn" />
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" runat="server" id="btnApDungKM" onserverclick="btnApDungKM_ServerClick">Áp dụng</button>
                        <button type="button" class="btn btn-danger" id="btnHuyKhuyenMai">Hủy</button>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        var a, b, c;
        function myTotal(id) {
            //var a, b, c;
            a = document.getElementById(id).value;
            b = document.getElementById("txtGiaDichVu" + id).innerHTML;
            //console.log(b)
            //str = b.replace(",", "");
            c = a * b;
            document.getElementById("txtThanhTien" + id).innerHTML = c;
        }
        // update
        function myUpdate(id) {
            a = document.getElementById(id).value;
            b = document.getElementById("txtGiaDichVu" + id).value;
            document.getElementById("<%= txt_ID.ClientID%>").value = id;
            document.getElementById("<%= txt_SoLuong.ClientID%>").value = a;
            document.getElementById("<%= txt_GiaTien.ClientID%>").value = b;
            document.getElementById("<%= txt_TongTien.ClientID%>").value = c;
            document.getElementById("<%= btnUpdateDichVu.ClientID%>").click();
        }

    </script>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="hibodybottom" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="hifooter" runat="Server">
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="hifootersite" runat="Server">
</asp:Content>

