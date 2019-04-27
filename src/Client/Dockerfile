FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

# Copiar csproj e restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Build da aplicacao
COPY . ./
RUN dotnet publish -c Release -o out

# Build da imagem
EXPOSE 5000
COPY _publish .
COPY appsettings.json .
ENTRYPOINT ["dotnet", "Client.dll"]