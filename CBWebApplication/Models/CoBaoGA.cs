using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CBWebApplication.Models
{
    public class CoBaoGA
    {
        [Key]
        public long CoBaoID { get; set; }
        public long CoBaoGoc { get; set; }
        public string DauMayID { get; set; }
        public string LoaiMayID { get; set; }
        public string DvdmID { get; set; }
        public string DvdmName { get; set; }
        public string SoCB { get; set; }
        public string DvcbID { get; set; }
        public string DvcbName { get; set; }
        public DateTime NgayCB { get; set; }
        public int RutGio { get; set; }
        public string ChatLuong { get; set; }
        public decimal SoLanRaKho { get; set; }
        public string TaiXe1ID { get; set; }
        public string TaiXe1Name { get; set; }
        public short TaiXe1GioLT { get; set; }
        public string PhoXe1ID { get; set; }
        public string PhoXe1Name { get; set; }
        public short PhoXe1GioLT { get; set; }
        public string TaiXe2ID { get; set; }
        public string TaiXe2Name { get; set; }
        public short TaiXe2GioLT { get; set; }
        public string PhoXe2ID { get; set; }
        public string PhoXe2Name { get; set; }
        public short PhoXe2GioLT { get; set; }
        public string TaiXe3ID { get; set; }
        public string TaiXe3Name { get; set; }
        public short TaiXe3GioLT { get; set; }
        public string PhoXe3ID { get; set; }
        public string PhoXe3Name { get; set; }
        public short PhoXe3GioLT { get; set; }
        public string KiemTra1ID { get; set; }
        public string KiemTra1Name { get; set; }
        public string KiemTra2ID { get; set; }
        public string KiemTra2Name { get; set; }
        public string KiemTra3ID { get; set; }
        public string KiemTra3Name { get; set; }
        public DateTime LenBan { get; set; }
        public DateTime NhanMay { get; set; }
        public DateTime RaKho { get; set; }
        public DateTime VaoKho { get; set; }
        public DateTime GiaoMay { get; set; }
        public DateTime XuongBan { get; set; }
        public int NLBanTruoc { get; set; }
        public int NLThucNhan { get; set; }
        public int NLLinh { get; set; }
        public string TramNLID { get; set; }
        public decimal NLTrongDoan { get; set; }
        public int NLBanSau { get; set; }
        public string SHDT { get; set; }
        public string MaCB { get; set; }
        public decimal DonDocDuong { get; set; }
        public decimal DungDocDuong { get; set; }
        public decimal DungNoMay { get; set; }
        public string GhiChu { get; set; }
        public int GaID { get; set; }
        public string GaName { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public string CreatedName { get; set; }
        public DateTime Modifydate { get; set; }
        public string Modifyby { get; set; }
        public string ModifyName { get; set; }
        public string TrangThai { get; set; }
        public bool KhoaCB { get; set; }
        public DateTime KhoaCBdate { get; set; }
        public string KhoaCBby { get; set; }
        public string KhoaCBName { get; set; }
        public List<CoBaoGACT> coBaoGACTs { get; set; }
        public List<CoBaoGADM> coBaoGADMs { get; set; }
    }
    public class CoBaoGACT
    {
        [Key]       
        public long CoBaoID { get; set; }
        public DateTime NgayXP { get; set; }
        public DateTime GioDen { get; set; }
        public DateTime GioDi { get; set; }
        public decimal PhutDon { get; set; }
        public long TauID { get; set; }
        public string MacTauID { get; set; }
        public decimal RutGioNL { get; set; }
        public bool DungGioPT { get; set; }
        public int GaID { get; set; }
        public string GaName { get; set; }
        public short TinhChatID { get; set; }
        public string TinhChatName { get; set; }
        public decimal Tan { get; set; }
        public int XeTotal { get; set; }
        public decimal TanXeRong { get; set; }
        public int XeRongTotal { get; set; }
        public string MayGhepID { get; set; }
        public decimal KmAdd { get; set; }
        public string CongTyID { get; set; }
        public string CongTyName { get; set; }
        public short LoaiTauID { get; set; }
        public string LoaiTauName { get; set; }       
        public short? TuyenID { get; set; }
        public string TuyenName { get; set; }
    }
    public class CoBaoGADM
    {
        [Key]       
        public long CoBaoID { get; set; }
        public short LoaiDauMoID { get; set; }
        public string LoaiDauMoName { get; set; }
        public string DonViTinh { get; set; }
        public decimal Nhan { get; set; }
        public decimal Linh { get; set; }
        public string MaTram { get; set; }
        public string TenTram { get; set; }
        public decimal Giao { get; set; }
    }
    public class CoBaoGAALL
    {
        [Key]
        public long CoBaoID { get; set; }
        public CoBaoGA coBaoGAs { get; set; }
        public DoanThongGA doanThongGAs { get; set; }
    }   
    public class DoanThongGA
    {
        [Key]
        public long DoanThongID { get; set; }
        public int DungKB { get; set; }
        public int ThangDT { get; set; }
        public int NamDT { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public string CreatedName { get; set; }
        public DateTime Modifydate { get; set; }
        public string Modifyby { get; set; }
        public string ModifyName { get; set; }
        public List<DoanThongGACT> doanThongGACTs { get; set; }
        public List<DoanThongGADM> doanThongGADMs { get; set; }
    }
    public class DoanThongGACT
    {
        [Key]       
        public long DoanThongID { get; set; }
        public short STT { get; set; }
        public DateTime NgayXP { get; set; }
        public long TauID { get; set; }
        public string MacTauID { get; set; }
        public string CongTyID { get; set; }
        public string CongTyName { get; set; }
        public short CongTacID { get; set; }
        public string CongTacName { get; set; }
        public short TinhChatID { get; set; }
        public string TinhChatName { get; set; }
        public string TuyenID { get; set; }
        public string TuyenName { get; set; }
        public int GaXPID { get; set; }
        public string GaXPName { get; set; }
        public int GaKTID { get; set; }
        public string GaKTName { get; set; }
        public string MayGhepID { get; set; }
        public int QuayVong { get; set; }
        public int LuHanh { get; set; }
        public int DonThuan { get; set; }
        public int DungDM { get; set; }
        public int DungDN { get; set; }
        public int DungQD { get; set; }
        public int DungXP { get; set; }
        public int DungDD { get; set; }
        public int DungKT { get; set; }
        public int DungKhoDM { get; set; }
        public int DungKhoDN { get; set; }
        public int DungNM { get; set; }
        public int DonXP { get; set; }
        public int DonDD { get; set; }
        public int DonKT { get; set; }
        public decimal KMChinh { get; set; }
        public decimal KMDon { get; set; }
        public decimal KMGhep { get; set; }
        public decimal KMDay { get; set; }
        public decimal TKMChinh { get; set; }
        public decimal TKMDon { get; set; }
        public decimal TKMGhep { get; set; }
        public decimal TKMDay { get; set; }
        public decimal Tan { get; set; }
        public int XeTotal { get; set; }
        public decimal TanXeRong { get; set; }
        public int XeRongTotal { get; set; }
        public decimal SLRKDM { get; set; }
        public decimal SLRKDN { get; set; }
        public string KhuDoan { get; set; }
        public string DienGiai { get; set; }
        public decimal DinhMuc { get; set; }
        public decimal TieuThu { get; set; }
        public decimal RutGioNL { get; set; }        
    }
    public class DoanThongGADM
    {
        [Key]        
        public long DoanThongID { get; set; }
        public short LoaiDauMoID { get; set; }
        public string LoaiDauMoName { get; set; }
        public string DonViTinh { get; set; }
        public string DienGiai { get; set; }
        public decimal DinhMuc { get; set; }
        public decimal TieuThu { get; set; }
    }
    public class DoanThongGAView
    {
        [Key]
        public long DoanThongID { get; set; }
        public long CoBaoGoc { get; set; }
        public string SoCB { get; set; }
        public string DauMayID { get; set; }
        public string LoaiMayID { get; set; }
        public string DvdmName { get; set; }
        public string DvcbID { get; set; }
        public string DvcbName { get; set; }
        public DateTime NgayCB { get; set; }
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
        public string KiemTra1ID { get; set; }
        public string KiemTra1Name { get; set; }
        public string KiemTra2ID { get; set; }
        public string KiemTra2Name { get; set; }
        public string KiemTra3ID { get; set; }
        public string KiemTra3Name { get; set; }
        public DateTime LenBan { get; set; }
        public DateTime NhanMay { get; set; }
        public DateTime RaKho { get; set; }
        public DateTime VaoKho { get; set; }
        public DateTime GiaoMay { get; set; }
        public DateTime XuongBan { get; set; }
        public int DungKB { get; set; }
        public int NLBanTruoc { get; set; }
        public int NLThucNhan { get; set; }
        public int NLLinh { get; set; }
        public string TramNLID { get; set; }
        public decimal NLTrongDoan { get; set; }
        public int NLBanSau { get; set; }
        public int ThangDT { get; set; }
        public int NamDT { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public string CreatedName { get; set; }
        public DateTime Modifydate { get; set; }
        public string Modifyby { get; set; }
        public string ModifyName { get; set; }
    }
    public class XCoBaoGA
    {
        [Key]
        public long CoBaoID { get; set; }
        public long CoBaoGoc { get; set; }
        public string SoCB { get; set; }
        public string DauMayID { get; set; }
        public string LoaiMayID { get; set; }
        public string DvdmID { get; set; }
        public string DvdmName { get; set; }
        public string DvcbID { get; set; }
        public string DvcbName { get; set; }
        public DateTime NgayCB { get; set; }
        public int RutGio { get; set; }
        public string ChatLuong { get; set; }
        public decimal SoLanRaKho { get; set; }
        public string TaiXe1ID { get; set; }
        public string TaiXe1Name { get; set; }
        public short TaiXe1GioLT { get; set; }
        public string PhoXe1ID { get; set; }
        public string PhoXe1Name { get; set; }
        public short PhoXe1GioLT { get; set; }
        public string TaiXe2ID { get; set; }
        public string TaiXe2Name { get; set; }
        public short TaiXe2GioLT { get; set; }
        public string PhoXe2ID { get; set; }
        public string PhoXe2Name { get; set; }
        public short PhoXe2GioLT { get; set; }
        public string TaiXe3ID { get; set; }
        public string TaiXe3Name { get; set; }
        public short TaiXe3GioLT { get; set; }
        public string PhoXe3ID { get; set; }
        public string PhoXe3Name { get; set; }
        public short PhoXe3GioLT { get; set; }
        public string KiemTra1ID { get; set; }
        public string KiemTra1Name { get; set; }
        public string KiemTra2ID { get; set; }
        public string KiemTra2Name { get; set; }
        public string KiemTra3ID { get; set; }
        public string KiemTra3Name { get; set; }
        public DateTime LenBan { get; set; }
        public DateTime NhanMay { get; set; }
        public DateTime RaKho { get; set; }
        public DateTime VaoKho { get; set; }
        public DateTime GiaoMay { get; set; }
        public DateTime XuongBan { get; set; }
        public int NLBanTruoc { get; set; }
        public int NLThucNhan { get; set; }
        public int NLLinh { get; set; }
        public string TramNLID { get; set; }
        public decimal NLTrongDoan { get; set; }
        public int NLBanSau { get; set; }
        public string SHDT { get; set; }
        public string MaCB { get; set; }
        public decimal DonDocDuong { get; set; }
        public decimal DungDocDuong { get; set; }
        public decimal DungNoMay { get; set; }
        public string GhiChu { get; set; }
    }
    public class XCoBaoGACT
    {
        [Key]        
        public long CoBaoID { get; set; }
        public DateTime NgayXP { get; set; }
        public DateTime GioDen { get; set; }
        public DateTime GioDi { get; set; }
        public decimal PhutDon { get; set; }
        public string MacTauID { get; set; }                
        public string CongTyID { get; set; }
        public string CongTyName { get; set; }
        public short LoaiTauID { get; set; }
        public string LoaiTauName { get; set; }
        public int GaID { get; set; }
        public string GaName { get; set; }
        public short? TuyenID { get; set; }
        public string TuyenName { get; set; }
        public decimal Tan { get; set; }
        public int XeTotal { get; set; }
        public decimal TanXeRong { get; set; }
        public int XeRongTotal { get; set; }
        public short TinhChatID { get; set; }
        public string TinhChatName { get; set; }
        public string MayGhepID { get; set; }
        public decimal KmAdd { get; set; }
        public decimal RutGioNL { get; set; }
        public bool DungGioPT { get; set; }
    }
    public class XCoBaoGADM
    {
        [Key]       
        public long CoBaoID { get; set; }
        public short LoaiDauMoID { get; set; }
        public string LoaiDauMoName { get; set; }
        public string DonViTinh { get; set; }
        public decimal Nhan { get; set; }
        public decimal Linh { get; set; }
        public string MaTram { get; set; }
        public string TenTram { get; set; }
        public decimal Giao { get; set; }
    }
}
