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
    public class PokemonService     {         private string _Api = "https://pokeapi.co/api/v2/pokemon/";         public async Task<Pokemon> GetPokemon(int id)         {
            try
            {
                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                string api = "";


                var response = await httpClient.
                    GetAsync<Pokemon>($"{_Api}{id}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    response.Value.Image = GetImageStreamFromUrl(response.Value.Sprites.FrontDefault.AbsoluteUri);
                    return response.Value;
                }
                else
                {
                    Debug.WriteLine(response.Error.Message);
                    return null;
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }         }          public byte[] GetImageStreamFromUrl(string url)
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
