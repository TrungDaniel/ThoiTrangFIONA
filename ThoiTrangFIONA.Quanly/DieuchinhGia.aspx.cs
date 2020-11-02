using QuanSinhTo.Controller;
using QuanSinhTo.Models;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace QuanSinhTo.Quanly
{
    public partial class DieuchinhGia : System.Web.UI.Page
    {
        private const string QUERYGIA = "gsp";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString[QUERYGIA]))
                {
                    long sanphamID = 0;
                    long.TryParse(Request.QueryString[QUERYGIA], out sanphamID);
                    if (sanphamID < 1)
                        Response.Redirect("~/DanhsachSanpham.aspx");
                    txtNgayapdung.Text = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
                    hienthiThongtinSanpham(sanphamID);
                    hienthiGiaCuaSanpham(sanphamID);
                    ltrThemSuaGia.Text = "Thêm giá mới cho sản phẩm:";
                }
                else
                    Response.Redirect("~/DanhsachSanpham.aspx");
            }
        }
        private void hienthiGiaCuaSanpham(long sanphamID)
        {
            List<BanggiaModel> glstGiaCuaSanpham = new SanphamController().GetBanggiaByFKiSanphamID(sanphamID);
            glstGiaCuaSanpham.Sort((gia1, gia2) => gia1.tNgaybatdauapdung.CompareTo(gia2.tNgaybatdauapdung));
            rptGiaCuaSanpham.DataSource = glstGiaCuaSanpham;
            rptGiaCuaSanpham.DataBind();
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
        }
        protected void btnThemgia_Click(object sender, EventArgs e)
        {
            lblRegexThemgiaSanpham.Text = string.Empty;
            long sanphamID = 0;
            long.TryParse(Request.QueryString[QUERYGIA], out sanphamID);
            try
            {
                new SanphamController().ThemBanggia(Convert.ToDecimal(txtGia.Text) * 1000, Convert.ToDateTime(txtNgayapdung.Text), sanphamID);
                hienthiGiaCuaSanpham(sanphamID);
                txtGia.Text = string.Empty;
                txtNgayapdung.Text = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
            }
            catch (ApplicationException ex)
            {
                lblRegexThemgiaSanpham.Text = ex.Message;
            }
        }
        protected void rptGiaCuaSanpham_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Suagia"))
            {
                lblRegexThemgiaSanpham.Text = string.Empty;
                long banggiaID = Convert.ToInt64(e.CommandArgument);
                if (banggiaID <= 0)
                    return;
                panelSuagia.Visible = true;
                btnThemgia.Visible = false;
                ltrThemSuaGia.Text = "Cập nhật giá sản phẩm:";
                List<BanggiaModel> glstBanggia = new SanphamController().GetBanggiaByPK(banggiaID);
                if (glstBanggia.Count > 0)
                {
                    txtGia.Attributes.Add("data-bangia", glstBanggia[0].PK_iBanggiaID.ToString());
                    txtGia.Text = (glstBanggia[0].mGia / 1000).ToString();
                    txtNgayapdung.Text = string.Format("{0:yyyy-MM-ddTHH:mm}", glstBanggia[0].tNgaybatdauapdung);
                }

            }
            if (e.CommandName.Equals("Xoagia"))
            {
                long banggiaID = Convert.ToInt64(e.CommandArgument);
                if (banggiaID <= 0)
                    return;
                new SanphamController().XoaBanggia(banggiaID);
                long sanphamID = 0;
                long.TryParse(Request.QueryString[QUERYGIA], out sanphamID);
                if (sanphamID > 0)
                    hienthiGiaCuaSanpham(sanphamID);
            }
        }
        protected void btnCapnhatGia_Click(object sender, EventArgs e)
        {
            lblRegexThemgiaSanpham.Text = string.Empty;
            long banggiaID = Convert.ToInt64(txtGia.Attributes["data-bangia"]);
            long sanphamID = 0;
            long.TryParse(Request.QueryString[QUERYGIA], out sanphamID);
            try
            {
                new SanphamController().SuaBanggia(banggiaID, Convert.ToDecimal(txtGia.Text) * 1000, Convert.ToDateTime(txtNgayapdung.Text), sanphamID);
                hienthiGiaCuaSanpham(sanphamID);
                ltrThemSuaGia.Text = "Thêm giá mới cho sản phẩm:";
                txtGia.Text = string.Empty;
                txtNgayapdung.Text = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
                btnThemgia.Visible = true;
                panelSuagia.Visible = false;
            }
            catch (ApplicationException ex)
            {
                lblRegexThemgiaSanpham.Text = ex.Message;
            }
        }
        protected void btnHuyCapnhat_Click(object sender, EventArgs e)
        {
            lblRegexThemgiaSanpham.Text = string.Empty;
            ltrThemSuaGia.Text = "Thêm giá mới cho sản phẩm:";
            txtGia.Text = string.Empty;
            txtNgayapdung.Text = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
            btnThemgia.Visible = true;
            panelSuagia.Visible = false;
        }
    }
}