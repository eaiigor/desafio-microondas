# **Desafio Micro-ondas**

Este projeto é uma aplicação para gerenciamento de micro-ondas e programas de aquecimento, seguindo princípios de **Domain-Driven Design (DDD)** e **CQRS**. Ele permite a criação, consulta e manipulação de micro-ondas e seus programas de aquecimento, utilizando **Angular** no frontend e **.NET 6** no backend.

---

## **Índice**

- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Configuração do Ambiente](#configuração-do-ambiente)
- [Uso da Aplicação](#uso-da-aplicação)
- [Endpoints Disponíveis](#endpoints-disponíveis)
- [Detalhes Técnicos](#detalhes-técnicos)

---

## **Tecnologias Utilizadas**

- **Frontend**: Angular 15
- **Backend**: .NET 6
- **ORM**: Entity Framework Core com SQLite
- **Comunicação em Tempo Real**: SignalR
- **Injeção de Dependência**: Microsoft DI
- **Validação**: FluentValidation
- **Padrões de Projeto**: CQRS, Background Services, e Behavior Pipelines

---

## **Estrutura do Projeto**

O projeto está organizado em camadas seguindo o padrão **DDD**:

### **API**

- **Controllers**: Controladores para gerenciar rotas HTTP e delegar ações aos serviços e comandos.
- **DTOs**: Objetos de transferência de dados para comunicação entre camadas.

### **Domain**

- **Entities**: Entidades principais como `Microwave` e `HeatingProgram`.
- **Enums**: Definições de estados e categorias, como `EMicrowaveState` e `ELeadStatus`.
- **Interfaces**: Contratos para comandos e validações.
- **Models**: Modelos auxiliares como `MicrowaveTask`.

### **Infrastructure**

- **Repositories**: Implementações de acesso a dados para `Microwave` e `HeatingProgram`.
- **Data**: Configuração do `DbContext` usando EF Core.
- **Hubs**: Integração com SignalR para comunicação em tempo real.
- **Services**: Serviços auxiliares, como manipulação de filas e lógica de domínio.

### **Application**

- **Commands**: Operações de escrita como iniciar, pausar e parar o aquecimento.
- **Queries**: Operações de leitura como listar e buscar programas de aquecimento.
- **Handlers**: Manipuladores para execução de comandos e queries.
- **Background Services**: Processos assíncronos para tarefas recorrentes, como gerenciamento de filas.
- **Behaviors**: Middleware para validação e manipulação do pipeline de comandos.

---

## **Configuração do Ambiente**

1. **Clone o Repositório**:

2. **Configuração do Banco de Dados**:
   O projeto usa **SQLite**. Um arquivo de banco pré-populado está incluído, mas você pode recriar o banco usando as migrations:
   ```bash
   dotnet tool install --global dotnet-ef --version 6.0.35
   dotnet ef migrations add InitialMigration --project desafio-microondas.Infrastructure
   dotnet ef database update --project desafio-microondas.Infrastructure
   ```

3. **Acesse a pasta ClientApp e instale as dependências**:
   ```bash
   npm install
   ```

---

## **Uso da Aplicação**

1. Inicie a aplicação.
2. Utilize o frontend ou ferramentas como Postman para interagir com os endpoints.

---

## **Endpoints Disponíveis**

### **Microwave**

- **POST /api/microwave/start**: Inicia o aquecimento.
- **POST /api/microwave/pause**: Pausa o aquecimento.
- **POST /api/microwave/stop**: Para o aquecimento.

### **Heating Program**

- **GET /api/heatingprogram**: Lista todos os programas de aquecimento.
- **POST /api/heatingprogram**: Cria um novo programa.
- **DELETE /api/heatingprogram/{id}**: Exclui um programa pelo ID.

---

## **Detalhes Técnicos**

### **Validação**

Todas as ações passam por validações utilizando **FluentValidation**, garantindo a integridade dos dados antes da execução.

### **CQRS**

O padrão **CQRS** separa as operações de leitura (queries) das operações de escrita (commands), garantindo um design modular e escalável.

### **SignalR**

- Atualizações em tempo real são gerenciadas via hubs SignalR, permitindo notificações instantâneas.

### **Background Service**

- Um serviço de fundo processa periodicamente a fila de micro-ondas, atualizando o estado e notificando clientes.

---
