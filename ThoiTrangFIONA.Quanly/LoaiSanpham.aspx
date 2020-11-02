<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="LoaiSanpham.aspx.cs" Inherits="QuanSinhTo.Quanly.LoaiSanpham" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function suaLoaisanpham(sanphamID) {
            var loaiSanpham = document.getElementById("loaiSanpham" + sanphamID);
            if (loaiSanpham != null) {
                var lblTenlsp = document.getElementById("lblTenlsp" + sanphamID);
                var txtTenlsp = document.getElementById("txtTenlsp" + sanphamID);
                var lblMotalsp = document.getElementById("lblMotalsp" + sanphamID);
                var txtMotalsp = document.getElementById("txtMotalsp" + sanphamID);
                var lblNgungkinhdoanh = document.getElementById("lblNgungkinhdoanh" + sanphamID);
                var cbNgungkinhdoanh = document.getElementById("cbNgungkinhdoanh" + sanphamID);
                var btnSualoaisp = loaiSanpham.querySelector(".btnSualoaisp");
                var btnCapnhatloaisp = loaiSanpham.querySelector(".btnCapnhatloaisp");
                var btnHuycapnhatLoaisp = loaiSanpham.querySelector(".btnHuycapnhatLoaisp");
                var btnXoaloaisp = loaiSanpham.querySelector(".btnXoaloaisp");
                if (lblTenlsp != null && txtTenlsp != null && lblMotalsp != null && txtMotalsp != null
                    && btnSualoaisp != null && btnCapnhatloaisp != null && btnHuycapnhatLoaisp != null
                    && btnXoaloaisp != null && lblNgungkinhdoanh != null && cbNgungkinhdoanh!=null) {
                    lblTenlsp.classList.add("d-none");
                    lblMotalsp.classList.add("d-none");
                    btnSualoaisp.classList.add("d-none");
                    btnXoaloaisp.classList.add("d-none");
                    lblNgungkinhdoanh.classList.add("d-none");

                    txtTenlsp.classList.remove("d-none");
                    txtMotalsp.classList.remove("d-none");
                    btnCapnhatloaisp.classList.remove("d-none");
                    btnHuycapnhatLoaisp.classList.remove("d-none");
                    cbNgungkinhdoanh.classList.remove("d-none");
                }
            }
        }
        function huycapnhatLoaisanpham(sanphamID) {
            var loaiSanpham = document.getElementById("loaiSanpham" + sanphamID);
            if (loaiSanpham != null) {
                var lblTenlsp = document.getElementById("lblTenlsp" + sanphamID);
                var txtTenlsp = document.getElementById("txtTenlsp" + sanphamID);
                var lblMotalsp = document.getElementById("lblMotalsp" + sanphamID);
                var txtMotalsp = document.getElementById("txtMotalsp" + sanphamID);
                var lblNgungkinhdoanh = document.getElementById("lblNgungkinhdoanh" + sanphamID);
                var cbNgungkinhdoanh = document.getElementById("cbNgungkinhdoanh" + sanphamID);
                var btnSualoaisp = loaiSanpham.querySelector(".btnSualoaisp");
                var btnCapnhatloaisp = loaiSanpham.querySelector(".btnCapnhatloaisp");
                var btnHuycapnhatLoaisp = loaiSanpham.querySelector(".btnHuycapnhatLoaisp");
                var btnXoaloaisp = loaiSanpham.querySelector(".btnXoaloaisp");
                if (lblTenlsp != null && txtTenlsp != null && lblMotalsp != null && txtMotalsp != null
                    && btnSualoaisp != null && btnCapnhatloaisp != null && btnHuycapnhatLoaisp != null
                    && btnXoaloaisp != null && lblNgungkinhdoanh != null && cbNgungkinhdoanh!=null) {
                    lblTenlsp.classList.remove("d-none");
                    lblMotalsp.classList.remove("d-none");
                    btnSualoaisp.classList.remove("d-none");
                    btnXoaloaisp.classList.remove("d-none");
                    lblNgungkinhdoanh.classList.remove("d-none");

                    txtTenlsp.classList.add("d-none");
                    txtMotalsp.classList.add("d-none");
                    btnCapnhatloaisp.classList.add("d-none");
                    btnHuycapnhatLoaisp.classList.add("d-none");
                    cbNgungkinhdoanh.classList.add("d-none");
                }
            }
        }
        function CapnhatLoaisanpham(sanphamID) {
            var txtTenlsp = document.getElementById("txtTenlsp" + sanphamID);
            var txtMotalsp = document.getElementById("txtMotalsp" + sanphamID);
            var cbNgungkinhdoanh = document.getElementById("cbNgungkinhdoanh" + sanphamID);
            var lblRgexTenloaisp = document.getElementById("lblRgexTenloaisp" + sanphamID);
            if (txtTenlsp != null && txtMotalsp != null && lblRgexTenloaisp != null && cbNgungkinhdoanh!=null) {
                lblRgexTenloaisp.classList.add("d-none");
                var eventArg = "CapnhatLoaisanpham♣" + sanphamID + "♣" + txtTenlsp.value + "♣" + txtMotalsp.value + "♣" + cbNgungkinhdoanh.checked;
                var context = sanphamID;
                <%=CapnhatLoaisanpham%>
            }
        }
        function traVeCapnhatLoaisanpham(data, context) {
            var danhSachloaisanpham = document.getElementById("danhSachloaisanpham");
            if (danhSachloaisanpham != null) {
                var arrketqua = data.split('♣');
                if (arrketqua != null) {
                    dieukienrenhanh = arrketqua[0];
                    switch (dieukienrenhanh) {
                        case "thanhcong":
                            huycapnhatLoaisanpham(context);
                            danhSachloaisanpham.innerHTML = arrketqua[1];
                            break;
                        case "thatbai":
                            var lblRgexTenloaisp = document.getElementById("lblRgexTenloaisp" + context);
                            if (lblRgexTenloaisp != null) {
                                lblRgexTenloaisp.classList.remove("d-none");
                                lblRgexTenloaisp.innerText=arrketqua[1];
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        function xoaLoaisanpham(sanphamID) {
            var lblRgexTenloaisp = document.getElementById("lblRgexTenloaisp" + sanphamID);
            if (lblRgexTenloaisp != null) {
                lblRgexTenloaisp.classList.add("d-none");
                var confirmxoa = confirm("Bạn có chắc chắn muốn xóa loại sản phẩm này?");
                if (confirmxoa) {
                    var eventArg = "XoaloaiSanpham♣" + sanphamID;
                    var context = sanphamID;
                    <%=XoaLoaisanpham%>
                }
            }
        }
        function traVeXoaloaisanpham(data, context) {
            var danhSachloaisanpham = document.getElementById("danhSachloaisanpham");
            if (danhSachloaisanpham != null) {
                var arrketqua = data.split('♣');
                if (arrketqua != null) {
                    dieukienrenhanh = arrketqua[0];
                    switch (dieukienrenhanh) {
                        case "thanhcong":
                            danhSachloaisanpham.innerHTML = arrketqua[1];
                            break;
                        case "thatbai":
                            var lblRgexTenloaisp = document.getElementById("lblRgexTenloaisp" + context);
                            if (lblRgexTenloaisp != null) {
                                lblRgexTenloaisp.classList.remove("d-none");
                                lblRgexTenloaisp.innerText=arrketqua[1];
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyInFormServer" runat="server">
    <h1 class="text-center">Quản lý loại sản phẩm</h1>
    <div class="panel">
        <h4>Thêm loại sản phẩm</h4>
        <label>
            Tên loại sản phẩm:
    <asp:TextBox ID="txtTenloaiSP" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTenloaiSP" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtTenloaiSP" ValidationGroup="themLoaiSanpahm" ErrorMessage="Tên loại sản phẩm không được để trống." />
            <asp:Label ID="lblThongbaothem" ForeColor="Red" runat="server" Visible="false"></asp:Label>
        </label>
        <label>
            Mô tả:
    <asp:TextBox ID="txtMotaloaiSP" CssClass="form-control" runat="server"></asp:TextBox>
        </label>
        <asp:Button ID="btnThemloaiSanpham" OnClick="btnThemloaiSanpham_Click" ValidationGroup="themLoaiSanpahm" CssClass="btn btn-success " runat="server" Text="Thêm" />
    </div>
    <div id="danhSachloaisanpham">
        <asp:Repeater ID="rptLoaiSanpham" runat="server">
            <HeaderTemplate>
                <table class="table table-striped">
                    <thead class="thead-dark">
                        <tr>
                            <th scope="col">STT</th>
                            <th scope="col">Tên loại Sản phẩm</th>
                            <th scope="col">Mô tả</th>
                            <th scope="col">Ngừng kinh doanh</th>
                            <th scope="col"></th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr id="loaiSanpham<%#Eval("PK_iLoaiSanphamID") %>">
                    <td scope="row"><%#Container.ItemIndex+1 %></td>
                    <td>
                        <label id="lblTenlsp<%#Eval("PK_iLoaiSanphamID") %>"><%#Eval("sTenLoaiSanpham") %></label>
                        <input id="txtTenlsp<%#Eval("PK_iLoaiSanphamID") %>" value="<%#Eval("sTenLoaiSanpham") %>" class="d-none" type="text" />
                        <label id="lblRgexTenloaisp<%#Eval("PK_iLoaiSanphamID") %>" class="text-danger d-none"></label>
                    </td>
                    <td>
                        <label id="lblMotalsp<%#Eval("PK_iLoaiSanphamID") %>"><%#Eval("sMota") %></label>
                        <textarea id="txtMotalsp<%#Eval("PK_iLoaiSanphamID") %>" class="d-none" cols="50" rows="3"><%#Eval("sMota") %></textarea>
                    </td>
                    <td>
                       <label id="lblNgungkinhdoanh<%#Eval("PK_iLoaiSanphamID") %>"><%# Convert.ToBoolean(Eval("IsNgungKinhdoanh"))==true?"<i title='Ngừng kinh doanh' class='fas fa-ban fa-2x text-danger'></i>":string.Empty %></label>  
                        <input type="checkbox" id="cbNgungkinhdoanh<%#Eval("PK_iLoaiSanphamID") %>" <%#Convert.ToBoolean(Eval("IsNgungKinhdoanh"))==true?"checked":string.Empty%> class="d-none form-control" />            
                    </td>
                    <td><a class="btnSualoaisp" href="javascript:suaLoaisanpham(<%#Eval("PK_iLoaiSanphamID") %>);"><i class="fas fa-edit fa-2x"></i></a><a class="btnXoaloaisp" href="javascript:xoaLoaisanpham(<%#Eval("PK_iLoaiSanphamID") %>);"><i class="fas fa-trash fa-2x text-danger"></i></a>
                        <a class="d-none btnCapnhatloaisp" href="javascript:CapnhatLoaisanpham(<%#Eval("PK_iLoaiSanphamID") %>);"><i class="fas fa-save fa-2x text-success"></i></a><a href="javascript:huycapnhatLoaisanpham(<%#Eval("PK_iLoaiSanphamID") %>);" class="d-none btnHuycapnhatLoaisp"><i class="fas fa-undo fa-2x text-warning"></i></a>
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
