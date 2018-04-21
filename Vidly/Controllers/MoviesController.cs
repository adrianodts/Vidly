using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
                this._context = new ApplicationDbContext();
        }
        //protected readonly List<Movie> movies = new List<Movie>
        //{
        //    new Movie { Id = 1, Name = "Shrek"},
        //    new Movie { Id = 2, Name = "Wall-e"}
        //};

        [Route("movies/all")]
        public ActionResult Random()
        {
            var movie = new Movie()
            {
                Name = "Shrek!"
            };

            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1"},
                new Customer { Name = "Customer 2"}
            };


            var movieViewModel = new MovieViewModel
            {
                Movie = movie,
                //Customers = customers
            };


            return View(movieViewModel);

            //JsonResult json = new JsonResult();
            //json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            //json.Data = movie;

            //return Json(movie, JsonRequestBehavior.AllowGet);

            //return HttpNotFound();

            //return Content("TESTE");

            //return RedirectToAction("Index", "Home");
        }

        [Route("movies/index")]
        public ActionResult Index()
        {
            IEnumerable<Movie> movies = this._context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }

        [Route("movies/detail/{id}")]
        public ActionResult Detail(int? id)
        {
            Movie movie = null;

            if (!id.HasValue || id <= 0)
            {
                return HttpNotFound();
            }

            movie = this._context.Movies.Include(m => m.Genre).FirstOrDefault(x => x.Id == id);

            if (movie == null) return HttpNotFound();

            return View(movie);
        }

        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        [Route("movies/released/{year:regex(\\d{4}):range(2000, 2018)}")]
        public ActionResult ByReleaseYear(int? year)
        {
            if (!year.HasValue)
                year = 2018;

            return Content(year.ToString());
        }


    }
}