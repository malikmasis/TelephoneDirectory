
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /app
EXPOSE 80

COPY src/Microservices/Auth/TelephoneDirectory.Auth/*.csproj src/Microservices/Auth/TelephoneDirectory.Auth/
COPY src/Microservices/Auth/TelephoneDirectory.Auth.Entities/*.csproj src/Microservices/Auth/TelephoneDirectory.Auth.Entities/

RUN dotnet restore src/Microservices/Auth/TelephoneDirectory.Auth/*.csproj
COPY . .
RUN dotnet publish src/Microservices/Auth/TelephoneDirectory.Auth/*.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "TelephoneDirectory.Auth.dll"]