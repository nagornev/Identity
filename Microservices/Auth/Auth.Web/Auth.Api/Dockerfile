# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Microservices/Auth/Auth.Web/Auth.Api/Auth.Api.csproj", "Microservices/Auth/Auth.Web/Auth.Api/"]
COPY ["Microservices/Auth/Auth.Core/Auth.Application/Auth.Application.csproj", "Microservices/Auth/Auth.Core/Auth.Application/"]
COPY ["Microservices/Auth/Auth.Core/Auth.Domain/Auth.Domain.csproj", "Microservices/Auth/Auth.Core/Auth.Domain/"]
COPY ["DDD/DDD.csproj", "DDD/"]
COPY ["Results/Results.csproj", "Results/"]
COPY ["Microservices/Auth/Auth.Infrastructure/Auth.Backgrounds/Auth.Backgrounds.csproj", "Microservices/Auth/Auth.Infrastructure/Auth.Backgrounds/"]
COPY ["Microservices/Auth/Auth.Infrastructure/Auth.Messaging/Auth.Messaging.csproj", "Microservices/Auth/Auth.Infrastructure/Auth.Messaging/"]
COPY ["MessageContracts/MessageContracts.csproj", "MessageContracts/"]
COPY ["Microservices/Auth/Auth.Infrastructure/Auth.Persistence/Auth.Persistence.csproj", "Microservices/Auth/Auth.Infrastructure/Auth.Persistence/"]
COPY ["Microservices/Auth/Auth.Infrastructure/Auth.Security/Auth.Security.csproj", "Microservices/Auth/Auth.Infrastructure/Auth.Security/"]
RUN dotnet restore "./Microservices/Auth/Auth.Web/Auth.Api/Auth.Api.csproj"
COPY . .
WORKDIR "/src/Microservices/Auth/Auth.Web/Auth.Api"
RUN dotnet build "./Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.Api.dll"]