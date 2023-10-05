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
    public class HaNoisController : ControllerBase
    {
        private readonly CoBaoDTContext _context;
        public HaNoisController(CoBaoDTContext context)
        {
            _context = context;
        }

        #region KhuDoan
        [HttpGet]
        [Route("HNGetKhuDoanOBJ")]
        public async Task<ActionResult<string>> HNGetKhuDoanOBJ(DateTime NgayHL, string LoaiMay, string Tuyen, string GaXP, string GaKT, string CongTac)
        {
            string KhuDoan = string.Empty;
            var query = (from item in _context.HN_KhuDoan
                        where item.LoaiMayID == LoaiMay && item.CacGa.Contains(GaXP) && item.CacGa.Contains(GaKT)
                        && item.NgayHL <= NgayHL orderby item.NgayHL descending select item).Distinct();
            if (!string.IsNullOrWhiteSpace(CongTac))
                query = query.Where(x => x.CongTac.Contains(CongTac)); 
            else
                query = query.Where(x => string.IsNullOrWhiteSpace(x.CongTac));
            var objList = await query.OrderBy(x => x.Km).ToListAsync();
            if (objList != null)
            {
                foreach (var obj in objList)
                {
                    KhuDoan += string.IsNullOrWhiteSpace(KhuDoan) ? obj.KhuDoanID : ";" + obj.KhuDoanID;
                    var kmGaXP = _context.LyTrinh.Where(x => x.GaID == int.Parse(GaXP) && x.TuyenID == Tuyen).FirstOrDefaultAsync().Result.Km;
                    var kmGaKT = _context.LyTrinh.Where(x => x.GaID == int.Parse(GaKT) && x.TuyenID == Tuyen).FirstOrDefaultAsync().Result.Km;
                    string chieu = kmGaXP < kmGaKT ? "di" : "ve";
                    KhuDoan += "-" + chieu;
                }
            }            
            return KhuDoan;
        }

        [HttpGet]
        [Route("HNGetKhuDoan")]
        public async Task<ActionResult<IEnumerable<HNKhuDoan>>> HNGetKhuDoan(DateTime NgayHL, string LoaiMay, string KhuDoan)
        {
            var query = (from item in _context.HN_KhuDoan where item.NgayHL <= NgayHL orderby item.NgayHL descending select item).Distinct();         
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay);
            if (!string.IsNullOrWhiteSpace(KhuDoan))
                query = query.Where(x => x.KhuDoanID.Contains(KhuDoan));
            return await query.OrderBy(x => x.LoaiMayID).ToListAsync();
        }
        [HttpPut]
        [Route("HNPutKhuDoan")]
        public async Task<ActionResult<HNKhuDoan>> HNPutKhuDoan(long id, HNKhuDoan hNKhuDoan)
        {
            hNKhuDoan.Modifydate = DateTime.Now;
            if (id != hNKhuDoan.ID)
            {
                return BadRequest();
            }
            _context.Entry(hNKhuDoan).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return hNKhuDoan;
        }

        [HttpPost]
        [Route("HNPostKhuDoan")]
        public async Task<ActionResult<HNKhuDoan>> HNPostKhuDoan(HNKhuDoan hNKhuDoan)
        {
            hNKhuDoan.Createddate = DateTime.Now;
            hNKhuDoan.Modifydate = hNKhuDoan.Createddate;
            var query = _context.HN_KhuDoan.Where(x => x.LoaiMayID == hNKhuDoan.LoaiMayID && x.KhuDoanID == hNKhuDoan.KhuDoanID && x.CongTac == hNKhuDoan.CongTac && x.NgayHL==hNKhuDoan.NgayHL).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.HN_KhuDoan.Add(hNKhuDoan);
            await _context.SaveChangesAsync();

            return hNKhuDoan;
        }

        [HttpDelete]
        [Route("HNDeleteKhuDoan")]
        public async Task<ActionResult<HNKhuDoan>> HNDeleteKhuDoan(long id)
        {
            var hNKhuDoan = _context.HN_KhuDoan.Where(x => x.ID == id).FirstOrDefault();
            if (hNKhuDoan == null)
            {
                return NotFound();
            }
            _context.HN_KhuDoan.Remove(hNKhuDoan);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "HN_KhuDoan";
            nk.NoiDung = "Xóa khu đoạn: " + hNKhuDoan.KhuDoanID + ". Loại máy: " + hNKhuDoan.LoaiMayID;
            nk.Createddate = DateTime.Now;
            nk.Createdby = hNKhuDoan.Modifyby;
            nk.CreatedName = hNKhuDoan.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return hNKhuDoan;
        }
        #endregion

        #region NLDinhMuc
        [HttpGet]
        [Route("HNGetTanMax")]
        public async Task<ActionResult<decimal>> HNGetTanMax(DateTime NgayHL, string LoaiMay, string KhuDoan, string CongTac)
        {
            decimal TanMax = 0;
            var query = (from item in _context.HN_NLDinhMuc
                         where item.NgayHL <= NgayHL && item.LoaiMayID == LoaiMay
                         orderby item.NgayHL descending
                         select item).Distinct();
            if (!string.IsNullOrWhiteSpace(KhuDoan))
                query = query.Where(x => x.KhuDoan.Contains(KhuDoan));

            if (CongTac == "Khach")
                query = query.Where(x => x.CongTac == CongTac);
            
            var obj = await query.OrderByDescending(x => x.TanMax).FirstOrDefaultAsync();
            if (obj != null)
                TanMax = (decimal)obj.TanMax;
            return TanMax;
        }

        [HttpGet]
        [Route("HNGetNLDinhMucOBJ")]
        public async Task<ActionResult<HNNLDinhMuc>> HNGetNLDinhMucOBJ(DateTime NgayHL, string LoaiMay, string KhuDoan, decimal Tan, string CongTac, string LoaiTau)
        {
            LoaiTau = LoaiTau.ToUpper().Substring(LoaiTau.Length - 1) == "T" ? LoaiTau.Substring(0, LoaiTau.Length - 1) : LoaiTau;

            var query = (from item in _context.HN_NLDinhMuc
                         where item.NgayHL <= NgayHL && item.LoaiMayID == LoaiMay && item.TanMin <= Tan && item.TanMax >= Tan
                         orderby item.NgayHL descending
                         select item).Distinct();          

            if (CongTac== "Khach")
                query = query.Where(x => x.CongTac == CongTac);   
            else
                query = query.Where(x => string.IsNullOrWhiteSpace(x.CongTac));

            var obj1 = await query.OrderByDescending(x => x.NgayHL).ToListAsync();
            if (!string.IsNullOrWhiteSpace(KhuDoan))
                query = query.Where(x => x.KhuDoan.Contains(KhuDoan));

            var obj = await query.Where(x => x.LoaiTau.Contains(LoaiTau)).OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();
            if (obj != null)
            {
                obj.LoaiTau = LoaiTau;
                return obj;
            }
            string _loaiTau = string.Empty;
            //Trừ từ đầu về cuối
            string _right0 = LoaiTau;
            for (int i = 0; i < LoaiTau.Length; i++)
            {                
               _loaiTau = LoaiTau.Substring(i, LoaiTau.Length - i);                
                obj = await query.Where(x => x.LoaiTau.Contains(";" + _loaiTau + ";")).OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();
                if (obj != null)
                {
                    obj.LoaiTau = _loaiTau;
                    return obj;
                }
            }
            //Thay 0 số cuối bằng dầu chấm            
            for (int i = 0; i < _right0.Length; i++)
            {
                _loaiTau = _right0.Substring(i, _right0.Length - i);
                obj = await query.Where(x => x.LoaiTau.Contains(";" + _loaiTau + "..;")).OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();
                if (obj != null)
                {
                    obj.LoaiTau = _loaiTau;
                    return obj;
                }
            }
            //Thay 1 số cuối bằng dầu chấm
            string _right1= LoaiTau.Substring(0, LoaiTau.Length - 1);
            for (int i = 0; i < _right1.Length; i++)
            {
                _loaiTau = _right1.Substring(i, _right1.Length - i);
                obj = await query.Where(x => x.LoaiTau.Contains(";" + _loaiTau + "..;")).OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();
                if (obj != null)
                {
                    obj.LoaiTau = _loaiTau;
                    return obj;
                }
            }            
            //Thay 2 số cuối bằng dầu chấm
            string _right2 = LoaiTau.Substring(0, LoaiTau.Length - 2);
            for (int i = 0; i < _right2.Length; i++)
            {
                _loaiTau = _right2.Substring(i, _right2.Length - i);
                obj = await query.Where(x => x.LoaiTau.Contains(";" + _loaiTau + "..;")).OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();
                if (obj != null)
                {
                    obj.LoaiTau = _loaiTau;
                    return obj;
                }
            }
            //Nếu là tầu thoi
            if (CongTac == "Thoi"|| CongTac == "Đá" || CongTac == "Công dụng")
            {
                _loaiTau = "Tầu thoi";
                obj = await query.Where(x => x.LoaiTau.Contains(";" + _loaiTau + ";")).OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();
                if (obj != null)
                {
                    obj.LoaiTau = _loaiTau;
                    return obj;
                }
            }
            //Nếu mác tầu là kiểu số         
            int _out;           
            if (LoaiTau.Length>=4 && int.TryParse(LoaiTau.Substring(LoaiTau.Length-4),out _out))
            {
                _loaiTau = "Tầu số";
                obj = await query.Where(x => x.LoaiTau.Contains(";" + _loaiTau + ";")).OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();
                if (obj != null)
                {
                    obj.LoaiTau = _loaiTau;
                    return obj;
                }
            }
            //Các trường hợp còn lại không có loại tầu
            obj = await query.OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();
            if (obj != null)
            {
                obj.LoaiTau = LoaiTau;
                return obj;
            }            
            else
            {                
                if (CongTac == "Khach")
                    _loaiTau = "Khách khác";
                else
                    _loaiTau = "Hàng khác";
                obj = obj1.Where(x => x.KhuDoan.Contains("Tất cả") && x.LoaiTau.Contains(";" + _loaiTau + ";")).OrderByDescending(x => x.NgayHL).FirstOrDefault();
                if (obj != null)
                {
                    obj.LoaiTau = _loaiTau;
                    return obj;
                }
            }
            return null;
        }

        [HttpGet]
        [Route("HNGetNLDinhMuc")]
        public async Task<ActionResult<IEnumerable<HNNLDinhMuc>>> HNGetNLDinhMuc(DateTime NgayBD, DateTime NgayKT, string LoaiMay, string KhuDoan, string LoaiTau)
        {
            var query = from item in _context.HN_NLDinhMuc where item.NgayHL >= NgayBD && item.NgayHL < NgayKT select item;
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay);
            if (!string.IsNullOrWhiteSpace(KhuDoan))
                query = query.Where(x => x.KhuDoan.Contains(KhuDoan));
            if (!string.IsNullOrWhiteSpace(LoaiTau))
                query = query.Where(x => x.LoaiTau.Contains(LoaiTau));
            return await query.OrderBy(x => x.LoaiMayID).ToListAsync();
        }
        [HttpPut]
        [Route("HNPutNLDinhMuc")]
        public async Task<ActionResult<HNNLDinhMuc>> HNPutNLDinhMuc(long id, HNNLDinhMuc hNNLDinhMuc)
        {
            hNNLDinhMuc.Modifydate = DateTime.Now;
            if (id != hNNLDinhMuc.ID)
            {
                return BadRequest();
            }
            _context.Entry(hNNLDinhMuc).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return hNNLDinhMuc;
        }

        [HttpPost]
        [Route("HNPostNLDinhMuc")]
        public async Task<ActionResult<HNNLDinhMuc>> HNPostNLDinhMuc(HNNLDinhMuc hNNLDinhMuc)
        {
            hNNLDinhMuc.Createddate = DateTime.Now;
            hNNLDinhMuc.Modifydate = hNNLDinhMuc.Createddate;
            var query = _context.HN_NLDinhMuc.Where(x => x.LoaiMayID == hNNLDinhMuc.LoaiMayID && x.TanMin == hNNLDinhMuc.TanMin && x.TanMax == hNNLDinhMuc.TanMax             
            && x.KhuDoan==hNNLDinhMuc.KhuDoan && x.LoaiTau==hNNLDinhMuc.LoaiTau && x.NgayHL == hNNLDinhMuc.NgayHL).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.HN_NLDinhMuc.Add(hNNLDinhMuc);
            await _context.SaveChangesAsync();

            return hNNLDinhMuc;
        }

        [HttpDelete]
        [Route("HNDeleteNLDinhMuc")]
        public async Task<ActionResult<HNNLDinhMuc>> HNDeleteNLDinhMuc(long id)
        {
            var hNNLDinhMuc = _context.HN_NLDinhMuc.Where(x => x.ID == id).FirstOrDefault();
            if (hNNLDinhMuc == null)
            {
                return NotFound();
            }
            _context.HN_NLDinhMuc.Remove(hNNLDinhMuc);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "HN_NLDinhMuc";
            nk.NoiDung = "Xóa NL định mức: " + hNNLDinhMuc.KhuDoan + ". Loại máy: " + hNNLDinhMuc.LoaiMayID;
            nk.Createddate = DateTime.Now;
            nk.Createdby = hNNLDinhMuc.Modifyby;
            nk.CreatedName = hNNLDinhMuc.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return hNNLDinhMuc;
        }
        #endregion

        #region NLPDinhMuc
        [HttpGet]
        [Route("HNGetNLPDinhMucOBJ")]
        public async Task<ActionResult<HNNLPDinhMuc>> HNGetNLPDinhMucOBJ(DateTime NgayHL, string LoaiMay, string CongTac, string KhuDoan)
        {
            var query = (from item in _context.HN_NLPDinhMuc
                         where item.NgayHL <= NgayHL && item.LoaiMayID == LoaiMay && item.CongTac == CongTac
                         orderby item.NgayHL descending
                         select item).Distinct();
            if (!string.IsNullOrWhiteSpace(KhuDoan))
                query = query.Where(x => x.KhuDoan.Contains(KhuDoan));
            else
                query = query.Where(x => string.IsNullOrWhiteSpace(x.KhuDoan));
            return await query.OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();
        }
        [HttpGet]
        [Route("HNGetNLPDinhMuc")]
        public async Task<ActionResult<IEnumerable<HNNLPDinhMuc>>> HNGetNLPDinhMuc(DateTime NgayHL, string LoaiMay, string KhuDoan)
        {
            var query = (from item in _context.HN_NLPDinhMuc where item.NgayHL <= NgayHL orderby item.NgayHL descending select item).Distinct();
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay);
            if (!string.IsNullOrWhiteSpace(KhuDoan))
                query = query.Where(x => x.KhuDoan.Contains(KhuDoan));
            return await query.OrderBy(x => x.LoaiMayID).ToListAsync();
        }
        [HttpPut]
        [Route("HNPutNLPDinhMuc")]
        public async Task<ActionResult<HNNLPDinhMuc>> HNPutNLPDinhMuc(long id, HNNLPDinhMuc hNNLPDinhMuc)
        {
            hNNLPDinhMuc.Modifydate = DateTime.Now;
            if (id != hNNLPDinhMuc.ID)
            {
                return BadRequest();
            }
            _context.Entry(hNNLPDinhMuc).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return hNNLPDinhMuc;
        }

        [HttpPost]
        [Route("HNPostNLPDinhMuc")]
        public async Task<ActionResult<HNNLPDinhMuc>> HNPostNLPDinhMuc(HNNLPDinhMuc hNNLPDinhMuc)
        {
            hNNLPDinhMuc.Createddate = DateTime.Now;
            hNNLPDinhMuc.Modifydate = hNNLPDinhMuc.Createddate;
            var query = _context.HN_NLPDinhMuc.Where(x => x.LoaiMayID == hNNLPDinhMuc.LoaiMayID && x.CongTac==hNNLPDinhMuc.CongTac && x.KhuDoan == hNNLPDinhMuc.KhuDoan && x.NgayHL == hNNLPDinhMuc.NgayHL).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.HN_NLPDinhMuc.Add(hNNLPDinhMuc);
            await _context.SaveChangesAsync();

            return hNNLPDinhMuc;
        }

        [HttpDelete]
        [Route("HNDeleteNLPDinhMuc")]
        public async Task<ActionResult<HNNLPDinhMuc>> HNDeleteNLPDinhMuc(long id)
        {
            var hNNLPDinhMuc = _context.HN_NLPDinhMuc.Where(x => x.ID == id).FirstOrDefault();
            if (hNNLPDinhMuc == null)
            {
                return NotFound();
            }
            _context.HN_NLPDinhMuc.Remove(hNNLPDinhMuc);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "HN_NLPDinhMuc";
            nk.NoiDung = "Xóa NL phụ định mức: " + hNNLPDinhMuc.KhuDoan + ". Loại máy: " + hNNLPDinhMuc.LoaiMayID;
            nk.Createddate = DateTime.Now;
            nk.Createdby = hNNLPDinhMuc.Modifyby;
            nk.CreatedName = hNNLPDinhMuc.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return hNNLPDinhMuc;
        }
        #endregion

        #region DMDinhMuc
        [HttpGet]
        [Route("HNGetDMDinhMuc")]
        public async Task<ActionResult<IEnumerable<HNDMDinhMuc>>> HNGetDMDinhMuc(DateTime NgayHL, string LoaiMay, short DauMoID)
        {
            var query = (from item in _context.HN_DMDinhMuc where item.NgayHL <= NgayHL orderby item.NgayHL descending select item).Distinct();
            if (LoaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == LoaiMay);
            if (DauMoID > 0)
                query = query.Where(x => x.DauMoID == DauMoID);
            return await query.OrderBy(x => x.LoaiMayID).ToListAsync();
        }
        [HttpPut]
        [Route("HNPutDMDinhMuc")]
        public async Task<ActionResult<HNDMDinhMuc>> HNPutDMDinhMuc(long id, HNDMDinhMuc hNDMDinhMuc)
        {
            hNDMDinhMuc.Modifydate = DateTime.Now;
            if (id != hNDMDinhMuc.ID)
            {
                return BadRequest();
            }
            _context.Entry(hNDMDinhMuc).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return hNDMDinhMuc;
        }

        [HttpPost]
        [Route("HNPostDMDinhMuc")]
        public async Task<ActionResult<HNDMDinhMuc>> HNPostDMDinhMuc(HNDMDinhMuc hNDMDinhMuc)
        {
            hNDMDinhMuc.Createddate = DateTime.Now;
            hNDMDinhMuc.Modifydate = hNDMDinhMuc.Createddate;
            var query = _context.HN_DMDinhMuc.Where(x => x.LoaiMayID == hNDMDinhMuc.LoaiMayID && x.DauMoID == hNDMDinhMuc.DauMoID && x.NgayHL == hNDMDinhMuc.NgayHL).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.HN_DMDinhMuc.Add(hNDMDinhMuc);
            await _context.SaveChangesAsync();

            return hNDMDinhMuc;
        }

        [HttpDelete]
        [Route("HNDeleteDMDinhMuc")]
        public async Task<ActionResult<HNDMDinhMuc>> HNDeleteDMDinhMuc(long id)
        {
            var hNDMDinhMuc = _context.HN_DMDinhMuc.Where(x => x.ID == id).FirstOrDefault();
            if (hNDMDinhMuc == null)
            {
                return NotFound();
            }
            _context.HN_DMDinhMuc.Remove(hNDMDinhMuc);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "HN_DMDinhMuc";
            nk.NoiDung = "Xóa dầu mỡ định mức: " + hNDMDinhMuc.DauMoName + ". Loại máy: " + hNDMDinhMuc.LoaiMayID;
            nk.Createddate = DateTime.Now;
            nk.Createdby = hNDMDinhMuc.Modifyby;
            nk.CreatedName = hNDMDinhMuc.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return hNDMDinhMuc;
        }
        #endregion

        #region PhieuThuong
        [HttpGet]
        [Route("HNGetPhieuThuong")]
        public async Task<ActionResult<IEnumerable<HNPhieuThuong>>> HNGetPhieuThuong(DateTime NgayHL, string LoaiPhieu, string MacTau)
        {
            var query = (from item in _context.HN_PhieuThuong where item.NgayHL <= NgayHL orderby item.NgayHL descending select item).Distinct();
            if (LoaiPhieu != "Tất cả")
                query = query.Where(x => x.LoaiPhieu == LoaiPhieu);
            if (!string.IsNullOrWhiteSpace(MacTau))
                query = query.Where(x => x.MacTau.Contains(MacTau));
            return await query.OrderBy(x => x.LoaiPhieu).ToListAsync();
        }
        [HttpGet]
        [Route("HNGetPhieuThuongOBJ")]
        public async Task<ActionResult<HNPhieuThuong>> HNGetPhieuThuongOBJ(DateTime NgayHL, string LoaiPhieu, string MacTau, string GaName)
        {
            var query = (from item in _context.HN_PhieuThuong
                         where item.NgayHL <= NgayHL && item.LoaiPhieu == LoaiPhieu && item.MacTau.Contains(MacTau)
                         orderby item.NgayHL descending
                         select item).Distinct();
            if (!string.IsNullOrWhiteSpace(GaName))
                query = query.Where(x => x.GaName.Contains(GaName));
            var obj = await query.OrderByDescending(x => x.NgayHL).FirstOrDefaultAsync();            
            if (obj != null)
            {              
                return obj;
            }          
            return null;
        }
        [HttpPut]
        [Route("HNPutPhieuThuong")]
        public async Task<ActionResult<HNPhieuThuong>> HNPutPhieuThuong(long id, HNPhieuThuong hNPhieuThuong)
        {
            hNPhieuThuong.ModifyDate = DateTime.Now;
            if (id != hNPhieuThuong.ID)
            {
                return BadRequest();
            }
            _context.Entry(hNPhieuThuong).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return hNPhieuThuong;
        }

        [HttpPost]
        [Route("HNPostPhieuThuong")]
        public async Task<ActionResult<HNPhieuThuong>> HNPostPhieuThuong(HNPhieuThuong hNPhieuThuong)
        {
            hNPhieuThuong.CreatedDate = DateTime.Now;
            hNPhieuThuong.ModifyDate = hNPhieuThuong.CreatedDate;
            var query = _context.HN_PhieuThuong.Where(x => x.LoaiPhieu == hNPhieuThuong.LoaiPhieu && x.MacTau == hNPhieuThuong.MacTau && x.GaID==hNPhieuThuong.GaID && x.NgayHL == hNPhieuThuong.NgayHL).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.HN_PhieuThuong.Add(hNPhieuThuong);
            await _context.SaveChangesAsync();

            return hNPhieuThuong;
        }

        [HttpDelete]
        [Route("HNDeletePhieuThuong")]
        public async Task<ActionResult<HNPhieuThuong>> HNDeletePhieuThuong(long id)
        {
            var hNPhieuThuong = _context.HN_PhieuThuong.Where(x => x.ID == id).FirstOrDefault();
            if (hNPhieuThuong == null)
            {
                return NotFound();
            }
            _context.HN_PhieuThuong.Remove(hNPhieuThuong);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "HN_PhieuThuong";
            nk.NoiDung = "Xóa phiếu thưởng: " + hNPhieuThuong.LoaiPhieu + ". Mác tầu: " + hNPhieuThuong.MacTau + ". Ga Name: " + hNPhieuThuong.GaName 
                + ". DonGia: " + hNPhieuThuong.DonGia + ". Ngày HL: " + hNPhieuThuong.NgayHL;
            nk.Createddate = DateTime.Now;
            nk.Createdby = hNPhieuThuong.ModifyBy;
            nk.CreatedName = hNPhieuThuong.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return hNPhieuThuong;
        }
        #endregion

        #region XuatDT
        [HttpGet]
        [Route("HNGetXuatDT")]
        public async Task<ActionResult<IEnumerable<HNXuatDT>>> HNGetXuatDT(string maDV, int thangDT, int namDT)
        {
            var query = (from dt in _context.DoanThong
                        join cb in _context.CoBao on dt.DoanThongID equals cb.CoBaoID
                        join ct in _context.DoanThongCT on dt.DoanThongID equals ct.DoanThongID                       
                        where dt.ThangDT == thangDT && dt.NamDT == namDT && cb.DvcbID == maDV
                        select new                         
                        {
                            DoanThongID = (long)dt.DoanThongID,
                            socb=(string)cb.SoCB,
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
                            dayxp=(DateTime)ct.NgayXP,
                            dtau = (string)cb.SHDT,
                            mghep=(string)ct.MayGhepID,
                            ThangDT = (int)dt.ThangDT,
                            NamDT = (int)dt.NamDT
                        });


            var query1 = from dt in query
                        //join bm in _context.CoBaoDM on dt.DoanThongID equals bm.CoBaoID into bms
                        // from bm in bms.DefaultIfEmpty()
                        // join dm in _context.DoanThongDM on dt.DoanThongID equals dm.DoanThongID into dms
                        // from dm in dms.DefaultIfEmpty()                         
                        select new HNXuatDT
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
                            mtau = (string)dt.mtau,
                            ctac = (short)dt.ctac,
                            tchat = (short)dt.tchat,
                            kdoan = (string)dt.kdoan,
                            slbt = (decimal)dt.slbt,
                            sllh = (decimal)dt.sllh,
                            slbs = (int)dt.slbs,
                            sltt = (decimal)dt.sltt,
                            sltc = (decimal)dt.sltc,
                            slpt = (decimal)dt.slpt,
                            nlanh = (string)dt.nlanh,
                            gaxp = (string)dt.gaxp,
                            daycb = (DateTime)dt.daycb,
                            dgkb = (decimal)dt.dgkb,
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
                            //l_dc = (decimal)bm.LoaiDauMoID == 1 ? bm.Linh : 0,
                            //l_tl = (decimal)bm.LoaiDauMoID == 3 ? bm.Linh : 0,
                            //l_gt = (decimal)bm.LoaiDauMoID == 2 ? bm.Linh : 0,
                            //t_dc = (decimal)bm.LoaiDauMoID == 1 ? dm.TieuThu : 0,
                            //t_tl = (decimal)bm.LoaiDauMoID == 3 ? dm.TieuThu : 0,
                            //t_gt = (decimal)bm.LoaiDauMoID == 2 ? dm.TieuThu : 0,
                            //c_dc = (decimal)bm.LoaiDauMoID == 1 ? dm.DinhMuc : 0,
                            //c_tl = (decimal)bm.LoaiDauMoID == 3 ? dm.DinhMuc : 0,
                            //c_gt = (decimal)bm.LoaiDauMoID == 2 ? dm.DinhMuc : 0,
                            slrkm = (decimal)dt.slrkm,
                            slrkn = (decimal)dt.slrkn,
                            dayxp=(DateTime)dt.dayxp,
                            dtau = (string)dt.dtau,
                            mghep=(string)dt.mghep,
                            ThangDT = (int)dt.ThangDT,
                            NamDT = (int)dt.NamDT
                        };
            return await query1.OrderBy(x => x.socb).OrderBy(x => x.daycb).ToListAsync();
        }

        [HttpGet]
        [Route("HNGetXuatDTGA")]
        public async Task<ActionResult<IEnumerable<HNXuatDT>>> HNGetXuatDTGA(string maDV, int thangDT, int namDT)
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
                             dayxp=(DateTime)ct.NgayXP,
                             dtau = (string)cb.SHDT,
                             mghep=(string)ct.MayGhepID,
                             ThangDT = (int)dt.ThangDT,
                             NamDT = (int)dt.NamDT
                         });


            var query1 = from dt in query
                         //join bm in _context.CoBaoGADM on dt.DoanThongID equals bm.CoBaoID into bms
                         //from bm in bms.DefaultIfEmpty()
                         //join dm in _context.DoanThongGADM on dt.DoanThongID equals dm.DoanThongID into dms
                         //from dm in dms.DefaultIfEmpty()
                         select new HNXuatDT
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
                             mtau = (string)dt.mtau,
                             ctac = (short)dt.ctac,
                             tchat = (short)dt.tchat,
                             kdoan = (string)dt.kdoan,
                             slbt = (decimal)dt.slbt,
                             sllh = (decimal)dt.sllh,
                             slbs = (int)dt.slbs,
                             sltt = (decimal)dt.sltt,
                             sltc = (decimal)dt.sltc,
                             slpt = (decimal)dt.slpt,
                             nlanh = (string)dt.nlanh,
                             gaxp = (string)dt.gaxp,
                             daycb = (DateTime)dt.daycb,
                             dgkb = (decimal)dt.dgkb,
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
                             //l_dc = (decimal)bm.LoaiDauMoID == 1 ? bm.Linh : 0,
                             //l_tl = (decimal)bm.LoaiDauMoID == 3 ? bm.Linh : 0,
                             //l_gt = (decimal)bm.LoaiDauMoID == 2 ? bm.Linh : 0,
                             //t_dc = (decimal)bm.LoaiDauMoID == 1 ? dm.TieuThu : 0,
                             //t_tl = (decimal)bm.LoaiDauMoID == 3 ? dm.TieuThu : 0,
                             //t_gt = (decimal)bm.LoaiDauMoID == 2 ? dm.TieuThu : 0,
                             //c_dc = (decimal)bm.LoaiDauMoID == 1 ? dm.DinhMuc : 0,
                             //c_tl = (decimal)bm.LoaiDauMoID == 3 ? dm.DinhMuc : 0,
                             //c_gt = (decimal)bm.LoaiDauMoID == 2 ? dm.DinhMuc : 0,
                             slrkm = (decimal)dt.slrkm,
                             slrkn = (decimal)dt.slrkn,
                             dayxp=(DateTime)dt.dayxp,
                             dtau = (string)dt.dtau,
                             mghep=(string)dt.mghep,
                             ThangDT = (int)dt.ThangDT,
                             NamDT = (int)dt.NamDT
                         };
            return await query1.OrderBy(x => x.socb).OrderBy(x => x.daycb).ToListAsync();
        }
        #endregion
    }
}
