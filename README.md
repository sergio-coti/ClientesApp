# ClientesApp

ClientesApp é um projeto .NET 8 API utilizando Dapper para acesso a dados. Este projeto serve para gerenciar clientes e suas informações.

## Tecnologias Utilizadas

- .NET 8
- Dapper
- AutoMapper
- SQL Server

## Configuração do Ambiente

Siga os passos abaixo para configurar o ambiente de desenvolvimento para o ClientesApp.

### Pré-requisitos

- .NET 8 SDK: [Download .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server: [Download SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Passo a Passo

1. Clone o repositório do projeto:

    ```bash
    git clone https://github.com/seu-usuario/ClientesApp.git
    cd ClientesApp
    ```

2. Crie um banco de dados no SQL Server:

    - Abra o SQL Server Management Studio (SSMS) ou seu cliente SQL preferido.
    - Crie um novo banco de dados. Por exemplo:
      ```sql
      CREATE DATABASE ClientesDB;
      ```

3. Execute o script `Script.sql` para criar as tabelas necessárias:

    - No SSMS ou no seu cliente SQL, abra o arquivo `Script.sql` localizado na raiz do projeto.
    - Execute o script no contexto do banco de dados criado anteriormente. Exemplo:
      ```sql
      USE ClientesDB;
      GO
      -- Execute o conteúdo de Script.sql aqui
      ```

4. Configure a string de conexão na classe 'ClienteRepository'.

## Frontend

O frontend deste projeto foi desenvolvido em Angular. Você pode acessar o repositório do frontend [aqui](https://github.com/sergio-coti/ClientesWeb).

## Contribuições

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.

## Licença

Este projeto está licenciado sob os termos da licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.
