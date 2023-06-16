using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DosyaYonetim.ViewModel
{
    public class DosyaModel
    {
        public int DosyaId { get; set; }
        public string Adi { get; set; }
        public string Uzanti { get; set; }
        public string Foto { get; set; }
        public DateTime? Tarih { get; set; }
        public int KategoriId { get; set; }
        public string KategoriAdi { get; set; }
        public int UyeId { get; set; }
        public string UyeKadi { get; set; }
        public int Goruntulenme { get; set; }
    }
}