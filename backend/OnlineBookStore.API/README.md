# Online Kitap Mağazası - Backend

## Kurulum ve Çalıştırma

1. Gerekli NuGet paketleri yüklü olmalı (Entity Framework, JWT, Swagger vb.).
2. `appsettings.json` dosyasındaki veritabanı bağlantı bilgisini güncelleyin.
3. Migration oluşturmak için:

   ```sh
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. Uygulamayı başlatmak için:

   ```sh
   dotnet run
   ```

5. Swagger arayüzü ile API uç noktalarını test edebilirsiniz: `https://localhost:5001/swagger`

## Örnek secrets.json

Kişisel ayarlarınızı saklamak için (kökte veya kullanıcı profili altında):

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=OnlineBookStoreDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "CokGizliBirAnahtarBurayaGelecek123!",
    "Issuer": "OnlineBookStore",
    "Audience": "OnlineBookStoreUsers"
  }
}
```

## Notlar
- Geliştirme ortamında HTTPS ve CORS açıktır.
- JWT ayarları ve connection string güvenliğiniz için production ortamında değiştirilmeli.
- XSS, CSRF ve SQL Injection'a karşı Entity Framework, JWT ve HTTPS ile temel koruma sağlanır. 