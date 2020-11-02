using QuanSinhTo.Controller;
using QuanSinhTo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanSinhTo.Quanly
{
    public partial class LoaiSanpham : System.Web.UI.Page, System.Web.UI.ICallbackEventHandler
    {
        #region khai báo biến cho CallbackEventHandler
        private string m_KetquaCallback = string.Empty;
        private string m_CapnhatLoaisanpham;
        private string m_XoaLoaisanpham;

        public string XoaLoaisanpham
        {
            get { return m_XoaLoaisanpham; }
        }
        public string CapnhatLoaisanpham
        {
            get { return m_CapnhatLoaisanpham; }
        }
        /// <summary>
        /// gán các client script cho string
        /// </summary>
        private void RegisterCallBackEvents()
        {
            m_CapnhatLoaisanpham = Page.ClientScript.GetCallbackEventReference
              (this, "eventArg", "traVeCapnhatLoaisanpham", "context", "null", true);
            m_XoaLoaisanpham = Page.ClientScript.GetCallbackEventReference
              (this, "eventArg", "traVeXoaloaisanpham", "context", "null", true);
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hienLoaiSanpham();
            }
            RegisterCallBackEvents();
        }
        private void hienLoaiSanpham()
        {
            List<LoaiSanphamModel> glstLoaiSanpham = new SanphamController().LoaiSanphamGetbyPK(0);
            glstLoaiSanpham.Sort((lsp1, lsp2) => lsp1.sTenLoaiSanpham.CompareTo(lsp2.sTenLoaiSanpham));
            rptLoaiSanpham.DataSource = glstLoaiSanpham;
            rptLoaiSanpham.DataBind();
        }
        protected void btnThemloaiSanpham_Click(object sender, EventArgs e)
        {
            try
            {
                lblThongbaothem.Visible = false;
                (new SanphamController()).ThemLoaiSanpham(txtTenloaiSP.Text, txtMotaloaiSP.Text);
                hienLoaiSanpham();
                txtTenloaiSP.Text = string.Empty;
                txtMotaloaiSP.Text = string.Empty;
            }
            catch (ApplicationException ex)
            {
                lblThongbaothem.Visible = true;
                lblThongbaothem.Text = ex.Message;
            }
        }
        public string GetCallbackResult()
        {
            return m_KetquaCallback;
        }
        public void RaiseCallbackEvent(string eventArgument)
        {
            string[] arrayKetQua = eventArgument.Split('♣');
            string dieuKienReNhanh = arrayKetQua[0];
            switch (dieuKienReNhanh)
            {
                case "CapnhatLoaisanpham":
                    try
                    {
                        new SanphamController().SuaLoaiSanpham(Convert.ToInt32(arrayKetQua[1]), arrayKetQua[2], arrayKetQua[3], Convert.ToBoolean(arrayKetQua[4]));
                        hienLoaiSanpham();
                        StringBuilder sbControlHtml = new StringBuilder();
                        using (StringWriter stringWriter = new StringWriter())
                        {
                            using (HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter))
                            {
                                rptLoaiSanpham.RenderControl(htmlWriter);
                                sbControlHtml.Append(stringWriter.ToString());
                            }
                        }
                        m_KetquaCallback = string.Format("thanhcong♣{0}", sbControlHtml.ToString());
                    }
                    catch (Exception ex)
                    {
                        m_KetquaCallback = string.Format("thatbai♣{0}", ex.Message);
                    }
                    break;
                case "XoaloaiSanpham":
                    try
                    {
                        new SanphamController().XoaLoaisanpham(Convert.ToInt32(arrayKetQua[1]));
                        hienLoaiSanpham();
                        StringBuilder sbControlHtml = new StringBuilder();
                        using (StringWriter stringWriter = new StringWriter())
                        {
                            using (HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter))
                            {
                                rptLoaiSanpham.RenderControl(htmlWriter);
                                sbControlHtml.Append(stringWriter.ToString());
                            }
                        }
                        m_KetquaCallback = string.Format("thanhcong♣{0}", sbControlHtml.ToString());
                    }
                    catch (Exception ex)
                    {
                        m_KetquaCallback = string.Format("thatbai♣{0}", ex.Message);
                    }
                    break;
            }
        }
    }
}