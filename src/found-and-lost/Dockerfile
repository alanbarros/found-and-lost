#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

ENV ASPNETCORE_URLS="http://*:5000"

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["found-and-lost/found-and-lost.csproj", "found-and-lost/"]
RUN dotnet restore "found-and-lost/found-and-lost.csproj"
COPY . .
WORKDIR "/src/found-and-lost"
RUN dotnet build "found-and-lost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "found-and-lost.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

# RUN apt update && \
#     apt install unzip && \
#     apt install curl -y && \
#     curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /vsdbg

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "found-and-lost.dll"]