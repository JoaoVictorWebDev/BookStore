BookStore - Desafio Técnico

Este projeto é uma API para gerenciamento de livros, desenvolvida com ASP.NET Core 8 e PostgreSQL. O sistema utiliza Docker para facilitar a configuração do banco de dados e implementa AutoMapper para mapeamento de objetos.

📌 Configuração do Ambiente

Requisitos

Antes de iniciar a instalação e configuração do projeto, certifique-se de ter os seguintes requisitos atendidos:

Visual Studio 2022 (ou superior)

.NET 8 SDK

Docker e Docker Compose

PostgreSQL

Git

🚀 Clonando o Repositório

Abra um terminal e execute o seguinte comando:

mkdir BookStore && cd BookStore

git clone https://github.com/JoaoVictorWebDev/GrupoCNArchive.git

📂 Abrindo o Projeto

Abra o Visual Studio 2022.

Vá para Arquivo → Abrir → Projeto/Solução.

Selecione o diretório onde o projeto foi clonado.

📦 Instalando Dependências

No Gerenciador de Pacotes NuGet, instale os seguintes pacotes conforme a estrutura do projeto:

BookStore.API

Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection -Version 11.0.0
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 9.0.2

BookStore.Application

Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection -Version 11.0.0
Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 8.0.0

BookStore.Infrastructure

Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore -Version 8.0.0
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.0
Install-Package Npgsql.EntityFrameworkCore.PostgreSQL -Version 9.0.3
Install-Package System.Configuration.ConfigurationManager -Version 9.0.2
Install-Package X.PagedList.Mvc -Version 8.0.7

🛠️ Configuração do Banco de Dados

Configurando o Docker

O projeto utiliza Docker para rodar o banco de dados PostgreSQL. No arquivo docker-compose.yml, temos a seguinte configuração:

services:
  bookstore.api:
    image: ${DOCKER_REGISTRY-}bookstoreapi
    build:
      context: .
      dockerfile: Application.BookStore/Dockerfile
    ports:
      - "5000:5000"
      - "5001:5001"

  bookstore.database:
    image: postgres:latest
    container_name: bookstore.data
    environment:
      - POSTGRES_DB=bookstore
      - POSTGRES_USER=postgres
      - POSTGRES_PASSWORD=postgres
    volumes:
      - ./.containers/bookstore-db:/var/lib/postgresql/data
    ports:
      - "5432:5432"

Configurando a String de Conexão

Abra o arquivo appsettings.json e adicione:

"ConnectionStrings": {
  "WebApiDatabase": "UserID=postgres;Password=postgres;Host=localhost;Port=5432;Database=bookstore"
}

Registrando o Banco no Program.cs

No arquivo Program.cs, registre o contexto do banco de dados:

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase"),
        x => x.MigrationsAssembly("BookStore.API"))
    .EnableSensitiveDataLogging());

▶️ Executando o Projeto

No terminal, suba os containers com Docker:

docker-compose up -d

No Visual Studio 2022, pressione F5 para rodar a API.

Acesse http://localhost:5000/swagger para testar os endpoints.

⚠️ Observação

Certifique-se de que o Docker Desktop está instalado e rodando antes de iniciar a aplicação.

📞 Dúvidas ou Sugestões?

Entre em contato! 😊
