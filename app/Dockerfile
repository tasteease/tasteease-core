#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/presentation/Fiap.TasteEase.Api/Fiap.TasteEase.Api.csproj", "src/presentation/Fiap.TasteEase.Api/"]
COPY ["src/adapters/Fiap.TasteEase.Infra/Fiap.TasteEase.Infra.csproj", "src/adapters/Fiap.TasteEase.Infra/"]
COPY ["src/adapters/Fiap.TasteEase.Webhost/Fiap.TasteEase.Webhost.csproj", "src/adapters/Fiap.TasteEase.Webhost/"]
COPY ["src/core/Fiap.TasteEase.Application/Fiap.TasteEase.Application.csproj", "src/core/Fiap.TasteEase.Application/"]
COPY ["src/core/Fiap.TasteEase.Domain/Fiap.TasteEase.Domain.csproj", "src/core/Fiap.TasteEase.Domain/"]

RUN dotnet restore "src/adapters/Fiap.TasteEase.Webhost/Fiap.TasteEase.Webhost.csproj"
COPY . .
WORKDIR "/src/src/adapters/Fiap.TasteEase.Webhost"
RUN dotnet build "Fiap.TasteEase.Webhost.csproj" -c Release --no-restore -o /app/build

FROM build AS publish
RUN dotnet publish "Fiap.TasteEase.Webhost.csproj" -c Release --no-restore -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fiap.TasteEase.Webhost.dll"]