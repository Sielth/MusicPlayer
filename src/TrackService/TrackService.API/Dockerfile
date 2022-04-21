#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TrackService.API/TrackService.API.csproj", "TrackService.API/"]
COPY ["TrackService.Application/TrackService.Application.csproj", "TrackService.Application/"]
COPY ["TrackService.Core/TrackService.Core.csproj", "TrackService.Core/"]
COPY ["TrackService.Shared/TrackService.Shared.csproj", "TrackService.Shared/"]
COPY ["TrackService.Persistence/TrackService.Persistence.csproj", "TrackService.Persistence/"]
COPY ["TrackService.Infrastructure/TrackService.Infrastructure.csproj", "TrackService.Infrastructure/"]
RUN dotnet restore "TrackService.API/TrackService.API.csproj"
COPY . .
WORKDIR "/src/TrackService.API"
RUN dotnet build "TrackService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrackService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrackService.API.dll"]