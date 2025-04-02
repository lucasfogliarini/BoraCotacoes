# Usa a imagem oficial do SDK do .NET 8 como base para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Define o diretório de trabalho dentro do container
WORKDIR /app

# Copia os arquivos do projeto para o diretório de trabalho
COPY . .

# Restaura as dependências e compila o projeto
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Define a imagem de runtime do Azure Functions para .NET 8 (isolated)
FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated8.0

# Define o diretório de trabalho padrão da Azure Function
WORKDIR /home/site/wwwroot

# Copia os arquivos da build para o diretório da função
COPY --from=build /app/publish .

# Expõe a porta padrão da Azure Function
EXPOSE 1002

# O ENTRYPOINT é definido automaticamente pela imagem do Azure Functions
