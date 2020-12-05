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