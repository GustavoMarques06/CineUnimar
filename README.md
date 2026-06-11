# Sistema de Compra de Ingressos

API REST para gerenciamento e venda de ingressos para eventos, desenvolvida com ASP.NET Core 10 seguindo os princípios de Domain-Driven Design (DDD).

## Tecnologias

- **.NET 10** — ASP.NET Core Web API
- **Entity Framework Core 10** — ORM com SQL Server (LocalDB)
- **JWT Bearer** — Autenticação e autorização por roles
- **Swashbuckle / Swagger UI** — Documentação e testes interativos

## Arquitetura

O projeto é organizado em dois **Bounded Contexts**:

```
BoundedContext/
├── Auth/          -> Autenticação, cadastro e login de usuários
├── Event/         -> Gestão de teatros, salas, cadeiras e eventos
└── Sell/          -> Venda e pagamento de ingressos
```

Cada contexto segue a separação em camadas:

```
Controller -> UseCase -> Domain (Entities / ValueObjects) -> Repository
```

## Pré-requisitos

- .NET 10 SDK
- SQL Server LocalDB

## Como executar

```bash
# 1. Clonar o repositório
git clone <url-do-repositorio>
cd Sistema-de-Compra-de-Ingressos

# 2. Aplicar as migrations (cria o banco de dados)
dotnet ef database update

# 3. Rodar a aplicação
dotnet run
```

A API estará disponível em `https://localhost:{porta}`.  
Acesse o Swagger em: `https://localhost:{porta}/swagger`

> O usuário administrador padrão é criado automaticamente na primeira execução.

## Autenticação

A API usa JWT Bearer. Para acessar endpoints protegidos:

1. Faça login em `POST /api/auth/login`
2. Copie o `accessToken` da resposta
3. No Swagger, clique em **Authorize** e cole o token (sem o prefixo `Bearer`)

### Usuário Admin padrão

| Campo | Valor |
|---|---|
| Email | `admin@sistema.com` |
| Senha | `Admin@123` |

## Endpoints

### Auth
| Método | Rota | Acesso | Descrição |
|---|---|---|---|
| POST | `/api/auth/register` | Público | Cadastra usuário comum |
| POST | `/api/auth/login` | Público | Login, retorna JWT |
| POST | `/api/auth/register-admin` | Admin | Cadastra novo admin |

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
| POST | `/api/tickets/sell` | Público | Compra ingresso (valida regras) |
| POST | `/api/tickets/{id}/payment/approve` | Público | Aprova pagamento |
| POST | `/api/tickets/{id}/payment/reject` | Público | Rejeita pagamento |
| DELETE | `/api/tickets/{id}` | Público | Remove ticket |

## Regras de Negócio

- Não é possível comprar ingresso para um **evento que já ocorreu**
- Não é possível comprar ingresso para eventos com status **Ended** ou **Cancelled**
- Uma cadeira **ocupada** não pode ser reservada novamente
- Ao comprar, o ingresso fica com status **Pending** até o pagamento ser processado
- O pagamento pode ser **aprovado** ou **rejeitado** separadamente

## Enums

| Enum | Valores |
|---|---|
| `EventStatus` | 1=Pendente, 2=Ocorrendo, 3=Finalizado, 4=Cancelado |
| `ChairStatus` | 1=Ocupado, 2=Vago |
| `PaymentStatus` | 1=Pendente, 2=Aprovado, 3=Rejeitado |
| `UserRole` | 1=Admin, 2=User |
