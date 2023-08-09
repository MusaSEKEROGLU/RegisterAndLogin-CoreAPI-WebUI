using RegisterAndLogin.CoreAPI.Entities;

namespace RegisterAndLogin.CoreAPI.DataAccess.Repositories.Abstract
{
    public interface IMusteriBankaHesaplariRepository
    {
        Musteriler GetCustomerWithBankAccounts(int Id);
        MusteriBankaHesaplari AddCustomerBankAccounts(MusteriBankaHesaplari bankaHesaplari);
    }
}
