FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /app
EXPOSE 80
COPY _publish .
ENTRYPOINT ["dotnet", "WebInterface.dll"]