# TelephoneDirectory
Telephone Directory with micro services and .net core 3.1 

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

Rabbitmq ile oluşan event'leri diğer servislere bildiriyoruz.