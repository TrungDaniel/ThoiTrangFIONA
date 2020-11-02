using QuanSinhTo.Controller;
using QuanSinhTo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;

namespace QuanSinhTo.Quanly
{
    public partial class ThemSuaSanpham : System.Web.UI.Page
    {
        private const string m_QUERYSUA = "ud";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (string.IsNullOrEmpty(Request.QueryString[m_QUERYSUA]))
                        hienthiDulieuKhiThemSanpham();
                    else
                        hienthiDulieuKhiSuaSanpham();
                }
                catch (Exception)
                {
                    Response.Redirect("~/DanhsachSanpham.aspx");
                }
            }
        }
        private void hienthiDulieuKhiThemSanpham()
        {
            ltrThemSuaSanpham.Text = "Thêm sản phẩm";
            panelNgungcungcap.Visible = false;
            btnCapnhatSanpham.Visible = false;
            btnThemSanpham.Visible = true;
            hienthiLoaisanpham();
        }
        private void hienthiDulieuKhiSuaSanpham()
        {
            ltrThemSuaSanpham.Text = "Cập nhật thông tin sản phẩm";
            panelNgungcungcap.Visible = true;
            btnThemSanpham.Visible = false;
            btnCapnhatSanpham.Visible = true;
            lblLoaiSanphamNayNgungkinhdoanh.Text = string.Empty;
            long sanphamID = Convert.ToInt64(Request.QueryString[m_QUERYSUA]);
            if (sanphamID < 1)
                throw new ApplicationException("Mã sản phẩm không hợp lệ.");
            SanphamController BrlSanpham = new SanphamController();
            List<SanphamModel> glstSanpham = BrlSanpham.SanphamGetbyPK(sanphamID);
            if (glstSanpham.Count < 1)
                throw new ApplicationException("Sản phẩm cần sửa không tồn tại.");
            txtTensanpham.Text = glstSanpham[0].sTenSanpham;
            txtXuatxu.Text = glstSanpham[0].sXuatxu;
            txtMota.Text = glstSanpham[0].sMota;
            privewimg.ImageUrl = string.Format("~/{0}", glstSanpham[0].sHinhanh);
            cbIsNgungcungcap.Checked = glstSanpham[0].isNgungcungcap;
            hienthiLoaisanpham();
            int loaiSanphamID = glstSanpham[0].FK_iLoaiSanphamID;
            if (loaiSanphamID > 0)
            {
                List<LoaiSanphamModel> glstLoaiSanpham = BrlSanpham.LoaiSanphamGetbyPK(loaiSanphamID);
                if (glstLoaiSanpham.Count > 0)
                {
                    if (glstLoaiSanpham[0].IsNgungKinhdoanh)
                        lblLoaiSanphamNayNgungkinhdoanh.Text = string.Format("Thông báo: Loại sản phẩm <b>\"{0}\"</b> đã ngừng kinh doanh,<br>nếu cập nhật sản phẩm này sẽ chuyển sang loại sản phẩm được chọn.", glstLoaiSanpham[0].sTenLoaiSanpham);
                    ddlLoaiSanpham.ClearSelection();
                    ListItem itemLoaisanpham = ddlLoaiSanpham.Items.FindByValue(loaiSanphamID.ToString());
                    if (itemLoaisanpham != null)
                        itemLoaisanpham.Selected = true;
                }
            }
        }
        private void hienthiLoaisanpham()
        {
            List<LoaiSanphamModel> glstLoaiSanpham = new SanphamController().LoaiSanphamGetbyPK(0);
            glstLoaiSanpham = glstLoaiSanpham.FindAll(lsp => !lsp.IsNgungKinhdoanh);
            glstLoaiSanpham.Sort((lsp1, lsp2) => lsp1.sTenLoaiSanpham.CompareTo(lsp2.sTenLoaiSanpham));
            ddlLoaiSanpham.DataSource = glstLoaiSanpham;
            ddlLoaiSanpham.DataTextField = "sTenLoaiSanpham";
            ddlLoaiSanpham.DataValueField = "PK_iLoaiSanphamID";
            ddlLoaiSanpham.DataBind();
        }
        private string Luuhinhanh(HiddenField hideninput)
        {
            if (string.IsNullOrEmpty(hideninput.Value))
                return string.Empty;
            string base64 = hideninput.Value.Split(',')[1];
            byte[] bytes = Convert.FromBase64String(base64);
            string filePath = string.Format("Images/Product/{0}.jpg", Path.GetRandomFileName());
            File.WriteAllBytes(Server.MapPath("~/" + filePath), bytes);
            return filePath;
        }
        protected void btnThemSanpham_Click(object sender, EventArgs e)
        {
            try
            {
                lblRgexSanpham.Text = string.Empty;
                lblRegexUploadImage.Text = string.Empty;
                string tenSP = txtTensanpham.Text;
                string mota = txtMota.Text;
                if (string.IsNullOrEmpty(mota))
                    mota = string.Empty;
                string xuatxu = txtXuatxu.Text;
                string hinhanh = string.IsNullOrEmpty(hdImageUpload.Value) ? string.Empty : "hinhanhtest";
                int loaispID = Convert.ToInt32(ddlLoaiSanpham.SelectedValue);
                SanphamController brlSanpham = new SanphamController();
                brlSanpham.ValidateSanpham(0, tenSP, mota, xuatxu, hinhanh, loaispID);
                try
                {
                    hinhanh = Luuhinhanh(hdImageUpload);
                }
                catch (Exception)
                {
                    lblRegexUploadImage.Text = "Vui lòng chọn tệp hành ảnh với phần mở rộng là png,jpg,jpeg.";
                    return;
                }
                if (string.IsNullOrEmpty(hinhanh))
                {
                    lblRegexUploadImage.Text = "Vui lòng chọn tệp hành ảnh với phần mở rộng là png,jpg,jpeg.";
                    return;
                }
                brlSanpham.ThemSanpham(tenSP, mota, xuatxu, hinhanh, loaispID);
                Response.Write(@"<script language='javascript'>alert('Thêm sản phẩm thành công.');</script>");
            }
            catch (Exception ex)
            {
                lblRgexSanpham.Text = ex.Message;
            }
        }
        protected void btnCapnhatSanpham_Click(object sender, EventArgs e)
        {
            long sanphamID = Convert.ToInt64(Request.QueryString[m_QUERYSUA]);
            try
            {

                lblRgexSanpham.Text = string.Empty;
                lblRegexUploadImage.Text = string.Empty;
                string tenSP = txtTensanpham.Text;
                string mota = txtMota.Text;
                if (string.IsNullOrEmpty(mota))
                    mota = string.Empty;
                string xuatxu = txtXuatxu.Text;
                string hinhanh = "hinhanhtest";
                int loaispID = Convert.ToInt32(ddlLoaiSanpham.SelectedValue);
                SanphamController brlSanpham = new SanphamController();
                brlSanpham.ValidateSanpham(sanphamID, tenSP, mota, xuatxu, hinhanh, loaispID);
                try
                {
                    hinhanh = Luuhinhanh(hdImageUpload);
                }
                catch (Exception)
                {
                    lblRegexUploadImage.Text = "Vui lòng chọn tệp hành ảnh với phần mở rộng là png,jpg,jpeg.";
                    return;
                }
                if(string.IsNullOrEmpty(hinhanh))
                    hinhanh= "hinhanhtest";
                brlSanpham.SuaSanpham(sanphamID, tenSP, mota, xuatxu, hinhanh, loaispID, cbIsNgungcungcap.Checked);
                hienthiDulieuKhiSuaSanpham();
                Response.Write(@"<script language='javascript'>alert('Cập nhật thành công.');</script>");
            }
            catch (Exception ex)
            {
                lblRgexSanpham.Text = ex.Message;
            }
        }
    }
}