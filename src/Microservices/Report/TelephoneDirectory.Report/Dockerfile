
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY src/Microservices/Report/TelephoneDirectory.Report/*.csproj src/Microservices/Report/TelephoneDirectory.Report/
COPY src/Microservices/Report/TelephoneDirectory.Report.Entities/*.csproj src/Microservices/Report/TelephoneDirectory.Report.Entities/
COPY src/Global/TelephoneDirectory.Contracts/*.csproj src/Global/TelephoneDirectory.Contracts/

RUN dotnet restore src/Microservices/Report/TelephoneDirectory.Report/*.csproj
COPY . .
RUN dotnet publish src/Microservices/Report/TelephoneDirectory.Report/*.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "TelephoneDirectory.Report.dll"]