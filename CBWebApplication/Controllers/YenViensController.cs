using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CBWebApplication.Context;
using CBWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CBWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YenViensController : ControllerBase
    {
        private readonly CoBaoDTContext _context;
        public YenViensController(CoBaoDTContext context)
        {
            _context = context;
        }

        #region KhuDoan
        [HttpGet]
        [Route("YVGetKhuDoanOBJ")]
        public async Task<ActionResult<string>> YVGetKhuDoanOBJ(string LoaiMay, string Tuyen,string GaXP, string GaKT, string CongTac)
        {
            string KhuDoan = string.Empty;
            var query = from item in _context.YV_KhuDoan
                         where item.LoaiMayID == LoaiMay && item.CacGa.Contains(GaXP) && item.CacGa.Contains(GaKT)
                        select item;
            if (!string.IsNullOrWhiteSpace(CongTac))
                query= query.Where(x => x.CongTac==CongTac);
            else
                query = query.Where(x => x.CongTac == null || x.CongTac == string.Empty);
          
            var objTuyen = await query.Where(x => x.Tuyen == Tuyen).OrderBy(x => x.Km).FirstOrDefaultAsync();
            if (objTuyen != null)
            {              
                KhuDoan = objTuyen.KhuDoanID;
                var kmGaXP = _context.LyTrinh.Where(x => x.GaID == int.Parse(GaXP) && x.TuyenID == Tuyen).FirstOrDefaultAsync().Result.Km;
                var kmGaKT = _context.LyTrinh.Where(x => x.GaID == int.Parse(GaKT) && x.TuyenID == Tuyen).FirstOrDefaultAsync().Result.Km;
                string chieu = kmGaXP < kmGaKT ? "di" : "ve";
                if(Tuyen=="BHVD")
                    chieu=kmGaXP < kmGaKT ? "ve" : "di";
                KhuDoan += "-" + chieu;
            }
            else
            {
                var obj = await query.OrderBy(x => x.Km).FirstOrDefaultAsync();
                if (obj != null)
                {
                    KhuDoan = obj.KhuDoanID;
                    var kmGaXP = _context.LyTrinh.Where(x => x.GaID == int.Parse(GaXP) && x.TuyenID == Tuyen).FirstOrDefaultAsync().Result.Km;
                    var kmGaKT = _context.LyTrinh.Where(x => x.GaID == int.Parse(GaKT) && x.TuyenID == Tuyen).FirstOrDefaultAsync().Result.Km;
                    string chieu = kmGaXP < kmGaKT ? "di" : "ve";
                    if (Tuyen == "BHVD")
                        chieu = kmGaXP < kmGaKT ? "ve" : "di";
                    KhuDoan += "-" + chieu;
                }
            }    
            return KhuDoan;
        }

        [HttpGet]
        [Route("YVGetKhuDoan")]
        public async Task<ActionResult<IEnumerable<YVKhuDoan>>> YVGetKhuDoan(string LoaiMay, string KhuDoan)
        {
            var query = from item in _context.YV_KhuDoan select item;           
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay);
            if (!string.IsNullOrWhiteSpace(KhuDoan))
                query = query.Where(x => x.KhuDoanID.Contains(KhuDoan));
            return await query.OrderBy(x=>x.LoaiMayID).ToListAsync();
        }
        [HttpPut]
        [Route("YVPutKhuDoan")]
        public async Task<ActionResult<YVKhuDoan>> YVPutKhuDoan(long id, YVKhuDoan yVKhuDoan)
        {
            yVKhuDoan.Modifydate = DateTime.Now;
            if (id != yVKhuDoan.ID)
            {
                return BadRequest();
            }
            _context.Entry(yVKhuDoan).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return yVKhuDoan;
        }

        [HttpPost]
        [Route("YVPostKhuDoan")]
        public async Task<ActionResult<YVKhuDoan>> YVPostKhuDoan(YVKhuDoan yVKhuDoan)
        {
            yVKhuDoan.Createddate = DateTime.Now;
            yVKhuDoan.Modifydate = yVKhuDoan.Createddate;
            var query = _context.YV_KhuDoan.Where(x =>x.LoaiMayID==yVKhuDoan.LoaiMayID && x.KhuDoanID == yVKhuDoan.KhuDoanID && x.CongTac == yVKhuDoan.CongTac).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.YV_KhuDoan.Add(yVKhuDoan);
            await _context.SaveChangesAsync();

            return yVKhuDoan;
        }

        [HttpDelete]
        [Route("YVDeleteKhuDoan")]
        public async Task<ActionResult<YVKhuDoan>> YVDeleteKhuDoan(long id)
        {
            var yVKhuDoan = _context.YV_KhuDoan.Where(x => x.ID == id).FirstOrDefault();
            if (yVKhuDoan == null)
            {
                return NotFound();
            }
            _context.YV_KhuDoan.Remove(yVKhuDoan);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "YV_KhuDoan";
            nk.NoiDung = "Xóa khu đoạn: " + yVKhuDoan.KhuDoanID + ". Loại máy: " + yVKhuDoan.LoaiMayID;
            nk.Createddate= DateTime.Now;
            nk.Createdby = yVKhuDoan.Modifyby;
            nk.CreatedName = yVKhuDoan.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return yVKhuDoan;
        }
        #endregion

        #region NLDinhMuc
        [HttpGet]
        [Route("YVGetTanMax")]
        public async Task<ActionResult<decimal>> YVGetTanMax(DateTime NgayHL, string LoaiMay, string KhuDoan, string CongTac)
        {
            decimal TanMax = 0;
            var query = (from item in _context.YV_NLDinhMuc
                         where item.NgayHL <= NgayHL && item.LoaiMayID == LoaiMay
                         orderby item.NgayHL descending
                         select item).Distinct();
            if (!string.IsNullOrWhiteSpace(KhuDoan))
                query = query.Where(x => x.KhuDoan.Contains(KhuDoan));
            if (!string.IsNullOrWhiteSpace(CongTac))
                query =  query.Where(x => x.CongTac == CongTac);
            else
                query = query.Where(x => x.CongTac == null || x.CongTac == string.Empty);
            var obj = await query.OrderByDescending(x => x.TanMax).FirstOrDefaultAsync();
            if (obj != null)
                TanMax = (decimal)obj.TanMax;
            return TanMax;
        }

        [HttpGet]
        [Route("YVGetNLDinhMucOBJ")]
        public async Task<ActionResult<YVNLDinhMuc>> YVGetNLDinhMucOBJ(DateTime NgayHL, string LoaiMay, string KhuDoan,decimal Tan, string CongTac)
        {
            var query = (from item in _context.YV_NLDinhMuc
                         where item.NgayHL <= NgayHL && item.LoaiMayID == LoaiMay && item.TanMin<=Tan && item.TanMax>=Tan
                         orderby item.NgayHL descending
                         select item).Distinct();
            if (!string.IsNullOrWhiteSpace(KhuDoan))
                query = query.Where(x => x.KhuDoan.Contains(KhuDoan));
            if (!string.IsNullOrWhiteSpace(CongTac))
                query = query.Where(x => x.CongTac==CongTac);
            else
                query = query.Where(x => x.CongTac == null || x.CongTac == string.Empty);
            return await query.OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();
        }
       
        [HttpGet]
        [Route("YVGetNLDinhMuc")]
        public async Task<ActionResult<IEnumerable<YVNLDinhMuc>>> YVGetNLDinhMuc(DateTime NgayHL, string LoaiMay, string KhuDoan)
        {           
            var query = (from item in _context.YV_NLDinhMuc where item.NgayHL <= NgayHL orderby item.NgayHL descending select item).Distinct();
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay);
            if (!string.IsNullOrWhiteSpace(KhuDoan))
                query = query.Where(x => x.KhuDoan.Contains(KhuDoan));
            return await query.OrderBy(x => x.LoaiMayID).ToListAsync();
        }
        [HttpPut]
        [Route("YVPutNLDinhMuc")]
        public async Task<ActionResult<YVNLDinhMuc>> YVPutNLDinhMuc(long id, YVNLDinhMuc yVNLDinhMuc)
        {
            yVNLDinhMuc.Modifydate = DateTime.Now;
            if (id != yVNLDinhMuc.ID)
            {
                return BadRequest();
            }
            _context.Entry(yVNLDinhMuc).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return yVNLDinhMuc;
        }

        [HttpPost]
        [Route("YVPostNLDinhMuc")]
        public async Task<ActionResult<YVNLDinhMuc>> YVPostNLDinhMuc(YVNLDinhMuc yVNLDinhMuc)
        {
            yVNLDinhMuc.Createddate = DateTime.Now;
            yVNLDinhMuc.Modifydate = yVNLDinhMuc.Createddate;
            var query = _context.YV_NLDinhMuc.Where(x => x.LoaiMayID == yVNLDinhMuc.LoaiMayID && x.CongTac==yVNLDinhMuc.CongTac && x.KhuDoan == yVNLDinhMuc.KhuDoan 
            && x.NgayHL == yVNLDinhMuc.NgayHL && x.TanMin==yVNLDinhMuc.TanMin && x.TanMax==yVNLDinhMuc.TanMax).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.YV_NLDinhMuc.Add(yVNLDinhMuc);
            await _context.SaveChangesAsync();

            return yVNLDinhMuc;
        }

        [HttpDelete]
        [Route("YVDeleteNLDinhMuc")]
        public async Task<ActionResult<YVNLDinhMuc>> YVDeleteNLDinhMuc(long id)
        {
            var yVNLDinhMuc = _context.YV_NLDinhMuc.Where(x => x.ID == id).FirstOrDefault();
            if (yVNLDinhMuc == null)
            {
                return NotFound();
            }
            _context.YV_NLDinhMuc.Remove(yVNLDinhMuc);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "YV_NLDinhMuc";
            nk.NoiDung = "Xóa NL định mức: " + yVNLDinhMuc.KhuDoan + ". Loại máy: " + yVNLDinhMuc.LoaiMayID;
            nk.Createddate = DateTime.Now;
            nk.Createdby = yVNLDinhMuc.Modifyby;
            nk.CreatedName = yVNLDinhMuc.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return yVNLDinhMuc;
        }
        #endregion

        #region NLPDinhMuc
        [HttpGet]
        [Route("YVGetNLPDinhMucOBJ")]
        public async Task<ActionResult<YVNLPDinhMuc>> YVGetNLPDinhMucOBJ(DateTime NgayHL, string LoaiMay, string CongTac, string KhuDoan)
        {
            var query = (from item in _context.YV_NLPDinhMuc where item.NgayHL <= NgayHL && item.LoaiMayID == LoaiMay && item.CongTac==CongTac
                         orderby item.NgayHL descending select item).Distinct();           
            if (!string.IsNullOrWhiteSpace(KhuDoan))
                query = query.Where(x => x.KhuDoan.Contains(KhuDoan));
            return await query.OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();
        }
        [HttpGet]
        [Route("YVGetNLPDinhMuc")]
        public async Task<ActionResult<IEnumerable<YVNLPDinhMuc>>> YVGetNLPDinhMuc(DateTime NgayHL, string LoaiMay, string KhuDoan)
        {
            var query = (from item in _context.YV_NLPDinhMuc where item.NgayHL <= NgayHL orderby item.NgayHL descending select item).Distinct();
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay);
            if (!string.IsNullOrWhiteSpace(KhuDoan))
                query = query.Where(x => x.KhuDoan.Contains(KhuDoan));
            return await query.OrderBy(x => x.LoaiMayID).ToListAsync();
        }
        [HttpPut]
        [Route("YVPutNLPDinhMuc")]
        public async Task<ActionResult<YVNLPDinhMuc>> YVPutNLPDinhMuc(long id, YVNLPDinhMuc yVNLPDinhMuc)
        {
            yVNLPDinhMuc.Modifydate = DateTime.Now;
            if (id != yVNLPDinhMuc.ID)
            {
                return BadRequest();
            }
            _context.Entry(yVNLPDinhMuc).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return yVNLPDinhMuc;
        }

        [HttpPost]
        [Route("YVPostNLPDinhMuc")]
        public async Task<ActionResult<YVNLPDinhMuc>> YVPostNLPDinhMuc(YVNLPDinhMuc yVNLPDinhMuc)
        {
            yVNLPDinhMuc.Createddate = DateTime.Now;
            yVNLPDinhMuc.Modifydate = yVNLPDinhMuc.Createddate;            
            var query = _context.YV_NLPDinhMuc.Where(x => x.LoaiMayID == yVNLPDinhMuc.LoaiMayID && x.KhuDoan == yVNLPDinhMuc.KhuDoan && x.NgayHL == yVNLPDinhMuc.NgayHL).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.YV_NLPDinhMuc.Add(yVNLPDinhMuc);
            await _context.SaveChangesAsync();

            return yVNLPDinhMuc;
        }

        [HttpDelete]
        [Route("YVDeleteNLPDinhMuc")]
        public async Task<ActionResult<YVNLPDinhMuc>> YVDeleteNLPDinhMuc(long id)
        {
            var yVNLPDinhMuc = _context.YV_NLPDinhMuc.Where(x => x.ID == id).FirstOrDefault();
            if (yVNLPDinhMuc == null)
            {
                return NotFound();
            }
            _context.YV_NLPDinhMuc.Remove(yVNLPDinhMuc);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "YV_NLPDinhMuc";
            nk.NoiDung = "Xóa NL phụ định mức: " + yVNLPDinhMuc.KhuDoan + ". Loại máy: " + yVNLPDinhMuc.LoaiMayID;
            nk.Createddate = DateTime.Now;
            nk.Createdby = yVNLPDinhMuc.Modifyby;
            nk.CreatedName = yVNLPDinhMuc.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return yVNLPDinhMuc;
        }
        #endregion

        #region DMDinhMuc
        [HttpGet]
        [Route("YVGetDMDinhMuc")]
        public async Task<ActionResult<IEnumerable<YVDMDinhMuc>>> YVGetDMDinhMuc(DateTime NgayHL, string LoaiMay, short DauMoID)
        {
            var query = (from item in _context.YV_DMDinhMuc where item.NgayHL <= NgayHL orderby item.NgayHL descending select item).Distinct();
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay);
            if (DauMoID>0)
                query = query.Where(x => x.DauMoID==DauMoID);
            return await query.OrderBy(x => x.LoaiMayID).ToListAsync();
        }
        [HttpPut]
        [Route("YVPutDMDinhMuc")]
        public async Task<ActionResult<YVDMDinhMuc>> YVPutDMDinhMuc(long id, YVDMDinhMuc yVDMDinhMuc)
        {
            yVDMDinhMuc.Modifydate = DateTime.Now;
            if (id != yVDMDinhMuc.ID)
            {
                return BadRequest();
            }
            _context.Entry(yVDMDinhMuc).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return yVDMDinhMuc;
        }

        [HttpPost]
        [Route("YVPostDMDinhMuc")]
        public async Task<ActionResult<YVDMDinhMuc>> YVPostDMDinhMuc(YVDMDinhMuc yVDMDinhMuc)
        {
            yVDMDinhMuc.Createddate = DateTime.Now;
            yVDMDinhMuc.Modifydate = yVDMDinhMuc.Createddate;
            var query = _context.YV_DMDinhMuc.Where(x => x.LoaiMayID == yVDMDinhMuc.LoaiMayID && x.DauMoID == yVDMDinhMuc.DauMoID && x.NgayHL == yVDMDinhMuc.NgayHL).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.YV_DMDinhMuc.Add(yVDMDinhMuc);
            await _context.SaveChangesAsync();

            return yVDMDinhMuc;
        }

        [HttpDelete]
        [Route("YVDeleteDMDinhMuc")]
        public async Task<ActionResult<YVDMDinhMuc>> YVDeleteDMDinhMuc(long id)
        {
            var yVDMDinhMuc = _context.YV_DMDinhMuc.Where(x => x.ID == id).FirstOrDefault();
            if (yVDMDinhMuc == null)
            {
                return NotFound();
            }
            _context.YV_DMDinhMuc.Remove(yVDMDinhMuc);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "YV_DMDinhMuc";
            nk.NoiDung = "Xóa dầu mỡ định mức: " + yVDMDinhMuc.DauMoName + ". Loại máy: " + yVDMDinhMuc.LoaiMayID;
            nk.Createddate = DateTime.Now;
            nk.Createdby = yVDMDinhMuc.Modifyby;
            nk.CreatedName = yVDMDinhMuc.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return yVDMDinhMuc;
        }
        #endregion

        #region XuatDT
        [HttpGet]
        [Route("YVGetXuatDT")]
        public async Task<ActionResult<IEnumerable<YVXuatDT>>> YVGetXuatDT(string maDV, int thangDT, int namDT)
        {
            var query = (from dt in _context.DoanThong
                         join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                         join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                         where dt.ThangDT == thangDT && dt.NamDT == namDT && cb.DvcbID == maDV
                         select new
                         {
                             DoanThongID = (long)dt.DoanThongID,
                             socb = (string)cb.SoCB,
                             lmay = (string)cb.LoaiMayID,
                             dmay = (string)cb.DauMayID,
                             sdb1 = (string)cb.TaiXe1ID,
                             ten1 = (string)cb.TaiXe1Name,
                             sdb2 = (string)cb.PhoXe1ID,
                             ten2 = (string)cb.PhoXe1Name,
                             sdb3 = (string)cb.TaiXe2ID,
                             ten3 = (string)cb.TaiXe2Name,
                             mtau = (string)ct.MacTauID,
                             ctac = (short)ct.CongTacID,
                             tchat = (short)ct.TinhChatID,
                             mdoan=(string)ct.TuyenID,
                             kdoan = (string)ct.KhuDoan,
                             slbt = (decimal)cb.NLBanTruoc,
                             sllh = (decimal)cb.NLLinh,
                             slbs = (int)cb.NLBanSau,
                             sltt = (decimal)ct.TieuThu,
                             sltc = (decimal)ct.DinhMuc,
                             slpt = (decimal)cb.NLTrongDoan,
                             nlanh = (string)cb.TramNLID,
                             gaxp = (string)ct.GaXPName,
                             daycb = (DateTime)cb.NgayCB,
                             dgkb = (decimal)dt.DungKB,
                             dgdm = (decimal)ct.DungDM,
                             dgdn = (decimal)ct.DungDN,
                             dgkm = (decimal)ct.DungKhoDM,
                             dgkn = (decimal)ct.DungKhoDN,
                             dgqd = (decimal)ct.DungQD,
                             giqv = (decimal)ct.QuayVong,
                             gilh = (decimal)ct.LuHanh,
                             gidt = (decimal)ct.DonThuan,
                             dgxp = (decimal)ct.DungXP,
                             dgdd = (decimal)ct.DungDD,
                             dgcc = (decimal)ct.DungKT,
                             dnxp = (decimal)ct.DonXP,
                             dndd = (decimal)ct.DonDD,
                             dncc = (decimal)ct.DonKT,
                             slrk = (decimal)cb.SoLanRaKho,
                             kmch = (decimal)ct.KMChinh,
                             kmdw = (decimal)ct.KMDon,
                             kmgh = (decimal)ct.KMGhep,
                             kmdy = (decimal)ct.KMDay,
                             tkch = (decimal)ct.TKMChinh,
                             tkdw = (decimal)ct.TKMDon,
                             tkgh = (decimal)ct.TKMGhep,
                             tkdy = (decimal)ct.TKMDay,
                             slrkm = (decimal)ct.SLRKDM,
                             slrkn = (decimal)ct.SLRKDN,
                             cty=(string)ct.CongTyID,
                             daylt=(DateTime)ct.NgayXP,
                             dtau = (string)cb.SHDT
                         });


            var query1 = from dt in query
                         join dmc in _context.CoBaoDM on dt.DoanThongID equals dmc.CoBaoID into dmcs
                         from dmc in dmcs.DefaultIfEmpty()
                         join dmd in _context.DoanThongDM on dt.DoanThongID equals dmd.DoanThongID into dmds
                         from dmd in dmds.DefaultIfEmpty()
                         join ty in _context.Tuyen on dt.mdoan equals ty.TuyenID into tys
                         from ty in tys.DefaultIfEmpty()
                         select new YVXuatDT
                         {
                             SOCB = (string)dt.socb,
                             LMAY = (string)dt.lmay,
                             DMAY = (string)dt.dmay,
                             SDB1 = (string)dt.sdb1,
                             TEN1 = (string)dt.ten1,
                             SDB2 = (string)dt.sdb2,
                             TEN2 = (string)dt.ten2,
                             SDB3 = (string)dt.sdb3,
                             TEN3 = (string)dt.ten3,
                             MTAU = (string)dt.mtau,
                             CTAC = (short)dt.ctac,
                             TCHAT = (short)dt.tchat,
                             MDOAN = (string)ty.TuyenMap.ToString(),
                             KDOAN = (string)dt.kdoan,
                             SLBT = (decimal)dt.slbt,
                             SLLH = (decimal)dt.sllh,
                             SLBS = (int)dt.slbs,
                             SLTT = (decimal)dt.sltt,
                             SLTC = (decimal)dt.sltc,
                             SLPT = (decimal)dt.slpt,
                             NLANH = (string)dt.nlanh,
                             GAXP = (string)dt.gaxp,
                             DAYCB = (DateTime)dt.daycb,
                             DGKB = (decimal)dt.dgkb,
                             DGDM = (decimal)dt.dgdm,
                             DGDN = (decimal)dt.dgdn,
                             DGKM = (decimal)dt.dgkm,
                             DGKN = (decimal)dt.dgkn,
                             DGQD = (decimal)dt.dgqd,
                             GIQV = (decimal)dt.giqv,
                             GILH = (decimal)dt.gilh,
                             GIDT = (decimal)dt.gidt,
                             DGXP = (decimal)dt.dgxp,
                             DGDD = (decimal)dt.dgdd,
                             DGCC = (decimal)dt.dgcc,
                             DNXP = (decimal)dt.dnxp,
                             DNDD = (decimal)dt.dndd,
                             DNCC = (decimal)dt.dncc,
                             SLRK = (decimal)dt.slrk,
                             KMCH = (decimal)dt.kmch,
                             KMDW = (decimal)dt.kmdw,
                             KMGH = (decimal)dt.kmgh,
                             KMDY = (decimal)dt.kmdy,
                             TKCH = (decimal)dt.tkch,
                             TKDW = (decimal)dt.tkdw,
                             TKGH = (decimal)dt.tkgh,
                             TKDY = (decimal)dt.tkdy,
                             L_DC = (decimal)dmc.LoaiDauMoID == 1 ? dmc.Linh : 0,
                             L_TL = (decimal)dmc.LoaiDauMoID == 3 ? dmc.Linh : 0,
                             L_GT = (decimal)dmc.LoaiDauMoID == 2 ? dmc.Linh : 0,
                             T_DC = (decimal)dmd.LoaiDauMoID == 1 ? dmd.TieuThu : 0,
                             T_TL = (decimal)dmd.LoaiDauMoID == 3 ? dmd.TieuThu : 0,
                             T_GT = (decimal)dmd.LoaiDauMoID == 2 ? dmd.TieuThu : 0,
                             C_DC = (decimal)dmd.LoaiDauMoID == 1 ? dmd.DinhMuc : 0,
                             C_TL = (decimal)dmd.LoaiDauMoID == 3 ? dmd.DinhMuc : 0,
                             C_GT = (decimal)dmd.LoaiDauMoID == 2 ? dmd.DinhMuc : 0,
                             SLRKM = (decimal)dt.slrkm,
                             SLRKN = (decimal)dt.slrkn,
                             CTY = (string)dt.cty,
                             DAY_LT = (DateTime)dt.daylt,
                             DTAU = (string)dt.dtau
                         };
            return await query1.OrderBy(x => x.SOCB).OrderBy(x => x.DAYCB).ToListAsync();
        }

        [HttpGet]
        [Route("YVGetXuatDTGA")]
        public async Task<ActionResult<IEnumerable<YVXuatDT>>> YVGetXuatDTGA(string maDV, int thangDT, int namDT)
        {
            var query = (from dt in _context.DoanThongGA
                         join cb in _context.CoBaoGA on dt.DoanThongID equals cb.CoBaoID
                         join ct in _context.DoanThongGACT on dt.DoanThongID equals ct.DoanThongID
                         where dt.ThangDT == thangDT && dt.NamDT == namDT && cb.DvcbID == maDV
                         select new
                         {
                             DoanThongID = (long)dt.DoanThongID,
                             socb = (string)cb.SoCB,
                             lmay = (string)cb.LoaiMayID,
                             dmay = (string)cb.DauMayID,
                             sdb1 = (string)cb.TaiXe1ID,
                             ten1 = (string)cb.TaiXe1Name,
                             sdb2 = (string)cb.PhoXe1ID,
                             ten2 = (string)cb.PhoXe1Name,
                             sdb3 = (string)cb.TaiXe2ID,
                             ten3 = (string)cb.TaiXe2Name,
                             mtau = (string)ct.MacTauID,
                             ctac = (short)ct.CongTacID,
                             tchat = (short)ct.TinhChatID,
                             mdoan=(string)ct.TuyenID,
                             kdoan = (string)ct.KhuDoan,
                             slbt = (decimal)cb.NLBanTruoc,
                             sllh = (decimal)cb.NLLinh,
                             slbs = (int)cb.NLBanSau,
                             sltt = (decimal)ct.TieuThu,
                             sltc = (decimal)ct.DinhMuc,
                             slpt = (decimal)cb.NLTrongDoan,
                             nlanh = (string)cb.TramNLID,
                             gaxp = (string)ct.GaXPName,
                             daycb = (DateTime)cb.NgayCB,
                             dgkb = (decimal)dt.DungKB,
                             dgdm = (decimal)ct.DungDM,
                             dgdn = (decimal)ct.DungDN,
                             dgkm = (decimal)ct.DungKhoDM,
                             dgkn = (decimal)ct.DungKhoDN,
                             dgqd = (decimal)ct.DungQD,
                             giqv = (decimal)ct.QuayVong,
                             gilh = (decimal)ct.LuHanh,
                             gidt = (decimal)ct.DonThuan,
                             dgxp = (decimal)ct.DungXP,
                             dgdd = (decimal)ct.DungDD,
                             dgcc = (decimal)ct.DungKT,
                             dnxp = (decimal)ct.DonXP,
                             dndd = (decimal)ct.DonDD,
                             dncc = (decimal)ct.DonKT,
                             slrk = (decimal)cb.SoLanRaKho,
                             kmch = (decimal)ct.KMChinh,
                             kmdw = (decimal)ct.KMDon,
                             kmgh = (decimal)ct.KMGhep,
                             kmdy = (decimal)ct.KMDay,
                             tkch = (decimal)ct.TKMChinh,
                             tkdw = (decimal)ct.TKMDon,
                             tkgh = (decimal)ct.TKMGhep,
                             tkdy = (decimal)ct.TKMDay,
                             slrkm = (decimal)ct.SLRKDM,
                             slrkn = (decimal)ct.SLRKDN,
                             cty = (string)ct.CongTyID,
                             daylt = (DateTime)ct.NgayXP,
                             dtau = (string)cb.SHDT
                         });


            var query1 = from dt in query
                         join dmc in _context.CoBaoGADM on dt.DoanThongID equals dmc.CoBaoID into dmcs
                         from dmc in dmcs.DefaultIfEmpty()
                         join dmd in _context.DoanThongGADM on dt.DoanThongID equals dmd.DoanThongID into dmds
                         from dmd in dmds.DefaultIfEmpty()
                         join ty in _context.Tuyen on dt.mdoan equals ty.TuyenID into tys
                         from ty in tys.DefaultIfEmpty()
                         select new YVXuatDT
                         {
                             SOCB = (string)dt.socb,
                             LMAY = (string)dt.lmay,
                             DMAY = (string)dt.dmay,
                             SDB1 = (string)dt.sdb1,
                             TEN1 = (string)dt.ten1,
                             SDB2 = (string)dt.sdb2,
                             TEN2 = (string)dt.ten2,
                             SDB3 = (string)dt.sdb3,
                             TEN3 = (string)dt.ten3,
                             MTAU = (string)dt.mtau,
                             CTAC = (short)dt.ctac,
                             TCHAT = (short)dt.tchat,
                             MDOAN =(string)ty.TuyenMap.ToString(),
                             KDOAN = (string)dt.kdoan,
                             SLBT = (decimal)dt.slbt,
                             SLLH = (decimal)dt.sllh,
                             SLBS = (int)dt.slbs,
                             SLTT = (decimal)dt.sltt,
                             SLTC = (decimal)dt.sltc,
                             SLPT = (decimal)dt.slpt,
                             NLANH = (string)dt.nlanh,
                             GAXP = (string)dt.gaxp,
                             DAYCB = (DateTime)dt.daycb,
                             DGKB = (decimal)dt.dgkb,
                             DGDM = (decimal)dt.dgdm,
                             DGDN = (decimal)dt.dgdn,
                             DGKM = (decimal)dt.dgkm,
                             DGKN = (decimal)dt.dgkn,
                             DGQD = (decimal)dt.dgqd,
                             GIQV = (decimal)dt.giqv,
                             GILH = (decimal)dt.gilh,
                             GIDT = (decimal)dt.gidt,
                             DGXP = (decimal)dt.dgxp,
                             DGDD = (decimal)dt.dgdd,
                             DGCC = (decimal)dt.dgcc,
                             DNXP = (decimal)dt.dnxp,
                             DNDD = (decimal)dt.dndd,
                             DNCC = (decimal)dt.dncc,
                             SLRK = (decimal)dt.slrk,
                             KMCH = (decimal)dt.kmch,
                             KMDW = (decimal)dt.kmdw,
                             KMGH = (decimal)dt.kmgh,
                             KMDY = (decimal)dt.kmdy,
                             TKCH = (decimal)dt.tkch,
                             TKDW = (decimal)dt.tkdw,
                             TKGH = (decimal)dt.tkgh,
                             TKDY = (decimal)dt.tkdy,
                             L_DC = (decimal)dmc.LoaiDauMoID == 1 ? dmc.Linh : 0,
                             L_TL = (decimal)dmc.LoaiDauMoID == 3 ? dmc.Linh : 0,
                             L_GT = (decimal)dmc.LoaiDauMoID == 2 ? dmc.Linh : 0,
                             T_DC = (decimal)dmd.LoaiDauMoID == 1 ? dmd.TieuThu : 0,
                             T_TL = (decimal)dmd.LoaiDauMoID == 3 ? dmd.TieuThu : 0,
                             T_GT = (decimal)dmd.LoaiDauMoID == 2 ? dmd.TieuThu : 0,
                             C_DC = (decimal)dmd.LoaiDauMoID == 1 ? dmd.DinhMuc : 0,
                             C_TL = (decimal)dmd.LoaiDauMoID == 3 ? dmd.DinhMuc : 0,
                             C_GT = (decimal)dmd.LoaiDauMoID == 2 ? dmd.DinhMuc : 0,
                             SLRKM = (decimal)dt.slrkm,
                             SLRKN = (decimal)dt.slrkn,
                             CTY = (string)dt.cty,
                             DAY_LT = (DateTime)dt.daylt,
                             DTAU = (string)dt.dtau                            
                         };
            return await query1.OrderBy(x => x.SOCB).OrderBy(x => x.DAYCB).ToListAsync();
        }
        #endregion
    }
}
