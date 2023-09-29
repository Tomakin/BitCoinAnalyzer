# BitCoinAnalyzer

Bu proje, Bitcoin fiyatlarını analiz etmek için kullanılan bir uygulamayı içerir.

## Yerel Geliştirme Ortamında Başlatma

### Gereksinimler
- .NET 6 SDK
- SQL Server
- Node.js ve NPM

### Kurulum Adımları
1. Projeyi bu repo üzerinden yerel makinenize klonlayın.
2. Terminali açın ve proje dizinine gidin.
3. Paketleri yüklemek için .sln dosyasının olduğu konumda aşağıdaki komutu;

   ```bash
   dotnet restore
   ```

4. BitCoinAnalyzer.API/ClientApp/ altında ise aşağıdaki komutu çalıştırın   

   ```bash
   npm install
   ```

5. SQL Server'a bağlantı için `appsettings.json` dosyasını düzenleyin. Bağlantı dizesini kendi SQL Server kimlik bilgilerinize göre güncelleyin.
6. Backend projesini aşağıdaki komutla başlatın:

   ```bash
   dotnet run --project Backend
   ```

   Bu, uygulamayı `https://localhost:44396/` adresinde çalıştıracaktır.

7. Frontend projesini başlatmak için proje kök dizininde aşağıdaki komutu çalıştırın:

   ```bash
   npm start
   ```

   Bu, uygulamayı `http://localhost:3000/` adresinde başlatacaktır.

## Docker ile Başlatma (Tavsiye Edilen Yöntem)

### Gereksinimler
- Docker

### Kurulum Adımları
1. Proje dizininde, `docker-compose.yml` dosyasının bulunduğu yerde bir terminal açın.
2. Aşağıdaki komutu çalıştırarak Docker konteynerlerini başlatın:

   ```bash
   docker-compose up
   ```

   Bu komut, backend'i `http://localhost:80/` adresinde, frontend'i ise `http://localhost:3000/` adresinde başlatacaktır.

Bu adımları takip ederek projenizi yerel geliştirme ortamınızda veya Docker kullanarak başlatabilirsiniz.

> Not: Backend tarafında Code First yaklaşımı kullanıldığı için Migration işlemine ihtiyaç duyulmamaktadır.

