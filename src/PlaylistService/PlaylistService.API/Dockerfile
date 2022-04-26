#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PlaylistService.API/PlaylistService.API.csproj", "PlaylistService.API/"]
COPY ["PlaylistService.Application/PlaylistService.Application.csproj", "PlaylistService.Application/"]
COPY ["PlaylistService.Core/PlaylistService.Core.csproj", "PlaylistService.Core/"]
COPY ["PlaylistService.Shared/PlaylistService.Shared.csproj", "PlaylistService.Shared/"]
COPY ["PlaylistService.Persistence/PlaylistService.Persistence.csproj", "PlaylistService.Persistence/"]
RUN dotnet restore "PlaylistService.API/PlaylistService.API.csproj"
COPY . .
WORKDIR "/src/PlaylistService.API"
RUN dotnet build "PlaylistService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlaylistService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlaylistService.API.dll"]