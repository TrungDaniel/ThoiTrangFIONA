using QuanSinhTo.Controller;
using QuanSinhTo.Models;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace QuanSinhTo.Quanly
{
    public partial class DanhsachSanpham : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hienthiLoaiSanpham();
                ddlLoaiSanpham_SelectedIndexChanged(sender, e);

            }
        }
        private void hienthiLoaiSanpham()
        {
            List<LoaiSanphamModel> glstLoaiSanpham = new SanphamController().LoaiSanphamGetbyPK(0);
            glstLoaiSanpham.Sort((lsp1, lsp2) => lsp1.sTenLoaiSanpham.CompareTo(lsp2.sTenLoaiSanpham));
            ddlLoaiSanpham.DataSource = glstLoaiSanpham;
            ddlLoaiSanpham.DataTextField = "sTenLoaiSanpham";
            ddlLoaiSanpham.DataValueField = "PK_iLoaiSanphamID";
            ddlLoaiSanpham.DataBind();
            ddlLoaiSanpham.Items.Insert(0, new ListItem("Tất cả", "0"));
        }
        protected void ddlLoaiSanpham_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblThongbaoLoaiSanphamNgungKinhdoanh.Text = string.Empty;
            SanphamController brlSanpham = new SanphamController();
            List<SanphamModel> glstSanpham;
            int loaiSanphamID = Convert.ToInt32(ddlLoaiSanpham.SelectedValue);
            if (loaiSanphamID < 1)
                glstSanpham = brlSanpham.SanphamGetbyPK(0);
            else
            {
                glstSanpham = brlSanpham.SanphamGetbyFK_iloaiSanphamID(loaiSanphamID);
                List<LoaiSanphamModel> glstLoaisanpham = brlSanpham.LoaiSanphamGetbyPK(loaiSanphamID);
                if (glstLoaisanpham.Count > 0)
                {
                    if (glstLoaisanpham[0].IsNgungKinhdoanh)
                        lblThongbaoLoaiSanphamNgungKinhdoanh.Text = string.Format("Thông báo: Loại sản phẩm <b>\"{0}\"</b> này đã ngừng kinh doanh.", glstLoaisanpham[0].sTenLoaiSanpham);
                }

            }
            glstSanpham.Sort((sp1, sp2) => sp1.sTenSanpham.CompareTo(sp2.sTenSanpham));
            rptSanpham.DataSource = glstSanpham;
            rptSanpham.DataBind();
        }
        protected void rptSanpham_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("xoaSanpham"))
            {
                try
                {
                    long sanphamid = Convert.ToInt64(e.CommandArgument);
                    new SanphamController().XoaSanpham(sanphamid);
                    ddlLoaiSanpham_SelectedIndexChanged(source, e);
                }
                catch (ApplicationException ex)
                {
                    Response.Write(string.Format("<script language='javascript'>alert('{0}');</script>", ex.Message));//@"");
                }
            }
        }
        protected void rptSanpham_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SanphamModel sanpham = e.Item.DataItem as SanphamModel;
                if (sanpham != null)
                {
                    Image imgSanpham = e.Item.FindControl("imgSanpham") as Image;
                    if (imgSanpham != null)
                        imgSanpham.ImageUrl = string.Format("~/{0}", sanpham.sHinhanh);
                    Literal ltrGiaDangApdung = e.Item.FindControl("ltrGiaDangApdung") as Literal;
                    SanphamController brlSanpham = new SanphamController();
                    if (ltrGiaDangApdung != null)
                    {
                        decimal giaDangApdung = brlSanpham.GetGiaDangApdungByFKSanphamID(sanpham.PK_iSanphamID);
                        ltrGiaDangApdung.Text = giaDangApdung < 0 ? "Liên hệ" : giaDangApdung.ToString("C0");
                    }
                    Literal ltrLoaiSanpham = e.Item.FindControl("ltrLoaiSanpham") as Literal;
                    if (ltrLoaiSanpham != null)
                    {
                        int loaiSanphamID = sanpham.FK_iLoaiSanphamID;
                        if (loaiSanphamID > 0)
                        {
                            List<LoaiSanphamModel> glstLoaisanpham = brlSanpham.LoaiSanphamGetbyPK(loaiSanphamID);
                            if (glstLoaisanpham.Count > 0)
                                ltrLoaiSanpham.Text = glstLoaisanpham[0].sTenLoaiSanpham;
                        }
                    }
                    Literal ltrKhuyenmaiDangApdung = e.Item.FindControl("ltrKhuyenmaiDangApdung") as Literal;
                    if (ltrKhuyenmaiDangApdung != null)
                    {
                        int khuyenmaiDangapdung = Convert.ToInt32(brlSanpham.GetKhuyenmaiDangApdungByFKSanphamID(sanpham.PK_iSanphamID) * 100);
                        ltrKhuyenmaiDangApdung.Text = khuyenmaiDangapdung < 0 ? string.Empty : string.Format("{0}%", khuyenmaiDangapdung);
                    }
                    HyperLink hrefDieuChinhgia = e.Item.FindControl("hrefDieuChinhgia") as HyperLink;
                    if (hrefDieuChinhgia != null)
                        hrefDieuChinhgia.NavigateUrl = string.Format("~/DieuchinhGia.aspx?gsp={0}", sanpham.PK_iSanphamID);
                    HyperLink hrefDieuChinhKhuyenmai = e.Item.FindControl("hrefDieuChinhKhuyenmai") as HyperLink;
                    if (hrefDieuChinhKhuyenmai != null)
                        hrefDieuChinhKhuyenmai.NavigateUrl = string.Format("~/DieuchinhKhuyenmai.aspx?kmsp={0}", sanpham.PK_iSanphamID);
                    HyperLink hrefCongthuc = e.Item.FindControl("hrefCongthuc") as HyperLink;
                    if (hrefCongthuc != null)
                        hrefCongthuc.NavigateUrl = string.Format("~/Congthuc.aspx?ctsp={0}", sanpham.PK_iSanphamID);
                }//sanpham
            }//ItemType
        }
    }
}