using QuanSinhTo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuanSinhTo.Controller
{
    public class SanphamController
    {
        public decimal GetGiaDangApdungByFKSanphamID(long sanphamID)
        {
            List<BanggiaModel> glstGiaCuaSanpham = GetBanggiaByFKiSanphamID(sanphamID);
            glstGiaCuaSanpham = glstGiaCuaSanpham.FindAll(gia => gia.tNgaybatdauapdung <= DateTime.Now);
            glstGiaCuaSanpham.Sort((gia1, gia2) => gia2.tNgaybatdauapdung.CompareTo(gia1.tNgaybatdauapdung));
            if (glstGiaCuaSanpham.Count < 1)
                return -1;
            return glstGiaCuaSanpham[0].mGia;
        }
        public List<BanggiaModel> GetBanggiaByFKiSanphamID(long fK_iSanphamID)
        {
            if (fK_iSanphamID < 1)
                throw new Exception("Mã sản phẩm không hợp lệ.");
            List<BanggiaModel> glstBanggia= (new db_QuanSinhToEntities()).spBanggia_GetByPK(0).ToList();
            glstBanggia = glstBanggia.FindAll(banggia => banggia.FK_iSanphamID == fK_iSanphamID);
            return glstBanggia;
        }
        public List<SanphamModel> SanphamGetbyPK(long sanphamID)
        {
            if (sanphamID < 0)
                throw new Exception("Mã sản phẩm không hợp lệ.");
            return new db_QuanSinhToEntities().spSanpham_GetByPK(sanphamID).ToList();
        }
        public List<SanphamModel> SanphamGetbyFK_iloaiSanphamID(int loaiSanphamID)
        {
            if (loaiSanphamID < 1)
                throw new Exception("Mã loại sản phẩm không hợp lệ.");
            return new db_QuanSinhToEntities().spSanpham_GetByFK_iLoaiSanphamID(loaiSanphamID).ToList();
        }
        public bool XoaSanpham(long sanphamID)
        {
            if (sanphamID < 1)
                throw new Exception("Mã sản phẩm không hợp lệ.");
            db_QuanSinhToEntities dalSanpham = new db_QuanSinhToEntities();
            if (dalSanpham.spSanpham_GetByPK(sanphamID).ToList().Count < 1)
                throw new Exception("Không tồn tại sản phẩm cần xóa.");
            if (dalSanpham.spChitietHoadonxuat_GetByFK_iSanphamID(sanphamID).ToList().Count > 0)
                throw new Exception("Sản phẩm này đã được bán ra không thể xóa, nếu sản phẩm đã ngừng kinh doanh vui lòng lựa chọn ngừng kinh doanh.");
            List<BanggiaModel> glstBanggiaCuaSanpham = dalSanpham.spBanggia_GetByPK(0).ToList();
            glstBanggiaCuaSanpham = glstBanggiaCuaSanpham.FindAll(bangia => bangia.FK_iSanphamID == sanphamID);
            foreach (BanggiaModel banggia in glstBanggiaCuaSanpham)
                dalSanpham.spBanggia_DeleteByPK(banggia.PK_iBanggiaID);
            List<KhuyenmaiModel> glstKhuyenmai = dalSanpham.spKhuyenmai_GetByFK_iSanphamID(sanphamID).ToList();
            foreach (KhuyenmaiModel khuyenmai in glstKhuyenmai)
                dalSanpham.spKhuyenmai_DeleteByPK(khuyenmai.PK_iKhuyenmaiID);
            return dalSanpham.spSanpham_DeleteByPK(sanphamID)>0?true:false;
        }
        public List<LoaiSanphamModel> LoaiSanphamGetbyPK(int loaisanphamID)
        {
            if (loaisanphamID < 0)
                throw new Exception("Mã loại sản phẩm không hợp lệ.");
            return new db_QuanSinhToEntities().spLoaiSanpham_GetByPK(loaisanphamID).ToList();
        }
        public void ValidateSanpham(long sanphamID, string sTenSanpham, string sMota, string sXuatxu, string sHinhanh, int FK_iLoaiSanphamID)
        {
            if (string.IsNullOrEmpty(sTenSanpham))
                throw new Exception("Tên sản phẩm không được để trống.");
            if (string.IsNullOrEmpty(sXuatxu))
                throw new Exception("Xuất xứ không được để trống.");
            if (FK_iLoaiSanphamID < 1)
                throw new Exception("Mã loại sản phẩm không hợp lệ.");
            List<LoaiSanphamModel> glstLoaisanpham = LoaiSanphamGetbyPK(FK_iLoaiSanphamID);
            if (glstLoaisanpham.Count < 1)
                throw new Exception("Loại sản phẩm không tồn tại.");
            List<SanphamModel> glstSanpham = SanphamGetbyPK(0);
            if (glstSanpham.FindAll(sanpham => sanpham.sTenSanpham.Trim().ToUpper().Equals(sTenSanpham.Trim().ToUpper()) && sanpham.PK_iSanphamID != sanphamID).Count > 0)
                throw new Exception(string.Format("Đã có sản phẩm khác có tên là {0}.", sTenSanpham));
        }
        public long ThemSanpham(string sTenSanpham, string sMota, string sXuatxu, string sHinhanh, int FK_iLoaiSanphamID)
        {
            ValidateSanpham(0, sTenSanpham, sMota, sXuatxu, sHinhanh, FK_iLoaiSanphamID);
            return Convert.ToInt64(new db_QuanSinhToEntities().spSanpham_Insert(sTenSanpham, sMota, 0, sXuatxu, sHinhanh, FK_iLoaiSanphamID, false, DateTime.Now, DateTime.Now.AddYears(999)).FirstOrDefault());
        }
        public bool SuaSanpham(long sanphamID, string sTenSanpham, string sMota, string sXuatxu, string sHinhanh, int FK_iLoaiSanphamID, bool IsNgungCungcap)
        {
            if (sanphamID < 1)
                throw new Exception("Mã sản phẩm không hợp lệ.");
            List<SanphamModel> glstSanpham = SanphamGetbyPK(sanphamID);
            if (glstSanpham.Count < 1)
                throw new Exception("Sản phẩm không tồn tại.");
            ValidateSanpham(sanphamID, sTenSanpham, sMota, sXuatxu, sHinhanh, FK_iLoaiSanphamID);
            if (sHinhanh.Equals("hinhanhtest"))
                sHinhanh = glstSanpham[0].sHinhanh;
            return new db_QuanSinhToEntities().spSanpham_UpdateByPK(sanphamID, sTenSanpham, sMota, 0, sXuatxu, sHinhanh, FK_iLoaiSanphamID, IsNgungCungcap) > 0 ? true : false;

        }
        private void validateLoaiSanpham(int loaiSanphamID, string sTenLoaiSanpham, string sMota)
        {
            if (string.IsNullOrEmpty(sTenLoaiSanpham))
                throw new Exception("Tên loại sản phẩm không được để trống.");
            if (sMota == null)
                throw new Exception("Mô tả không được null hãy thử string.Empty.");
            List<LoaiSanphamModel> glstLoaiSanpham = LoaiSanphamGetbyPK(0);
            if (glstLoaiSanpham.FindAll(loaiSanpham => loaiSanpham.sTenLoaiSanpham.Trim().ToUpper().Equals(sTenLoaiSanpham.Trim().ToUpper()) && loaiSanpham.PK_iLoaiSanphamID != loaiSanphamID).Count > 0)
                throw new Exception(string.Format("Đã có loại sản phẩm khác có tên là {0}.", sTenLoaiSanpham));
        }
        public int ThemLoaiSanpham(string tenloaiSanpham, string mota)
        {
            validateLoaiSanpham(0, tenloaiSanpham, mota);
            return Convert.ToInt32(new db_QuanSinhToEntities().spLoaiSanpham_Insert(tenloaiSanpham, mota, false).FirstOrDefault());
        }
        public bool SuaLoaiSanpham(int loaiSanphamID, string tenloaiSanpham, string mota, bool IsNgungkinhDoanh)
        {
            if (loaiSanphamID < 1)
                throw new Exception("Mã loại sản phẩm không hợp lệ.");
            if (LoaiSanphamGetbyPK(loaiSanphamID).Count < 1)
                throw new Exception("Loại sản phẩm không tồn tại.");
            validateLoaiSanpham(loaiSanphamID, tenloaiSanpham, mota);
            return new db_QuanSinhToEntities().spLoaiSanpham_UpdateByPK(loaiSanphamID, tenloaiSanpham, mota, IsNgungkinhDoanh) > 0 ? true : false;
        }
        public List<KhuyenmaiModel> GetKhuyenmaiByFKiSanphamID(long fK_iSanphamID)
        {
            if (fK_iSanphamID < 1)
                throw new Exception("Mã sản phẩm không hợp lệ.");
            return (new db_QuanSinhToEntities()).spKhuyenmai_GetByFK_iSanphamID(fK_iSanphamID).ToList();
        }
        public float GetKhuyenmaiDangApdungByFKSanphamID(long sanphamID)
        {
            List<KhuyenmaiModel> glstKhuyenmaiCuaSanpham = GetKhuyenmaiByFKiSanphamID(sanphamID);
            glstKhuyenmaiCuaSanpham = glstKhuyenmaiCuaSanpham.FindAll(km => km.tNgaybatdau <= DateTime.Now && km.tNgayketthuc > DateTime.Now);
            if (glstKhuyenmaiCuaSanpham.Count > 0)
                return Convert.ToSingle(glstKhuyenmaiCuaSanpham[0].fKhuyenmai);
            else
                return -1;
        }
        private void validateBanggia(long banggiaID, decimal fGia, DateTime tNgaybatdauapdung, long fK_iSanphamID)
        {
            if (fGia < 0)
                throw new Exception("Giá không thể nhỏ hơn 0.");
            if (tNgaybatdauapdung < DateTime.Now.AddMinutes(-1) && banggiaID == 0)
                throw new Exception("Ngày bắt đầu áp dụng không thể nhỏ hơn ngày hiện tại.");
            if (fK_iSanphamID < 1)
                throw new Exception("Mã sản phẩm không hợp lệ.");
            if (SanphamGetbyPK(fK_iSanphamID).Count < 1)
                throw new Exception("Sản phẩm không tồn tại.");
        }
        public long ThemBanggia(decimal fGia, DateTime tNgaybatdauapdung, long fK_iSanphamID)
        {
            validateBanggia(0, fGia, tNgaybatdauapdung, fK_iSanphamID);
            return Convert.ToInt64((new db_QuanSinhToEntities()).spBanggia_Insert(fGia, tNgaybatdauapdung, fK_iSanphamID).FirstOrDefault());
        }
        public List<BanggiaModel> GetBanggiaByPK(long bangGiaID)
        {
            if (bangGiaID < 0)
                throw new Exception("Mã bảng giá không hợp lệ.");
            return new db_QuanSinhToEntities().spBanggia_GetByPK(bangGiaID).ToList();
        }
        public bool XoaBanggia(long banggiaID)
        {
            if (banggiaID < 1)
                throw new Exception("Mã bảng giá không hợp lệ.");
            List<BanggiaModel> glstBanggia = GetBanggiaByPK(banggiaID);
            if (glstBanggia.Count < 1)
                throw new Exception("Bảng giá không tồn tại.");
            return (new db_QuanSinhToEntities()).spBanggia_DeleteByPK(banggiaID)>0?true:false;
        }
        public bool SuaBanggia(long banggiaID, decimal fGia, DateTime tNgaybatdauapdung, long fK_iSanphamID)
        {
            if (banggiaID < 1)
                throw new Exception("Mã bảng giá không hợp lệ.");
            if (GetBanggiaByPK(banggiaID).Count < 1)
                throw new Exception("Không tồn tại giá cần sửa.");
            validateBanggia(banggiaID, fGia, tNgaybatdauapdung, fK_iSanphamID);
            return new db_QuanSinhToEntities().spBanggia_UpdateByPK(banggiaID, fGia, tNgaybatdauapdung, fK_iSanphamID)>0?true:false;
        }
        private void validateKhuyenmai(long pK_iKhuyenmaiID, int iKhuyenmai, DateTime tNgaybatdau, DateTime tNgayketthuc, long fK_iSanphamID)
        {
            if (fK_iSanphamID < 1)
                throw new Exception("Mã sản phẩm không hợp lệ.");
            if (SanphamGetbyPK(fK_iSanphamID).Count < 1)
                throw new Exception("Sản phẩm không tồn tại.");
            if (tNgaybatdau < DateTime.Now && pK_iKhuyenmaiID == 0)
                throw new Exception("Ngày bắt đầu khuyến mại không thể nhỏ hơn ngày hiện tại.");
            if (tNgayketthuc < tNgaybatdau)
                throw new Exception("Ngày kết thúc không được nhỏ hơn ngày bắt đầu.");
            if (iKhuyenmai <= 0 || iKhuyenmai > 100)
                throw new Exception("Khuyến mại phải nằm trong khoảng [0-100]");
            List<KhuyenmaiModel> glstKhuyenmai = GetKhuyenmaiByFKiSanphamID(fK_iSanphamID);
            glstKhuyenmai = glstKhuyenmai.FindAll(km => km.tNgayketthuc >= tNgaybatdau);
            if (glstKhuyenmai.Count > 0)
                if (pK_iKhuyenmaiID != glstKhuyenmai[0].PK_iKhuyenmaiID)
                    throw new Exception("Ngày bắt đầu của khuyến mại phải lớn hơn ngày kết thúc của khuyến mại cũ.");
        }
        public long ThemKhuyenmai(int iKhuyenmai, DateTime tNgaybatdau, DateTime tNgayketthuc, long fK_iSanphamID)
        {
            validateKhuyenmai(0, iKhuyenmai, tNgaybatdau, tNgayketthuc, fK_iSanphamID);
            float fKhuyenmai = (Convert.ToSingle(iKhuyenmai) / 100);
            return Convert.ToInt64((new db_QuanSinhToEntities()).spKhuyenmai_Insert(fKhuyenmai, tNgaybatdau, tNgayketthuc, fK_iSanphamID).FirstOrDefault());
        }
        public List<KhuyenmaiModel> GetKhuyenmaiByPK(long pk_iKhuyenmaiID)
        {
            if (pk_iKhuyenmaiID < 0)
                throw new Exception("Mã khuyến mại không hợp lệ.");
            return (new db_QuanSinhToEntities()).spKhuyenmai_GetByPK(pk_iKhuyenmaiID).ToList();
        }
        public bool SuaKhuyenmai(long pK_iKhuyenmaiID, int iKhuyenmai, DateTime tNgaybatdau, DateTime tNgayketthuc, long fK_iSanphamID)
        {
            if (pK_iKhuyenmaiID < 1)
                throw new Exception("Mã khuyến mại không hợp lệ.");
            if (GetKhuyenmaiByPK(pK_iKhuyenmaiID).Count < 1)
                throw new Exception("Không tồn tại khuyến mại cần sửa.");
            validateKhuyenmai(pK_iKhuyenmaiID, iKhuyenmai, tNgaybatdau, tNgayketthuc, fK_iSanphamID);
            float fKhuyenmai = (Convert.ToSingle(iKhuyenmai) / 100);
            return new db_QuanSinhToEntities().spKhuyenmai_UpdateByPK(pK_iKhuyenmaiID, fKhuyenmai, tNgaybatdau, tNgayketthuc, fK_iSanphamID)>0?true:false;
        }
        public bool XoaKhuyenmai(long pk_iKhuyenmaiID)
        {
            if (pk_iKhuyenmaiID < 1)
                throw new Exception("Mã khuyến mại không hợp lệ.");
            List<KhuyenmaiModel> glstKhuyenmai = GetKhuyenmaiByPK(pk_iKhuyenmaiID);
            if (glstKhuyenmai.Count < 1)
                throw new Exception("Khuyến mại cần xóa không tồn tại.");
            return (new db_QuanSinhToEntities()).spKhuyenmai_DeleteByPK(pk_iKhuyenmaiID)>0?true:false;
        }
        public bool XoaLoaisanpham(int loaiSanphamID)
        {
            if (loaiSanphamID < 1)
                throw new Exception("Mã loại sản phẩm không hợp lệ.");
            if (SanphamGetbyFK_iloaiSanphamID(loaiSanphamID).Count > 0)
                throw new Exception("Loại sản phẩm này đã được sử dụng nếu không còn kinh doanh hãy chọn ngừng kinh doanh.");
            return new db_QuanSinhToEntities().spLoaiSanpham_DeleteByPK(loaiSanphamID)>0?true:false;
        }
    }
}
