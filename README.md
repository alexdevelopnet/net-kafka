# Loja Virtual - Exemplo com .NET, Kafka, PostgreSQL e Docker

Este projeto é um exemplo didático de uma loja virtual, criado para aprender e demonstrar integração entre:

- **ASP.NET Core (C#)** - Backend (API REST)
- **PostgreSQL** - Banco de dados relacional
- **Apache Kafka** - Mensageria para eventos
- **Docker Compose** - Orquestração dos serviços
- (Em breve) **Angular** - Frontend

## Funcionalidades
- Cadastro de produtos (CRUD)
- Cadastro de pedidos (CRUD)
- Relacionamento N:N entre pedidos e produtos
- Integração com PostgreSQL via Entity Framework Core
- Documentação e testes via Swagger
- Pronto para integração com Kafka (publicação/consumo de eventos)

## Como rodar o projeto

### 1. Suba os serviços com Docker Compose
```sh
docker-compose up -d
```

### 2. Rode a API
```sh
dotnet run --project src/api/api.csproj
```
Acesse o Swagger em: [http://localhost:5022/swagger](http://localhost:5022/swagger)

### 3. Teste os endpoints
- Cadastre produtos
- Cadastre pedidos
- Liste e atualize status

## Estrutura do Projeto
```
net-kafka/
  ├─ docker-compose.yml
  └─ src/
      ├─ api/        # Projeto ASP.NET Core (API)
      └─ worker/     # (Em breve) Worker para consumir Kafka
```

## Tecnologias
- .NET 9
- PostgreSQL 15
- Apache Kafka 7.5 (Confluent)
- Docker Compose
- Swagger (Swashbuckle)

## Próximos Passos
- [ ] Publicar eventos no Kafka ao criar pedido
- [ ] Worker .NET para consumir eventos do Kafka
- [ ] Frontend Angular

---

Feito para estudo e aprendizado 🚀 