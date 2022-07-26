# TelephoneDirectory
microservices-> .net 6.0, golang - Docker, Jenkins, Ocelot, RabbitMq, MassTransit, Serilog, Elasticsearch, Kibana, Swagger / SwaggerForOcelot, Jwt, Postgresql, Mssql
Bu projeyi daha ayrıntılı ele aldığım blog yazısı: https://medium.com/software-development-turkey/mikroservis-maceram-1e070463d0ea 

##### Projeyi Ayağa Kaldırmak

Proje Code First mantığıyla oluşturulmuştur. Bu şekilde kod tarafından veri tabanını ayağa kaldırabileceğiz.
Veritabanı olarak postgre ve mssql kullanıldı. 
Burada her servise ait farklı bir veri tabanı bulunmaktadır. Bu şekilde sadece bir veritabanına olan bağımlılık ortadan kaldırılmıştır.

```
* add-migration Initial -> .net core console için : dotnet ef migrations add Initial
* update-database -> .net core console için : dotnet ef database update
```
Not: Projede içerisindeki mevcut migration'lar otomatik olarak ayağa kalktığından herhangi bir işlem yapılmasına gerek yoktur.

Genel Yapı
- Api Gateway -> Ocelot
- Message Broker -> Rabbitmq
- Service Bus -> Mass Transit
- Health Check -> Watchdog - (via slack implementation)
- Continuous Integration -> Jenkins
- Logging -> Serilog, elastic search and kibana
- Databases -> Postgre and Mssql
- Open Doc -> Swagger and SwaggerForOcelot
- Applied Pattern -> DDD, Saga Pattern, Mediator, CQRS, Circuit Breaker, Event Sourcing, Publish-Subscribe
- Tests -> xUnit (via MassTransit) - Mocking, Integration and Functional Tests

Bulunan Microservisler
- Ocelot - Gateway, .net6  - Aradaki kordinasyonu sağlar.
- Guide - Postgre, .net6 - Rehber ve iletişim(1-N) bilgileriyle ilgili her türlü CRUD işlemini yapabilmekteyiz.
- Report - Postgre, .net6 - Rehberdeki kişiler ve bunların bulunduğu yerlerle ilgili bazı rapor verilmektedir.
- Auth - MSSQL, .net6 - Kullanıcıları tutuyoruz. Jwt ile authentication yapılmaktadır.
- Example - No DB, golang - Basit bir golang projesi oluşturuldu. Geliştirilmesi devam ediyor.
- Saga - Transaction yönetimi için Saga Pattern'ın Orchestration yöntemi kullanılmıştır.

Kurulum
docker-compose.yml dosyası sayesinde hem kullanılan araçlar hem veri tabanları ayağa kalkacaktır.
```
* docker-compose up -d -> yapmak yeterli olacaktır.
```


- Unit Test'ler eklenmeye başlandı. Consumer'ler için mass transsit ile kullanmaya başladık. Ayrıca mock'lama kullanılmaktadır.<br />
https://www.youtube.com/watch?v=Cx-Mc0DCpfE&ab_channel=ChrisPatterson - Mass Transit <br />
https://medium.com/software-development-turkey/birim-unit-test-ile-veri-k%C3%BCmeleri-xunit-inline-member-class-data-601b3fb4e723 <br />
https://www.youtube.com/watch?v=6oxNumwFmR0&t=1s&ab_channel=MalikMasis - Mocking

- Integration Test: <br />
https://medium.com/software-development-turkey/integration-test-net-core-xunit-web-application-factory-600ca6a52223 <br />
https://www.youtube.com/watch?v=My0FdMKq2JA&t=4s&ab_channel=MalikMasis
