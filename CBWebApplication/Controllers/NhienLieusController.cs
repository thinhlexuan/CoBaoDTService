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
    public class NhienLieusController : ControllerBase
    {
        private readonly CoBaoDTContext _context;
        public NhienLieusController(CoBaoDTContext context)
        {
            _context = context;
        }

        #region NL_LoaiDauMo    
        [HttpGet]
        [Route("NLGetLoaiDauMo")]
        public async Task<ActionResult<IEnumerable<DMLoaiDauMo>>> NLGetLoaiDauMo(string tenDM)
        {
            var query = from item in _context.DMLoaiDauMo select item;
            if (!string.IsNullOrWhiteSpace(tenDM))
                query = query.Where(x => x.LoaiDauMo.Contains(tenDM));
            return await query.OrderBy(x => x.ID).ToListAsync();
        }
        [HttpPut]
        [Route("NLPutLoaiDauMo")]
        public async Task<ActionResult<DMLoaiDauMo>> NLPutLoaiDauMo(short id, DMLoaiDauMo nL_LoaiDauMo)
        {
            nL_LoaiDauMo.ModifyDate = DateTime.Now;
            if (id != nL_LoaiDauMo.ID)
            {
                return BadRequest();
            }
            _context.Entry(nL_LoaiDauMo).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return nL_LoaiDauMo;
        }

        [HttpPost]
        [Route("NLPostLoaiDauMo")]
        public async Task<ActionResult<DMLoaiDauMo>> NLPostLoaiDauMo(DMLoaiDauMo nL_LoaiDauMo)
        {
            nL_LoaiDauMo.CreatedDate = DateTime.Now;
            nL_LoaiDauMo.ModifyDate = nL_LoaiDauMo.CreatedDate;
            var query = _context.DMLoaiDauMo.Where(x => x.LoaiDauMo == nL_LoaiDauMo.LoaiDauMo && x.DonViTinh == nL_LoaiDauMo.DonViTinh).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.DMLoaiDauMo.Add(nL_LoaiDauMo);
            await _context.SaveChangesAsync();

            return nL_LoaiDauMo;
        }

        [HttpDelete]
        [Route("NLDeleteLoaiDauMo")]
        public async Task<ActionResult<DMLoaiDauMo>> NLDeleteLoaiDauMo(short id)
        {
            var nL_LoaiDauMo = _context.DMLoaiDauMo.Where(x => x.ID == id).FirstOrDefault();
            if (nL_LoaiDauMo == null)
            {
                return NotFound();
            }
            _context.DMLoaiDauMo.Remove(nL_LoaiDauMo);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "DMLoaiDauMo";
            nk.NoiDung = "Xóa loại dầu mỡ: " + nL_LoaiDauMo.LoaiDauMo + "-" + nL_LoaiDauMo.DonViTinh;
            nk.Createddate = DateTime.Now;
            nk.Createdby = nL_LoaiDauMo.ModifyBy;
            nk.CreatedName = nL_LoaiDauMo.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return nL_LoaiDauMo;
        }
        #endregion

        #region NL_NhaCC       
        [HttpGet]
        [Route("NLGetNhaCC")]
        public async Task<ActionResult<IEnumerable<NL_NhaCC>>> NLGetNhaCC(string tenNCC)
        {
            var query = from item in _context.NL_NhaCC select item;
            if (!string.IsNullOrWhiteSpace(tenNCC))
                query = query.Where(x => x.TenNCC.Contains(tenNCC));
            return await query.OrderBy(x => x.ID).ToListAsync();
        }
        [HttpPut]
        [Route("NLPutNhaCC")]
        public async Task<ActionResult<NL_NhaCC>> NLPutNhaCC(long id, NL_NhaCC nL_NhaCC)
        {
            nL_NhaCC.ModifyDate = DateTime.Now;
            if (id != nL_NhaCC.ID)
            {
                return BadRequest();
            }
            _context.Entry(nL_NhaCC).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return nL_NhaCC;
        }

        [HttpPost]
        [Route("NLPostNhaCC")]
        public async Task<ActionResult<NL_NhaCC>> NLPostNhaCC(NL_NhaCC nL_NhaCC)
        {
            nL_NhaCC.CreatedDate = DateTime.Now;
            nL_NhaCC.ModifyDate = nL_NhaCC.CreatedDate;
            var query = _context.NL_NhaCC.Where(x => x.TenTat == nL_NhaCC.TenTat && x.TenNCC==nL_NhaCC.TenNCC).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.NL_NhaCC.Add(nL_NhaCC);
            await _context.SaveChangesAsync();

            return nL_NhaCC;
        }

        [HttpDelete]
        [Route("NLDeleteNhaCC")]
        public async Task<ActionResult<NL_NhaCC>> NLDeleteNhaCC(long id)
        {
            var nL_NhaCC = _context.NL_NhaCC.Where(x => x.ID == id).FirstOrDefault();
            if (nL_NhaCC == null)
            {
                return NotFound();
            }
            _context.NL_NhaCC.Remove(nL_NhaCC);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "NL_NhaCC";
            nk.NoiDung = "Xóa nhà cung cấp: " + nL_NhaCC.TenTat+"-"+nL_NhaCC.TenNCC;
            nk.Createddate = DateTime.Now;
            nk.Createdby = nL_NhaCC.ModifyBy;
            nk.CreatedName = nL_NhaCC.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return nL_NhaCC ;
        }
        #endregion

        #region NL_HopDong       
        [HttpGet]
        [Route("NLGetHopDong")]
        public async Task<ActionResult<IEnumerable<NL_HopDong>>> NLGetHopDong(string tenNCC, string hopDong)
        {
            var query = from item in _context.NL_HopDong select item;
            if (!string.IsNullOrWhiteSpace(tenNCC))
                query = query.Where(x => x.TenNCC.Contains(tenNCC));
            if (!string.IsNullOrWhiteSpace(hopDong))
                query = query.Where(x => x.HopDong.Contains(hopDong));
            return await query.OrderBy(x => x.ID).ToListAsync();
        }
        [HttpPut]
        [Route("NLPutHopDong")]
        public async Task<ActionResult<NL_HopDong>> NLPutHopDong(long id, NL_HopDong nL_HopDong)
        {
            nL_HopDong.ModifyDate = DateTime.Now;
            if (id != nL_HopDong.ID)
            {
                return BadRequest();
            }
            _context.Entry(nL_HopDong).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return nL_HopDong;
        }

        [HttpPost]
        [Route("NLPostHopDong")]
        public async Task<ActionResult<NL_HopDong>> NLPostHopDong(NL_HopDong nL_HopDong)
        {
            nL_HopDong.CreatedDate = DateTime.Now;
            nL_HopDong.ModifyDate = nL_HopDong.CreatedDate;
            var query = _context.NL_HopDong.Where(x => x.MaNCC == nL_HopDong.MaNCC && x.HopDong == nL_HopDong.HopDong).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.NL_HopDong.Add(nL_HopDong);
            await _context.SaveChangesAsync();

            return nL_HopDong;
        }

        [HttpDelete]
        [Route("NLDeleteHopDong")]
        public async Task<ActionResult<NL_HopDong>> NLDeleteHopDong(long id)
        {
            var nL_HopDong = _context.NL_HopDong.Where(x => x.ID == id).FirstOrDefault();
            if (nL_HopDong == null)
            {
                return NotFound();
            }
            _context.NL_HopDong.Remove(nL_HopDong);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "NL_HopDong";
            nk.NoiDung = "Xóa hợp đồng: " + nL_HopDong.TenNCC + "-" + nL_HopDong.HopDong;
            nk.Createddate = DateTime.Now;
            nk.Createdby = nL_HopDong.ModifyBy;
            nk.CreatedName = nL_HopDong.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return nL_HopDong;
        }
        #endregion

        #region NL_BangGia       
        [HttpGet]
        [Route("NLGetBangGia")]
        public async Task<ActionResult<IEnumerable<NL_BangGia>>> NLGetBangGia(string maTram, short maDauMo, DateTime ngayBD, DateTime ngayKT)
        {
            ngayKT = ngayKT.AddDays(1);
            var query = from item in _context.NL_BangGia where item.NgayHL >= ngayBD && item.NgayHL < ngayKT select item;
            if (!string.IsNullOrWhiteSpace(maTram))
                query = query.Where(x => maTram.Contains(x.MaTramNL));
            if (maDauMo >= 0)
                query = query.Where(x => x.MaDauMo == maDauMo);
            return await query.ToListAsync();
        }
        [HttpGet]
        [Route("NLGetBangGiaOBJ")]
        public async Task<ActionResult<NL_BangGia>> NLGetBangGiaOBJ(string maTram, short maDauMo, DateTime ngayHL)
        {
            var query = await  (from item in _context.NL_BangGia
                         where item.NgayHL <= ngayHL && item.MaTramNL == maTram && item.MaDauMo==maDauMo
                         orderby item.NgayHL descending
                                select item).FirstOrDefaultAsync();          
                return query;
        }
        [HttpPut]
        [Route("NLPutBangGia")]
        public async Task<ActionResult<NL_BangGia>> NLPutBangGia(NL_BangGia nL_BangGia)
        {
            nL_BangGia.ModifyDate = DateTime.Now;           
            _context.Entry(nL_BangGia).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return nL_BangGia;
        }

        [HttpPost]
        [Route("NLPostBangGia")]
        public async Task<ActionResult<NL_BangGia>> NLPostBangGia(NL_BangGia nL_BangGia)
        {
            nL_BangGia.CreatedDate = DateTime.Now;
            nL_BangGia.ModifyDate = nL_BangGia.CreatedDate;
            var query = _context.NL_BangGia.Where(x => x.MaTramNL == nL_BangGia.MaTramNL && x.MaDauMo == nL_BangGia.MaDauMo && x.NgayHL==nL_BangGia.NgayHL).FirstOrDefault();
            if (query != null)
                return NotFound();
            _context.NL_BangGia.Add(nL_BangGia);
            await _context.SaveChangesAsync();

            return nL_BangGia;
        }

        [HttpDelete]
        [Route("NLDeleteBangGia")]
        public async Task<ActionResult<NL_BangGia>> NLDeleteBangGia(string maTram,short maDauMo, DateTime ngayHL)
        {
            var query = _context.NL_BangGia.Where(x => x.MaTramNL == maTram && x.MaDauMo == maDauMo && x.NgayHL == ngayHL).FirstOrDefault();
            if (query == null)
            {
                return NotFound();
            }
            _context.NL_BangGia.Remove(query);
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "NL_BangGia";
            nk.NoiDung = "Xóa giá: " + query.TenTramNL + "-" + query.TenDauMo+"-"+query.NgayHL;
            nk.Createddate = DateTime.Now;
            nk.Createdby = query.ModifyBy;
            nk.CreatedName = query.ModifyName;
            _context.NhatKy.Add(nk);

            await _context.SaveChangesAsync();
            return query;
        }
        #endregion

        #region NL_54BASTM
        [HttpGet]
        [Route("NLGet54BASTM")]        
        public async Task<ActionResult<IEnumerable<NL_54BASTM>>> NLGet54BASTM()
        {
            return await _context.NL_54BASTM.ToListAsync();
        }
        #endregion

        #region NL_PhieuNhap       
        [HttpGet]
        [Route("NLGetPhieuNhap")]
        public async Task<ActionResult<IEnumerable<NL_PhieuNhap>>> NLGetPhieuNhap(string maTram, int maNCC,string maPN, DateTime ngayBD, DateTime ngayKT)
        {
            ngayKT = ngayKT.AddDays(1);
            var query = from item in _context.NL_PhieuNhap where item.NgayNhap >= ngayBD && item.NgayNhap < ngayKT select item;
            if (!string.IsNullOrWhiteSpace(maTram))
                query = query.Where(x => maTram.Contains(x.MaTramNL));
            if (maNCC > 0)
                query = query.Where(x => x.MaNCC == maNCC);
            if(!string.IsNullOrWhiteSpace(maPN))
                query = query.Where(x => x.PhieuNhapID.ToString().Contains(maPN));
            var phieuNhap = await query.ToListAsync();
            foreach (var pn in phieuNhap)
            {
                var phieuNhapCT = await _context.NL_PhieuNhapCT.Where(x => x.PhieuNhapID == pn.PhieuNhapID).ToListAsync();
                pn.NL_PhieuNhapCTs = phieuNhapCT;
            }    
            return phieuNhap.ToList();
        }
        [HttpGet]
        [Route("NLGetPhieuNhapOBJ")]
        public async Task<ActionResult<NL_PhieuNhap>> NLGetPhieuNhapOBJ(long id)
        {            
            var phieuNhap = await  (from item in _context.NL_PhieuNhap where item.PhieuNhapID==id select item).FirstOrDefaultAsync();
            var phieuNhapCT = await _context.NL_PhieuNhapCT.Where(x => x.PhieuNhapID == id).ToListAsync();
            phieuNhap.NL_PhieuNhapCTs = phieuNhapCT;           
            return phieuNhap;
        }
        [HttpPut]
        [Route("NLPutPhieuNhap")]
        public async Task<ActionResult<NL_PhieuNhap>> NLPutPhieuNhap(NL_PhieuNhap nL_PhieuNhap)
        {            
            var phieuNhapCTOld = nL_PhieuNhap.NL_PhieuNhapCTs.ToList();
            nL_PhieuNhap.ModifyDate = DateTime.Now;
            _context.Entry(nL_PhieuNhap).State = EntityState.Modified;
            //Xóa hết phiếu nhập chi tiết           
            var phieuNhapCT = await _context.NL_PhieuNhapCT.Where(x => x.PhieuNhapID == nL_PhieuNhap.PhieuNhapID).ToListAsync();
            //if (phieuNhapCT != null)
            //{
            //    _context.NL_PhieuNhapCT.RemoveRange(phieuNhapCT);
            //}
            //Xóa hết bảng giá của phiếu nhập           
            var listBangGia = await _context.NL_BangGia.Where(x => x.MaTramNL == nL_PhieuNhap.MaTramNL && x.PhieuNhapID == nL_PhieuNhap.PhieuNhapID).ToListAsync();
            if (listBangGia != null)
            {
                _context.NL_BangGia.RemoveRange(listBangGia);
            }
            //Thêm mới phiếu nhập chi tiết            
            if (phieuNhapCTOld != null)
            {
                foreach (var pnctDB in phieuNhapCT)
                {
                    //Xóa chi tiết trong db mà không có trong danh sách phiếu xuất ct
                    var pnctDelete = phieuNhapCTOld.Where(x => x.PhieuNhapID == pnctDB.PhieuNhapID && x.MaDauMo == pnctDB.MaDauMo).FirstOrDefault();
                    if (pnctDelete == null && pnctDB.SoLuong == pnctDB.ConLai)
                    {
                        _context.NL_PhieuNhapCT.Remove(pnctDB);                       
                    }
                }
                foreach (NL_PhieuNhapCT ct in phieuNhapCTOld)
                {
                    //Thêm giá vào bảng giá
                    var bangGia = await _context.NL_BangGia.Where(x => x.MaTramNL == nL_PhieuNhap.MaTramNL && x.MaDauMo == ct.MaDauMo && x.NgayHL == nL_PhieuNhap.NgayNhap).FirstOrDefaultAsync();
                    if (bangGia == null)
                    {
                        await NL_NapBangGia(nL_PhieuNhap, ct);
                    }
                    var pnctEdit = phieuNhapCT.Where(x => x.PhieuNhapID == ct.PhieuNhapID && x.MaDauMo == ct.MaDauMo).FirstOrDefault();
                    //Nếu không có thì thêm mới
                    if (pnctEdit == null)
                    {
                        _context.NL_PhieuNhapCT.Add(ct);
                    }
                    //Nếu không sửa lại số lượng thì cho phép cập nhật lại các trường khác
                    else if(pnctEdit.SoLuongVCF==ct.SoLuongVCF)
                    {
                        pnctEdit.PhieuNhapID = ct.PhieuNhapID;
                        pnctEdit.MaDauMo = ct.MaDauMo;
                        pnctEdit.TenDauMo = ct.TenDauMo;
                        pnctEdit.DonViTinh = ct.DonViTinh;
                        pnctEdit.NhietDo = ct.NhietDo;
                        pnctEdit.TyTrong = ct.TyTrong;
                        pnctEdit.VCF = ct.VCF;
                        pnctEdit.SoLuong = ct.SoLuong;
                        //pnctEdit.SoLuongVCF = ct.SoLuongVCF;
                        //pnctEdit.ConLai = ct.ConLai;
                        pnctEdit.DonGia = ct.DonGia;
                        pnctEdit.ThanhTien = ct.SoLuongVCF * ct.DonGia;
                        _context.Entry(pnctEdit).State = EntityState.Modified;
                        //Nếu có thay đổi giá thì cập nhật lại các phiếu xuất theo giá thay đổi
                        //if(pnctEdit.DonGia!=ct.DonGia)
                        //{
                            bangGia = await _context.NL_BangGia.Where(x => x.MaTramNL == nL_PhieuNhap.MaTramNL && x.MaDauMo == ct.MaDauMo && x.NgayHL > nL_PhieuNhap.NgayNhap).FirstOrDefaultAsync();
                            DateTime ngayKT = bangGia != null ? bangGia.NgayHL : new DateTime(2099, 12, 31);
                            var pxctList = await (from pxct in _context.NL_PhieuXuatCT
                                                  join px in _context.NL_PhieuXuat on pxct.PhieuXuatID equals px.PhieuXuatID
                                                  where px.MaTramNL == nL_PhieuNhap.MaTramNL && pxct.MaDauMo == ct.MaDauMo && px.NgayXuat >= nL_PhieuNhap.NgayNhap && px.NgayXuat < ngayKT
                                                  select pxct).ToListAsync();
                            foreach(var pxctDB in pxctList)
                            {
                                pxctDB.BangGiaID = ct.PhieuNhapID;
                                pxctDB.DonGia = ct.DonGia;
                                pxctDB.ThanhTien = pxctDB.SoLuongVCF * ct.DonGia;
                                _context.Entry(pxctDB).State = EntityState.Modified;
                            }    
                        //}        
                    }
                }
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            var listPhieuNhapCT = await _context.NL_PhieuNhapCT.Where(x => x.PhieuNhapID == nL_PhieuNhap.PhieuNhapID).ToListAsync();
            nL_PhieuNhap.NL_PhieuNhapCTs = listPhieuNhapCT;
            return nL_PhieuNhap;
        }
        
        [HttpPost]
        [Route("NLPostPhieuNhap")]
        public async Task<ActionResult<NL_PhieuNhap>> NLPostPhieuNhap(NL_PhieuNhap nL_PhieuNhap)
        {
            //Nếu đã nhập phiếu nhập cho trạm ấy rồi thì không nhập nữa
            //var query = _context.NL_PhieuNhap.Where(x => x.MaTramNL == nL_PhieuNhap.MaTramNL && x.NgayNhap == nL_PhieuNhap.NgayNhap).FirstOrDefault();
            //if (query != null)
            //    return NotFound();
            //Thêm phiếu nhập
            nL_PhieuNhap.CreatedDate = DateTime.Now;
            nL_PhieuNhap.ModifyDate = nL_PhieuNhap.CreatedDate;
            var phieuNhapID = _context.NL_PhieuNhap.OrderByDescending(x => x.PhieuNhapID).FirstOrDefault();
            nL_PhieuNhap.PhieuNhapID = phieuNhapID != null ? phieuNhapID.PhieuNhapID + 1 : 1;
            if (nL_PhieuNhap.NL_PhieuNhapCTs != null)
            {
                foreach (var ct in nL_PhieuNhap.NL_PhieuNhapCTs)
                {
                    ct.PhieuNhapID = nL_PhieuNhap.PhieuNhapID;                   
                    //Thêm giá vào bảng giá
                    var bangGia = _context.NL_BangGia.Where(x => x.MaTramNL == nL_PhieuNhap.MaTramNL && x.MaDauMo == ct.MaDauMo && x.NgayHL == nL_PhieuNhap.NgayNhap).FirstOrDefault();
                    if (bangGia==null)
                    {
                        await NL_NapBangGia(nL_PhieuNhap, ct);
                    }
                }
            }
            _context.NL_PhieuNhap.Add(nL_PhieuNhap);
           
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return nL_PhieuNhap;
        }

        private async Task NL_NapBangGia(NL_PhieuNhap nL_PhieuNhap, NL_PhieuNhapCT ct)
        {
            //Lấy giá nhập của kỳ trước
            var giaKytruoc = await (from gia in _context.NL_BangGia where gia.MaTramNL == nL_PhieuNhap.MaTramNL && gia.MaDauMo == ct.MaDauMo && gia.NgayHL < nL_PhieuNhap.NgayNhap 
                                    orderby gia.NgayHL descending select gia).FirstOrDefaultAsync();
            DateTime ngayBD = giaKytruoc != null ? giaKytruoc.NgayHL : new DateTime(2023, 1, 1);
            //Lấy tất cả còn lại của loại dầu mỡ ấy trở về trước để tính lại tổng tiền
            var nhapDK = await (from pnct in _context.NL_PhieuNhapCT
                                     join pn in _context.NL_PhieuNhap on pnct.PhieuNhapID equals pn.PhieuNhapID
                                     where pn.MaTramNL==nL_PhieuNhap.MaTramNL && pnct.MaDauMo == ct.MaDauMo && pn.NgayNhap>ngayBD && pn.NgayNhap < nL_PhieuNhap.NgayNhap && pnct.ConLai > 0
                                     select new
                                     {
                                         MaDauMo = (short)pnct.MaDauMo,
                                         SoLuongVCF = (decimal)(pnct.ConLai),
                                         SoLuongTyTrong = (decimal)(pnct.ConLai* pnct.TyTrong),
                                         ThanhTien = (decimal)(pnct.ConLai * pnct.DonGia)
                                     }
                               ).ToListAsync();
            //Lấy số lượng và mã phiếu nhập các phiếu xuất trong kỳ trở về sau
            var xuatDK = await (from pxct in _context.NL_PhieuXuatCT
                                join px in _context.NL_PhieuXuat on pxct.PhieuXuatID equals px.PhieuXuatID
                                join pn in _context.NL_PhieuNhap on pxct.PhieuNhapID equals pn.PhieuNhapID
                                join pnct in _context.NL_PhieuNhapCT on new { pxct.PhieuNhapID, pxct.MaDauMo } equals new { pnct.PhieuNhapID, pnct.MaDauMo }
                                where pn.MaTramNL == nL_PhieuNhap.MaTramNL && pxct.MaDauMo == ct.MaDauMo && px.NgayXuat>=nL_PhieuNhap.NgayNhap && pn.NgayNhap > ngayBD && pn.NgayNhap < nL_PhieuNhap.NgayNhap
                                select new
                                {
                                    MaDauMo = (short)pxct.MaDauMo,
                                    SoLuongVCF = (decimal)(pxct.SoLuongVCF),
                                    SoLuongTyTrong = (decimal)(pxct.SoLuongVCF * pnct.TyTrong),
                                    ThanhTien = (decimal)(pxct.SoLuongVCF * pnct.DonGia)                                
                                }).ToListAsync();
            var tonDK = nhapDK.Concat(xuatDK).ToList();

            var phieuNhapCTCL = (from pnct in tonDK
                                 group pnct by new { pnct.MaDauMo } into g
                                 select new
                                 {
                                     MaDauMo = (short)g.Key.MaDauMo,
                                     SoLuongVCF = (decimal)g.Sum(x => x.SoLuongVCF),
                                     SoLuongTyTrong = (decimal)g.Sum(x=>x.SoLuongTyTrong),
                                     ThanhTien = (decimal)g.Sum(x => x.ThanhTien)
                                 }
                              ).FirstOrDefault();
            decimal donGia = ct.DonGia;
            decimal tyTrong = ct.TyTrong;
            if (donGia > 0 && !string.IsNullOrWhiteSpace(nL_PhieuNhap.MaTramNL))
            {
                if (phieuNhapCTCL!=null)
                {
                    decimal soLuongVCF = phieuNhapCTCL.SoLuongVCF + ct.SoLuongVCF;
                    decimal soLuongTyTrong = phieuNhapCTCL.SoLuongTyTrong + (ct.SoLuongVCF * ct.TyTrong);
                    decimal thanhTien = phieuNhapCTCL.ThanhTien + (ct.SoLuongVCF * donGia);
                    donGia = thanhTien / soLuongVCF;
                    tyTrong = soLuongTyTrong / soLuongVCF;
                }
                NL_BangGia bg = new NL_BangGia();
                bg.MaTramNL = nL_PhieuNhap.MaTramNL;
                bg.TenTramNL = nL_PhieuNhap.TenTramNL;
                bg.MaDauMo = ct.MaDauMo;
                bg.TenDauMo = ct.TenDauMo;
                bg.DonViTinh = ct.DonViTinh;
                bg.NgayHL = nL_PhieuNhap.NgayNhap;
                bg.PhieuNhapID = nL_PhieuNhap.PhieuNhapID;
                bg.DonGia = donGia;
                bg.TyTrong = tyTrong;
                bg.GhiChu = "Giá theo phiếu nhập";
                bg.CreatedDate = nL_PhieuNhap.CreatedDate;
                bg.CreatedBy = nL_PhieuNhap.CreatedBy;
                bg.CreatedName = nL_PhieuNhap.CreatedName;
                bg.ModifyDate = nL_PhieuNhap.ModifyDate;
                bg.ModifyBy = nL_PhieuNhap.ModifyBy;
                bg.ModifyName = nL_PhieuNhap.ModifyName;
                _context.NL_BangGia.Add(bg);
            }
        }

        [HttpDelete]
        [Route("NLDeletePhieuNhap")]
        public async Task<ActionResult<NL_PhieuNhap>> NLDeletePhieuNhap(long id, string maNV, string tenNV)
        {
            var phieuNhap = await _context.NL_PhieuNhap.FindAsync(id);
            if (phieuNhap == null)
            {
                return NotFound();
            }
            phieuNhap.NL_PhieuNhapCTs = await _context.NL_PhieuNhapCT.Where(x => x.PhieuNhapID == phieuNhap.PhieuNhapID).ToListAsync();
            //Kiểm tra xem phiếu nhập đã xuất chưa
            var query = phieuNhap.NL_PhieuNhapCTs.Where(x => x.SoLuongVCF != x.ConLai).FirstOrDefault();
            if (query != null)
            {
                return BadRequest();
            }
            _context.NL_PhieuNhap.Remove(phieuNhap);
            //Xóa hết phiếu nhập chi tiết                      
            if (phieuNhap.NL_PhieuNhapCTs != null)
            {
                _context.NL_PhieuNhapCT.RemoveRange(phieuNhap.NL_PhieuNhapCTs);
            }
            //Xóa hết bảng giá của phiếu nhập           
            var listBangGia = await _context.NL_BangGia.Where(x => x.PhieuNhapID == phieuNhap.PhieuNhapID).ToListAsync();
            if (listBangGia != null)
            {
                _context.NL_BangGia.RemoveRange(listBangGia);
            }
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "NL_PhieuNhap";
            nk.NoiDung = "Xóa phiếu nhập: " + phieuNhap.PhieuNhapID+ "-" + phieuNhap.TenTramNL + "-" + phieuNhap.NgayNhap;
            nk.Createddate = DateTime.Now;
            nk.Createdby = maNV;
            nk.CreatedName = tenNV;
            _context.NhatKy.Add(nk);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return phieuNhap;
        }

        [HttpPut]
        [Route("NLKhoaPhieuNhap")]
        public async Task<ActionResult<NL_PhieuNhap>> NLKhoaPhieuNhap(NL_PhieuNhap nL_PhieuNhap)
        {
            nL_PhieuNhap.ModifyDate = DateTime.Now;
            _context.Entry(nL_PhieuNhap).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return nL_PhieuNhap;
        }
        #endregion

        #region NL_PhieuXuat       
        [HttpGet]
        [Route("NLGetPhieuXuat")]
        public async Task<ActionResult<IEnumerable<NL_PhieuXuat>>> NLGetPhieuXuat(string maTram, string loaiMay, string dauMay,string maPX, DateTime ngayBD, DateTime ngayKT)
        {
            ngayKT = ngayKT.AddDays(1);
            var query = from item in _context.NL_PhieuXuat where item.NgayXuat >= ngayBD && item.NgayXuat < ngayKT select item;
            if (!string.IsNullOrWhiteSpace(maTram))
                query = query.Where(x => maTram.Contains(x.MaTramNL));
            if (loaiMay != "ALL")
                query = query.Where(x => x.LoaiMayID == loaiMay);
            if (!string.IsNullOrWhiteSpace(dauMay))
                query = query.Where(x => x.DauMayID.Contains(dauMay));
            if (!string.IsNullOrWhiteSpace(maPX))
                query = query.Where(x => x.PhieuXuatID.ToString().Contains(maPX));
            var phieuXuat = await query.ToListAsync();
            foreach (var px in phieuXuat)
            {
                var phieuXuatCT = await _context.NL_PhieuXuatCT.Where(x => x.PhieuXuatID == px.PhieuXuatID).ToListAsync();
                px.NL_PhieuXuatCTs = phieuXuatCT;
            }
            return phieuXuat.ToList();
        }
        [HttpGet]
        [Route("NLGetPhieuXuatOBJ")]
        public async Task<ActionResult<NL_PhieuXuat>> NLGetPhieuXuatOBJ(long id)
        {
            var phieuXuat = await (from item in _context.NL_PhieuXuat where item.PhieuXuatID == id select item).FirstOrDefaultAsync();
            var phieuXuatCT = await _context.NL_PhieuXuatCT.Where(x => x.PhieuXuatID == id).ToListAsync();
            phieuXuat.NL_PhieuXuatCTs = phieuXuatCT;
            return phieuXuat;
        }
        [HttpGet]
        [Route("NLGetConLaiOBJ")]
        public async Task<ActionResult<decimal>> NLGetConLaiOBJ(string maTram, short maDauMo, DateTime ngayHL)
        {
            var phieuNhapCTCL = await (from pnct in _context.NL_PhieuNhapCT
                                       join pn in _context.NL_PhieuNhap on pnct.PhieuNhapID equals pn.PhieuNhapID
                                       where pn.MaTramNL == maTram && pnct.MaDauMo == maDauMo && pn.NgayNhap <= ngayHL && pnct.ConLai > 0
                                       group pnct by new { pnct.MaDauMo } into g
                                       select new
                                       {
                                           MaDauMo = (short)g.Key.MaDauMo,
                                           SoLuong = (decimal)g.Sum(x => x.ConLai)
                                       }).ToListAsync();
            if (phieuNhapCTCL.Count > 0)
                return phieuNhapCTCL.Sum(x => x.SoLuong);
            else
                return 0;
        }

        [HttpGet]
        [Route("GetCoBaoOBJ")]
        public async Task<ActionResult<CoBao>> GetCoBaoOBJ(DateTime ngayCB, string dauMay)
        {
            var coBao = await (from cb in _context.CoBao                         
                         where ngayCB >= cb.NhanMay && ngayCB <= cb.GiaoMay && cb.DauMayID == dauMay
                         select cb).FirstOrDefaultAsync();
            if (coBao != null)
            {
                var coBaoCT = await _context.CoBaoCT.Where(x => x.CoBaoID == coBao.CoBaoID).ToListAsync();
                foreach (CoBaoCT ct in coBaoCT)
                    coBao.coBaoCTs.Add(ct);
            }
            return coBao;
        }
        [HttpGet]
        [Route("GetCoBaoGAOBJ")]
        public async Task<ActionResult<CoBaoGA>> GetCoBaoGAOBJ(DateTime ngayCB, string dauMay)
        {
            var coBao = await (from cb in _context.CoBaoGA                               
                               where ngayCB >= cb.NhanMay && ngayCB <= cb.GiaoMay && cb.DauMayID == dauMay
                               select cb).FirstOrDefaultAsync();
            if (coBao != null)
            {
                var coBaoCT = await _context.CoBaoGACT.Where(x => x.CoBaoID == coBao.CoBaoID).ToListAsync();
                foreach (CoBaoGACT ct in coBaoCT)
                    coBao.coBaoGACTs.Add(ct);
            }            
            return coBao;
        }
        private async Task PhieuXuatCT_Themmoi(NL_PhieuXuat nL_PhieuXuat, NL_PhieuXuatCT ct)
        {
            var phieuNhapCTCL = await (from pnct in _context.NL_PhieuNhapCT
                                       join pn in _context.NL_PhieuNhap on pnct.PhieuNhapID equals pn.PhieuNhapID
                                       where pn.MaTramNL == nL_PhieuXuat.MaTramNL && pnct.MaDauMo == ct.MaDauMo && pn.NgayNhap <= nL_PhieuXuat.NgayXuat && pnct.ConLai > 0
                                       group pnct by new { pn.PhieuNhapID, pn.NgayNhap, pnct.MaDauMo } into g
                                       select new
                                       {
                                           PhieuNhapID = (long)g.Key.PhieuNhapID,
                                           NgayNhap = (DateTime)g.Key.NgayNhap,
                                           MaDauMo = (short)g.Key.MaDauMo,
                                           SoLuong = (decimal)g.Sum(x => x.ConLai)
                                       }
                                            ).ToListAsync();
            if (phieuNhapCTCL.Count > 0)
            {
                decimal soLuong = ct.SoLuongVCF;
                phieuNhapCTCL = phieuNhapCTCL.OrderBy(x => x.NgayNhap).ToList();
                foreach (var pnct in phieuNhapCTCL)
                {
                    //Nếu số lượng xuất bằng 0 thì thoát khỏi mã dầu mỡ cần xuất
                    if (soLuong <= 0) break;
                    var pnctDBNew = await _context.NL_PhieuNhapCT.Where(x => x.PhieuNhapID == pnct.PhieuNhapID && x.MaDauMo == pnct.MaDauMo).FirstOrDefaultAsync();
                    if (pnctDBNew != null)
                    {
                        var sumSoluongXuat = await _context.NL_PhieuXuatCT.Where(x => x.PhieuNhapID == ct.PhieuNhapID && x.MaDauMo == ct.MaDauMo).SumAsync(x => x.SoLuongVCF);
                        //Nếu mà lượng xuất rồi lớn hơn lượng nhập của phiếu nhập ấy thì cập nhật còn lại của phiếu nhập ấy về 0 để không xuất được nữa
                        if (Math.Round(sumSoluongXuat, 4) >= Math.Round(pnctDBNew.SoLuongVCF, 4))
                        {
                            pnctDBNew.ConLai = 0;
                            _context.Entry(pnctDBNew).State = EntityState.Modified;
                        }
                        //Nếu còn lại còn thì xuất và cập nhật còn lại cho phiếu nhập ấy
                        else
                        {
                            decimal conLai = pnctDBNew.ConLai;
                            //Cập nhật còn lại của phiếu nhập chi tiết
                            pnctDBNew.ConLai = conLai > soLuong ? conLai - soLuong : 0;
                            _context.Entry(pnctDBNew).State = EntityState.Modified;
                            //Thêm phiếu xuất chi tiết
                            NL_PhieuXuatCT pcxctDB = new NL_PhieuXuatCT();
                            pcxctDB.PhieuXuatID = nL_PhieuXuat.PhieuXuatID;
                            pcxctDB.MaDauMo = ct.MaDauMo;
                            pcxctDB.TenDauMo = ct.TenDauMo;
                            pcxctDB.DonViTinh = ct.DonViTinh;
                            pcxctDB.NhietDo = ct.NhietDo;
                            pcxctDB.TyTrong = ct.TyTrong;
                            pcxctDB.VCF = ct.VCF;
                            pcxctDB.BangGiaID = ct.BangGiaID;
                            pcxctDB.DonGia = ct.DonGia;
                            pcxctDB.PhieuNhapID = pnctDBNew.PhieuNhapID;
                            pcxctDB.SoLuongVCF = conLai > soLuong ? soLuong : conLai;
                            pcxctDB.SoLuong = Math.Round(pcxctDB.SoLuongVCF / ct.VCF, 4);
                            pcxctDB.ThanhTien = pcxctDB.SoLuongVCF * ct.DonGia;
                            soLuong = conLai >= soLuong ? 0 : soLuong - conLai;
                            _context.NL_PhieuXuatCT.Add(pcxctDB);
                        }
                    }
                }
            }
        }
        [HttpPut]
        [Route("NLPutPhieuXuat")]
        public async Task<ActionResult<NL_PhieuXuat>> NLPutPhieuXuat(NL_PhieuXuat nL_PhieuXuat)
        {
            var phieuXuatCTOld = nL_PhieuXuat.NL_PhieuXuatCTs.ToList();
            var phieuXuatCT = await _context.NL_PhieuXuatCT.Where(x => x.PhieuXuatID == nL_PhieuXuat.PhieuXuatID).ToListAsync();
            if (phieuXuatCTOld != null)
            {
                foreach (var pxctDB in phieuXuatCT)
                {
                    //Xóa chi tiết trong db mà không có trong danh sách phiếu xuất ct
                    var pxctDelete = phieuXuatCTOld.Where(x => x.PhieuXuatID == pxctDB.PhieuXuatID && x.MaDauMo == pxctDB.MaDauMo && x.PhieuNhapID == pxctDB.PhieuNhapID).FirstOrDefault();
                    if (pxctDelete == null)
                    {
                        var pnctDB = await _context.NL_PhieuNhapCT.Where(x => x.PhieuNhapID == pxctDB.PhieuNhapID && x.MaDauMo == pxctDB.MaDauMo).FirstOrDefaultAsync();
                        var sumSoluongXuat = await _context.NL_PhieuXuatCT.Where(x => x.PhieuNhapID == pxctDB.PhieuNhapID && x.MaDauMo == pxctDB.MaDauMo
                        && x.PhieuXuatID != pxctDB.PhieuXuatID).SumAsync(x => x.SoLuongVCF);
                        decimal chenhLech = pnctDB.SoLuongVCF - sumSoluongXuat;
                        if (chenhLech <= 0)
                            pnctDB.ConLai = 0;
                        else
                            pnctDB.ConLai += pxctDB.SoLuongVCF <= chenhLech ? pxctDB.SoLuongVCF : chenhLech;
                        _context.Entry(pnctDB).State = EntityState.Modified;
                        _context.NL_PhieuXuatCT.Remove(pxctDB);
                    }
                }
                foreach (NL_PhieuXuatCT ct in phieuXuatCTOld)
                {                    
                    //Thêm mới phiếu xuất chi tiết
                    if (ct.PhieuNhapID == 0)
                    {
                        await PhieuXuatCT_Themmoi(nL_PhieuXuat, ct);
                    }                     
                    //Sửa chi tiết
                    else
                    {   
                        var pnctDB = await _context.NL_PhieuNhapCT.Where(x => x.PhieuNhapID == ct.PhieuNhapID && x.MaDauMo == ct.MaDauMo).FirstOrDefaultAsync();
                       var pxctDB = phieuXuatCT.Where(x => x.PhieuXuatID==ct.PhieuXuatID && x.PhieuNhapID == ct.PhieuNhapID && x.MaDauMo == ct.MaDauMo).FirstOrDefault();                       
                        decimal chenhLech = Math.Round(ct.SoLuongVCF,4)  - Math.Round(pxctDB.SoLuongVCF, 4);
                        //Nếu số lượng sửa nhỏ hơn số lượng đã lưu thì update lại phiếu xuất chi tiết và cộng thêm còn lại của phiếu nhập chi tiết
                        pxctDB.PhieuXuatID = ct.PhieuXuatID;
                        pxctDB.MaDauMo = ct.MaDauMo;
                        pxctDB.TenDauMo = ct.TenDauMo;
                        pxctDB.DonViTinh = ct.DonViTinh;
                        pxctDB.NhietDo = ct.NhietDo;
                        pxctDB.TyTrong = ct.TyTrong;
                        pxctDB.VCF = ct.VCF;
                        pxctDB.BangGiaID = ct.BangGiaID;
                        pxctDB.DonGia = ct.DonGia;
                        pxctDB.PhieuNhapID = ct.PhieuNhapID;
                        pxctDB.SoLuongVCF = ct.SoLuongVCF;
                        pxctDB.SoLuong = ct.SoLuong;
                        pxctDB.ThanhTien = ct.ThanhTien;
                        if (chenhLech < 0)
                        {
                            pnctDB.ConLai -= chenhLech;                            
                            chenhLech = 0;                            
                            _context.Entry(pnctDB).State = EntityState.Modified;
                            _context.Entry(pxctDB).State = EntityState.Modified;                           
                        }
                        //Nếu phiếu nhập của phiếu xuất ấy vẫn còn còn lại
                        else if (pnctDB.ConLai > 0 && chenhLech > 0)
                        {
                            decimal conLai = pnctDB.ConLai;
                            pnctDB.ConLai -= conLai >= chenhLech ? chenhLech : conLai;
                            _context.Entry(pnctDB).State = EntityState.Modified;
                            chenhLech -= conLai >= chenhLech ? chenhLech : conLai;                           
                            _context.Entry(pxctDB).State = EntityState.Modified;                           
                        }
                        //Nếu còn lại không đủ để bù cho số lượng chênh lệch
                        if (chenhLech > 0)
                        {
                            await PhieuXuatCT_Themmoi(nL_PhieuXuat, ct);
                        }
                    }
                }
            }
           
            nL_PhieuXuat.ModifyDate = DateTime.Now;
            _context.Entry(nL_PhieuXuat).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (NLPhieuXuatCTExists(nL_PhieuXuat.PhieuXuatID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
                //return NotFound();
            }
            var listPhieuXuatCT = await _context.NL_PhieuXuatCT.Where(x => x.PhieuXuatID == nL_PhieuXuat.PhieuXuatID).ToListAsync();
            nL_PhieuXuat.NL_PhieuXuatCTs = listPhieuXuatCT;
            return nL_PhieuXuat;
        }
        [HttpPost]
        [Route("NLPostPhieuXuat")]
        public async Task<ActionResult<NL_PhieuXuat>> NLPostPhieuXuat(NL_PhieuXuat nL_PhieuXuat)
        {           
            //Thêm phiếu xuat
            nL_PhieuXuat.CreatedDate = DateTime.Now;
            nL_PhieuXuat.ModifyDate = nL_PhieuXuat.CreatedDate;
            var phieuXuatID = await _context.NL_PhieuXuat.OrderByDescending(x => x.PhieuXuatID).FirstOrDefaultAsync();
            nL_PhieuXuat.PhieuXuatID = phieuXuatID != null ? phieuXuatID.PhieuXuatID + 1 : 1;
            var phieuXuatCTOld = nL_PhieuXuat.NL_PhieuXuatCTs.ToList();
            List<NL_PhieuXuatCT> listPhieuXuatCT = new List<NL_PhieuXuatCT>();
            if (phieuXuatCTOld != null)
            {
                foreach (var ct in phieuXuatCTOld)
                {
                    await PhieuXuatCT_Themmoi(nL_PhieuXuat, ct);
                }                
            }
            nL_PhieuXuat.NL_PhieuXuatCTs = listPhieuXuatCT;
            _context.NL_PhieuXuat.Add(nL_PhieuXuat);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (NLPhieuXuatCTExists(nL_PhieuXuat.PhieuXuatID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
                //return NotFound();
            }          
            return nL_PhieuXuat;
        }

        [HttpDelete]
        [Route("NLDeletePhieuXuat")]
        public async Task<ActionResult<NL_PhieuXuat>> NLDeletePhieuXuat(long id, string maNV, string tenNV)
        {
            var phieuXuat = await _context.NL_PhieuXuat.FindAsync(id);
            if (phieuXuat == null)
            {
                return NotFound();
            }
            phieuXuat.NL_PhieuXuatCTs = await _context.NL_PhieuXuatCT.Where(x => x.PhieuXuatID == phieuXuat.PhieuXuatID).ToListAsync();           
            _context.NL_PhieuXuat.Remove(phieuXuat);
            //Xóa hết phiếu xuất chi tiết                      
            if (phieuXuat.NL_PhieuXuatCTs != null)
            {
                foreach (var ct in phieuXuat.NL_PhieuXuatCTs)
                {
                    var pnct = await _context.NL_PhieuNhapCT.Where(x => x.PhieuNhapID == ct.PhieuNhapID && x.MaDauMo == ct.MaDauMo).FirstOrDefaultAsync();
                    if (pnct != null)
                    {
                        var sumSoluongXuat = await _context.NL_PhieuXuatCT.Where(x => x.PhieuNhapID == ct.PhieuNhapID && x.MaDauMo == ct.MaDauMo && x.PhieuXuatID != ct.PhieuXuatID).SumAsync(x => x.SoLuongVCF);
                        decimal chenhLech = pnct.SoLuongVCF - sumSoluongXuat;
                        if (chenhLech <= 0)
                            pnct.ConLai = 0;
                        else
                            pnct.ConLai += ct.SoLuongVCF <= chenhLech ? ct.SoLuongVCF : chenhLech;                        
                        _context.Entry(pnct).State = EntityState.Modified;
                    }
                }
                _context.NL_PhieuXuatCT.RemoveRange(phieuXuat.NL_PhieuXuatCTs);
            }           
            //Ghi nhật ký
            NhatKy nk = new NhatKy();
            nk.TenBang = "NL_PhieuXuat";
            nk.NoiDung = "Xóa phiếu xuất: " + phieuXuat.PhieuXuatID + "-" + phieuXuat.TenTramNL + "-" + phieuXuat.NgayXuat;
            nk.Createddate = DateTime.Now;
            nk.Createdby = maNV;
            nk.CreatedName = tenNV;
            _context.NhatKy.Add(nk);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return phieuXuat;
        }
        [HttpPut]
        [Route("NLKhoaPhieuXuat")]
        public async Task<ActionResult<NL_PhieuXuat>> NLKhoaPhieuXuat(NL_PhieuXuat nL_PhieuXuat)
        {
            nL_PhieuXuat.ModifyDate = DateTime.Now;
            _context.Entry(nL_PhieuXuat).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return nL_PhieuXuat;
        }
        private bool NLPhieuXuatCTExists(long id)
        {
            return _context.NL_PhieuXuatCT.Any(e => e.PhieuXuatID == id);
        }
        #endregion

        #region NL_DieuChinh
        [HttpGet]
        [Route("NLGetDieuChinh")]
        public async Task<ActionResult<string>> NLGetDieuChinh(string maTramNL, short maDauMo,long phieuNhapID)
        {
            try
            {
                var phieuXuatCT = (from ct in _context.NL_PhieuXuatCT                                         
                                         group ct by new { ct.PhieuNhapID, ct.MaDauMo } into g
                                         select new
                                         {
                                             PhieuNhapID = (long)g.Key.PhieuNhapID,
                                             MaDauMo = (short)g.Key.MaDauMo,
                                             SoLuongVCF = (decimal)g.Sum(x => x.SoLuongVCF)
                                         });
                var phieuNhapCT = await (from pnct in _context.NL_PhieuNhapCT
                                         join pn in _context.NL_PhieuNhap on pnct.PhieuNhapID equals pn.PhieuNhapID
                                         join pxct in phieuXuatCT on new { pnct.PhieuNhapID, pnct.MaDauMo } equals new { pxct.PhieuNhapID, pxct.MaDauMo }
                                         where pnct.SoLuongVCF - pnct.ConLai != pxct.SoLuongVCF && pn.MaTramNL==maTramNL  && pnct.MaDauMo==maDauMo && pnct.PhieuNhapID==phieuNhapID
                                         select new
                                         {
                                             PhieuNhapID = (long)pnct.PhieuNhapID,                                             
                                             MaDauMo = (short)pnct.MaDauMo,
                                             SLNhapVCF = (decimal)pnct.SoLuongVCF,
                                             SLXuatVCF = (decimal)pxct.SoLuongVCF,
                                             ConLai = (decimal)pnct.ConLai,
                                             ChenhLech = (decimal)(pnct.SoLuongVCF - pxct.SoLuongVCF)
                                         }).ToListAsync();
                if (phieuNhapCT.Count > 0)
                {
                    foreach (var pnct in phieuNhapCT)
                    {
                        var pnctDB = await _context.NL_PhieuNhapCT.Where(x => x.PhieuNhapID == pnct.PhieuNhapID && x.MaDauMo == pnct.MaDauMo).FirstOrDefaultAsync();
                        if (pnct.ChenhLech >= 0)//Nếu tổng lượng nhập-xuất mà nhỏ hơn còn lại thì cập nhật lại còn lại
                        {
                            pnctDB.ConLai = pnct.SLNhapVCF - pnct.SLXuatVCF;
                            _context.Entry(pnctDB).State = EntityState.Modified;
                        }
                        else //Nếu tổng lượng xuất mà nhỏ hơn còn lại thì thì chuyển số chênh lệch sang phiếu nhập khác và cập nhật còn lại của phiếu nhập ấy=0
                        {
                            //Cập nhật còn lại của phiếu nhập ấy=0                           
                            pnctDB.ConLai = 0;
                            _context.Entry(pnctDB).State = EntityState.Modified;
                            //Lấy danh sách phiếu xuất chi tiết của phiếu nhập và mã dầu mỡ
                            var pxctDB = await _context.NL_PhieuXuatCT.Where(x => x.PhieuNhapID == pnct.PhieuNhapID && x.MaDauMo == pnct.MaDauMo).ToListAsync();
                            decimal slXuat = 0M;
                            bool isFirst = true;

                            foreach (var pxct in pxctDB)
                            {
                                slXuat += pxct.SoLuongVCF;
                                var pxDB = await _context.NL_PhieuXuat.Where(x => x.PhieuXuatID == pxct.PhieuXuatID).FirstOrDefaultAsync();
                                if (pnct.SLNhapVCF < slXuat)
                                {
                                    //Kiểm tra xem còn phiều nhập nào khác phiếu nhập này còn đủ số lượng để chuyển không
                                    var pnctDBChange = await (from ct in _context.NL_PhieuNhapCT
                                                              join pn in _context.NL_PhieuNhap on ct.PhieuNhapID equals pn.PhieuNhapID
                                                              where pn.PhieuNhapID != pnct.PhieuNhapID && pn.NgayNhap <= pxDB.NgayXuat && pn.MaTramNL == pxDB.MaTramNL
                                                              && ct.MaDauMo == pnct.MaDauMo && ct.ConLai >= pxct.SoLuongVCF
                                                              select ct).FirstOrDefaultAsync();
                                    //Nếu không tìm được phiếu nhập có còn lại thì lấy phiếu xuất cuối cùng để đổi sang phiếu nhập khác
                                    if (pnctDBChange == null)
                                    {
                                        var pxctDBChange = await (from ct in _context.NL_PhieuXuatCT
                                                                  join px in _context.NL_PhieuXuat on ct.PhieuXuatID equals px.PhieuXuatID
                                                                  where ct.PhieuNhapID == pnct.PhieuNhapID
                                                                  && ct.MaDauMo == pnct.MaDauMo
                                                                  orderby px.NgayXuat descending
                                                                  select ct).FirstOrDefaultAsync();
                                        if (pxctDBChange != null)
                                        {
                                            var pxDBChange = await _context.NL_PhieuXuat.Where(x => x.PhieuXuatID == pxctDBChange.PhieuXuatID).FirstOrDefaultAsync();
                                            pnctDBChange = await (from ct in _context.NL_PhieuNhapCT
                                                                  join pn in _context.NL_PhieuNhap on ct.PhieuNhapID equals pn.PhieuNhapID
                                                                  where pn.PhieuNhapID != pxctDBChange.PhieuNhapID && pn.NgayNhap <= pxDBChange.NgayXuat && pn.MaTramNL == pxDBChange.MaTramNL
                                                                  && ct.MaDauMo == pxctDBChange.MaDauMo && ct.ConLai >= pxctDBChange.SoLuongVCF                                               
                                                                  select ct).FirstOrDefaultAsync();                                           
                                            var pxctOld = await _context.NL_PhieuXuatCT.Where(x => x.PhieuXuatID == pxct.PhieuXuatID && x.MaDauMo == pxct.MaDauMo && x.PhieuNhapID == pnctDBChange.PhieuNhapID).FirstOrDefaultAsync();
                                            if (pxctOld != null)
                                            {
                                                //Cập nhật thêm số lượng của id phiếu nhập cũ
                                                pxctOld.SoLuongVCF += pxct.SoLuongVCF;
                                                pxctOld.SoLuong += Math.Round(pxct.SoLuongVCF / pxctOld.VCF, 4);
                                                pxctOld.ThanhTien += pxct.SoLuongVCF * pxctOld.DonGia;
                                                _context.Entry(pxctOld).State = EntityState.Modified;
                                                //Cập nhật lại còn lại cho phiếu nhập cũ
                                                pnctDB.ConLai += pxct.SoLuongVCF;
                                                _context.Entry(pnctDB).State = EntityState.Modified;
                                            }
                                            else
                                            {
                                                //Thêm phiếu xuất chi tiết mới
                                                NL_PhieuXuatCT pcxctDB = new NL_PhieuXuatCT();
                                                pcxctDB.PhieuXuatID = pxct.PhieuXuatID;
                                                pcxctDB.MaDauMo = pxct.MaDauMo;
                                                pcxctDB.TenDauMo = pxct.TenDauMo;
                                                pcxctDB.DonViTinh = pxct.DonViTinh;
                                                pcxctDB.NhietDo = pxct.NhietDo;
                                                pcxctDB.TyTrong = pxct.TyTrong;
                                                pcxctDB.VCF = pxct.VCF;
                                                pcxctDB.BangGiaID = pxct.BangGiaID;
                                                pcxctDB.DonGia = pxct.DonGia;
                                                pcxctDB.PhieuNhapID = pnctDBChange.PhieuNhapID;
                                                pcxctDB.SoLuongVCF = pxct.SoLuongVCF;
                                                pcxctDB.SoLuong = pxct.SoLuong;
                                                pcxctDB.ThanhTien = pxct.ThanhTien;
                                                _context.NL_PhieuXuatCT.Add(pcxctDB);
                                            }
                                            //Trừ còn lại cho phiếu nhập mới
                                            pnctDBChange.ConLai -= pxct.SoLuongVCF;
                                            _context.Entry(pnctDBChange).State = EntityState.Modified;
                                            //Xóa phiếu xuất chi tiết cũ                                           
                                            _context.NL_PhieuXuatCT.Remove(pxctDBChange);
                                        }
                                    }
                                    //Nếu đủ thì chuyển id phiếu nhập cũ sang id của phiếu nhập liền kề sau đó
                                    else if (pnctDBChange != null)
                                    {
                                        //Thấy bản ghi đầu tiên của phiếu xuất nhiều hơn còn lại của phiếu nhập thì cập nhật lại phiếu xuất cho đủ còn lại
                                        if (isFirst == true)
                                        {
                                            decimal _chenhLech = slXuat - pnct.SLNhapVCF;                                           
                                            var pxctOld = await _context.NL_PhieuXuatCT.Where(x => x.PhieuXuatID == pxct.PhieuXuatID && x.MaDauMo == pxct.MaDauMo && x.PhieuNhapID == pnctDBChange.PhieuNhapID).FirstOrDefaultAsync();
                                            if (pxctOld != null)
                                            {
                                                pxctOld.SoLuongVCF += _chenhLech;
                                                pxctOld.SoLuong += Math.Round(_chenhLech / pxctOld.VCF, 4);
                                                pxctOld.ThanhTien += _chenhLech * pxctOld.DonGia;
                                                _context.Entry(pxctOld).State = EntityState.Modified;
                                            }
                                            else
                                            {
                                                //Thêm phiếu xuất chi tiết mới
                                                NL_PhieuXuatCT pcxctDB = new NL_PhieuXuatCT();
                                                pcxctDB.PhieuXuatID = pxct.PhieuXuatID;
                                                pcxctDB.MaDauMo = pxct.MaDauMo;
                                                pcxctDB.TenDauMo = pxct.TenDauMo;
                                                pcxctDB.DonViTinh = pxct.DonViTinh;
                                                pcxctDB.NhietDo = pxct.NhietDo;
                                                pcxctDB.TyTrong = pxct.TyTrong;
                                                pcxctDB.VCF = pxct.VCF;
                                                pcxctDB.BangGiaID = pxct.BangGiaID;
                                                pcxctDB.DonGia = pxct.DonGia;
                                                pcxctDB.PhieuNhapID = pnctDBChange.PhieuNhapID;
                                                pcxctDB.SoLuongVCF = _chenhLech;
                                                pcxctDB.SoLuong = Math.Round(_chenhLech / pxct.VCF, 4);
                                                pcxctDB.ThanhTien = _chenhLech * pxct.DonGia;
                                                _context.NL_PhieuXuatCT.Add(pcxctDB);
                                            }
                                            //Cập nhật còn lại của phiếu nhập mới
                                            pnctDBChange.ConLai -= _chenhLech;
                                            _context.Entry(pnctDBChange).State = EntityState.Modified;

                                            if (pxct.SoLuongVCF > _chenhLech)
                                            {
                                                decimal _conLai = pxct.SoLuongVCF - _chenhLech;
                                                //Cập nhật phiếu xuất cũ theo số lượng còn lại
                                                pxct.SoLuongVCF = _conLai;
                                                pxct.SoLuong = Math.Round(_conLai / pxct.VCF, 4);
                                                pxct.ThanhTien = _conLai * pxct.DonGia;
                                                _context.Entry(pxct).State = EntityState.Modified;
                                            }
                                            else
                                            {
                                                //Xóa phiếu xuất chi tiết cũ                                           
                                                _context.NL_PhieuXuatCT.Remove(pxct);
                                            }

                                            isFirst = false;
                                        }
                                        //Nếu là bản ghi tiếp theo thì chuyển phiếu xuất sang phiếu nhập mới
                                        else
                                        {
                                            var pxctOld = await _context.NL_PhieuXuatCT.Where(x => x.PhieuXuatID == pxct.PhieuXuatID && x.MaDauMo == pxct.MaDauMo && x.PhieuNhapID == pnctDBChange.PhieuNhapID).FirstOrDefaultAsync();
                                            if (pxctOld != null)
                                            {
                                                pxctOld.SoLuongVCF += pxct.SoLuongVCF;
                                                pxctOld.SoLuong += Math.Round(pxct.SoLuongVCF / pxctOld.VCF, 4);
                                                pxctOld.ThanhTien += pxct.SoLuongVCF * pxctOld.DonGia;
                                                _context.Entry(pxctOld).State = EntityState.Modified;
                                            }
                                            else
                                            {
                                                //Thêm phiếu xuất chi tiết mới
                                                NL_PhieuXuatCT pcxctDB = new NL_PhieuXuatCT();
                                                pcxctDB.PhieuXuatID = pxct.PhieuXuatID;
                                                pcxctDB.MaDauMo = pxct.MaDauMo;
                                                pcxctDB.TenDauMo = pxct.TenDauMo;
                                                pcxctDB.DonViTinh = pxct.DonViTinh;
                                                pcxctDB.NhietDo = pxct.NhietDo;
                                                pcxctDB.TyTrong = pxct.TyTrong;
                                                pcxctDB.VCF = pxct.VCF;
                                                pcxctDB.BangGiaID = pxct.BangGiaID;
                                                pcxctDB.DonGia = pxct.DonGia;
                                                pcxctDB.PhieuNhapID = pnctDBChange.PhieuNhapID;
                                                pcxctDB.SoLuongVCF = pxct.SoLuongVCF;
                                                pcxctDB.SoLuong = pxct.SoLuong;
                                                pcxctDB.ThanhTien = pxct.ThanhTien;
                                                _context.NL_PhieuXuatCT.Add(pcxctDB);
                                            }
                                            //Trừ còn lại của phiếu nhập mới
                                            pnctDBChange.ConLai -= pxct.SoLuongVCF;
                                            _context.Entry(pnctDBChange).State = EntityState.Modified;
                                            //Xóa phiếu xuất chi tiết cũ                                           
                                            _context.NL_PhieuXuatCT.Remove(pxct);
                                        }
                                    }
                                }
                               
                            }
                        }
                    }
                }
                await _context.SaveChangesAsync();
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region NL_Báo cáo
        [HttpGet]
        [Route("GetBCTheKho")]
        public async Task<ActionResult<IEnumerable<NL_BCTheKho>>> GetBCTheKho(string maTram, short maDauMo, DateTime ngayBD, DateTime ngayKT)
        {
            DateTime _ngayKT = ngayKT.AddDays(1);
            List<NL_BCTheKho> listTK = new List<NL_BCTheKho>();           
            //Lấy số lượng và tiền của đầu kỳ
            var nhapDK= await (from pn in _context.NL_PhieuNhap
                       join ct in _context.NL_PhieuNhapCT on pn.PhieuNhapID equals ct.PhieuNhapID
                       where pn.NgayNhap < ngayBD && maTram.Contains(pn.MaTramNL) && ct.MaDauMo == maDauMo && ct.ConLai > 0
                       group ct by new { ct.MaDauMo } into g           
                        select new NL_BCTheKho
                        {                           
                            PhieuID =(long)0,
                            Ngay= (DateTime)ngayBD.AddDays(-1),                           
                            LoaiPhieu=(string)"DK",
                            TramNL=(string)"",
                            DienGiai = (string)"Số dư đầu kỳ",
                            SoLuong = (decimal)g.Sum(x=>x.ConLai),
                            ThanhTien = (decimal)g.Sum(x => x.ConLai*x.DonGia)
                        }).ToListAsync();
            //Lấy số lượng và mã phiếu nhập các phiếu xuất trong kỳ trở về sau
            var xuatDK = await (from ct in _context.NL_PhieuXuatCT
                                 join px in _context.NL_PhieuXuat on ct.PhieuXuatID equals px.PhieuXuatID
                                 join pn in _context.NL_PhieuNhap on ct.PhieuNhapID equals pn.PhieuNhapID
                                join pnct in _context.NL_PhieuNhapCT on new { ct.PhieuNhapID, ct.MaDauMo } equals new { pnct.PhieuNhapID, pnct.MaDauMo }
                                where px.NgayXuat >= ngayBD && pn.NgayNhap < ngayBD && maTram.Contains(px.MaTramNL) && ct.MaDauMo == maDauMo                              
                                select new NL_BCTheKho
                                 {                                   
                                     PhieuID = (long)0,
                                     Ngay = (DateTime)ngayBD.AddDays(-1),
                                     LoaiPhieu = (string)"DK",
                                     TramNL = (string)"",
                                     DienGiai=(string)"Số dư đầu kỳ",
                                     SoLuong = (decimal)ct.SoLuongVCF,
                                     ThanhTien = (decimal)ct.SoLuongVCF*pnct.DonGia,
                                 }).ToListAsync();
           
             var tonDK =  nhapDK.Concat(xuatDK).ToList();            
            //Lấy Nhập trong kỳ
            var nhapTK = await (from pn in _context.NL_PhieuNhap
                               join ct in _context.NL_PhieuNhapCT on pn.PhieuNhapID equals ct.PhieuNhapID
                               where pn.NgayNhap >= ngayBD && pn.NgayNhap < _ngayKT && maTram.Contains(pn.MaTramNL) && ct.MaDauMo == maDauMo
                               select new NL_BCTheKho
                               {                                 
                                   PhieuID = (long)pn.PhieuNhapID,
                                   Ngay = (DateTime)pn.NgayNhap,
                                   LoaiPhieu = "PN",
                                   TramNL = pn.TenTramNL,
                                   DienGiai=pn.LyDo,
                                   SoLuong = (decimal)ct.SoLuongVCF,
                                   ThanhTien = (decimal)ct.SoLuongVCF*ct.DonGia
                               }).ToListAsync();
            //Lấy Xuất trong kỳ
            var xuatTK = await (from px in _context.NL_PhieuXuat
                                join ct in _context.NL_PhieuXuatCT on px.PhieuXuatID equals ct.PhieuXuatID
                                join pnct in _context.NL_PhieuNhapCT on new { ct.PhieuNhapID, ct.MaDauMo } equals new { pnct.PhieuNhapID, pnct.MaDauMo }
                                where px.NgayXuat >= ngayBD && px.NgayXuat < _ngayKT && maTram.Contains(px.MaTramNL) && ct.MaDauMo == maDauMo
                                select new NL_BCTheKho
                                {                                  
                                    PhieuID = (long)px.PhieuXuatID,
                                    Ngay = (DateTime)px.NgayXuat,
                                    LoaiPhieu = "PX",
                                    TramNL = px.TenTramNL,
                                    DienGiai=px.LyDo,
                                    SoLuong = (decimal)ct.SoLuongVCF,
                                    ThanhTien = (decimal)ct.SoLuongVCF*pnct.DonGia
                                }).ToListAsync();
            var queryUnion = tonDK.Concat(nhapTK).Concat(xuatTK).ToList();
            listTK = (from tk in queryUnion group tk by new { tk.PhieuID,tk.Ngay,tk.LoaiPhieu,tk.TramNL,tk.DienGiai } into g
                      select new NL_BCTheKho
                      {                          
                          PhieuID = (long)g.Key.PhieuID,
                          Ngay = (DateTime)g.Key.Ngay,
                          LoaiPhieu = (string)g.Key.LoaiPhieu,
                          TramNL = (string)g.Key.TramNL,
                          DienGiai=(string)g.Key.DienGiai,
                          SoLuong = (decimal)g.Sum(x=>x.SoLuong),
                          ThanhTien = (decimal)g.Sum(x=>x.ThanhTien)
                      }).OrderBy(x=>x.Ngay).ToList();
            return listTK;
        }
        [HttpGet]
        [Route("GetBCTonKho")]
        public async Task<ActionResult<IEnumerable<NL_BCTonKho>>> GetBCTonKho(string maTram, short maDauMo, DateTime ngayBD, DateTime ngayKT)
        {
            DateTime _ngayKT = ngayKT.AddDays(1);
            List<NL_BCTonKho> listTK = new List<NL_BCTonKho>();           
            //Lấy số lượng và tiền của đầu kỳ
            var nhapDK = await (from pn in _context.NL_PhieuNhap
                               join ct in _context.NL_PhieuNhapCT on pn.PhieuNhapID equals ct.PhieuNhapID
                               where pn.NgayNhap < ngayBD && maTram.Contains(pn.MaTramNL) && ct.ConLai > 0
                               group ct by new { ct.MaDauMo, ct.TenDauMo,ct.DonViTinh } into g
                               select new NL_BCTonKho
                               {
                                   MaDauMo = (short)g.Key.MaDauMo,
                                   TenDauMo = (string)g.Key.TenDauMo,
                                   DonViTinh=(string)g.Key.DonViTinh,
                                   LuongTD = (decimal)g.Sum(x => x.ConLai),
                                   TienTD = (decimal)g.Sum(x => x.ConLai* x.DonGia),
                                   LuongPN = (decimal)0,
                                   TienPN=(decimal)0,
                                   LuongPXTT = (decimal)0,
                                   LuongPX =(decimal)0,
                                   TienPX=(decimal)0,
                                   LuongTK=(decimal)0,
                                   TienTK=(decimal)0,
                               }).ToListAsync();
            //Lấy số lượng và mã phiếu nhập các phiếu xuất trong kỳ trở về sau
            var xuatDK = await (from ct in _context.NL_PhieuXuatCT
                                 join px in _context.NL_PhieuXuat on ct.PhieuXuatID equals px.PhieuXuatID
                                 join pn in _context.NL_PhieuNhap on ct.PhieuNhapID equals pn.PhieuNhapID
                                join pnct in _context.NL_PhieuNhapCT on new { ct.PhieuNhapID, ct.MaDauMo } equals new { pnct.PhieuNhapID, pnct.MaDauMo }
                                 where px.NgayXuat >= ngayBD && pn.NgayNhap < ngayBD && maTram.Contains(px.MaTramNL)                               
                                select new NL_BCTonKho
                                 {
                                     MaDauMo = (short)ct.MaDauMo,
                                     TenDauMo = (string)ct.TenDauMo,
                                     DonViTinh = (string)ct.DonViTinh,
                                     LuongTD = (decimal)ct.SoLuongVCF,
                                     TienTD = (decimal)ct.SoLuongVCF*pnct.DonGia,
                                     LuongPN = (decimal)0,
                                     TienPN = (decimal)0,
                                    LuongPXTT = (decimal)0,
                                    LuongPX = (decimal)0,
                                     TienPX = (decimal)0,
                                     LuongTK = (decimal)0,
                                     TienTK = (decimal)0,
                                 }).ToListAsync();            
            var tonDK = nhapDK.Concat(xuatDK).ToList();           
            //Lấy Nhập trong kỳ
            var nhapTK = await (from pn in _context.NL_PhieuNhap
                                join ct in _context.NL_PhieuNhapCT on pn.PhieuNhapID equals ct.PhieuNhapID
                                where pn.NgayNhap >= ngayBD && pn.NgayNhap < _ngayKT && maTram.Contains(pn.MaTramNL)
                                group ct by new { ct.MaDauMo, ct.TenDauMo,ct.DonViTinh } into g
                                select new NL_BCTonKho
                                {
                                    MaDauMo = (short)g.Key.MaDauMo,
                                    TenDauMo = (string)g.Key.TenDauMo,
                                    DonViTinh=(string)g.Key.DonViTinh,
                                    LuongTD = (decimal)0,
                                    TienTD = (decimal)0,
                                    LuongPN = (decimal)g.Sum(x => x.SoLuongVCF),
                                    TienPN = (decimal)g.Sum(x => x.SoLuongVCF*x.DonGia),
                                    LuongPXTT = (decimal)0,
                                    LuongPX = (decimal)0,
                                    TienPX = (decimal)0,
                                    LuongTK = (decimal)0,
                                    TienTK = (decimal)0,
                                }).ToListAsync();
            //Lấy Xuất trong kỳ
            var xuatTK = await (from px in _context.NL_PhieuXuat
                                join ct in _context.NL_PhieuXuatCT on px.PhieuXuatID equals ct.PhieuXuatID
                                join pnct in _context.NL_PhieuNhapCT on new { ct.PhieuNhapID, ct.MaDauMo } equals new { pnct.PhieuNhapID, pnct.MaDauMo }
                                where px.NgayXuat >= ngayBD && px.NgayXuat < _ngayKT && maTram.Contains(px.MaTramNL)                               
                                select new NL_BCTonKho
                                {
                                    MaDauMo = (short)ct.MaDauMo,
                                    TenDauMo = (string)ct.TenDauMo,
                                    DonViTinh=(string)ct.DonViTinh,
                                    LuongTD = (decimal)0,
                                    TienTD = (decimal)0,
                                    LuongPN = (decimal)0,
                                    TienPN = (decimal)0,
                                    LuongPXTT = (decimal)ct.SoLuong,
                                    LuongPX = (decimal)ct.SoLuongVCF,
                                    TienPX = (decimal)ct.SoLuongVCF*pnct.DonGia,
                                    LuongTK = (decimal)0,
                                    TienTK = (decimal)0,
                                }).ToListAsync();
            var queryUnion = tonDK.Concat(nhapTK).Concat(xuatTK).ToList();
            listTK = (from tk in queryUnion
                      group tk by new { tk.MaDauMo, tk.TenDauMo,tk.DonViTinh } into g
                      select new NL_BCTonKho
                      {
                          MaDauMo = (short)g.Key.MaDauMo,
                          TenDauMo = (string)g.Key.TenDauMo,
                          DonViTinh=(string)g.Key.DonViTinh,
                          LuongTD = (decimal)g.Sum(x => x.LuongTD),
                          TienTD = (decimal)g.Sum(x => x.TienTD),
                          LuongPN = (decimal)g.Sum(x => x.LuongPN),
                          TienPN = (decimal)g.Sum(x => x.TienPN),
                          LuongPXTT = (decimal)g.Sum(x => x.LuongPXTT),
                          LuongPX = (decimal)g.Sum(x => x.LuongPX),
                          TienPX = (decimal)g.Sum(x => x.TienPX),
                          LuongTK = (decimal)g.Sum(x => x.LuongTD+x.LuongPN-x.LuongPX),
                          TienTK = (decimal)g.Sum(x => x.TienTD+x.TienPN-x.TienPX),
                      }).OrderBy(x => x.MaDauMo).ToList();
            if (maDauMo != -1)
                listTK = listTK.Where(x => x.MaDauMo == maDauMo).ToList();
            return listTK;
        }
        #endregion
    }
}
