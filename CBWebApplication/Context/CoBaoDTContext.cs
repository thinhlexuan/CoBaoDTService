using Microsoft.EntityFrameworkCore;
using CBWebApplication.Models;

namespace CBWebApplication.Context
{
    public partial class CoBaoDTContext: DbContext
    {
        public CoBaoDTContext()
        { }
        public CoBaoDTContext(DbContextOptions<CoBaoDTContext> options):base(options)
        { }
        #region "5 Xí Nghiệp"
        public virtual DbSet<CoBao> CoBao { get; set; }
        public virtual DbSet<CoBaoCT> CoBaoCT { get; set; }
        public virtual DbSet<CoBaoDM> CoBaoDM { get; set; }
        public virtual DbSet<CoBaoTT> View_ThanhTich { get; set; }
        public virtual DbSet<DoanThongView> View_DoanThong { get; set; }
        public virtual DbSet<DoanThong> DoanThong { get; set; }
        public virtual DbSet<DoanThongCT> DoanThongCT { get; set; }
        public virtual DbSet<DoanThongDM> DoanThongDM { get; set; }       
        public virtual DbSet<DMGa> DMGa { get; set; }
        public virtual DbSet<GaChuyenDon> GaChuyenDon { get; set; }
        public virtual DbSet<DauMay> DauMay { get; set; }
        public virtual DbSet<DMDauMay> DMDauMay { get; set; }
        public virtual DbSet<DMTaiXe> DMTaiXe { get; set; }
        public virtual DbSet<DMPhoXe> DMPhoXe { get; set; }
        public virtual DbSet<LoaiMay> LoaiMay { get; set; }
        public virtual DbSet<HeSoQdnl> HeSoQDNL { get; set; }
        public virtual DbSet<DmdonVi> DmdonVi { get; set; }
        public virtual DbSet<DonViDM> DonViDM { get; set; }
        public virtual DbSet<DonViKT> DonViKT { get; set; }
        public virtual DbSet<CongTy> CongTy { get; set; }
        public virtual DbSet<DmtramNhienLieu> DmtramNhienLieu { get; set; }        
        public virtual DbSet<LyTrinh> LyTrinh { get; set; }
        public virtual DbSet<CongTac> CongTac { get; set; }
        public virtual DbSet<LoaiTau> LoaiTau { get; set; }
        public virtual DbSet<DMMacTau> DMMacTau { get; set; }
        public virtual DbSet<MacTau> MacTau { get; set; }
        public virtual DbSet<TinhChat> TinhChat { get; set; }
        public virtual DbSet<Tuyen> Tuyen { get; set; }
        public virtual DbSet<TuyenMap> TuyenMap { get; set; }
        public virtual DbSet<DMLoaiDauMo> DMLoaiDauMo { get; set; }
        public virtual DbSet<CongLenhSK> CongLenhSK { get; set; }
        public virtual DbSet<NhanVien> NhanVien { get; set; }
        public virtual DbSet<DMNhanVien> DMNhanVien { get; set; }
        public virtual DbSet<NhatKy> NhatKy { get; set; }
        public virtual DbSet<MienPhat> MienPhat { get; set; }
        public virtual DbSet<PhienBan> PhienBan { get; set; }
        #endregion

        #region "Cơ Báo Giấy"
        public virtual DbSet<CoBaoGA> CoBaoGA { get; set; }
        public virtual DbSet<CoBaoGACT> CoBaoGACT { get; set; }
        public virtual DbSet<CoBaoGADM> CoBaoGADM { get; set; }       
        public virtual DbSet<DoanThongGA> DoanThongGA { get; set; }
        public virtual DbSet<DoanThongGACT> DoanThongGACT { get; set; }
        public virtual DbSet<DoanThongGADM> DoanThongGADM { get; set; }
        public virtual DbSet<DoanThongGAView> DoanThongGAView { get; set; }
        #endregion

        #region "Yên Viên"
        public virtual DbSet<YVKhuDoan> YV_KhuDoan { get; set; }
        public virtual DbSet<YVNLDinhMuc> YV_NLDinhMuc { get; set; }
        public virtual DbSet<YVNLPDinhMuc> YV_NLPDinhMuc { get; set; }
        public virtual DbSet<YVDMDinhMuc> YV_DMDinhMuc { get; set; }
        #endregion

