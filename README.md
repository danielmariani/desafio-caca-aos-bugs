![balta](https://baltaio.blob.core.windows.net/static/images/dark/balta-logo.svg)

## 🎖️ Desafio
**Caça aos Bugs 2024** é a sexta edição dos **Desafios .NET** realizados pelo [balta.io](https://balta.io). Durante esta jornada, fizemos parte da equipe M1nh0k onde resolvemos todos os bugs de uma aplicação e aplicamos testes de unidade no projeto.

## 📱 Projeto
Depuração e solução de bugs, pensamento crítico e analítico, segurança e qualidade de software aplicando testes de unidade.

## Participantes
### 🚀 Líder Técnico
[M1nh0k](https://github.com/danielmariani/desafio-caca-aos-bugs)
Marlitos

### 👻 Caçadores de Bugs
* [M1nh0k](https://github.com/danielmariani/desafio-caca-aos-bugs)
* Marlitos
* Daniel Lima
* Alex Marafon

## ⚙️ Tecnologias
* C# 12
* .NET 8
* ASP.NET
* Minimal APIs
* Blazor Web Assembly
* xUnit

## 🥋 Skills Desenvolvidas
* Comunicação
* Trabalho em Equipe
* Networking
* Muito conhecimento técnico

## 🧪 Como testar o projeto
### Bugs (Solution Dima.sln)
* Abra a solution Dima.sln na sua IDE preferida, ajuste o string de conexão no appsettings.json do projeto Dima.Api de acordo com as credenciais e servidor do seu banco Sql Server
* No projeto Dima.Api, execute o comando dotnet ef database update para ser criado o banco de dados via migrations do Enetity Framework.
* Acesse o bando de dados e insira os dados em massa do script: \Dima.Api\Data\Scripts\seed.sql
* Execute também a criação das 3 views via script no banco:
  \Dima.Api\Data\Views\vwGetExpensesByCategory.sql;
  \Dima.Api\Data\Views\vwGetIncomesAndExpenses.sql;
  \Dima.Api\Data\Views\vwGetIncomesByCategory.sql.
* Execute a aplicação a partir de sua IDE preferida.
  
### Teste (Solution Balta.sln)
* Execute a aplicação a partir de sua IDE preferida.
* Execute os testes automatizados.


# 💜 Participe
Quer participar dos próximos desafios? Junte-se a [maior comunidade .NET do Brasil 🇧🇷 💜](https://balta.io/discord)
