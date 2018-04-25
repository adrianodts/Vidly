using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly
{
    public class MappingProfile : Profile
    {
        //public MappingProfile()
        //{
        //    var config = new MapperConfiguration(cfg =>
        //        {
        //            cfg.CreateMap<Movie, MoviesDto>();
        //            cfg.CreateMap<MoviesDto, Movie>();
        //        }
        //    );
        //}

        public MappingProfile()
        {
            CreateMap<Movie, MoviesDto>().ReverseMap();
        }
    }
}