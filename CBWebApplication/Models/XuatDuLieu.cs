using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBWebApplication.Models
{
    public partial class DL_NguoiDung
    {
        [Key]       
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string MaDV { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public DateTime? SessionDate { get; set; }
        public int? UserCount { get; set; }
        public bool? Active { get; set; }       
    }
    public partial class DL_Danhmucga
    {
        [Key]
        public string TrangThai { get; set; }
        public List<Danhmucga> DuLieu { get; set; }
    }
    public partial class Danhmucga
    {
        [Key]
        public string TuyenID { get; set; }
        public string TuyenName { get; set; }
        public int? GaID { get; set; }
        public string GaName { get; set; }
        public decimal? Km { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
    public partial class DL_Danhmucloaimay
    {
        [Key]
        public string TrangThai { get; set; }
        public List<Danhmucloaimay> DuLieu { get; set; }
    }
    public partial class Danhmucloaimay
    {
        [Key]
        public string LoaiMayID { get; set; }
        public string LoaiMayName { get; set; }        
        public DateTime? ModifyDate { get; set; }
    }
    public partial class DL_Danhmucdaumay
    {
        [Key]
        public string TrangThai { get; set; }
        public List<Danhmucdaumay> DuLieu { get; set; }
    }
    public partial class Danhmucdaumay
    {
        [Key]
        public string DauMayID { get; set; }
        public string DauMayName { get; set; }
        public string LoaiMayID { get; set; }
        public string LoaiMayName { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
    public partial class DL_Danhmuctramnl
    {
        [Key]
        public string TrangThai { get; set; }
        public List<Danhmuctramnl> DuLieu { get; set; }
    }
    public partial class Danhmuctramnl
    {
        [Key]
        public string TramID { get; set; }
        public string TramName { get; set; }      
        public DateTime? ModifyDate { get; set; }
    }
    public partial class DL_Danhmuctinhchat
    {
        [Key]
        public string TrangThai { get; set; }
        public List<Danhmuctinhchat> DuLieu { get; set; }
    }
    public partial class Danhmuctinhchat
    {
        [Key]
        public short? TinhChatID { get; set; }
        public string TinhChatName { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
    public partial class DL_Danhmuccongtac
    {
        [Key]
        public string TrangThai { get; set; }
        public List<Danhmuccongtac> DuLieu { get; set; }
    }
    public partial class Danhmuccongtac
    {
        [Key]
        public short? CongTacID { get; set; }
        public string CongTacName { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
    public partial class DL_Danhmucmactau
    {
        [Key]
        public string TrangThai { get; set; }
        public List<Danhmucmactau> DuLieu { get; set; }
    }
    public partial class Danhmucmactau
    {
        [Key]
        public string MacTauID { get; set; }
        public short? LoaiTauID { get; set; }
        public string LoaiTauName { get; set; }
        public short? CongTacID { get; set; }
        public string CongTacName { get; set; }       
        public DateTime? ModifyDate { get; set; }      
    }
    public partial class DL_Danhmucdonvi
    {
        [Key]
        public string TrangThai { get; set; }
        public List<Danhmucdonvi> DuLieu { get; set; }
    }
    public partial class Danhmucdonvi
    {
        [Key]
        public string DonViID { get; set; }
        public string DonViName { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
    public partial class DL_Danhmuckhudoan
    {
        [Key]
        public string TrangThai { get; set; }
        public List<Danhmuckhudoan> DuLieu { get; set; }
    }
    public partial class Danhmuckhudoan
    {
        [Key]
        public string KhuDoanID { get; set; }
        public string KhuDoanName { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
    public partial class DL_Danhmuctuyen
    {
        [Key]
        public string TrangThai { get; set; }
        public List<Danhmuctuyen> DuLieu { get; set; }
    }
    public partial class Danhmuctuyen
    {
        [Key]
        public short? TuyenID { get; set; }
        public string TuyenName { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
    public partial class DL_Danhmuctuyensl
    {
        [Key]
        public string TrangThai { get; set; }
        public List<Danhmuctuyensl> DuLieu { get; set; }
    }
    public partial class Danhmuctuyensl
    {
        [Key]
        public string TuyenID { get; set; }
        public string TuyenName { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
    public partial class DL_Danhmucdaumo
    {
        [Key]
        public string TrangThai { get; set; }
        public List<Danhmucdaumo> DuLieu { get; set; }
    }
    public partial class Danhmucdaumo
    {
        [Key]
        public short? DauMoID { get; set; }
        public string DauMoName { get; set; }
        public string DonViTinh { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
    public partial class DL_Cobaodt
    {
        [Key]
        public string TrangThai { get; set; }
        public List<XCoBao> DuLieu { get; set; }
    }
    public partial class DL_Cobaodtct
    {
        [Key]
        public string TrangThai { get; set; }
        public List<XCoBaoCT> DuLieu { get; set; }
    }
    public partial class DL_Cobaodtdm
    {
        [Key]
        public string TrangThai { get; set; }
        public List<XCoBaoDM> DuLieu { get; set; }
    }
    public partial class DL_Cobaoga
    {
        [Key]
        public string TrangThai { get; set; }
        public List<XCoBaoGA> DuLieu { get; set; }
    }
    public partial class DL_Cobaogac
    {
        [Key]
        public string TrangThai { get; set; }
        public List<XCoBaoGAC> DuLieu { get; set; }
    }
    public class XCoBaoGAC
    {
        [Key]
        public long CoBaoID { get; set; }
        public string SoCB { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
    public partial class DL_Cobaogact
    {
        [Key]
        public string TrangThai { get; set; }
        public List<XCoBaoGACT> DuLieu { get; set; }
    }
    public partial class DL_Cobaogadm
    {
        [Key]
        public string TrangThai { get; set; }
        public List<XCoBaoGADM> DuLieu { get; set; }
    }
}
