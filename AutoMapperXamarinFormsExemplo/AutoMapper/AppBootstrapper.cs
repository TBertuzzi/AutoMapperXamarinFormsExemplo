using System;
using AutoMapper;
using AutoMapperXamarinFormsExemplo.Models;
using AutoMapperXamarinFormsExemplo.ViewModels;

namespace AutoMapperXamarinFormsExemplo.AutoMapper
{
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
}
