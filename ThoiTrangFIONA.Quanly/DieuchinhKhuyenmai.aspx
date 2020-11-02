<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DieuchinhKhuyenmai.aspx.cs" Inherits="QuanSinhTo.Quanly.DieuchinhKhuyenmai" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btn-SuaKhuyenmai, .btn-XoaKhuyenmai {
           cursor: pointer;
        }

            .btn-SuaKhuyenmai:hover, .btn-XoaKhuyenmai:hover {
                background-color: #aeaeae;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyInFormServer" runat="server">
    <h1 class="text-center">Điều chỉnh khuyến mại sản phẩm</h1>
    <div class="d-flex flex-row mt-2 pt-5">
        <div class="col-12 col-md-4 row">
            <div class="card card-body">
                <h5>
                    <asp:Literal ID="ltrThemSuaKhuyenmai" runat="server" /></h5>
                <label>
                    Khuyến mại (%):
        <asp:TextBox ID="txtKhuyenmai" CssClass="form-control" placeholder="Khuyến mại (%)" TextMode="number" runat="server"></asp:TextBox>
                    <asp:RangeValidator ID="rvKhuyenmai" Display="Dynamic" runat="server" MinimumValue="0" MaximumValue="100" Type="Integer" ForeColor="Red" ControlToValidate="txtKhuyenmai" ValidationGroup="KhuyenmaiSanpham" ErrorMessage="Khuyến mại phải trong khoảng [0-100]."></asp:RangeValidator>
                    <asp:RequiredFieldValidator ID="rfvKhuyenmai" ControlToValidate="txtKhuyenmai" ValidationGroup="KhuyenmaiSanpham" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="khuyến mại được để trống."></asp:RequiredFieldValidator>
                </label>
                <label>
                    Ngày bắt đầu áp dụng:
        <asp:TextBox ID="txtNgayapdung" CssClass="form-control" TextMode="DateTimeLocal" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNgayapdung" ControlToValidate="txtNgayapdung" ValidationGroup="KhuyenmaiSanpham" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Ngày bắt đầu áp dụng không được để trống."></asp:RequiredFieldValidator>
                </label>
                <label>
                    Ngày kết thúc:
        <asp:TextBox ID="txtNgayKetthuc" CssClass="form-control" TextMode="DateTimeLocal" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNgayKetthuc" ControlToValidate="txtNgayKetthuc" ValidationGroup="KhuyenmaiSanpham" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Ngày kết thúc không được để trống."></asp:RequiredFieldValidator>
                </label>
                <asp:Button CssClass="btn btn-success" ValidationGroup="KhuyenmaiSanpham" OnClick="btnThemKhuyenmai_Click" ID="btnThemKhuyenmai" runat="server" Text="Thêm" />
                <asp:Panel ID="panelSuakhuyenmai" Visible="false" runat="server">
                    <asp:Button ID="btnCapnhatKhuyenmai" OnClick="btnCapnhatGia_Click" CssClass="btn btn-success" runat="server" Text="Cập nhật" />
                    <asp:Button ID="btnHuyCapnhat" OnClick="btnHuyCapnhat_Click" CssClass="btn btn-warning ml-2" runat="server" Text="Hủy bỏ" />
                </asp:Panel>
                <asp:Label ID="lblRegexKhuyenmaiSanpham" ForeColor="Red" runat="server" />
            </div>
            <div class="col-12 pt-3">
                <h5>Loại sản phẩm:
                    <asp:Label ID="lblLoaiSanpham" runat="server" /></h5>
                <h4>Sản phẩm: <b>
                    <asp:Label ID="lblTenSanpham" runat="server" /></b></h4>
                <h5>Giá không khuyến mại:
                    <b> <asp:Label ID="lblGiaChuaKhuyenmai" runat="server" /></b>
                </h5>
                <asp:Image ID="imageSanpham" Width="100%" CssClass="img-fluid" runat="server" />
            </div>

        </div>
        <div class="col-12 col-md-8 row">
            <div class="col-12">
                <h5>Bảng khuyến mại của sản phẩm:</h5>
                <asp:Repeater ID="rptKhuyenmaiCuaSanpham" OnItemCommand="rptKhuyenmaiCuaSanpham_ItemCommand" runat="server">
                    <HeaderTemplate>
                        <table class="table table-striped">
                            <thead class="thead-dark">
                                <tr>
                                    <th scope="col">STT</th>
                                    <th scope="col">Khuyến mại</th>
                                    <th scope="col">Ngày bắt đầu áp dụng</th>
                                    <th scope="col">Ngày kết thúc</th>
                                    <th scope="col"></th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td scope="row"><%#Container.ItemIndex+1 %></td>
                            <td><%#string.Format("{0}%", Convert.ToSingle(Eval("fKhuyenmai"))*100)%></td>
                            <td><%#string.Format("{0:dd/MM/yyy HH:mm}",Eval("tNgaybatdau"))%></td>
                             <td><%#string.Format("{0:dd/MM/yyy HH:mm}",Eval("tNgayketthuc"))%></td>
                            <td>
                                <label class="btn btn-SuaKhuyenmai">
                                    <i class="fas fa-edit fa-2x "></i>
                                    <asp:Button ID="btnSuaKhuyenmai" CommandName="SuaKhuyenmai" CommandArgument='<%#Eval("PK_iKhuyenmaiID") %>' CssClass="d-none" runat="server" />
                                </label>
                                <label class="btn btn-XoaKhuyenmai">
                                    <i class="fas fa-trash fa-2x text-danger"></i>
                                    <asp:Button ID="btnXoaKhuyenmai" CommandName="XoaKhuyenmai" OnClientClick='return confirm("Bạn có chắc chắn xóa khuyến mại này?");' CommandArgument='<%#Eval("PK_iKhuyenmaiID") %>' CssClass="d-none" runat="server" />
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
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyOutFormServer" runat="server">
</asp:Content>
