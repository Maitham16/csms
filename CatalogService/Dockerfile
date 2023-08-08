# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# asp.net development
ENV ASPNETCORE_ENVIRONMENT Development

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CatalogService.csproj", "./"]
RUN dotnet restore "./CatalogService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CatalogService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CatalogService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatalogService.dll"]