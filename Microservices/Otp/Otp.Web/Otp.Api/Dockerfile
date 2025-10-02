FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Microservices/Otp/Otp.Web/Otp.Api/Otp.Api.csproj", "Microservices/Otp/Otp.Web/Otp.Api/"]
COPY ["Microservices/Otp/Otp.Core/Otp.Application/Otp.Application.csproj", "Microservices/Otp/Otp.Core/Otp.Application/"]
COPY ["Microservices/Otp/Otp.Core/Otp.Domain/Otp.Domain.csproj", "Microservices/Otp/Otp.Core/Otp.Domain/"]
COPY ["DDD/DDD.csproj", "DDD/"]
COPY ["Results/Results.csproj", "Results/"]
COPY ["Microservices/Otp/Otp.Infrastructure/Otp.Backgrounds/Otp.Backgrounds.csproj", "Microservices/Otp/Otp.Infrastructure/Otp.Backgrounds/"]
COPY ["Microservices/Otp/Otp.Infrastructure/Otp.Messaging/Otp.Messaging.csproj", "Microservices/Otp/Otp.Infrastructure/Otp.Messaging/"]
COPY ["MessageContracts/MessageContracts.csproj", "MessageContracts/"]
COPY ["Microservices/Otp/Otp.Infrastructure/Otp.Persistence/Otp.Persistence.csproj", "Microservices/Otp/Otp.Infrastructure/Otp.Persistence/"]
RUN dotnet restore "./Microservices/Otp/Otp.Web/Otp.Api/Otp.Api.csproj"
COPY . .
WORKDIR "/src/Microservices/Otp/Otp.Web/Otp.Api"
RUN dotnet build "./Otp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Otp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Otp.Api.dll"]