using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CBWebApplication.Context;
using CBWebApplication.Models;

namespace CBWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeSoQdnlsController : ControllerBase
    {
        private readonly CoBaoDTContext _context;

        public HeSoQdnlsController(CoBaoDTContext context)
        {
            _context = context;
        }

        // GET: api/HeSoQdnls
        [HttpGet]
        [Route("GetByTraTim")]
        public async Task<ActionResult<IEnumerable<HeSoQdnl>>> GetByTraTim(string MaDV, int TuThang,int DenThang,int TuNam,int DenNam)
        {            
            var query = from item in _context.HeSoQDNL
                        where item.Thang>=TuThang && item.Thang<=DenThang && item.Nam >= TuNam && item.Nam<=DenNam
                        select item;
            if (MaDV != "TCT")
                query = query.Where(x => x.MaDv == MaDV);            
            return await query.ToListAsync();
        }

        // GET: api/HeSoQdnls/5
        [HttpGet]
        [Route("GetByID")]
        public async Task<ActionResult<HeSoQdnl>> GetByID(long id)
        {
            var heSoQdnl = await _context.HeSoQDNL.FindAsync(id);

            if (heSoQdnl == null)
            {
                return NotFound();
            }

            return heSoQdnl;
        }

        // PUT: api/HeSoQdnls/5       
        [HttpPut]
        [Route("PutByID")]
        public async Task<ActionResult<HeSoQdnl>> PutByID(long id, HeSoQdnl heSoQdnl)
        {            
            if (id != heSoQdnl.ID)
            {
                return BadRequest();
            }

            _context.Entry(heSoQdnl).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return heSoQdnl;
        }

        // POST: api/HeSoQdnls       
        [HttpPost]
        [Route("PostByID")]
        public async Task<ActionResult<HeSoQdnl>> PostByID(HeSoQdnl heSoQdnl)
        {
            var query = _context.HeSoQDNL.Where(x => x.MaDv == heSoQdnl.MaDv && x.Thang == heSoQdnl.Thang && x.Nam == heSoQdnl.Nam).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.HeSoQDNL.Add(heSoQdnl);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetByID", new { id = heSoQdnl.ID }, heSoQdnl);
        }

        // DELETE: api/HeSoQdnls/5
        [HttpDelete]
        [Route("DeleteByID")]
        public async Task<ActionResult<HeSoQdnl>> DeleteByID(long id)
        {
            var heSoQdnl = await _context.HeSoQDNL.FindAsync(id);
            if (heSoQdnl == null)
            {
                return NotFound();
            }
            _context.HeSoQDNL.Remove(heSoQdnl);
            await _context.SaveChangesAsync();

            return heSoQdnl;
        }
    }
}
