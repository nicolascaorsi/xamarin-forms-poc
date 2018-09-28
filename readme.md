Este repositório contém o código inicial para uma prova de conceito, de uma arquiteutura, que visa solucionar de forma evetiva aplicações orientadas a fluxo de dados, utilizando programação reativa.

# Tech Stack
* [Xamarin Forms]
* [Prism]
* [Realm]

# Requisitos
* [macOS]
* [XCode]
* [Visual Studio for Mac]

# Executando a aplicação
1. Abrir a solução src/MyApp.sln.
2. Selecionar o projeto Company.MyApp.iOS como padrão
3. Selecionar o modo de Debug
4. Selecionar um simulador ou dispositivo físico (iPhone)
5. Executar o projeto (botão :arrow_forward:)

# Separação, arquitetura e conceitos.
## src/Company.Modules.Message
 Modulo do Prism, contendo toda a lógica de leitura de mensagens (análogo a emails
 
### src/Company.Modules.Message/Database

Contém os modelos das entidades que são persistidas na base de dados.

### src/Company.Modules.Message/UseCases
São os casos de uso propriamente dito, como a possibilidade de realizar download de mensagens, obtenção e listagem. Cada usecase é responsável por somente uma única tarefa. As funcionalidades de download estão implementadas através de mocks.

### src/Company.Modules.Message/UI
Contém o código relacionado a telas, como view models e arquivos de interface. Estão separados de acordo com cada funcionalidade.

## src/Company.MyApp
Projeto Xamarin Forms base, responsável por realizar a configuração da [DryIoC] e do módulo de mensagens.

## src/Company.MyApp.iOS
Aplicação Xamarin Forms iOS, responsável por executar o app.

## src/NarcForms.Behaviors
Projeto que contém os behaviors utilizados pelo projeto. Futuramente será movido para um pacote [NuGet].
   
   [NuGet]: <https://nuget.org>
   [Xcode]: <https://developer.apple.com/xcode/>
   [DryIoC]: <https://bitbucket.org/dadhi/dryioc>
   [Visual Studio for Mac]: <https://visualstudio.microsoft.com/pt-br/vs/mac/>
   [Prism]: <https://github.com/PrismLibrary/Prism>
   [Realm]: <https://realm.io/docs/dotnet/latest/>
   [Xamarin Forms]: <https://docs.microsoft.com/pt-br/xamarin/xamarin-forms/>
   [macOS]: <https://www.apple.com/br/macos/>


# ToDo
* Adicionar a camada de API
* Adicionar testes unitários
* Adicioanr testes de interface
* Separar a responsabilidade da Model, para que ela não seja responsável tanto pela persistencia, como pela interação com UI
* Adicionar suporte a outras plataformas (Android, MacOS, Windows)
* Criar um loader para quando não houverem dados