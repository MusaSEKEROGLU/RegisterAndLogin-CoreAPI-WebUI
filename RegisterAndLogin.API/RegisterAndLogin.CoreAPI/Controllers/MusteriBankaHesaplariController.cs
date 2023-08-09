using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegisterAndLogin.CoreAPI.DataAccess.Repositories.Abstract;
using RegisterAndLogin.CoreAPI.Entities;

namespace RegisterAndLogin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusteriBankaHesaplariController : ControllerBase
    {
        private readonly IMusteriBankaHesaplariRepository _repository;

        public MusteriBankaHesaplariController(IMusteriBankaHesaplariRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{MusteriId}")]
        public IActionResult GetCustomerWithBankAccounts(int MusteriId)
        {
            var musteri = _repository.GetCustomerWithBankAccounts(MusteriId);

            if (musteri == null)
            {
                return NotFound("Musteri Bulunamadı."); // Eğer müşteri bulunamazsa 404 döndürür.
            }

            return Ok(musteri); // Müşteri bulunursa 200 OK döndürür.
        }

        [HttpPost]
        public IActionResult AddCustomerBankAccounts([FromBody] MusteriBankaHesaplari bankaHesaplari)
        {
            if (bankaHesaplari == null)
            {
                return BadRequest();
            }

            var addedBankaHesaplari = _repository.AddCustomerBankAccounts(bankaHesaplari);

            return CreatedAtRoute("InsertMusteriBankaHesap", new { id = addedBankaHesaplari.MusteriId }, addedBankaHesaplari);
        }
    }
}
