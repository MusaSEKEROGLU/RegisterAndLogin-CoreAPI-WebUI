using System;

namespace RegisterAndLogin.CoreAPI.Entities
{
    public class MusteriBankaHesaplari
    {
        public int Id { get; set; }
        public int MusteriId { get; set; }
        public string HesapNumarasi { get; set; }
        public string IbanNumarasi { get; set; }
        public string HesapTuru { get; set; }
        public string SubeAdi { get; set; }
        public int SubeKodu { get; set; }
        public decimal HesapBakiyesi { get; set; }
        public decimal KullanılabilirBakiye { get; set; }
        public DateTime HesapAcilmaTarihi { get; set; }
    }
}
