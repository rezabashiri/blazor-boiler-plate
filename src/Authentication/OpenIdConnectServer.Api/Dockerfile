#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["src/Authentication/OpenIdConnectServer.Api/OpenIdConnectServer.Api.csproj", "src/Authentication/OpenIdConnectServer.Api/"]
COPY ["src/Authentication/Identity/Identity.csproj", "src/Authentication/Identity/"]
RUN dotnet restore "src/Authentication/OpenIdConnectServer.Api/OpenIdConnectServer.Api.csproj"
COPY . .
WORKDIR "/src/src/Authentication/OpenIdConnectServer.Api"
RUN dotnet build "OpenIdConnectServer.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OpenIdConnectServer.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenIdConnectServer.Api.dll"]