# Usa a imagem oficial do SDK do .NET 8 como base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Define o diretório de trabalho dentro do container
WORKDIR /app
# Copia os arquivos do projeto para o diretório de trabalho
COPY . .

# Restaura as dependências e compila o projeto
RUN dotnet restore
RUN dotnet build -c Release -o /app/build

# Define a imagem de publicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Define o diretório de trabalho dentro do container
WORKDIR /app

# Copia os arquivos de construção do estágio de compilação
COPY --from=build /app/build .

# Exponha a porta que sua aplicação será executada
EXPOSE 1000
EXPOSE 1001

# Inicia a aplicação
ENTRYPOINT ["dotnet", "BoraCotacoes.WebApi.dll"]
