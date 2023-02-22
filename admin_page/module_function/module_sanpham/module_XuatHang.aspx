<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_MasterPage.master" AutoEventWireup="true" CodeFile="module_XuatHang.aspx.cs" Inherits="admin_page_module_function_module_sanpham_module_XuatHang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headlink" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="hihead" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="himenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="hibodyhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="hibodywrapper" Runat="Server">
    <script>
        function btnChiTiet() {
            document.getElementById('<%=btnChiTiet.ClientID%>').click();
        }
    </script>
    <div class="card card-block">
        <div class="form-group row">
            <div class="col-sm-10">
                <span><a href="../../../admin-xuat-hang" class="btn btn-primary" >Xuất hàng</a></span>
                <span>
                    <asp:Button CssClass="btn btn-primary" ID="btnChiTiet" runat="server" OnClick="btnChiTiet_Click" Text="Chi tiết"></asp:Button></span>
            </div>
        </div>
        <div class="form-group table-responsive">
            <dx:ASPxGridView ID="grvList" runat="server" CssClass="table-hover" ClientInstanceName="grvList" KeyFieldName="xuathang_id" Width="100%">
                <Columns>
                    <dx:GridViewDataColumn Caption="STT" HeaderStyle-HorizontalAlign="Center" Width="5%">
                        <DataItemTemplate>
                            <%#Container.ItemIndex+1 %>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="Mã đơn xuất" FieldName="xuathang_code" HeaderStyle-HorizontalAlign="Center" Width="20%"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="Nhân viên" FieldName="username_fullname" HeaderStyle-HorizontalAlign="Center" Width="20%"></dx:GridViewDataColumn>
                     <dx:GridViewDataColumn Caption="Ngày xuất hàng" FieldName="xuathang_createdate" HeaderStyle-HorizontalAlign="Center" Width="20%"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="Nội dung xuất hàng" FieldName="xuathang_content" HeaderStyle-HorizontalAlign="Center" Width="40%"></dx:GridViewDataColumn>
                </Columns>
                <ClientSideEvents RowDblClick="btnChiTiet" />
                <SettingsSearchPanel Visible="true" />
                <SettingsBehavior AllowFocusedRow="true" />
                <SettingsText EmptyDataRow="Trống" SearchPanelEditorNullText="Gõ từ cần tìm kiếm và enter..." />
                <SettingsLoadingPanel Text="Đang tải..." />
                <SettingsPager PageSize="10" Summary-Text="Trang {0} / {1} ({2} trang)"></SettingsPager>
            </dx:ASPxGridView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="hibodybottom" Runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="hifooter" Runat="Server">
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="hifootersite" Runat="Server">
</asp:Content>

