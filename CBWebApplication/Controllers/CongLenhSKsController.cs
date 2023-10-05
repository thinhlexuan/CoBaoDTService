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
    public class CongLenhSKsController : ControllerBase
    {
        private readonly CoBaoDTContext _context;

        public CongLenhSKsController(CoBaoDTContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetByTraTim")]
        public async Task<ActionResult<IEnumerable<CongLenhSK>>> GetByTraTim(DateTime NgayHL, string KhuDoan)
        {
            var query = (from item in _context.CongLenhSK where item.NgayHL<=NgayHL orderby item.NgayHL descending select item).Distinct();
            if (!string.IsNullOrWhiteSpace(KhuDoan))               
                query = query.Where(x => x.KhuDoan.Contains(KhuDoan));
            return await query.ToListAsync();
        }
        [HttpGet]
        [Route("GetByLyTrinh")]
        public async Task<ActionResult<CongLenhSK>> GetByLyTrinh(string Tuyen, decimal KmXP,decimal KmKT,string Chieu)
        {                       
            var result = await (from x in _context.CongLenhSK
                         where x.TuyenID==Tuyen && x.KmXP<=KmXP && x.KmKT>=KmXP && x.KmXP<=KmKT && x.KmKT>=KmKT && x.GhiChu.Contains(Chieu) 
                         select x).FirstOrDefaultAsync();
            if (result == null)
            {
                result = await  (from x in _context.CongLenhSK
                              where x.TuyenID == Tuyen && x.KmKT <= KmXP && x.KmXP >= KmXP && x.KmKT <= KmKT && x.KmXP >= KmKT && x.GhiChu.Contains(Chieu)
                              select x).FirstOrDefaultAsync();
            }
            return result;            
        }

        [HttpGet]
        [Route("GetByID")]
        public async Task<ActionResult<CongLenhSK>> GetByID(long id)
        {
            var congLenhSK = await _context.CongLenhSK.Where(x => x.ID == id).FirstOrDefaultAsync();
            if (congLenhSK == null)
            {
                return NotFound();
            }
            return congLenhSK;
        }

        [HttpPut]
        [Route("PutByID")]
        public async Task<ActionResult<CongLenhSK>> PutByID(long id, CongLenhSK congLenhSK)
        {
            congLenhSK.Modifydate = DateTime.Now;
            congLenhSK.KmXP = _context.LyTrinh.Where(x => x.GaID == congLenhSK.GaXP && x.TuyenID == congLenhSK.TuyenID).FirstOrDefault().Km;
            congLenhSK.KmKT = _context.LyTrinh.Where(x => x.GaID == congLenhSK.GaKT && x.TuyenID == congLenhSK.TuyenID).FirstOrDefault().Km;
            if (id != congLenhSK.ID)
            {
                return BadRequest();
            }
            _context.Entry(congLenhSK).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return congLenhSK;
        }

        [HttpPost]
        [Route("PostByID")]
        public async Task<ActionResult<CongLenhSK>> PostByID(CongLenhSK congLenhSK)
        {
            congLenhSK.Createddate = DateTime.Now;
            congLenhSK.Modifydate = congLenhSK.Createddate;
            congLenhSK.KmXP = _context.LyTrinh.Where(x => x.GaID == congLenhSK.GaXP && x.TuyenID == congLenhSK.TuyenID).FirstOrDefault().Km;
            congLenhSK.KmKT = _context.LyTrinh.Where(x => x.GaID == congLenhSK.GaKT && x.TuyenID == congLenhSK.TuyenID).FirstOrDefault().Km;
            var query = _context.CongLenhSK.Where(x => x.KhuDoan == congLenhSK.KhuDoan && x.NgayHL == congLenhSK.NgayHL).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.CongLenhSK.Add(congLenhSK);
            await _context.SaveChangesAsync();

            return congLenhSK;
        }

        [HttpDelete]
        [Route("DeleteByID")]
        public async Task<ActionResult<CongLenhSK>> DeleteByID(long id)
        {
            var congLenhSK = _context.CongLenhSK.Where(x => x.ID == id).FirstOrDefault();
            if (congLenhSK == null)
            {
                return NotFound();
            }
            _context.CongLenhSK.Remove(congLenhSK);

            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "CongLenhSK";
            nk.NoiDung = "Xóa công lệnh sức kéo: " + congLenhSK.ID + ". Khu đoạn: " + congLenhSK.KhuDoan +". Ngày HL: " +congLenhSK.NgayHL.ToString("dd.MM.yyyy");
            nk.Createddate = DateTime.Now;
            nk.Createdby = congLenhSK.Modifyby;
            nk.CreatedName = congLenhSK.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return congLenhSK;
        }
    }
}
