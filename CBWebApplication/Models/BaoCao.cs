using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBWebApplication.Models
{
    public partial class ViewBcvanDung
    {
        [Key]        
        public string DvcbID { get; set; }
        public string TuyenID { get; set; }
        public int ThangDT { get; set; }
        public int NamDT { get; set; }
        public string LoaiMayID { get; set; }
        public int? CongTacID { get; set; }
        public int? TinhChatID { get; set; }
        public int? GioDm { get; set; }
        public int? GioLh { get; set; }
        public int? GioDt { get; set; }       
        public int? Dgxp { get; set; }
        public int? Dgdd { get; set; }
        public int? Dgcc { get; set; }
        public int? Dgqd { get; set; }
        public int? Dgdm { get; set; }
        public int? Dgdn { get; set; }
        public int? Dgkm { get; set; }
        public int? Dgkn { get; set; }
        public int? Dnxp { get; set; }
        public int? Dndd { get; set; }
        public int? Dncc { get; set; }
        public decimal? Kmch { get; set; }
        public decimal? Kmdw { get; set; }
        public decimal? Kmgh { get; set; }
        public decimal? Kmdy { get; set; }
        public decimal? Tkch { get; set; }
        public decimal? Tkdw { get; set; }
        public decimal? Tkgh { get; set; }
        public decimal? Tkdy { get; set; }
        public decimal? Slrkm { get; set; }
        public decimal? Slrkn { get; set; }
        public decimal? Sltt { get; set; }
        public decimal? Sltt15 { get; set; }
        public decimal? Sltc { get; set; }
    }
    public partial class ViewBcGioDon
    {
        [Key]      
        public string DvcbID { get; set; }
        public string LoaiMayID { get; set; }
        public string GaXPName { get; set; }
        public short? CongTacID { get; set; }
        public int? GioDon { get; set; }
    }
    public partial class ViewBcNhienLieu
    {
        [Key]       
        public string DvcbID { get; set; }
        public string LoaiMayID { get; set; }
        public string DauMayID { get; set; }
        public DateTime NgayCB { get; set; }
        public int ThangDT { get; set; }
        public int NamDT { get; set; }
        public string TuyenID { get; set; }
        public short? CongTacID { get; set; }
        public short? TinhChatID { get; set; }
        public int GaXPID { get; set; }
        public int GaKTID { get; set; }
        public int? GioDon { get; set; }
        public decimal? TanKM { get; set; }
        public decimal? DinhMuc { get; set; }
        public decimal? TieuThu { get; set; }
    }
    public partial class ViewBcNhienLieuKD
    {
        [Key]        
        public string DvcbID { get; set; }
        public string LoaiMayID { get; set; } 
        public short? CongTacID { get; set; }
        public string KhuDoan { get; set; }
        public int ThangDT { get; set; }
        public int NamDT { get; set; }
        public int? GioDon { get; set; }
        public decimal? KM { get; set; }
        public decimal? TanKM { get; set; }
        public decimal? DinhMuc { get; set; }
        public decimal? TieuThu { get; set; }
    }   
    public partial class ViewBcTTNhienLieu
    {
        [Key]       
        public string DvcbID { get; set; }
        public string LoaiMayID { get; set; }
        public string DauMayID { get; set; }
        public DateTime NgayCB { get; set; }
        public int ThangDT { get; set; }
        public int NamDT { get; set; }
        public string TuyenID { get; set; }
        public short? CongTacID { get; set; }
        public string CongTacName { get; set; }
        public short? TinhChatID { get; set; }
        public string GaXP { get; set; }
        public string GaKT { get; set; }
        public int? GioDon { get; set; }
        public int? GioDung { get; set; }
        public decimal? KMChinh { get; set; }
        public decimal? KMDon { get; set; }
        public decimal? KMGhep { get; set; }
        public decimal? KMDay { get; set; }       
        public decimal? TanKM { get; set; }
        public decimal? DinhMuc { get; set; }
        public decimal? TieuThu { get; set; }
        public decimal? TieuThu15 { get; set; }
    }
    public partial class ViewBcTTTaiXe
    {
        [Key]
        public long CoBaoID { get; set; }
        public int ThangDT { get; set; }
        public int NamDT { get; set; }
        public string DvcbID { get; set; }
        public string TaiXe1ID { get; set; }
        public string TaiXe1Name { get; set; }
        public string PhoXe1ID { get; set; }
        public string PhoXe1Name { get; set; }
        public string TaiXe2ID { get; set; }
        public string TaiXe2Name { get; set; }
        public string PhoXe2ID { get; set; }
        public string PhoXe2Name { get; set; }
        public string TaiXe3ID { get; set; }
        public string TaiXe3Name { get; set; }
        public string PhoXe3ID { get; set; }
        public string PhoXe3Name { get; set; }
        public int? GioDung { get; set; }
        public int? GioDon { get; set; }
        public decimal? KM { get; set; }       
        public decimal? DinhMuc { get; set; }
        public decimal? TieuThu { get; set; }       
    }
    public partial class ViewBcTacNghiep
    {
        [Key]       
        public string DvcbID { get; set; }
        public string DvdmID { get; set; }
        public string LoaiMayID { get; set; }
        public string DauMayID { get; set; }
        public DateTime NgayCB { get; set; }
        public int ThangDT { get; set; }
        public int NamDT { get; set; }
        public short? CongTacID { get; set; }
        public short? TinhChatID { get; set; }
        public string GaXP { get; set; }
        public string GaKT { get; set; }
        public decimal? KMChinh { get; set; }
        public decimal? KMPhuTro { get; set; }
        public int? GioLH { get; set; }
        public int? GioDon { get; set; }
        public decimal? TanKM { get; set; }       
        public decimal? TieuThu { get; set; }
    }
    public partial class ViewBcTonNL
    {
        [Key]       
        public string LoaiMayID { get; set; }
        public string DauMayID { get; set; }
        public DateTime NhanMay { get; set; }
        public string MaTram { get; set; }
        public string TenTram { get; set; }
        public decimal? TonDau { get; set; }
        public decimal? Linh { get; set; }
        public decimal? TieuThu { get; set; }
        public decimal? TonCuoi { get; set; }       
    }
    public partial class ViewBcBKDauMo
    {
        [Key]
        public string LoaiMayID { get; set; }
        public string DauMayID { get; set; }
        public DateTime NgayCB { get; set; }
        public string SoCB { get; set; }
        public string MaTram { get; set; }          
        public decimal? Linh { get; set; }       
    }
    public partial class ViewBcBKLuong
    {
        [Key]       
        public string DauMayID { get; set; }
        public DateTime NgayCB { get; set; }
        public string SoCB { get; set; }
        public string MaCB { get; set; }
        public string TaiXe1ID { get; set; }
        public string TaiXe1Name { get; set; }
        public string PhoXe1ID { get; set; }
        public string PhoXe1Name { get; set; }
        public string TaiXe2ID { get; set; }
        public string TaiXe2Name { get; set; }
        public string PhoXe2ID { get; set; }
        public string PhoXe2Name { get; set; }
        public string TaiXe3ID { get; set; }
        public string TaiXe3Name { get; set; }
        public string PhoXe3ID { get; set; }
        public string PhoXe3Name { get; set; }
        public string MacTauID { get; set; }
        public short? TinhChatID { get; set; }
        public int? GioDm { get; set; }
        public decimal? Km { get; set; }
        public decimal? NLLoiLo { get; set; }
    }
    public partial class ViewBcGioDonCT
    {
        [Key]
        public string DvcbID { get; set; }
        public string GaName { get; set; }
        public string DauMayID { get; set; }
        public DateTime NhanMay { get; set; }
        public string SoCB { get; set; }        
        public string TaiXeID { get; set; }
        public string TaiXeName { get; set; }       
        public int? GioDon { get; set; }       
    }
}

