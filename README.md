# AutoMapperXamarinFormsExemplo

Exemplo de como utilizar AutoMapper com XamarinForms

<img src="https://github.com/TBertuzzi/AutoMapperXamarinFormsExemplo/blob/master/Resources/automapper.png?raw=true" alt="Smiley face" >

Vamos ao Nuget Instalar o [AutoMapper](https://www.nuget.org/packages/Plugin.AppShortcuts/) em todos os projetos.

Em seguida existem algumas coisas q precisamos fazer. Antes de tudo para utilizar o AutoMapper precisamos configurar o arquivo do Linker em nossos projetos para que ele não remova algumas referencias importantes do AutoMapper :

Basta criar o arquivo Linker.xml nos projetos Android e iOS com o conteudo :

```xml

<linker>
  <assembly fullname="mscorlib">
    <type fullname="System.Convert" preserve="All" />
  </assembly>
</linker>

```

Agora no projeto iOS devemos editar o csproj e adicionar a seguinte referencia :

```c#
  <PackageReference Include="System.Reflection.Emit" Version="4.7.0">
      <ExcludeAssets>all</ExcludeAssets>
      <IncludeAssets>none</IncludeAssets>
  </PackageReference>

```

Pronto! Agora vamos criar uma classe para configurar o AutoMapper :


```c#

   public static class AppBootstrapper
    {
        public static IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Pokemon, PokemonViewModel>()
                .ForMember(vm => vm.ObterProximoPokemonCommand, opt => opt.Ignore());

                cfg.CreateMap<PokemonViewModel, Pokemon>();
            });

            return mapperConfiguration.CreateMapper();
        }
    }

```

Notem que na configuração eu especifico qual Model sera mapeada para ViewModel e vice e versa. Tambem estou ignorando o Command na viewModel, para que não exista problema no mapeamento.

Agora em Nossa ViewModel basta chamar o AutoMapper, para isso implementei um serviço que obtem um Pokemon e o AutoMapper ira mapear o resultado em nossa ViewModel :

```c#

 IMapper _mapper;
 
    public PokemonViewModel()
        {
            Titulo = "AutoMapper Exemplo";
            _ApiService = new PokemonService();

            _mapper = AppBootstrapper.CreateMapper();

            ObterProximoPokemonCommand = new Command(ExecuteObterProximoPokemonCommand);
        }
        
         private async Task CarregarPokemon(int id)
        {
            try
            {
                Ocupado = true;

                var pokemon = await _ApiService.GetPokemon(_cont);
                _mapper.Map(pokemon, this);


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {

                Ocupado = false;
            }
        }

```

Ao Executar temos o conteudo exibido na Tela.

Recomendo ler a documentação do [AutoMapper](https://automapper.org/) para ver tudo que é possivel fazer com ele.
E tambem o Mark ([Mark Allibone](https://mallibone.com/about)) escreveu um artigo bem 
legal sobre isso, e você pode conferir [Clicando Aqui](https://mallibone.com/post/xamarin-automapper/)

Caso fique a duvida este repositorio tem um exemplo da implementação completa.

Quer ver outros artigos sobre Xamarin ? [Clique aqui](https://github.com/TBertuzzi/XXamarin)

Espero ter ajudado!

Aquele abraço!

