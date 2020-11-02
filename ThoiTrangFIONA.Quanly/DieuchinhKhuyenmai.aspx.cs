using QuanSinhTo.Controller;
using QuanSinhTo.Models;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace QuanSinhTo.Quanly
{
    public partial class DieuchinhKhuyenmai : System.Web.UI.Page
    {
        private const string QUERYKHUYENMAI = "kmsp";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString[QUERYKHUYENMAI]))
                {
                    long sanphamID = 0;
                    long.TryParse(Request.QueryString[QUERYKHUYENMAI], out sanphamID);
                    if (sanphamID < 1)
                        Response.Redirect("~/DanhsachSanpham.aspx");
                    hienthiThongtinSanpham(sanphamID);
                    hienthiKhuyenmaiCuaSanpham(sanphamID);
                    ltrThemSuaKhuyenmai.Text = "Thêm khuyến mại mới cho sản phẩm:";
                }
                else
                    Response.Redirect("~/DanhsachSanpham.aspx");
            }
        }
        private void hienthiKhuyenmaiCuaSanpham(long sanphamID)
        {
            List<KhuyenmaiModel> glstKhuyenmaiCuaSanpham = new SanphamController().GetKhuyenmaiByFKiSanphamID(sanphamID);
            glstKhuyenmaiCuaSanpham.Sort((km1, km2) => km1.tNgaybatdau.CompareTo(km2.tNgaybatdau));
            rptKhuyenmaiCuaSanpham.DataSource = glstKhuyenmaiCuaSanpham;
            rptKhuyenmaiCuaSanpham.DataBind();
        }
        private void hienthiThongtinSanpham(long sanphamID)
        {
            SanphamController BRLsanpham = new SanphamController();
            List<SanphamModel> glstSanpham = BRLsanpham.SanphamGetbyPK(sanphamID);
            if (glstSanpham.Count < 1)
                Response.Redirect("~/DanhsachSanpham.aspx");
            int loaispID = glstSanpham[0].FK_iLoaiSanphamID;
            if (loaispID > 0)
            {
                List<LoaiSanphamModel> glstLoaiSanpham = BRLsanpham.LoaiSanphamGetbyPK(loaispID);
                if (glstSanpham.Count > 0)
                    lblLoaiSanpham.Text = glstLoaiSanpham[0].sTenLoaiSanpham;
            }
            imageSanpham.ImageUrl = string.Format("~/{0}", glstSanpham[0].sHinhanh);
            lblTenSanpham.Text = glstSanpham[0].sTenSanpham;
            decimal giaDangapdung = BRLsanpham.GetGiaDangApdungByFKSanphamID(glstSanpham[0].PK_iSanphamID);
            if (giaDangapdung > 0)
                lblGiaChuaKhuyenmai.Text = string.Format("{0:C0}", giaDangapdung);
            else
                lblGiaChuaKhuyenmai.Text = "Liên hệ";
        }
        protected void btnThemKhuyenmai_Click(object sender, EventArgs e)
        {
            lblRegexKhuyenmaiSanpham.Text = string.Empty;
            long sanphamID = 0;
            long.TryParse(Request.QueryString[QUERYKHUYENMAI], out sanphamID);
            try
            {
                new SanphamController().ThemKhuyenmai(Convert.ToInt32(txtKhuyenmai.Text), Convert.ToDateTime(txtNgayapdung.Text), Convert.ToDateTime(txtNgayKetthuc.Text), sanphamID);
                hienthiKhuyenmaiCuaSanpham(sanphamID);
                txtKhuyenmai.Text = string.Empty;
                txtNgayapdung.Text = string.Empty;
                txtNgayKetthuc.Text = string.Empty;
            }
            catch (ApplicationException ex)
            {
                lblRegexKhuyenmaiSanpham.Text = ex.Message;
            }
        }
        protected void rptKhuyenmaiCuaSanpham_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("SuaKhuyenmai"))
            {
                lblRegexKhuyenmaiSanpham.Text = string.Empty;
                long khuyenmaiID = Convert.ToInt64(e.CommandArgument);
                if (khuyenmaiID <= 0)
                    return;
                panelSuakhuyenmai.Visible = true;
                btnThemKhuyenmai.Visible = false;
                ltrThemSuaKhuyenmai.Text = "Chỉnh sửa khuyến mại cho sản phẩm:";
                List<KhuyenmaiModel> glstKhuyenmai = new SanphamController().GetKhuyenmaiByPK(khuyenmaiID);
                if (glstKhuyenmai.Count > 0)
                {
                    txtKhuyenmai.Attributes.Add("data-khuyenmai", glstKhuyenmai[0].PK_iKhuyenmaiID.ToString());
                    txtKhuyenmai.Text = Convert.ToInt32((glstKhuyenmai[0].fKhuyenmai * 100)).ToString();
                    txtNgayapdung.Text = string.Format("{0:yyyy-MM-ddTHH:mm}", glstKhuyenmai[0].tNgaybatdau);
                    txtNgayKetthuc.Text = string.Format("{0:yyyy-MM-ddTHH:mm}", glstKhuyenmai[0].tNgayketthuc);
                }

            }
            if (e.CommandName.Equals("XoaKhuyenmai"))
            {
                long khuyenmaiID = Convert.ToInt64(e.CommandArgument);
                if (khuyenmaiID <= 0)
                    return;
                new SanphamController().XoaKhuyenmai(khuyenmaiID);
                long sanphamID = 0;
                long.TryParse(Request.QueryString[QUERYKHUYENMAI], out sanphamID);
                if (sanphamID > 0)
                    hienthiKhuyenmaiCuaSanpham(sanphamID);
            }
        }
        protected void btnCapnhatGia_Click(object sender, EventArgs e)
        {
            lblRegexKhuyenmaiSanpham.Text = string.Empty;
            long khuyenmaiID = Convert.ToInt64(txtKhuyenmai.Attributes["data-khuyenmai"]);
            long sanphamID = 0;
            long.TryParse(Request.QueryString[QUERYKHUYENMAI], out sanphamID);
            try
            {
                if (khuyenmaiID > 0)
                    new SanphamController().SuaKhuyenmai(khuyenmaiID, Convert.ToInt32(txtKhuyenmai.Text), Convert.ToDateTime(txtNgayapdung.Text), Convert.ToDateTime(txtNgayKetthuc.Text), sanphamID);
                hienthiKhuyenmaiCuaSanpham(sanphamID);
                ltrThemSuaKhuyenmai.Text = "Thêm khuyến mại mới cho sản phẩm:";
                txtKhuyenmai.Text = string.Empty;
                txtNgayapdung.Text = string.Empty;
                txtNgayKetthuc.Text = string.Empty;
                btnThemKhuyenmai.Visible = true;
                panelSuakhuyenmai.Visible = false;
            }
            catch (ApplicationException ex)
            {
                lblRegexKhuyenmaiSanpham.Text = ex.Message;
            }
        }
        protected void btnHuyCapnhat_Click(object sender, EventArgs e)
        {
            lblRegexKhuyenmaiSanpham.Text = string.Empty;
            ltrThemSuaKhuyenmai.Text = "Thêm khuyến mại mới cho sản phẩm:";
            txtKhuyenmai.Text = string.Empty;
            txtNgayapdung.Text = string.Empty;
            txtNgayKetthuc.Text = string.Empty;
            btnThemKhuyenmai.Visible = true;
            panelSuakhuyenmai.Visible = false;
        }

    }
}