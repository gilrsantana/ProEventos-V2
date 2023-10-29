# INFORMAÇÕES GERAIS DO PROJETO

- Projeto desenvolvido em ambiente Linux.
- Eventualmente, algumas definições e instalações devem se adequar ao ambiente de SO diferente.
- Este documento contém informações gerais de configuração e clone para o projeto

## 1 - Configuração e instalações iniciais iniciais

<br>

### 1.1 - Extensões VS Code

- C# For Visual Studio Code By Microsoft
- C# IDE Extensions for VSCode
- .NET Core Tools
- NuGet Gallery
- Auto-Using for C#

<br><hr><br>

### 1.2 - Arquivo global.json

- O projeto foi estruturado para trabalhar com dotnet 7.0
- Listar as versões de dotnet instaladas

> dotnet --list-sdks

- Definir o global.json adequado ao dotnet 7.0

> dotnet new globaljson --sdk-version 7.0.403

<br><hr><br>

### 1.3 - node, nvm, npm e angular

- O projeto utiliza node, npm e angular nas versões abaixo:

> Node: v18.16.0

> Npm: 9.5.1

> Angular: Versão 16

- Instalação do nvm conforme link

> https://letscodepare.com/blog/how-to-install-nvm-node-version-manager-on-linux

- Comandos nvm - Listar versões disponíveis no servidor remoto:

> nvm ls-remote

- Comandos nvm - Instalar última versão estável:

> nvm install stable

- Comandos nvm - Instalar versão específica:

> nvm install v18.16.0

- Comandos nvm - Listar versões do node instaladas na máquina:

> nvm list

- Comandos nvm - Definir a versão de node a ser utilizada:

> nvm use v18.16.0

- Instalação do node

> sudo apt install nodejs

- Instalação do npm

> sudo apt install npm

- Instalação do angular

> npm install -g @angular/cli

- Apresentar versão dos programas instalados

> ng version

<br><hr><br>

### 1.4 - Divisão do projeto

- O projeto tem as seções de Front e Back, cada uma com suas respectivas funções.
- Em Back/src/ foi criada a web API através do comando:

> dotnet new webapi -n ProEventos.API


<br><hr><br>

### 1.5 - Habilitação do https

- Habilitar https

> dotnet dev-certs https --trust

- Em Linux, em caso de falha na identificação do certificado https executar as instruções abaixo contidas em:

> https://docs.microsoft.com/pt-br/aspnet/core/security/enforcing-ssl?view=aspnetcore-6.0&tabs=visual-studio#ssl-linux

- Execução dos comandos abaixo para gerar certificado confiável:

> dotnet dev-certs https

> sudo -E dotnet dev-certs https -ep /usr/local/share/ca-certificates/aspnet/https.crt --format PEM

> cat <<EOF | sudo tee /usr/lib/firefox/distribution/policies.json
> {
> "policies": {
> "Certificates": {
> "Install": [
> "/usr/local/share/ca-certificates/aspnet/https.crt"
> ]
> }
> }
> }
> EOF

- No Firefox

* Configurar a confiança do certificado HTTPS usando o navegador Firefox

> Insira about:config no navegador FireFox.

> Selecione Aceitar o Risco e Continuar se você aceitar o risco.

> Selecione Mostrar Tudo

> Definir security.enterprise_roots.enabled = true

> Sair e reiniciar o Firefox

<br><hr><br>

## 2 - Entity Framework

<br>

### 2.1 - Acessórios

- Verificação das ferramentas .NET instaladas

> dotnet tool list --global

<br><hr><br>

### 2.2 - Instalação do EF

- Instalação da última versão do Entity Framework

> dotnet tool install --global dotnet-ef

- Instalação de uma versão específica do Entity Framework. Como meu projeto é em .NET 5 instalei a versão 5.0.13

> dotnet tool install --global dotnet-ef --version 5.0.13

- Utilitário via CLI do Entity Framework

> dotnet-ef

- Instalação via NuGet Gallery:
- Pressione CTRL SHIFT P
- Digite:

> \>NuGet: Open NuGet Gallery

- No campo de busca digite:

> EntityFrameworkCore

- Clique nele e selecione a versão desejada.

- A referência abaixo será incluída no arquivo .csproj

> \<PackageReference Include="Microsoft.EntityFrameworkCore" Version="7.0.12" />

- Instalação das ferramentas EntityFrameworkCore.Tools e EntityFrameworkCore.Design EntityFrameworkCore.Sqlite
  utilizando a mesma lógica.

- Após a instalação das ferramentas o csproj ficará conforme abaixo:

> \<ItemGroup>
> \<PackageReference Include="Microsoft.EntityFrameworkCore" Version="7.0.12" />
> \<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="7.0.12">
> \<PrivateAssets>all</PrivateAssets>
> \<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
> \</PackageReference>
> \<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="7.0.12" />
> \<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="7.0.12">
> \<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
> \<PrivateAssets>all</PrivateAssets>
> \ </PackageReference>
> \</ItemGroup>

