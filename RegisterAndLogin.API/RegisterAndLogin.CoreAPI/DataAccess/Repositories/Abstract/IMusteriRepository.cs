using RegisterAndLogin.CoreAPI.Entities;

namespace RegisterAndLogin.CoreAPI.DataAccess.Repositories.Abstract
{
    public interface IMusteriRepository
    {
        bool RegisterCustomer(Musteriler musteriler);
        Musteriler LoginCustomer(string MusteriKullaniciAdi, string Sifre);
    }
}
