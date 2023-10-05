using CBWebApplication.Context;
using CBWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoBaoGAsController : ControllerBase
    {
        private readonly CoBaoDTContext _context;
        public CoBaoGAsController(CoBaoDTContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetCoBaoGAView")]
        public async Task<ActionResult<IEnumerable<CoBaoGA>>> GetCoBaoGAView(string MaDV, DateTime NgayBD, DateTime NgayKT, string DauMay, string SoCB, string TaiXe, string TrangThai)
        {
            List<CoBaoGA> list = new List<CoBaoGA>();
            DateTime _ngayKT = NgayKT.AddDays(1);
            var queryGA = from item in _context.CoBaoGA where item.NgayCB >= NgayBD && item.NgayCB < _ngayKT select item;            
            if (MaDV != "TCT")
                queryGA = queryGA.Where(x => x.DvcbID == MaDV);
            if (!string.IsNullOrWhiteSpace(DauMay))
                queryGA = queryGA.Where(x => x.DauMayID.Contains(DauMay));
            if (!string.IsNullOrWhiteSpace(SoCB))
                queryGA = queryGA.Where(x => x.SoCB.Contains(SoCB));
            if (!string.IsNullOrWhiteSpace(TaiXe))
                queryGA = queryGA.Where(x => x.TaiXe1ID.Contains(TaiXe) || x.PhoXe1ID.Contains(TaiXe) || x.TaiXe2ID.Contains(TaiXe) || x.PhoXe2ID.Contains(TaiXe) || x.TaiXe3ID.Contains(TaiXe) || x.PhoXe3ID.Contains(TaiXe));
            if (TrangThai != "Tất cả")
            {
                if (TrangThai == "Thêm mới")
                    queryGA = queryGA.Where(x => x.TrangThai.Contains(TrangThai));
                else
                    queryGA = queryGA.Where(x => x.TrangThai!= "Thêm mới");
            }
            await queryGA.ToListAsync();
           foreach(CoBaoGA cb in queryGA)
            {
                cb.TrangThai = cb.CoBaoGoc == 0 ? "Thêm mới" : "Đã chuyển";
                list.Add(cb);
            }
            if (TrangThai == "Tất cả" || TrangThai == "Chưa chuyển")
            {
                var queryDT = from item in _context.CoBao where item.NgayCB >= NgayBD && item.NgayCB < _ngayKT select item;  
                if (MaDV != "TCT")
                    queryDT = queryDT.Where(x => x.DvcbID == MaDV);
                if (!string.IsNullOrWhiteSpace(DauMay))
                    queryDT = queryDT.Where(x => x.DauMayID.Contains(DauMay));
                if (!string.IsNullOrWhiteSpace(SoCB))
                    queryDT = queryDT.Where(x => x.SoCB.Contains(SoCB));
                if (!string.IsNullOrWhiteSpace(TaiXe))
                    queryDT = queryDT.Where(x => x.TaiXe1ID.Contains(TaiXe) || x.PhoXe1ID.Contains(TaiXe) || x.TaiXe2ID.Contains(TaiXe) || x.PhoXe2ID.Contains(TaiXe) || x.TaiXe3ID.Contains(TaiXe) || x.PhoXe3ID.Contains(TaiXe));
                await queryDT.ToListAsync();

                var queryTH = from item in queryDT
                                     where !queryGA.Any(x => x.CoBaoGoc == item.CoBaoGoc)
                                     select item;
                
                foreach (CoBao cbDT in queryTH)
                {
                    CoBaoGA cb = new CoBaoGA();                   
                    cb.CoBaoID = 0;
                    cb.CoBaoGoc = cbDT.CoBaoGoc;
                    cb.DauMayID = cbDT.DauMayID;
                    cb.DvdmID = cbDT.DvdmID;
                    cb.DvdmName = cbDT.DvdmName;
                    cb.LoaiMayID = cbDT.LoaiMayID;
                    cb.SoCB = cbDT.SoCB;
                    cb.DvcbID = cbDT.DvcbID;
                    cb.DvcbName = cbDT.DvcbName;
                    cb.NgayCB = cbDT.NgayCB;
                    cb.RutGio = cbDT.RutGio;
                    cb.ChatLuong = cbDT.ChatLuong;
                    cb.SoLanRaKho = cbDT.SoLanRaKho;

                    cb.TaiXe1ID = cbDT.TaiXe1ID;
                    cb.TaiXe1Name = cbDT.TaiXe1Name;
                    cb.TaiXe1GioLT = cbDT.TaiXe1GioLT;
                    cb.PhoXe1ID = cbDT.PhoXe1ID;
                    cb.PhoXe1Name = cbDT.PhoXe1Name;
                    cb.PhoXe1GioLT = cbDT.PhoXe1GioLT;
                    cb.TaiXe2ID = cbDT.TaiXe2ID;
                    cb.TaiXe2Name = cbDT.TaiXe2Name;
                    cb.TaiXe2GioLT = cbDT.TaiXe2GioLT;
                    cb.PhoXe2ID = cbDT.PhoXe2ID;
                    cb.PhoXe2Name = cbDT.PhoXe2Name;
                    cb.PhoXe2GioLT = cbDT.PhoXe2GioLT;
                    cb.TaiXe3ID = cbDT.TaiXe3ID;
                    cb.TaiXe3Name = cbDT.TaiXe3Name;
                    cb.TaiXe3GioLT = cbDT.TaiXe3GioLT;
                    cb.PhoXe3ID = cbDT.PhoXe3ID;
                    cb.PhoXe3Name = cbDT.PhoXe3Name;
                    cb.PhoXe3GioLT = cbDT.PhoXe3GioLT;

                    cb.LenBan = cbDT.LenBan;
                    cb.NhanMay = cbDT.NhanMay;
                    cb.RaKho = cbDT.RaKho;

                    cb.VaoKho = cbDT.VaoKho;
                    cb.GiaoMay = cbDT.GiaoMay;
                    cb.XuongBan = cbDT.XuongBan;

                    cb.NLBanTruoc = cbDT.NLBanTruoc;
                    cb.NLThucNhan = cbDT.NLThucNhan;
                    cb.NLLinh = cbDT.NLLinh;
                    cb.TramNLID = cbDT.TramNLID;
                    cb.NLTrongDoan = cbDT.NLTrongDoan;
                    cb.NLBanSau = cbDT.NLBanSau;

                    cb.SHDT = cbDT.SHDT;
                    cb.MaCB = cbDT.MaCB;
                    cb.DonDocDuong = cbDT.DonDocDuong;
                    cb.DungDocDuong = cbDT.DungDocDuong;
                    cb.DungNoMay = cbDT.DungNoMay;
                    cb.GhiChu = cbDT.GhiChu;
                    cb.GaID = cbDT.GaID;
                    cb.GaName = cbDT.GaName;

                    cb.Createddate = cbDT.Createddate;
                    cb.Createdby = cbDT.Createdby;
                    cb.CreatedName = cbDT.CreatedName;
                    cb.Modifydate = cbDT.Modifydate;
                    cb.Modifyby = cbDT.Modifyby;
                    cb.ModifyName = cbDT.ModifyName;
                    cb.TrangThai = "Chưa chuyển";

                    cb.KhoaCB = cbDT.KhoaCB;
                    cb.KhoaCBdate = cbDT.Modifydate;
                    cb.KhoaCBby = cbDT.Modifyby;
                    cb.KhoaCBName = cbDT.ModifyName;
                    list.Add(cb);
                }
                if (TrangThai == "Chưa chuyển")
                    list=list.Where(x => x.TrangThai == "Chưa chuyển").ToList();

            }            
            return list.OrderBy(x=>x.LenBan).ToList();
        }

        [HttpGet]
        [Route("GetCoBaoGADMView")]
        public async Task<ActionResult<IEnumerable<CoBaoGADM>>> GetCoBaoGADMView(long cobaoID,long cobaoGoc)
        {
            if (cobaoID != 0)
                return await _context.CoBaoGADM.Where(x => x.CoBaoID == cobaoID).ToListAsync();
            else
            {
                var querry = await _context.CoBao.Where(x => x.CoBaoGoc == cobaoGoc).FirstOrDefaultAsync();
                if (querry != null)
                {
                    cobaoID = querry.CoBaoID;
                    return await (from item in _context.CoBaoDM
                              where item.CoBaoID == cobaoID
                              select new CoBaoGADM
                              {
                                  CoBaoID = 0,
                                  LoaiDauMoID = item.LoaiDauMoID,
                                  LoaiDauMoName = item.LoaiDauMoName,
                                  DonViTinh = item.DonViTinh,
                                  Nhan = item.Nhan,
                                  Linh = item.Linh,
                                  MaTram = item.MaTram,
                                  TenTram = item.TenTram,
                                  Giao = item.Giao
                              }).OrderBy(x=>x.LoaiDauMoID).ToListAsync();
                }
                else
                    return NotFound();
            }
        }

        [HttpGet]
        [Route("GetCoBaoGACTView")]
        public async Task<ActionResult<IEnumerable<CoBaoGACT>>> GetCoBaoGACTView(long cobaoID, long cobaoGoc)
        {
            if (cobaoID != 0)
                return await _context.CoBaoGACT.Where(x => x.CoBaoID == cobaoID).ToListAsync();
            else
            {
                var query = await _context.CoBao.Where(x => x.CoBaoGoc == cobaoGoc).FirstOrDefaultAsync();
                if (query != null)
                {
                    cobaoID = query.CoBaoID;
                    var queryct = await (from item in _context.CoBaoCT where item.CoBaoID == cobaoID select item).ToListAsync();
                    return (from item in queryct
                                  select new CoBaoGACT
                                  {
                                      CoBaoID = 0,
                                      NgayXP = item.NgayXP,
                                      GioDen = item.GioDen,
                                      GioDi = item.GioDi,
                                      PhutDon = item.GioDon,
                                      MacTauID = item.MacTauID,
                                      CongTyID = item.CongTyID,
                                      CongTyName = item.CongTyName,
                                      TauID=item.TauID,
                                      LoaiTauID = item.CongTacID,
                                      LoaiTauName = item.CongTacName,
                                      GaID = item.GaID,
                                      GaName = item.GaName,
                                      TuyenID = item.TuyenID,
                                      TuyenName = item.TuyenName,
                                      Tan = item.Tan,
                                      XeTotal = item.XeTotal,
                                      TanXeRong = item.TanXeRong,
                                      XeRongTotal = item.XeRongTotal,
                                      TinhChatID = item.TinhChatID,
                                      TinhChatName = item.TinhChatName,
                                      MayGhepID = item.MayGhepID,
                                      KmAdd = item.KmAdd
                                  }).OrderBy(x => x.GioDi).ToList();
                }
                else
                    return NotFound();
            }
        }

        [HttpGet]
        [Route("GetCoBaoGAALL")]
        public async Task<ActionResult<CoBaoGAALL>> GetCoBaoGAALL(long id)
        {
            var coBaoGA = await _context.CoBaoGA.FindAsync(id);
            if (coBaoGA == null)
            {
                return NotFound();
            }
            coBaoGA.coBaoGACTs = await _context.CoBaoGACT.Where(x => x.CoBaoID == id).ToListAsync();
            coBaoGA.coBaoGADMs = await _context.CoBaoGADM.Where(x => x.CoBaoID == id).ToListAsync();

            var doanThongGA = await _context.DoanThongGA.FindAsync(id);
            doanThongGA.doanThongGACTs = await _context.DoanThongGACT.Where(x => x.DoanThongID == id).ToListAsync();
            doanThongGA.doanThongGADMs = await _context.DoanThongGADM.Where(x => x.DoanThongID == id).ToListAsync();
            var coBaoGAAll = new CoBaoGAALL();
            coBaoGAAll.CoBaoID = coBaoGA.CoBaoID;
            coBaoGAAll.coBaoGAs = coBaoGA;
            coBaoGAAll.doanThongGAs = doanThongGA;
            return coBaoGAAll;
        }

        [HttpGet]
        [Route("GetCoBaoGocID")]
        public async Task<ActionResult<CoBaoGA>> GetCoBaoGocID(long id, long cobaoGoc)
        {
            var cb = await _context.CoBaoGA.Where(x => x.CoBaoID == id).FirstOrDefaultAsync();           
            if (cb != null)
            {
                cb.coBaoGACTs = await _context.CoBaoGACT.Where(x => x.CoBaoID == cb.CoBaoID).ToListAsync();
                cb.coBaoGADMs = await _context.CoBaoGADM.Where(x => x.CoBaoID == cb.CoBaoID).ToListAsync();
            }
            else
            {
                var cbDT = await _context.CoBao.Where(x => x.CoBaoGoc == cobaoGoc).FirstOrDefaultAsync();
                if(cbDT != null)
                {
                    cb = new CoBaoGA();
                    cb.CoBaoID = 0;
                    cb.CoBaoGoc = cbDT.CoBaoGoc;
                    cb.DauMayID = cbDT.DauMayID;
                    cb.DvdmID = cbDT.DvdmID;
                    cb.DvdmName = cbDT.DvdmName;
                    cb.LoaiMayID = cbDT.LoaiMayID;
                    cb.SoCB = cbDT.SoCB;
                    cb.DvcbID = cbDT.DvcbID;
                    cb.DvcbName = cbDT.DvcbName;
                    cb.NgayCB = cbDT.NgayCB;
                    cb.RutGio = cbDT.RutGio;
                    cb.ChatLuong = cbDT.ChatLuong;
                    cb.SoLanRaKho = cbDT.SoLanRaKho;

                    cb.TaiXe1ID = cbDT.TaiXe1ID;
                    cb.TaiXe1Name = cbDT.TaiXe1Name;
                    cb.TaiXe1GioLT = cbDT.TaiXe1GioLT;
                    cb.PhoXe1ID = cbDT.PhoXe1ID;
                    cb.PhoXe1Name = cbDT.PhoXe1Name;
                    cb.PhoXe1GioLT = cbDT.PhoXe1GioLT;
                    cb.TaiXe2ID = cbDT.TaiXe2ID;
                    cb.TaiXe2Name = cbDT.TaiXe2Name;
                    cb.TaiXe2GioLT = cbDT.TaiXe2GioLT;
                    cb.PhoXe2ID = cbDT.PhoXe2ID;
                    cb.PhoXe2Name = cbDT.PhoXe2Name;
                    cb.PhoXe2GioLT = cbDT.PhoXe2GioLT;
                    cb.TaiXe3ID = cbDT.TaiXe3ID;
                    cb.TaiXe3Name = cbDT.TaiXe3Name;
                    cb.TaiXe3GioLT = cbDT.TaiXe3GioLT;
                    cb.PhoXe3ID = cbDT.PhoXe3ID;
                    cb.PhoXe3Name = cbDT.PhoXe3Name;
                    cb.PhoXe3GioLT = cbDT.PhoXe3GioLT;

                    cb.LenBan = cbDT.LenBan;
                    cb.NhanMay = cbDT.NhanMay;
                    cb.RaKho = cbDT.RaKho;

                    cb.VaoKho = cbDT.VaoKho;
                    cb.GiaoMay = cbDT.GiaoMay;
                    cb.XuongBan = cbDT.XuongBan;

                    cb.NLBanTruoc = cbDT.NLBanTruoc;
                    cb.NLThucNhan = cbDT.NLThucNhan;
                    cb.NLLinh = cbDT.NLLinh;
                    cb.TramNLID = cbDT.TramNLID;
                    cb.NLTrongDoan = cbDT.NLTrongDoan;
                    cb.NLBanSau = cbDT.NLBanSau;

                    cb.SHDT = cbDT.SHDT;
                    cb.MaCB = cbDT.MaCB;
                    cb.DonDocDuong = cbDT.DonDocDuong;
                    cb.DungDocDuong = cbDT.DungDocDuong;
                    cb.DungNoMay = cbDT.DungNoMay;
                    cb.GhiChu = cbDT.GhiChu;
                    cb.GaID = cbDT.GaID;
                    cb.GaName = cbDT.GaName;

                    cb.Createddate = cbDT.Createddate;
                    cb.Createdby = cbDT.Createdby;
                    cb.CreatedName = cbDT.CreatedName;
                    cb.Modifydate = cbDT.Modifydate;
                    cb.Modifyby = cbDT.Modifyby;
                    cb.ModifyName = cbDT.ModifyName;
                    cb.TrangThai = "Chưa chuyển";

                    cb.KhoaCB = cbDT.KhoaCB;
                    cb.KhoaCBdate = cbDT.Modifydate;
                    cb.KhoaCBby = cbDT.Modifyby;
                    cb.KhoaCBName = cbDT.ModifyName;
                    //Nạp chi tiết
                    var queryct = await (from x in _context.CoBaoCT where x.CoBaoID == cbDT.CoBaoID select x).ToListAsync();
                    var listCT= (from item in queryct
                            select new CoBaoGACT
                            {
                                CoBaoID = 0,
                                NgayXP = item.NgayXP,
                                GioDen = item.GioDen,
                                GioDi = item.GioDi,
                                PhutDon = item.GioDon,
                                MacTauID = item.MacTauID,
                                CongTyID = item.CongTyID,
                                CongTyName = item.CongTyName,
                                TauID = item.TauID,
                                LoaiTauID = item.CongTacID,
                                LoaiTauName = item.CongTacName,
                                GaID = item.GaID,
                                GaName = item.GaName,
                                TuyenID = item.TuyenID,
                                TuyenName = item.TuyenName,
                                Tan = item.Tan,
                                XeTotal = item.XeTotal,
                                TanXeRong = item.TanXeRong,
                                XeRongTotal = item.XeRongTotal,
                                TinhChatID = item.TinhChatID,
                                TinhChatName = item.TinhChatName,
                                MayGhepID = item.MayGhepID,
                                KmAdd = item.KmAdd
                            }).OrderBy(x => x.GioDi).ToList();
                    cb.coBaoGACTs = listCT;
                    //Nạp dầu mỡ
                    var querydm = await (from x in _context.CoBaoDM where x.CoBaoID == cbDT.CoBaoID select x).ToListAsync();
                    var listDM = (from item in querydm
                                  select new CoBaoGADM
                                  {
                                      CoBaoID = 0,
                                      LoaiDauMoID = item.LoaiDauMoID,
                                      LoaiDauMoName = item.LoaiDauMoName,
                                      DonViTinh = item.DonViTinh,
                                      Nhan = item.Nhan,
                                      Linh = item.Linh,
                                      MaTram = item.MaTram,
                                      TenTram = item.TenTram,
                                      Giao = item.Giao
                                  }).OrderBy(x => x.LoaiDauMoID).ToList();
                    cb.coBaoGADMs = listDM;
                }
                else
                {
                    return NotFound();
                }    
            }    
            return cb;
        }

        [HttpGet]
        [Route("GetThanhTichGA")]
        public async Task<ActionResult<CoBaoTT>> GetThanhTichGA(long id)
        {
            var query = from dt in _context.DoanThongGA
                        join cb in _context.CoBaoGA on dt.DoanThongID equals cb.CoBaoID
                        join ct in _context.DoanThongGACT on dt.DoanThongID equals ct.DoanThongID
                        where cb.CoBaoID == id
                        select new CoBaoTT
                        {
                            CoBaoID = (long)cb.CoBaoID,
                            CoBaoGoc = (long)cb.CoBaoGoc,
                            SoCB = (string)cb.SoCB,
                            NgayCB = (DateTime)cb.NgayCB,
                            DauMayID = (string)cb.DauMayID,
                            LoaiMayID = (string)cb.LoaiMayID,
                            QuayVong = (int)(ct.QuayVong+ ct.DungDM + ct.DungDN),
                            LuHanh = (int)ct.LuHanh,
                            DonThuan = (int)ct.DonThuan,
                            GioDon = (int)(ct.DonXP + ct.DonDD + ct.DonKT),
                            GioDung = (int)(ct.DungDM + ct.DungDN + ct.DungXP + ct.DungDD + ct.DungQD + ct.DungKT),
                            KM = (decimal)(ct.KMChinh + ct.KMDon + ct.KMGhep + ct.KMDay),
                            TKM = (decimal)(ct.TKMChinh + ct.TKMDon + ct.TKMGhep + ct.TKMDay),
                            NLTieuThu = (decimal)ct.TieuThu,
                            NLTieuChuan = (decimal)ct.DinhMuc
                        };
            CoBaoTT cbBaoTT = await (from x in query
                              group x by new { x.CoBaoID, x.CoBaoGoc, x.SoCB, x.NgayCB, x.DauMayID, x.LoaiMayID } into g
                              select new CoBaoTT
                              {
                                  CoBaoID = g.Key.CoBaoID,
                                  CoBaoGoc = g.Key.CoBaoGoc,
                                  SoCB = g.Key.SoCB,
                                  NgayCB = g.Key.NgayCB,
                                  DauMayID = g.Key.DauMayID,
                                  LoaiMayID = g.Key.LoaiMayID,
                                  QuayVong = g.Sum(x => x.QuayVong),
                                  LuHanh = g.Sum(x => x.LuHanh),
                                  DonThuan = g.Sum(x => x.DonThuan),
                                  GioDon = g.Sum(x => x.GioDon),
                                  GioDung = g.Sum(x => x.GioDung),
                                  KM = g.Sum(x => x.KM),
                                  TKM = g.Sum(x => x.TKM),
                                  NLTieuThu = g.Sum(x => x.NLTieuThu),
                                  NLTieuChuan = g.Sum(x => x.NLTieuChuan),
                                  NLLoiLo = g.Sum(x => x.NLTieuChuan) - g.Sum(x => x.NLTieuThu)
                              }).FirstOrDefaultAsync();
            return cbBaoTT;
        }

        [HttpGet]
        [Route("GetDoanThongGAView")]
        public async Task<ActionResult<IEnumerable<DoanThongGAView>>> GetDoanThongGAView(int ThangDT, int NamDT, string LoaiMay, string DonVi, String DauMay, string SoCB, string TaiXe, string MacTau)
        {
            var query = from dt in _context.DoanThongGA
                        join cb in _context.CoBaoGA on dt.DoanThongID equals cb.CoBaoID
                        where dt.ThangDT == ThangDT && dt.NamDT == NamDT
                        select new DoanThongGAView
                        {
                            DoanThongID = dt.DoanThongID,
                            CoBaoGoc=cb.CoBaoGoc,
                            SoCB=cb.SoCB,
                            DauMayID=cb.DauMayID,
                            LoaiMayID=cb.LoaiMayID,
                            DvdmName=cb.DvdmName,
                            DvcbID=cb.DvcbID,
                            DvcbName=cb.DvcbName,
                            NgayCB=cb.NgayCB,
                            TaiXe1ID=cb.TaiXe1ID,
                            TaiXe1Name=cb.TaiXe1Name,
                            PhoXe1ID=cb.PhoXe1ID,
                            PhoXe1Name=cb.PhoXe1Name,
                            TaiXe2ID=cb.TaiXe2ID,
                            TaiXe2Name=cb.TaiXe2Name,
                            PhoXe2ID=cb.PhoXe2ID,
                            PhoXe2Name=cb.PhoXe2Name,
                            TaiXe3ID=cb.TaiXe3ID,
                            TaiXe3Name=cb.TaiXe3Name,
                            PhoXe3ID=cb.PhoXe3ID,
                            PhoXe3Name=cb.PhoXe3Name,
                            KiemTra1ID=cb.KiemTra1ID,
                            KiemTra1Name=cb.KiemTra1Name,
                            KiemTra2ID=cb.KiemTra2ID,
                            KiemTra2Name=cb.KiemTra2Name,
                            KiemTra3ID=cb.KiemTra3ID,
                            KiemTra3Name=cb.KiemTra3Name,
                            LenBan=cb.LenBan,
                            NhanMay=cb.NhanMay,
                            RaKho=cb.RaKho,
                            VaoKho=cb.VaoKho,
                            GiaoMay=cb.GiaoMay,
                            XuongBan=cb.XuongBan,
                            DungKB=dt.DungKB,
                            NLBanTruoc=cb.NLBanTruoc,
                            NLThucNhan=cb.NLThucNhan,
                            NLLinh=cb.NLLinh,
                            TramNLID=cb.TramNLID,
                            NLTrongDoan=cb.NLTrongDoan,
                            NLBanSau=cb.NLBanSau,
                            ThangDT=dt.ThangDT,NamDT=dt.NamDT,
                            Createddate=dt.Createddate,
                            Createdby=dt.Createdby,
                            CreatedName=dt.CreatedName,
                            Modifydate=dt.Modifydate,
                            Modifyby=dt.Modifyby,
                            ModifyName=dt.ModifyName
                        
                        };
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay);
            if (DonVi != "TCT")
                query = query.Where(x => x.DvcbID == DonVi);
            if (!string.IsNullOrWhiteSpace(DauMay))
                query = query.Where(x => x.DauMayID.Contains(DauMay));
            if (!string.IsNullOrWhiteSpace(SoCB))
                query = query.Where(x => x.SoCB.Contains(SoCB));
            if (!string.IsNullOrWhiteSpace(TaiXe))
                query = query.Where(x => x.TaiXe1ID.Contains(TaiXe) || x.PhoXe1ID.Contains(TaiXe) || x.TaiXe2ID.Contains(TaiXe) || x.PhoXe2ID.Contains(TaiXe) || x.TaiXe3ID.Contains(TaiXe) || x.PhoXe3ID.Contains(TaiXe));
            //return await query.ToListAsync();
            if (!string.IsNullOrWhiteSpace(MacTau))
            {
                return await (from dt in query
                              join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                              where ct.MacTauID.Contains(MacTau)
                              select dt).ToListAsync();
            }
            else
                return await query.ToListAsync();
        }

        [HttpGet]
        [Route("GetTTDoanThong")]
        public async Task<ActionResult<IEnumerable<DoanThongGAView>>> GetTTDoanThong(DateTime NgayBD, DateTime NgayKT, string LoaiMay, string DonVi, String DauMay, string SoCB, string TaiXe, string MacTau)
        {
            DateTime _ngayKT = NgayKT.AddDays(1);
            var query = from dt in _context.DoanThongGA
                        join cb in _context.CoBaoGA on dt.DoanThongID equals cb.CoBaoID
                        where cb.NgayCB >= NgayBD && cb.NgayCB < _ngayKT
                        select new DoanThongGAView
                        {
                            DoanThongID = dt.DoanThongID,
                            CoBaoGoc = cb.CoBaoGoc,
                            SoCB = cb.SoCB,
                            DauMayID = cb.DauMayID,
                            LoaiMayID = cb.LoaiMayID,
                            DvdmName = cb.DvdmName,
                            DvcbID = cb.DvcbID,
                            DvcbName = cb.DvcbName,
                            NgayCB = cb.NgayCB,
                            TaiXe1ID = cb.TaiXe1ID,
                            TaiXe1Name = cb.TaiXe1Name,
                            PhoXe1ID = cb.PhoXe1ID,
                            PhoXe1Name = cb.PhoXe1Name,
                            TaiXe2ID = cb.TaiXe2ID,
                            TaiXe2Name = cb.TaiXe2Name,
                            PhoXe2ID = cb.PhoXe2ID,
                            PhoXe2Name = cb.PhoXe2Name,
                            TaiXe3ID = cb.TaiXe3ID,
                            TaiXe3Name = cb.TaiXe3Name,
                            PhoXe3ID = cb.PhoXe3ID,
                            PhoXe3Name = cb.PhoXe3Name,
                            KiemTra1ID=cb.KiemTra1ID,
                            KiemTra1Name=cb.KiemTra1Name,
                            KiemTra2ID=cb.KiemTra2ID,
                            KiemTra2Name=cb.KiemTra2Name,
                            KiemTra3ID=cb.KiemTra3ID,
                            KiemTra3Name=cb.KiemTra3Name,
                            LenBan = cb.LenBan,
                            NhanMay = cb.NhanMay,
                            RaKho = cb.RaKho,
                            VaoKho = cb.VaoKho,
                            GiaoMay = cb.GiaoMay,
                            XuongBan = cb.XuongBan,
                            DungKB = dt.DungKB,
                            NLBanTruoc = cb.NLBanTruoc,
                            NLThucNhan = cb.NLThucNhan,
                            NLLinh = cb.NLLinh,
                            TramNLID = cb.TramNLID,
                            NLTrongDoan = cb.NLTrongDoan,
                            NLBanSau = cb.NLBanSau,
                            ThangDT = dt.ThangDT,
                            NamDT = dt.NamDT,
                            Createddate = dt.Createddate,
                            Createdby = dt.Createdby,
                            CreatedName = dt.CreatedName,
                            Modifydate = dt.Modifydate,
                            Modifyby = dt.Modifyby,
                            ModifyName = dt.ModifyName

                        };          
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay);
            if (DonVi != "TCT")
                query = query.Where(x => x.DvcbID == DonVi);
            if (!string.IsNullOrWhiteSpace(DauMay))
                query = query.Where(x => x.DauMayID.Contains(DauMay));
            if (!string.IsNullOrWhiteSpace(SoCB))
                query = query.Where(x => x.SoCB.Contains(SoCB));
            if (!string.IsNullOrWhiteSpace(TaiXe))
                query = query.Where(x => x.TaiXe1ID.Contains(TaiXe) || x.PhoXe1ID.Contains(TaiXe) || x.TaiXe2ID.Contains(TaiXe) || x.PhoXe2ID.Contains(TaiXe) || x.TaiXe3ID.Contains(TaiXe) || x.PhoXe3ID.Contains(TaiXe));
            //await query.ToListAsync();
            if (!string.IsNullOrWhiteSpace(MacTau))
            {
                return await (from dt in query
                              join ct in _context.DoanThongGACT on dt.DoanThongID equals ct.DoanThongID
                              where ct.MacTauID.Contains(MacTau)
                              select dt).ToListAsync();
            }
            else
                return await query.ToListAsync();
        }

        [HttpGet]
        [Route("GetDoanThongViewID")]
        public async Task<ActionResult<DoanThongGAView>> GetDoanThongViewID(long id)
        {           
            var doanThong = await (from dt in _context.DoanThongGA
                        join cb in _context.CoBaoGA on dt.DoanThongID equals cb.CoBaoID
                        where dt.DoanThongID==id
                        select new DoanThongGAView
                        {
                            DoanThongID = dt.DoanThongID,
                            CoBaoGoc = cb.CoBaoGoc,
                            SoCB = cb.SoCB,
                            DauMayID = cb.DauMayID,
                            LoaiMayID = cb.LoaiMayID,
                            DvdmName = cb.DvdmName,
                            DvcbID = cb.DvcbID,
                            DvcbName = cb.DvcbName,
                            NgayCB = cb.NgayCB,
                            TaiXe1ID = cb.TaiXe1ID,
                            TaiXe1Name = cb.TaiXe1Name,
                            PhoXe1ID = cb.PhoXe1ID,
                            PhoXe1Name = cb.PhoXe1Name,
                            TaiXe2ID = cb.TaiXe2ID,
                            TaiXe2Name = cb.TaiXe2Name,
                            PhoXe2ID = cb.PhoXe2ID,
                            PhoXe2Name = cb.PhoXe2Name,
                            TaiXe3ID = cb.TaiXe3ID,
                            TaiXe3Name = cb.TaiXe3Name,
                            PhoXe3ID = cb.PhoXe3ID,
                            PhoXe3Name = cb.PhoXe3Name,
                            KiemTra1ID=cb.KiemTra1ID,
                            KiemTra1Name=cb.KiemTra1Name,
                            KiemTra2ID=cb.KiemTra2ID,
                            KiemTra2Name=cb.KiemTra2Name,
                            KiemTra3ID=cb.KiemTra3ID,
                            KiemTra3Name=cb.KiemTra3Name,
                            LenBan = cb.LenBan,
                            NhanMay = cb.NhanMay,
                            RaKho = cb.RaKho,
                            VaoKho = cb.VaoKho,
                            GiaoMay = cb.GiaoMay,
                            XuongBan = cb.XuongBan,
                            DungKB = dt.DungKB,
                            NLBanTruoc = cb.NLBanTruoc,
                            NLThucNhan = cb.NLThucNhan,
                            NLLinh = cb.NLLinh,
                            TramNLID = cb.TramNLID,
                            NLTrongDoan = cb.NLTrongDoan,
                            NLBanSau = cb.NLBanSau,
                            ThangDT = dt.ThangDT,
                            NamDT = dt.NamDT,
                            Createddate = dt.Createddate,
                            Createdby = dt.Createdby,
                            CreatedName = dt.CreatedName,
                            Modifydate = dt.Modifydate,
                            Modifyby = dt.Modifyby,
                            ModifyName = dt.ModifyName

                        }).FirstOrDefaultAsync();
            if (doanThong == null)
            {
                return NotFound();
            }
            return doanThong;
        }
        [HttpGet]
        [Route("GetDoanThongGADMView")]
        public async Task<ActionResult<IEnumerable<DoanThongGADM>>> GetDoanThongGADMView(long id)
        {
            var listdoanthongdm = await _context.DoanThongGADM.Where(x => x.DoanThongID == id).ToListAsync();
            if (listdoanthongdm == null)
            {
                return NotFound();
            }
            return listdoanthongdm;
        }

        [HttpGet]
        [Route("GetDoanThongGACTView")]
        public async Task<ActionResult<IEnumerable<DoanThongGACT>>> GetDoanThongGACTView(long id)
        {
            var listdoanthongct = await _context.DoanThongGACT.Where(x => x.DoanThongID == id).ToListAsync();
            if (listdoanthongct == null)
            {
                return NotFound();
            }
            return listdoanthongct;
        }
        [HttpGet]
        [Route("GetXCoBaoGA")]
        public async Task<ActionResult<IEnumerable<XCoBaoGA>>> GetXCoBaoGA(int thangDT, int namDT, string DonVi, string LoaiMay)
        {
            var query = from cb in _context.CoBaoGA
                        join dt in _context.DoanThongGA on cb.CoBaoID equals dt.DoanThongID
                        where dt.ThangDT == thangDT && dt.NamDT == namDT && cb.DvcbID == DonVi
                        select cb;
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay).OrderBy(x => x.NgayCB);
            return await query.Select(s => new XCoBaoGA
            {
                CoBaoID = s.CoBaoID,
                CoBaoGoc = s.CoBaoGoc,
                SoCB = s.SoCB,
                DauMayID = s.DauMayID,
                LoaiMayID = s.LoaiMayID,
                DvdmID = s.DvdmID,
                DvdmName = s.DvdmName,
                DvcbID = s.DvcbID,
                DvcbName = s.DvcbName,
                NgayCB = s.NgayCB,
                RutGio = s.RutGio,
                ChatLuong = s.ChatLuong,
                SoLanRaKho = s.SoLanRaKho,
                TaiXe1ID = s.TaiXe1ID,
                TaiXe1Name = s.TaiXe1Name,
                TaiXe1GioLT = s.TaiXe1GioLT,
                PhoXe1ID = s.PhoXe1ID,
                PhoXe1Name = s.PhoXe1Name,
                PhoXe1GioLT = s.PhoXe1GioLT,
                TaiXe2ID = s.TaiXe2ID,
                TaiXe2Name = s.TaiXe2Name,
                TaiXe2GioLT = s.TaiXe2GioLT,
                PhoXe2ID = s.PhoXe2ID,
                PhoXe2Name = s.PhoXe2Name,
                PhoXe2GioLT = s.PhoXe2GioLT,
                TaiXe3ID = s.TaiXe3ID,
                TaiXe3Name = s.TaiXe3Name,
                TaiXe3GioLT = s.TaiXe3GioLT,
                PhoXe3ID = s.PhoXe3ID,
                PhoXe3Name = s.PhoXe3Name,
                PhoXe3GioLT = s.PhoXe3GioLT,
                KiemTra1ID=s.KiemTra1ID,
                KiemTra1Name=s.KiemTra1Name,
                KiemTra2ID=s.KiemTra2ID,
                KiemTra2Name=s.KiemTra2Name,
                KiemTra3ID=s.KiemTra3ID,
                KiemTra3Name=s.KiemTra3Name,
                LenBan = s.LenBan,
                NhanMay = s.NhanMay,
                RaKho = s.RaKho,
                VaoKho = s.VaoKho,
                GiaoMay = s.GiaoMay,
                XuongBan = s.XuongBan,
                NLBanTruoc = s.NLBanTruoc,
                NLThucNhan = s.NLThucNhan,
                NLLinh = s.NLLinh,
                TramNLID = s.TramNLID,
                NLTrongDoan = s.NLTrongDoan,
                NLBanSau = s.NLBanSau,
                SHDT = s.SHDT,
                MaCB = s.MaCB,
                DonDocDuong = s.DonDocDuong,
                DungDocDuong = s.DungDocDuong,
                DungNoMay = s.DungNoMay,
                GhiChu = s.GhiChu

            }).ToListAsync();
        }
        [HttpGet]
        [Route("GetXCoBaoGACT")]
        public async Task<ActionResult<IEnumerable<XCoBaoGACT>>> GetXCoBaoGACT(int thangDT, int namDT, string DonVi, string LoaiMay)
        {
            var queryCBID = from cb in _context.CoBaoGA
                            join dt in _context.DoanThongGA on cb.CoBaoID equals dt.DoanThongID
                            where dt.ThangDT == thangDT && dt.NamDT == namDT && cb.DvcbID == DonVi
                            select cb;
            if (LoaiMay != "ALL")
            {
                queryCBID = queryCBID.Where(x => x.LoaiMayID == LoaiMay);
            }
            await queryCBID.ToListAsync();
            var query = from ct in _context.CoBaoGACT
                        join cb in queryCBID on ct.CoBaoID equals cb.CoBaoID
                        select ct;

            return await query.Select(s => new XCoBaoGACT
            {              
                CoBaoID = s.CoBaoID,
                NgayXP = s.NgayXP,
                GioDen = s.GioDen,
                GioDi = s.GioDi,
                PhutDon = s.PhutDon,
                MacTauID = s.MacTauID,
                RutGioNL=s.RutGioNL,
                DungGioPT=s.DungGioPT,
                CongTyID = s.CongTyID,
                CongTyName = s.CongTyName,
                LoaiTauID = s.LoaiTauID,
                LoaiTauName = s.LoaiTauName,
                GaID = s.GaID,
                GaName = s.GaName,
                TuyenID = s.TuyenID,
                TuyenName = s.TuyenName,
                Tan = s.Tan,
                XeTotal = s.XeTotal,
                TanXeRong = s.TanXeRong,
                XeRongTotal = s.XeRongTotal,
                TinhChatID = s.TinhChatID,
                TinhChatName = s.TinhChatName,
                MayGhepID = s.MayGhepID,
                KmAdd = s.KmAdd
            }).ToListAsync();
        }
        [HttpGet]
        [Route("GetXCoBaoGADM")]
        public async Task<ActionResult<IEnumerable<XCoBaoGADM>>> GetXCoBaoGADM(int thangDT, int namDT, string DonVi, string LoaiMay)
        {
            var queryCBID = from cb in _context.CoBaoGA
                            join dt in _context.DoanThongGA on cb.CoBaoID equals dt.DoanThongID
                            where dt.ThangDT == thangDT && dt.NamDT == namDT && cb.DvcbID == DonVi
                            select cb;
            if (LoaiMay != "ALL")
            {
                queryCBID = queryCBID.Where(x => x.LoaiMayID == LoaiMay);
            }
            await queryCBID.ToListAsync();
            var query = from ct in _context.CoBaoGADM
                        join cb in queryCBID on ct.CoBaoID equals cb.CoBaoID
                        select ct;

            return await query.Select(s => new XCoBaoGADM
            {                
                CoBaoID = s.CoBaoID,
                LoaiDauMoID = s.LoaiDauMoID,
                LoaiDauMoName = s.LoaiDauMoName,
                DonViTinh = s.LoaiDauMoName,
                Nhan = s.Nhan,
                Linh = s.Linh,
                MaTram = s.MaTram,
                TenTram = s.TenTram,
                Giao = s.Giao
            }).ToListAsync();
        }

        [HttpPut]
        [Route("PutCoBaoGAALL")]
        public async Task<ActionResult<CoBaoGAALL>> PutCoBaoGAALL(long id, CoBaoGAALL coBaoALL)
        {
            if (id != coBaoALL.CoBaoID)
            {
                return BadRequest();
            }
            coBaoALL.coBaoGAs.Modifydate = DateTime.Now;
            _context.Entry(coBaoALL.coBaoGAs).State = EntityState.Modified;
            //Xóa hết cơ báo chi tiết và thêm mới
            var coBaoCTOld = coBaoALL.coBaoGAs.coBaoGACTs.ToList();
            var coBaoCT = await _context.CoBaoGACT.Where(x => x.CoBaoID == id).ToListAsync();
            if (coBaoCT != null)
            {
                _context.CoBaoGACT.RemoveRange(coBaoCT);
            }
            //Thêm mới cơ báo chi tiết
            if (coBaoCTOld != null)
            {
                foreach (CoBaoGACT ct in coBaoCTOld)
                {
                    ct.CoBaoID = coBaoALL.CoBaoID;
                    _context.CoBaoGACT.Add(ct);
                }
            }
            //Xóa hết cơ báo dầu mỡ và thêm mới
            var coBaoDMOld = coBaoALL.coBaoGAs.coBaoGADMs.ToList();
            var coBaoDM = await _context.CoBaoGADM.Where(x => x.CoBaoID == id).ToListAsync();
            if (coBaoDM != null)
            {
                _context.CoBaoGADM.RemoveRange(coBaoDM);
            }
            //Thêm mới cơ báo dầu mỡ
            if (coBaoDMOld != null)
            {
                foreach (CoBaoGADM ct in coBaoDMOld)
                {                    
                    ct.CoBaoID = coBaoALL.CoBaoID;
                    _context.CoBaoGADM.Add(ct);
                }
            }

            //Đối với đoạn thống thì phần chi tiết xóa hết cũ và thêm mới.
            //Xóa
            var doanThong = await _context.DoanThongGA.FindAsync(id);
            if (doanThong != null)
            {
                _context.DoanThongGA.Remove(doanThong);
            }
            var doanThongCT = await _context.DoanThongGACT.Where(x => x.DoanThongID == id).ToListAsync();
            if (doanThongCT != null)
            {
                _context.DoanThongGACT.RemoveRange(doanThongCT);
            }
            var doanThongDM = await _context.DoanThongGADM.Where(x => x.DoanThongID == id).ToListAsync();
            if (doanThongDM != null)
            {
                _context.DoanThongGADM.RemoveRange(doanThongDM);
            }
            //Thêm mới
            coBaoALL.doanThongGAs.DoanThongID = coBaoALL.CoBaoID;
            coBaoALL.doanThongGAs.Createddate = coBaoALL.coBaoGAs.Createddate;
            coBaoALL.doanThongGAs.Modifydate = coBaoALL.coBaoGAs.Modifydate;
            if (coBaoALL.doanThongGAs.doanThongGACTs != null)
            {
                foreach (DoanThongGACT ct in coBaoALL.doanThongGAs.doanThongGACTs)
                {                    
                    ct.DoanThongID = coBaoALL.CoBaoID;
                }
            }
            if (coBaoALL.doanThongGAs.doanThongGADMs != null)
            {
                foreach (DoanThongGADM ct in coBaoALL.doanThongGAs.doanThongGADMs)
                {                   
                    ct.DoanThongID = coBaoALL.CoBaoID;
                }
            }
            _context.DoanThongGA.Add(coBaoALL.doanThongGAs);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {                
                    return NotFound();                
            }
            return coBaoALL;
        }

        [HttpPut]
        [Route("PutDoanThongGAALL")]
        public async Task<ActionResult<DoanThongGA>> PutDoanThongGAALL(long id, DoanThongGA doanthongGA)
        {
            if (id != doanthongGA.DoanThongID)
            {
                return BadRequest();
            }
            //Đối với đoạn thống thì phần chi tiết xóa hết cũ và thêm mới.
            //Xóa
            var doanThong = await _context.DoanThongGA.FindAsync(id);
            if (doanThong != null)
            {
                _context.DoanThongGA.Remove(doanThong);
            }
            var doanThongCT = await _context.DoanThongGACT.Where(x => x.DoanThongID == id).ToListAsync();
            if (doanThongCT != null)
            {
                _context.DoanThongGACT.RemoveRange(doanThongCT);
            }
            var doanThongDM = await _context.DoanThongGADM.Where(x => x.DoanThongID == id).ToListAsync();
            if (doanThongDM != null)
            {
                _context.DoanThongGADM.RemoveRange(doanThongDM);
            }
            //Thêm mới
            doanthongGA.DoanThongID = id;            
            doanthongGA.Modifydate = DateTime.Now;
            if (doanthongGA.doanThongGACTs != null)
            {
                foreach (DoanThongGACT ct in doanthongGA.doanThongGACTs)
                {                   
                    ct.DoanThongID = id;
                }
            }
            if (doanthongGA.doanThongGADMs != null)
            {
                foreach (DoanThongGADM ct in doanthongGA.doanThongGADMs)
                {                    
                    ct.DoanThongID = id;
                }
            }
            _context.DoanThongGA.Add(doanthongGA);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {                
                    return NotFound();
            }
            return doanthongGA;
        }

        [HttpPut]
        [Route("PutCoBaoGA")]
        public async Task<IActionResult> PutCoBaoGA(long id, CoBaoGA coBaoGA)
        {
            if (id != coBaoGA.CoBaoID)
            {
                return BadRequest();
            }
            coBaoGA.Modifydate = DateTime.Now;
            _context.Entry(coBaoGA).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {                
                    return NotFound();
            }
            return NoContent();
        }
        [HttpPut]
        [Route("KhoaCoBaoGA")]
        public async Task<IActionResult> KhoaCoBaoGA(long id, CoBaoGA coBaoGA)
        {
            if (id != coBaoGA.CoBaoID)
            {
                return BadRequest();
            }
            coBaoGA.KhoaCBdate = DateTime.Now;
            _context.Entry(coBaoGA).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut]
        [Route("PutDoanThongGA")]
        public async Task<IActionResult> PutDoanThongGA(long id, DoanThongGA doanthongGA)
        {
            if (id != doanthongGA.DoanThongID)
            {
                return BadRequest();
            }
            doanthongGA.Modifydate = DateTime.Now;
            _context.Entry(doanthongGA).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {                
                    return NotFound();               
            }
            return NoContent();
        }

        [Route("PostCoBaoGAALL")]
        public async Task<ActionResult<CoBaoGAALL>> PostCoBaoGAALL(CoBaoGAALL coBaoALL)
        {
            try
            {
                var cobaoG = _context.CoBaoGA.Where(x => x.CoBaoGoc == coBaoALL.coBaoGAs.CoBaoGoc && coBaoALL.coBaoGAs.CoBaoGoc>0).FirstOrDefault();
                var cobaosl = _context.CoBaoGA.OrderByDescending(x => x.CoBaoID).FirstOrDefault();
                coBaoALL.CoBaoID = cobaosl != null ? cobaosl.CoBaoID + 1 : 1;
                if (cobaoG == null)
                {                   
                    coBaoALL.coBaoGAs.CoBaoID = coBaoALL.CoBaoID;
                    coBaoALL.coBaoGAs.Createddate = DateTime.Now;
                    coBaoALL.coBaoGAs.Modifydate = coBaoALL.coBaoGAs.Createddate;
                    if (coBaoALL.coBaoGAs.coBaoGACTs != null)
                    {
                        foreach (CoBaoGACT ct in coBaoALL.coBaoGAs.coBaoGACTs)
                        {                            
                            ct.CoBaoID = coBaoALL.CoBaoID;
                        }
                    }
                    if (coBaoALL.coBaoGAs.coBaoGADMs != null)
                    {
                        foreach (CoBaoGADM ct in coBaoALL.coBaoGAs.coBaoGADMs)
                        {                           
                            ct.CoBaoID = coBaoALL.CoBaoID;
                        }
                    }

                    coBaoALL.doanThongGAs.DoanThongID = coBaoALL.CoBaoID;
                    coBaoALL.doanThongGAs.Createddate = coBaoALL.coBaoGAs.Createddate;
                    coBaoALL.doanThongGAs.Modifydate = coBaoALL.coBaoGAs.Modifydate;
                   
                    if (coBaoALL.doanThongGAs.doanThongGACTs != null)
                    {
                        foreach (DoanThongGACT ct in coBaoALL.doanThongGAs.doanThongGACTs)
                        {                            
                            ct.DoanThongID = coBaoALL.CoBaoID;                          
                        }
                    }
                    if (coBaoALL.doanThongGAs.doanThongGADMs != null)
                    {
                        foreach (DoanThongGADM ct in coBaoALL.doanThongGAs.doanThongGADMs)
                        {                           
                            ct.DoanThongID = coBaoALL.CoBaoID;
                        }
                    }
                }
                _context.CoBaoGA.Add(coBaoALL.coBaoGAs);
                _context.DoanThongGA.Add(coBaoALL.doanThongGAs);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return coBaoALL;
        }

        [HttpDelete]
        [Route("DeleteCoBaoGAALL")]
        public async Task<ActionResult<CoBaoGA>> DeleteCoBaoGAALL(long id, string manv, string tennv)
        {
            var coBaoGA = await _context.CoBaoGA.FindAsync(id);
            if (coBaoGA == null)
            {
                return NotFound();
            }
            _context.CoBaoGA.Remove(coBaoGA);
            var coBaoGACT = await _context.CoBaoGACT.Where(x => x.CoBaoID == id).ToListAsync();
            if (coBaoGACT != null)
            {
                _context.CoBaoGACT.RemoveRange(coBaoGACT);
            }
            var coBaoGADM = await _context.CoBaoGADM.Where(x => x.CoBaoID == id).ToListAsync();
            if (coBaoGADM != null)
            {
                _context.CoBaoGADM.RemoveRange(coBaoGADM);
            }

            var doanThongGA = await _context.DoanThongGA.FindAsync(id);
            if (doanThongGA != null)
            {
                _context.DoanThongGA.Remove(doanThongGA);
            }
            var doanThongGACT = await _context.DoanThongGACT.Where(x => x.DoanThongID == id).ToListAsync();
            if (doanThongGACT != null)
            {
                _context.DoanThongGACT.RemoveRange(doanThongGACT);
            }
            var doanThongGADM = await _context.DoanThongGADM.Where(x => x.DoanThongID == id).ToListAsync();
            if (doanThongGADM != null)
            {
                _context.DoanThongGADM.RemoveRange(doanThongGADM);
            }
            //Ghi nhật ký xóa cơ báo            
            NhatKy nk = new NhatKy();
            nk.TenBang = "CoBaoGA";
            nk.NoiDung = "Xóa cơ báo giấy: " + coBaoGA.CoBaoID + "-" + coBaoGA.CoBaoGoc + "-" + coBaoGA.SoCB + "-" + coBaoGA.NgayCB;
            nk.Createddate = DateTime.Now;
            nk.Createdby = manv;
            nk.CreatedName = tennv;
            _context.NhatKy.Add(nk);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }            
            return coBaoGA;
        }
    }
}
