FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Microservices/Notification/Notification.Web/Notification.Api/Notification.Api.csproj", "Microservices/Notification/Notification.Web/Notification.Api/"]
COPY ["Microservices/Notification/Notification.Core/Notification.Application/Notification.Application.csproj", "Microservices/Notification/Notification.Core/Notification.Application/"]
COPY ["Microservices/Notification/Notification.Core/Notification.Domain/Notification.Domain.csproj", "Microservices/Notification/Notification.Core/Notification.Domain/"]
COPY ["DDD/DDD.csproj", "DDD/"]
COPY ["MessageContracts/MessageContracts.csproj", "MessageContracts/"]
COPY ["Results/Results.csproj", "Results/"]
COPY ["Microservices/Notification/Notification.Infrastructure/Notification.Backgrounds/Notification.Backgrounds.csproj", "Microservices/Notification/Notification.Infrastructure/Notification.Backgrounds/"]
COPY ["Microservices/Notification/Notification.Infrastructure/Notification.Messaging/Notification.Messaging.csproj", "Microservices/Notification/Notification.Infrastructure/Notification.Messaging/"]
COPY ["Microservices/Notification/Notification.Infrastructure/Notification.Persistence/Notification.Persistence.csproj", "Microservices/Notification/Notification.Infrastructure/Notification.Persistence/"]
RUN dotnet restore "./Microservices/Notification/Notification.Web/Notification.Api/Notification.Api.csproj"
COPY . .
WORKDIR "/src/Microservices/Notification/Notification.Web/Notification.Api"
RUN dotnet build "./Notification.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Notification.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notification.Api.dll"]