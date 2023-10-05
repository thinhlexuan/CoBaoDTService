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
    public class DaNangsController : ControllerBase
    {
        private readonly CoBaoDTContext _context;
        public DaNangsController(CoBaoDTContext context)
        {
            _context = context;
        }

        #region KhuDoan
        [HttpGet]
        [Route("DNGetKhuDoanOBJ")]
        public async Task<ActionResult<string>> DNGetKhuDoanOBJ(string Tuyen, string GaXP, string GaKT)
        {
            string KhuDoan = string.Empty;
            var query = from item in _context.DN_KhuDoan
                        where item.CacGa.Contains(GaXP) && item.CacGa.Contains(GaKT)
                        select item;
            var obj = await query.OrderBy(x => x.Km).FirstOrDefaultAsync();
            if (obj != null)
            {
                KhuDoan = obj.KhuDoanID;
                var kmGaXP = _context.LyTrinh.Where(x => x.GaID == int.Parse(GaXP) && x.TuyenID == Tuyen).FirstOrDefaultAsync().Result.Km;
                var kmGaKT = _context.LyTrinh.Where(x => x.GaID == int.Parse(GaKT) && x.TuyenID == Tuyen).FirstOrDefaultAsync().Result.Km;
                string chieu = kmGaXP < kmGaKT ? "di" : "ve";
                KhuDoan += "-" + chieu;
            }
            return KhuDoan;
        }

        [HttpGet]
        [Route("DNGetKhuDoan")]
        public async Task<ActionResult<IEnumerable<DNKhuDoan>>> DNGetKhuDoan(string KhuDoan)
        {
            var query = from item in _context.DN_KhuDoan select item;
            if (!string.IsNullOrWhiteSpace(KhuDoan))
                query = query.Where(x => x.KhuDoanID.Contains(KhuDoan));
            return await query.OrderBy(x => x.KhuDoanID).ToListAsync();
        }
        [HttpPut]
        [Route("DNPutKhuDoan")]
        public async Task<ActionResult<DNKhuDoan>> DNPutKhuDoan(long id, DNKhuDoan dNKhuDoan)
        {
            dNKhuDoan.Modifydate = DateTime.Now;
            if (id != dNKhuDoan.ID)
            {
                return BadRequest();
            }
            _context.Entry(dNKhuDoan).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return dNKhuDoan;
        }

        [HttpPost]
        [Route("DNPostKhuDoan")]
        public async Task<ActionResult<DNKhuDoan>> DNPostKhuDoan(DNKhuDoan dNKhuDoan)
        {
            dNKhuDoan.Createddate = DateTime.Now;
            dNKhuDoan.Modifydate = dNKhuDoan.Createddate;
            var query = _context.DN_KhuDoan.Where(x => x.KhuDoanID == dNKhuDoan.KhuDoanID).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.DN_KhuDoan.Add(dNKhuDoan);
            await _context.SaveChangesAsync();

            return dNKhuDoan;
        }

        [HttpDelete]
        [Route("DNDeleteKhuDoan")]
        public async Task<ActionResult<DNKhuDoan>> DNDeleteKhuDoan(long id)
        {
            var dNKhuDoan = _context.DN_KhuDoan.Where(x => x.ID == id).FirstOrDefault();
            if (dNKhuDoan == null)
            {
                return NotFound();
            }
            _context.DN_KhuDoan.Remove(dNKhuDoan);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "DN_KhuDoan";
            nk.NoiDung = "Xóa khu đoạn: " + dNKhuDoan.KhuDoanID;
            nk.Createddate = DateTime.Now;
            nk.Createdby = dNKhuDoan.Modifyby;
            nk.CreatedName = dNKhuDoan.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return dNKhuDoan;
        }
        #endregion

        #region NLDinhMuc
        [HttpGet]
        [Route("DNGetNLDinhMucOBJ")]
        public async Task<ActionResult<DNNLDinhMuc>> YVGetNLDinhMucOBJ(DateTime NgayHL, string LoaiMay, string KhuDoan, decimal Tan, string LoaiTau)
        {
            var query = (from item in _context.DN_NLDinhMuc
                         where item.NgayHL <= NgayHL && item.TanMin <= Tan && item.TanMax >= Tan
                         orderby item.NgayHL descending
                         select item).Distinct();
            if (!string.IsNullOrWhiteSpace(KhuDoan))
                query = query.Where(x => x.KhuDoan.Contains(KhuDoan));
            if (!string.IsNullOrWhiteSpace(LoaiMay))
                query = query.Where(x => x.LoaiMay.Contains(LoaiMay));           
            var obj = await query.Where(x => x.LoaiTau.Contains(LoaiTau)).OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();
            if (obj != null)
            {
                obj.LoaiTau = LoaiTau;
                return obj;
            }
            int ktDau = char.IsDigit(LoaiTau[0]) ? 0 : 1;
            for (int i = ktDau; i < LoaiTau.Length; i++)
            {
                string dauTau = LoaiTau.Substring(i, LoaiTau.Length - i);
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
        [Route("DNGetNLDinhMucTemp")]
        public async Task<ActionResult<IEnumerable<DNNLDinhMucTemp>>> DNGetNLDinhMucTemp()
        {           
            return await _context.DinhMucDN.OrderBy(x => x.TanMax).OrderBy(x=>x.TanMin).OrderBy(x=>x.DinhMuc).ToListAsync();
        }

        [HttpGet]
        [Route("DNGetNLDinhMuc")]
        public async Task<ActionResult<IEnumerable<DNNLDinhMuc>>> DNGetNLDinhMuc(DateTime NgayHL, string LoaiMay, string KhuDoan, string LoaiTau)
        {
            var query = (from item in _context.DN_NLDinhMuc where item.NgayHL <= NgayHL orderby item.NgayHL descending select item).Distinct();
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMay.Contains(LoaiMay));
            if (!string.IsNullOrWhiteSpace(KhuDoan))
                query = query.Where(x => x.KhuDoan.Contains(KhuDoan));
            if (!string.IsNullOrWhiteSpace(LoaiTau))
                query = query.Where(x => x.LoaiTau.Contains(LoaiTau));

            return await query.OrderBy(x => x.KhuDoan).ToListAsync();
        }
        [HttpPut]
        [Route("DNPutNLDinhMuc")]
        public async Task<ActionResult<DNNLDinhMuc>> DNPutNLDinhMuc(long id, DNNLDinhMuc dNNLDinhMuc)
        {
            dNNLDinhMuc.Modifydate = DateTime.Now;
            if (id != dNNLDinhMuc.ID)
            {
                return BadRequest();
            }
            _context.Entry(dNNLDinhMuc).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return dNNLDinhMuc;
        }

        [HttpPost]
        [Route("DNPostNLDinhMuc")]
        public async Task<ActionResult<DNNLDinhMuc>> DNPostNLDinhMuc(DNNLDinhMuc dNNLDinhMuc)
        {
            dNNLDinhMuc.Createddate = DateTime.Now;
            dNNLDinhMuc.Modifydate = dNNLDinhMuc.Createddate;
            //var query = _context.DN_NLDinhMuc.Where(x => x.LoaiMay == dNNLDinhMuc.LoaiMay && x.KhuDoan == dNNLDinhMuc.KhuDoan && x.NgayHL == dNNLDinhMuc.NgayHL).FirstOrDefault();
            //if (query != null)
            //    return NotFound();
            _context.DN_NLDinhMuc.Add(dNNLDinhMuc);
            await _context.SaveChangesAsync();

            return dNNLDinhMuc;
        }

        [HttpDelete]
        [Route("DNDeleteNLDinhMuc")]
        public async Task<ActionResult<DNNLDinhMuc>> DNDeleteNLDinhMuc(long id)
        {
            var dNNLDinhMuc = _context.DN_NLDinhMuc.Where(x => x.ID == id).FirstOrDefault();
            if (dNNLDinhMuc == null)
            {
                return NotFound();
            }
            _context.DN_NLDinhMuc.Remove(dNNLDinhMuc);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "DN_NLDinhMuc";
            nk.NoiDung = "Xóa NL định mức: " + dNNLDinhMuc.KhuDoan + ". Loại máy: " + dNNLDinhMuc.LoaiMay;
            nk.Createddate = DateTime.Now;
            nk.Createdby = dNNLDinhMuc.Modifyby;
            nk.CreatedName = dNNLDinhMuc.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return dNNLDinhMuc;
        }
        #endregion

        #region XuatDT
        [HttpGet]
        [Route("DNGetXuatDT")]
        public async Task<ActionResult<IEnumerable<DNXuatDT>>> DNGetXuatDT(string maDV, int thangDT, int namDT)
        {
            return await (from dt in _context.DoanThong
                          join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID                        
                          join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID
                          where dt.ThangDT == thangDT && dt.NamDT == namDT && cb.DvcbID == maDV
                          select new DNXuatDT
                          {
                              socb = (string)cb.SoCB,
                              madm = (string)cb.DauMayID,
                              mact = (short)ct.CongTacID,
                              makd = (string)ct.KhuDoan,
                              ngaydi = (DateTime)cb.NhanMay,
                              mactau = (string)ct.MacTauID,
                              matc = (short)ct.TinhChatID,
                              solanrk = (decimal)cb.SoLanRaKho,
                              km = (decimal)(ct.KMChinh + ct.KMDon + ct.KMGhep + ct.KMDay),
                              tkm = (decimal)(ct.TKMChinh + ct.TKMDon + ct.TKMGhep + ct.TKMDay),
                              donxp = (decimal)ct.DonXP,
                              dondd = (decimal)ct.DonDD,
                              doncc = (decimal)ct.DonKT,
                              dungdm = (decimal)ct.DungDM,
                              dungdn = (decimal)ct.DungDN,
                              dungxp = (decimal)ct.DungXP,
                              dungdd = (decimal)ct.DungDD,
                              dungcc = (decimal)ct.DungKT,
                              qvlh = (decimal)ct.QuayVong,
                              lh = (decimal)ct.LuHanh,
                              nltt = (decimal)ct.TieuThu,
                              nltc = (decimal)ct.DinhMuc,
                              gaxp = (string)ct.GaXPName,
                              ngayxp = (DateTime)ct.NgayXP,
                              ThangDT = (int)dt.ThangDT,
                              NamDT = (int)dt.NamDT
                          }).OrderBy(x => x.socb).OrderBy(x => x.ngaydi).ToListAsync();
        }

        [HttpGet]
        [Route("DNGetXuatDTGA")]
        public async Task<ActionResult<IEnumerable<DNXuatDT>>> DNGetXuatDTGA(string maDV, int thangDT, int namDT)
        {
            return await (from dt in _context.DoanThongGA
                         join cb in _context.CoBaoGA on dt.DoanThongID equals cb.CoBaoID                        
                         join ct in _context.DoanThongGACT on dt.DoanThongID equals ct.DoanThongID
                         where dt.ThangDT == thangDT && dt.NamDT == namDT && cb.DvcbID == maDV
                         select new DNXuatDT
                         {
                             socb = (string)cb.SoCB,
                             madm = (string)cb.DauMayID,
                             mact = (short)ct.CongTacID,
                             makd = (string)ct.KhuDoan,
                             ngaydi = (DateTime)cb.NhanMay,
                             mactau = (string)ct.MacTauID,
                             matc = (short)ct.TinhChatID,
                             solanrk = (decimal)cb.SoLanRaKho,
                             km = (decimal)(ct.KMChinh+ ct.KMDon+ct.KMGhep+ct.KMDay),
                             tkm = (decimal)(ct.TKMChinh + ct.TKMDon + ct.TKMGhep + ct.TKMDay),                             
                             donxp = (decimal)ct.DonXP,
                             dondd = (decimal)ct.DonDD,
                             doncc = (decimal)ct.DonKT,                            
                             dungdm = (decimal)ct.DungDM,
                             dungdn = (decimal)ct.DungDN,                           
                             dungxp = (decimal)ct.DungXP,
                             dungdd = (decimal)ct.DungDD,
                             dungcc = (decimal)ct.DungKT,
                             qvlh = (decimal)ct.QuayVong,
                             lh = (decimal)ct.LuHanh,                             
                             nltt = (decimal)ct.TieuThu,
                             nltc = (decimal)ct.DinhMuc,
                             gaxp = (string)ct.GaXPName,
                             ngayxp = (DateTime)ct.NgayXP,
                             ThangDT = (int)dt.ThangDT,
                             NamDT = (int)dt.NamDT
                         }).OrderBy(x => x.socb).OrderBy(x => x.ngaydi).ToListAsync();
        }
        #endregion
    }
}
