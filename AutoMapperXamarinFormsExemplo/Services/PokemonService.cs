using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapperXamarinFormsExemplo.Models;
using HttpExtension;

namespace AutoMapperXamarinFormsExemplo.Services
{
    public class PokemonService     {         public async Task<List<Pokemon>> GetPokemonsAsync(int max = 20)         {
            List<Pokemon> pokemons = new List<Pokemon>();

            try
            {
                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                string api = "https://pokeapi.co/api/v2/pokemon/";

                for (int i = 1; i < max; i++)
                {
                    var response = await httpClient.
                        GetAsync<Pokemon>($"{api}{i}");

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        pokemons.Add(response.Value);
                    }
                    else
                    {
                        Debug.WriteLine(response.Error.Message);
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return pokemons;         }          public byte[] GetImageStreamFromUrl(string url)
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
        }     } 
}
