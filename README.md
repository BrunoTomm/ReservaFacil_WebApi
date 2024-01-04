# ReservaFacil - WebAPI

A API ReservaFacil permite que você faça reservas de maneira simplificada em eventos cadastrados.

## Configuração Inicial

1. Modifique a conexão do servidor no arquivo `appsettings.json` localizado em `ReservaFacil\ReservaFacil.WebApi\appsettings.json` para apontar para seu próprio servidor de banco de dados. No caso o projeto foi planejado para SQL Server. Caso for utilizar a imagem de container é necessario colocar no SERVIDOR da connection o IP do seu banco de dados, se não, basta inicializar com seus dados ou autenticação do windows. 

2. Para criar as tabelas no banco de dados, execute as migrações do Entity Framework. Abra o Terminal no Visual Studio (Ferramentas > Gerenciador de Pacotes do NuGet > Console do Gerenciador de Pacotes), selecione o projeto ReservaFacil.Infra e execute o comando `Update-Database` após configurar a conexão. Isso criará as tabelas no seu banco de dados.
 - Nesta migration, ja foi criado no banco de dados na tabela de USUARIO, um usário padrão, ficticio, somente para fins de autenticação:
 <br>
 Nome: Admin
 <br>
 Email: admin@exemplo.com
  <br>
 IdUsuario: BEC019F0-B73A-4A56-9A8C-F9246B9919E3
  <br>
 <br>
3. Na pasta raiz do projeto, você encontrará um arquivo chamado `Build.bat`. Execute este arquivo para iniciar o Docker e provisionar a imagem necessária. Após a conclusão deste processo, a API estará totalmente operacional (Caso queira utilizar pelo Docker > `http://localhost:51770/swagger/index.html`). Caso não, basta inicializar o projeto no Visual Studio.

## Uso da API

Após as configurações iniciais você precisará se autenticar para conseguir utilizar os endpoints: 

1. `/api/Login/Autenticar`: Aqui coloque o Nome (Admin) e Email (admin@exemplo.com) passados anteriormente.

Você obterá um retorno com o token, basta copiá-lo e colocar na parte superior do swagger em Authorize. Após isso, conseguirá utilizar todos os endpoints.

## EndPoints
GET /api/Eventos/ObterTodosEventos: Retorna todos os eventos disponíveis.
 <br>
GET /api/Eventos/ObterEventoPorId/{idEvento}: Retorna um evento específico com base no ID fornecido.
 <br>
POST /api/Eventos/CriarEvento: Cria um novo evento.
 <br>
PUT /api/Eventos/AtualizarEvento/{idEvento}: Atualiza um evento existente com base no ID fornecido.
 <br>
DELETE /api/Eventos/ExcluirEvento/{id}: Remove um evento com base no ID fornecido.
 <br>
POST /api/Login/Autenticar: Autentica um usuário para login.
 <br>
POST /api/Reservas/CriarReserva: Cria uma nova reserva.
 <br>
GET /api/Reservas/ObterReservasPorUsuario/{usuarioId}: Retorna reservas associadas a um usuário específico com base 
no ID do usuário.
 <br>
PUT /api/Reservas/AtualizarReserva/{idReserva}: Atualiza uma reserva existente com base no ID fornecido.
 <br>
PUT /api/Reservas/CancelarReserva/{idReserva}: Cancela uma reserva com base no ID fornecido.
 <br>

## Observações Importantes

- O sistema foi desenvolvido em C# (.Net 8) e utiliza o Entity Framework com o SQL Server como sistema de gerenciamento de banco de dados. 

- Arquitetura CQRS.

- Certifique-se de verificar a documentação da API para obter informações detalhadas sobre as solicitações e respostas esperadas.

## Contato

Se você tiver alguma dúvida ou encontrar problemas ao usar a API, entre em contato em [bruno.alexandre.tomm@gmail.com] para obter suporte.




