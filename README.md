# TelephoneDirectory
Telephone Directory with micro services and .net core 3.1 
Ocelot, rabbit mq, mass transit

##### Projeyi Ayağa Kaldırmak

Proje Code First mantığıyla oluşturulmuştur. Bu şekilde kod tarafından veri tabanını ayağa kaldırabileceğiz.
Veritabanı olarak postgre sql kullanıldı. Hem open source olması hem de platform bağımsız çalışması dolayısıyla seçildi.
Burada her servise ait farklı bir veri tabanı bulunmaktadır. Bu şekilde sadece bir veritabanına olan bağımlılık ortadan kaldırılmıştır.
İleri bir süreçte loglar için mongo db kullanmak isteyeceğiz.

```
* add-migration Initial -> .net core console için : dotnet ef migrations add Initial
* update-database -> .net core console için : dotnet ef database update
```
Not: Her iki servis için de bu işlemleri yapmak gerekmektedir.

1 genel (ocelot) 2 de iş odaklı servisimiz bulunmaktadır.
-  Ocelot api gateway işlemini üstleniş olacaktır.
- Guide servisimizde rehber ve iletişim(1-N) bilgileriyle ilgili her türlü CRUD işlemini yapabilmekteyiz.
- Report servisimizde ise rehberdeki kişiler ve bunların bulunduğu yerlerle ilgili bazı rapor verilmektedir.

Rabbitmq ve Mass transit ikilisi ile eventleri publish-consume pattern ile haberleştiriyoruz.
Rabbitmq için aşağıdaki 2 dosyayı kurup gerek ayarlamaları yapmak gerekecektir. (windwos)
https://www.erlang.org/downloads 
https://www.rabbitmq.com/install-windows.html#installer

Not: docs başlığı altında postman dosyasına erişebilirsiniz.

Unit test ve Integration testleri de ekliyor olacağız. Aşağıdaki linkler bize bu konuda yardımcı olacaktır.

- Unit Test:
https://medium.com/software-development-turkey/birim-unit-test-ile-veri-k%C3%BCmeleri-xunit-inline-member-class-data-601b3fb4e723

- Integration Test:
https://medium.com/software-development-turkey/integration-test-net-core-xunit-web-application-factory-600ca6a52223