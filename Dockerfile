#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BitCoinAnalyzer.API/BitCoinAnalyzer.API.csproj", "BitCoinAnalyzer.API/"]
COPY ["BitCoinAnalyzer.Data/BitCoinAnalyzer.Data.csproj", "BitCoinAnalyzer.Data/"]
COPY ["BitCoinAnalyzer.Core/BitCoinAnalyzer.Core.csproj", "BitCoinAnalyzer.Core/"]
COPY ["BitCoinAnalyzer.Entity/BitCoinAnalyzer.Entity.csproj", "BitCoinAnalyzer.Entity/"]
COPY ["BitCoinAnalyzer.Service/BitCoinAnalyzer.Service.csproj", "BitCoinAnalyzer.Service/"]
RUN dotnet restore "BitCoinAnalyzer.API/BitCoinAnalyzer.API.csproj"
COPY . .
WORKDIR "/src/BitCoinAnalyzer.API"
RUN dotnet build "BitCoinAnalyzer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BitCoinAnalyzer.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BitCoinAnalyzer.API.dll"]