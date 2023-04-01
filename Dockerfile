FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

COPY . .

RUN dotnet restore

RUN dotnet build --configuration Debug --no-restore

RUN dotnet publish --configuration Release --no-restore --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "portfolio-backend.dll"]
