
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /app
EXPOSE 80

COPY src/Global/Gateway.WebApi/*.csproj src/Global/Gateway.WebApi/

RUN dotnet restore src/Global/Gateway.WebApi/*.csproj
COPY . .
RUN dotnet publish src/Global/Gateway.WebApi/*.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Gateway.WebApi.dll"]