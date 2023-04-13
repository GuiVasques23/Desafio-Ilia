## Desafio Ilia
Descrição
API para controle de pontos de funcionários.
---
## Pré-requisitos
.NET 6 SDK ou superior<br>
SQL Server<br>
---
## Configurações do banco de dados

Abra o arquivo appsettings.json.<br>
Configure a ConnectionString do banco de dados:<br>
json<br>
Copy code<br>
{<br>
  "ConnectionStrings": {<br>
    "DefaultConnection": "Server=<nome_do_servidor>;Database=<nome_do_banco_de_dados>;Trusted_Connection=True;MultipleActiveResultSets=true"<br>
  }<br>
}<br>
Certifique-se de que o banco de dados está criado e pronto para uso.<br>
---
## Como rodar a aplicação
Clone o repositório:<br>
bash <br>
Copy code<br>
git clone https://github.com/GuiVasques23/Desafio-Ilia.git<br>
Acesse o diretório do projeto:<br>
bash<br>
Copy code<br>
cd Desafio-Ilia/TimeSheet.Api<br>
Execute o comando dotnet run:<br>
arduino<br>
Copy code<br>
dotnet run<br>
A aplicação estará disponível na URL http://localhost:5000.<br>
---
## Como executar os testes

Acesse o diretório do projeto:<br>
bash<br>
Copy code<br>
cd Desafio-Ilia/TimeSheet.Tests<br>
Execute o comando dotnet test:<br>
bash<br>
Copy code<br>
dotnet test<br>
---
## Autor
Guilherme Vasques - guilherme.vasques23@gmail.com<br>