# TelephoneDirectory
microservices-> .net core 3.1 - Ocelot, RabbitMq, MassTransit, Serilog, Seq, Swagger / SwaggerForOcelot, Jwt, Postgresql, Mssql

##### Projeyi Ayağa Kaldırmak

Proje Code First mantığıyla oluşturulmuştur. Bu şekilde kod tarafından veri tabanını ayağa kaldırabileceğiz.
Veritabanı olarak postgre ve mssql kullanıldı. 
Burada her servise ait farklı bir veri tabanı bulunmaktadır. Bu şekilde sadece bir veritabanına olan bağımlılık ortadan kaldırılmıştır.

```
* add-migration Initial -> .net core console için : dotnet ef migrations add Initial
* update-database -> .net core console için : dotnet ef database update
```
Not: Bütün servisler için bu işlemleri yapmak gerekmektedir.

Genel Yapı
- Api Gateway -> Ocelot
- Message Broker -> Rabbitmq
- Service Bus -> Mass Transit
- Logging -> Serilog and Seq
- Database -> Postgre and Mssql
- Open Doc -> Swagger and SwaggerForOcelot

Bulunan Microservisler
- Ocelot - Gateway - Aradaki kordinasyonu sağlar.
- Guide - Postgre - Rehber ve iletişim(1-N) bilgileriyle ilgili her türlü CRUD işlemini yapabilmekteyiz.
- Report - Postgre - Rehberdeki kişiler ve bunların bulunduğu yerlerle ilgili bazı rapor verilmektedir.
- Auth - MSSQL - Kullanıcıları tutuyoruz. Jwt ile authentication yapılmaktadır.

Kurulum
Rabbitmq için aşağıdaki 2 dosyayı kurup gerek ayarlamaları yapmak gerekecektir. (windwos)
https://www.erlang.org/downloads 
https://www.rabbitmq.com/install-windows.html#installer

seq ile logları görüntülemek için
https://datalust.co/download


Unit test ve Integration testleri de ekliyor olacağız. Aşağıdaki linkler bize bu konuda yardımcı olacaktır.

- Unit Test:
https://medium.com/software-development-turkey/birim-unit-test-ile-veri-k%C3%BCmeleri-xunit-inline-member-class-data-601b3fb4e723

- Integration Test:
https://medium.com/software-development-turkey/integration-test-net-core-xunit-web-application-factory-600ca6a52223

Not: docs klasörü altında postman koleskiyon dosyasına erişebilirsiniz.