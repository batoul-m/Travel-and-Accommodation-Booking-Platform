FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/TravelBookingPlatform.Api/TravelBookingPlatform.Api.csproj", "src/TravelBookingPlatform.Api/"]
COPY ["src/TravelBookingPlatform.Application/TravelBookingPlatform.Application.csproj", "src/TravelBookingPlatform.Application/"]
COPY ["src/TravelBookingPlatform.Domain/TravelBookingPlatform.Domain.csproj", "src/TravelBookingPlatform.Domain/"]
COPY ["src/TravelBookingPlatform.Infrastructure/TravelBookingPlatform.Infrastructure.csproj", "src/TravelBookingPlatform.Infrastructure/"]
COPY ["src/TravelBookingPlatform.Shared/TravelBookingPlatform.Shared.csproj", "src/TravelBookingPlatform.Shared/"]
RUN dotnet restore "src/TravelBookingPlatform.Api/TravelBookingPlatform.Api.csproj"
COPY . .
WORKDIR "/src/src/TravelBookingPlatform.Api"
RUN dotnet build "TravelBookingPlatform.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TravelBookingPlatform.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TravelBookingPlatform.Api.dll"]
