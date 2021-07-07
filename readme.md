Criando uma aplicação do zero em netcore 5 com suporte a [OpenApi Specification](https://spec.openapis.org/oas/v3.1.0)
===

A solução proposta utiliza o [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) para gerar a
[OpenApi Spec](https://spec.openapis.org/oas/v3.1.0).

Passo 1 - Criando a aplicação
---

Antes de executar os comandos abaixo, crie um arquivo global.json na pasta do projeto definindo a versão do sdk (importante caso você tenha diversas versões do sdk instaladas mas sua app não usa a última versão).

```sh
dotnet new sln -n ApiUsers
dotnet new tool-manifest
dotnet tool install --version 6.1.2 Swashbuckle.AspNetCore.Cli
mkdir src
cd src
dotnet new webapi --name Users --language "C#" -f net5.0
cd Users
dotnet add package Swashbuckle.AspNetCore
dotnet add package Swashbuckle.AspNetCore.Annotations
cd ..
cd ..
dotnet sln add ./src/Users/Users.csproj
```
Passo 2 - Instrumentando a API
---

Basicamente são dois passos. Você irá configurar o SwashBuckle na Startup.cs e utilizar *annotations* nas Controllers para adicionar informações nos recursos.

### Classe Startup.cs

A configuração do Swashbuckle começa na classe Startup.cs.

* Linhas 31 a 46 - Registro do Swagger Generator e a especificação de um ou mais documentos.
* Linhas 32 a 44 - Especificação de um ou mais documentos com as informações básicas da API.
* Linha 45 - Define que vamos utilizar *annotations* para documentar os recursos.
* Linhas 60 e 63 - Insere na pipeline os middlewares que expoem o arquivo json da spec e a interface do Swagger no endpoint especificado.

### Controllers

Nas controllers usamos *Annotations* para documentar diversas informações relevantes como: "Status-codes de response, response body, request body, operation id, além de outras informações". Essas informações são muito importantes e irão aparecer na OpenApi Specification.

* Linhas 13 e 14 - Define o tipo de conteúdo que todos os recursos recebem e retornam.
* Linhas 34 a 41 - Definição de informações relevantes do recurso.
  * OperationId - Conforme definido na [OpenApi Spec](https://spec.openapis.org/oas/v3.1.0): *"Unique string used to identify the operation."*
  * Summary - Conforme definido na [OpenApi Spec](https://spec.openapis.org/oas/v3.1.0): *"A short summary of what the operation does.."*
  * Description - Conforme definido na [OpenApi Spec](https://spec.openapis.org/oas/v3.1.0): *"A verbose explanation of the operation behavior."*
  * Tags - Conforme definido na [OpenApi Spec](https://spec.openapis.org/oas/v3.1.0): *"A list of tags for API documentation control. Tags can be used for logical grouping of operations by resources or any other qualifier."*
  * Linha 41 - Definição dos possíveis status-codes e o formato do response body

Passo 3 - Exportando a OpenApi Spec para json
---

Para exportar a OpenApi Spec para um arquivo json automaticamente toda vez que compilar a aplicação, adicione a instrução abaixo no .csproj da sua WebApi.

```xml
<Target Name="PostBuild" AfterTargets="PostBuildEvent">
    <Exec Command="dotnet tool restore" />
    <Exec Command="dotnet swagger tofile --output openapispec.json $(OutputPath)\$(AssemblyName).dll v1 " />
</Target>
```

O projeto conta ainda com um arquivo global.json para indicar o framework que queremos utilizar na solution.

Como gerar a OpenApi Spec
---

Toda vez que a aplicação for compilada com sucesso, será gerada a spec automaticamente.

```sh
dotnet restore
dotnet build
```

Acessando a interface gráfica
---

Com a aplicação executando acesse http://localhost:<porta>/swagger/ui

## Referencias

* https://docs.microsoft.com/pt-br/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-5.0&tabs=visual-studio

* https://renatogroffe.medium.com/novidades-do-asp-net-5-suporte-a-swagger-open-api-habilitado-por-default-b3db7bbeb149