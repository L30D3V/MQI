FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app
EXPOSE 5000
COPY _publish .
COPY appsettings.json .
ENTRYPOINT ["dotnet", "Client.dll"]