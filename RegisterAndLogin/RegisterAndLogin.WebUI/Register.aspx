<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="RegisterAndLogin.WebUI.Register" %>

<!DOCTYPE html>
<html>
<head>
    <title>POST İşlemi</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
</head>
<body>
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header">
                        <h4 class="mb-0">Müşteri Kayıt Alanı</h4>
                    </div>
                    <div class="card-body">
                        <form id="postForm">
                            <div class="form-group">
                                <label for="musteriAdi">Müşteri Adı:</label>
                                <input type="text" class="form-control" id="musteriAdi" name="musteriAdi" required>
                            </div>
                            <div class="form-group">
                                <label for="musteriSoyadi">Müşteri Soyadı:</label>
                                <input type="text" class="form-control" id="musteriSoyadi" name="musteriSoyadi" required>
                            </div>
                            <div class="form-group">
                                <label for="musteriKullaniciAdi">Müşteri Kullanıcı Adı:</label>
                                <input type="text" class="form-control" id="musteriKullaniciAdi" name="musteriKullaniciAdi" required>
                            </div>
                            <div class="form-group">
                                <label for="emailAdresi">E-mail Adresi:</label>
                                <input type="email" class="form-control" id="emailAdresi" name="emailAdresi" required>
                            </div>
                            <div class="form-group">
                                <label for="telefonNumarasi">Telefon Numarası:</label>
                                <input type="number" class="form-control" id="telefonNumarasi" name="telefonNumarasi" pattern="[0-9]{11}" required>
                                <small class="form-text text-muted">Örnek: 12345678901 (11 rakam)</small>
                            </div>
                            <div class="form-group">
                                <label for="sifre">Şifre:</label>
                                <input type="password" class="form-control" id="sifre" name="sifre" required>
                            </div>
                            <a class="btn btn-primary" href="Login.aspx" id="postButton">Kayıt Ol</a>
                            <div id="responseMessage" class="mt-3"></div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        document.getElementById("postButton").addEventListener("click", function () {
            var apiUrl = "https://localhost:44398/api/Musteriler/register";

            var requestData = {
                musteriAdi: document.getElementById("musteriAdi").value,
                musteriSoyadi: document.getElementById("musteriSoyadi").value,
                musteriKullaniciAdi: document.getElementById("musteriKullaniciAdi").value,
                emailAdresi: document.getElementById("emailAdresi").value,
                telefonNumarasi: document.getElementById("telefonNumarasi").value,
                sifre: document.getElementById("sifre").value,
                musteriOlusturmaTarihi: new Date().toISOString()
            };

            var responseMessageElement = document.getElementById("responseMessage");

            // Giriş doğrulama
            if (!requestData.musteriAdi || !requestData.musteriSoyadi || !requestData.musteriKullaniciAdi || 
                !requestData.emailAdresi || !requestData.telefonNumarasi || !requestData.sifre) {
                responseMessageElement.innerHTML = "Lütfen tüm alanları doldurun.";
                return;
            }

            // Telefon numarası doğrulama
            if (!/^(\d{11})$/.test(requestData.telefonNumarasi)) {
                responseMessageElement.innerHTML = "Telefon numarası 11 rakam olmalıdır.";
                return;
            }

            // İstek gönderme
            fetch(apiUrl, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(requestData)
            })
            .then(function (response) {
                if (response.ok) {
                    return response.text();
                } else {
                    throw new Error("İstek başarısız.");
                }
            })
            .then(function (data) {
                responseMessageElement.innerHTML = "İstek başarıyla gönderildi: " + data;
                responseMessageElement.innerHTML += "<br>Yönlendiriliyor...";
                setTimeout(function () {
                    window.location.href = "Login.aspx"; // Yönlendirilecek sayfanın adresi
                }, 2000); // 2 saniye sonra yönlendirme gerçekleşecek
                document.getElementById("postForm").reset();
            })
            .catch(function (error) {
                responseMessageElement.innerHTML = "Hata oluştu: " + error.message;
            });
        });
    </script>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.1/dist/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
