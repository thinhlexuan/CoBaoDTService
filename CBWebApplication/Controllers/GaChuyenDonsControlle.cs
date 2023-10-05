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
    public class GaChuyenDonsController : ControllerBase
    {
        private readonly CoBaoDTContext _context;

        public GaChuyenDonsController(CoBaoDTContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetGaChuyenDon")]
        public async Task<ActionResult<IEnumerable<GaChuyenDon>>> GetGaChuyenDon(DateTime ngayHL,string gaName)
        {
            var query = from item in _context.GaChuyenDon where item.NgayHL<=ngayHL select item;            
            if (!string.IsNullOrWhiteSpace(gaName))
                query = query.Where(x=>x.GaName.Contains(gaName));
            return await query.ToListAsync();            
        }       

        [HttpPut]
        [Route("PutGaChuyenDon")]
        public async Task<ActionResult<GaChuyenDon>> PutGaChuyenDon(GaChuyenDon gaChuyenDon)
        {
            gaChuyenDon.ModifyDate = DateTime.Now;
            _context.Entry(gaChuyenDon).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return gaChuyenDon;
        }

        [HttpPost]
        [Route("PostGaChuyenDon")]
        public async Task<ActionResult<GaChuyenDon>> PostGaChuyenDon(GaChuyenDon gaChuyenDon)
        {
            gaChuyenDon.CreatedDate = DateTime.Now;
            gaChuyenDon.ModifyDate = gaChuyenDon.CreatedDate;            
            var query = _context.GaChuyenDon.Where(x => x.GaId == gaChuyenDon.GaId && x.NgayHL== gaChuyenDon.NgayHL).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.GaChuyenDon.Add(gaChuyenDon);
            await _context.SaveChangesAsync();

            return gaChuyenDon;
        }

        [HttpDelete]
        [Route("DeleteGaChuyenDon")]
        public async Task<ActionResult<GaChuyenDon>> DeleteGaChuyenDon(DateTime ngayHL, int gaId)
        {
            var gaChuyenDon = _context.GaChuyenDon.Where(x => x.GaId == gaId && x.NgayHL==ngayHL).FirstOrDefault();
            if (gaChuyenDon == null)
            {
                return NotFound();
            }
            _context.GaChuyenDon.Remove(gaChuyenDon);

            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "GaChuyenDon";
            nk.NoiDung = "Xóa ga chuyên dồn: " + gaChuyenDon.GaId + "-" + gaChuyenDon.GaName + "-Ngày hiệu lực: " + gaChuyenDon.NgayHL;
            nk.Createddate = DateTime.Now;
            nk.Createdby = gaChuyenDon.ModifyBy;
            nk.CreatedName = gaChuyenDon.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return gaChuyenDon;
        }
    }
}
