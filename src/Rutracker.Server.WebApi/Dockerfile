FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-disco AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-disco AS build
WORKDIR /src
COPY ["src/Rutracker.Server.DataAccessLayer/Rutracker.Server.DataAccessLayer.csproj", "src/Rutracker.Server.DataAccessLayer/"]
COPY ["src/Rutracker.Server.BusinessLayer/Rutracker.Server.BusinessLayer.csproj", "src/Rutracker.Server.BusinessLayer/"]
COPY ["src/Rutracker.Server.WebApi/Rutracker.Server.WebApi.csproj", "src/Rutracker.Server.WebApi/"]
COPY ["src/Rutracker.Client.Blazor/Rutracker.Client.BusinessLayer.csproj", "src/Rutracker.Client.BusinessLayer/"]
COPY ["src/Rutracker.Client.Blazor/Rutracker.Client.BlazorWasm.csproj", "src/Rutracker.Client.BlazorWasm/"]
COPY ["src/Rutracker.Shared.Models/Rutracker.Shared.Infrastructure.csproj", "src/Rutracker.Shared.Infrastructure/"]
COPY ["src/Rutracker.Shared.Models/Rutracker.Shared.Models.csproj", "src/Rutracker.Shared.Models/"]
RUN dotnet restore "src/Rutracker.Server.WebApi/Rutracker.Server.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Rutracker.Server.WebApi"
RUN dotnet build "Rutracker.Server.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Rutracker.Server.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Rutracker.Server.WebApi.dll"]