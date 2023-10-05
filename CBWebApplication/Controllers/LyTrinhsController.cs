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
    public class LyTrinhsController : ControllerBase
    {
        private readonly CoBaoDTContext _context;

        public LyTrinhsController(CoBaoDTContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("GetByTraTim")]
        public async Task<ActionResult<IEnumerable<LyTrinh>>> GetByTraTim(string TuyenID, string TenGa)
        {
            var query = from item in _context.LyTrinh select item;
            if (TuyenID != "ALL")
                query = query.Where(x => x.TuyenID == TuyenID);
            if(!string.IsNullOrWhiteSpace(TenGa))
                query = query.Where(x => x.TenGa.Contains(TenGa));
            return await query.ToListAsync();
        }

        [HttpGet]
        [Route("GetDMLyTrinh")]
        public async Task<ActionResult<DmLyTrinh>> GetDMLyTrinh(int GaXP, int GaKT, string Tuyen)
        {
            DmLyTrinh dm = new DmLyTrinh(); 
            if(!string.IsNullOrWhiteSpace(Tuyen))
            {
                var xp = await _context.LyTrinh.Where(x => x.GaID == GaXP && x.TuyenID == Tuyen).FirstOrDefaultAsync();
                var kt = await _context.LyTrinh.Where(x => x.GaID == GaKT && x.TuyenID == Tuyen).FirstOrDefaultAsync();
                if(xp!=null && kt!=null)
                {
                    dm = new DmLyTrinh();
                    dm.TuyenId = xp.TuyenID;
                    dm.TuyenName = xp.TuyenName;
                    dm.GaDiId = xp.GaID;
                    dm.GaDiName = xp.TenGa;
                    dm.GaDiKM = xp.Km;
                    dm.GaDenId = kt.GaID;
                    dm.GaDenName = kt.TenGa;
                    dm.GaDenKM = kt.Km;
                    dm.Chieu = xp.Km <= kt.Km ? "di" : "ve";
                    return dm;
                }    

            }    
            var listLTXP = await _context.LyTrinh.Where(x => x.GaID == GaXP).ToListAsync();
            if (listLTXP == null)
            {
                return null;
            }
            foreach (var xp in listLTXP)
            {
                try
                {
                    var kt = _context.LyTrinh.Where(x => x.GaID == GaKT && x.TuyenID == xp.TuyenID).FirstOrDefault();
                    if (kt != null)
                    {
                        dm = new DmLyTrinh();
                        dm.TuyenId = xp.TuyenID;
                        dm.TuyenName = xp.TuyenName;
                        dm.GaDiId = xp.GaID;
                        dm.GaDiName = xp.TenGa;
                        dm.GaDiKM = xp.Km;
                        dm.GaDenId = kt.GaID;
                        dm.GaDenName = kt.TenGa;
                        dm.GaDenKM = kt.Km;
                        dm.Chieu = xp.Km <= kt.Km ? "di" : "ve";
                        return dm;
                    }
                }
                catch
                {
                    continue;
                }
            }           
            return null;
        }

        [HttpGet]
        [Route("GetByID")]
        public async Task<ActionResult<LyTrinh>> GetByID(long id)
        {
            var lyTrinh = await _context.LyTrinh.Where(x=>x.ID==id).FirstOrDefaultAsync();
            if (lyTrinh == null)
            {
                return NotFound();
            }
            return lyTrinh;
        }

        [HttpPut]
        [Route("PutByID")]
        public async Task<ActionResult<LyTrinh>> PutByID(long id, LyTrinh lyTrinh)
        {
            lyTrinh.Modifydate = DateTime.Now;
            if (id != lyTrinh.ID)
            {
                return BadRequest();
            }
            _context.Entry(lyTrinh).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            } 
            return lyTrinh;
        }
       
        [HttpPost]
        [Route("PostByID")]
        public async Task<ActionResult<LyTrinh>> PostByID(LyTrinh lyTrinh)
        {
            lyTrinh.Createddate = DateTime.Now;
            lyTrinh.Modifydate = lyTrinh.Createddate;
            var query = _context.LyTrinh.Where(x => x.TuyenID == lyTrinh.TuyenID && x.GaID == lyTrinh.GaID).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.LyTrinh.Add(lyTrinh);
            await _context.SaveChangesAsync();

            return lyTrinh;
        }
        
        [HttpDelete]
        [Route("DeleteByID")]
        public async Task<ActionResult<LyTrinh>> DeleteByID(long id)
        {
            var lyTrinh = _context.LyTrinh.Where(x => x.ID == id).FirstOrDefault();
            if (lyTrinh == null)
            {
                return NotFound();
            }
            _context.LyTrinh.Remove(lyTrinh);

            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "LyTrinh";
            nk.NoiDung = "Xóa Lý trình tuyến: " + lyTrinh.TuyenName + ". Ga: " + lyTrinh.TenGa;
            nk.Createddate = DateTime.Now;
            nk.Createdby = lyTrinh.Modifyby;
            nk.CreatedName = lyTrinh.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return lyTrinh;
        }       
    }
}
