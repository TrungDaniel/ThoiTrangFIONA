using QuanSinhTo.Controller;
using QuanSinhTo.Models;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace QuanSinhTo.Quanly
{
    public partial class Taikhoan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hienthiQuyen();
                hienthiTaikhoan();
            }
        }

        private void hienthiQuyen()
        {
            List<QuyenModel> glstQuyen = new TaiKhoanController().QuyenGetbyPK(0);
            ddlQuyen.DataSource = glstQuyen;
            ddlQuyen.DataValueField = "PK_iQuyenID";
            ddlQuyen.DataTextField = "sTenQuyen";
            ddlQuyen.DataBind();
        }

        private void hienthiTaikhoan()
        {
            List<NguoidungModel> glstNguoidung = new TaiKhoanController().NguoidungGetbyPK(0);
            glstNguoidung.Sort((nd1, nd2) => nd1.sTennguoidung.CompareTo(nd2.sTennguoidung));
            rptTaikhoan.DataSource = glstNguoidung;
            rptTaikhoan.DataBind();
        }

        protected void rptTaikhoan_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Sua")
            {
                long userID = Convert.ToInt64(e.CommandArgument);
                if (userID < 1)
                    lblRegexTaikhoan.Text = "Mã người dùng không hợp lệ.";
                else
                {
                    TaiKhoanController taikhoanController = new TaiKhoanController();
                    List<NguoidungModel> glstNguoidung = taikhoanController.NguoidungGetbyPK(userID);
                    if (glstNguoidung.Count > 0)
                    {
                        List<PhanquyenModel> glstPhanquyen = taikhoanController.PhanquyenGetbyFK_iNguoidungID(glstNguoidung[0].PK_iNguoidungID);
                        if (glstPhanquyen.Count > 0)
                        {
                            List<QuyenModel> glstQuyen = taikhoanController.QuyenGetbyPK(glstPhanquyen[0].FK_iQuyenID);
                            if (glstQuyen.Count > 0)
                            {
                                ListItem item = ddlQuyen.Items.FindByValue(glstQuyen[0].PK_iQuyenID.ToString());
                                if (item != null)
                                    item.Selected = true;
                            }
                        }
                        txtTenDangnhap.Text = glstNguoidung[0].sTennguoidung;
                        txtTenNhanvien.Text = glstNguoidung[0].sTenNhanvien;
                        txtMatkhau.Text = "";
                        btnCapnhat.CommandArgument = userID.ToString();
                        btnThem.Visible = false;
                        btnCapnhat.Visible = true;
                    }
                }


            }
            if (e.CommandName == "Xoa")
            {
                //tạm thời không xóa
            }
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                TaiKhoanController taikhoanController = new TaiKhoanController();
                long userID = taikhoanController.ThemTaiKhoan(txtTenDangnhap.Text, txtTenNhanvien.Text, txtMatkhau.Text);
                taikhoanController.ThemPhanquyen(Convert.ToInt32(ddlQuyen.SelectedValue), userID);
                btnHuy_Click(sender, e);
                hienthiQuyen();
                hienthiTaikhoan();
            }
            catch (Exception ex)
            {
                lblRegexTaikhoan.Text = ex.Message;
            }
        }

        protected void btnCapnhat_Click(object sender, EventArgs e)
        {
            try
            {
                long userID = Convert.ToInt64(((Button)sender).CommandArgument);
                new TaiKhoanController().SuaTaiKhoan(userID, txtTenDangnhap.Text, txtTenNhanvien.Text, txtMatkhau.Text);
                btnHuy_Click(sender, e);
                hienthiQuyen();
                hienthiTaikhoan();
            }
            catch (Exception ex)
            {
                lblRegexTaikhoan.Text = ex.Message;
            }
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            txtTenDangnhap.Text = "";
            txtTenNhanvien.Text = "";
            txtMatkhau.Text = "";
            btnThem.Visible = true;
            btnCapnhat.Visible = false;
        }

        protected void rptTaikhoan_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                NguoidungModel nguoidung = e.Item.DataItem as NguoidungModel;
                if (nguoidung != null)
                {
                    Literal ltrQuyen = e.Item.FindControl("ltrQuyen") as Literal;
                    if (ltrQuyen != null)
                    {
                        TaiKhoanController taikhoanController = new TaiKhoanController();
                        List<PhanquyenModel> glstPhanquyen = taikhoanController.PhanquyenGetbyFK_iNguoidungID(nguoidung.PK_iNguoidungID);
                        if (glstPhanquyen.Count > 0)
                        {
                            List<QuyenModel> glstQuyen = taikhoanController.QuyenGetbyPK(glstPhanquyen[0].FK_iQuyenID);
                            if (glstQuyen.Count > 0)
                                ltrQuyen.Text = glstQuyen[0].sTenQuyen;
                        }
                    }
                }
            }
        }
    }
}