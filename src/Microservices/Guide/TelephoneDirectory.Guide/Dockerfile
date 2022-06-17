
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
EXPOSE 80

COPY src/Microservices/Guide/TelephoneDirectory.Guide/*.csproj src/Microservices/Guide/TelephoneDirectory.Guide/
COPY src/Microservices/Guide/TelephoneDirectory.Guide.Entities/*.csproj src/Microservices/Guide/TelephoneDirectory.Guide.Entities/
COPY src/Global/TelephoneDirectory.Contracts/*.csproj src/Global/TelephoneDirectory.Contracts/

RUN dotnet restore src/Microservices/Guide/TelephoneDirectory.Guide/*.csproj
COPY . .
RUN dotnet publish src/Microservices/Guide/TelephoneDirectory.Guide/*.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "TelephoneDirectory.Guide.dll"]