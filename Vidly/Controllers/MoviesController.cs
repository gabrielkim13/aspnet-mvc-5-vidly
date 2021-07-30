using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private List<Movie> Movies = new List<Movie>
        {
            new Movie() { Id = 1, Name = "Shrek" },
            new Movie() { Id = 2, Name = "Wall-e" },
        };

        public ActionResult Index()
        {
            return View(Movies);
        }

        public ActionResult Random()
        {
            var random = new Random();
            var randomIndex = random.Next(0, Movies.Count);
            
            var movie = Movies[randomIndex];
            var customers = new List<Customer>
            {
                new Customer { Name = "Alice" },
                new Customer { Name = "Bob" },
                new Customer { Name = "Carol" },
            };

            var randomMovieViewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            //ViewData["Movie"] = movie;
            //ViewBag.Movie = movie;

            return View(randomMovieViewModel);
        }

        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(string.Format("Release date: {0}/{1}", month, year));
        }
    }
}