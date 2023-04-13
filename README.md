## Desafio Ilia
Descrição


API para controle de pontos de funcionários.
---
## Pré-requisitos
.NET 6 SDK ou superior


SQL Server
---
## Configurações do banco de dados


Abra o arquivo appsettings.json.


Configure a ConnectionString do banco de dados:


json


Copy code


{


  "ConnectionStrings": {


    "DefaultConnection": "Server=<nome_do_servidor>;Database=<nome_do_banco_de_dados>;Trusted_Connection=True;MultipleActiveResultSets=true"


  }


}


Certifique-se de que o banco de dados está criado e pronto para uso.
---
## Como rodar a aplicação


Clone o repositório:


bash 


Copy code


git clone https://github.com/GuiVasques23/Desafio-Ilia.git


Acesse o diretório do projeto:


bash


Copy code


cd Desafio-Ilia/TimeSheet.Api


Execute o comando dotnet run:


arduino


Copy code


dotnet run


A aplicação estará disponível na URL http://localhost:5000.
---
## Como executar os testes

Acesse o diretório do projeto:


bash


Copy code


cd Desafio-Ilia/TimeSheet.Tests


Execute o comando dotnet test:


bash


Copy code


dotnet test
---
## Autor


Guilherme Vasques - guilherme.vasques23@gmail.com