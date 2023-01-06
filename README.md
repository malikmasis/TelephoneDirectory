# TelephoneDirectory
microservices -> .net 6.0, golang - [Docker](https://medium.com/software-development-turkey/docker-%C3%A7al%C4%B1%C5%9Fma-notlar%C4%B1m-be626fd14cc1), [Tye](https://medium.com/software-development-turkey/deneysel-bir-%C3%BCr%C3%BCn-project-tye-768e335527d2),[Dapr](https://medium.com/software-development-turkey/dapr-ile-ba%C4%9F%C4%B1ms%C4%B1z-servisler-3b3882e58bca), Jenkins, [Ocelot](https://ocelot.readthedocs.io/en/latest/introduction/gettingstarted.html), RabbitMq, [MassTransit](https://medium.com/software-development-turkey/lightweight-service-bus-masstransit-1c31c7a6e483), Serilog, Elasticsearch, Kibana, [Swagger / SwaggerForOcelot](https://feyyazacet.medium.com/swagger-ocelot-swaggerforocelot-1ec49bbf8790), Jwt, Postgresql, Mssql
>If you want to read in detail in Turkish, please visit [here](https://medium.com/software-development-turkey/mikroservis-maceram-1e070463d0ea)

> [Summary of the project - video - Turkish](https://www.youtube.com/watch?v=uXuOKL1cxIE&t=3s&ab_channel=MalikMasis) 

##### Running the project

The project was created with Code First approach. In this way, we will be able to run the database on the code side.
Also, there is a different database for each service. In this way, we are not dependent on only one database.
Postgre and Mssql were used as databases. You can switch them easily.

```
* dotnet ef migrations add Initial
* dotnet ef database update
```
> Note: No need to take any action as the existing migrations in the project are automatically run.

##### General structure
- API Gateway -> Ocelot
- Message Broker -> Rabbitmq, redis(dapr)
- Service Bus -> Mass Transit
- Health Check -> Watchdog - (via slack implementation)
- Continuous Integration -> Jenkins
- Logging -> Serilog, elastic search and kibana
- Databases -> Postgre and Mssql
- Open Doc -> Swagger and SwaggerForOcelot
- Applied Pattern -> DDD, Saga Pattern, Mediator, CQRS, Circuit Breaker, Event Sourcing, Publish-Subscribe, Sidecar
- Tests -> xUnit (via MassTransit) - Mocking, Integration and Functional Tests

##### Existed microservices
- Ocelot - Gateway, .net6  - It provides coordination as a gateway.
- Guide - Postgre, .net6 - We can perform all kinds of CRUD operations related to contacts.
- Report - Postgre, .net6 - There are some reports on the people in the directory.
- Auth - MSSQL, .net6 - We keep users. Authentication is done with JWT.
- Example - No DB, golang - A simple golang project has been created. It consumes events by integrated dapr.
- Saga - Saga Pattern's Orchestration method is used for transaction management.

##### Installation
Thanks to the `docker-compose.yml` file, both the tools used and the databases will run properly.
```
* docker-compose up -d
```
Or
```
* tye run / run-tye.ps1
```

##### Testing

- Unit Tests started to be added. We started to use it with MassTransit for consumers. Also mocking is used.

[MassTransit unit testing - video](https://www.youtube.com/watch?v=Cx-Mc0DCpfE&ab_channel=ChrisPatterson)  <br />
[Unit testing in general - blog - Turkish](https://medium.com/software-development-turkey/birim-unit-test-ile-veri-k%C3%BCmeleri-xunit-inline-member-class-data-601b3fb4e723) <br /> 
[Mocking in unit testing  - video - Turkish](https://www.youtube.com/watch?v=6oxNumwFmR0&t=1s&ab_channel=MalikMasis)
<br />
- Integration Test:

[Integration testing in general - blog - Turkish](https://medium.com/software-development-turkey/integration-test-net-core-xunit-web-application-factory-600ca6a52223) <br />
[Integration testing in general - video - Turkish](https://www.youtube.com/watch?v=My0FdMKq2JA&t=4s&ab_channel=MalikMasis)

