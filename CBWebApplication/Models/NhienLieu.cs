using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBWebApplication.Models
{
    public partial class DMLoaiDauMo
    {
        [Key]
        public short ID { get; set; }
        public string LoaiDauMo { get; set; }
        public string DonViTinh { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class NL_NhaCC
    {
        [Key]
        public int ID { get; set; }
        public string TenTat { get; set; }
        public string TenNCC { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Mst { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string NganHang { get; set; }
        public string TaiKhoan { get; set; }        
        public bool Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class NL_HopDong
    {
        [Key]
        public int ID { get; set; }
        public int MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string HopDong { get; set; }
        public string DienGiai { get; set; }
        public DateTime NgayHL { get; set; }
        public decimal TyLe { get; set; }       
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class NL_BangGia
    {
        [Key]        
        public string MaTramNL { get; set; }
        public string TenTramNL { get; set; }
        public short MaDauMo { get; set; }
        public string TenDauMo { get; set; }
        public string DonViTinh { get; set; }
        public DateTime NgayHL { get; set; }
        public long PhieuNhapID { get; set; }
        public decimal DonGia { get; set; }
        public Decimal TyTrong { get; set; }
        public string GhiChu { get; set; }        
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }        
    }
    public partial class NL_54BASTM
    {
        [Key]
        public decimal NhietDo { get; set; }
        public decimal K790 { get; set; }
        public decimal K792 { get; set; }
        public decimal K794 { get; set; }
        public decimal K796 { get; set; }
        public decimal K798 { get; set; }
        public decimal K800 { get; set; }
        public decimal K802 { get; set; }
        public decimal K804 { get; set; }
        public decimal K806 { get; set; }
        public decimal K808 { get; set; }
        public decimal K810 { get; set; }
        public decimal K812 { get; set; }
        public decimal K814 { get; set; }
        public decimal K816 { get; set; }
        public decimal K818 { get; set; }
        public decimal K820 { get; set; }
        public decimal K822 { get; set; }
        public decimal K824 { get; set; }
        public decimal K826 { get; set; }
        public decimal K828 { get; set; }
        public decimal K830 { get; set; }
        public decimal K832 { get; set; }
        public decimal K834 { get; set; }
        public decimal K836 { get; set; }
        public decimal K838 { get; set; }
        public decimal K840 { get; set; }
        public decimal K842 { get; set; }
        public decimal K844 { get; set; }
        public decimal K846 { get; set; }
        public decimal K848 { get; set; }
        public decimal K850 { get; set; }
        public decimal K852 { get; set; }
        public decimal K854 { get; set; }
        public decimal K856 { get; set; }
        public decimal K858 { get; set; }
        public decimal K860 { get; set; }
        public decimal K862 { get; set; }
        public decimal K864 { get; set; }
        public decimal K866 { get; set; }
        public decimal K868 { get; set; }
        public decimal K870 { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }
    }
    public partial class NL_PhieuNhap
    {
        [Key]
        public long PhieuNhapID { get; set; }
        public DateTime NgayNhap { get; set; }
        public string LoaiPhieu { get; set; }
        public string MaTramNL { get; set; }
        public string TenTramNL { get; set; }       
        public int MaNCC { get; set; }
        public string TenNCC { get; set; }
        public int MaHopDong { get; set; }
        public string TenHopDong { get; set; }
        public string SoHoaDon { get; set; }
        public DateTime NgayHoaDon { get; set; }
        public decimal TyLe { get; set; }
        public Decimal VAT { get; set; }      
        public string NguoiGiao { get; set; }
        public string LyDo { get; set; }        
        public bool KhoaSo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }        
        public List<NL_PhieuNhapCT> NL_PhieuNhapCTs { get; set; }        
    }
    public partial class NL_PhieuNhapCT
    {
        [Key]
        public long PhieuNhapID { get; set; }
        public short MaDauMo { get; set; }
        public string TenDauMo { get; set; }
        public string DonViTinh { get; set; }
        public Decimal NhietDo { get; set; }
        public Decimal TyTrong { get; set; }
        public Decimal VCF { get; set; }
        public decimal SoLuong { get; set; }
        public decimal SoLuongVCF { get; set; }
        public decimal ConLai { get; set; }       
        public decimal DonGia { get; set; }       
        public decimal TyLe { get; set; }
        public decimal Vat { get; set; }
        public decimal ThanhTien { get; set; }       
    }
    public partial class NL_PhieuXuat
    {
        [Key]
        public long PhieuXuatID { get; set; }
        public DateTime NgayXuat { get; set; }
        public string LoaiPhieu { get; set; }
        public string MaTramNL { get; set; }
        public string TenTramNL { get; set; }             
        public string DauMayID { get; set; }        
        public string LoaiMayID { get; set; }      
        public string SoChungTu { get; set; }
        public string NguoiNhan { get; set; }
        public string LyDo { get; set; }
        public bool KhoaSo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyName { get; set; }
        public List<NL_PhieuXuatCT> NL_PhieuXuatCTs { get; set; }
    }
    public partial class NL_PhieuXuatCT
    {
        [Key]
        public long PhieuXuatID { get; set; }
        public short MaDauMo { get; set; }
        public string TenDauMo { get; set; }
        public string DonViTinh { get; set; }
        public Decimal NhietDo { get; set; }
        public Decimal TyTrong { get; set; }
        public Decimal VCF { get; set; }
        public decimal SoLuong { get; set; }
        public decimal SoLuongVCF { get; set; }
        public long PhieuNhapID { get; set; }        
        public decimal DonGia { get; set; }       
        public long BangGiaID { get; set; }
        public decimal ThanhTien { get; set; }       
    }
    public partial class NL_BCTheKho
    {
        [Key]      
        public long PhieuID { get; set; }
        public DateTime Ngay { get; set; }        
        public string LoaiPhieu { get; set; }
        public string TramNL { get; set; }
        public string DienGiai { get; set; }       
        public decimal SoLuong { get; set; }        
        public decimal ThanhTien { get; set; }
        public decimal LuongTK { get; set; }
        public decimal TienTK { get; set; }
    }
    public partial class NL_BCTonKho
    {
        [Key]
        public short MaDauMo { get; set; }
        public string TenDauMo { get; set; }
        public string DonViTinh { get; set; }
        public decimal LuongTD { get; set; }
        public decimal TienTD { get; set; }
        public decimal LuongPN { get; set; }
        public decimal TienPN { get; set; }
        public decimal LuongPXTT { get; set; }
        public decimal LuongPX { get; set; }
        public decimal TienPX { get; set; }
        public decimal LuongTK { get; set; }
        public decimal TienTK { get; set; }
    }
}
