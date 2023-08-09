using System.ComponentModel.DataAnnotations;
using System;

namespace RegisterAndLogin.CoreAPI.Entities
{
    public class Musteriler
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Musteri Adi is required.")]
        public string MusteriAdi { get; set; }

        [Required(ErrorMessage = "Musteri Soyadi is required.")]
        public string MusteriSoyadi { get; set; }

        [Required(ErrorMessage = "Musteri Kullanici Adi is required.")]
        public string MusteriKullaniciAdi { get; set; }

        [Required(ErrorMessage = "Email Adresi is required.")]
        [EmailAddress(ErrorMessage = "Invalid email adresi format.")]
        public string EmailAdresi { get; set; }

        [Required(ErrorMessage = "Telefon Numarasi is required.")]
        [Phone(ErrorMessage = "Invalid telefon numarasi format.")]
        public string TelefonNumarasi { get; set; }

        [Required(ErrorMessage = "Sifre is required.")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "Musteri Oluşturma Tarihi is required.")]
        public DateTime MusteriOluşturmaTarihi { get; set; }


        public MusteriBankaHesaplari MusteriBankaHesaplari { get; set; }
    }
}
