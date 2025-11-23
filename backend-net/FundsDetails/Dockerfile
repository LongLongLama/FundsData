# ===== STAGE 1: BUILD & PUBLISH =====
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia solo i file di progetto per sfruttare il caching dei layer
COPY *.slnx ./
COPY Funds.Api/*.csproj ./Funds.Api/

RUN dotnet restore

# Copia il resto del codice
COPY . .

# Pubblica in modalità Release
RUN dotnet publish Funds.Api/Funds.Api.csproj -c Release -o /app/publish /p:UseAppHost=false


# ===== STAGE 2: RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Copia i file pubblicati dallo stage build
COPY --from=build /app/publish .

# Porta esposta dal container (puoi cambiarla)
EXPOSE 8080

# Imposta l�URL di ascolto di Kestrel
ENV ASPNETCORE_URLS=http://+:8080


ENTRYPOINT ["dotnet", "Funds.Api.dll"]

# ENTRYPOINT [ "bash", "sleep", "infinity" ]
