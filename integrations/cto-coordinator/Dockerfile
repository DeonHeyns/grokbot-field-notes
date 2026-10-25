FROM mcr.microsoft.com/dotnet/sdk:10.0.402 AS build
WORKDIR /source
COPY src/Coordinator.csproj src/packages.lock.json ./
RUN dotnet restore --locked-mode
COPY src/ ./
RUN dotnet publish --no-restore -c Release -o /out
FROM mcr.microsoft.com/dotnet/aspnet:10.0.13
WORKDIR /app
COPY --from=build /out ./
RUN mkdir /data && chown $APP_UID /data
USER $APP_UID
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Coordinator.dll"]
