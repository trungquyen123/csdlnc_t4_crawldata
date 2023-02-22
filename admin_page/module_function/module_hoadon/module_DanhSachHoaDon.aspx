﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_MasterPage.master" AutoEventWireup="true" CodeFile="module_DanhSachHoaDon.aspx.cs" Inherits="admin_page_module_function_module_hoadon_module_DanhSachHoaDon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headlink" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="hihead" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="himenu" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="hibodyhead" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="hibodywrapper" runat="Server">
    <script type="text/javascript">
        function func() {
            grvList.Refresh();
            popupControl.Hide();
        }
        function btnChiTiet() {
            document.getElementById('<%=btnChiTiet.ClientID%>').click();
        }
        function popupHide() {
            document.getElementById('btnClosePopup').click();
        }
       <%-- function checkNULL() {
            var CityName = document.getElementById('<%= txtTitle.ClientID%>');

            if (CityName.value.trim() == "") {
                swal('Tên form không được để trống!', '', 'warning').then(function () { CityName.focus(); });
                return false;
            }
            return true;
        }--%>
        function confirmDel() {
            swal("Bạn có thực sự muốn xóa?",
                "Nếu xóa, dữ liệu sẽ không thể khôi phục.",
                "warning",
                {
                    buttons: true,
                    dangerMode: true
                }).then(function (value) {
                    if (value == true) {
                        var xoa = document.getElementById('<%=btnXoa.ClientID%>');
                        xoa.click();
                    }
                });
        }
    </script>
    <div class="card card-block">
        <div class="form-group row">
            <div class="col-sm-10">
                <asp:UpdatePanel ID="udButton" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnThem" runat="server" Text="Thêm" CssClass="btn btn-primary" OnClick="btnThem_Click" />
                        <asp:Button ID="btnChiTiet" runat="server" Text="Chi tiết" CssClass="btn btn-primary" OnClick="btnChiTiet_Click" />
                        <input type="submit" class="btn btn-primary" value="Xóa" onclick="confirmDel()" />
                        <asp:Button ID="btnXoa" runat="server" CssClass="invisible" OnClick="btnXoa_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-group table-responsive">
            <dx:ASPxGridView ID="grvList" runat="server" ClientInstanceName="grvList" KeyFieldName="hoadon_id" Width="100%">
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" VisibleIndex="0" Width="5%">
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn Caption="Khách hàng" FieldName="khachhang_name" HeaderStyle-HorizontalAlign="Center" Width="20%"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="Tổng hóa đơn" FieldName="hoadon_tongtien" Settings-AllowEllipsisInText="True" HeaderStyle-HorizontalAlign="Center" Width="10%"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="Giảm giá" FieldName="hoadon_tongtiengiam" Settings-AllowEllipsisInText="True" HeaderStyle-HorizontalAlign="Center" Width="10%"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="Phải trả" FieldName="hoadon_phaitra" Settings-AllowEllipsisInText="True" HeaderStyle-HorizontalAlign="Center" Width="10%"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="Ngày giao dịch" FieldName="hoadon_createdate" Settings-AllowEllipsisInText="True" HeaderStyle-HorizontalAlign="Center" Width="10%"></dx:GridViewDataColumn>
                </Columns>
                <ClientSideEvents RowDblClick="btnChiTiet" />
                <%--<SettingsSearchPanel Visible="true" />--%>
                <SettingsBehavior AllowFocusedRow="true" />
                <SettingsText EmptyDataRow="Không có dữ liệu" SearchPanelEditorNullText="Gỏ từ cần tìm kiếm và enter..." />
                <SettingsLoadingPanel Text="Đang tải..." />
                <SettingsPager PageSize="10" Summary-Text="Trang {0} / {1} ({2} trang)"></SettingsPager>
            </dx:ASPxGridView>
        </div>
    </div>
    <dx:ASPxPopupControl ID="popupControl" runat="server" Width="600px" Height="500px" CloseAction="CloseButton" ShowCollapseButton="True" ShowMaximizeButton="True" ScrollBars="Auto" CloseOnEscape="true" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popupControl" ShowFooter="true"
        HeaderText="Hóa Đơn Chi Tiết" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False" AutoUpdatePosition="true" ClientSideEvents-CloseUp="function(s,e){grvList.Refresh();}">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <asp:UpdatePanel ID="udPopup" runat="server">
                    <ContentTemplate>
                        <div class="popup-main">
                            <div class="div_content col-12">
                                <div class="col-12">
                                    <div class="col-6">
                                        <div class="col-12 form-group">
                                            <label class="col-6 form-control-label">Khách hàng:</label>
                                            <div class="col-6">
                                                <asp:Label ID="txt_KhachHang" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-12 form-group">
                                            <label class="col-6 form-control-label">Ngày giao dịch:</label>
                                            <div class="col-6">
                                                <asp:Label ID="txt_Date" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-12 form-group">
                                            <label class="col-6 form-control-label">Thu ngân:</label>
                                            <div class="col-6">
                                                <asp:Label ID="txt_NhanVien" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <div class="col-12 form-group">
                                            <label class="col-12 form-control-label">Hóa đơn chi tiết:</label>
                                            <div class="col-12 form-group">
                                                <table class="table table-borderless">
                                                    <thead>
                                                        <tr>
                                                            <th scope="col">STT</th>
                                                            <th scope="col">Dịch vụ</th>
                                                            <th scope="col">SL</th>
                                                            <th scope="col">ĐG</th>
                                                            <th scope="col">Thành tiền</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater runat="server" ID="rpHoaDonChiTiet">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><%#Container.ItemIndex+1 %></td>
                                                                    <td>
                                                                        <label><%#Eval("dichvu_title") %></label>
                                                                    </td>
                                                                    <td>
                                                                        <label><%#Eval("dichvu_soluong") %></label>
                                                                    </td>
                                                                    <td>
                                                                        <label><%#Eval("dichvu_price") %></label>
                                                                    </td>
                                                                    <td>
                                                                        <label><%#Eval("dichvu_thanhtien") %></label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="col-6">
                                            <div class="col-12 form-group">
                                                <label class="col-6 form-control-label">Tổng hóa đơn:</label>
                                                <div class="col-6">
                                                    <asp:Label ID="txt_TongHoaDon" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-12 form-group">
                                                <label class="col-6 form-control-label">Giảm giá:</label>
                                                <div class="col-6">
                                                    <asp:Label ID="txt_GiamGia" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-12 form-group">
                                                <label class="col-6 form-control-label">Phải trả:</label>
                                                <div class="col-6">
                                                    <asp:Label ID="txt_PhaiTra" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <FooterContentTemplate>
            <div class="mar_but button" hidden="hidden">
                <asp:Button ID="btnLuu" runat="server" ClientIDMode="Static" Text="Lưu" CssClass="btn btn-primary" OnClientClick="return checkNULL()" />
            </div>
        </FooterContentTemplate>
        <ContentStyle>
            <Paddings PaddingBottom="0px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="hibodybottom" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="hifooter" runat="Server">
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="hifootersite" runat="Server">
</asp:Content>

