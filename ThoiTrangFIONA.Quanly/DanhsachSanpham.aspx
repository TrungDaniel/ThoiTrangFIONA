<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DanhsachSanpham.aspx.cs" Inherits="QuanSinhTo.Quanly.DanhsachSanpham" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btn-xoaSanpham {
            cursor: pointer;
        }

        .btn-SuaSanpham, .btn-xoaSanpham {
            margin-bottom: 5px;
        }

            .btn-SuaSanpham:hover, .btn-xoaSanpham:hover {
                background-color: #aeaeae;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyInFormServer" runat="server">
       <h1 class="text-center">Danh sách sản phẩm</h1>
    <label>
        Loại sản phẩm:
    <asp:DropDownList ID="ddlLoaiSanpham" AutoPostBack="true" OnSelectedIndexChanged="ddlLoaiSanpham_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
    </label>
    <asp:Label ID="lblThongbaoLoaiSanphamNgungKinhdoanh" runat="server" ForeColor="Red"></asp:Label>
    <asp:Repeater ID="rptSanpham" OnItemDataBound="rptSanpham_ItemDataBound" OnItemCommand="rptSanpham_ItemCommand" runat="server">
        <HeaderTemplate>
            <table class="table table-striped">
                <thead class="thead-dark">
                    <tr>
                        <th scope="col">STT</th>
                        <th scope="col">Hình ảnh</th>
                        <th scope="col">Tên sản phẩm</th>
                        <th scope="col">Còn kinh doanh</th>
                        <th scope="col">Mô tả</th>
                        <th scope="col">Xuất xứ</th>
                        <th scope="col">Loại sản phẩm</th>
                        <th scope="col">Giá đang áp dụng</th>
                        <th scope="col">Khuyến mại</th>
                        <th scope="col"></th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td scope="row"><%#Container.ItemIndex+1 %></td>
                <td>
                    <asp:Image ID="imgSanpham" Width="150px" ImageUrl="~/Images/No-image.png" runat="server" /></td>
                <td><b><%#Eval("sTenSanpham") %></b></td>
                <td><%# Convert.ToBoolean(Eval("isNgungCungcap"))==true?"<i  title='Ngừng kinh doanh' class='fas fa-ban fa-2x text-danger'></i>":string.Empty %></td>
                <td><%#Eval("sMota") %></td>
                <td><%#Eval("sXuatxu") %></td>
                <td>
                    <asp:Literal ID="ltrLoaiSanpham" runat="server" /></td>
                <td>
                  <b>  <asp:Literal ID="ltrGiaDangApdung" Text="Liên hệ" runat="server" /></b>
                </td>
                <td>
                     <b>  <asp:Literal ID="ltrKhuyenmaiDangApdung" runat="server" /></b>
                </td>
                <td class="text-center">
                    <a href='ThemSuaSanpham.aspx?ud=<%#Eval("PK_iSanphamID") %>' class="btn btn-SuaSanpham" title="Sửa sản phẩm">
                        <i class="fas fa-edit fa-2x"></i>
                    </a>
                    <label class="btn btn-xoaSanpham" title="Xóa sản phẩm">
                        <i class='fas fa-trash fa-2x text-danger '></i>
                        <asp:Button ID="btnXoaSanpham" OnClientClick='return confirm("Bạn có chắc chắn xóa sản phẩm này?");' CommandName="xoaSanpham" CommandArgument='<%#Eval("PK_iSanphamID") %>' CssClass="d-none" runat="server" />
                    </label>
                    <div class="row m-0">
                    <asp:HyperLink CssClass="btn btn-success col-12" ID="hrefDieuChinhgia" runat="server" ToolTip="Điều chỉnh giá">Giá</asp:HyperLink>
                       <asp:HyperLink CssClass="btn btn-warning col-12 mt-2" ID="hrefDieuChinhKhuyenmai" runat="server" ToolTip="Điều chỉnh khuyến mại">Khuyến mại</asp:HyperLink>
                        <asp:HyperLink CssClass="btn btn-light col-12 mt-2" ID="hrefCongthuc" runat="server" ToolTip="Điều chỉnh công thức">Công thức</asp:HyperLink>
                        </div>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tbody>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyOutFormServer" runat="server">
</asp:Content>