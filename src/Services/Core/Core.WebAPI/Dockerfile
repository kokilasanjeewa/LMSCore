#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8004
#EXPOSE 8001

ENV ASPNETCORE_URLS=http://+:8004
ENV CONNECTIONSTRINGS__DEFAULTCONNECTION="Server=192.168.1.209;Database=CoreDB;User Id=sa;Password=Sqlserver2019;MultipleActiveResultSets=true;TrustServerCertificate=True;"
ENV CONNECTIONSTRINGS__DEFAULTTOCKENCONNECTION="Server=192.168.1.209;Database=CoreDB;User Id=sa;Password=Sqlserver2019;MultipleActiveResultSets=true;TrustServerCertificate=True;"
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_HTTP_PORTS=8004
#
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Core/Core.WebAPI/Core.WebAPI.csproj", "Services/Core/Core.WebAPI/"]
COPY ["Services/Core/Core.Application/Core.Application.csproj", "Services/Core/Core.Application/"]
COPY ["Services/Core/Core.Domain/Core.Domain.csproj", "Services/Core/Core.Domain/"]
COPY ["Services/Core/Core.Shared/Core.Shared.csproj", "Services/Core/Core.Shared/"]
COPY ["Services/Core/Core.Infrastructure/Core.Infrastructure.csproj", "Services/Core/Core.Infrastructure/"]
COPY ["Services/Core/Core.Persistence/Core.Persistence.csproj", "Services/Core/Core.Persistence/"]
COPY ["Services/CommonLibrary/JwtTokenAuthentication.csproj", "Services/CommonLibrary/"]
COPY ["Services/AggregatorService/ApplicationService.csproj", "Services/AggregatorService/"]

RUN dotnet restore "./Services/Core/Core.WebAPI/./Core.WebAPI.csproj"
COPY . .
WORKDIR "/src/Services/Core/Core.WebAPI"
RUN dotnet build "./Core.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Core.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
ENV ASPNETCORE_URLS=http://+:8004
ENV CONNECTIONSTRINGS__DEFAULTCONNECTION="Server=192.168.1.209;Database=CoreDB;User Id=sa;Password=Sqlserver2019;MultipleActiveResultSets=true;TrustServerCertificate=True;"
ENV CONNECTIONSTRINGS__DEFAULTTOCKENCONNECTION="Server=192.168.1.209;Database=CoreDB;User Id=sa;Password=Sqlserver2019;MultipleActiveResultSets=true;TrustServerCertificate=True;"
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_HTTP_PORTS=8004
#WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Core.WebAPI.dll"]

