FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SpiceDB.SCIM.Provider/SpiceDB.SCIM.Provider.csproj", "SpiceDB.SCIM.Provider/"]
RUN dotnet restore "SpiceDB.SCIM.Provider/SpiceDB.SCIM.Provider.csproj"
COPY . .
WORKDIR "/src/SpiceDB.SCIM.Provider"
RUN dotnet build "SpiceDB.SCIM.Provider.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SpiceDB.SCIM.Provider.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpiceDB.SCIM.Provider.dll"]
