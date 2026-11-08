FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Travel-and-Accommodation-Booking-Platform-BackEnd/Travel-and-Accommodation-Booking-Platform-BackEnd.csproj", "Travel-and-Accommodation-Booking-Platform-BackEnd/"]
COPY ["Travel-and-Accommodation-Booking-Platform-BackEnd.Infrastructure/Travel-and-Accommodation-Booking-Platform-BackEnd.Infrastructure.csproj", "Travel-and-Accommodation-Booking-Platform-BackEnd.Infrastructure/"]
COPY ["Travel-and-Accommodation-Booking-Platform-BackEnd.Application/Travel-and-Accommodation-Booking-Platform-BackEnd.Application.csproj", "Travel-and-Accommodation-Booking-Platform-BackEnd.Application/"]
COPY ["Travel-and-Accommodation-Booking-Platform-BackEnd.Domain/Travel-and-Accommodation-Booking-Platform-BackEnd.Domain.csproj", "Travel-and-Accommodation-Booking-Platform-BackEnd.Domain/"]
RUN dotnet restore "Travel-and-Accommodation-Booking-Platform-BackEnd/Travel-and-Accommodation-Booking-Platform-BackEnd.csproj"
COPY . .
WORKDIR "/src/Travel-and-Accommodation-Booking-Platform-BackEnd"
RUN dotnet build "./Travel-and-Accommodation-Booking-Platform-BackEnd.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Travel-and-Accommodation-Booking-Platform-BackEnd.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Travel-and-Accommodation-Booking-Platform-BackEnd.dll"]
