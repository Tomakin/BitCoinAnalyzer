# BitCoinAnalyzer

Projeyi 2 �ekilde aya�a kald�rabilirsiniz.

1. Y�ntem: localde development ortam�nda
2. Y�ntem: docker-compose ile(tavsiye)


1.1 �lk y�ntem i�in gerekli toollar:
- .NET 6 SDK
- SQL Server
- NPM

1.2 Gerekli a�amalar:
- Proje gitten locale kurulur ve paketleri restore edilir.
- Sisteme kurulan SQL Server credentiallar�na g�re package.json'daki connectionString d�zenlenir.

2.1 Y�ntem i�in gerekli Toollar:
- Docker

2.2 Gerekli a�amalar
- root dizinindeyken(docker-compose.yml dosyas�n�n bulundu�u dizin) bir terminal arac�l��� ile "docker-compose up" komutu �al��t�r�l�r
