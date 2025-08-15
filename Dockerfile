FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

WORKDIR /app

EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Docker
WORKDIR /src
COPY ["AuroraRates.Api/AuroraRates.Api.csproj", "AuroraRates.Api/"]
COPY ["AuroraRates.Application/AuroraRates.Application.csproj", "AuroraRates.Application/"]
COPY ["AuroraRates.Domain/AuroraRates.Domain.csproj", "AuroraRates.Domain/"]
COPY ["AuroraRates.DataAccess/AuroraRates.DataAccess.csproj", "AuroraRates.DataAccess/"]
COPY ["AuroraRates.Infrastructure/AuroraRates.Infrastructure.csproj", "AuroraRates.Infrastructure/"]
RUN dotnet restore "AuroraRates.Api/AuroraRates.Api.csproj"
COPY . .
WORKDIR "/src/AuroraRates.Api"
RUN dotnet build "AuroraRates.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Docker
RUN dotnet publish "AuroraRates.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuroraRates.Api.dll"]
