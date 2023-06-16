using DosyaYonetim.Models;
using DosyaYonetim.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DosyaYonetim.Auth
{
    public class UyeService
    {
        DosyaYonetimDBEntities db = new DosyaYonetimDBEntities();
        public UyeModel UyeOturumAc(string kullaniciadi, string parola)
        {
            UyeModel uye =db.Uye.Where(s => s.KullaniciAdi == kullaniciadi && s.Sifre == parola).Select(x => new UyeModel() { 
            KullaniciAdi = x.KullaniciAdi,
            AdSoyad = x.AdSoyad,
            Email = x.Email,
            Sifre = x.Sifre,
            UyeId = x.UyeId,
            UyeAdmin = x.UyeAdmin,
            Foto = x.Foto
            }).SingleOrDefault();
            
            return uye;
        }
    }
}