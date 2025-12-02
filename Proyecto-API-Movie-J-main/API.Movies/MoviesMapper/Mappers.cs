using API.Movies.DAL.Models;
using API.Movies.DAL.Models.Dtos;
using AutoMapper;

namespace API.Movies.MoviesMapper
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();

            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<Movie, MovieCreateDto>().ReverseMap();
        }
    }
}