using QuanSinhTo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace QuanSinhTo.Controller
{
    public static class QuyenNumberic {
        private static byte admin = 1, nhanvienPhache = 2, nhanvienThungan = 3;
        public static byte Admin { get { return admin; } }
        public static byte NhanvienPhache { get { return nhanvienPhache; } }
        public static byte NhanvienThungan { get { return nhanvienThungan; } }
    }
    public class TaiKhoanController
    {
        public string StringSHA256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));
                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }
        private void validateTaikhoan(long userID, string tennguoidung, string tennhanvien, string matkhau)
        {
            tennguoidung = tennguoidung.Trim();
            tennguoidung = tennguoidung.ToLower();
            tennhanvien = tennhanvien.Trim();
            if (string.IsNullOrEmpty(tennguoidung))
                throw new Exception("Tên đăng nhập không được để trống.");
            if (string.IsNullOrEmpty(tennhanvien))
                throw new Exception("Tên nhân viên không được để trống.");
            if (string.IsNullOrEmpty(matkhau))
                throw new Exception("Mật khẩu không được để trống.");
            if (tennguoidung.Length < 8 || matkhau.Length < 8)
                throw new Exception("Tên đăng nhập và mật khẩu không được nhỏ hơn 8 ký tự.");
            List<NguoidungModel> glstNguoidung = NguoidungGetbyPK(0);
            glstNguoidung = glstNguoidung.FindAll(nguoidung => nguoidung.sTennguoidung.Equals(tennguoidung) && nguoidung.PK_iNguoidungID != userID);
            if (glstNguoidung.Count > 0)
                throw new Exception("Tên đăng nhập này đã được sử dụng.");
        }
        public long ThemTaiKhoan(string tennguoidung, string tennhanvien, string matkhau)
        {
            validateTaikhoan(0, tennguoidung, tennhanvien, matkhau);
            return Convert.ToInt64(new db_QuanSinhToEntities().spNguoidung_Insert(tennguoidung, tennhanvien, StringSHA256_hash(matkhau)).FirstOrDefault());
        }
        public bool SuaTaiKhoan(long userID, string tennguoidung, string tennhanvien, string matkhau)
        {
            if (userID < 1)
                throw new Exception("Mã người dùng không hợp lệ.");
            List<NguoidungModel> glstNguoidung = NguoidungGetbyPK(userID);
            if (glstNguoidung.Count < 1)
                throw new Exception("Người dùng không tồn tại.");
            validateTaikhoan(userID, tennguoidung, tennhanvien, matkhau);
            return (new db_QuanSinhToEntities()).spNguoidung_Update(userID, tennguoidung, tennhanvien, StringSHA256_hash(matkhau)) > 0 ? true : false;
        }
        public bool XoaTaiKhoan(long userID)
        {
            if (userID < 1)
                throw new Exception("Mã người dùng không hợp lệ.");
            List<NguoidungModel> glstNguoidung = NguoidungGetbyPK(userID);
            if (glstNguoidung.Count < 1)
                throw new Exception("Người dùng không tồn tại.");
            return (new db_QuanSinhToEntities()).spNguoidung_DeleteByPK(userID) > 0 ? true : false;
        }
        public NguoidungModel Login(string tennguoidung, string matkhau)
        {
            List<NguoidungModel> glstNguoidung = NguoidungGetbyPK(0);
            glstNguoidung = glstNguoidung.FindAll(nguoidung => nguoidung.sTennguoidung.Equals(tennguoidung) && nguoidung.sMatkhau.Equals(StringSHA256_hash(matkhau)));
            if (glstNguoidung.Count > 0)
                return glstNguoidung[0];
            return null;
        }
        public List<NguoidungModel> NguoidungGetbyPK(long userID)
        {
            if (userID < 0)
                throw new Exception("Mã người dùng không hợp lệ.");
            return (new db_QuanSinhToEntities()).spNguoidung_GetByPK(userID).ToList();
        }
        private void validateQuyen(int quyenID, string tenquyen, string ynghia)
        {
            tenquyen = tenquyen.Trim();
            ynghia = ynghia.Trim();
            if (string.IsNullOrEmpty(tenquyen))
                throw new Exception("Tên quyền không được để trống.");
            if (string.IsNullOrEmpty(ynghia))
                throw new Exception("Ý nghĩa không được để trống.");
            List<QuyenModel> glstQuyen = QuyenGetbyPK(0);
            glstQuyen = glstQuyen.FindAll(quyen => quyen.sTenQuyen.Equals(tenquyen) && quyenID != quyen.PK_iQuyenID);
            if (glstQuyen.Count > 0)
                throw new Exception("Tên quyền trùng lặp.");
        }
        public int ThemQuyen(string tenquyen, string ynghia)
        {
            validateQuyen(0, tenquyen, ynghia);
            return Convert.ToInt32(new db_QuanSinhToEntities().spQuyen_Insert(tenquyen, ynghia).FirstOrDefault());
        }
        public bool Suaquyen(int quyenID, string tenquyen, string ynghia)
        {
            if (quyenID < 1)
                throw new Exception("Mã quyền không hợp lệ.");
            List<QuyenModel> glstQuyen = QuyenGetbyPK(quyenID);
            if (glstQuyen.Count < 1)
                throw new Exception("Quyền không tồn tại.");
            validateQuyen(quyenID, tenquyen, ynghia);
            return new db_QuanSinhToEntities().spQuyen_UpdateByPK(quyenID, tenquyen, ynghia) > 0 ? true : false;
        }
        public bool Xoaquyen(int quyenID)
        {
            if (quyenID < 1)
                throw new Exception("Mã quyền không hợp lệ.");
            List<QuyenModel> glstQuyen = QuyenGetbyPK(quyenID);
            if (glstQuyen.Count < 1)
                throw new Exception("Quyền không tồn tại.");
            List<PhanquyenModel> glstPhanquyen = PhanquyenGetbyFK_iQuyenID(quyenID);
            if (glstPhanquyen.Count > 0)
                throw new Exception("Quyền đã được sử dụng không thể xóa.");
            return new db_QuanSinhToEntities().spQuyen_DeleteByPK(quyenID) > 0 ? true : false;
        }
        public long ThemPhanquyen(int quyenID, long userID)
        {
            if (quyenID < 1)
                throw new Exception("Mã quyền không hợp lệ.");
            if (userID < 1)
                throw new Exception("Mã người dùng không hợp lệ.");
            List<QuyenModel> glstQuyen = QuyenGetbyPK(quyenID);
            if (glstQuyen.Count < 1)
                throw new Exception("Quyền không tồn tại.");
            List<NguoidungModel> glstNguoidung = NguoidungGetbyPK(userID);
            if (glstNguoidung.Count < 1)
                throw new Exception("Người dùng không tồn tại.");
            return Convert.ToInt64(new db_QuanSinhToEntities().spPhanquyen_Insert(userID, quyenID, DateTime.Now, DateTime.Now.AddYears(999)).FirstOrDefault());
        }
        public List<PhanquyenModel> PhanquyenGetbyPK(long phanquyenID)
        {
            if (phanquyenID < 0)
                throw new Exception("Mã phân quyền không hợp lệ.");
            return new db_QuanSinhToEntities().spPhanquyen_GetByPK(phanquyenID).ToList();
        }
        public List<PhanquyenModel> PhanquyenGetbyFK_iQuyenID(int quyenID)
        {
            if (quyenID < 1)
                throw new Exception("Mã quyền không hợp lệ.");
            return new db_QuanSinhToEntities().spPhanquyen_GetByFK_iQuyenID(quyenID).ToList();
        }
        public List<PhanquyenModel> PhanquyenGetbyFK_iNguoidungID(long userID)
        {
            if (userID < 1)
                throw new Exception("Mã Người dùng không hợp lệ.");
            return new db_QuanSinhToEntities().spPhanquyen_GetByFK_iNguoidungID(userID).ToList();
        }
        public List<QuyenModel> QuyenGetbyPK(int quyenID)
        {
            if (quyenID < 0)
                throw new Exception("Mã quyền không hợp lệ.");
            return new db_QuanSinhToEntities().spQuyen_GetByPK(quyenID).ToList();
        }
    }
}
