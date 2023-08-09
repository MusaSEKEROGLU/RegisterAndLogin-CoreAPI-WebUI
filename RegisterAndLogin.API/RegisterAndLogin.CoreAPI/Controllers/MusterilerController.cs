using Microsoft.AspNetCore.Mvc;
using RegisterAndLogin.CoreAPI.DataAccess.Repositories.Abstract;
using RegisterAndLogin.CoreAPI.Entities;
using System.Security.Cryptography;
using System.Text;

namespace RegisterAndLogin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusterilerController : ControllerBase
    {
        private readonly IMusteriRepository _customerRepository;
        public MusterilerController(IMusteriRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost("register")]
        public IActionResult RegisterCustomer([FromBody] Musteriler musteriler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            musteriler.Sifre = HashPassword(musteriler.Sifre);

            if (_customerRepository.RegisterCustomer(musteriler))
            {
                return Ok("Müşteri başarıyla kaydedildi.");
            }
            else
            {
                return BadRequest("Müşteri kaydı başarısız oldu.");
            }
        }

        [HttpPost("login")]
        public IActionResult LoginCustomer([FromBody] MusteriLoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string passwordHash = HashPassword(loginRequest.Sifre); 

            Musteriler musteri = _customerRepository.LoginCustomer(loginRequest.MusteriKullaniciAdi, passwordHash);

            if (musteri != null)
            {
                return Ok("Giriş başarılı.");
            }
            else
            {
                return BadRequest("Geçersiz kimlik bilgileri.");
            }
        }
        //Şifre Hashleme
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    stringBuilder.Append(hash[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
