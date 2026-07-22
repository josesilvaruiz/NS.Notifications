FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore "5-Workers/NS.Notifications.EmailWorker/NS.Notifications.EmailWorker.csproj"
RUN dotnet publish "5-Workers/NS.Notifications.EmailWorker/NS.Notifications.EmailWorker.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/runtime:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "NS.Notifications.EmailWorker.dll"]
