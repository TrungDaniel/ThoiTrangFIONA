<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Taikhoan.aspx.cs" Inherits="QuanSinhTo.Quanly.Taikhoan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        .container {
            z-index: 0;
        }
        .cursor-pointer{
            cursor:pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyInFormServer" runat="server">
    <h2 class="text-center">Quản lý tài khoản</h2>
    <div class="container">
        <label class="form-group">
            Tên đăng nhập
    <asp:TextBox ID="txtTenDangnhap" placeholder="Tên đăng nhập" CssClass="form-control" runat="server" />
            <asp:RequiredFieldValidator ID="rfvTenDangnhap" ControlToValidate="txtTenDangnhap" Display="Dynamic" ForeColor="Red" ValidationGroup="themSuaNguoidung" runat="server" ErrorMessage="Tên đăng nhập không dược để trống." />
        </label>
        <label class="form-group">
            Tên Nhân viên
    <asp:TextBox ID="txtTenNhanvien" placeholder="Tên Nhân viên" CssClass="form-control" runat="server" />
            <asp:RequiredFieldValidator ID="rfvTenNhanvien" ControlToValidate="txtTenNhanvien" Display="Dynamic" ForeColor="Red" ValidationGroup="themSuaNguoidung" runat="server" ErrorMessage="Tên nhân viên không dược để trống." />
        </label>
        <label class="form-group">
            Mật khẩu
    <asp:TextBox ID="txtMatkhau" TextMode="Password" placeholder="Mật khẩu" CssClass="form-control" runat="server" />
            <asp:RequiredFieldValidator ID="rfvMatkhau" ControlToValidate="txtMatkhau" Display="Dynamic" ForeColor="Red" ValidationGroup="themSuaNguoidung" runat="server" ErrorMessage="Mật khẩu không dược để trống." />
        </label>
        <br />
        <label class="form-group">
            Quyền
    <asp:DropDownList ID="ddlQuyen" CssClass="form-control" runat="server" />
        </label>
        <br />
        <asp:Button ID="btnThem" OnClick="btnThem_Click" ValidationGroup="themSuaNguoidung" CssClass="btn btn-success" runat="server" Text="Thêm" />
        <asp:Button ID="btnCapnhat" OnClick="btnCapnhat_Click" ValidationGroup="themSuaNguoidung" CssClass="btn btn-warning" Visible="false" runat="server" Text="Cập nhật" />
        <asp:Button ID="btnHuy" OnClick="btnHuy_Click" CausesValidation="false" CssClass="btn btn-danger" runat="server" Text="Hủy" />
        <asp:Label ID="lblRegexTaikhoan" ForeColor="Red" runat="server" />
        <asp:Repeater ID="rptTaikhoan" OnItemDataBound="rptTaikhoan_ItemDataBound" OnItemCommand="rptTaikhoan_ItemCommand" runat="server">
            <HeaderTemplate>
                <table class="table mt-5">
                    <thead>
                        <tr>
                            <th scope="col">STT</th>
                            <th scope="col">Tên đăng nhập</th>
                            <th scope="col">Tên nhân viên</th>
                            <th scope="col">Quyền</th>
                            <th scope="col">Hành động</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <th scope="row"><%#Container.ItemIndex + 1 %></th>
                    <td><%#Eval("sTennguoidung") %></td>
                    <td><%#Eval("sTenNhanvien") %></td>
                    <td>
                        <asp:Literal ID="ltrQuyen" runat="server" /></td>
                    <td>
                        <label>
                            <i class="btn cursor-pointer fas fa-edit text-warning"></i>
                            <asp:Button ID="btnSua" CommandArgument='<%#Eval("PK_iNguoidungID") %>' CommandName="Sua" CssClass="d-none" runat="server" Text="Sửa" />
                        </label>
                        <label>
                            <i class="btn cursor-pointer fas fa-trash-alt text-danger"></i>
                            <asp:Button ID="btnXoa" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa tài khoản này?');" CommandArgument='<%#Eval("PK_iNguoidungID") %>' CommandName="Xoa" CssClass="d-none" runat="server" Text="Xóa" />
                        </label>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
            </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyOutFormServer" runat="server">
</asp:Content>
