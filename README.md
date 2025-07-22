# Loja Virtual - Exemplo com .NET, Kafka, PostgreSQL e Docker

Este projeto é um exemplo didático de uma loja virtual, criado para aprender e demonstrar integração entre:

- **ASP.NET Core (C#)** - Backend (API REST)
- **PostgreSQL** - Banco de dados relacional
- **Apache Kafka** - Mensageria para eventos
- **Docker Compose** - Orquestração dos serviços
- (Em breve) **Angular** - Frontend

## Fluxo de Eventos com Kafka

### 1. **KafkaProducerService (Produtor de eventos Kafka na API)**
- Classe responsável por publicar mensagens no Kafka.
- Usa o endereço do broker (ex: `localhost:9092`) configurado no `appsettings.json`.
- Método `PublishAsync` serializa o objeto e publica no tópico `pedidos`.

### 2. **API .NET (Backend)**
- Expõe endpoints REST para criar produtos e pedidos.
- Ao criar um pedido, salva no banco e publica um evento no Kafka usando o `KafkaProducerService`.

### 3. **Worker .NET (Consumidor Kafka)**
- Serviço separado que se conecta ao Kafka e escuta o tópico `pedidos`.
- Exibe/processa as mensagens recebidas no console.

### Diagrama do fluxo

```mermaid
sequenceDiagram
    participant API as API .NET
    participant Kafka as Kafka (Docker)
    participant Worker as Worker .NET

    API->>Kafka: Publica mensagem (pedido criado) no tópico "pedidos"
    Worker-->>Kafka: Fica escutando o tópico "pedidos"
    Kafka-->>Worker: Entrega a mensagem publicada
    Worker->>Console: Exibe/processa a mensagem recebida
```

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

### 3. Rode o Worker para consumir mensagens do Kafka
```sh
dotnet run --project src/worker/worker.csproj
```

### 4. Teste os endpoints
- Cadastre produtos
- Cadastre pedidos (veja a mensagem aparecer no Worker)

## Estrutura do Projeto
```
net-kafka/
  ├─ docker-compose.yml
  └─ src/
      ├─ api/        # Projeto ASP.NET Core (API)
      └─ worker/     # Worker para consumir Kafka
```

## Tecnologias
- .NET 9
- PostgreSQL 15
- Apache Kafka 7.5 (Confluent)
- Docker Compose
- Swagger (Swashbuckle)

## Próximos Passos
- [ ] Frontend Angular elegante e funcional

# Frontend

This project was generated using [Angular CLI](https://github.com/angular/angular-cli) version 19.2.14.

## Development server

To start a local development server, run:

```bash
ng serve
```

Once the server is running, open your browser and navigate to `http://localhost:4200/`. The application will automatically reload whenever you modify any of the source files.

## Code scaffolding

Angular CLI includes powerful code scaffolding tools. To generate a new component, run:

```bash
ng generate component component-name
```

For a complete list of available schematics (such as `components`, `directives`, or `pipes`), run:

```bash
ng generate --help
```

## Building

To build the project run:

```bash
ng build
```

This will compile your project and store the build artifacts in the `dist/` directory. By default, the production build optimizes your application for performance and speed.

## Running unit tests

To execute unit tests with the [Karma](https://karma-runner.github.io) test runner, use the following command:

```bash
ng test
```

## Running end-to-end tests

For end-to-end (e2e) testing, run:

```bash
ng e2e
```

Angular CLI does not come with an end-to-end testing framework by default. You can choose one that suits your needs.

## Additional Resources

For more information on using the Angular CLI, including detailed command references, visit the [Angular CLI Overview and Command Reference](https://angular.dev/tools/cli) page.