        #region "Hà Nội"
        public virtual DbSet<HNKhuDoan> HN_KhuDoan { get; set; }
        public virtual DbSet<HNNLDinhMuc> HN_NLDinhMuc { get; set; }
        public virtual DbSet<HNNLPDinhMuc> HN_NLPDinhMuc { get; set; }
        public virtual DbSet<HNDMDinhMuc> HN_DMDinhMuc { get; set; }
        public virtual DbSet<HNPhieuThuong> HN_PhieuThuong { get; set; }
        public virtual DbSet<HNXuatDT> HN_XuatDT { get; set; }
        #endregion

        #region "Vinh"
        public virtual DbSet<VIKhuDoan> VI_KhuDoan { get; set; }
        public virtual DbSet<VINLDinhMuc> VI_NLDinhMuc { get; set; }
        public virtual DbSet<VINLDDinhMuc> VI_NLDDinhMuc { get; set; }
        public virtual DbSet<VIHSTan> VI_HSTan { get; set; }
        public virtual DbSet<VIDMDinhMuc> VI_DMDinhMuc { get; set; }
        #endregion

        #region "Đà Nẵng"
        public virtual DbSet<DNKhuDoan> DN_KhuDoan { get; set; }
        public virtual DbSet<DNNLDinhMuc> DN_NLDinhMuc { get; set; }
        public virtual DbSet<DNNLDinhMucTemp> DinhMucDN { get; set; }
        #endregion

        #region "Sài Gòn"
        public virtual DbSet<SGKhuDoan> SG_KhuDoan { get; set; }
        public virtual DbSet<SGNLDinhMuc> SG_NLDinhMuc { get; set; }
        public virtual DbSet<SGHSTan> SG_HSTan { get; set; }
        public virtual DbSet<SGXuatDT> SG_XuatDT { get; set; }

        #endregion

        #region "Đường Sắt"       
        public virtual DbSet<DSNLDinhMuc> DS_NLDinhMuc { get; set; }
        public virtual DbSet<CapSCCB> CapSCCB { get; set; }
        public virtual DbSet<LoaiKeHoach> LoaiKeHoach { get; set; }
        public virtual DbSet<KeHoach> KeHoach { get; set; }
        #endregion

        #region "Nhiên Liệu"       
        public virtual DbSet<NL_NhaCC> NL_NhaCC{ get; set; }
        public virtual DbSet<NL_HopDong> NL_HopDong { get; set; }
        public virtual DbSet<NL_BangGia> NL_BangGia { get; set; }
        public virtual DbSet<NL_54BASTM> NL_54BASTM { get; set; }
        public virtual DbSet<NL_PhieuNhap> NL_PhieuNhap { get; set; }
        public virtual DbSet<NL_PhieuNhapCT> NL_PhieuNhapCT { get; set; }
        public virtual DbSet<NL_PhieuXuat> NL_PhieuXuat { get; set; }
        public virtual DbSet<NL_PhieuXuatCT> NL_PhieuXuatCT { get; set; }
        #endregion

        #region "Thống Kê"       
        public virtual DbSet<ThongKeDM> ThongKeDM { get; set; }
        public virtual DbSet<ThongKeSL> ThongKeSL { get; set; }
        #endregion

        #region "Xuất Dữ Liệu"       
        public virtual DbSet<DL_NguoiDung> DL_NguoiDung { get; set; }       
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoBaoGACT>().HasKey(c => new { c.CoBaoID, c.GioDen,c.GioDi,c.MacTauID,c.GaID,c.KmAdd });
            modelBuilder.Entity<CoBaoGADM>().HasKey(c => new { c.CoBaoID, c.LoaiDauMoID });
            modelBuilder.Entity<DoanThongGACT>().HasKey(c => new { c.DoanThongID, c.STT });
            modelBuilder.Entity<DoanThongGADM>().HasKey(c => new { c.DoanThongID, c.LoaiDauMoID });
            modelBuilder.Entity<GaChuyenDon>().HasKey(c => new { c.GaId, c.NgayHL });
            modelBuilder.Entity<NL_PhieuNhapCT>().HasKey(c => new { c.PhieuNhapID, c.MaDauMo });
            modelBuilder.Entity<NL_PhieuXuatCT>().HasKey(c => new { c.PhieuXuatID, c.MaDauMo, c.PhieuNhapID });
            modelBuilder.Entity<NL_BangGia>().HasKey(c => new { c.MaTramNL, c.MaDauMo, c.NgayHL });
        }
    }
}
