using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Vidly.Data;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController :ApiController
    {
        private readonly ApplicationDbContext _dbContext;

        public NewRentalsController()
        {
            this._dbContext = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(RentalDto rentalDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //if (rentalDto.MovieIds.Count == 0)
            //    return BadRequest("No movie Ids have been given.");

            //var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == rentalDto.CustomerId);
            //if (customer == null)
            //    return BadRequest("Customer id is not valid.");
            var customer = _dbContext.Customers.Single(c => c.Id == rentalDto.CustomerId);

            var movies = _dbContext.Movies.Where(m => rentalDto.MovieIds.Contains(m.Id)).ToList();

            //if(movies.Count != rentalDto.MovieIds.Count)
            //    return BadRequest("One or more MovidIds are valid.");

            foreach (var movie in movies)
            {

                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };
                _dbContext.Rental.Add(rental);
            }
            
            _dbContext.SaveChanges();

            return Ok();
        }
    }

    
}