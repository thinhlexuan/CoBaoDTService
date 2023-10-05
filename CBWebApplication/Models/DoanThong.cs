using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBWebApplication.Models
{
    public class DoanThongView
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
    public class DoanThong
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
        public List<DoanThongCT> doanThongCTs { get; set; }
        public List<DoanThongDM> doanThongDMs { get; set; }
    }
    public class DoanThongCT
    {
        [Key]        
        public long ID { get; set; }
        public long DoanThongID { get; set; }
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
    }
    public class DoanThongDM
    {
        [Key]       
        public long ID { get; set; }        
        public long DoanThongID { get; set; }
        public short LoaiDauMoID { get; set; }
        public string LoaiDauMoName { get; set; }
        public string DonViTinh { get; set; }       
        public string DienGiai { get; set; }
        public decimal DinhMuc { get; set; }
        public decimal TieuThu { get; set; }        
    }
}
