<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ThemSuaSanpham.aspx.cs" Inherits="QuanSinhTo.Quanly.ThemSuaSanpham" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/cropper.css" rel="stylesheet" />
    <script src="../Js/cropper.js"></script>
    <script type="text/javascript">
        var cropper = null;
        function showImage() {
            var btnSaveScrop = document.getElementById("btnSaveScrop");
            var lblRegexUploadImage = document.getElementById("<%=lblRegexUploadImage.ClientID%>");
            if (btnSaveScrop != null && lblRegexUploadImage != null) {
                btnSaveScrop.classList.add("d-none");
                if (cropper != null)
                    cropper.destroy();
                if (this.files && this.files[0]) {
                    var image = document.getElementById("<%=privewimg.ClientID%>");
                       var type = this.files[0].type
                       switch (type) {
                           case 'image/png': break;
                           case 'image/jpeg': break;
                           case 'image/pjpeg': break;
                           default:
                               lblRegexUploadImage.innerText = "Vui lòng chọn tệp hành ảnh với phần mở rộng là png,jpg,jpeg.";
                               return;
                       }
                       btnSaveScrop.classList.remove("d-none");
                       lblRegexUploadImage.innerText = "";
                       var obj = new FileReader();
                       obj.onload = function (data) {
                           image.src = data.target.result;
                           var options = {
                               aspectRatio: 1 / 1,
                               viewMode: 2,
                               crop: function (e) {
                                   var data = e.detail;
                               },

                           };
                           cropper = new Cropper(image, options);
                       }
                       obj.readAsDataURL(this.files[0]);

                   }
               }
           }
           function saveImageCrop() {
               var btnSaveScrop = document.getElementById("btnSaveScrop");
               var lblRgexSanpham = document.getElementById("<%=lblRgexSanpham.ClientID%>");
               if (lblRgexSanpham != null)
                   lblRgexSanpham.innerText = "";
               if (btnSaveScrop != null) {
                   btnSaveScrop.classList.add("d-none");
                   var image = document.getElementById("<%=privewimg.ClientID%>");
                   var imgSrc = cropper.getCroppedCanvas({
                   }).toDataURL();
                   image.src = imgSrc;
                   cropper.destroy();
                   var hdImage = document.getElementById('<%=hdImageUpload.ClientID%>');
                   if (hdImage != null)
                       hdImage.value = imgSrc;
                   var inputfile = document.getElementById('<%=uploadImage.ClientID%>');
                   if (inputfile != null)
                       inputfile.value = "";
               }
           }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyInFormServer" runat="server">
    <h1 class="text-center">
        <asp:Literal ID="ltrThemSuaSanpham" runat="server" /></h1>
    <div class="panel row p-5">
        <div class="col-6">
            <div class="form-group">
                <label>
                    Loại sản phẩm:
        <asp:DropDownList ID="ddlLoaiSanpham" runat="server"></asp:DropDownList>
                </label>
            </div>
            <div class="form-group">
                <label>
                    Tên sản phẩm:
        <asp:TextBox ID="txtTensanpham" CssClass="form-control" placeholder="Tên sản phẩm" runat="server" />
                </label>
                <asp:RequiredFieldValidator ID="rfvTensanpham" ForeColor="Red" Display="Dynamic" ValidationGroup="validateSanpham" ControlToValidate="txtTensanpham" runat="server" ErrorMessage="* Tên sản phẩm không được để trống." />
            </div>
            <div class="form-group">
                <label>
                    Mô tả:
        <asp:TextBox ID="txtMota" CssClass="form-control" placeholder="Mô tả sản phẩm" runat="server" />
                </label>
            </div>
            <div class="form-group">
                <label>
                    Xuất xứ:
        <asp:TextBox ID="txtXuatxu" CssClass="form-control" placeholder="Xuất xứ sản phẩm" runat="server" />
                </label>
                <asp:RequiredFieldValidator ID="rfvXuatxu" ForeColor="Red" Display="Dynamic" ValidationGroup="validateSanpham" ControlToValidate="txtXuatxu" runat="server" ErrorMessage="* Xuất xứ không được để trống." />
            </div>
            <asp:Panel ID="panelNgungcungcap" CssClass="form-group" runat="server">
                <label>
                    Ngừng cung cấp:
         <asp:CheckBox ID="cbIsNgungcungcap" Width="35px" CssClass="ml-2  btn btn-dark" runat="server" />
                </label>
                <br />
                <asp:Label ForeColor="Red" ID="lblLoaiSanphamNayNgungkinhdoanh" runat="server"/>
            </asp:Panel>
            <asp:Button ID="btnThemSanpham" OnClick="btnThemSanpham_Click" CssClass="btn btn-success" ValidationGroup="validateSanpham" runat="server" Text="Thêm" />
            <asp:Button ID="btnCapnhatSanpham" OnClick="btnCapnhatSanpham_Click" CssClass="btn btn-success" ValidationGroup="validateSanpham" runat="server" Text="Cập nhật" />
        </div>
        <div class="col-4 row" style="min-width:100px;">
            <asp:Label ID="lblRgexSanpham" ForeColor="Red" runat="server"></asp:Label>
            <div class="col-12">
                <h5>Ảnh của sản phẩm:</h5>
                <asp:Image ID="privewimg" CssClass="img-fluid" Width="100%" ImageUrl="~/Images/No-image.png" runat="server" />
            </div>
            <label class="col-12 btn btn-light">
                Lựa chọn hình ảnh
            <i class="fas fa-images"></i>
            <asp:FileUpload ID="uploadImage" CssClass="d-none" onchange="showImage.call(this)" runat="server" />
                </label>
            <asp:Label ID="lblRegexUploadImage" runat="server" ForeColor="Red"></asp:Label>
            <p id="btnSaveScrop" class="btn btn-warning d-none col-12" onclick="saveImageCrop()">Lưu hình ảnh</p>
            <asp:HiddenField ID="hdImageUpload" runat="server" />
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyOutFormServer" runat="server">
</asp:Content>
