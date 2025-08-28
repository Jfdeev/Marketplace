# Marketplace .NET 8 - Arquitetura Hexagonal

🎉 Acabei de criar um marketplace completo usando **.NET 8** e **PostgreSQL**, seguindo uma **arquitetura hexagonal**.

---

## Estrutura do Projeto

Organizei o projeto em camadas bem definidas:

- **Domain**: Entities, Value Objects, Repository Interfaces e Domain Exceptions  
- **Application**: Use Cases usando **MediatR**, DTOs e validação com **FluentValidation**  
- **Infrastructure**: Implementações de repositório com **Entity Framework Core** e PostgreSQL  
- **API**: Controllers REST, middleware de tratamento de erros, Swagger e injeção de dependências  

---

## Funcionalidades

O marketplace já conta com funcionalidades principais:

- CRUD de **Produtos** com controle de estoque e busca  
- Cadastro de **Usuários** com validação de email  
- **Carrinho**: adicionar/remover produtos e cálculo automático  
- **Pedidos**: criação com transação e redução de estoque automática  
- Estrutura de **Pagamento simulado**, pronta para integrar uma solução real  

---

## Stack Tecnológica

- **.NET 8** com ASP.NET Core Web API  
- **PostgreSQL**  
- **Entity Framework Core 8**  
- **MediatR** (CQRS)  
- **FluentValidation**  
- **Swagger/OpenAPI**  

---

## Como Rodar o Projeto

1. Clone o projeto:  

```bash
git clone https://github.com/Jfdeev/Marketplace.git
cd Marketplace
