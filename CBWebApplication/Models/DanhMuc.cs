using System;
using System.ComponentModel.DataAnnotations;

namespace CBWebApplication.Models
{
    public partial class NhanVien
    {
        [Key]
        public string MaNV { get; set; }
        public short? MaQH { get; set; }
        public short? NL { get; set; }
        public string MaDV { get; set; }
        public bool Active { get; set; }
    }
    public class DMNhanVien
    {
        [Key]
        public int NhanVienID { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string MaSo { get; set; }
        public string ChucVu { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public string MaDV { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }       
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }       

    }
    public class ViewDMNhanVien
    {
        [Key]
        public int NhanVienID { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string MaSo { get; set; }
        public string ChucVu { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string MaDV { get; set; }
        public string TenDV { get; set; }
        public string MaCT { get; set; }
        public string DVQL { get; set; }
        public string CodeQL { get; set; }
        public short? CapQL { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }        
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }        
    }
   
    public partial class CongTac
    {
        [Key]
        public short CongTacId { get; set; }
        public string CongTacName { get; set; }
    }
    public partial class LoaiTau
    {
        [Key]
        public short LoaiTauID { get; set; }
        public string LoaiTauName { get; set; }
        public short CongTacID { get; set; }
    }
    public partial class DMMacTau
    {
        [Key]
        public string MacTauID { get; set; }
        public short? LoaiTauID { get; set; }
        public string LoaiTauName { get; set; }
        public string CongTyID { get; set; }
        public string CongTyName { get; set; }
        public short? TuyenID { get; set; }
        public string TuyenName { get; set; }
    }
    public partial class MacTau
    {
        [Key]
        public string MacTauID { get; set; }
        public short? LoaiTauID { get; set; }
        public string LoaiTauName { get; set; }
        public short CongTacID { get; set; }
        public string CongTacName { get; set; }
        public string CongTyID { get; set; }
        public string CongTyName { get; set; }
        public short? TuyenID { get; set; }
        public string TuyenName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class TinhChat
    {
        [Key]
        public short TinhChatId { get; set; }
        public string TinhChatName { get; set; }
    }
    public partial class LoaiMay
    {
        [Key]
        public string LoaiMayId { get; set; }
        public string LoaiMayMap { get; set; }
        public string LoaiMayName { get; set; }
        public short KhoDuong { get; set; }
        public decimal TuTrong { get; set; }
        public short MaLuc { get; set; }
        public short NhomMay { get; set; }
        public short SoTT { get; set; }
        public bool? Active { get; set; }
    }
    public partial class DMDauMay
    {
        [Key]
        public int Id { get; set; }
        public string DauMaySo { get; set; }
        public string SoHieuMay { get; set; }
        public string PhanLoai { get; set; }
        public decimal? TuTrong { get; set; }
        public int? Tam2DauDam { get; set; }
        public string MaCtsoHuu { get; set; }
        public string MaCtquanLy { get; set; }
        public int? KhoDuong { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
    }
    public partial class DMTaiXe
    {
        [Key]
        public string TaiXeID { get; set; }
        public string TaiXeName { get; set; }
        public string DonViID { get; set; }
        public string DonViName { get; set; }
    }
    public partial class DMPhoXe
    {
        [Key]
        public string PhoXeID { get; set; }
        public string PhoXeName { get; set; }
        public string DonViID { get; set; }
        public string DonViName { get; set; }
    }
    public partial class Tuyen
    {
        [Key]
        public string TuyenID { get; set; }
        public short TuyenMap { get; set; }
        public string TuyenName { get; set; }
        public bool Active { get; set; }
    }
    public partial class TuyenMap
    {
        [Key]
        public short TuyenId { get; set; }
        public string TuyenName { get; set; }
    }
    public partial class DMGa
    {
        [Key]
        public int GaId { get; set; }
        public string MaGa { get; set; }
        public string TenGa { get; set; }
        public string MaGaDs { get; set; }
        public string Keyword { get; set; }
        public int? IsActive { get; set; }
    }
    public partial class GaChuyenDon
    {
        [Key]
        public int GaId { get; set; }       
        public string GaName { get; set; }
        public DateTime NgayHL { get; set; }
        public bool Active { get; set; }
        public string GhiChu { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class DmtramNhienLieu
    {
        [Key]
        public int Id { get; set; }
        public string MaTram { get; set; }
        public string TenTram { get; set; }
        public string MaDvql { get; set; }
        public byte? IsActive { get; set; }
    }
    public partial class DmdonVi
    {
        [Key]
        public string MaDv { get; set; }
        public string TenDv { get; set; }
        public string MaDvql { get; set; }
        public string MaCt { get; set; }
        public string Dvql { get; set; }
        public string CodeQl { get; set; }
        public short? CapQl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public short? LoaiDv { get; set; }
        public int? IsActive { get; set; }
    }
    public partial class DonViDM
    {
        [Key]
        public string MaDV { get; set; }
        public string TenDV { get; set; }
        public string TenTat { get; set; }
        public string DiaChi { get; set; }
        public string Tinh { get; set; }
        public string DienThoai { get; set; }
        public string Fax { get; set; }
        public string MST { get; set; }
        public string SoTK { get; set; }
        public string NganHang { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string GaDMList { get; set; }
        public string MaCha { get; set; }
    }
    public partial class DonViKT
    {
        [Key]
        public string MaDV { get; set; }
        public string TenDV { get; set; }
        public string MaDVCha { get; set; }
        public short SapXep { get; set; }
        public int GaID { get; set; }       
    }
    public partial class CongTy
    {
        [Key]
        public string CongTyID { get; set; }
        public string CongTyName { get; set; }
        public bool Active { get; set; }       
    }
    public class MienPhat
    {
        [Key]
        public long CoBaoID { get; set; }                
        public string SoCB { get; set; }
        public int ThangDT { get; set; }
        public int NamDT { get; set; }       
        public decimal TyLe { get; set; }
        public string LyDo { get; set; }
        public string MaDV { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }        
    }
    public partial class PhienBan
    {
        [Key]
        public string ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }
    }
}
