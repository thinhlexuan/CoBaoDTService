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
    public class SaiGonsController : ControllerBase
    {
        private readonly CoBaoDTContext _context;
        public SaiGonsController(CoBaoDTContext context)
        {
            _context = context;
        }

        #region KhuDoan
        [HttpGet]
        [Route("SGGetKhuDoanOBJ")]
        public async Task<ActionResult<string>> SGGetKhuDoanOBJ(string Tuyen, string GaXP, string GaKT)
        {
            string KhuDoan = string.Empty;
            var query = from item in _context.SG_KhuDoan
                        where item.CacGa.Contains(GaXP) && item.CacGa.Contains(GaKT)
                        select item;           
            var obj = await query.OrderBy(x => x.Km).FirstOrDefaultAsync();
            if (obj != null)
            {
                KhuDoan = obj.KhuDoanID;
                var kmGaXP = _context.LyTrinh.Where(x => x.GaID == int.Parse(GaXP) && x.TuyenID == Tuyen).FirstOrDefaultAsync().Result.Km;
                var kmGaKT = _context.LyTrinh.Where(x => x.GaID == int.Parse(GaKT) && x.TuyenID == Tuyen).FirstOrDefaultAsync().Result.Km;
                string chieu = kmGaXP < kmGaKT ? "ve": "di";
                KhuDoan += "-" + chieu;
            }            
            return KhuDoan;
        }

        [HttpGet]
        [Route("SGGetKhuDoan")]
        public async Task<ActionResult<IEnumerable<SGKhuDoan>>> SGGetKhuDoan(string KhuDoan)
        {
            var query = from item in _context.SG_KhuDoan select item;           
            if (!string.IsNullOrWhiteSpace(KhuDoan))
                query = query.Where(x => x.KhuDoanID.Contains(KhuDoan));
            return await query.OrderBy(x => x.KhuDoanID).ToListAsync();
        }
        [HttpPut]
        [Route("SGPutKhuDoan")]
        public async Task<ActionResult<SGKhuDoan>> SGPutKhuDoan(long id, SGKhuDoan sGKhuDoan)
        {
            sGKhuDoan.Modifydate = DateTime.Now;
            if (id != sGKhuDoan.ID)
            {
                return BadRequest();
            }
            _context.Entry(sGKhuDoan).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return sGKhuDoan;
        }

        [HttpPost]
        [Route("SGPostKhuDoan")]
        public async Task<ActionResult<SGKhuDoan>> SGPostKhuDoan(SGKhuDoan sGKhuDoan)
        {
            sGKhuDoan.Createddate = DateTime.Now;
            sGKhuDoan.Modifydate = sGKhuDoan.Createddate;
            var query = _context.SG_KhuDoan.Where(x => x.KhuDoanID == sGKhuDoan.KhuDoanID).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.SG_KhuDoan.Add(sGKhuDoan);
            await _context.SaveChangesAsync();

            return sGKhuDoan;
        }

        [HttpDelete]
        [Route("SGDeleteKhuDoan")]
        public async Task<ActionResult<SGKhuDoan>> SGDeleteKhuDoan(long id)
        {
            var sGKhuDoan = _context.SG_KhuDoan.Where(x => x.ID == id).FirstOrDefault();
            if (sGKhuDoan == null)
            {
                return NotFound();
            }
            _context.SG_KhuDoan.Remove(sGKhuDoan);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "SG_KhuDoan";
            nk.NoiDung = "Xóa khu đoạn: " + sGKhuDoan.KhuDoanID;
            nk.Createddate = DateTime.Now;
            nk.Createdby = sGKhuDoan.Modifyby;
            nk.CreatedName = sGKhuDoan.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return sGKhuDoan;
        }
        #endregion

        #region NLDinhMuc
        [HttpGet]
        [Route("SGGetTanMax")]
        public async Task<ActionResult<decimal>> SGGetTanMax(DateTime NgayHL, string LoaiMay)
        {
            decimal TanMax = 0;
            var query = (from item in _context.SG_HSTan
                         where item.NgayHL <= NgayHL && item.LoaiMayID == LoaiMay
                         orderby item.NgayHL descending
                         select item).Distinct();           
            var obj = await query.OrderByDescending(x => x.TanMax).FirstOrDefaultAsync();
            if (obj != null)
                TanMax = (decimal)obj.TanMax;
            return TanMax;
        }

        [HttpGet]
        [Route("SGGetNLDinhMucOBJ")]
        public async Task<ActionResult<SGNLDinhMuc>> SGGetNLDinhMucOBJ(DateTime NgayHL, string LoaiMay, string KhuDoan, string MacTau)
        {   
            var query = (from item in _context.SG_NLDinhMuc
                         where item.NgayHL <= NgayHL && item.LoaiMayID == LoaiMay && item.KhuDoan==KhuDoan
                         orderby item.NgayHL descending
                         select item).Distinct();
            var obj = await query.Where(x => x.LoaiTau.Contains(MacTau)).OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();
            if (obj != null)
            {
                obj.LoaiTau = MacTau;
                return obj;
            }
            int ktDau = char.IsDigit(MacTau[0]) ? 0 : 1;
            for (int i = ktDau; i < MacTau.Length; i++)               
            {
                string dauTau = MacTau.Substring(i, MacTau.Length - i);
                for (int j = 0; j < dauTau.Length; j++)
                {
                    string _loaiTau = "," + dauTau.Substring(0, dauTau.Length - j) + ",";
                    obj = await query.Where(x => x.LoaiTau.Contains(_loaiTau)).OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();
                    if (obj != null)
                    {
                        obj.LoaiTau = dauTau.Substring(0, dauTau.Length - j);
                        return obj;
                    }
                    else
                    {
                        string LoaiTau = ",*" + dauTau.Substring(0, dauTau.Length - j) + "*,";
                        obj = await query.Where(x => x.LoaiTau.Contains(LoaiTau)).OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();
                        if (obj != null)
                        {
                            obj.LoaiTau = dauTau.Substring(0, dauTau.Length - j);
                            return obj;
                        }
                    }
                }
            }
            return null;
        }

        [HttpGet]
        [Route("SGGetNLDinhMuc")]
        public async Task<ActionResult<IEnumerable<SGNLDinhMuc>>> SGGetNLDinhMuc(DateTime NgayBD, DateTime NgayKT, string LoaiMay, string KhuDoan,string LoaiTau)
        {
            var query = from item in _context.SG_NLDinhMuc where item.NgayHL >= NgayBD && item.NgayHL<NgayKT select item;
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay);
            if (!string.IsNullOrWhiteSpace(KhuDoan))
                query = query.Where(x => x.KhuDoan.Contains(KhuDoan));
            if (!string.IsNullOrWhiteSpace(LoaiTau))
                query = query.Where(x => x.LoaiTau.Contains(LoaiTau));
            return await query.OrderBy(x => x.LoaiMayID).ToListAsync();
        }
        [HttpPut]
        [Route("SGPutNLDinhMuc")]
        public async Task<ActionResult<SGNLDinhMuc>> SGPutNLDinhMuc(long id, SGNLDinhMuc sGNLDinhMuc)
        {
            sGNLDinhMuc.Modifydate = DateTime.Now;
            if (id != sGNLDinhMuc.ID)
            {
                return BadRequest();
            }
            _context.Entry(sGNLDinhMuc).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return sGNLDinhMuc;
        }

        [HttpPost]
        [Route("SGPostNLDinhMuc")]
        public async Task<ActionResult<SGNLDinhMuc>> SGPostNLDinhMuc(SGNLDinhMuc sGNLDinhMuc)
        {
            sGNLDinhMuc.Createddate = DateTime.Now;
            sGNLDinhMuc.Modifydate = sGNLDinhMuc.Createddate;
            var query = _context.SG_NLDinhMuc.Where(x => x.LoaiMayID == sGNLDinhMuc.LoaiMayID && x.KhuDoan == sGNLDinhMuc.KhuDoan 
            && x.LoaiTau==sGNLDinhMuc.LoaiTau && x.DinhMuc==sGNLDinhMuc.DinhMuc && x.NgayHL == sGNLDinhMuc.NgayHL).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.SG_NLDinhMuc.Add(sGNLDinhMuc);
            await _context.SaveChangesAsync();

            return sGNLDinhMuc;
        }

        [HttpDelete]
        [Route("SGDeleteNLDinhMuc")]
        public async Task<ActionResult<SGNLDinhMuc>> SGDeleteNLDinhMuc(long id)
        {
            var sGNLDinhMuc = _context.SG_NLDinhMuc.Where(x => x.ID == id).FirstOrDefault();
            if (sGNLDinhMuc == null)
            {
                return NotFound();
            }
            _context.SG_NLDinhMuc.Remove(sGNLDinhMuc);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "SG_NLDinhMuc";
            nk.NoiDung = "Xóa NL định mức: " + sGNLDinhMuc.KhuDoan + ". Loại máy: " + sGNLDinhMuc.LoaiMayID;
            nk.Createddate = DateTime.Now;
            nk.Createdby = sGNLDinhMuc.Modifyby;
            nk.CreatedName = sGNLDinhMuc.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return sGNLDinhMuc;
        }
        #endregion

        #region HSTan
       
        [HttpGet]
        [Route("SGGetHSTanOBJ")]
        public async Task<ActionResult<SGHSTan>> SGGetHSTanOBJ(DateTime NgayHL, string LoaiMay, decimal Tan, string DonVi)
        {
            var query = (from item in _context.SG_HSTan
                         where item.NgayHL <= NgayHL && item.LoaiMayID == LoaiMay && item.TanMin<=Tan && item.TanMax>=Tan
                         orderby item.NgayHL descending
                         select item).Distinct();
            if (!string.IsNullOrWhiteSpace(DonVi))
                query = query.Where(x => x.DonVi.Contains(DonVi));           
            return await query.OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();
        }

        [HttpGet]
        [Route("SGGetHSTan")]
        public async Task<ActionResult<IEnumerable<SGHSTan>>> SGGetHSTan(DateTime NgayHL, string LoaiMay)
        {
            var query = (from item in _context.SG_HSTan where item.NgayHL <= NgayHL orderby item.NgayHL descending select item).Distinct();
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay);            
            return await query.OrderBy(x => x.LoaiMayID).ToListAsync();
        }
        [HttpPut]
        [Route("SGPutHSTan")]
        public async Task<ActionResult<SGHSTan>> SGPutHSTan(long id, SGHSTan sGHSTan)
        {
            sGHSTan.Modifydate = DateTime.Now;
            if (id != sGHSTan.ID)
            {
                return BadRequest();
            }
            _context.Entry(sGHSTan).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return sGHSTan;
        }

        [HttpPost]
        [Route("SGPostHSTan")]
        public async Task<ActionResult<SGHSTan>> SGPostHSTan(SGHSTan sGHSTan)
        {
            sGHSTan.Createddate = DateTime.Now;
            sGHSTan.Modifydate = sGHSTan.Createddate;
            var query = _context.SG_HSTan.Where(x => x.LoaiMayID == sGHSTan.LoaiMayID && x.TanMin==sGHSTan.TanMin && x.TanMax==sGHSTan.TanMax && x.NgayHL == sGHSTan.NgayHL).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.SG_HSTan.Add(sGHSTan);
            await _context.SaveChangesAsync();

            return sGHSTan;
        }

        [HttpDelete]
        [Route("SGDeleteHSTan")]
        public async Task<ActionResult<SGHSTan>> SGDeleteHSTan(long id)
        {
            var sGHSTan = _context.SG_HSTan.Where(x => x.ID == id).FirstOrDefault();
            if (sGHSTan == null)
            {
                return NotFound();
            }
            _context.SG_HSTan.Remove(sGHSTan);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "SG_HSTan";
            nk.NoiDung = "Xóa hệ số tấn. Loại máy: " + sGHSTan.LoaiMayID;
            nk.Createddate = DateTime.Now;
            nk.Createdby = sGHSTan.Modifyby;
            nk.CreatedName = sGHSTan.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return sGHSTan;
        }
        #endregion

        #region XuatDT
        [HttpGet]
        [Route("SGGetXuatDT")]
        public async Task<ActionResult<IEnumerable<SGXuatDT>>> SGGetXuatDT(string maDV,int thangDT, int namDT)
        {
            var query = from dt in _context.DoanThong
                        join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                        join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                        where dt.ThangDT == thangDT && dt.NamDT == namDT && cb.DvcbID==maDV
                        select new SGXuatDT
                        {
                            socb = (long)dt.DoanThongID,
                            sohieucb = (string)cb.SoCB,                           
                            lmay = (string)cb.LoaiMayID,
                            dmay = (string)cb.DauMayID,
                            madv = (string)ct.CongTyID,
                            maso =(string)cb.MaCB,
                            sdb1 = (string)cb.TaiXe1ID,
                            ten1 = (string)cb.TaiXe1Name,
                            sdb2 = (string)cb.PhoXe1ID,
                            ten2 = (string)cb.PhoXe1Name,
                            sdb3 = (string)cb.TaiXe2ID,
                            ten3 = (string)cb.TaiXe2Name,
                            sdb4 = (string)cb.PhoXe2ID,
                            ten4 = (string)cb.PhoXe2Name,
                            sdb5 = (string)cb.TaiXe3ID,
                            ten5 = (string)cb.TaiXe3Name,
                            sdb6 = (string)cb.PhoXe3ID,
                            ten6 = (string)cb.PhoXe3Name,
                            tau = (string)ct.MacTauID,
                            ctac = (short)ct.CongTacID,
                            tchat = (short)ct.TinhChatID,
                            kdoan = (string)ct.KhuDoan,
                            slbt = (decimal)cb.NLBanTruoc,
                            sllh = (decimal)cb.NLLinh,
                            slbs = (int)cb.NLBanSau,
                            slsd = (decimal)cb.NLTrongDoan,
                            sltt = (decimal)ct.TieuThu,
                            sltc = (decimal)ct.DinhMuc,
                            nlanh = (string)cb.TramNLID,
                            gaxp = (string)ct.GaXPName,
                            gakt=(string)ct.GaKTName,
                            daycb = (DateTime)cb.NgayCB,
                            glb= (string)cb.LenBan.ToString("HHmm"),
                            gnm = (string)cb.NhanMay.ToString("HHmm"),
                            grk = (string)cb.RaKho.ToString("HHmm"),
                            gvk = (string)cb.VaoKho.ToString("HHmm"),
                            ggm = (string)cb.GiaoMay.ToString("HHmm"),
                            gxb = (string)cb.XuongBan.ToString("HHmm"),
                            dgkb = (decimal)dt.DungKB,
                            dgdm = (decimal)ct.DungDM,
                            dgdn = (decimal)ct.DungDN,
                            dgkm = (decimal)ct.DungKhoDM,
                            dgkn = (decimal)ct.DungKhoDN,
                            dgqd = (decimal)ct.DungQD,
                            giqv = (decimal)ct.QuayVong,
                            gilh = (decimal)ct.LuHanh,
                            gidt = (decimal)ct.DonThuan,
                            dgxp=(decimal)ct.DungXP,
                            dgdd=(decimal)ct.DungDD,
                            dgcc=(decimal)ct.DungKT,
                            dnxp=(decimal)ct.DonXP,
                            dndd=(decimal)ct.DonDD,
                            dncc=(decimal)ct.DonKT,
                            slrk=(decimal)cb.SoLanRaKho,
                            kmch=(decimal)ct.KMChinh,
                            kmdw=(decimal)ct.KMDon,
                            kmgh=(decimal)ct.KMGhep,
                            kmdy=(decimal)ct.KMDay,
                            tkch=(decimal)ct.TKMChinh,
                            tkdw=(decimal)ct.TKMDon,
                            tkgh=(decimal)ct.TKMGhep,
                            tkdy=(decimal)ct.TKMDay,
                            slrkm=(decimal)ct.SLRKDM,
                            slrkn=(decimal)ct.SLRKDN,
                            ThangDT=(int)dt.ThangDT,
                            NamDT = (int)dt.NamDT
                        };            
            return await query.OrderBy(x=>x.socb).OrderBy(x=>x.daycb).ToListAsync();
        }

        [HttpGet]
        [Route("SGGetXuatDTGA")]
        public async Task<ActionResult<IEnumerable<SGXuatDT>>> SGGetXuatDTGA(string maDV, int thangDT, int namDT)
        {
            var query = from dt in _context.DoanThongGA
                        join cb in _context.CoBaoGA on dt.DoanThongID equals cb.CoBaoID
                        join ct in _context.DoanThongGACT on dt.DoanThongID equals ct.DoanThongID
                        where dt.ThangDT == thangDT && dt.NamDT == namDT && cb.DvcbID == maDV
                        select new SGXuatDT
                        {
                            socb = (long)dt.DoanThongID,
                            sohieucb = (string)cb.SoCB,
                            lmay = (string)cb.LoaiMayID,
                            dmay = (string)cb.DauMayID,
                            madv = (string)ct.CongTyID,
                            maso = (string)cb.MaCB,
                            sdb1 = (string)cb.TaiXe1ID,
                            ten1 = (string)cb.TaiXe1Name,
                            sdb2 = (string)cb.PhoXe1ID,
                            ten2 = (string)cb.PhoXe1Name,
                            sdb3 = (string)cb.TaiXe2ID,
                            ten3 = (string)cb.TaiXe2Name,
                            sdb4 = (string)cb.PhoXe2ID,
                            ten4 = (string)cb.PhoXe2Name,
                            sdb5 = (string)cb.TaiXe3ID,
                            ten5 = (string)cb.TaiXe3Name,
                            sdb6 = (string)cb.PhoXe3ID,
                            ten6 = (string)cb.PhoXe3Name,
                            tau = (string)ct.MacTauID,
                            ctac = (short)ct.CongTacID,
                            tchat = (short)ct.TinhChatID,
                            kdoan = (string)ct.KhuDoan,
                            slbt = (decimal)cb.NLBanTruoc,
                            sllh = (decimal)cb.NLLinh,
                            slbs = (int)cb.NLBanSau,
                            slsd = (decimal)cb.NLTrongDoan,
                            sltt = (decimal)ct.TieuThu,
                            sltc = (decimal)ct.DinhMuc,
                            nlanh = (string)cb.TramNLID,
                            gaxp = (string)ct.GaXPName,
                            gakt = (string)ct.GaKTName,
                            daycb = (DateTime)cb.NgayCB,
                            glb = (string)cb.LenBan.ToString("HHmm"),
                            gnm = (string)cb.NhanMay.ToString("HHmm"),
                            grk = (string)cb.RaKho.ToString("HHmm"),
                            gvk = (string)cb.VaoKho.ToString("HHmm"),
                            ggm = (string)cb.GiaoMay.ToString("HHmm"),
                            gxb = (string)cb.XuongBan.ToString("HHmm"),
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
                            ThangDT = (int)dt.ThangDT,
                            NamDT = (int)dt.NamDT
                        };
            return await query.OrderBy(x => x.socb).OrderBy(x => x.daycb).ToListAsync();
        }
        #endregion
    }
}
