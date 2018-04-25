using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public MoviesController()
        {
            this._context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult GetMovies()
        {
            var movieList = this._context.Movies.ToList();
                //.Include(m => m.Genre)

            if (movieList == null || movieList.Count == 0)
                return NotFound();

            return Ok(movieList.Select(Mapper.Map<Movie, MoviesDto>));
        }

        [HttpGet]
        public IHttpActionResult GetMovieById(int id)
        {
            var movie = _context.Movies
                .SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MoviesDto>(movie));
        }

        [HttpPost]
        public IHttpActionResult CreateMovie(MoviesDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();    

            var movie = Mapper.Map<MoviesDto, Movie>(movieDto);

            this._context.Movies.Add(movie);
            this._context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MoviesDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _context.Movies
                .SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            Mapper.Map(movieDto, movieInDb);

            //this because autommaper (movieDto) changes the value of Id field (movieInDb)
            movieInDb.Id = id;

            this._context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult RemoveMovie(int id)
        {
            var movie = this._context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            this._context.Movies.Remove(movie);
            this._context.SaveChanges();

            return Ok();
        }
    }
}