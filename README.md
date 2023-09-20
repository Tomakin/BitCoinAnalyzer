# BitCoinAnalyzer

Projeyi 2 þekilde ayaða kaldýrabilirsiniz.

1. Yöntem: localde development ortamýnda
2. Yöntem: docker-compose ile(tavsiye)


1.1 Ýlk yöntem için gerekli toollar:
- .NET 6 SDK
- SQL Server
- NPM

1.2 Gerekli aþamalar:
- Proje gitten locale kurulur ve paketleri restore edilir.
- Sisteme kurulan SQL Server credentiallarýna göre package.json'daki connectionString düzenlenir.

2.1 Yöntem için gerekli Toollar:
- Docker

2.2 Gerekli aþamalar
- root dizinindeyken(docker-compose.yml dosyasýnýn bulunduðu dizin) bir terminal aracýlýðý ile "docker-compose up" komutu çalýþtýrýlýr
