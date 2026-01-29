# 🎬 Filmes API - Gestão de Cinemas e Sessões

![.NET Version](https://img.shields.io/badge/.NET-9.0-purple)
![Entity Framework](https://img.shields.io/badge/EF%20Core-9.0-blue)
![License](https://img.shields.io/badge/license-MIT-green)

Esta é uma API REST robusta desenvolvida em **ASP.NET Core** para gerenciar um catálogo de filmes, cinemas, endereços e sessões de exibição. O projeto utiliza uma arquitetura moderna com separação de responsabilidades através de **DTOs** e mapeamento com **AutoMapper**.

---

## 🚀 Funcionalidades

- ✅ **CRUD de Filmes**: Cadastro, listagem, atualização e remoção.
- ✅ **Gestão de Cinemas**: Cadastro de cinemas vinculados a endereços únicos.
- ✅ **Sessões de Cinema**: Relacionamento N:N entre Filmes e Cinemas.
- ✅ **Paginação**: Listagem de filmes com suporte a parâmetros de busca (skip/take).
- ✅ **Documentação Automática**: Swagger integrado para testes rápidos.

---

## 🏗️ Estrutura do Projeto

O projeto segue a estrutura padrão de Web APIs em .NET:

- **`Controllers/`**: Endpoints da API (Filme, Cinema, Endereco, Sessao).
- **`Models/`**: Entidades que representam as tabelas no banco de dados.
- **`Data/`**: Contexto do Entity Framework (`FilmeContext`) e Migrations.
- **`DTOs/`**: Objetos de transferência para garantir que a API não exponha dados sensíveis do banco.
- **`Profiles/`**: Configurações de mapeamento do AutoMapper.

---

## 🛠️ Tecnologias e Bibliotecas

- **[ASP.NET Core 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)**: Framework principal.
- **[Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)**: ORM para persistência de dados.
- **[AutoMapper](https://automapper.org/)**: Mapeamento entre Models e DTOs.
- **[Swagger/OpenAPI](https://swagger.io/)**: Documentação interativa.

---

## 🏁 Como Iniciar

### 1. Pré-requisitos
* .NET SDK 9.0
* Banco de Dados (SQL Server ou MySQL - verifique sua string de conexão)

### 2. Configuração
Clone o repositório e navegue até a pasta do projeto:
```bash
git clone [https://github.com/seu-usuario/FilmesApi.git](https://github.com/seu-usuario/FilmesApi.git)
cd FilmesApi
```
### 3. Arquitetura e Relações (Database Schema)

O projeto foca fortemente em relacionamentos entre entidades, utilizando o **Entity Framework Fluent API**:

- **Cinema ↔ Endereço**: Relacionamento **1:1** (Um cinema possui um único endereço).
- **Cinema ↔ Sessão**: Relacionamento **1:N** (Um cinema pode ter várias sessões).
- **Filme ↔ Sessão**: Relacionamento **1:N** (Um filme pode estar em várias sessões).
- **Cinema ↔ Filme**: Relacionamento **N:N** através da entidade intermédia **Sessão**.

### 4. Configurações Avançadas

### AutoMapper Profiles
A aplicação utiliza **Profiles** para desacoplar a camada de persistência da camada de apresentação. Isso permite que alteres o banco de dados sem quebrar o contrato da API.

Exemplo de mapeamento:
```csharp
CreateMap<CreateCinemaDto, Cinema>();
CreateMap<Cinema, ReadCinemaDto>()
    .ForMember(dto => dto.Endereco, opt => opt.MapFrom(src => src.Endereco));

