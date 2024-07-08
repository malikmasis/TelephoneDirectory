
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80

COPY src/Microservices/Csharp/Auth/TelephoneDirectory.Auth/*.csproj src/Microservices/Csharp/Auth/TelephoneDirectory.Auth/
COPY src/Microservices/Csharp/Auth/TelephoneDirectory.Auth.Entities/*.csproj src/Microservices/Csharp/Auth/TelephoneDirectory.Auth.Entities/
COPY src/Global/TelephoneDirectory.Common/*.csproj src/Global/TelephoneDirectory.Common/

RUN dotnet restore src/Microservices/Csharp/Auth/TelephoneDirectory.Auth/*.csproj
COPY . .
RUN dotnet publish src/Microservices/Csharp/Auth/TelephoneDirectory.Auth/*.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "TelephoneDirectory.Auth.dll"]