FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Rutracker.Server/Rutracker.Server.csproj", "src/Rutracker.Server/"]
COPY ["src/Rutracker.Core/Rutracker.Core.csproj", "src/Rutracker.Core/"]
COPY ["src/Rutracker.Shared/Rutracker.Shared.csproj", "src/Rutracker.Shared/"]
COPY ["src/Rutracker.Infrastructure/Rutracker.Infrastructure.csproj", "src/Rutracker.Infrastructure/"]
COPY ["src/Rutracker.Client/Rutracker.Client.csproj", "src/Rutracker.Client/"]

RUN dotnet restore "src/Rutracker.Server/Rutracker.Server.csproj"

COPY . .
WORKDIR "/src/src/"
RUN dotnet build "Rutracker.Server/Rutracker.Server.csproj" -c Release -o /app
#RUN dotnet build "Rutracker.Client/Rutracker.Client.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Rutracker.Server/Rutracker.Server.csproj" -c Release -o /app
RUN dotnet publish "Rutracker.Client/Rutracker.Client.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
#RUN mv -n wwwroot/* Rutracker.Client/dist
#RUN rm wwwroot/
ENTRYPOINT ["dotnet", "Rutracker.Server.dll"]