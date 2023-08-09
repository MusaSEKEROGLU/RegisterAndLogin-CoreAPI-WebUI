using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RegisterAndLogin.CoreAPI.DataAccess.Contexts;
using RegisterAndLogin.CoreAPI.DataAccess.Repositories.Abstract;
using RegisterAndLogin.CoreAPI.Entities;
using System;
using System.Data;

namespace RegisterAndLogin.API.DataAccess.Repositories.Concrete
{
    public class MusteriRepository : IMusteriRepository
    {
        private readonly BankingDbContext _dbContext;
        public MusteriRepository(BankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool RegisterCustomer(Musteriler musteriler)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_MusteriRegister", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MusteriAdi", musteriler.MusteriAdi);
                        cmd.Parameters.AddWithValue("@MusteriSoyadi", musteriler.MusteriSoyadi);
                        cmd.Parameters.AddWithValue("@MusteriKullaniciAdi", musteriler.MusteriKullaniciAdi);
                        cmd.Parameters.AddWithValue("@EmailAdresi", musteriler.EmailAdresi);
                        cmd.Parameters.AddWithValue("@TelefonNumarasi", musteriler.TelefonNumarasi);
                        cmd.Parameters.AddWithValue("@Sifre", musteriler.Sifre);
                        cmd.Parameters.AddWithValue("@MusteriOluşturmaTarihi", DateTime.Now);

                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch
            {             
                return false;
            }
        }

        public Musteriler LoginCustomer(string MusteriKullaniciAdi, string Sifre)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_MusteriLogin", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MusteriKullaniciAdi", MusteriKullaniciAdi);
                        cmd.Parameters.AddWithValue("@Sifre", Sifre);
                        connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Musteriler
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    MusteriAdi = reader["MusteriAdi"].ToString(),
                                    MusteriSoyadi = reader["MusteriSoyadi"].ToString(),
                                    MusteriKullaniciAdi = reader["MusteriKullaniciAdi"].ToString(),
                                    EmailAdresi = reader["EmailAdresi"].ToString(),
                                    TelefonNumarasi = reader["TelefonNumarasi"].ToString(),
                                    Sifre = reader["Sifre"].ToString(),
                                    MusteriOluşturmaTarihi = Convert.ToDateTime(reader["MusteriOluşturmaTarihi"])
                                };
                            }
                        }
                    }
                }
                return null; 
            }
            catch 
            {           
                return null;
            }
        }
    }
}

