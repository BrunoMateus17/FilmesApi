using AutoMapper;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;

namespace FilmesApi.Profiles
{
  public class FilmeProfile : Profile
  {
    public FilmeProfile()
    {
      CreateMap<CreateFilmeDto, Models.Filme>();
      CreateMap<UpdateFilmeDto, Models.Filme>();
      CreateMap<Filme, UpdateFilmeDto>();
      CreateMap<Filme, ReadFilmeDto>();



    }
  }
}
