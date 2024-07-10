FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Microservices/Csharp/Auth/TelephoneDirectory.Auth.OpenIdDict/TelephoneDirectory.Auth.OpenIdDict.csproj", "src/Microservices/Csharp/Auth/TelephoneDirectory.Auth.OpenIdDict/"]
RUN dotnet restore "src/Microservices/Csharp/Auth/TelephoneDirectory.Auth.OpenIdDict/TelephoneDirectory.Auth.OpenIdDict.csproj"
COPY . .
WORKDIR "/src/src/Microservices/Csharp/Auth/TelephoneDirectory.Auth.OpenIdDict"
RUN dotnet build "TelephoneDirectory.Auth.OpenIdDict.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TelephoneDirectory.Auth.OpenIdDict.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TelephoneDirectory.Auth.OpenIdDict.dll"]
