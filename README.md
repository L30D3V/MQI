# Projeto
### Desenvolvimento de sistema .NET Core com MongoDB + ElasticSearch running on Docker

#### Executar o projeto
Passo 1: Publicar o projeto na pasta _publish
- Abra o terminal, navegue até a pasta src/ e execute o comando:
```
dotnet publish DockerDotNetCore.sln --output _publish
```

Passo 2: Executar o arquivo Docker compose.
- Navegue até a pasta src/_docker e execute os comandos:
```
docker-compose build
docker-compose up
```
