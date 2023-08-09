using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RegisterAndLogin.CoreAPI.DataAccess.Contexts;
using RegisterAndLogin.CoreAPI.DataAccess.Repositories.Abstract;
using RegisterAndLogin.CoreAPI.Entities;
using System;
using System.Data;

namespace RegisterAndLogin.API.DataAccess.Repositories.Concrete
{
    public class MusteriBankaHesaplariRepository : IMusteriBankaHesaplariRepository
    {
        private readonly BankingDbContext _dbContext;
     

        public MusteriBankaHesaplariRepository(BankingDbContext dbContext)
        {
            _dbContext = dbContext;           
        }

        public MusteriBankaHesaplari AddCustomerBankAccounts(MusteriBankaHesaplari bankaHesaplari)
        {
            using (SqlConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("InsertMusteriBankaHesap", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parametreleri ekleyin
                    command.Parameters.AddWithValue("@MusteriId", bankaHesaplari.MusteriId);
                    command.Parameters.AddWithValue("@HesapNumarasi", bankaHesaplari.HesapNumarasi);
                    command.Parameters.AddWithValue("@IbanNumarasi", bankaHesaplari.IbanNumarasi);
                    command.Parameters.AddWithValue("@HesapTuru", bankaHesaplari.HesapTuru);
                    command.Parameters.AddWithValue("@SubeAdi", bankaHesaplari.SubeAdi);
                    command.Parameters.AddWithValue("@SubeKodu", bankaHesaplari.SubeKodu);
                    command.Parameters.AddWithValue("@HesapBakiyesi", bankaHesaplari.HesapBakiyesi);
                    command.Parameters.AddWithValue("@KullanılabilirBakiye", bankaHesaplari.KullanılabilirBakiye);
                    command.Parameters.AddWithValue("@HesapAcilmaTarihi", bankaHesaplari.HesapAcilmaTarihi);
                  

                    command.ExecuteNonQuery();
                }
                return bankaHesaplari;
            }
        }

        public Musteriler GetCustomerWithBankAccounts(int Id)
        {
            try
            {
                Musteriler musteriler = null;
                //Musteriler musteriler = null;
                using (SqlConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("MusteriVeBankaHesaplarınıGetir", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", Id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                musteriler = new Musteriler
                                {
                                    Id = (int)reader["Id"],
                                    MusteriAdi = reader["MusteriAdi"].ToString(),
                                    MusteriSoyadi = reader["MusteriSoyadi"].ToString(),
                                    MusteriKullaniciAdi = reader["MusteriKullaniciAdi"].ToString(),
                                    EmailAdresi = reader["EmailAdresi"].ToString(),
                                    TelefonNumarasi = reader["TelefonNumarasi"].ToString(),
                                    MusteriOluşturmaTarihi = Convert.ToDateTime(reader["MusteriOluşturmaTarihi"]),
                                    MusteriBankaHesaplari = new MusteriBankaHesaplari
                                    {
                                        Id = (int)reader["Id"],
                                        MusteriId = (int)reader["MusteriId"],
                                        HesapNumarasi = reader["HesapNumarasi"].ToString(),
                                        IbanNumarasi = reader["IbanNumarasi"].ToString(),
                                        HesapTuru = reader["HesapTuru"].ToString(),
                                        SubeAdi = reader["SubeAdi"].ToString(),
                                        SubeKodu = (int)reader["SubeKodu"],
                                        HesapBakiyesi = Convert.ToDecimal(reader["HesapBakiyesi"]),
                                        KullanılabilirBakiye = Convert.ToDecimal(reader["KullanılabilirBakiye"]),
                                        HesapAcilmaTarihi = Convert.ToDateTime(reader["HesapAcilmaTarihi"]),
                                    }
                                };
                            }
                        }
                    }
                    return musteriler;
                }
            }
            catch
            {

                return null;
            }
        }
    }
}

