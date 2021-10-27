#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["SpotHero_Backend_Challenge.csproj", "."]
RUN dotnet restore "./SpotHero_Backend_Challenge.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SpotHero_Backend_Challenge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SpotHero_Backend_Challenge.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpotHero_Backend_Challenge.dll"]