using CBWebApplication.Context;
using CBWebApplication.Models;
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
    public class BaoCaosController : ControllerBase
    {
        private readonly CoBaoDTContext _context;

        public BaoCaosController(CoBaoDTContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetBCVanDung")]
        public async Task<ActionResult<IEnumerable<ViewBcvanDung>>> GetBCVanDung(string maDV, int loaiBC, DateTime ngayBD, DateTime ngayKT, string loaiMayID, string tuyenID)
        {

            DateTime _ngayKT = ngayKT.AddDays(1);
            var query = from dt in _context.DoanThong
                        join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                        join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID                       
                        select new
                        {
                            DvcbID = (string)cb.DvcbID,
                            TuyenID = (string)ct.TuyenID,
                            NgayCB = (DateTime)cb.NgayCB,
                            ThangDT = (int)dt.ThangDT,
                            NamDT = (int)dt.NamDT,
                            LoaiMayID = (string)cb.LoaiMayID,
                            CongTacID = (int)ct.CongTacID,
                            TinhChatID = (int)ct.TinhChatID,
                            QuayVong = (int)ct.QuayVong,
                            LuHanh = (int)ct.LuHanh,
                            DonThuan = (int)ct.DonThuan,
                            DungDM = (int)ct.DungDM,
                            DungDN = (int)ct.DungDN,
                            DungKhoDM = (int)ct.DungKhoDM,
                            DungKhoDN = (int)ct.DungKhoDN,
                            DungXP = (int)ct.DungXP,
                            DungDD = (int)ct.DungDD,
                            DungQD = (int)ct.DungQD,
                            DungKT = (int)ct.DungKT,
                            DonXP = (int)ct.DonXP,
                            DonDD = (int)ct.DonDD,
                            DonKT = (int)ct.DonKT,
                            KMChinh = (decimal)ct.KMChinh,
                            KMDon = (decimal)ct.KMDon,
                            KMGhep = (decimal)ct.KMGhep,
                            KMDay = (decimal)ct.KMDay,
                            TKMChinh = (decimal)ct.TKMChinh,
                            TKMDon = (decimal)ct.TKMDon,
                            TKMGhep = (decimal)ct.TKMGhep,
                            TKMDay = (decimal)ct.TKMDay,
                            SLRKDM = (decimal)ct.SLRKDM,
                            SLRKDN = (decimal)ct.SLRKDN,
                            TieuThu = (decimal)ct.TieuThu,
                            DinhMuc = (decimal)ct.DinhMuc
                        };           
            if (loaiBC < 5)
                query = query.Where(x => x.ThangDT >= ngayBD.Month && x.ThangDT <= ngayKT.Month && x.NamDT == ngayKT.Year);
            else
                query = query.Where(x => x.NgayCB >= ngayBD && x.NgayCB < _ngayKT);
            if (maDV != "TCT")
                query = query.Where(x => x.DvcbID == maDV);
            if (loaiMayID != "ALL")
                query = query.Where(x => x.LoaiMayID == loaiMayID);
            if (tuyenID != "ALL")
                query = query.Where(x => tuyenID.Contains(x.TuyenID));
            return await (from x in query
                          group x by new { x.DvcbID, x.TuyenID, x.ThangDT, x.NamDT, x.LoaiMayID, x.CongTacID, x.TinhChatID } into g
                          select new ViewBcvanDung
                          {
                              DvcbID = g.Key.DvcbID,
                              TuyenID = g.Key.TuyenID,
                              ThangDT = g.Key.ThangDT,
                              NamDT = g.Key.NamDT,
                              LoaiMayID = g.Key.LoaiMayID,
                              CongTacID = g.Key.CongTacID,
                              TinhChatID = g.Key.TinhChatID,
                              GioDm = g.Sum(x => x.QuayVong + x.DungDM + x.DungDN + x.DungKhoDM + x.DungKhoDN),
                              GioLh = g.Sum(x => x.LuHanh),
                              GioDt = g.Sum(x => x.DonThuan),
                              Dgxp = g.Sum(x => x.DungXP),
                              Dgdd = g.Sum(x => x.DungDD),
                              Dgqd = g.Sum(x => x.DungQD),
                              Dgcc = g.Sum(x => x.DungKT),
                              Dgdm = g.Sum(x => x.DungDM),
                              Dgdn = g.Sum(x => x.DungDN),
                              Dgkm = g.Sum(x => x.DungKhoDM),
                              Dgkn = g.Sum(x => x.DungKhoDN),
                              Dnxp = g.Sum(x => x.DonXP),
                              Dndd = g.Sum(x => x.DonDD),
                              Dncc = g.Sum(x => x.DonKT),
                              Kmch = g.Sum(x => x.KMChinh),
                              Kmdw = g.Sum(x => x.KMDon),
                              Kmgh = g.Sum(x => x.KMGhep),
                              Kmdy = g.Sum(x => x.KMDay),
                              Tkch = g.Sum(x => x.TKMChinh),
                              Tkdw = g.Sum(x => x.TKMDon),
                              Tkgh = g.Sum(x => x.TKMGhep),
                              Tkdy = g.Sum(x => x.TKMDay),
                              Slrkm = g.Sum(x => x.SLRKDM),
                              Slrkn = g.Sum(x => x.SLRKDN),
                              Sltt = g.Sum(x => x.TieuThu),
                              Sltc = g.Sum(x => x.DinhMuc)
                          }).ToListAsync();
        }

        [HttpGet]
        [Route("GetBCGioDon")]
        public async Task<ActionResult<IEnumerable<ViewBcGioDon>>> GetBCGioDon(string MaDV, int loaiBC, DateTime ngayBD, DateTime ngayKT)
        {
            if (loaiBC < 5)
            {
                //Lấy kiêm dồn và chuyên dồn
                var query1 = from dt in _context.DoanThong
                            join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                            join ct in _context.CoBaoCT on cb.CoBaoID equals ct.CoBaoID
                            where dt.ThangDT >= ngayBD.Month && dt.ThangDT <= ngayKT.Month && dt.NamDT == ngayKT.Year
                            && ct.GioDon > 0
                            select new
                            {
                                DvcbID = (string)cb.DvcbID,
                                LoaiMayID = (string)cb.LoaiMayID,
                                GaXPName = (string)ct.GaName,
                                CongTacID = (short)ct.CongTacID,
                                GioDon = (int)ct.GioDon
                            };
                if (MaDV != "TCT")
                    query1 = query1.Where(x => x.DvcbID == MaDV);
               await query1.ToListAsync();
                //Lấy dồn quy đổi từ tầu công trình
                var query2 = from dt in _context.DoanThong
                             join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                             join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                             where dt.ThangDT >= ngayBD.Month && dt.ThangDT <= ngayKT.Month && dt.NamDT == ngayKT.Year
                             && ct.CongTacID == 8 && ct.MacTauID!= "CDON"
                             select new
                             {
                                 DvcbID = (string)cb.DvcbID,
                                 LoaiMayID = (string)cb.LoaiMayID,
                                 GaXPName = (string)ct.GaXPName,
                                 CongTacID = (short)998,
                                 GioDon = (int)ct.DonXP
                             };
                if (MaDV != "TCT")
                    query2 = query2.Where(x => x.DvcbID == MaDV);
                await query2.ToListAsync();

                var query = await query1.Concat(query2).ToListAsync();

                return (from x in query
                              group x by new { x.DvcbID, x.LoaiMayID, x.GaXPName, x.CongTacID } into g
                              select new ViewBcGioDon
                              {
                                  DvcbID = g.Key.DvcbID,
                                  LoaiMayID = g.Key.LoaiMayID,
                                  GaXPName = g.Key.GaXPName,
                                  CongTacID = g.Key.CongTacID,
                                  GioDon = g.Sum(x => x.GioDon)
                              }).ToList();
            }
            else
            {
                DateTime _ngayKT = ngayKT.AddDays(1);
                //Lấy kiêm dồn và chuyên dồn
                var query1 = from dt in _context.DoanThong
                            join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                            join ct in _context.CoBaoCT on cb.CoBaoID equals ct.CoBaoID
                            where cb.NgayCB >= ngayBD && cb.NgayCB < _ngayKT
                            && ct.GioDon > 0
                            select new
                            {
                                DvcbID = (string)cb.DvcbID,
                                LoaiMayID = (string)cb.LoaiMayID,
                                GaXPName = (string)ct.GaName,
                                CongTacID = (short)ct.CongTacID,
                                GioDon = (int)ct.GioDon
                            };
                if (MaDV != "TCT")
                    query1 = query1.Where(x => x.DvcbID == MaDV);
                await query1.ToListAsync();
                //Lấy dồn quy đổi từ tầu công trình
                var query2 = from dt in _context.DoanThong
                             join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                             join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                             where cb.NgayCB >= ngayBD && cb.NgayCB < _ngayKT
                             && ct.CongTacID == 8 && ct.MacTauID != "CDON"
                             select new
                             {
                                 DvcbID = (string)cb.DvcbID,
                                 LoaiMayID = (string)cb.LoaiMayID,
                                 GaXPName = (string)ct.GaXPName,
                                 CongTacID = (short)998,
                                 GioDon = (int)ct.DonXP
                             };
                if (MaDV != "TCT")
                    query2 = query2.Where(x => x.DvcbID == MaDV);
                await query2.ToListAsync();

                var query = await query1.Concat(query2).ToListAsync();

                return (from x in query
                              group x by new { x.DvcbID, x.LoaiMayID, x.GaXPName, x.CongTacID } into g
                              select new ViewBcGioDon
                              {
                                  DvcbID = g.Key.DvcbID,
                                  LoaiMayID = g.Key.LoaiMayID,
                                  GaXPName = g.Key.GaXPName,
                                  CongTacID = g.Key.CongTacID,
                                  GioDon = g.Sum(x => x.GioDon)
                              }).ToList();
            }
        }

        [HttpGet]
        [Route("GetBCGioDonCT")]
        public async Task<ActionResult<IEnumerable<ViewBcGioDonCT>>> GetBCGioDonCT(string MaDV, int loaiBC, DateTime ngayBD, DateTime ngayKT)
        {
            if (loaiBC < 5)
            {
                //Lấy kiêm dồn và chuyên dồn
                var query1 = from dt in _context.DoanThong
                            join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                            join ct in _context.CoBaoCT on cb.CoBaoID equals ct.CoBaoID
                            where dt.ThangDT >= ngayBD.Month && dt.ThangDT <= ngayKT.Month && dt.NamDT == ngayKT.Year
                            && ct.GioDon > 0
                            select new
                            {
                                DvcbID = (string)cb.DvcbID,                                
                                GaName = (string)ct.GaName,
                                DauMayID = (string)cb.DauMayID,
                                NhanMay=(DateTime)cb.NhanMay,
                                SoCB=(string)cb.SoCB,
                                TaiXe1ID = (string)cb.TaiXe1ID,
                                TaiXe1Name = (string)cb.TaiXe1Name,
                                GioDon = (int)ct.GioDon
                            };
                if (MaDV != "TCT")
                    query1 = query1.Where(x => x.DvcbID == MaDV);
                await query1.ToListAsync();
                //Lấy dồn quy đổi từ tầu công trình
                var query2 = from dt in _context.DoanThong
                             join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                             join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                             where dt.ThangDT >= ngayBD.Month && dt.ThangDT <= ngayKT.Month && dt.NamDT == ngayKT.Year
                              && ct.CongTacID == 8 && ct.MacTauID != "CDON"
                             select new
                             {
                                 DvcbID = (string)cb.DvcbID,
                                 GaName = (string)ct.GaXPName,
                                 DauMayID = (string)cb.DauMayID,
                                 NhanMay = (DateTime)cb.NhanMay,
                                 SoCB = (string)cb.SoCB,
                                 TaiXe1ID = (string)cb.TaiXe1ID,
                                 TaiXe1Name = (string)cb.TaiXe1Name,
                                 GioDon = (int)ct.DonXP
                             };
                if (MaDV != "TCT")
                    query2 = query2.Where(x => x.DvcbID == MaDV);
                await query2.ToListAsync();

                var query = await query1.Concat(query2).ToListAsync();

                return (from x in query
                              group x by new { x.DvcbID, x.GaName,x.DauMayID,x.NhanMay,x.SoCB,x.TaiXe1ID,x.TaiXe1Name } into g
                              select new ViewBcGioDonCT
                              {
                                  DvcbID = g.Key.DvcbID,                                  
                                  GaName = g.Key.GaName,
                                  DauMayID = g.Key.DauMayID,
                                  NhanMay=g.Key.NhanMay,
                                  SoCB=g.Key.SoCB,
                                  TaiXeID=g.Key.TaiXe1ID,
                                  TaiXeName=g.Key.TaiXe1Name,
                                  GioDon = g.Sum(x => x.GioDon)
                              }).ToList();
            }
            else
            {
                DateTime _ngayKT = ngayKT.AddDays(1);
                //Lấy kiêm dồn và chuyên dồn
                var query1 = from dt in _context.DoanThong
                            join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                            join ct in _context.CoBaoCT on cb.CoBaoID equals ct.CoBaoID
                            where cb.NgayCB >= ngayBD && cb.NgayCB < _ngayKT
                            && ct.GioDon > 0
                            select new
                            {
                                DvcbID = (string)cb.DvcbID,
                                GaName = (string)ct.GaName,
                                DauMayID = (string)cb.DauMayID,
                                NhanMay = (DateTime)cb.NhanMay,
                                SoCB = (string)cb.SoCB,
                                TaiXe1ID = (string)cb.TaiXe1ID,
                                TaiXe1Name = (string)cb.TaiXe1Name,
                                GioDon = (int)ct.GioDon
                            };
                if (MaDV != "TCT")
                    query1 = query1.Where(x => x.DvcbID == MaDV);
                await query1.ToListAsync();
                //Lấy dồn quy đổi từ tầu công trình
                var query2 = from dt in _context.DoanThong
                             join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                             join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                             where cb.NgayCB >= ngayBD && cb.NgayCB < _ngayKT
                              && ct.CongTacID == 8 && ct.MacTauID != "CDON"
                             select new
                             {
                                 DvcbID = (string)cb.DvcbID,
                                 GaName = (string)ct.GaXPName,
                                 DauMayID = (string)cb.DauMayID,
                                 NhanMay = (DateTime)cb.NhanMay,
                                 SoCB = (string)cb.SoCB,
                                 TaiXe1ID = (string)cb.TaiXe1ID,
                                 TaiXe1Name = (string)cb.TaiXe1Name,
                                 GioDon = (int)ct.DonXP
                             };
                if (MaDV != "TCT")
                    query2 = query2.Where(x => x.DvcbID == MaDV);
                await query2.ToListAsync();

                var query = await query1.Concat(query2).ToListAsync();

                return (from x in query
                              group x by new { x.DvcbID, x.GaName, x.DauMayID, x.NhanMay, x.SoCB, x.TaiXe1ID, x.TaiXe1Name } into g
                              select new ViewBcGioDonCT
                              {
                                  DvcbID = g.Key.DvcbID,
                                  GaName = g.Key.GaName,
                                  DauMayID = g.Key.DauMayID,
                                  NhanMay = g.Key.NhanMay,
                                  SoCB = g.Key.SoCB,
                                  TaiXeID = g.Key.TaiXe1ID,
                                  TaiXeName = g.Key.TaiXe1Name,
                                  GioDon = g.Sum(x => x.GioDon)
                              }).ToList();
            }
        }

        [HttpGet]
        [Route("GetBCNhienLieu")]
        public async Task<ActionResult<IEnumerable<ViewBcNhienLieu>>> GetBCNhienLieu(string maDV, int loaiBC, DateTime ngayBD, DateTime ngayKT, string tuyenID)
        {
            DateTime _ngayKT = ngayKT.AddDays(1);
            var query = from dt in _context.DoanThong
                        join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                        join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID                        
                        select new
                        {
                            DvcbID = (string)cb.DvcbID,
                            LoaiMayID = (string)cb.LoaiMayID,
                            DauMayID = (string)cb.DauMayID,
                            NgayCB = (DateTime)cb.NgayCB,
                            ThangDT = (int)dt.ThangDT,
                            NamDT = (int)dt.NamDT,
                            TuyenID = (string)ct.TuyenID,
                            CongTacID = (short)ct.CongTacID,
                            TinhChatID=(short)ct.TinhChatID,
                            GaXPID = (int)ct.GaXPID,
                            GaKTID = (int)ct.GaKTID,
                            GioDon = (int)(ct.DonXP + ct.DonDD + ct.DonKT),
                            TanKM = (decimal)(ct.TKMChinh + ct.TKMDon + ct.TKMGhep + ct.TKMDay),
                            DinhMuc = (decimal)ct.DinhMuc,
                            TieuThu = (decimal)ct.TieuThu
                        };
            if (loaiBC < 5)
                query = query.Where(x => x.ThangDT >= ngayBD.Month && x.ThangDT <= ngayKT.Month && x.NamDT == ngayKT.Year);
            else
                query = query.Where(x => x.NgayCB >= ngayBD && x.NgayCB < _ngayKT);
            if (maDV != "TCT")
                query = query.Where(x => x.DvcbID == maDV);
            if (tuyenID != "ALL")
                query = query.Where(x => tuyenID.Contains(x.TuyenID));
            return await (from x in query
                          group x by new { x.DvcbID, x.LoaiMayID, x.DauMayID, x.NgayCB, x.ThangDT, x.NamDT, x.TuyenID, x.CongTacID,x.TinhChatID, x.GaXPID, x.GaKTID } into g
                          select new ViewBcNhienLieu
                          {
                              DvcbID = g.Key.DvcbID,
                              LoaiMayID = g.Key.LoaiMayID,
                              DauMayID = g.Key.DauMayID,
                              NgayCB = g.Key.NgayCB,
                              ThangDT = g.Key.ThangDT,
                              NamDT = g.Key.NamDT,
                              TuyenID = g.Key.TuyenID,
                              CongTacID = g.Key.CongTacID,
                              TinhChatID=g.Key.TinhChatID,
                              GaXPID = g.Key.GaXPID,
                              GaKTID = g.Key.GaKTID,
                              GioDon = g.Sum(x => x.GioDon),
                              TanKM = g.Sum(x => x.TanKM),
                              DinhMuc = g.Sum(x => x.DinhMuc),
                              TieuThu = g.Sum(x => x.TieuThu)
                          }).ToListAsync();
        }

        [HttpGet]
        [Route("GetBCNhienLieuKD")]
        public async Task<ActionResult<IEnumerable<ViewBcNhienLieuKD>>> GetBCNhienLieuKD(string MaDV, int loaiBC, DateTime ngayBD, DateTime ngayKT, string tuyenID)
        {
            if (loaiBC < 5)
            {
                var query = from dt in _context.DoanThong
                            join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                            join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                            where dt.ThangDT >= ngayBD.Month && dt.ThangDT <= ngayKT.Month && dt.NamDT == ngayKT.Year
                            select new
                            {
                                DvcbID = (string)cb.DvcbID,
                                LoaiMayID = (string)cb.LoaiMayID,
                                CongTacID = (short)ct.CongTacID,
                                KhuDoan = (string)ct.KhuDoan,
                                TuyenID = (string)ct.TuyenID,
                                ThangDT = (int)dt.ThangDT,
                                NamDT = (int)dt.NamDT,
                                GioDon = (int)(ct.DonXP + ct.DonDD + ct.DonKT),
                                KM = (decimal)(ct.KMChinh + ct.KMDon + ct.KMGhep + ct.KMDay),
                                TanKM = (decimal)(ct.TKMChinh + ct.TKMDon + ct.TKMGhep + ct.TKMDay),
                                DinhMuc = (decimal)ct.DinhMuc,
                                TieuThu = (decimal)ct.TieuThu
                            };
                if (MaDV != "TCT")
                    query = query.Where(x => x.DvcbID == MaDV);
                if (tuyenID != "ALL")
                    query = query.Where(x => tuyenID.Contains(x.TuyenID));
                return await (from x in query
                              group x by new { x.DvcbID, x.LoaiMayID, x.CongTacID, x.KhuDoan, x.ThangDT, x.NamDT } into g
                              select new ViewBcNhienLieuKD
                              {
                                  DvcbID = g.Key.DvcbID,
                                  LoaiMayID = g.Key.LoaiMayID,
                                  CongTacID = g.Key.CongTacID,
                                  KhuDoan = g.Key.KhuDoan,
                                  ThangDT = g.Key.ThangDT,
                                  NamDT = g.Key.NamDT,
                                  GioDon = g.Sum(x => x.GioDon),
                                  KM = g.Sum(x => x.KM),
                                  TanKM = g.Sum(x => x.TanKM),
                                  DinhMuc = g.Sum(x => x.DinhMuc),
                                  TieuThu = g.Sum(x => x.TieuThu)
                              }).ToListAsync();
            }
            else
            {
                DateTime _ngayKT = ngayKT.AddDays(1);
                var query = from dt in _context.DoanThong
                            join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                            join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                            where cb.NgayCB >= ngayBD && cb.NgayCB < _ngayKT
                            select new
                            {
                                DvcbID = (string)cb.DvcbID,
                                LoaiMayID = (string)cb.LoaiMayID,
                                CongTacID = (short)ct.CongTacID,
                                KhuDoan = (string)ct.KhuDoan,
                                TuyenID = (string)ct.TuyenID,
                                ThangDT = (int)dt.ThangDT,
                                NamDT = (int)dt.NamDT,
                                GioDon = (int)(ct.DonXP + ct.DonDD + ct.DonKT),
                                KM = (decimal)(ct.KMChinh + ct.KMDon + ct.KMGhep + ct.KMDay),
                                TanKM = (decimal)(ct.TKMChinh + ct.TKMDon + ct.TKMGhep + ct.TKMDay),
                                DinhMuc = (decimal)ct.DinhMuc,
                                TieuThu = (decimal)ct.TieuThu
                            };
                if (MaDV != "TCT")
                    query = query.Where(x => x.DvcbID == MaDV);
                if (tuyenID != "ALL")
                    query = query.Where(x => tuyenID.Contains(x.TuyenID));
                return await (from x in query
                              group x by new { x.DvcbID, x.LoaiMayID, x.CongTacID, x.KhuDoan, x.ThangDT, x.NamDT } into g
                              select new ViewBcNhienLieuKD
                              {
                                  DvcbID = g.Key.DvcbID,
                                  LoaiMayID = g.Key.LoaiMayID,
                                  CongTacID = g.Key.CongTacID,
                                  KhuDoan = g.Key.KhuDoan,
                                  ThangDT = g.Key.ThangDT,
                                  NamDT = g.Key.NamDT,
                                  GioDon = g.Sum(x => x.GioDon),
                                  KM = g.Sum(x => x.KM),
                                  TanKM = g.Sum(x => x.TanKM),
                                  DinhMuc = g.Sum(x => x.DinhMuc),
                                  TieuThu = g.Sum(x => x.TieuThu)
                              }).ToListAsync();
            }
        }

        [HttpGet]
        [Route("GetBCTTNhienLieu")]
        public async Task<ActionResult<IEnumerable<ViewBcTTNhienLieu>>> GetBCTTNhienLieu(string maDV, int loaiBC, DateTime ngayBD, DateTime ngayKT, string tuyenID)
        {
            DateTime _ngayKT = ngayKT.AddDays(1);
            var query = from dt in _context.DoanThong
                        join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                        join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                        select new
                        {
                            DvcbID = (string)cb.DvcbID,
                            LoaiMayID = (string)cb.LoaiMayID,
                            DauMayID = (string)cb.DauMayID,
                            NgayCB = (DateTime)cb.NgayCB,
                            ThangDT = (int)dt.ThangDT,
                            NamDT = (int)dt.NamDT,
                            TuyenID = (string)ct.TuyenID,
                            CongTacID = (short)ct.CongTacID,
                            CongTacName = (string)ct.CongTacName,
                            TinhChatID = (short)ct.TinhChatID,
                            GaXP = (string)ct.GaXPName,
                            GaKT = (string)ct.GaKTName,
                            GioDon = (int)(ct.DonXP + ct.DonDD + ct.DonKT),
                            GioDung = (int)(ct.DungDM + ct.DungDN + ct.DungXP + ct.DungDD + ct.DungQD + ct.DungKT + ct.DungKhoDM + ct.DungKhoDN),
                            KMChinh = (decimal)ct.KMChinh,
                            KMDon = (decimal)ct.KMDon,
                            KMGhep = (decimal)ct.KMGhep,
                            KMDay = (decimal)ct.KMDay,                           
                            TanKM = (decimal)(ct.TKMChinh + ct.TKMDon + ct.TKMGhep + ct.TKMDay),
                            DinhMuc = (decimal)ct.DinhMuc,
                            TieuThu = (decimal)ct.TieuThu
                        };
            if (loaiBC < 5)
                query = query.Where(x => x.ThangDT >= ngayBD.Month && x.ThangDT <= ngayKT.Month && x.NamDT == ngayKT.Year);
            else
                query = query.Where(x => x.NgayCB >= ngayBD && x.NgayCB < _ngayKT);
            if (maDV != "TCT")
                query = query.Where(x => x.DvcbID == maDV);
            if (tuyenID != "ALL")
                query = query.Where(x => tuyenID.Contains(x.TuyenID));
            return await (from x in query
                          group x by new { x.DvcbID, x.LoaiMayID, x.DauMayID, x.NgayCB, x.ThangDT, x.NamDT, x.TuyenID, x.CongTacID, x.CongTacName, x.TinhChatID, x.GaXP, x.GaKT } into g
                          select new ViewBcTTNhienLieu
                          {
                              DvcbID = g.Key.DvcbID,
                              LoaiMayID = g.Key.LoaiMayID,
                              DauMayID = g.Key.DauMayID,
                              NgayCB = g.Key.NgayCB,
                              ThangDT = g.Key.ThangDT,
                              NamDT = g.Key.NamDT,
                              TuyenID = g.Key.TuyenID,
                              CongTacID = g.Key.CongTacID,
                              CongTacName = g.Key.CongTacName,
                              TinhChatID = g.Key.TinhChatID,
                              GaXP = g.Key.GaXP,
                              GaKT = g.Key.GaKT,
                              GioDon = g.Sum(x => x.GioDon),
                              GioDung = g.Sum(x => x.GioDung),
                              KMChinh = g.Sum(x => x.KMChinh),
                              KMDon = g.Sum(x => x.KMDon),
                              KMGhep = g.Sum(x => x.KMGhep),
                              KMDay = g.Sum(x => x.KMDay),                             
                              TanKM = g.Sum(x => x.TanKM),
                              DinhMuc = g.Sum(x => x.DinhMuc),
                              TieuThu = g.Sum(x => x.TieuThu)
                          }).ToListAsync();
        }

        [HttpGet]
        [Route("GetBCTTTaiXe")]
        public async Task<ActionResult<IEnumerable<ViewBcTTTaiXe>>> GetBCTTTaiXe(string maDV, int loaiBC, DateTime ngayBD, DateTime ngayKT)
        {
            DateTime _ngayKT = ngayKT.AddDays(1);
            var query = from dt in _context.DoanThong
                        join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                        join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                        select new
                        {
                            DvcbID = (string)cb.DvcbID,
                            CoBaoID = (long)cb.CoBaoID,
                            NgayCB = (DateTime)cb.NgayCB,
                            ThangDT = (int)dt.ThangDT,
                            NamDT = (int)dt.NamDT,
                            TaiXe1ID = (string)cb.TaiXe1ID,
                            TaiXe1Name = (string)cb.TaiXe1Name,
                            PhoXe1ID = (string)cb.PhoXe1ID,
                            PhoXe1Name = (string)cb.PhoXe1Name,
                            TaiXe2ID = (string)cb.TaiXe2ID,
                            TaiXe2Name = (string)cb.TaiXe2Name,
                            PhoXe2ID = (string)cb.PhoXe2ID,
                            PhoXe2Name = (string)cb.PhoXe2Name,
                            TaiXe3ID = (string)cb.TaiXe3ID,
                            TaiXe3Name = (string)cb.TaiXe3Name,
                            PhoXe3ID = (string)cb.PhoXe3ID,
                            PhoXe3Name = (string)cb.PhoXe3Name,
                            GioDung = (int)(ct.DungDM + ct.DungDN + ct.DungXP + ct.DungDD + ct.DungQD + ct.DungKT + ct.DungKhoDM + ct.DungKhoDN),
                            GioDon = (int)(ct.DonXP + ct.DonDD + ct.DonKT),
                            KM = (decimal)(ct.KMChinh + ct.KMDon + ct.KMGhep + ct.KMDay),
                            DinhMuc = (decimal)ct.DinhMuc,
                            TieuThu = (decimal)ct.TieuThu
                        };
            //query = query.Where(x => x.TieuThu > 0);
            if (loaiBC < 5)
                query = query.Where(x => x.ThangDT >= ngayBD.Month && x.ThangDT <= ngayKT.Month && x.NamDT == ngayKT.Year);
            else
                query = query.Where(x => x.NgayCB >= ngayBD && x.NgayCB < _ngayKT);
            if (maDV != "TCT")
                query = query.Where(x => x.DvcbID == maDV);
            return await (from x in query
                          group x by new
                          {
                              x.DvcbID,
                              x.CoBaoID,
                              x.ThangDT,
                              x.NamDT,
                              x.TaiXe1ID,
                              x.TaiXe1Name,
                              x.PhoXe1ID,
                              x.PhoXe1Name,
                              x.TaiXe2ID,
                              x.TaiXe2Name,
                              x.PhoXe2ID,
                              x.PhoXe2Name,
                              x.TaiXe3ID,
                              x.TaiXe3Name,
                              x.PhoXe3ID,
                              x.PhoXe3Name
                          } into g
                          select new ViewBcTTTaiXe
                          {
                              DvcbID = g.Key.DvcbID,
                              CoBaoID = g.Key.CoBaoID,
                              ThangDT = g.Key.ThangDT,
                              NamDT = g.Key.NamDT,
                              TaiXe1ID = g.Key.TaiXe1ID,
                              TaiXe1Name = g.Key.TaiXe1Name,
                              PhoXe1ID = g.Key.PhoXe1ID,
                              PhoXe1Name = g.Key.PhoXe1Name,
                              TaiXe2ID = g.Key.TaiXe2ID,
                              TaiXe2Name = g.Key.TaiXe2Name,
                              PhoXe2ID = g.Key.PhoXe2ID,
                              PhoXe2Name = g.Key.PhoXe2Name,
                              TaiXe3ID = g.Key.TaiXe3ID,
                              TaiXe3Name = g.Key.TaiXe3Name,
                              PhoXe3ID = g.Key.PhoXe3ID,
                              PhoXe3Name = g.Key.PhoXe3Name,
                              GioDung = g.Sum(x => x.GioDung),
                              GioDon = g.Sum(x => x.GioDon),
                              KM = g.Sum(x => x.KM),
                              DinhMuc = g.Sum(x => x.DinhMuc),
                              TieuThu = g.Sum(x => x.TieuThu)
                          }).ToListAsync();
        }

        [HttpGet]
        [Route("GetBCTacNghiep")]
        public async Task<ActionResult<IEnumerable<ViewBcTacNghiep>>> GetBCTacNghiep(string maDV, int loaiBC, DateTime ngayBD, DateTime ngayKT)
        {
            DateTime _ngayKT = ngayKT.AddDays(1);
            var query = from dt in _context.DoanThong
                        join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                        join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                        select new
                        {
                            DvcbID = (string)cb.DvcbID,
                            DvdmID = (string)cb.DvdmID,
                            LoaiMayID = (string)cb.LoaiMayID,
                            DauMayID = (string)cb.DauMayID,
                            NgayCB = (DateTime)cb.NgayCB,
                            ThangDT = (int)dt.ThangDT,
                            NamDT = (int)dt.NamDT,
                            CongTacID = (short)ct.CongTacID,
                            TinhChatID = (short)ct.TinhChatID,
                            GaXP = (string)ct.GaXPName,
                            GaKT = (string)ct.GaKTName,
                            KMChinh = (decimal)ct.KMChinh,
                            KMPhuTro = (decimal)(ct.KMDon + ct.KMGhep + ct.KMDay),
                            GioLH=(int)ct.LuHanh,
                            GioDon = (int)(ct.DonXP + ct.DonDD + ct.DonKT),
                            TanKM = (decimal)(ct.TKMChinh + ct.TKMDon + ct.TKMGhep + ct.TKMDay),
                            TieuThu = (decimal)ct.TieuThu
                        };
            if (loaiBC < 5)
                query = query.Where(x => x.ThangDT >= ngayBD.Month && x.ThangDT <= ngayKT.Month && x.NamDT == ngayKT.Year);
            else
                query = query.Where(x => x.NgayCB >= ngayBD && x.NgayCB < _ngayKT);
            if (maDV != "TCT")
                query = query.Where(x => x.DvcbID == maDV);

            return await (from x in query
                          group x by new { x.DvcbID, x.DvdmID, x.LoaiMayID, x.DauMayID, x.NgayCB, x.ThangDT, x.NamDT, x.CongTacID, x.TinhChatID, x.GaXP, x.GaKT } into g
                          select new ViewBcTacNghiep
                          {
                              DvcbID = g.Key.DvcbID,
                              DvdmID = g.Key.DvdmID,
                              LoaiMayID = g.Key.LoaiMayID,
                              DauMayID = g.Key.DauMayID,
                              NgayCB = g.Key.NgayCB,
                              ThangDT = g.Key.ThangDT,
                              NamDT = g.Key.NamDT,
                              CongTacID = g.Key.CongTacID,
                              TinhChatID = g.Key.TinhChatID,
                              GaXP = g.Key.GaXP,
                              GaKT = g.Key.GaKT,
                              KMChinh = g.Sum(x => x.KMChinh),
                              KMPhuTro = g.Sum(x => x.KMPhuTro),
                              GioLH=g.Sum(x=>x.GioLH),
                              GioDon = g.Sum(x => x.GioDon),
                              TanKM = g.Sum(x => x.TanKM),
                              TieuThu = g.Sum(x => x.TieuThu)
                          }).ToListAsync();
        }

        [HttpGet]
        [Route("GetBCTonNL")]
        public async Task<ActionResult<IEnumerable<ViewBcTonNL>>> GetBCTonNL(string MaDV, int tuThang, int denThang, int nam)
        {           
            var query = from dt in _context.DoanThong
                        join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                        where dt.ThangDT >= tuThang && dt.ThangDT <= denThang && dt.NamDT == nam
                        select new
                        {
                            DvcbID = (string)cb.DvcbID,
                            LoaiMayID = (string)cb.LoaiMayID,
                            DauMayID = (string)cb.DauMayID,
                            NhanMay = (DateTime)cb.NhanMay,
                            MaTram = (string)cb.TramNLID,
                            NLThucNhan = (decimal)cb.NLThucNhan,
                            Linh = (decimal)cb.NLLinh,
                            TieuThu = (decimal)(cb.NLThucNhan + cb.NLLinh - cb.NLBanSau)
                        };
            if (MaDV != "TCT")
                query = query.Where(x => x.DvcbID == MaDV);
            return await (from x in query
                          group x by new { x.LoaiMayID, x.DauMayID, x.NhanMay, x.MaTram, x.NLThucNhan } into g
                          select new ViewBcTonNL
                          {
                              LoaiMayID = g.Key.LoaiMayID,
                              DauMayID = g.Key.DauMayID,
                              NhanMay = g.Key.NhanMay,
                              MaTram = g.Key.MaTram,
                              TonDau = g.Key.NLThucNhan,
                              Linh = g.Sum(x => x.Linh),
                              TieuThu = g.Sum(x => x.TieuThu)
                          }).ToListAsync();
        }

        [HttpGet]
        [Route("GetBCBKDauMo")]
        public async Task<ActionResult<IEnumerable<ViewBcBKDauMo>>> GetBCBKDauMo(string MaDV, short LoaiDM, int tuThang, int denThang, int nam)
        {
            if (LoaiDM == 0)
            {
                var query = from dt in _context.DoanThong
                            join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                            where dt.ThangDT >= tuThang && dt.ThangDT <= denThang && dt.NamDT == nam && cb.NLLinh > 0
                            select new
                            {
                                DvcbID = (string)cb.DvcbID,
                                LoaiMayID = (string)cb.LoaiMayID,
                                DauMayID = (string)cb.DauMayID,
                                NgayCB = (DateTime)cb.NgayCB,
                                SoCB = (string)cb.SoCB,
                                MaTram = (string)cb.TramNLID,
                                Linh = (decimal)cb.NLLinh
                            };
                if (MaDV != "TCT")
                    query = query.Where(x => x.DvcbID == MaDV);
                return await (from x in query
                              select new ViewBcBKDauMo
                              {
                                  LoaiMayID = x.LoaiMayID,
                                  DauMayID = x.DauMayID,
                                  NgayCB = x.NgayCB,
                                  SoCB = x.SoCB,
                                  MaTram = x.MaTram,
                                  Linh = x.Linh
                              }).ToListAsync();
            }
            else
            {
                var query = from dt in _context.DoanThong
                            join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                            join dm in _context.CoBaoDM on dt.DoanThongID equals dm.CoBaoID
                            where dt.ThangDT >= tuThang && dt.ThangDT <= denThang && dt.NamDT == nam && dm.LoaiDauMoID == LoaiDM && dm.Linh > 0
                            select new
                            {
                                DvcbID = (string)cb.DvcbID,
                                LoaiMayID = (string)cb.LoaiMayID,
                                DauMayID = (string)cb.DauMayID,
                                NgayCB = (DateTime)cb.NgayCB,
                                SoCB = (string)cb.SoCB,
                                MaTram = (string)dm.MaTram,
                                Linh = (decimal)dm.Linh
                            };
                if (MaDV != "TCT")
                    query = query.Where(x => x.DvcbID == MaDV);
                return await (from x in query
                              select new ViewBcBKDauMo
                              {
                                  LoaiMayID = x.LoaiMayID,
                                  DauMayID = x.DauMayID,
                                  NgayCB = x.NgayCB,
                                  SoCB = x.SoCB,
                                  MaTram = x.MaTram,
                                  Linh = x.Linh
                              }).ToListAsync();
            }
        }
        [HttpGet]
        [Route("GetBCBKLuong")]
        public async Task<ActionResult<IEnumerable<ViewBcBKLuong>>> GetBCBKLuong(string MaDV, int loaiBC, DateTime ngayBD, DateTime ngayKT)
        {
            if (loaiBC < 5)
            {
                var query = from dt in _context.DoanThong
                            join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                            join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                            where dt.ThangDT >= ngayBD.Month && dt.ThangDT <= ngayKT.Month && dt.NamDT == ngayKT.Year && !string.IsNullOrWhiteSpace(ct.MacTauID)
                            select new
                            {
                                DvcbID = (string)cb.DvcbID,
                                DauMayID = (string)cb.DauMayID,
                                NgayCB = (DateTime)cb.NgayCB,
                                SoCB = (string)cb.SoCB,
                                MaCB = (string)cb.MaCB,
                                TaiXe1ID = (string)cb.TaiXe1ID,
                                TaiXe1Name = (string)cb.TaiXe1Name,
                                PhoXe1ID = (string)cb.PhoXe1ID,
                                PhoXe1Name = (string)cb.PhoXe1Name,
                                TaiXe2ID = (string)cb.TaiXe2ID,
                                TaiXe2Name = (string)cb.TaiXe2Name,
                                PhoXe2ID = (string)cb.PhoXe2ID,
                                PhoXe2Name = (string)cb.PhoXe2Name,
                                TaiXe3ID = (string)cb.TaiXe3ID,
                                TaiXe3Name = (string)cb.TaiXe3Name,
                                PhoXe3ID = (string)cb.PhoXe3ID,
                                PhoXe3Name = (string)cb.PhoXe3Name,
                                MacTauID = (string)ct.MacTauID,
                                TinhChatID = (short)ct.TinhChatID,
                                GioDm = (int)ct.QuayVong,
                                GioDung = (int)(ct.DungXP + ct.DungDD + ct.DungQD + ct.DungKT),
                                GioDon = (int)(ct.DonXP + ct.DonDD + ct.DonKT),
                                KM = (decimal)(ct.KMChinh + ct.KMDon + ct.KMGhep + ct.KMDay),
                                NLLoiLo = (decimal)(ct.DinhMuc - ct.TieuThu)
                            };
                if (MaDV != "TCT")
                    query = query.Where(x => x.DvcbID == MaDV);
                return await (from x in query                             
                              select new ViewBcBKLuong
                              {
                                  DauMayID = x.DauMayID,
                                  NgayCB = x.NgayCB,
                                  SoCB = x.SoCB,
                                  MaCB = x.MaCB,
                                  TaiXe1ID = x.TaiXe1ID,
                                  TaiXe1Name = x.TaiXe1Name,
                                  PhoXe1ID = x.PhoXe1ID,
                                  PhoXe1Name = x.PhoXe1Name,
                                  TaiXe2ID = x.TaiXe2ID,
                                  TaiXe2Name = x.TaiXe2Name,
                                  PhoXe2ID = x.PhoXe2ID,
                                  PhoXe2Name = x.PhoXe2Name,
                                  TaiXe3ID = x.TaiXe3ID,
                                  TaiXe3Name = x.TaiXe3Name,
                                  PhoXe3ID = x.PhoXe3ID,
                                  PhoXe3Name = x.PhoXe3Name,
                                  MacTauID = x.MacTauID,
                                  TinhChatID = x.TinhChatID,
                                  GioDm = x.GioDm,
                                  Km = x.KM+x.GioDung / 60 + x.GioDon / 6,
                                  NLLoiLo = x.NLLoiLo
                              }).ToListAsync();
            }
            else
            {
                DateTime _ngayKT = ngayKT.AddDays(1);
                var query = from dt in _context.DoanThong
                            join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                            join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                            where cb.NgayCB >= ngayBD && cb.NgayCB < _ngayKT && !string.IsNullOrWhiteSpace(ct.MacTauID)
                            select new
                            {
                                DvcbID = (string)cb.DvcbID,
                                DauMayID = (string)cb.DauMayID,
                                NgayCB = (DateTime)cb.NgayCB,
                                SoCB = (string)cb.SoCB,
                                MaCB = (string)cb.MaCB,
                                TaiXe1ID = (string)cb.TaiXe1ID,
                                TaiXe1Name = (string)cb.TaiXe1Name,
                                PhoXe1ID = (string)cb.PhoXe1ID,
                                PhoXe1Name = (string)cb.PhoXe1Name,
                                TaiXe2ID = (string)cb.TaiXe2ID,
                                TaiXe2Name = (string)cb.TaiXe2Name,
                                PhoXe2ID = (string)cb.PhoXe2ID,
                                PhoXe2Name = (string)cb.PhoXe2Name,
                                TaiXe3ID = (string)cb.TaiXe3ID,
                                TaiXe3Name = (string)cb.TaiXe3Name,
                                PhoXe3ID = (string)cb.PhoXe3ID,
                                PhoXe3Name = (string)cb.PhoXe3Name,
                                MacTauID = (string)ct.MacTauID,
                                TinhChatID = (short)ct.TinhChatID,
                                GioDm = (int)ct.QuayVong,
                                GioDung = (int)(ct.DungXP + ct.DungDD + ct.DungQD + ct.DungKT),
                                GioDon = (int)(ct.DonXP + ct.DonDD + ct.DonKT),
                                KM = (decimal)(ct.KMChinh + ct.KMDon + ct.KMGhep + ct.KMDay),
                                NLLoiLo = (decimal)(ct.DinhMuc - ct.TieuThu)
                            };
                if (MaDV != "TCT")
                    query = query.Where(x => x.DvcbID == MaDV);
                return await (from x in query
                              select new ViewBcBKLuong
                              {
                                  DauMayID = x.DauMayID,
                                  NgayCB = x.NgayCB,
                                  SoCB = x.SoCB,
                                  MaCB = x.MaCB,
                                  TaiXe1ID = x.TaiXe1ID,
                                  TaiXe1Name = x.TaiXe1Name,
                                  PhoXe1ID = x.PhoXe1ID,
                                  PhoXe1Name = x.PhoXe1Name,
                                  TaiXe2ID = x.TaiXe2ID,
                                  TaiXe2Name = x.TaiXe2Name,
                                  PhoXe2ID = x.PhoXe2ID,
                                  PhoXe2Name = x.PhoXe2Name,
                                  TaiXe3ID = x.TaiXe3ID,
                                  TaiXe3Name = x.TaiXe3Name,
                                  PhoXe3ID = x.PhoXe3ID,
                                  PhoXe3Name = x.PhoXe3Name,
                                  MacTauID = x.MacTauID,
                                  TinhChatID = x.TinhChatID,
                                  GioDm = x.GioDm,
                                  Km = x.KM + x.GioDung / 60 + x.GioDon / 6,
                                  NLLoiLo = x.NLLoiLo
                              }).ToListAsync();
            }
        }
        [HttpGet]
        [Route("GetBCKeHoach")]
        public async Task<ActionResult<IEnumerable<ViewBcKeHoach>>> GetBCKeHoach(string MaDV, int tuThang, int denThang, int nam)
        {
            var query = from dt in _context.DoanThong
                        join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                        join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                        where dt.ThangDT >= tuThang && dt.ThangDT <= denThang && dt.NamDT == nam
                        select new
                        {
                            DvcbID = (string)cb.DvcbID,
                            CongTacID = (short)ct.CongTacID,
                            KMChinh = (decimal)ct.KMChinh,
                            KMPhuTro = (decimal)(ct.KMDon + ct.KMGhep + ct.KMDay),
                            TKMChinh = (decimal)ct.TKMChinh,
                            TKMPhuTro = (decimal)(ct.TKMDon + ct.TKMGhep + ct.TKMDay),
                            GioDon = (int)(ct.DonXP + ct.DonDD + ct.DonKT)
                        };
            if (MaDV != "TCT")
                query = query.Where(x => x.DvcbID == MaDV);
            return await (from x in query
                          group x by new { x.DvcbID, x.CongTacID } into g
                          select new ViewBcKeHoach
                          {
                              DvcbID = g.Key.DvcbID,
                              CongTacID = g.Key.CongTacID,
                              KMChinh = g.Sum(x => x.KMChinh),
                              KMPhuTro = g.Sum(x => x.KMPhuTro),
                              TKMChinh = g.Sum(x => x.TKMChinh),
                              TKMPhuTro = g.Sum(x => x.TKMPhuTro),
                              GioDon = g.Sum(x => x.GioDon)
                          }).ToListAsync();
        }
        
    }
}