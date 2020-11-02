<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DieuchinhGia.aspx.cs" Inherits="QuanSinhTo.Quanly.DieuchinhGia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btn-Suagia, .btn-Xoagia {
           cursor: pointer;
        }

            .btn-Suagia:hover, .btn-Xoagia:hover {
                background-color: #aeaeae;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyInFormServer" runat="server">
    <h1 class="text-center">Điều chỉnh giá sản phẩm</h1>
    <div class="d-flex flex-row mt-2 pt-5">
        <div class="col-12 col-md-4 row">
            <div class="card card-body">
                <h5>
                    <asp:Literal ID="ltrThemSuaGia" runat="server" /></h5>
                <label>
                    Giá (*1000 vnđ):
        <asp:TextBox ID="txtGia" CssClass="form-control" placeholder="Giá (*1000 vnđ)" TextMode="number" runat="server"></asp:TextBox>
                    <asp:RangeValidator ID="rvGia" Display="Dynamic" runat="server" Type="Integer" MinimumValue="0" MaximumValue="9999999" ForeColor="Red" ControlToValidate="txtGia" ValidationGroup="ThemgiaSanpham" ErrorMessage="Giá tối thiếu là 0 vnđ."></asp:RangeValidator>
                    <asp:RequiredFieldValidator ID="rfvGia" ControlToValidate="txtGia" ValidationGroup="ThemgiaSanpham" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Giá không được để trống."></asp:RequiredFieldValidator>
                </label>
                <label>
                    Ngày bắt đầu áp dụng:
        <asp:TextBox ID="txtNgayapdung" CssClass="form-control" TextMode="DateTimeLocal" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNgayapdung" ControlToValidate="txtNgayapdung" ValidationGroup="ThemgiaSanpham" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Ngày bắt đầu áp dụng không được để trống."></asp:RequiredFieldValidator>
                </label>
                <asp:Button CssClass="btn btn-success" ValidationGroup="ThemgiaSanpham" OnClick="btnThemgia_Click" ID="btnThemgia" runat="server" Text="Thêm" />
                <asp:Panel ID="panelSuagia" Visible="false" runat="server">
                    <asp:Button ID="btnCapnhatGia" OnClick="btnCapnhatGia_Click" CssClass="btn btn-success" runat="server" Text="Cập nhật" /><asp:Button ID="btnHuyCapnhat" OnClick="btnHuyCapnhat_Click" CssClass="btn btn-warning ml-2" runat="server" Text="Hủy bỏ" />
                </asp:Panel>
                <asp:Label ID="lblRegexThemgiaSanpham" ForeColor="Red" runat="server" />
            </div>
            <div class="col-12 pt-3">
                <h5>Loại sản phẩm:
                    <asp:Label ID="lblLoaiSanpham" runat="server" /></h5>
                <h4>Sản phẩm: <b>
                    <asp:Label ID="lblTenSanpham" runat="server" /></b></h4>
                <asp:Image ID="imageSanpham" Width="100%" CssClass="img-fluid" runat="server" />
            </div>

        </div>
        <div class="col-12 col-md-8 row">
            <div class="col-12">
                <h5>Bảng giá của sản phẩm:</h5>
                <asp:Repeater ID="rptGiaCuaSanpham" OnItemCommand="rptGiaCuaSanpham_ItemCommand" runat="server">
                    <HeaderTemplate>
                        <table class="table table-striped">
                            <thead class="thead-dark">
                                <tr>
                                    <th scope="col">STT</th>
                                    <th scope="col">Giá</th>
                                    <th scope="col">Ngày bắt đầu áp dụng</th>
                                    <th scope="col"></th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td scope="row"><%#Container.ItemIndex+1 %></td>
                            <td><%#string.Format("{0:C0}", Eval("mGia"))%></td>
                            <td><%#string.Format("{0:dd/MM/yyy HH:mm}",Eval("tNgaybatdauapdung"))%></td>
                            <td>
                                <label class="btn btn-Suagia">
                                    <i class="fas fa-edit fa-2x "></i>
                                    <asp:Button ID="btnSuagia" CommandName="Suagia" CommandArgument='<%#Eval("PK_iBanggiaID") %>' CssClass="d-none" runat="server" />
                                </label>
                                <label class="btn btn-Xoagia">
                                    <i class="fas fa-trash fa-2x text-danger"></i>
                                    <asp:Button ID="btnXoagia" CommandName="Xoagia" OnClientClick='return confirm("Bạn có chắc chắn xóa giá này?");' CommandArgument='<%#Eval("PK_iBanggiaID") %>' CssClass="d-none" runat="server" />
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
