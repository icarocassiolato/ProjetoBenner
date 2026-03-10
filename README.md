# Microondas Benner
Exemplo de funcionamento de um microondas web, afim de demonstrar técnicas de programação

Tecnologias/Frameworks usados:
- .NET Core 10
- ASP .NET
- MS SQL Server
- Dapper
- Javascript

# Instalação
1. Abrir o script ScriptBanco.sql, contido na pasta raíz, e rodá-lo no SQL Server, para a criação das tabelas do banco de dados.

2.
- Obter a string de conexão do banco de dados criado
- Criptografar a string de conexão com o método Encrypt, contido em MicroondasBennerAPI/Utils/CryptoUtils
  (Não deixei esse método exposto no controller por questão de segurança)
- Colocar a string criptografada no campo CONNECTION_STRING_PADRAO do arquivo .env contido na pasta MicroondasBennerAPI
  (usei esse arquivo .env como padrão para colocar todas as variáveis de ambiente, pois esse é o padrão de segurança utilizado hoje em dia, para não deixar as informações expostas em um cenário real)

3. Abrir a solução (.sln) contida na pasta raíz. Essa Solução deverá carregar os seguintes projetos:
MicroondasBennerAPI
MicroondasBennerAPI.Tests
MicroondasBennerFrontend
MicroondasBennerFrontend.Tests

4. Garantir que a url contida em MicroondasBennerAPI/Properties/launchSettings.json/campo applicationUrl esteja apontando para o mesmo endereço contido no frontend
   MicroondasBennerFrontend/wwwroot/js/api.js na primeira linha (window.API_URL = "https://localhost:5001")

5. No Visual Studio > Clicar com botão direito na solução > Propriedades > Propriedades Comuns > Configurar projetos de inicialização
   Marcar MicroondasBennerFrontend e MicroondasBennerAPI com a ação "Iniciar"

6. Ao salvar e rodar com F5, os dois projetos deverão ser carregados, o frontend deve estar conectado ao backend, e a tela de login deverá aparecer.

7. Clicar no botão cadastrar usuário, e cadastrar um uasuário para poder fazer login com ele

# Observações [IMPORTANTE]
- Tanto no cadastro de usuários, quanto no cadastro de programas, fiz um CRUD simples, mostrando todos os registros.
  O ideal seria mostrar somente o cadastro e os programas do usuário autenticado, podrém, fiz dessa forma por se tratar de um teste,
  fiz dessa forma, para o avaliador ver os CRUDs em funcionamento

- Em um cenário real, eu criaria uma tela de CRUD base, para ser herdada por todos os CRUDs a serem criados, e assim, reaproveitar código.
  Também criaria um repositório base na api para manipulação do banco de dados, pelo mesmo motivo do CRUD (reutilização)
  
- Na tela principal, vai carregar somente os programas de aquecimento registrados pelo usuário logado

- Preferi não deixar os cadastros de programas que são fixos (peixe, boi, etc...) no banco, pois quis mostrar técnicas como herança e reflection.
  Fazendo isso, aumentei a complexidade propositalmente, por se tratar de um teste técnico.
  Eu poderia simplesmente criar um flag na tabela de programas, dizendo se permite ou não alterações, assim cadastraria tanto os programas fixos, quanto os personalizados

- O exercício não pediu, mas criei um healthcheck (caminho https://HostDaAPI/healthcheck)

- Criei um middleware de exceções para capturar todas, e quando for uma exceção tratada (ApplicationException) devolva para o front com o devido tratamento

# Técnicas/Padrões de projeto utilizados
- SOLID
- DDD
- Orientação a Objetos
- Herança
- Reflection
Autenticação com token JWT (geralmente uso refresh token para tornar mais seguro, mas por se tratar de um teste, já atende os requisitos)

>  This is a challenge by [Coodesh](https://coodesh.com/)
