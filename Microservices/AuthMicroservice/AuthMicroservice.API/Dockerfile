#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 443
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Microservices/AuthMicroservice/AuthMicroservice.API/AuthMicroservice.API.csproj", "Microservices/AuthMicroservice/AuthMicroservice.API/"]
COPY ["Microservices/AuthMicroservice/AuthMicroservice.BusinessLogic/AuthMicroservice.BusinessLogic.csproj", "Microservices/AuthMicroservice/AuthMicroservice.BusinessLogic/"]
COPY ["Microservices/AuthMicroservice/AuthMicroservice.DataAccess/AuthMicroservice.DataAccess.csproj", "Microservices/AuthMicroservice/AuthMicroservice.DataAccess/"]
RUN dotnet restore "./Microservices/AuthMicroservice/AuthMicroservice.API/./AuthMicroservice.API.csproj"
COPY . .
WORKDIR "/src/Microservices/AuthMicroservice/AuthMicroservice.API"
RUN dotnet build "./AuthMicroservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AuthMicroservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthMicroservice.API.dll"]