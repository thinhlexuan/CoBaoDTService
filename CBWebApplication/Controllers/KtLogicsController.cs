using CBWebApplication.Context;
using CBWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CBWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KtLogicsController : ControllerBase
    {
        private readonly CoBaoDTContext _context;

        public KtLogicsController(CoBaoDTContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetKTQuayVong")]
        public async Task<ActionResult<IEnumerable<KTQuayVong>>> GetKTQuayVong(string madv, int thangdt, int namdt, string daumay)
        {
            var query = from dt in _context.DoanThong
                        join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                        join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                        where dt.ThangDT == thangdt && dt.NamDT == namdt
                        select new
                        {
                            CoBaoID = (long)cb.CoBaoID,
                            SoCB = (string)cb.SoCB,
                            NgayCB = (DateTime)cb.NgayCB,
                            DauMayID = (string)cb.DauMayID,
                            DvcbID = (string)cb.DvcbID,
                            TaiXe1ID = (string)cb.TaiXe1ID,
                            TaiXe1Name = (string)cb.TaiXe1Name,
                            ThangDT = (int)dt.ThangDT,
                            NamDT = (int)dt.NamDT,
                            QuayVong = (int)ct.QuayVong
                        };
            if (madv != "TCT")
                query = query.Where(x => x.DvcbID == madv);
            if (!string.IsNullOrWhiteSpace(daumay))
                query = query.Where(x => x.DauMayID.Contains(daumay));
            return await (from x in query
                          group x by new { x.CoBaoID, x.SoCB, x.NgayCB, x.DauMayID, x.DvcbID, x.TaiXe1ID, x.TaiXe1Name, x.ThangDT, x.NamDT } into g
                          select new KTQuayVong
                          {
                              CoBaoID = g.Key.CoBaoID,
                              SoCB = g.Key.SoCB,
                              NgayCB = g.Key.NgayCB,
                              DauMayID = g.Key.DauMayID,
                              DvcbID = g.Key.DvcbID,
                              TaiXe1ID = g.Key.TaiXe1ID,
                              TaiXe1Name = g.Key.TaiXe1Name,
                              ThangDT = g.Key.ThangDT,
                              NamDT = g.Key.NamDT,
                              QuayVong = g.Sum(x => x.QuayVong)
                          }).Where(x => x.QuayVong < 0 || x.QuayVong > 720).ToListAsync();
        }

        [HttpGet]
        [Route("GetKTQuayVongGA")]
        public async Task<ActionResult<IEnumerable<KTQuayVong>>> GetKTQuayVongGA(string madv, int thangdt, int namdt,string daumay)
        {
            var query = from dt in _context.DoanThongGA
                        join cb in _context.CoBaoGA on dt.DoanThongID equals cb.CoBaoID
                        join ct in _context.DoanThongGACT on dt.DoanThongID equals ct.DoanThongID
                        where dt.ThangDT == thangdt && dt.NamDT == namdt
                        select new
                        {
                            CoBaoID = (long)cb.CoBaoID,
                            SoCB = (string)cb.SoCB,
                            NgayCB = (DateTime)cb.NgayCB,
                            DauMayID = (string)cb.DauMayID,
                            DvcbID = (string)cb.DvcbID,
                            TaiXe1ID = (string)cb.TaiXe1ID,
                            TaiXe1Name = (string)cb.TaiXe1Name,
                            ThangDT = (int)dt.ThangDT,
                            NamDT = (int)dt.NamDT,
                            QuayVong = (int)ct.QuayVong
                        };
            if (madv != "TCT")
                query = query.Where(x => x.DvcbID == madv);
            if (!string.IsNullOrWhiteSpace(daumay))
                query = query.Where(x => x.DauMayID.Contains(daumay));
            return await (from x in query
                          group x by new { x.CoBaoID, x.SoCB, x.NgayCB, x.DauMayID, x.DvcbID, x.TaiXe1ID, x.TaiXe1Name, x.ThangDT, x.NamDT } into g
                          select new KTQuayVong
                          {
                              CoBaoID = g.Key.CoBaoID,
                              SoCB = g.Key.SoCB,
                              NgayCB = g.Key.NgayCB,
                              DauMayID = g.Key.DauMayID,
                              DvcbID = g.Key.DvcbID,
                              TaiXe1ID = g.Key.TaiXe1ID,
                              TaiXe1Name = g.Key.TaiXe1Name,
                              ThangDT = g.Key.ThangDT,
                              NamDT = g.Key.NamDT,
                              QuayVong = g.Sum(x => x.QuayVong)
                          }).Where(x => x.QuayVong < 0 || x.QuayVong > 720).ToListAsync();
        }

        [HttpGet]
        [Route("GetKTDonThuan")]
        public async Task<ActionResult<IEnumerable<KTDonThuan>>> GetKTDonThuan(string madv, int thangdt, int namdt,string daumay)
        {
            var query = from dt in _context.DoanThong
                        join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                        join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                        where dt.ThangDT == thangdt && dt.NamDT == namdt
                        select new
                        {
                            CoBaoID = (long)cb.CoBaoID,
                            SoCB = (string)cb.SoCB,
                            NgayCB = (DateTime)cb.NgayCB,
                            DauMayID = (string)cb.DauMayID,
                            DvcbID = (string)cb.DvcbID,
                            TaiXe1ID = (string)cb.TaiXe1ID,
                            TaiXe1Name = (string)cb.TaiXe1Name,
                            ThangDT = (int)dt.ThangDT,
                            NamDT = (int)dt.NamDT,
                            DonThuan = (int)ct.DonThuan
                        };
            if (madv != "TCT")
                query = query.Where(x => x.DvcbID == madv);
            if (!string.IsNullOrWhiteSpace(daumay))
                query = query.Where(x => x.DauMayID.Contains(daumay));
            return await (from x in query
                          group x by new { x.CoBaoID, x.SoCB, x.NgayCB, x.DauMayID, x.DvcbID, x.TaiXe1ID, x.TaiXe1Name, x.ThangDT, x.NamDT } into g
                          select new KTDonThuan
                          {
                              CoBaoID = g.Key.CoBaoID,
                              SoCB = g.Key.SoCB,
                              NgayCB = g.Key.NgayCB,
                              DauMayID = g.Key.DauMayID,
                              DvcbID = g.Key.DvcbID,
                              TaiXe1ID = g.Key.TaiXe1ID,
                              TaiXe1Name = g.Key.TaiXe1Name,
                              ThangDT = g.Key.ThangDT,
                              NamDT = g.Key.NamDT,
                              DonThuan = g.Sum(x => x.DonThuan)
                          }).Where(x => x.DonThuan < 0 || x.DonThuan > 600).ToListAsync();
        }

        [HttpGet]
        [Route("GetKTDonThuanGA")]
        public async Task<ActionResult<IEnumerable<KTDonThuan>>> GetKTDonThuanGA(string madv, int thangdt, int namdt,string daumay)
        {
            var query = from dt in _context.DoanThongGA
                        join cb in _context.CoBaoGA on dt.DoanThongID equals cb.CoBaoID
                        join ct in _context.DoanThongGACT on dt.DoanThongID equals ct.DoanThongID
                        where dt.ThangDT == thangdt && dt.NamDT == namdt
                        select new
                        {
                            CoBaoID = (long)cb.CoBaoID,
                            SoCB = (string)cb.SoCB,
                            NgayCB = (DateTime)cb.NgayCB,
                            DauMayID = (string)cb.DauMayID,
                            DvcbID = (string)cb.DvcbID,
                            TaiXe1ID = (string)cb.TaiXe1ID,
                            TaiXe1Name = (string)cb.TaiXe1Name,
                            ThangDT = (int)dt.ThangDT,
                            NamDT = (int)dt.NamDT,
                            DonThuan = (int)ct.DonThuan
                        };
            if (madv != "TCT")
                query = query.Where(x => x.DvcbID == madv);
            if (!string.IsNullOrWhiteSpace(daumay))
                query = query.Where(x => x.DauMayID.Contains(daumay));
            return await (from x in query
                          group x by new { x.CoBaoID, x.SoCB, x.NgayCB, x.DauMayID, x.DvcbID, x.TaiXe1ID, x.TaiXe1Name, x.ThangDT, x.NamDT } into g
                          select new KTDonThuan
                          {
                              CoBaoID = g.Key.CoBaoID,
                              SoCB = g.Key.SoCB,
                              NgayCB = g.Key.NgayCB,
                              DauMayID = g.Key.DauMayID,
                              DvcbID = g.Key.DvcbID,
                              TaiXe1ID = g.Key.TaiXe1ID,
                              TaiXe1Name = g.Key.TaiXe1Name,
                              ThangDT = g.Key.ThangDT,
                              NamDT = g.Key.NamDT,
                              DonThuan = g.Sum(x => x.DonThuan)
                          }).Where(x => x.DonThuan < 0 || x.DonThuan > 600).ToListAsync();
        }

        [HttpGet]
        [Route("GetKTVanTocKT")]
        public async Task<ActionResult<IEnumerable<KTVanTocKT>>> GetKTVanTocKT(string madv, int thangdt, int namdt,string daumay)
        {
            var query = from dt in _context.DoanThong
                        join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                        join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                        where dt.ThangDT == thangdt && dt.NamDT == namdt && ct.DonThuan>0
                        select new
                        {
                            CoBaoID = (long)cb.CoBaoID,
                            SoCB = (string)cb.SoCB,
                            NgayCB = (DateTime)cb.NgayCB,
                            DauMayID = (string)cb.DauMayID,
                            DvcbID = (string)cb.DvcbID,
                            TaiXe1ID = (string)cb.TaiXe1ID,
                            TaiXe1Name = (string)cb.TaiXe1Name,
                            MacTauID = (string)ct.MacTauID,     
                            CongTacID=(short)ct.CongTacID,
                            CongTacName=(string)ct.CongTacName,
                            KM = (decimal)(ct.KMChinh+ct.KMDon+ct.KMGhep+ct.KMDay),
                            DonThuan= ct.DonThuan
                        };
            if (madv != "TCT")
                query = query.Where(x => x.DvcbID == madv);
            if (!string.IsNullOrWhiteSpace(daumay))
                query = query.Where(x => x.DauMayID.Contains(daumay));
            return await (from x in query
                          group x by new { x.CoBaoID, x.SoCB, x.NgayCB, x.DauMayID, x.DvcbID, x.TaiXe1ID, x.TaiXe1Name,x.MacTauID,x.CongTacID,x.CongTacName } into g
                          select new KTVanTocKT
                          {
                              CoBaoID = g.Key.CoBaoID,
                              SoCB = g.Key.SoCB,
                              NgayCB = g.Key.NgayCB,
                              DauMayID = g.Key.DauMayID,
                              DvcbID = g.Key.DvcbID,
                              TaiXe1ID = g.Key.TaiXe1ID,
                              TaiXe1Name = g.Key.TaiXe1Name,
                              MacTauID= g.Key.MacTauID,
                              CongTacID=g.Key.CongTacID,
                              CongTacName=g.Key.CongTacName,
                              VanToc = (decimal)g.Sum(x => x.KM) * 60 / g.Sum(x => x.DonThuan)
                          }).Where(x => x.VanToc < 8 || x.VanToc > 70).ToListAsync();           
        }

        [HttpGet]
        [Route("GetKTVanTocKTGA")]
        public async Task<ActionResult<IEnumerable<KTVanTocKT>>> GetKTVanTocKTGA(string madv, int thangdt, int namdt,string daumay)
        {
            var query = from dt in _context.DoanThongGA
                        join cb in _context.CoBaoGA on dt.DoanThongID equals cb.CoBaoID
                        join ct in _context.DoanThongGACT on dt.DoanThongID equals ct.DoanThongID
                        where dt.ThangDT == thangdt && dt.NamDT == namdt && ct.DonThuan > 0
                        select new 
                        {
                            CoBaoID = (long)cb.CoBaoID,
                            SoCB = (string)cb.SoCB,
                            NgayCB = (DateTime)cb.NgayCB,
                            DauMayID = (string)cb.DauMayID,
                            DvcbID = (string)cb.DvcbID,
                            TaiXe1ID = (string)cb.TaiXe1ID,
                            TaiXe1Name = (string)cb.TaiXe1Name,
                            MacTauID = (string)ct.MacTauID,
                            CongTacID = (short)ct.CongTacID,
                            CongTacName = (string)ct.CongTacName,
                            KM = (decimal)(ct.KMChinh + ct.KMDon + ct.KMGhep + ct.KMDay),
                            DonThuan = ct.DonThuan
                        };
            if (madv != "TCT")
                query = query.Where(x => x.DvcbID == madv);
            if (!string.IsNullOrWhiteSpace(daumay))
                query = query.Where(x => x.DauMayID.Contains(daumay));
            return await (from x in query
                          group x by new { x.CoBaoID, x.SoCB, x.NgayCB, x.DauMayID, x.DvcbID, x.TaiXe1ID, x.TaiXe1Name, x.MacTauID, x.CongTacID, x.CongTacName } into g
                          select new KTVanTocKT
                          {
                              CoBaoID = g.Key.CoBaoID,
                              SoCB = g.Key.SoCB,
                              NgayCB = g.Key.NgayCB,
                              DauMayID = g.Key.DauMayID,
                              DvcbID = g.Key.DvcbID,
                              TaiXe1ID = g.Key.TaiXe1ID,
                              TaiXe1Name = g.Key.TaiXe1Name,
                              MacTauID = g.Key.MacTauID,
                              CongTacID = g.Key.CongTacID,
                              CongTacName = g.Key.CongTacName,
                              VanToc = (decimal)g.Sum(x => x.KM) * 60 / g.Sum(x => x.DonThuan)
                          }).Where(x => x.VanToc < 8 || x.VanToc > 70).ToListAsync();
        }

        [HttpGet]
        [Route("GetKTDungKB")]
        public async Task<ActionResult<IEnumerable<KTDungKB>>> GetKTDungKB(string madv, int thangdt, int namdt,string daumay)
        {
            var query = from dt in _context.DoanThong
                        join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID                      
                        where dt.ThangDT == thangdt && dt.NamDT == namdt && (dt.DungKB<0||dt.DungKB>1440)
                        select new
                        {
                            DauMayID = (string)cb.DauMayID,
                            DungKB = (int)dt.DungKB,
                            CoBaoID = (long)cb.CoBaoID,
                            SoCB = (string)cb.SoCB,
                            NhanMay = (DateTime)cb.NhanMay,
                            GiaoMay = (DateTime)cb.GiaoMay,
                            DvcbID = (string)cb.DvcbID,
                            TaiXe1Name = (string)cb.TaiXe1Name
                        };
            if (madv != "TCT")
                query = query.Where(x => x.DvcbID == madv);
            if (!string.IsNullOrWhiteSpace(daumay))
                query = query.Where(x => x.DauMayID.Contains(daumay));           
            var querylist = await query.OrderBy(x => x.DauMayID).ThenBy(x => x.GiaoMay).ToListAsync();

            DateTime ngayBD = new DateTime(namdt, thangdt, 1).AddDays(-5);
            int dayinMonth = DateTime.DaysInMonth(namdt, thangdt);
            DateTime ngayKT = new DateTime(namdt, thangdt, dayinMonth, 23, 59, 59);
            var queryOld = _context.CoBao.Where(x => x.GiaoMay >= ngayBD && x.GiaoMay <= ngayKT);
            if (!string.IsNullOrWhiteSpace(daumay))
                queryOld = queryOld.Where(x => x.DauMayID.Contains(daumay));
            var querylistOld = await queryOld.OrderBy(x => x.DauMayID).ThenBy(x => x.GiaoMay).ToListAsync();

            List<KTDungKB> _list = new List<KTDungKB>();
            foreach (var ct in query)
            {
                var ctOld = querylistOld.Where(x => x.DauMayID == ct.DauMayID && x.GiaoMay < ct.GiaoMay).OrderByDescending(x => x.GiaoMay).FirstOrDefault();
                if (ctOld != null)
                {
                    KTDungKB cb = new KTDungKB();
                    cb.DauMayID = ct.DauMayID;
                    cb.DungKB = ct.DungKB;
                    cb.GiaoMay = ctOld.GiaoMay;
                    cb.NhanMay = ct.NhanMay;
                    cb.SoCBGiao = ctOld.SoCB;
                    cb.SoCBNhan = ct.SoCB;
                    cb.TaiXeGiao = ctOld.TaiXe1Name;
                    cb.TaiXeNhan = ct.TaiXe1Name;
                    cb.DvGiao = ctOld.DvcbID;
                    cb.DvNhan = ct.DvcbID;
                    cb.CoBaoIDGiao = ctOld.CoBaoID;
                    cb.CoBaoIDNhan = ct.CoBaoID;
                    _list.Add(cb);
                }
            }
            return _list;
        }

        [HttpGet]
        [Route("GetKTDungKBGA")]
        public async Task<ActionResult<IEnumerable<KTDungKB>>> GetKTDungKBGA(string madv, int thangdt, int namdt, string daumay)
        {
            var query = from dt in _context.DoanThongGA
                        join cb in _context.CoBaoGA on dt.DoanThongID equals cb.CoBaoID
                        where dt.ThangDT == thangdt && dt.NamDT == namdt && (dt.DungKB < 0 || dt.DungKB > 1440)
                        select new
                        {
                            DauMayID = (string)cb.DauMayID,
                            DungKB = (int)dt.DungKB,
                            CoBaoID = (long)cb.CoBaoID,
                            SoCB = (string)cb.SoCB,
                            NhanMay = (DateTime)cb.NhanMay,
                            GiaoMay = (DateTime)cb.GiaoMay,
                            DvcbID = (string)cb.DvcbID,
                            TaiXe1Name = (string)cb.TaiXe1Name
                        };
            if (madv != "TCT")
                query = query.Where(x => x.DvcbID == madv);
            if (!string.IsNullOrWhiteSpace(daumay))
                query = query.Where(x => x.DauMayID.Contains(daumay));
            var querylist = await query.OrderBy(x => x.DauMayID).ThenBy(x => x.GiaoMay).ToListAsync();

            DateTime ngayBD = new DateTime(namdt, thangdt, 1).AddDays(-5);
            int dayinMonth = DateTime.DaysInMonth(namdt, thangdt);
            DateTime ngayKT = new DateTime(namdt, thangdt, dayinMonth, 23, 59, 59);
            var queryOld = _context.CoBao.Where(x => x.GiaoMay >= ngayBD && x.GiaoMay <= ngayKT);
            if (!string.IsNullOrWhiteSpace(daumay))
                queryOld = queryOld.Where(x => x.DauMayID.Contains(daumay));
            var querylistOld = await queryOld.OrderBy(x => x.DauMayID).ThenBy(x => x.GiaoMay).ToListAsync();

            List<KTDungKB> _list = new List<KTDungKB>();
            foreach (var ct in query)
            {
                var ctOld = querylistOld.Where(x => x.DauMayID == ct.DauMayID && x.GiaoMay < ct.GiaoMay).OrderByDescending(x => x.GiaoMay).FirstOrDefault();
                if (ctOld != null)
                {
                    KTDungKB cb = new KTDungKB();
                    cb.DauMayID = ct.DauMayID;
                    cb.DungKB = ct.DungKB;
                    cb.GiaoMay = ctOld.GiaoMay;
                    cb.NhanMay = ct.NhanMay;
                    cb.SoCBGiao = ctOld.SoCB;
                    cb.SoCBNhan = ct.SoCB;
                    cb.TaiXeGiao = ctOld.TaiXe1Name;
                    cb.TaiXeNhan = ct.TaiXe1Name;
                    cb.DvGiao = ctOld.DvcbID;
                    cb.DvNhan = ct.DvcbID;
                    cb.CoBaoIDGiao = ctOld.CoBaoID;
                    cb.CoBaoIDNhan = ct.CoBaoID;
                    _list.Add(cb);
                }
            }
            return _list;
        }

        [HttpGet]
        [Route("GetKTNhienLieu")]
        public async Task<ActionResult<IEnumerable<KTNhienLieu>>> GetKTNhienLieu(string madv, int thangdt, int namdt, string daumay)
        {
            var query = from dt in _context.DoanThong
                        join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                        where dt.ThangDT == thangdt && dt.NamDT == namdt
                        select new
                        {
                            DauMayID = (string)cb.DauMayID,
                            NhanMay = (DateTime)cb.NhanMay,
                            GiaoMay = (DateTime)cb.GiaoMay,
                            CoBaoID = (long)cb.CoBaoID,
                            SoCB = (string)cb.SoCB,                            
                            DvcbID = (string)cb.DvcbID,
                            TaiXe1Name = (string)cb.TaiXe1Name,
                            NLBanTruoc = (int)cb.NLBanTruoc,
                            NLBanSau = (int)cb.NLBanSau                           
                        };
            if (madv != "TCT")
                query = query.Where(x => x.DvcbID == madv);
            if (!string.IsNullOrWhiteSpace(daumay))
                query = query.Where(x => x.DauMayID.Contains(daumay));
            var querylist = await query.OrderBy(x => x.DauMayID).ThenBy(x => x.GiaoMay).ToListAsync();

            DateTime ngayBD = new DateTime(namdt, thangdt, 1).AddDays(-5);
            int dayinMonth = DateTime.DaysInMonth(namdt, thangdt);
            DateTime ngayKT = new DateTime(namdt, thangdt, dayinMonth, 23, 59, 59);
            var queryOld = _context.CoBao.Where(x => x.GiaoMay >= ngayBD && x.GiaoMay <= ngayKT);
            if (!string.IsNullOrWhiteSpace(daumay))
                queryOld = queryOld.Where(x => x.DauMayID.Contains(daumay));
            var querylistOld = await queryOld.OrderBy(x => x.DauMayID).ThenBy(x => x.GiaoMay).ToListAsync();

            List<KTNhienLieu> _list = new List<KTNhienLieu>();
            foreach (var ct in querylist)
            {               
                var ctOld = querylistOld.Where(x => x.DauMayID == ct.DauMayID && x.GiaoMay < ct.GiaoMay).OrderByDescending(x => x.GiaoMay).FirstOrDefault();
                if (ctOld != null)
                {
                    if (ct.DauMayID == ctOld.DauMayID && ct.CoBaoID!=ctOld.CoBaoID && ct.NLBanTruoc != ctOld.NLBanSau)
                    {
                        KTNhienLieu nl = new KTNhienLieu();
                        nl.DauMayID = ct.DauMayID;
                        nl.NLBanGiao = ctOld.NLBanSau;
                        nl.NLBanNhan = ct.NLBanTruoc;
                        nl.GiaoMay = ctOld.GiaoMay;
                        nl.NhanMay = ct.NhanMay;
                        nl.SoCBGiao = ctOld.SoCB;
                        nl.SoCBNhan = ct.SoCB;
                        nl.TaiXeGiao = ctOld.TaiXe1Name;
                        nl.TaiXeNhan = ct.TaiXe1Name;
                        nl.DvGiao = ctOld.DvcbID;
                        nl.DvNhan = ct.DvcbID;
                        nl.CoBaoIDGiao = ctOld.CoBaoID;
                        nl.CoBaoIDNhan = ct.CoBaoID;                       
                        _list.Add(nl);
                    }
                }
            }           
            return _list;
        }

        [HttpGet]
        [Route("GetKTNhienLieuGA")]
        public async Task<ActionResult<IEnumerable<KTNhienLieu>>> GetKTNhienLieuGA(string madv, int thangdt, int namdt, string daumay)
        {
            var query = from dt in _context.DoanThongGA
                        join cb in _context.CoBaoGA on dt.DoanThongID equals cb.CoBaoID
                        where dt.ThangDT == thangdt && dt.NamDT == namdt
                        select new
                        {
                            DauMayID = (string)cb.DauMayID,
                            NhanMay = (DateTime)cb.NhanMay,
                            GiaoMay = (DateTime)cb.GiaoMay,
                            CoBaoID = (long)cb.CoBaoID,
                            SoCB = (string)cb.SoCB,
                            DvcbID = (string)cb.DvcbID,
                            TaiXe1Name = (string)cb.TaiXe1Name,
                            NLBanTruoc = (int)cb.NLBanTruoc,
                            NLBanSau = (int)cb.NLBanSau
                        };
            if (madv != "TCT")
                query = query.Where(x => x.DvcbID == madv);
            if (!string.IsNullOrWhiteSpace(daumay))
                query = query.Where(x => x.DauMayID.Contains(daumay));
            var querylist = await query.OrderBy(x => x.DauMayID).ThenBy(x => x.GiaoMay).ToListAsync();

            DateTime ngayBD = new DateTime(namdt, thangdt, 1).AddDays(-5);
            int dayinMonth = DateTime.DaysInMonth(namdt, thangdt);
            DateTime ngayKT = new DateTime(namdt, thangdt, dayinMonth, 23, 59, 59);
            var queryOld = _context.CoBaoGA.Where(x => x.GiaoMay >= ngayBD && x.GiaoMay <= ngayKT);
            if (!string.IsNullOrWhiteSpace(daumay))
                queryOld = queryOld.Where(x => x.DauMayID.Contains(daumay));
            var querylistOld = await queryOld.OrderBy(x => x.DauMayID).ThenBy(x => x.GiaoMay).ToListAsync();

            List<KTNhienLieu> _list = new List<KTNhienLieu>();
            foreach (var ct in querylist)
            {
                var ctOld = querylistOld.Where(x => x.DauMayID == ct.DauMayID && x.GiaoMay < ct.GiaoMay).OrderByDescending(x => x.GiaoMay).FirstOrDefault();
                if (ctOld != null)
                    if (ct.DauMayID == ctOld.DauMayID && ct.CoBaoID != ctOld.CoBaoID && ct.NLBanTruoc != ctOld.NLBanSau)
                    {
                        KTNhienLieu nl = new KTNhienLieu();
                        nl.DauMayID = ct.DauMayID;
                        nl.NLBanGiao = ctOld.NLBanSau;
                        nl.NLBanNhan = ct.NLBanTruoc;
                        nl.GiaoMay = ctOld.GiaoMay;
                        nl.NhanMay = ct.NhanMay;
                        nl.SoCBGiao = ctOld.SoCB;
                        nl.SoCBNhan = ct.SoCB;
                        nl.TaiXeGiao = ctOld.TaiXe1Name;
                        nl.TaiXeNhan = ct.TaiXe1Name;
                        nl.DvGiao = ctOld.DvcbID;
                        nl.DvNhan = ct.DvcbID;
                        nl.CoBaoIDGiao = ctOld.CoBaoID;
                        nl.CoBaoIDNhan = ct.CoBaoID;
                        _list.Add(nl);
                    }
            }
            if (madv != "TCT")
                _list = _list.Where(x => x.DvGiao == madv && x.DvNhan == madv).ToList();           
            return _list;
        }
    }
}
