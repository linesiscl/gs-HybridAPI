# Global Solution - Software Development C#

## Integrantes

- Aline Fernandes Zeppelini – RM97966
- Julia Leite Galvão – RM550201

---

## Descrição do projeto

O projeto HybridAPI é uma aplicação desenvolvida em **ASP.NET Core Web API** com o objetivo de oferecer uma plataforma de gestão de produtividade híbrida. A solução permite que empresas e equipes monitorem métricas de desempenho e bem-estar dos colaboradores, como tempo online, pausas e metas atingidas, de forma centralizada e transparente. O sistema foi projetado com foco em boas práticas de arquitetura, versionamento e integração com banco de dados relacional, visando escalabilidade e fácil manutenção.

---

## Funcionalidades e implementação técnica

- Boas práticas de API RESTful: Uso correto dos verbos HTTP (GET, POST, PUT, DELETE) e retorno de status codes adequados como 200 OK, 201 Created, 400 Bad Request e 404 Not Found, garantindo clareza e padronização nas respostas da API.
  
- Versionamento da API: Implementado com o pacote Microsoft.AspNetCore.Mvc.Versioning, utilizando o padrão de rotas /api/v{version:apiVersion}/[controller]. A versão v1 contém as operações principais (Users, Sessions, Goals e Pauses), e uma v2 de demonstração foi criada para ilustrar a evolução e compatibilidade futura da API.
  
- Integração e persistência de dados: Desenvolvida com Entity Framework Core e banco de dados SQL Server, garantindo operações seguras e eficientes de CRUD. O contexto HybridApiDbContext gerencia as entidades User, WorkSession, Goal e Pause, assegurando integridade relacional e abstração do acesso ao banco.
  
- Tratamento centralizado de erros: Implementado via ErrorHandlingMiddleware, responsável por capturar exceções globais e retornar respostas padronizadas em formato JSON.

---

## Documentação e arquitetura

A documentação da API foi desenvolvida com o Swagger, permitindo explorar e testar todos os endpoints diretamente no navegador, com suporte a múltiplas versões (v1 e v2).

Além disso, o fluxo da aplicação foi representado graficamente no Draw.io, detalhando a interação entre as camadas do sistema — Controllers, DTOs, Models, Data, Middleware e Banco de Dados —, o que garante uma visão clara da arquitetura e da comunicação interna da aplicação.

<img width="741" height="811" alt="gs-hybridapi drawio" src="https://github.com/user-attachments/assets/f24167a5-893d-4a4f-b60e-a4dbe31b6f8e" />

---

## Instruções de Execução do Projeto

### Requisitos
- **.NET 8.0 SDK** ou superior  
- **Visual Studio 2022**   
- **SQL Server** instalado e em execução  

### Passos para executar

1. **Clonar o repositório**
   ```bash
   git clone https://github.com/linesiscl/gs-HybridAPI.git
   cd gs-hybrid
   ```

2. **Executar o projeto**
   ```bash
   dotnet run
   ```
   Ou pelo próprio Visual Studio

3. **Acessar a documentação Swagger**
   Se tiver rodado pelo comando no terminal:
   ```
   http://localhost:5088/swagger/index.html
   ```

   Se rodar pelo Visual Studio:
   ```
   https://localhost:7171/swagger/index.html
   ```
   > A partir do Swagger, você pode testar todos os endpoints disponíveis.

---

## Forma de Funcionamento da API

A **HybridAPI** é uma aplicação voltada para o controle de produtividade híbrida, gerenciando:
- Usuários (`Users`)
- Sessões de trabalho (`WorkSessions`)
- Pausas (`Pauses`)
- Metas (`Goals`)

O sistema segue o padrão RESTful, com rotas versionadas:  
```
/api/v1/[controller]
```
e também contém uma **versão v2 de demonstração** para o módulo de usuários:
```
/api/v2/users
```

---

