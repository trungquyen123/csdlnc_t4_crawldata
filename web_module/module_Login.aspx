<%@ Page Title="" Language="C#" MasterPageFile="~/Web_MasterPage.master" AutoEventWireup="true" CodeFile="module_Login.aspx.cs" Inherits="web_module_module_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .form-login {
            display: grid;
            justify-items: center;
        }

            .form-login h3 {
                font-size: 30px;
                font-weight: 400;
            }

            .form-login p {
                font-size: larger;
            }

        .form-input {
            width: 30%;
        }

        .input-dangnhap {
            width: 30%;
            text-align: center;
            background: #cd6420;
            padding: 7px;
            font-size: 20px;
            color: white;
            border-radius: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    <br />
    <div class="form-login">
        <h3 class="text-center">ĐĂNG NHẬP TÀI KHOẢN</h3>
        <p class="text-center">Bạn chưa có tài khoản? <a href="/dang-ky">Đăng ký tại đây</a></p>
        <div class="form-input">
            <label>Tên đăng nhập <span style="color: orangered">*</span></label>
            <input type="text" name="name" runat="server" id="txtUser" class="form-control" value="" placeholder="Nhập tên đăng nhập" />
        </div>
        <br />
        <div class="form-input">
            <label>Mật khẩu <span style="color: orangered">*</span></label>
            <input type="password" name="name" runat="server" id="txtPass" class="form-control" value="" placeholder="Nhập mật khẩu" />
        </div>
        <br />
        <a href="#" class="input-dangnhap" runat="server" id="btnLogin" onserverclick="btnLogin_ServerClick">Đăng nhập</a>
        <br />
        <br />
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>

