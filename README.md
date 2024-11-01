# ClientesApp

**ClientesApp** é uma API desenvolvida em .NET 8 para gerenciamento de clientes. O projeto segue uma arquitetura em camadas, com as seguintes divisões:

- **API**: Camada responsável por expor os endpoints da API.
- **Domain**: Camada de domínio, onde estão localizadas as regras de negócio.
- **Infra.Data**: Camada de infraestrutura e acesso a dados, incluindo a configuração do Entity Framework.

## Tecnologias Utilizadas

O projeto utiliza as seguintes tecnologias e bibliotecas:

- **.NET 8 API**: Framework principal para desenvolvimento da API RESTful. Saiba mais em [.NET 8](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8).
- **Swagger**: Utilizado para documentação interativa da API, facilitando o teste e a visualização dos endpoints. Saiba mais em [Swagger](https://swagger.io/).
- **CORS (Cross-Origin Resource Sharing)**: Configurado para permitir o compartilhamento de recursos entre diferentes origens, garantindo segurança e flexibilidade na comunicação entre o front-end e a API. Saiba mais em [CORS](https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS).
- **Entity Framework Core (Code First)**: Ferramenta ORM para mapeamento dos objetos da aplicação com o banco de dados, utilizando o método Code First. Saiba mais em [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/).

## Estrutura do Projeto

- **ClientesApp.API**: Camada de apresentação, onde estão configurados os endpoints e o Swagger.
- **ClientesApp.Domain**: Camada onde ficam as entidades e as regras de negócio.
- **ClientesApp.Infra.Data**: Camada responsável pelo acesso ao banco de dados e pela implementação do Entity Framework Core.

## Funcionalidades

- CRUD completo para gerenciamento de clientes.
- Documentação da API com Swagger.
- Configuração de CORS para comunicação segura entre front-end e API.
- Integração com o banco de dados usando Entity Framework Core em abordagem Code First.
