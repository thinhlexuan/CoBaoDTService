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
    public class VinhsController : ControllerBase
    {
        private readonly CoBaoDTContext _context;
        public VinhsController(CoBaoDTContext context)
        {
            _context = context;
        }

        #region KhuDoan
        [HttpGet]
        [Route("VIGetKhuDoanOBJ")]
        public async Task<ActionResult<string>> VIGetKhuDoanOBJ(string Tuyen, string GaXP, string GaKT,string TinhChat)
        {
            string KhuDoan = string.Empty;
            var query = from item in _context.VI_KhuDoan
                        where item.CacGa.Contains(GaXP) && item.CacGa.Contains(GaKT)
                        select item;
            if (!string.IsNullOrWhiteSpace(TinhChat))
                query = query.Where(x => x.TinhChat.Contains(TinhChat));
            else
                query = query.Where(x => string.IsNullOrEmpty(x.TinhChat));
            var obj = await query.OrderBy(x => x.Km).FirstOrDefaultAsync();
            if (obj != null)
            {
                KhuDoan = obj.KhuDoanID;
                //var kmGaXP = _context.LyTrinh.Where(x => x.GaID == int.Parse(GaXP) && x.TuyenID == Tuyen).FirstOrDefaultAsync().Result.Km;
                //var kmGaKT = _context.LyTrinh.Where(x => x.GaID == int.Parse(GaKT) && x.TuyenID == Tuyen).FirstOrDefaultAsync().Result.Km;
                //string chieu = kmGaXP < kmGaKT ? "di" : "ve";
                //KhuDoan += "-" + chieu;
            }           
            return KhuDoan;
        }

        [HttpGet]
        [Route("VIGetKhuDoan")]
        public async Task<ActionResult<IEnumerable<VIKhuDoan>>> VIGetKhuDoan(string KhuDoan)
        {
            var query = from item in _context.VI_KhuDoan select item;           
            if (!string.IsNullOrWhiteSpace(KhuDoan))
                query = query.Where(x => x.KhuDoanID.Contains(KhuDoan));
            return await query.OrderBy(x => x.KhuDoanID).ToListAsync();
        }
        [HttpPut]
        [Route("VIPutKhuDoan")]
        public async Task<ActionResult<VIKhuDoan>> VIPutKhuDoan(long id, VIKhuDoan vIKhuDoan)
        {
            vIKhuDoan.Modifydate = DateTime.Now;
            if (id != vIKhuDoan.ID)
            {
                return BadRequest();
            }
            _context.Entry(vIKhuDoan).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return vIKhuDoan;
        }

        [HttpPost]
        [Route("VIPostKhuDoan")]
        public async Task<ActionResult<VIKhuDoan>> VIPostKhuDoan(VIKhuDoan vIKhuDoan)
        {
            vIKhuDoan.Createddate = DateTime.Now;
            vIKhuDoan.Modifydate = vIKhuDoan.Createddate;
            var query = _context.VI_KhuDoan.Where(x => x.KhuDoanID == vIKhuDoan.KhuDoanID).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.VI_KhuDoan.Add(vIKhuDoan);
            await _context.SaveChangesAsync();

            return vIKhuDoan;
        }

        [HttpDelete]
        [Route("VIDeleteKhuDoan")]
        public async Task<ActionResult<VIKhuDoan>> VIDeleteKhuDoan(long id)
        {
            var vIKhuDoan = _context.VI_KhuDoan.Where(x => x.ID == id).FirstOrDefault();
            if (vIKhuDoan == null)
            {
                return NotFound();
            }
            _context.VI_KhuDoan.Remove(vIKhuDoan);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "VI_KhuDoan";
            nk.NoiDung = "Xóa khu đoạn: " + vIKhuDoan.KhuDoanID;
            nk.Createddate = DateTime.Now;
            nk.Createdby = vIKhuDoan.Modifyby;
            nk.CreatedName = vIKhuDoan.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return vIKhuDoan;
        }
        #endregion

        #region NLDinhMuc
        [HttpGet]
        [Route("VIGetTanMax")]
        public async Task<ActionResult<decimal>> SGGetTanMax(DateTime NgayHL, string LoaiMay)
        {
            decimal TanMax = 0;
            var query = (from item in _context.VI_HSTan
                         where item.NgayHL <= NgayHL && item.LoaiMayID == LoaiMay
                         orderby item.NgayHL descending
                         select item).Distinct();
            var obj = await query.OrderByDescending(x => x.TanMax).FirstOrDefaultAsync();
            if (obj != null)
                TanMax = (decimal)obj.TanMax;
            return TanMax;
        }

        [HttpGet]
        [Route("VIGetNLDinhMucOBJ")]
        public async Task<ActionResult<VINLDinhMuc>> VIGetNLDinhMucOBJ(DateTime NgayHL, string LoaiMay, string KhuDoan, string MacTau)
        {
            var query = (from item in _context.VI_NLDinhMuc
                         where item.NgayHL <= NgayHL && item.LoaiMayID == LoaiMay && item.KhuDoan == KhuDoan
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
                        _loaiTau = "," + dauTau.Substring(0, dauTau.Length - j) + "*,";
                        obj = await query.Where(x => x.LoaiTau.Contains(_loaiTau)).OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();
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
        [Route("VIGetNLDinhMuc")]
        public async Task<ActionResult<IEnumerable<VINLDinhMuc>>> VIGetNLDinhMuc(DateTime NgayHL, string LoaiMay, string KhuDoan, string LoaiTau)
        {
            var query = (from item in _context.VI_NLDinhMuc where item.NgayHL <= NgayHL orderby item.NgayHL descending select item).Distinct();
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay);
            if (!string.IsNullOrWhiteSpace(KhuDoan))
                query = query.Where(x => x.KhuDoan.Contains(KhuDoan));
            if (!string.IsNullOrWhiteSpace(LoaiTau))
                query = query.Where(x => x.LoaiTau.Contains(LoaiTau));
            return await query.OrderBy(x => x.LoaiMayID).ToListAsync();
        }
        [HttpPut]
        [Route("VIPutNLDinhMuc")]
        public async Task<ActionResult<VINLDinhMuc>> VIPutNLDinhMuc(long id, VINLDinhMuc vINLDinhMuc)
        {
            vINLDinhMuc.Modifydate = DateTime.Now;
            if (id != vINLDinhMuc.ID)
            {
                return BadRequest();
            }
            _context.Entry(vINLDinhMuc).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return vINLDinhMuc;
        }

        [HttpPost]
        [Route("VIPostNLDinhMuc")]
        public async Task<ActionResult<VINLDinhMuc>> VIPostNLDinhMuc(VINLDinhMuc vINLDinhMuc)
        {
            vINLDinhMuc.Createddate = DateTime.Now;
            vINLDinhMuc.Modifydate = vINLDinhMuc.Createddate;
            var query = _context.SG_NLDinhMuc.Where(x => x.LoaiMayID == vINLDinhMuc.LoaiMayID && x.KhuDoan == vINLDinhMuc.KhuDoan && x.NgayHL == vINLDinhMuc.NgayHL).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.VI_NLDinhMuc.Add(vINLDinhMuc);
            await _context.SaveChangesAsync();

            return vINLDinhMuc;
        }

        [HttpDelete]
        [Route("VIDeleteNLDinhMuc")]
        public async Task<ActionResult<VINLDinhMuc>> VIDeleteNLDinhMuc(long id)
        {
            var vINLDinhMuc = _context.VI_NLDinhMuc.Where(x => x.ID == id).FirstOrDefault();
            if (vINLDinhMuc == null)
            {
                return NotFound();
            }
            _context.VI_NLDinhMuc.Remove(vINLDinhMuc);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "VI_NLDinhMuc";
            nk.NoiDung = "Xóa NL định mức: " + vINLDinhMuc.KhuDoan + ". Loại máy: " + vINLDinhMuc.LoaiMayID;
            nk.Createddate = DateTime.Now;
            nk.Createdby = vINLDinhMuc.Modifyby;
            nk.CreatedName = vINLDinhMuc.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return vINLDinhMuc;
        }
        #endregion

        #region NLDDinhMuc        

        [HttpGet]
        [Route("VIGetNLDDinhMucOBJ")]
        public async Task<ActionResult<VINLDDinhMuc>> VIGetNLDDinhMucOBJ(DateTime NgayHL, string MayChinh, string MayPhu, string KhuDoan)
        {
            var query = (from item in _context.VI_NLDDinhMuc
                         where item.NgayHL <= NgayHL && item.MayChinhID == MayChinh && item.MayPhuID==MayPhu && item.KhuDoan == KhuDoan
                         orderby item.NgayHL descending
                         select item).Distinct();
            var obj = await query.OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();
            if (obj != null)
            {
               
                return obj;
            }            
            return null;
        }
        [HttpGet]
        [Route("VIGetNLDDinhMuc")]
        public async Task<ActionResult<IEnumerable<VINLDDinhMuc>>> VIGetNLDDinhMuc(DateTime NgayHL, string MayChinh, string MayPhu, string KhuDoan)
        {
            var query = (from item in _context.VI_NLDDinhMuc where item.NgayHL <= NgayHL orderby item.NgayHL descending select item).Distinct();
            if (MayChinh != "ALL")
                query = query.Where(x => x.MayChinhID == MayChinh);
            if (MayPhu != "ALL")
                query = query.Where(x => x.MayPhuID == MayPhu);
            if (!string.IsNullOrWhiteSpace(KhuDoan))
                query = query.Where(x => x.KhuDoan.Contains(KhuDoan));          
            return await query.OrderBy(x => x.MayChinhID).ToListAsync();
        }
        [HttpPut]
        [Route("VIPutNLDDinhMuc")]
        public async Task<ActionResult<VINLDDinhMuc>> VIPutNLDDinhMuc(long id, VINLDDinhMuc vINLDDinhMuc)
        {
            vINLDDinhMuc.Modifydate = DateTime.Now;
            if (id != vINLDDinhMuc.ID)
            {
                return BadRequest();
            }
            _context.Entry(vINLDDinhMuc).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return vINLDDinhMuc;
        }

        [HttpPost]
        [Route("VIPostNLDDinhMuc")]
        public async Task<ActionResult<VINLDDinhMuc>> VIPostNLDDinhMuc(VINLDDinhMuc vINLDDinhMuc)
        {
            vINLDDinhMuc.Createddate = DateTime.Now;
            vINLDDinhMuc.Modifydate = vINLDDinhMuc.Createddate;
            var query = _context.VI_NLDDinhMuc.Where(x => x.MayChinhID == vINLDDinhMuc.MayChinhID && x.MayPhuID==vINLDDinhMuc.MayPhuID 
            && x.KhuDoan == vINLDDinhMuc.KhuDoan && x.NgayHL == vINLDDinhMuc.NgayHL).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.VI_NLDDinhMuc.Add(vINLDDinhMuc);
            await _context.SaveChangesAsync();
            return vINLDDinhMuc;
        }

        [HttpDelete]
        [Route("VIDeleteNLDDinhMuc")]
        public async Task<ActionResult<VINLDDinhMuc>> VIDeleteNLDDinhMuc(long id)
        {
            var vINLDDinhMuc = _context.VI_NLDDinhMuc.Where(x => x.ID == id).FirstOrDefault();
            if (vINLDDinhMuc == null)
            {
                return NotFound();
            }
            _context.VI_NLDDinhMuc.Remove(vINLDDinhMuc);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "VI_NLDDinhMuc";
            nk.NoiDung = "Xóa NL đẩy định mức: " + vINLDDinhMuc.KhuDoan + ". Máy chính: " + vINLDDinhMuc.MayChinhID + ". Máy phụ: " + vINLDDinhMuc.MayPhuID;
            nk.Createddate = DateTime.Now;
            nk.Createdby = vINLDDinhMuc.Modifyby;
            nk.CreatedName = vINLDDinhMuc.ModifyName;
            _context.NhatKy.Add(nk);
            await _context.SaveChangesAsync();
            return vINLDDinhMuc;
        }
        #endregion

        #region HSTan

        [HttpGet]
        [Route("VIGetHSTanOBJ")]
        public async Task<ActionResult<VIHSTan>> VIGetHSTanOBJ(DateTime NgayHL, string LoaiMay, decimal Tan)
        {
            var query = (from item in _context.VI_HSTan
                         where item.NgayHL <= NgayHL && item.LoaiMayID == LoaiMay && item.TanMin <= Tan && item.TanMax >= Tan
                         orderby item.NgayHL descending
                         select item).Distinct();           
            return await query.OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();
        }

        [HttpGet]
        [Route("VIGetHSTan")]
        public async Task<ActionResult<IEnumerable<VIHSTan>>> VIGetHSTan(DateTime NgayHL, string LoaiMay)
        {
            var query = (from item in _context.VI_HSTan where item.NgayHL <= NgayHL orderby item.NgayHL descending select item).Distinct();
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay);
            return await query.OrderBy(x => x.LoaiMayID).ToListAsync();
        }
        [HttpPut]
        [Route("VIPutHSTan")]
        public async Task<ActionResult<VIHSTan>> VIPutHSTan(long id, VIHSTan vIHSTan)
        {
            vIHSTan.Modifydate = DateTime.Now;
            if (id != vIHSTan.ID)
            {
                return BadRequest();
            }
            _context.Entry(vIHSTan).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return vIHSTan;
        }

        [HttpPost]
        [Route("VIPostHSTan")]
        public async Task<ActionResult<VIHSTan>> VIPostHSTan(VIHSTan vIHSTan)
        {
            vIHSTan.Createddate = DateTime.Now;
            vIHSTan.Modifydate = vIHSTan.Createddate;
            var query = _context.VI_HSTan.Where(x => x.LoaiMayID == vIHSTan.LoaiMayID && x.NgayHL == vIHSTan.NgayHL && x.TanMin==vIHSTan.TanMin && x.TanMax==vIHSTan.TanMax).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.VI_HSTan.Add(vIHSTan);
            await _context.SaveChangesAsync();

            return vIHSTan;
        }

        [HttpDelete]
        [Route("VIDeleteHSTan")]
        public async Task<ActionResult<VIHSTan>> VIDeleteHSTan(long id)
        {
            var vIHSTan = _context.VI_HSTan.Where(x => x.ID == id).FirstOrDefault();
            if (vIHSTan == null)
            {
                return NotFound();
            }
            _context.VI_HSTan.Remove(vIHSTan);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "VI_HSTan";
            nk.NoiDung = "Xóa hệ số tấn. Loại máy: " + vIHSTan.LoaiMayID;
            nk.Createddate = DateTime.Now;
            nk.Createdby = vIHSTan.Modifyby;
            nk.CreatedName = vIHSTan.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return vIHSTan;
        }
        #endregion

        #region DMDinhMuc
        [HttpGet]
        [Route("VIGetDMDinhMuc")]
        public async Task<ActionResult<IEnumerable<VIDMDinhMuc>>> VIGetDMDinhMuc(DateTime NgayHL, string LoaiMay, short DauMoID)
        {
            var query = (from item in _context.VI_DMDinhMuc where item.NgayHL <= NgayHL orderby item.NgayHL descending select item).Distinct();
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay);
            if (DauMoID > 0)
                query = query.Where(x => x.DauMoID == DauMoID);
            return await query.OrderBy(x => x.LoaiMayID).ToListAsync();
        }
        [HttpPut]
        [Route("VIPutDMDinhMuc")]
        public async Task<ActionResult<VIDMDinhMuc>> VIPutDMDinhMuc(long id, VIDMDinhMuc vIDMDinhMuc)
        {
            vIDMDinhMuc.Modifydate = DateTime.Now;
            if (id != vIDMDinhMuc.ID)
            {
                return BadRequest();
            }
            _context.Entry(vIDMDinhMuc).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return vIDMDinhMuc;
        }

        [HttpPost]
        [Route("VIPostDMDinhMuc")]
        public async Task<ActionResult<VIDMDinhMuc>> VIPostDMDinhMuc(VIDMDinhMuc vIDMDinhMuc)
        {
            vIDMDinhMuc.Createddate = DateTime.Now;
            vIDMDinhMuc.Modifydate = vIDMDinhMuc.Createddate;
            var query = _context.VI_DMDinhMuc.Where(x => x.LoaiMayID == vIDMDinhMuc.LoaiMayID && x.DauMoID == vIDMDinhMuc.DauMoID && x.NgayHL == vIDMDinhMuc.NgayHL).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.VI_DMDinhMuc.Add(vIDMDinhMuc);
            await _context.SaveChangesAsync();

            return vIDMDinhMuc;
        }

        [HttpDelete]
        [Route("VIDeleteDMDinhMuc")]
        public async Task<ActionResult<VIDMDinhMuc>> VIDeleteDMDinhMuc(long id)
        {
            var vIDMDinhMuc = _context.VI_DMDinhMuc.Where(x => x.ID == id).FirstOrDefault();
            if (vIDMDinhMuc == null)
            {
                return NotFound();
            }
            _context.VI_DMDinhMuc.Remove(vIDMDinhMuc);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "VI_DMDinhMuc";
            nk.NoiDung = "Xóa dầu mỡ định mức: " + vIDMDinhMuc.DauMoName + ". Loại máy: " + vIDMDinhMuc.LoaiMayID;
            nk.Createddate = DateTime.Now;
            nk.Createdby = vIDMDinhMuc.Modifyby;
            nk.CreatedName = vIDMDinhMuc.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return vIDMDinhMuc;
        }
        #endregion

        #region XuatDT
        [HttpGet]
        [Route("VIGetXuatDT")]
        public async Task<ActionResult<IEnumerable<VIXuatDT>>> VIGetXuatDT(string maDV, int thangDT, int namDT)
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
                             sdb4 = (string)cb.PhoXe2ID,
                             ten4 = (string)cb.PhoXe2Name,
                             tau = (string)ct.MacTauID,
                             cty=(string)ct.CongTyID,
                             ctac = (short)ct.CongTacID,
                             tchat = (short)ct.TinhChatID,
                             kdoan = (string)ct.KhuDoan,
                             slbt = (decimal)cb.NLBanTruoc,
                             sllh = (decimal)cb.NLLinh,
                             slbs = (int)cb.NLBanSau,
                             sltt = (decimal)ct.TieuThu,
                             sltc = (decimal)ct.DinhMuc,
                             slpt = (decimal)cb.NLTrongDoan,
                             kho1 = (string)cb.TramNLID,
                             gaxp = (string)ct.GaXPName,
                             daycb = (DateTime)cb.NgayCB,                             
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
                             dtau = (string)cb.SHDT,
                             mghep = (string)ct.MayGhepID,
                             ThangDT = (int)dt.ThangDT,
                             NamDT = (int)dt.NamDT
                         });


            var query1 = from dt in query
                         join bm in _context.CoBaoDM on dt.DoanThongID equals bm.CoBaoID into bms
                         from bm in bms.DefaultIfEmpty()
                         join dm in _context.DoanThongDM on dt.DoanThongID equals dm.DoanThongID into dms
                         from dm in dms.DefaultIfEmpty()
                         select new VIXuatDT
                         {
                             socb = (string)dt.socb,
                             lmay = (string)dt.lmay,
                             dmay = (string)dt.dmay,
                             sdb1 = (string)dt.sdb1,
                             ten1 = (string)dt.ten1,
                             sdb2 = (string)dt.sdb2,
                             ten2 = (string)dt.ten2,
                             sdb3 = (string)dt.sdb3,
                             ten3 = (string)dt.ten3,
                             sdb4 = (string)dt.sdb4,
                             ten4 = (string)dt.ten4,
                             tau = (string)dt.tau,
                             cty=(string)dt.cty,
                             ctac = (short)dt.ctac,
                             tchat = (short)dt.tchat,
                             kdoan = (string)dt.kdoan,
                             slbt = (decimal)dt.slbt,                             
                             slbs = (int)dt.slbs,
                             sltt = (decimal)dt.sltt,
                             sltc = (decimal)dt.sltc,
                             slpt = (decimal)dt.slpt,
                             kho1 = (string)dt.kho1,
                             gaxp = (string)dt.gaxp,
                             daycb = (DateTime)dt.daycb,                             
                             dgdm = (decimal)dt.dgdm,
                             dgdn = (decimal)dt.dgdn,
                             dgkm = (decimal)dt.dgkm,
                             dgkn = (decimal)dt.dgkn,
                             dgqd = (decimal)dt.dgqd,
                             giqv = (decimal)dt.giqv,
                             gilh = (decimal)dt.gilh,
                             gidt = (decimal)dt.gidt,
                             dgxp = (decimal)dt.dgxp,
                             dgdd = (decimal)dt.dgdd,
                             dgcc = (decimal)dt.dgcc,
                             dnxp = (decimal)dt.dnxp,
                             dndd = (decimal)dt.dndd,
                             dncc = (decimal)dt.dncc,
                             slrk = (decimal)dt.slrk,
                             kmch = (decimal)dt.kmch,
                             kmdw = (decimal)dt.kmdw,
                             kmgh = (decimal)dt.kmgh,
                             kmdy = (decimal)dt.kmdy,
                             tkch = (decimal)dt.tkch,
                             tkdw = (decimal)dt.tkdw,
                             tkgh = (decimal)dt.tkgh,
                             tkdy = (decimal)dt.tkdy,                            
                             slrkm = (decimal)dt.slrkm,
                             slrkn = (decimal)dt.slrkn,                            
                             ThangDT = (int)dt.ThangDT,
                             NamDT = (int)dt.NamDT
                         };
            return await query1.OrderBy(x => x.socb).OrderBy(x => x.daycb).ToListAsync();
        }

        [HttpGet]
        [Route("VIGetXuatDTGA")]
        public async Task<ActionResult<IEnumerable<VIXuatDT>>> VIGetXuatDTGA(string maDV, int thangDT, int namDT)
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
                              sdb4 = (string)cb.PhoXe2ID,
                              ten4 = (string)cb.PhoXe2Name,
                              tau = (string)ct.MacTauID,
                              cty = (string)ct.CongTyID,
                              ctac = (short)ct.CongTacID,
                              tchat = (short)ct.TinhChatID,
                              kdoan = (string)ct.KhuDoan,
                              slbt = (decimal)cb.NLBanTruoc,
                              sllh = (decimal)cb.NLLinh,
                              slbs = (int)cb.NLBanSau,
                              sltt = (decimal)ct.TieuThu,
                              sltc = (decimal)ct.DinhMuc,
                              slpt = (decimal)cb.NLTrongDoan,
                              kho1 = (string)cb.TramNLID,
                              gaxp = (string)ct.GaXPName,
                              daycb = (DateTime)cb.NgayCB,
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
                              dtau = (string)cb.SHDT,
                              mghep = (string)ct.MayGhepID,
                              ThangDT = (int)dt.ThangDT,
                              NamDT = (int)dt.NamDT
                          });


            var query1 = from dt in query
                         join bm in _context.CoBaoDM on dt.DoanThongID equals bm.CoBaoID into bms
                         from bm in bms.DefaultIfEmpty()
                         join dm in _context.DoanThongDM on dt.DoanThongID equals dm.DoanThongID into dms
                         from dm in dms.DefaultIfEmpty()
                         select new VIXuatDT
                         {
                             socb = (string)dt.socb,
                             lmay = (string)dt.lmay,
                             dmay = (string)dt.dmay,
                             sdb1 = (string)dt.sdb1,
                             ten1 = (string)dt.ten1,
                             sdb2 = (string)dt.sdb2,
                             ten2 = (string)dt.ten2,
                             sdb3 = (string)dt.sdb3,
                             ten3 = (string)dt.ten3,
                             sdb4 = (string)dt.sdb4,
                             ten4 = (string)dt.ten4,
                             tau = (string)dt.tau,
                             cty = (string)dt.cty,
                             ctac = (short)dt.ctac,
                             tchat = (short)dt.tchat,
                             kdoan = (string)dt.kdoan,
                             slbt = (decimal)dt.slbt,
                             slbs = (int)dt.slbs,
                             sltt = (decimal)dt.sltt,
                             sltc = (decimal)dt.sltc,
                             slpt = (decimal)dt.slpt,
                             kho1 = (string)dt.kho1,
                             gaxp = (string)dt.gaxp,
                             daycb = (DateTime)dt.daycb,
                             dgdm = (decimal)dt.dgdm,
                             dgdn = (decimal)dt.dgdn,
                             dgkm = (decimal)dt.dgkm,
                             dgkn = (decimal)dt.dgkn,
                             dgqd = (decimal)dt.dgqd,
                             giqv = (decimal)dt.giqv,
                             gilh = (decimal)dt.gilh,
                             gidt = (decimal)dt.gidt,
                             dgxp = (decimal)dt.dgxp,
                             dgdd = (decimal)dt.dgdd,
                             dgcc = (decimal)dt.dgcc,
                             dnxp = (decimal)dt.dnxp,
                             dndd = (decimal)dt.dndd,
                             dncc = (decimal)dt.dncc,
                             slrk = (decimal)dt.slrk,
                             kmch = (decimal)dt.kmch,
                             kmdw = (decimal)dt.kmdw,
                             kmgh = (decimal)dt.kmgh,
                             kmdy = (decimal)dt.kmdy,
                             tkch = (decimal)dt.tkch,
                             tkdw = (decimal)dt.tkdw,
                             tkgh = (decimal)dt.tkgh,
                             tkdy = (decimal)dt.tkdy,
                             slrkm = (decimal)dt.slrkm,
                             slrkn = (decimal)dt.slrkn,
                             ThangDT = (int)dt.ThangDT,
                             NamDT = (int)dt.NamDT
                         };
            return await query1.OrderBy(x => x.socb).OrderBy(x => x.daycb).ToListAsync();
        }
        #endregion
    }
}