<br><hr><br>

### 2.3 - Iniciando as Migrations

- Acessar o diretório do projeto (/home/gilmar/Projetos/ProEventos/Back/src/ProEventos.API)
- Digitar o comando para iniciar as Migrations com o nome Initial (pode ser nome qualquer) e definindo a saída para o
  diretório Data/Migrations

> dotnet ef migrations add Initial -o Data/Migrations

- Iniciando o database

> dotnet ef database update

- Para gerenciamento do banco de dados SqLite pode-se utilizar o DB Browser for SQLite

<br><hr><br>

## 3 - Angular

<br>

### 3.1 - Extensão VS Code

- instalar extensão Angular essentials for VS Code.

<br><hr><br>

### 3.2 - Instalações no Angular

- Iniciar o projeto Angular

> ng new ProEventos.App

- Would you like to add Angular routing? *Yes*

- Which stylesheet format would you like to use? *SCSS*

<br><hr><br>

### 3.3 - Comandos do projeto

- Todos os comandos precisam ser dados dentro do diretório do projeto no caso:

> /home/gilmar/Projetos/ProEventos/Front/ProEventos-App

- O projeto utiliza alguns comandos padrão que são definidos no arquivo package.json "scripts"

- Os comandos podem ser dados com npm ou ng. Por exemplo para a opção start (ambos iniciariam o projeto) ficaria assim:

> npm start ou ng serve

<br><hr><br>

### 3.4 - Geração de componentes e outros elementos

- Os componentes no angular podem ser gerados pela CLI

- Para gerar um componente pode-se utilizar o comando completo ou o comando resumido.

- Geração do componente eventos com o comando completo

> ng generate component eventos

- Geração do componete palestrantes com o comando resumido

> ng g c palestrantes

- A geração de elementos com o 'ng' é bem ampla, abaixo alguns exemplos de componetes gerados:

> ng generate class Constants

> ng generate pipe DateTimeFormat

> ng generate service evento

### 3.5 - Fontawesome, ngx-bootstrap e bootstrap

- Todos os comandos precisam ser dados dentro do diretório do projeto no caso:

> /home/gilmar/Projetos/ProEventos/Front/ProEventos-App

- Instalação do Fontawesome

> npm install --save @fortawesome/fontawesome-free

- Instalação do ngx-bootstrap

> npm install ngx-bootstrap --save

- Instalação do bootstrap
- Observação: a versão do bootstrap deve estar relacionada à versão suportada pelo ngx-bootstrap

> npm install bootstrap@5

- Adicionar as referências abaixo no arquivo styles.scss

> @import '../node_modules/bootstrap/dist/css/bootstrap.min.css';

> @import '../node_modules/ngx-bootstrap/datepicker/bs-datepicker.css';

> @import '../node_modules//@fortawesome/fontawesome-free/css/all.min.css';
<br>

## 5 - CLone do projeto

- Execute o comando abaixo para fazer o clone do projeto no diretório de preferência de seu computador

> https://github.com/gilrsantana/ProEventos.git

## 5.1 - Restaurando Back

- Acesse, no diretório recém criado, a pasta ProEventos/Back/src/ProEventos.API e nela digite o comando abaixo para
  fazer a restauração do projeto com suas respectivas dependências:

> dotnet restore

- Após isso, faça a compilação do projeto com o comando abaixo

> dotnet build

- Se tudo correu bem, será mostrada a mensagem "Compilação com êxito"

- Inicie o projeto com o comando abaixo

> dotnet watch run

## 5.2 - Restaurando Front

- Acesse, no diretório recém criado, a pasta ProEventos/Front/ProEventos.App e nela digite o comando abaixo para fazer a
  restauração do projeto com suas respectivas dependências:

> npm install

- Após isso, inicie o projeto com o comando abaixo

> npm start

- Se tudo correu bem, será aberta a janela do navegador com o projeto em execução.

<br>

## 6 - Estruturando o projeto Back

- Criando a solução

> dotnet sln ProEventos.sln add ProEventos.API

- Criando as ClassLib

> dotnet new classlib -n ProEventos.Persistence

> dotnet new classlib -n ProEventos.Domain

> dotnet new classlib -n ProEventos.Application

- Adicionando as ClassLib à Solução

> dotnet sln ProEventos.sln add ProEventos.Application

> dotnet sln ProEventos.sln add ProEventos.API

> dotnet sln ProEventos.sln add ProEventos.Domain

> dotnet sln ProEventos.sln add ProEventos.Persistence

- Adicionando referência às ClassLib

> dotnet add ProEventos.API/ProEventos.API.csproj reference ProEventos.Application

> dotnet add ProEventos.Application/ProEventos.Application.csproj reference ProEventos.Domain

> dotnet add ProEventos.Application/ProEventos.Application.csproj reference ProEventos.Persistence

> dotnet add ProEventos.Persistence/ProEventos.Persistence.csproj reference ProEventos.Domain 
