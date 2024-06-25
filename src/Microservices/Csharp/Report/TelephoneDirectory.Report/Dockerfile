
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY src/Microservices/Csharp/Report/TelephoneDirectory.Report/*.csproj src/Microservices/Csharp/Report/TelephoneDirectory.Report/
COPY src/Microservices/Csharp/Report/TelephoneDirectory.Report.Entities/*.csproj src/Microservices/Csharp/Report/TelephoneDirectory.Report.Entities/
COPY src/Global/TelephoneDirectory.Contracts/*.csproj src/Global/TelephoneDirectory.Contracts/

RUN dotnet restore src/Microservices/Csharp/Report/TelephoneDirectory.Report/*.csproj
COPY . .
RUN dotnet publish src/Microservices/Csharp/Report/TelephoneDirectory.Report/*.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "TelephoneDirectory.Report.dll"]