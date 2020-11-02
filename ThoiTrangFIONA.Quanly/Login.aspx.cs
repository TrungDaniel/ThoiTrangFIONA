using QuanSinhTo.Controller;
using QuanSinhTo.Models;
using System;
using System.Collections.Generic;

namespace QuanSinhTo.Quanly
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                NguoidungModel nguoidung = Session["User"] as NguoidungModel;
                if (nguoidung != null)
                {
                    if (nguoidung.PK_iNguoidungID > 0)
                    {
                        List<NguoidungModel> glstNguoidung = new TaiKhoanController().NguoidungGetbyPK(nguoidung.PK_iNguoidungID);
                        if (glstNguoidung.Count > 0)
                        {
                            Response.Redirect("~/Welcome.aspx");
                        }
                    }
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblRegexDangnhap.Text = "";
            NguoidungModel nguoidung = new TaiKhoanController().Login(txtTendangnhap.Text, txtPassword.Text);
            if (nguoidung != null)
            {
                Session["User"] = nguoidung;
                Response.Redirect("~/Welcome.aspx");
            }
            else
                lblRegexDangnhap.Text = "Tài khoản hoặc mật khẩu không chính xác.";
        }
    }
}