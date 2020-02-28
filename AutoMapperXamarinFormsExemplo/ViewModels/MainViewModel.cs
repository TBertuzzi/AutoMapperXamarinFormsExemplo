using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapperXamarinFormsExemplo.Models;
using AutoMapperXamarinFormsExemplo.Services;

namespace AutoMapperXamarinFormsExemplo.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Pokemon> Pokemons { get; }
        PokemonService _ApiService;


        public MainViewModel()
        {
            Titulo = "AutoMapper Exemplo";
            _ApiService = new PokemonService();
            Pokemons = new ObservableCollection<Pokemon>();
        }

        public override async Task LoadAsync()
        {
            try
            {
                Ocupado = true;

                var pokemons = await _ApiService.GetPokemonsAsync();

                Pokemons.Clear();

                foreach (var pokemon in pokemons)
                {
                    pokemon.Image = GetImageStreamFromUrl(pokemon.Sprites.FrontDefault.AbsoluteUri);
                    Pokemons.Add(pokemon);
                }



            }
            catch (Exception ex)
            {

            }
            finally
            {

                Ocupado = false;
            }
        }


        public static byte[] GetImageStreamFromUrl(string url)
        {
            try
            {
                using (var webClient = new HttpClient())
                {
                    var imageBytes = webClient.GetByteArrayAsync(url).Result;

                    return imageBytes;

                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return null;

            }
        }
    }
}
