using CBWebApplication.Context;
using CBWebApplication.Datas;
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
    public class XuatDuLieusController : ControllerBase
    {
        private readonly CoBaoDTContext _context;

        public XuatDuLieusController(CoBaoDTContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Getnguoidung")]
        public async Task<ActionResult<string>> Getnguoidung(string username, string password, string madv)
        {
            var nguoiDung = await _context.DL_NguoiDung.Where(x => x.UserName == username && x.Password == password && x.MaDV == madv).FirstOrDefaultAsync();
            if(nguoiDung!=null)
            {
                nguoiDung.SessionDate = DateTime.Now;
                string strKey = username +"," +madv +","+ nguoiDung.PublicKey;
                nguoiDung.PrivateKey = Library.Encrypt(strKey);
                nguoiDung.UserCount = nguoiDung.UserCount + 1;
                _context.Entry(nguoiDung).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return nguoiDung.PrivateKey;
            }   
            else
               return string.Empty;                                       
        }

        [HttpGet]
        [Route("Getdmga")]
        public async Task<ActionResult<DL_Danhmucga>> Getdmga(string key)
        {
            DL_Danhmucga dl = new DL_Danhmucga();
            try
            {
                string[] strKey= Library.Decrypt(key).Split(',');
                var privateKey = await _context.DL_NguoiDung.Where(x => x.UserName == strKey[0] && x.MaDV==strKey[1] && x.PublicKey==strKey[2]).FirstOrDefaultAsync();
                if (privateKey == null) throw new Exception("Lỗi xác thực, kiểm tra lại đăng nhập.");
                else
                {
                    TimeSpan timeSpan = DateTime.Now - (DateTime)privateKey.SessionDate;
                    if (timeSpan.TotalMinutes > 30) throw new Exception("Lỗi quá thời gian đăng nhập, đăng nhập lại.");
                    else
                    {
                        dl.TrangThai = "OK";
                        dl.DuLieu = await (from lt in _context.LyTrinh
                                           select new Danhmucga
                                           {
                                               TuyenID = (string)lt.TuyenID,
                                               TuyenName = (string)lt.TuyenName,
                                               GaID = (int)lt.GaID,
                                               GaName = (string)lt.TenGa,
                                               Km = (decimal)lt.Km,
                                               ModifyDate = (DateTime)lt.Modifydate
                                           }).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                dl.TrangThai = ex.Message;                
            }
            return dl;
        }

        [HttpGet]
        [Route("Getdmloaimay")]
        public async Task<ActionResult<DL_Danhmucloaimay>> Getdmloaimay(string key)
        {
            DL_Danhmucloaimay dl = new DL_Danhmucloaimay();
            try
            {
                string[] strKey = Library.Decrypt(key).Split(',');
                var privateKey = await _context.DL_NguoiDung.Where(x => x.UserName == strKey[0] && x.MaDV == strKey[1] && x.PublicKey == strKey[2]).FirstOrDefaultAsync();
                if (privateKey == null) throw new Exception("Lỗi xác thực, kiểm tra lại đăng nhập.");
                else
                {
                    TimeSpan timeSpan = DateTime.Now - (DateTime)privateKey.SessionDate;
                    if (timeSpan.TotalMinutes > 30) throw new Exception("Lỗi quá thời gian đăng nhập, đăng nhập lại.");
                    else
                    {
                        dl.TrangThai = "OK";
                        dl.DuLieu = await (from lm in _context.LoaiMay
                                           select new Danhmucloaimay
                                           {
                                               LoaiMayID = (string)lm.LoaiMayId,
                                               LoaiMayName = (string)lm.LoaiMayName,                                              
                                               ModifyDate = (DateTime)DateTime.Now
                                           }).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                dl.TrangThai = ex.Message;
            }
            return dl;
        }

        [HttpGet]
        [Route("Getdmdaumay")]
        public async Task<ActionResult<DL_Danhmucdaumay>> Getdmdaumay(string key)
        {
            DL_Danhmucdaumay dl = new DL_Danhmucdaumay();
            try
            {
                string[] strKey = Library.Decrypt(key).Split(',');
                var privateKey = await _context.DL_NguoiDung.Where(x => x.UserName == strKey[0] && x.MaDV == strKey[1] && x.PublicKey == strKey[2]).FirstOrDefaultAsync();
                if (privateKey == null) throw new Exception("Lỗi xác thực, kiểm tra lại đăng nhập.");
                else
                {
                    TimeSpan timeSpan = DateTime.Now - (DateTime)privateKey.SessionDate;
                    if (timeSpan.TotalMinutes > 30) throw new Exception("Lỗi quá thời gian đăng nhập, đăng nhập lại.");
                    else
                    {
                        dl.TrangThai = "OK";
                        dl.DuLieu = await (from dm in _context.DMDauMay
                                           join lm in _context.LoaiMay on dm.PhanLoai equals lm.LoaiMayId
                                           select new Danhmucdaumay
                                           {
                                               DauMayID = (string)dm.SoHieuMay,
                                               DauMayName = (string)dm.DauMaySo,
                                               LoaiMayID = (string)lm.LoaiMayId,
                                               LoaiMayName = (string)lm.LoaiMayName,
                                               ModifyDate = (DateTime)DateTime.Now
                                           }).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                dl.TrangThai = ex.Message;
            }
            return dl;
        }

        [HttpGet]
        [Route("Getdmtramnl")]
        public async Task<ActionResult<DL_Danhmuctramnl>> Getdmtramnl(string key)
        {
            DL_Danhmuctramnl dl = new DL_Danhmuctramnl();
            try
            {
                string[] strKey = Library.Decrypt(key).Split(',');
                var privateKey = await _context.DL_NguoiDung.Where(x => x.UserName == strKey[0] && x.MaDV == strKey[1] && x.PublicKey == strKey[2]).FirstOrDefaultAsync();
                if (privateKey == null) throw new Exception("Lỗi xác thực, kiểm tra lại đăng nhập.");
                else
                {
                    TimeSpan timeSpan = DateTime.Now - (DateTime)privateKey.SessionDate;
                    if (timeSpan.TotalMinutes > 30) throw new Exception("Lỗi quá thời gian đăng nhập, đăng nhập lại.");
                    else
                    {
                        dl.TrangThai = "OK";
                        dl.DuLieu = await (from tr in _context.DmtramNhienLieu                                      
                                           select new Danhmuctramnl
                                           {
                                               TramID = (string)tr.MaTram,
                                               TramName = (string)tr.TenTram,                                             
                                               ModifyDate = (DateTime)DateTime.Now
                                           }).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                dl.TrangThai = ex.Message;
            }
            return dl;
        }

        [HttpGet]
        [Route("Getdmtinhchat")]
        public async Task<ActionResult<DL_Danhmuctinhchat>> Getdmtinhchat(string key)
        {
            DL_Danhmuctinhchat dl = new DL_Danhmuctinhchat();
            try
            {
                string[] strKey = Library.Decrypt(key).Split(',');
                var privateKey = await _context.DL_NguoiDung.Where(x => x.UserName == strKey[0] && x.MaDV == strKey[1] && x.PublicKey == strKey[2]).FirstOrDefaultAsync();
                if (privateKey == null) throw new Exception("Lỗi xác thực, kiểm tra lại đăng nhập.");
                else
                {
                    TimeSpan timeSpan = DateTime.Now - (DateTime)privateKey.SessionDate;
                    if (timeSpan.TotalMinutes > 30) throw new Exception("Lỗi quá thời gian đăng nhập, đăng nhập lại.");
                    else
                    {
                        dl.TrangThai = "OK";
                        dl.DuLieu = await (from tr in _context.TinhChat
                                           select new Danhmuctinhchat
                                           {
                                               TinhChatID = (short)tr.TinhChatId,
                                               TinhChatName = (string)tr.TinhChatName,
                                               ModifyDate = (DateTime)DateTime.Now
                                           }).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                dl.TrangThai = ex.Message;
            }
            return dl;
        }

        [HttpGet]
        [Route("Getdmcongtac")]
        public async Task<ActionResult<DL_Danhmuccongtac>> Getdmcongtac(string key)
        {
            DL_Danhmuccongtac dl = new DL_Danhmuccongtac();
            try
            {
                string[] strKey = Library.Decrypt(key).Split(',');
                var privateKey = await _context.DL_NguoiDung.Where(x => x.UserName == strKey[0] && x.MaDV == strKey[1] && x.PublicKey == strKey[2]).FirstOrDefaultAsync();
                if (privateKey == null) throw new Exception("Lỗi xác thực, kiểm tra lại đăng nhập.");
                else
                {
                    TimeSpan timeSpan = DateTime.Now - (DateTime)privateKey.SessionDate;
                    if (timeSpan.TotalMinutes > 30) throw new Exception("Lỗi quá thời gian đăng nhập, đăng nhập lại.");
                    else
                    {
                        dl.TrangThai = "OK";
                        dl.DuLieu = await (from tr in _context.CongTac
                                           select new Danhmuccongtac
                                           {
                                               CongTacID = (short)tr.CongTacId,
                                               CongTacName = (string)tr.CongTacName,
                                               ModifyDate = (DateTime)DateTime.Now
                                           }).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                dl.TrangThai = ex.Message;
            }
            return dl;
        }

        [HttpGet]
        [Route("Getdmmactau")]
        public async Task<ActionResult<DL_Danhmucmactau>> Getdmmactau(string key)
        {
            DL_Danhmucmactau dl = new DL_Danhmucmactau();
            try
            {
                string[] strKey = Library.Decrypt(key).Split(',');
                var privateKey = await _context.DL_NguoiDung.Where(x => x.UserName == strKey[0] && x.MaDV == strKey[1] && x.PublicKey == strKey[2]).FirstOrDefaultAsync();
                if (privateKey == null) throw new Exception("Lỗi xác thực, kiểm tra lại đăng nhập.");
                else
                {
                    TimeSpan timeSpan = DateTime.Now - (DateTime)privateKey.SessionDate;
                    if (timeSpan.TotalMinutes > 30) throw new Exception("Lỗi quá thời gian đăng nhập, đăng nhập lại.");
                    else
                    {
                        dl.TrangThai = "OK";
                        dl.DuLieu = await (from tr in _context.MacTau
                                           select new Danhmucmactau
                                           {
                                               MacTauID = (string)tr.MacTauID,
                                               LoaiTauID = (short)tr.LoaiTauID,
                                               LoaiTauName=(string)tr.LoaiTauName,
                                               CongTacID = (short)tr.CongTacID,
                                               CongTacName = (string)tr.CongTacName,
                                               ModifyDate = (DateTime)tr.ModifyDate
                                           }).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                dl.TrangThai = ex.Message;
            }
            return dl;
        }

        [HttpGet]
        [Route("Getdmdonvi")]
        public async Task<ActionResult<DL_Danhmucdonvi>> Getdmdonvi(string key)
        {
            DL_Danhmucdonvi dl = new DL_Danhmucdonvi();
            try
            {
                string[] strKey = Library.Decrypt(key).Split(',');
                var privateKey = await _context.DL_NguoiDung.Where(x => x.UserName == strKey[0] && x.MaDV == strKey[1] && x.PublicKey == strKey[2]).FirstOrDefaultAsync();
                if (privateKey == null) throw new Exception("Lỗi xác thực, kiểm tra lại đăng nhập.");
                else
                {
                    TimeSpan timeSpan = DateTime.Now - (DateTime)privateKey.SessionDate;
                    if (timeSpan.TotalMinutes > 30) throw new Exception("Lỗi quá thời gian đăng nhập, đăng nhập lại.");
                    else
                    {
                        dl.TrangThai = "OK";
                        dl.DuLieu = await (from tr in _context.DonViDM
                                           select new Danhmucdonvi
                                           {
                                               DonViID = (string)tr.MaDV,
                                               DonViName = (string)tr.TenDV,
                                               ModifyDate = (DateTime)DateTime.Now
                                           }).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                dl.TrangThai = ex.Message;
            }
            return dl;
        }

        [HttpGet]
        [Route("Getdmkhudoan")]
        public async Task<ActionResult<DL_Danhmuckhudoan>> Getdmkhudoan(string key)
        {
            DL_Danhmuckhudoan dl = new DL_Danhmuckhudoan();
            try
            {
                string[] strKey = Library.Decrypt(key).Split(',');
                var privateKey = await _context.DL_NguoiDung.Where(x => x.UserName == strKey[0] && x.MaDV == strKey[1] && x.PublicKey == strKey[2]).FirstOrDefaultAsync();
                if (privateKey == null) throw new Exception("Lỗi xác thực, kiểm tra lại đăng nhập.");
                else
                {
                    TimeSpan timeSpan = DateTime.Now - (DateTime)privateKey.SessionDate;
                    if (timeSpan.TotalMinutes > 30) throw new Exception("Lỗi quá thời gian đăng nhập, đăng nhập lại.");
                    else
                    {
                        dl.TrangThai = "OK";
                        dl.DuLieu = await (from tr in _context.HN_KhuDoan
                                           group tr by new { tr.KhuDoanID,tr.KhuDoanName } into g
                                           select new Danhmuckhudoan
                                           {
                                               KhuDoanID = (string)g.Key.KhuDoanID,
                                               KhuDoanName = (string)g.Key.KhuDoanName,
                                               ModifyDate = (DateTime)DateTime.Now
                                           }).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                dl.TrangThai = ex.Message;
            }
            return dl;
        }

        [HttpGet]
        [Route("Getdmtuyen")]
        public async Task<ActionResult<DL_Danhmuctuyen>> Getdmtuyen(string key)
        {
            DL_Danhmuctuyen dl = new DL_Danhmuctuyen();
            try
            {
                string[] strKey = Library.Decrypt(key).Split(',');
                var privateKey = await _context.DL_NguoiDung.Where(x => x.UserName == strKey[0] && x.MaDV == strKey[1] && x.PublicKey == strKey[2]).FirstOrDefaultAsync();
                if (privateKey == null) throw new Exception("Lỗi xác thực, kiểm tra lại đăng nhập.");
                else
                {
                    TimeSpan timeSpan = DateTime.Now - (DateTime)privateKey.SessionDate;
                    if (timeSpan.TotalMinutes > 30) throw new Exception("Lỗi quá thời gian đăng nhập, đăng nhập lại.");
                    else
                    {
                        dl.TrangThai = "OK";
                        dl.DuLieu = await (from tr in _context.TuyenMap
                                           select new Danhmuctuyen
                                           {
                                               TuyenID = (short)tr.TuyenId,
                                               TuyenName = (string)tr.TuyenName,
                                               ModifyDate = (DateTime)DateTime.Now
                                           }).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                dl.TrangThai = ex.Message;
            }
            return dl;
        }

        [HttpGet]
        [Route("Getdmtuyensl")]
        public async Task<ActionResult<DL_Danhmuctuyensl>> Getdmtuyensl(string key)
        {
            DL_Danhmuctuyensl dl = new DL_Danhmuctuyensl();
            try
            {
                string[] strKey = Library.Decrypt(key).Split(',');
                var privateKey = await _context.DL_NguoiDung.Where(x => x.UserName == strKey[0] && x.MaDV == strKey[1] && x.PublicKey == strKey[2]).FirstOrDefaultAsync();
                if (privateKey == null) throw new Exception("Lỗi xác thực, kiểm tra lại đăng nhập.");
                else
                {
                    TimeSpan timeSpan = DateTime.Now - (DateTime)privateKey.SessionDate;
                    if (timeSpan.TotalMinutes > 30) throw new Exception("Lỗi quá thời gian đăng nhập, đăng nhập lại.");
                    else
                    {
                        dl.TrangThai = "OK";
                        dl.DuLieu = await (from tr in _context.Tuyen
                                           select new Danhmuctuyensl
                                           {
                                               TuyenID = (string)tr.TuyenID,
                                               TuyenName = (string)tr.TuyenName,
                                               ModifyDate = (DateTime)DateTime.Now
                                           }).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                dl.TrangThai = ex.Message;
            }
            return dl;
        }

        [HttpGet]
        [Route("Getdmdaumo")]
        public async Task<ActionResult<DL_Danhmucdaumo>> Getdmdaumo(string key)
        {
            DL_Danhmucdaumo dl = new DL_Danhmucdaumo();
            try
            {
                string[] strKey = Library.Decrypt(key).Split(',');
                var privateKey = await _context.DL_NguoiDung.Where(x => x.UserName == strKey[0] && x.MaDV == strKey[1] && x.PublicKey == strKey[2]).FirstOrDefaultAsync();
                if (privateKey == null) throw new Exception("Lỗi xác thực, kiểm tra lại đăng nhập.");
                else
                {
                    TimeSpan timeSpan = DateTime.Now - (DateTime)privateKey.SessionDate;
                    if (timeSpan.TotalMinutes > 30) throw new Exception("Lỗi quá thời gian đăng nhập, đăng nhập lại.");
                    else
                    {
                        dl.TrangThai = "OK";
                        dl.DuLieu = await (from tr in _context.DMLoaiDauMo
                                           select new Danhmucdaumo
                                           {
                                               DauMoID = (short)tr.ID,
                                               DauMoName = (string)tr.LoaiDauMo,
                                               DonViTinh=(string)tr.DonViTinh,
                                               ModifyDate = (DateTime)tr.ModifyDate
                                           }).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                dl.TrangThai = ex.Message;
            }
            return dl;
        }

        [HttpGet]
        [Route("Getcobaodt")]
        public async Task<ActionResult<DL_Cobaodt>> Getcobaodt(string key, int thang, int nam, string madv,string socb)
        {
            DL_Cobaodt dl = new DL_Cobaodt();
            try
            {
                string[] strKey = Library.Decrypt(key).Split(',');
                var privateKey = await _context.DL_NguoiDung.Where(x => x.UserName == strKey[0] && x.MaDV == strKey[1] && x.PublicKey == strKey[2]).FirstOrDefaultAsync();
                if (privateKey == null) throw new Exception("Lỗi xác thực, kiểm tra lại đăng nhập.");
                else
                {
                    TimeSpan timeSpan = DateTime.Now - (DateTime)privateKey.SessionDate;
                    if (timeSpan.TotalMinutes > 30) throw new Exception("Lỗi quá thời gian đăng nhập, đăng nhập lại.");
                    else
                    {
                        dl.TrangThai = "OK";
                        var query = from cb in _context.CoBao
                                    join dt in _context.DoanThong on cb.CoBaoID equals dt.DoanThongID
                                    where dt.ThangDT == thang && dt.NamDT == nam && cb.DvcbID == madv
                                    select cb;
                        if (!string.IsNullOrWhiteSpace(socb))
                        {
                            query = query.Where(x => x.SoCB == socb);
                        }
                        dl.DuLieu = await (from s in query orderby s.NhanMay
                        select new XCoBao
                        {
                            CoBaoID = s.CoBaoID,
                            CoBaoGoc = s.CoBaoGoc,
                            SoCB = s.SoCB,
                            DauMayID = s.DauMayID,
                            LoaiMayID = s.LoaiMayID,
                            DvdmID = s.DvdmID,
                            DvdmName = s.DvdmName,
                            DvcbID = s.DvcbID,
                            DvcbName = s.DvcbName,
                            NgayCB = s.NgayCB,
                            RutGio = s.RutGio,
                            ChatLuong = s.ChatLuong,
                            SoLanRaKho = s.SoLanRaKho,
                            TaiXe1ID = s.TaiXe1ID,
                            TaiXe1Name = s.TaiXe1Name,
                            TaiXe1GioLT = s.TaiXe1GioLT,
                            PhoXe1ID = s.PhoXe1ID,
                            PhoXe1Name = s.PhoXe1Name,
                            PhoXe1GioLT = s.PhoXe1GioLT,
                            TaiXe2ID = s.TaiXe2ID,
                            TaiXe2Name = s.TaiXe2Name,
                            TaiXe2GioLT = s.TaiXe2GioLT,
                            PhoXe2ID = s.PhoXe2ID,
                            PhoXe2Name = s.PhoXe2Name,
                            PhoXe2GioLT = s.PhoXe2GioLT,
                            TaiXe3ID = s.TaiXe3ID,
                            TaiXe3Name = s.TaiXe3Name,
                            TaiXe3GioLT = s.TaiXe3GioLT,
                            PhoXe3ID = s.PhoXe3ID,
                            PhoXe3Name = s.PhoXe3Name,
                            PhoXe3GioLT = s.PhoXe3GioLT,
                            LenBan = s.LenBan,
                            NhanMay = s.NhanMay,
                            RaKho = s.RaKho,
                            VaoKho = s.VaoKho,
                            GiaoMay = s.GiaoMay,
                            XuongBan = s.XuongBan,
                            NLBanTruoc = s.NLBanTruoc,
                            NLThucNhan = s.NLThucNhan,
                            NLLinh = s.NLLinh,
                            TramNLID = s.TramNLID,
                            NLTrongDoan = s.NLTrongDoan,
                            NLBanSau = s.NLBanSau,
                            SHDT = s.SHDT,
                            MaCB = s.MaCB,
                            DonDocDuong = s.DonDocDuong,
                            DungDocDuong = s.DungDocDuong,
                            DungNoMay = s.DungNoMay,
                            GhiChu = s.GhiChu
                        }).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                dl.TrangThai = ex.Message;
            }
            return dl;
        }

        [HttpGet]
        [Route("Getcobaodtct")]
        public async Task<ActionResult<DL_Cobaodtct>> Getcobaodtct(string key, int thang, int nam, string madv, string socb)
        {
            DL_Cobaodtct dl = new DL_Cobaodtct();
            try
            {
                string[] strKey = Library.Decrypt(key).Split(',');
                var privateKey = await _context.DL_NguoiDung.Where(x => x.UserName == strKey[0] && x.MaDV == strKey[1] && x.PublicKey == strKey[2]).FirstOrDefaultAsync();
                if (privateKey == null) throw new Exception("Lỗi xác thực, kiểm tra lại đăng nhập.");
                else
                {
                    TimeSpan timeSpan = DateTime.Now - (DateTime)privateKey.SessionDate;
                    if (timeSpan.TotalMinutes > 30) throw new Exception("Lỗi quá thời gian đăng nhập, đăng nhập lại.");
                    else
                    {
                        dl.TrangThai = "OK";
                        var query = from cb in _context.CoBao
                                    join dt in _context.DoanThong on cb.CoBaoID equals dt.DoanThongID
                                    where dt.ThangDT == thang && dt.NamDT == nam && cb.DvcbID == madv
                                    select cb;
                        if (!string.IsNullOrWhiteSpace(socb))
                        {
                            query = query.Where(x => x.SoCB == socb);
                        }

                        var queryCBID = from cb in query                                        
                                               join ct in _context.CoBaoCT on cb.CoBaoID equals ct.CoBaoID                                               
                                               orderby cb.NhanMay
                                               select ct;

                        dl.DuLieu = await (from s in queryCBID
                                           select new XCoBaoCT
                                           {
                                               ID = s.ID,
                                               CoBaoID = s.CoBaoID,
                                               NgayXP = s.NgayXP,
                                               GioDen = s.GioDen,
                                               GioDi = s.GioDi,
                                               GioDon = s.GioDon,
                                               MacTauID = s.MacTauID,
                                               CongTyID = s.CongTyID,
                                               CongTyName = s.CongTyName,
                                               CongTacID = s.CongTacID,
                                               CongTacName = s.CongTacName,
                                               GaID = s.GaID,
                                               GaName = s.GaName,
                                               TuyenID = s.TuyenID,
                                               TuyenName = s.TuyenName,
                                               Tan = s.Tan,
                                               XeTotal = s.XeTotal,
                                               TanXeRong = s.TanXeRong,
                                               XeRongTotal = s.XeRongTotal,
                                               TinhChatID = s.TinhChatID,
                                               TinhChatName = s.TinhChatName,
                                               MayGhepID = s.MayGhepID,
                                               KmAdd = s.KmAdd
                                           }).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                dl.TrangThai = ex.Message;
            }
            return dl;
        }

        [HttpGet]
        [Route("Getcobaodtdm")]
        public async Task<ActionResult<DL_Cobaodtdm>> Getcobaodtdm(string key, int thang, int nam, string madv, string socb)
        {
            DL_Cobaodtdm dl = new DL_Cobaodtdm();
            try
            {
                string[] strKey = Library.Decrypt(key).Split(',');
                var privateKey = await _context.DL_NguoiDung.Where(x => x.UserName == strKey[0] && x.MaDV == strKey[1] && x.PublicKey == strKey[2]).FirstOrDefaultAsync();
                if (privateKey == null) throw new Exception("Lỗi xác thực, kiểm tra lại đăng nhập.");
                else
                {
                    TimeSpan timeSpan = DateTime.Now - (DateTime)privateKey.SessionDate;
                    if (timeSpan.TotalMinutes > 30) throw new Exception("Lỗi quá thời gian đăng nhập, đăng nhập lại.");
                    else
                    {
                        dl.TrangThai = "OK";
                        var query = from cb in _context.CoBao
                                    join dt in _context.DoanThong on cb.CoBaoID equals dt.DoanThongID
                                    where dt.ThangDT == thang && dt.NamDT == nam && cb.DvcbID == madv
                                    select cb;
                        if (!string.IsNullOrWhiteSpace(socb))
                        {
                            query = query.Where(x => x.SoCB == socb);
                        }
                        var queryCBID = from cb in query                                        
                                        join ct in _context.CoBaoDM on cb.CoBaoID equals ct.CoBaoID
                                        orderby cb.NhanMay
                                        select ct;

                        dl.DuLieu = await (from s in queryCBID
                                           select new XCoBaoDM
                                           {
                                               ID = s.ID,
                                               CoBaoID = s.CoBaoID,
                                               LoaiDauMoID = s.LoaiDauMoID,
                                               LoaiDauMoName = s.LoaiDauMoName,
                                               DonViTinh = s.LoaiDauMoName,
                                               Nhan = s.Nhan,
                                               Linh = s.Linh,
                                               MaTram = s.MaTram,
                                               TenTram = s.TenTram,
                                               Giao = s.Giao
                                           }).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                dl.TrangThai = ex.Message;
            }
            return dl;
        }

        [HttpGet]
        [Route("Getcobaoga")]
        public async Task<ActionResult<DL_Cobaoga>> Getcobaoga(string key, int thang, int nam, string madv, string socb)
        {
            DL_Cobaoga dl = new DL_Cobaoga();
            try
            {
                string[] strKey = Library.Decrypt(key).Split(',');
                var privateKey = await _context.DL_NguoiDung.Where(x => x.UserName == strKey[0] && x.MaDV == strKey[1] && x.PublicKey == strKey[2]).FirstOrDefaultAsync();
                if (privateKey == null) throw new Exception("Lỗi xác thực, kiểm tra lại đăng nhập.");
                else
                {
                    TimeSpan timeSpan = DateTime.Now - (DateTime)privateKey.SessionDate;
                    if (timeSpan.TotalMinutes > 30) throw new Exception("Lỗi quá thời gian đăng nhập, đăng nhập lại.");
                    else
                    {
                        dl.TrangThai = "OK";                       
                        var query = from cb in _context.CoBaoGA
                                    join dt in _context.DoanThongGA on cb.CoBaoID equals dt.DoanThongID
                                    where dt.ThangDT == thang && dt.NamDT == nam && cb.DvcbID == madv
                                    select cb;
                        if (!string.IsNullOrWhiteSpace(socb))
                        {
                            query = query.Where(x => x.SoCB == socb);
                        }

                        dl.DuLieu = await (from s in query orderby s.NhanMay
                                           select new XCoBaoGA
                                           {
                                               CoBaoID = s.CoBaoID,
                                               CoBaoGoc = s.CoBaoGoc,
                                               SoCB = s.SoCB,
                                               DauMayID = s.DauMayID,
                                               LoaiMayID = s.LoaiMayID,
                                               DvdmID = s.DvdmID,
                                               DvdmName = s.DvdmName,
                                               DvcbID = s.DvcbID,
                                               DvcbName = s.DvcbName,
                                               NgayCB = s.NgayCB,
                                               RutGio = s.RutGio,
                                               ChatLuong = s.ChatLuong,
                                               SoLanRaKho = s.SoLanRaKho,
                                               TaiXe1ID = s.TaiXe1ID,
                                               TaiXe1Name = s.TaiXe1Name,
                                               TaiXe1GioLT = s.TaiXe1GioLT,
                                               PhoXe1ID = s.PhoXe1ID,
                                               PhoXe1Name = s.PhoXe1Name,
                                               PhoXe1GioLT = s.PhoXe1GioLT,
                                               TaiXe2ID = s.TaiXe2ID,
                                               TaiXe2Name = s.TaiXe2Name,
                                               TaiXe2GioLT = s.TaiXe2GioLT,
                                               PhoXe2ID = s.PhoXe2ID,
                                               PhoXe2Name = s.PhoXe2Name,
                                               PhoXe2GioLT = s.PhoXe2GioLT,
                                               TaiXe3ID = s.TaiXe3ID,
                                               TaiXe3Name = s.TaiXe3Name,
                                               TaiXe3GioLT = s.TaiXe3GioLT,
                                               PhoXe3ID = s.PhoXe3ID,
                                               PhoXe3Name = s.PhoXe3Name,
                                               PhoXe3GioLT = s.PhoXe3GioLT,
                                               KiemTra1ID = s.KiemTra1ID,
                                               KiemTra1Name = s.KiemTra1Name,
                                               KiemTra2ID = s.KiemTra2ID,
                                               KiemTra2Name = s.KiemTra2Name,
                                               KiemTra3ID = s.KiemTra3ID,
                                               KiemTra3Name = s.KiemTra3Name,
                                               LenBan = s.LenBan,
                                               NhanMay = s.NhanMay,
                                               RaKho = s.RaKho,
                                               VaoKho = s.VaoKho,
                                               GiaoMay = s.GiaoMay,
                                               XuongBan = s.XuongBan,
                                               NLBanTruoc = s.NLBanTruoc,
                                               NLThucNhan = s.NLThucNhan,
                                               NLLinh = s.NLLinh,
                                               TramNLID = s.TramNLID,
                                               NLTrongDoan = s.NLTrongDoan,
                                               NLBanSau = s.NLBanSau,
                                               SHDT = s.SHDT,
                                               MaCB = s.MaCB,
                                               DonDocDuong = s.DonDocDuong,
                                               DungDocDuong = s.DungDocDuong,
                                               DungNoMay = s.DungNoMay,
                                               GhiChu = s.GhiChu
                                           }).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                dl.TrangThai = ex.Message;
            }
            return dl;
        }

        [HttpGet]
        [Route("Getcobaogac")]
        public async Task<ActionResult<DL_Cobaogac>> Getcobaogac(string key, int thang, int nam, string madv, string socb)
        {
            DL_Cobaogac dl = new DL_Cobaogac();
            try
            {
                string[] strKey = Library.Decrypt(key).Split(',');
                var privateKey = await _context.DL_NguoiDung.Where(x => x.UserName == strKey[0] && x.MaDV == strKey[1] && x.PublicKey == strKey[2]).FirstOrDefaultAsync();
                if (privateKey == null) throw new Exception("Lỗi xác thực, kiểm tra lại đăng nhập.");
                else
                {
                    TimeSpan timeSpan = DateTime.Now - (DateTime)privateKey.SessionDate;
                    if (timeSpan.TotalMinutes > 30) throw new Exception("Lỗi quá thời gian đăng nhập, đăng nhập lại.");
                    else
                    {
                        dl.TrangThai = "OK";
                        var query = from cb in _context.CoBaoGA
                                    join dt in _context.DoanThongGA on cb.CoBaoID equals dt.DoanThongID
                                    where dt.ThangDT == thang && dt.NamDT == nam && cb.DvcbID == madv
                                    select cb;
                        if (!string.IsNullOrWhiteSpace(socb))
                        {
                            query = query.Where(x => x.SoCB == socb);
                        }

                        dl.DuLieu = await (from s in query orderby s.NhanMay
                                           select new XCoBaoGAC
                                           {
                                               CoBaoID = s.CoBaoID,
                                               SoCB = s.SoCB,
                                               ModifyDate=s.Modifydate
                                           }).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                dl.TrangThai = ex.Message;
            }
            return dl;
        }

        [HttpGet]
        [Route("Getcobaogact")]
        public async Task<ActionResult<DL_Cobaogact>> Getcobaogact(string key, int thang, int nam, string madv,string socb)
        {
            DL_Cobaogact dl = new DL_Cobaogact();
            try
            {
                string[] strKey = Library.Decrypt(key).Split(',');
                var privateKey = await _context.DL_NguoiDung.Where(x => x.UserName == strKey[0] && x.MaDV == strKey[1] && x.PublicKey == strKey[2]).FirstOrDefaultAsync();
                if (privateKey == null) throw new Exception("Lỗi xác thực, kiểm tra lại đăng nhập.");
                else
                {
                    TimeSpan timeSpan = DateTime.Now - (DateTime)privateKey.SessionDate;
                    if (timeSpan.TotalMinutes > 30) throw new Exception("Lỗi quá thời gian đăng nhập, đăng nhập lại.");
                    else
                    {
                        dl.TrangThai = "OK";
                        var query = from cb in _context.CoBaoGA
                                    join dt in _context.DoanThongGA on cb.CoBaoID equals dt.DoanThongID
                                    where dt.ThangDT == thang && dt.NamDT == nam && cb.DvcbID == madv
                                    select cb;
                        if (!string.IsNullOrWhiteSpace(socb))
                        {
                            query = query.Where(x => x.SoCB == socb);
                        }
                        var queryCBID = from cb in query                                        
                                        join ct in _context.CoBaoGACT on cb.CoBaoID equals ct.CoBaoID                                        
                                        orderby cb.NhanMay
                                        select ct;

                        dl.DuLieu = await (from s in queryCBID
                                           select new XCoBaoGACT
                                           {
                                               CoBaoID = s.CoBaoID,
                                               NgayXP = s.NgayXP,
                                               GioDen = s.GioDen,
                                               GioDi = s.GioDi,
                                               PhutDon = s.PhutDon,
                                               MacTauID = s.MacTauID,
                                               RutGioNL = s.RutGioNL,
                                               DungGioPT = s.DungGioPT,
                                               CongTyID = s.CongTyID,
                                               CongTyName = s.CongTyName,
                                               LoaiTauID = s.LoaiTauID,
                                               LoaiTauName = s.LoaiTauName,
                                               GaID = s.GaID,
                                               GaName = s.GaName,
                                               TuyenID = s.TuyenID,
                                               TuyenName = s.TuyenName,
                                               Tan = s.Tan,
                                               XeTotal = s.XeTotal,
                                               TanXeRong = s.TanXeRong,
                                               XeRongTotal = s.XeRongTotal,
                                               TinhChatID = s.TinhChatID,
                                               TinhChatName = s.TinhChatName,
                                               MayGhepID = s.MayGhepID,
                                               KmAdd = s.KmAdd
                                           }).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                dl.TrangThai = ex.Message;
            }
            return dl;
        }

        [HttpGet]
        [Route("Getcobaogadm")]
        public async Task<ActionResult<DL_Cobaogadm>> Getcobaogadm(string key, int thang, int nam, string madv,string socb)
        {
            DL_Cobaogadm dl = new DL_Cobaogadm();
            try
            {
                string[] strKey = Library.Decrypt(key).Split(',');
                var privateKey = await _context.DL_NguoiDung.Where(x => x.UserName == strKey[0] && x.MaDV == strKey[1] && x.PublicKey == strKey[2]).FirstOrDefaultAsync();
                if (privateKey == null) throw new Exception("Lỗi xác thực, kiểm tra lại đăng nhập.");
                else
                {
                    TimeSpan timeSpan = DateTime.Now - (DateTime)privateKey.SessionDate;
                    if (timeSpan.TotalMinutes > 30) throw new Exception("Lỗi quá thời gian đăng nhập, đăng nhập lại.");
                    else
                    {
                        dl.TrangThai = "OK";
                        var query = from cb in _context.CoBaoGA
                                    join dt in _context.DoanThongGA on cb.CoBaoID equals dt.DoanThongID
                                    where dt.ThangDT == thang && dt.NamDT == nam && cb.DvcbID == madv
                                    select cb;
                        if (!string.IsNullOrWhiteSpace(socb))
                        {
                            query = query.Where(x => x.SoCB == socb);
                        }
                        var queryCBID = from cb in query                                        
                                        join ct in _context.CoBaoGADM on cb.CoBaoID equals ct.CoBaoID                                        
                                        orderby cb.NhanMay
                                        select ct;

                        dl.DuLieu = await (from s in queryCBID
                                           select new XCoBaoGADM
                                           {
                                               CoBaoID = s.CoBaoID,
                                               LoaiDauMoID = s.LoaiDauMoID,
                                               LoaiDauMoName = s.LoaiDauMoName,
                                               DonViTinh = s.LoaiDauMoName,
                                               Nhan = s.Nhan,
                                               Linh = s.Linh,
                                               MaTram = s.MaTram,
                                               TenTram = s.TenTram,
                                               Giao = s.Giao
                                           }).ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                dl.TrangThai = ex.Message;
            }
            return dl;
        }
    }
}
