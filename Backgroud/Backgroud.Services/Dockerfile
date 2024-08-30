FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

ENV SMTP_HOST=smtp.example.com
ENV SMTP_PORT=587
ENV SMTP_USERNAME=username
ENV SMTP_PASSWORD=password

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Backgroud.Services/Backgroud.Services.csproj", "Backgroud.Services/"]
RUN dotnet restore "Backgroud.Services/Backgroud.Services.csproj"
COPY . .
WORKDIR "/src/Backgroud.Services"
RUN dotnet build "Backgroud.Services.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Backgroud.Services.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backgroud.Services.dll"]
