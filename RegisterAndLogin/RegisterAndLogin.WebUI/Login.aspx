<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RegisterAndLogin.WebUI.Login" %>

<!DOCTYPE html>
<html>
<head>
    <title>Giriş Sayfası</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
</head>
<body>
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header">
                        <h4 class="mb-0">Müşteri Girişi</h4>
                    </div>
                    <div class="card-body">
                        <form id="loginForm">
                            <div class="form-group">
                                <label for="musteriKullaniciAdi">Müşteri Kullanıcı Adı:</label>
                                <input type="text" class="form-control" id="musteriKullaniciAdi" name="musteriKullaniciAdi" required>
                            </div>
                            <div class="form-group">
                                <label for="sifre">Şifre:</label>
                                <input type="password" class="form-control" id="sifre" name="sifre" required>
                            </div>
                            <a class="btn btn-primary" href="HomePage.aspx" id="loginButton">Giriş Yap</a>
                            <div id="responseMessage" class="mt-3"></div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        document.getElementById("loginButton").addEventListener("click", function () {
            var apiUrl = "https://localhost:44398/api/Musteriler/login";

            var requestData = {
                musteriKullaniciAdi: document.getElementById("musteriKullaniciAdi").value,
                sifre: document.getElementById("sifre").value
            };

            var responseMessageElement = document.getElementById("responseMessage");

            // Giriş doğrulama
            if (!requestData.musteriKullaniciAdi || !requestData.sifre) {
                responseMessageElement.innerHTML = "Lütfen kullanıcı adı ve şifre girin.";
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
                        throw new Error("Giriş başarısız.");
                    }
                })
                .then(function (data) {
                    responseMessageElement.innerHTML = "Giriş başarılı: " + data;
                    responseMessageElement.innerHTML += "<br>Yönlendiriliyor...";
                    setTimeout(function () {
                        window.location.href = "HomePage.aspx"; // Yönlendirilecek sayfanın adresi
                    }, 2000); // 2 saniye sonra yönlendirme gerçekleşecek
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

