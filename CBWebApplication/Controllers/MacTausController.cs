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
    public class MacTausController : ControllerBase
    {
        private readonly CoBaoDTContext _context;

        public MacTausController(CoBaoDTContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetMacTau")]
        public async Task<ActionResult<IEnumerable<MacTau>>> GetMacTau(short CongTac,string MacTau)
        {
            var query = from item in _context.MacTau select item; ;
            if (CongTac != 0)
                query = query.Where(x => x.CongTacID == CongTac);
            if (!string.IsNullOrWhiteSpace(MacTau))
                query = query.Where(x=>x.MacTauID.Contains(MacTau));
            return await query.ToListAsync();
        }
        [HttpGet]
        [Route("GetMacTauNotInDTCT")]
        public async Task<ActionResult<IEnumerable<MacTau>>> GetMacTauNotInDTCT()
        {
            var querylistMactau = await _context.MacTau.Select(x=>x.MacTauID).ToListAsync();
            return await (from x in _context.DoanThongCT
                          where !querylistMactau.Contains(x.MacTauID)
                          group x by new { x.MacTauID, x.CongTacID, x.CongTacName } into g
                          select new MacTau
                          {
                              MacTauID = g.Key.MacTauID,
                              CongTacID = g.Key.CongTacID,
                              CongTacName = g.Key.CongTacName,
                              CreatedDate=DateTime.Now,
                              CreatedBy="admin",
                              CreatedName="Administrator",
                              ModifyDate=DateTime.Now,
                              ModifyBy="admin",
                              ModifyName="Administrator"
                          }).ToListAsync();
        }       

        [HttpPut]
        [Route("PutMacTau")]
        public async Task<ActionResult<MacTau>> PutMacTau(string id, MacTau macTau)
        {
            macTau.ModifyDate = DateTime.Now;           
            if (id != macTau.MacTauID)
            {
                return BadRequest();
            }
            _context.Entry(macTau).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return macTau;
        }

        [HttpPost]
        [Route("PostMacTau")]
        public async Task<ActionResult<MacTau>> PostMacTau(MacTau macTau)
        {
            macTau.CreatedDate = DateTime.Now;
            macTau.ModifyDate = macTau.CreatedDate;            
            var query = _context.MacTau.Where(x => x.MacTauID == macTau.MacTauID).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.MacTau.Add(macTau);
            await _context.SaveChangesAsync();

            return macTau;
        }

        [HttpDelete]
        [Route("DeleteMacTau")]
        public async Task<ActionResult<MacTau>> DeleteMacTau(string id)
        {
            var macTau = _context.MacTau.Where(x => x.MacTauID == id).FirstOrDefault();
            if (macTau == null)
            {
                return NotFound();
            }
            _context.MacTau.Remove(macTau);

            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "MacTau";
            nk.NoiDung = "Xóa mác tầu: " + macTau.MacTauID + "-Công tác: " + macTau.CongTacID +"-" +macTau.CongTacName;
            nk.Createddate = DateTime.Now;
            nk.Createdby = macTau.ModifyBy;
            nk.CreatedName = macTau.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return macTau;
        }
    }
}
