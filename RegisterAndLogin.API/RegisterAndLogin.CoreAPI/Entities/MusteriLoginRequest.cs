using System.ComponentModel.DataAnnotations;

namespace RegisterAndLogin.CoreAPI.Entities
{
    public class MusteriLoginRequest
    {
        [Required(ErrorMessage = "Musteri Kullanici Adi is required.")]
        public string MusteriKullaniciAdi { get; set; }


        [Required(ErrorMessage = "Sifre is required.")]
        public string Sifre { get; set; }
    }
}
