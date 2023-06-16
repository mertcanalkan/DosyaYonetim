using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DosyaYonetim.ViewModel
{
    public class YorumModel
    {
        public int YorumId { get; set; }
        public string YorumIcerik { get; set; }
        public int UyeId { get; set; }
        public int DosyaId { get; set; }
        public string KullaniciAdi { get; set; }
        public string DosyaAdi { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
    }
}