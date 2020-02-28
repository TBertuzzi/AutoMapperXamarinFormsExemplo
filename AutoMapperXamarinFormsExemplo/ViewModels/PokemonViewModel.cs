using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using AutoMapperXamarinFormsExemplo.AutoMapper;
using AutoMapperXamarinFormsExemplo.Models;
using AutoMapperXamarinFormsExemplo.Services;
using Xamarin.Forms;

namespace AutoMapperXamarinFormsExemplo.ViewModels
{
    public class PokemonViewModel : BaseViewModel
    {
        PokemonService _ApiService;
        IMapper _mapper;
        int _cont = 1;

        private long _id;
        public long Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        private long _height;
        public long Height
        {
            get { return _height; }
            set
            {
                SetProperty(ref _height, value);
            }
        }

        private byte[] _image;
        public byte[] Image
        {
            get { return _image; }
            set
            {
                SetProperty(ref _image, value);
            }
        }

        public ICommand ObterProximoPokemonCommand { get; }


        public PokemonViewModel()
        {
            Titulo = "AutoMapper Exemplo";
            _ApiService = new PokemonService();

            _mapper = AppBootstrapper.CreateMapper();

            ObterProximoPokemonCommand = new Command(ExecuteObterProximoPokemonCommand);
        }

        private async void ExecuteObterProximoPokemonCommand(object obj)
        {
            if (_cont < 800)
                _cont++;

            await CarregarPokemon(_cont);
        }

        public override async Task LoadAsync()
        {
            await CarregarPokemon(_cont);
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

      
    }
}