## Endpoints e Exemplos de Requisição

### Usuários (`/api/v1/Users`)

A aplicação criará um ID de usuário que deverá ser usado para algumas operações de `User`, mas também de outros endpoints

#### Criar um novo usuário
**POST** `/api/v1/Users`
```json
{
  "fullName": "Julia Leite",
  "email": "julia@empresa.com",
  "role": "User"
}
```
**Resposta**
```json
{
  "id": "9bce3c12-2f6b-4a55-9fd0-2a35f15b88d7",
  "fullName": "Julia Leite",
  "email": "julia@empresa.com",
  "role": "User"
}
```

#### Listar todos os usuários
**GET** `/api/v1/Users`

#### Atualizar usuário
**PUT** `/api/v1/Users/{id}`
```json
{
  "fullName": "Julia Leite",
  "email": "julia.leite@empresa.com",
  "role": "Admin"
}
```

#### Excluir usuário
**DELETE** `/api/v1/Users/{id}`

---

### Sessões de Trabalho (`/api/v1/Sessions`)

#### Iniciar nova sessão de trabalho
**POST** `/api/v1/Sessions`
```json
{
  "userId": "9bce3c12-2f6b-4a55-9fd0-2a35f15b88d7",
  "startUtc": "2025-11-11T13:00:00Z",
  "isProductive": true
}
```

#### Encerrar sessão
**PUT** `/api/v1/Sessions/{id}/close`
```json
{
  "endUtc": "2025-11-11T17:30:00Z"
}
```

#### Listar todas as sessões
**GET** `/api/v1/Sessions`

---

### Pausas (`/api/v1/Pauses`)

#### Registrar pausa
**POST** `/api/v1/Pauses`
```json
{
  "workSessionId": "bc1d2e7b-5c89-4f2d-93a8-d1b4b45d7e88",
  "startUtc": "2025-11-11T15:00:00Z",
  "endUtc": "2025-11-11T15:15:00Z",
  "pauseType": "Coffee Break"
}
```

#### Listar pausas 
**GET** `/api/v1/Pauses`

---

### Metas (`/api/v1/Goals`)

#### Criar meta
**POST** `/api/v1/Goals`
```json
{
  "userId": "9bce3c12-2f6b-4a55-9fd0-2a35f15b88d7",
  "title": "Completar 5 relatórios semanais",
  "description": "Melhorar produtividade",
  "targetDateUtc": "2025-11-30T00:00:00Z",
  "isCompleted": false
}
```

#### Listar metas
**GET** `/api/v1/Goals`

#### Atualizar meta
**PUT** `/api/v1/Goals/{id}`
```json
{
  "userId": "9bce3c12-2f6b-4a55-9fd0-2a35f15b88d7",
  "title": "Entregar relatórios no prazo",
  "description": "Revisado após feedback",
  "targetDateUtc": "2025-12-31T00:00:00Z",
  "isCompleted": true
}
```

#### Excluir meta
**DELETE** `/api/v1/Goals/{id}`


---

## 🧾 Status Codes Utilizados

| Código | Significado | Situação de Uso |
|--------|--------------|----------------|
| **200 OK** | Requisição executada com sucesso | GET, PUT, DELETE |
| **201 Created** | Recurso criado com sucesso | POST |
| **400 Bad Request** | Erro de validação ou entrada inválida | Dados incorretos no corpo da requisição |
| **404 Not Found** | Recurso não encontrado | ID inexistente |
| **500 Internal Server Error** | Erro interno tratado pelo middleware | Exceções não previstas |

---

## Observações

- Todas as rotas seguem o padrão RESTful: `/api/v{version}/{controller}`  
- A API utiliza o **Entity Framework Core** com **SQL Server** para persistência.  
- O **Swagger** exibe e organiza automaticamente as versões `v1` e `v2`.  

---

## Link para o vídeo apresentação

https://youtu.be/Flwk_17aBcU


