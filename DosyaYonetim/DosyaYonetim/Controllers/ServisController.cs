
using DosyaYonetim.Models;
using DosyaYonetim.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DosyaYonetim.Controllers
{
    // [Authorize]
    public class ServisController : ApiController
    {
        DosyaYonetimDBEntities db = new DosyaYonetimDBEntities();

        SonucModel sonuc = new SonucModel();

        #region Kategori
        [HttpGet]
        [Route("api/kategoriliste")]
        public List<KategoriModel> KategoriListe()
        {
            List<KategoriModel> liste = db.Kategori.Select(x => new KategoriModel() { KategoriId = x.KategoriId, KategoriAdi = x.KategoriAdi, KatDosyaSay = x.Dosya.Count}).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/kategoribyid/{katId}")]
        public KategoriModel KategoriById(int katId)
        {
            KategoriModel kayit = db.Kategori.Where(s => s.KategoriId == katId).Select(x => new KategoriModel() { KategoriId = x.KategoriId, KategoriAdi = x.KategoriAdi, KatDosyaSay = x.Dosya.Count }).SingleOrDefault();
            return kayit;
        }
        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(s => s.KategoriAdi == model.KategoriAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen kategori zaten kayıtlı !";
                return sonuc;
            }
            Kategori yeni = new Kategori();
            yeni.KategoriAdi = model.KategoriAdi;
            db.Kategori.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori ekleme işlemi başarılı !";
            return sonuc;
        }

        [HttpPut]
        [Route("api/kategoriduzenle")]
        public SonucModel KategoriDuzenle(KategoriModel model)
        {
            Kategori kayit = db.Kategori.Where(s => s.KategoriId == model.KategoriId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt bulunamadı !";
                return sonuc;
            }

            kayit.KategoriAdi = model.KategoriAdi;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori düzenleme işlemi başarılı !";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/kategorisil/{katId}")]
        public SonucModel KategoriSil(int katId)
        {
            Kategori kayit = db.Kategori.Where(s => s.KategoriId == katId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt bulunamadı !";
                return sonuc;
            }

            if (db.Dosya.Count(s => s.KategoriId == katId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Dosyaya kayıtlı kategori silinemez !";
                return sonuc;
            }

            db.Kategori.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori silme işlemi başarılı !";
            return sonuc;
        }
        #endregion

        #region Dosya

        [HttpGet]
        [Route("api/dosyaliste")]
        public List<DosyaModel> DosyaListe()
        {
            List<DosyaModel> liste = db.Dosya.Select(x => new DosyaModel()
            {
                DosyaId = x.DosyaId,
                Adi = x.Adi,
                Uzanti = x.Uzanti,
                KategoriId = x.KategoriId,
                KategoriAdi = x.Kategori.KategoriAdi,
                Goruntulenme = x.Goruntulenme,
                Tarih = x.Tarih,
                UyeId = x.UyeId,
                UyeKadi = x.Uye.KullaniciAdi,

            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/dosyalistebykatid/{katId}")]
        public List<DosyaModel> DosyaListeByKatId(int katId)
        {
            List<DosyaModel> liste = db.Dosya.Where(s => s.KategoriId == katId).Select(x => new DosyaModel()
            {
                DosyaId = x.DosyaId,
                Adi = x.Adi,
                Uzanti = x.Uzanti,
                KategoriId = x.KategoriId,
                KategoriAdi = x.Kategori.KategoriAdi,
                Goruntulenme = x.Goruntulenme,
                Tarih = x.Tarih,
                UyeId = x.UyeId,
                UyeKadi = x.Uye.KullaniciAdi,

            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/dosyalistebyuyeid/{uyeId}")]
        public List<DosyaModel> DosyaListeByUyeId(int uyeId)
        {
            List<DosyaModel> liste = db.Dosya.Where(s => s.UyeId == uyeId).Select(x => new DosyaModel()
            {
                DosyaId = x.DosyaId,
                Adi = x.Adi,
                Uzanti = x.Uzanti,
                KategoriId = x.KategoriId,
                KategoriAdi = x.Kategori.KategoriAdi,
                Goruntulenme = x.Goruntulenme,
                Tarih = x.Tarih,
                UyeId = x.UyeId,
                UyeKadi = x.Uye.KullaniciAdi,

            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/dosyabyid/{dosyaId}")]
        public DosyaModel DosyaById(int dosyaId)
        {
            DosyaModel kayit = db.Dosya.Where(s => s.DosyaId == dosyaId).Select(x => new DosyaModel()
            {
                DosyaId = x.DosyaId,
                Adi = x.Adi,
                Uzanti = x.Uzanti,
                Foto = x.Foto,
                KategoriId = x.KategoriId,
                KategoriAdi = x.Kategori.KategoriAdi,
                Goruntulenme = x.Goruntulenme,
                Tarih = x.Tarih,
                UyeId = x.UyeId,
                UyeKadi = x.Uye.KullaniciAdi
            }).SingleOrDefault();
            return kayit;
        }
        [HttpPost]
        [Route("api/dosyaekle")]
        public SonucModel DosyaEkle(DosyaModel model)
        {
            if (db.Dosya.Count(s => s.Adi == model.Adi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen dosya adı zaten kayıtlı !";
                return sonuc;
            }
            Dosya yeni = new Dosya();
            yeni.Adi = model.Adi;
            yeni.Uzanti = model.Uzanti;
            yeni.Tarih = model.Tarih;
            yeni.Goruntulenme = model.Goruntulenme;
            yeni.KategoriId = model.KategoriId;
            yeni.UyeId = model.UyeId;
            yeni.Foto = model.Foto;

            db.Dosya.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Dosya ekleme işlemi başarılı !";
            return sonuc;
        }
        [HttpPut]
        [Route("api/dosyaduzenle")]
        public SonucModel DosyaDuzenle(DosyaModel model)
        {
            Dosya kayit = db.Dosya.Where(s => s.DosyaId == model.DosyaId).SingleOrDefault();
            
            if(kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Aranan dosya kaydı bulunamadı !";
                return sonuc;
            }
            
            Dosya yeni = new Dosya();
            kayit.Adi = model.Adi;
            kayit.Uzanti = model.Uzanti;
            kayit.Tarih = model.Tarih;
            kayit.Goruntulenme = model.Goruntulenme;
            kayit.KategoriId = model.KategoriId;
            kayit.UyeId = model.UyeId;
            kayit.Foto = model.Foto;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Dosya düzenleme işlemi başarılı !";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/dosyasil/{dosyaId}")]
        public SonucModel DosyaSil(int dosyaId)
        {
            Dosya kayit = db.Dosya.Where(s => s.DosyaId == dosyaId).SingleOrDefault();
            if(kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt bulunamadı";
                return sonuc;
            }
            if (db.Yorum.Count(s => s.DosyaId == dosyaId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde yorum olan dosya silinemez !";
                return sonuc;
            }
            db.Dosya.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Dosya silindi !";
            return sonuc;
        }
        
        [HttpGet]
        [Route("api/dosyalistesoneklenenler/{s}")]
        public List<DosyaModel> DosyaListeSonEklenenler(int s)
        {
            List<DosyaModel> liste = db.Dosya.OrderByDescending(o => o.DosyaId).Take(s).Select(x => new DosyaModel()
            {
                DosyaId = x.DosyaId,
                Adi = x.Adi,
                Uzanti = x.Uzanti,
                KategoriId = x.KategoriId,
                KategoriAdi = x.Kategori.KategoriAdi,
                Goruntulenme = x.Goruntulenme,
                Tarih = x.Tarih,
                UyeId = x.UyeId,
                UyeKadi = x.Uye.KullaniciAdi,

            }).ToList();
            return liste;

        }
        #endregion

        #region Uye
        [HttpGet]
        [Route("api/uyeliste")]
        public List<UyeModel> UyeListe()
        {
            List<UyeModel> liste = db.Uye.Select(x => new UyeModel()
            {
                UyeId = x.UyeId,
                AdSoyad = x.AdSoyad,
                Email = x.Email,
                KullaniciAdi = x.KullaniciAdi,
                Foto = x.Foto,
                Sifre = x.Sifre,
                UyeAdmin = x.UyeAdmin   
            }).ToList();
            return liste;
        }
        
        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public UyeModel UyeById(int uyeId)
        {
            UyeModel kayit = db.Uye.Where(s => s.UyeId == uyeId).Select(x => new UyeModel()
            {
                UyeId = x.UyeId,
                AdSoyad = x.AdSoyad,
                Email = x.Email,
                KullaniciAdi = x.KullaniciAdi,
                Foto = x.Foto,
                Sifre = x.Sifre,
                UyeAdmin = x.UyeAdmin
            }).SingleOrDefault();
            return kayit;
        }
        [HttpPost]
        [Route("api/uyeekle")]
        public SonucModel UyeEkle(UyeModel model)
        {
            if (db.Uye.Count(s => s.KullaniciAdi == model.KullaniciAdi || s.Email == model.Email) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen kullanıcı adı veya E-Posta adresi kayıtlıdır !";
                return sonuc;
            }
            
            Uye yeni = new Uye();
            yeni.AdSoyad = model.AdSoyad;
            yeni.Email = model.Email;
            yeni.KullaniciAdi = model.KullaniciAdi;
            yeni.Foto = model.Foto;
            yeni.Sifre = model.Sifre;
            yeni.UyeAdmin = model.UyeAdmin;

            db.Uye.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Uye başarıyla eklendi !";
            return sonuc;
        }

        [HttpPut]
        [Route("api/uyeduzenle")]
        public SonucModel UyeDuzenle(UyeModel model)
        {
            Uye kayit = db.Uye.Where(s => s.UyeId == model.UyeId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Id numarasına sahip üye bulunamadı !";
                return sonuc;
            }
            
            kayit.AdSoyad = model.AdSoyad;
            kayit.Email = model.Email;
            kayit.KullaniciAdi = model.KullaniciAdi;
            kayit.Foto = model.Foto;
            kayit.Sifre = model.Sifre;
            kayit.UyeAdmin = model.UyeAdmin;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye düzenleme işlemi başarılı !";

            return sonuc;
        }
        [HttpDelete]
        [Route("api/uyesil/{uyeId}")]
        public SonucModel UyeSil(int uyeId)
        {
            Uye kayit = db.Uye.Where(s => s.UyeId == uyeId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Id numarasına sahip üye bulunamadı !";
                return sonuc;
            }

            if(db.Dosya.Count(s=> s.UyeId == uyeId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde makale kaydı olan üye silinemez";
                return sonuc;
            }
            if (db.Yorum.Count(s => s.UyeId == uyeId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde yorum kaydı olan üye silinemez";
                return sonuc;
            }
            db.Uye.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye silme işlemi başarılı !";
            return sonuc;
        }


        #endregion
        #region Yorum
        [HttpGet]
        [Route("api/yorumliste")]
        public List<YorumModel> YorumListe()
        {
            List<YorumModel> liste = db.Yorum.Select(x => new YorumModel()
            {
                YorumId=x.YorumId,
                YorumIcerik =x.YorumIcerik,
                DosyaId = x.DosyaId,
                UyeId =x.UyeId,
                Tarih = x.Tarih,
                KullaniciAdi = x.Uye.KullaniciAdi,
                DosyaAdi = x.Dosya.Adi

            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/yorumlistebyuyeid/{uyeId}")]
        public List<YorumModel> YorumListeByUyeId(int uyeId)
        {
            List<YorumModel> liste = db.Yorum.Where(s => s.UyeId == uyeId).Select(x => new YorumModel()
            {
                YorumId = x.YorumId,
                YorumIcerik = x.YorumIcerik,
                DosyaId = x.DosyaId,
                UyeId = x.UyeId,
                Tarih = x.Tarih,
                KullaniciAdi = x.Uye.KullaniciAdi,
                DosyaAdi = x.Dosya.Adi

            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/yorumlistebydosyaid/{dosyaId}")]
        public List<YorumModel> YorumListeByDosyaId(int dosyaId)
        {
            List<YorumModel> liste = db.Yorum.Where(s => s.DosyaId == dosyaId).Select(x => new YorumModel()
            {
                YorumId = x.YorumId,
                YorumIcerik = x.YorumIcerik,
                DosyaId = x.DosyaId,
                UyeId = x.UyeId,
                Tarih = x.Tarih,
                KullaniciAdi = x.Uye.KullaniciAdi,
                DosyaAdi = x.Dosya.Adi
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/yorumlistesoneklenenler/{s}")]
        public List<YorumModel> YorumListeSonEklenenler(int s)
        {
            List<YorumModel> liste = db.Yorum.OrderByDescending(o => o.YorumId).Take(s).Select(x => new YorumModel()
            {
                YorumId = x.YorumId,
                YorumIcerik = x.YorumIcerik,
                DosyaId = x.DosyaId,
                UyeId = x.UyeId,
                Tarih = x.Tarih,
                KullaniciAdi = x.Uye.KullaniciAdi,
                DosyaAdi = x.Dosya.Adi
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/yorumbyid/{yorumId}")]
        public YorumModel YorumById(int yorumId)
        {
            YorumModel kayit = db.Yorum.Where(s => s.YorumId == yorumId).Select(x => new YorumModel()
            {
                YorumId = x.YorumId,
                YorumIcerik = x.YorumIcerik,
                DosyaId = x.DosyaId,
                UyeId = x.UyeId,
                Tarih = x.Tarih,
                KullaniciAdi = x.Uye.KullaniciAdi,
                DosyaAdi = x.Dosya.Adi
            }).SingleOrDefault();
            
            return kayit;
        }
        [HttpPost]
        [Route("api/yorumekle")]
        public SonucModel YorumEkle(YorumModel model)
        {
            if (db.Yorum.Count(s =>s.UyeId == model.UyeId && s.DosyaId == model.DosyaId && s.YorumIcerik == model.YorumIcerik) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bir üye aynı dosyaya birden fazla yorum ekleyemez !";
                return sonuc;
            }

            Yorum yeni = new Yorum();
            yeni.YorumId = model.YorumId;
            yeni.YorumIcerik = model.YorumIcerik;
            yeni.DosyaId = model.DosyaId;
            yeni.UyeId = model.UyeId;
            yeni.Tarih = model.Tarih;
            db.Yorum.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yorumunuz başarıyla eklendi !";
            return sonuc;
        }
        [HttpPut]
        [Route("api/yorumduzenle")]
        public SonucModel YorumDuzenle(YorumModel model)
        {
            Yorum kayit = db.Yorum.Where(s => s.YorumId == model.YorumId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Düzenlemek istediğiniz yorum kaydı bulunamadı !";
                return sonuc;
            }
            kayit.YorumId = model.YorumId;
            kayit.YorumIcerik = model.YorumIcerik;
            kayit.DosyaId = model.DosyaId;
            kayit.UyeId = model.UyeId;
            kayit.Tarih = model.Tarih;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yorumunuz düzenleme işlemi başarılı !";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/yorumsil/{yorumId}")]
        public SonucModel YorumSil(int yorumId)
        {
            Yorum kayit = db.Yorum.Where(s => s.YorumId == yorumId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Silmek istediğiniz yorum kaydı bulunamadı !";
                return sonuc;
            }

            db.Yorum.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Yorum silme işlemi başarılı";
            return sonuc;
        }
        #endregion
    }
}
