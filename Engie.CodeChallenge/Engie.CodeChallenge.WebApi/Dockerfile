#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Engie.CodeChallenge.WebApi/Engie.CodeChallenge.WebApi.csproj", "Engie.CodeChallenge.WebApi/"]
RUN dotnet restore "./Engie.CodeChallenge.WebApi/./Engie.CodeChallenge.WebApi.csproj"
COPY . .
WORKDIR "/src/Engie.CodeChallenge.WebApi"
RUN dotnet build "./Engie.CodeChallenge.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Engie.CodeChallenge.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Engie.CodeChallenge.WebApi.dll"]