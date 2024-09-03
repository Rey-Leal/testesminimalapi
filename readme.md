# Minimal API Login
![Badge em Desenvolvimento](https://img.shields.io/static/v1?label=STATUS&message=FINALIZADO&color=GREEN&style=for-the-badge)

## Introdução
API Minimal Fornecendo serviço de login e geração de token JWT caso usuário seja validado. 

### Tecnologias Utilizadas:
* .NET
* Minimal API
* JWT
* Swagger

## O que é Minimal API?
A Minimal API foca em simplicidade e performance, evitando a necessidade de overhead de controladores e ações como em uma aplicação ASP.NET Core MVC completa.
O código é mais direto e fácil de entender, o que facilita a manutenção.

## Caminhos de Acesso aos Dados da API
Abaixo estão os endpoints para acessar os dados da API quando a mesma estiver em execução:

- **Login**: `https://localhost:7210/login`</br>
POST login raw/JSON {"email": "admin@teste.com", "senha": "123"}
RETURN Token JWT

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