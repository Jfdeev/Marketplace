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
````

2. Configure a connection string no `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=marketplace;Username=postgres;Password=SUA_SENHA;SSL Mode=Disable"
}
```

3. Rode as migrations para criar o banco:

```bash
dotnet ef database update --project src/Marketplace.Infrastructure --startup-project src/Marketplace.API
```

4. Execute a API:

```bash
dotnet run --project src/Marketplace.API
```

5. Acesse o Swagger para testar os endpoints:

```
https://localhost:7000
```

---

## Por que essa arquitetura?

* Separação clara de responsabilidades, facilitando manutenção
* Altamente testável graças a interfaces e dependency injection
* Flexível: posso trocar banco ou framework sem afetar a camada de domínio
* Escalável: consigo adicionar novas features sem quebrar código existente
* Princípios **SOLID** aplicados em todas as camadas

---

## Próximos passos que posso implementar

* Autenticação com **JWT**
* Integração com gateways de pagamento reais
* Notificações por email ou push
* Relatórios e dashboards de vendas

---

Feito com ❤️ por Jfdeev

