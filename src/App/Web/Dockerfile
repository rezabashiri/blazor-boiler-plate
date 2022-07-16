#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["src/App/Web/Web.csproj", "src/App/Web/"]
COPY ["src/Service.Share/Service.Share.csproj", "src/Service.Share/"]
COPY ["src/App/App.Share/App.Share.csproj", "src/App/App.Share/"]
RUN dotnet restore "src/App/Web/Web.csproj"
COPY . .
WORKDIR "/src/src/App/Web"
RUN dotnet build "Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]