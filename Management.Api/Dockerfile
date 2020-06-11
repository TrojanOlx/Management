#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Management.Api/Management.Api.csproj", "Management.Api/"]
RUN dotnet restore "Management.Api/Management.Api.csproj"
COPY . .
WORKDIR "/src/Management.Api"
RUN dotnet build "Management.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Management.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Management.Api.dll"]