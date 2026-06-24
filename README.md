# Sistema de Compra de Ingressos

Sistema fullstack para gerenciamento e venda de ingressos para eventos, com API REST em ASP.NET Core e interface web em React.

## Tecnologias

**Backend**
- **.NET 10** — ASP.NET Core Web API
- **Entity Framework Core 10** — ORM com SQL Server LocalDB
- **JWT Bearer** — Autenticação e autorização por roles (Admin/User)
- **BCrypt.Net** — Hash de senhas
- **Swagger / Swashbuckle** — Documentação interativa da API

**Frontend**
- **React 18** com TypeScript
- **Vite** — Build tool
- **React Router v6** — Roteamento client-side
- **Tailwind CSS** — Estilização
- **Axios** — Requisições HTTP

## Arquitetura

O backend segue **Domain-Driven Design (DDD)** com três Bounded Contexts independentes:

```
BoundedContext/
├── Auth/    — Cadastro, login e gerenciamento de usuários
├── Event/   — Teatros, salas, cadeiras e eventos
└── Sell/    — Venda e pagamento de ingressos
```

Cada contexto é dividido em camadas:

```
Controller → UseCase → Domain (Entities / ValueObjects) → Repository
```

## Pré-requisitos

- .NET 10 SDK
- SQL Server LocalDB
- Node.js 18+

## Como executar

### Backend

```bash
# 1. Clonar o repositório
git clone <url-do-repositorio>
cd Sistema-de-Compra-de-Ingressos

# 2. Criar o banco de dados
dotnet ef database update

# 3. Iniciar a API
dotnet run
```

A API estará disponível em:
- HTTP: `http://localhost:5071`
- HTTPS: `https://localhost:7121`
- Swagger: `https://localhost:7121/swagger`

### Frontend

```bash
cd frontend

# Instalar dependências
npm install

# Iniciar em modo desenvolvimento
npm run dev
```

## Autenticação

A API usa JWT Bearer. Para acessar endpoints protegidos:

1. Faça `POST /api/auth/login` com e-mail e senha
2. Copie o `accessToken` da resposta
3. No Swagger, clique em **Authorize** e cole o token (sem o prefixo `Bearer`)

### Usuário admin padrão

Criado automaticamente na primeira execução:

| Campo | Valor |
|---|---|
| E-mail | `admin@sistema.com` |
| Senha | `Admin@123` |

## Endpoints

### Auth

| Método | Rota | Acesso | Descrição |
|---|---|---|---|
| POST | `/api/auth/register` | Público | Cadastra usuário comum |
| POST | `/api/auth/login` | Público | Login — retorna JWT |
| POST | `/api/auth/register-admin` | Admin | Cadastra novo administrador |

### Theater

| Método | Rota | Acesso |
|---|---|---|
| GET | `/api/Theater/list` | Público |
| GET | `/api/Theater/get/{id}` | Público |
| POST | `/api/Theater/create` | Admin |
| PUT | `/api/Theater/update` | Admin |
| DELETE | `/api/Theater/delete/{id}` | Admin |

### Room

| Método | Rota | Acesso |
|---|---|---|
| GET | `/api/Room/list` | Público |
| GET | `/api/Room/get/{id}` | Público |
| POST | `/api/Room/create` | Admin |
| PUT | `/api/Room/update` | Admin |
| DELETE | `/api/Room/delete/{id}` | Admin |

### RoomEvent

| Método | Rota | Acesso |
|---|---|---|
| GET | `/api/RoomEvent/list` | Público |
| GET | `/api/RoomEvent/get/{id}` | Público |
| POST | `/api/RoomEvent/create` | Admin |
| PUT | `/api/RoomEvent/update` | Admin |
| DELETE | `/api/RoomEvent/delete/{id}` | Admin |

### Chair

| Método | Rota | Acesso |
|---|---|---|
| GET | `/api/Chair/list` | Público |
| GET | `/api/Chair/get/{id}` | Público |
| POST | `/api/Chair/create` | Admin |
| PUT | `/api/Chair/update` | Admin |
| DELETE | `/api/Chair/delete/{id}` | Admin |

### ChairsInEvent

| Método | Rota | Acesso |
|---|---|---|
| GET | `/api/ChairsInEvent/list` | Público |
| GET | `/api/ChairsInEvent/get/{id}` | Público |
| POST | `/api/ChairsInEvent/create` | Admin |
| PUT | `/api/ChairsInEvent/update` | Admin |
| DELETE | `/api/ChairsInEvent/delete/{id}` | Admin |

### Events

| Método | Rota | Acesso |
|---|---|---|
| GET | `/api/Events/list` | Público |
| GET | `/api/Events/get/{id}` | Público |
| POST | `/api/Events/create` | Admin |
| PUT | `/api/Events/update` | Admin |
| DELETE | `/api/Events/delete/{id}` | Admin |

### Tickets

| Método | Rota | Acesso | Descrição |
|---|---|---|---|
| GET | `/api/tickets` | Público | Lista todos os tickets |
| GET | `/api/tickets/{id}` | Público | Busca ticket por ID |
| POST | `/api/tickets` | Público | Cria ticket manualmente |
| POST | `/api/tickets/sell` | Público | Compra ingresso (aplica regras de negócio) |
| POST | `/api/tickets/{id}/payment/approve` | Público | Aprova pagamento |
| POST | `/api/tickets/{id}/payment/reject` | Público | Rejeita pagamento |
| DELETE | `/api/tickets/{id}` | Público | Remove ticket |

## Regras de negócio

- Não é possível comprar ingresso para um evento que já ocorreu
- Não é possível comprar ingresso para eventos com status **Finalizado** ou **Cancelado**
- Uma cadeira **ocupada** não pode ser reservada novamente
- Ao comprar, o ingresso fica com status **Pendente** até o pagamento ser processado
- O pagamento pode ser **aprovado** ou **rejeitado** separadamente

## Enums

| Enum | Valores |
|---|---|
| `EventStatus` | 1=Pendente, 2=Ocorrendo, 3=Finalizado, 4=Cancelado |
| `ChairStatus` | 1=Ocupado, 2=Vago |
| `PaymentStatus` | 1=Pendente, 2=Aprovado, 3=Rejeitado |
| `UserRole` | 1=Admin, 2=User |
