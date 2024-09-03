# Minimal API
![Badge em Desenvolvimento](https://img.shields.io/static/v1?label=STATUS&message=FINALIZADO&color=GREEN&style=for-the-badge)

## Introdução
API Minimal tem por intuíto ser um projeto API com os recursos mínimos possíveis para execução do mesmo. As principais tecnologias utilizadas são:

* C#
* API
* Swagger

## Caminhos de Acesso aos Dados da API
Abaixo estão os endpoints para acessar os dados da API quando a mesma estiver em execução:

- **?**: `http://localhost:7210/api/?`
- **?: `http://localhost:7210/api/?`

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