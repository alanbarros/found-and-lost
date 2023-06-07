#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS debug

ENV ASPNETCORE_URLS="http://*:5000"

WORKDIR /src/
COPY . .
RUN dotnet restore "found-and-lost/found-and-lost.csproj"

#Change to project directory
WORKDIR /src/found-and-lost
ENTRYPOINT ["dotnet", "run"]