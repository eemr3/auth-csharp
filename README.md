# Autenticação e autorização

Projeto de estudo na aceleração C# da Trybe, visando apreender sobre autenticação e autorização usando a bibliotéca/package "Authentication" e "JwtBearer" do ASP Net Core.
Criei um cadastro de usuário e login, para um blog.

## Stack utilizada

**Back-end:** C# ASp Net Core 6, EntityFramework, SQL Server, docker-compose, Authentication e JwtBearer

## Pré-requisitos

Antes de começar, verifique se você atendeu aos seguintes requisitos:

- SDK do .NET Core 6 instalado
- Docker e docker-compose instalados

## Instalação

Você precisará instalar a CLI do Entity Framework Core:

```bash
  dotnet tool install --global dotnet-ef
```

## Variáveis de Ambiente

Para rodar esse projeto, você vai precisar adicionar as seguintes variáveis de ambiente no seu .env

`.env.example`
Deve ser renomeada para `.env`. Onde deve contém a variável de com a senha do banco.

Ps: Você deve adicionar na variável a senha do banco de dados.

`appsettings.Development.example.json`
Deve ser renomeado para `appsettings.Development.json`. Onde deve conter a configuração para acesso ao banco de dados e chave secreta para o JWT.

Ps: Você alterar o "DefaultConnection" passando a string de conexão, onde deverá alterar o nome do banco de dados e a senha, lembrando que a senha é que criou para o banco, que esta no .env: Ex: `"Server=127.0.0.1;Database=NomeDoBanco;User=sa;Password=senhaDoBanco;TrustServerCertificate=True;"`

## Rodando localmente

Clone o projeto

```bash
  git clone git@github.com:eemr3/auth-csharp.git
```

Entre no diretório do projeto

```bash
  cd auth-csharp
```

Crie o container do banco de dados SQL Server

```bash
  docker compose up -d --build
```

Instale as dependências

```bash
  dotnet restore
```

Crie as tabelas no banco de dados

```bash
  dotnet ef database update
```

Inicie o servidor

```bash
  dotnet run
```

## Documentação da API

Dentro do Asp Net Core já esta configurado um Swagger, para acessar:

```bash
https://localhost:7111/swagger
```

ou

```bash
http://localhost:5010/swagger
```

Ps.: Precisa configurar um certificado SSL para o acesso ao "https".

## Autores

- [Emerson Moreira](https://www.github.com/eemr3)
