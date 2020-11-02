using QuanSinhTo.Controller;
using QuanSinhTo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanSinhTo.Quanly
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["User"] != null)
                {
                    NguoidungModel nguoidung = Session["User"] as NguoidungModel;
                    if (nguoidung != null)
                    {
                        if (nguoidung.PK_iNguoidungID > 0)
                        {
                            List<NguoidungModel> glstNguoidung = new TaiKhoanController().NguoidungGetbyPK(nguoidung.PK_iNguoidungID);
                            if (glstNguoidung.Count < 1)
                            {
                                Response.Redirect("~/Login.aspx");
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
    }
}