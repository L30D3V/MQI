FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
COPY _publish .
RUN mkdir ./logs
ENTRYPOINT ["dotnet", "WebApi.dll"]