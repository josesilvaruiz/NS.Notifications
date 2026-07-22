FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore "1-Api/NS.Notifications.Api/NS.Notifications.Api.csproj"
RUN dotnet publish "1-Api/NS.Notifications.Api/NS.Notifications.Api.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "NS.Notifications.Api.dll"]
