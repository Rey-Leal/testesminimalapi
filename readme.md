# API com Login e Autenticação JWT
![Badge em Desenvolvimento](https://img.shields.io/static/v1?label=STATUS&message=FINALIZADO&color=GREEN&style=for-the-badge)

## Introdução
API completa fornecendo serviço de login e geração de token JWT. Após o login efetuado é possível realizar novas requisições apenas utilizando o token gerado.

Persistência de dados utilizando SQL Server e o Entity Framework.

Interface Swagger para auxílio do uso da API.

Testes de integração da API utilizando xUnit, simulando através de uma Factory a hospedagem da API.

### Tecnologias Utilizadas:
* .NET
* API
* JWT
* Swagger
* Entity Framework
* xUnit

## Caminhos de Acesso aos Dados da API
Os endpoints para acessar a API estarão disponíveis em:

- **Swagger**: `https://localhost:7210/swagger`</br>

## Configuração e Execução
Para executar este projeto localmente, siga os passos abaixo:

1. **Clone o repositório**:
   ```bash
   git clone <URL-do-repositorio>
   ```

2. **Navegue até o diretório do projeto**:
   ```bash
   cd nome-do-projeto
   ```

3. **Instale as dependências**:
   Certifique-se de que você tenha o .NET SDK instalado em sua máquina. Instale as dependências com:
   ```bash
   dotnet restore
   ```

4. **Atualize as configurações da API**:
   Configure as definições necessárias no arquivo `appsettings.json`.

5. **Execute o projeto**:
   ```bash
   dotnet run
   ```