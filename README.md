# Autenticação e autorização

Projeto de estudo na aceleração C# da Trybe, visando apreender sobre autenticação e autorização usando a bibliotéca/package "Authentication" e "JwtBearer" do ASP Net Core.
Criei um cadastro de usuário e login, para um blog.

## Stack utilizada

**Back-end:** C# ASp Net Core 6, EntityFramework, SQL Server, docker-compose, Authentication e JwtBearer

## Variáveis de Ambiente

Para rodar esse projeto, você vai precisar adicionar as seguintes variáveis de ambiente no seu .env

`.env.example`
Deve ser renomeada para `.env`. Onde deve contém a variável de com a senha do banco.

`appsettings.Development.example.json`
Deve ser renomeado para `appsettings.Development.json`. Onde deve conter a configuração para acesso ao banco de dados e chave secreta para o JWT.

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

Inicie o servidor

```bash
  dotnet run
```

## Documentação da API

Dentro do Asp Net Core já esta configurado um Swagger, para acessar:

```bash
https://localhost:7261/swagger
```

ou

```bash
http://localhost:5035/swagger
```

Ps.: Precisa configurar um certificado SSL para o acesso ao "https".

## Autores

- [Emerson Moreira](https://www.github.com/eemr3)
